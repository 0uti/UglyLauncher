using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

using Internet;

using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using System.Windows.Forms;
using System.Diagnostics;

namespace Minecraft
{
    public class Launcher
    {
        public string sPackServer = "http://outi-networks.de/UglyLauncher";
        public string sVersionServer = "http://s3.amazonaws.com/Minecraft.Download/versions";
        public string sLibraryServer = "https://libraries.minecraft.net";
        public string sAssetsIndexServer = "https://s3.amazonaws.com/Minecraft.Download/indexes";
        public string sAssetsFileServer = "http://resources.download.minecraft.net";
        
        private static UglyLauncher.frm_progressbar bar = new UglyLauncher.frm_progressbar();
        private WebClient Downloader = new WebClient();

        // constructor
        public Launcher()
        {
            this.Downloader.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
        }

        // Get Package liste from Server
        public MCPacksAvailable GetClientPackList(string MCPlayerName)
        {
            MCPacksAvailable Packs = new MCPacksAvailable();
            Http H = new Http();
            string jsonString = H.GET(sPackServer + @"/packs.php?player=" + MCPlayerName);

            Packs = UglyLauncher.JsonHelper.JsonDeserializer<MCPacksAvailable>(jsonString);
            return Packs;
        }

        // Get installes packages
        public MCPacksInstalled GetInstalledPacks()
        {
            MCPacksInstalled Packs = new MCPacksInstalled();
            List<string> dirs = new List<string>(Directory.EnumerateDirectories(UglyLauncher.AppPathes.sPacksDir));

            foreach (var dir in dirs)
            {
                if (File.Exists(dir + @"\version") && File.Exists(dir + @"\pack.json"))
                {
                    MCPacksInstalled.pack pack = new MCPacksInstalled.pack();
                    pack.name = dir.Substring(dir.LastIndexOf("\\") + 1);
                    // Get versions files
                    pack.current_version = File.ReadAllText(dir + @"\version").Trim();
                    if (File.Exists(dir + @"\selected")) pack.selected_version = File.ReadAllText(dir + @"\selected").Trim();
                    else pack.selected_version = "recommended";
                    Packs.packs.Add(pack);
                }
            }
            return Packs;
        }

        // Check if Pack is Installed (and the right version)
        public bool IsPackInstalled(string sPackName, string sPackVersion)
        {
            // find Pack in List
            int iPackId = -1;
            for (int i = 0; i < Statics.PacksInstalled.packs.Count; i++)
                if (Statics.PacksInstalled.packs[i].name == sPackName) iPackId = i;

            // return false if pack not found
            if (iPackId == -1) return false;

            MCPacksInstalled.pack oInstalledPack = new MCPacksInstalled.pack();
            oInstalledPack = Statics.PacksInstalled.packs[iPackId];

            // if recommended version, getting the version from available packs
            if (sPackVersion == "recommended") sPackVersion = GetRecommendedVersion(sPackName);
           
            // check if version is installed
            if (oInstalledPack.current_version != sPackVersion) return false;

            // Pack is fine :)
            return true;
        }

        // prepare Pack
        public void PreparePack(string sPackName)
        {
            Http H = new Http();
            
            // Getting pack.json
            string sPackJson = File.ReadAllText(UglyLauncher.AppPathes.sPacksDir + @"\" + sPackName + @"\pack.json").Trim();
            MCGameStructure MCLocal = UglyLauncher.JsonHelper.JsonDeserializer<MCGameStructure>(sPackJson);

            // Getting Mojang Version json
            string sVersionJson = H.GET(sVersionServer + "/" + MCLocal.id + "/" + MCLocal.id + ".json");
            MCGameStructure MCMojang = UglyLauncher.JsonHelper.JsonDeserializer<MCGameStructure>(sVersionJson);

            // fix assets
            if (MCMojang.assets == null) MCMojang.assets = "legacy"; 

            // Merging Objects
            MCGameStructure MCMerge = this.MergeObjects(MCLocal, MCMojang);

            // checking and downloading version files
            DownloadGameJar(MCMerge);
            
            // checking and downloading libraries
            DownloadLibs(MCMerge);

            // checking and downloading assets
            DownloadAssets(MCMerge);
            
        }

        private void DownloadAssets(MCGameStructure MC)
        {
            Http H = new Http();
            // Getting assets json (version or legacy)
            string sVersionJson = H.GET(sAssetsIndexServer + "/" + MC.assets + ".json");
        }

        public void StartPack(string sPackName)
        {
            // Getting pack.json
            string sPackJson = File.ReadAllText(UglyLauncher.AppPathes.sPacksDir + @"\" + sPackName + @"\pack.json").Trim();
            MCGameStructure MCLocal = UglyLauncher.JsonHelper.JsonDeserializer<MCGameStructure>(sPackJson);

            // Getting downloaded Mojang version json
            string sVersionJson = File.ReadAllText(UglyLauncher.AppPathes.sVersionDir + @"\" + MCLocal.id + @"\" + MCLocal.id + ".json").Trim();
            MCGameStructure MCMojang = UglyLauncher.JsonHelper.JsonDeserializer<MCGameStructure>(sVersionJson);

            // fix assets
            if (MCMojang.assets == null) MCMojang.assets = "legacy";

            // Merging Objects
            MCGameStructure MCMerge = this.MergeObjects(MCLocal, MCMojang);

            string args = "";

            args += "java";
            
            // Path to natives
            args += " -Djava.library.path=" + UglyLauncher.AppPathes.sNativesDir + @"\" + MCMerge.id;

            // Libs
            args += " -cp \"";
            args += UglyLauncher.AppPathes.sVersionDir + @"\" + MCMerge.id + @"\" + MCMerge.id + ".jar";

            foreach (MCGameStructureLib Lib in MCMerge.libraries)
            {
                string LocalPath = UglyLauncher.AppPathes.sLibraryDir;
                // Checking rules
                if (Lib.rules != null)
                {
                    bool bWindows = false;
                    foreach (MCGameStructureLibRule Rule in Lib.rules)
                    {
                        if (Rule.action == "allow")
                        {
                            if (Rule.os == null) bWindows = true;
                            else if (Rule.os.name == null || Rule.os.name == "windows") bWindows = true;
                        }
                        if (Rule.action == "disallow" && Rule.os.name == "windows") bWindows = false;
                    }
                    if (bWindows == false) continue;
                }

                string[] LibName = Lib.name.Split(':');
                string[] LibOrg = LibName[0].Split('.');
                string sFileName = null;

                // create package directories
                foreach (string LibOrgPart in LibOrg)
                    LocalPath += @"\" + LibOrgPart;
                // create name directory
                LocalPath += @"\" + LibName[1];
                // create version directory
                LocalPath += @"\" + LibName[2];

                // filename
                if (Lib.natives != null)
                {
                    sFileName = LibName[1] + "-" + LibName[2] + "-" + Lib.natives.windows + ".jar";
                    sFileName = sFileName.Replace("${arch}", "64");
                }
                else sFileName = LibName[1] + "-" + LibName[2] + ".jar";

                LocalPath += @"\" + sFileName;
                if (Lib.extract == null) args += ";" + LocalPath;
            }

            // startup class
            args += "\" " + MCMerge.mainClass;

            this.Start(args);


        }



        

        private void DownloadGameJar(MCGameStructure MC)
        {
            string DownLoadURL = this.sVersionServer + "/" + MC.id;
            string LocalPath = UglyLauncher.AppPathes.sVersionDir + @"\" + MC.id;

            // create directory if not exists
            if (!Directory.Exists(LocalPath)) Directory.CreateDirectory(LocalPath);

            // download jar
            DownloadFileTo(DownLoadURL + "/" + MC.id + ".jar", LocalPath + @"\" + MC.id + ".jar");

            // download json
            DownloadFileTo(DownLoadURL + "/" + MC.id + ".json", LocalPath + @"\" + MC.id + ".json");
        }

        private void DownloadFileTo(string sRemotePath, string sLocalPath)
        {
            // download file if needed
            if (!File.Exists(sLocalPath))
            {
                if (bar.Visible == false) bar.Show();
                bar.setLabel(sRemotePath.Substring(sRemotePath.LastIndexOf('/') + 1));
                Downloader.DownloadFileAsync(new Uri(sRemotePath), sLocalPath);
                Application.DoEvents();
                while (Downloader.IsBusy)
                    Application.DoEvents();
            }
        }

        private void DownloadLibs(MCGameStructure MC)
        {
            foreach (MCGameStructureLib Lib in MC.libraries)
            {
                string DownLoadURL = this.sLibraryServer;
                string LocalPath = UglyLauncher.AppPathes.sLibraryDir;

                // Checking rules
                if (Lib.rules != null)
                {
                    bool bWindows = false;
                    foreach (MCGameStructureLibRule Rule in Lib.rules)
                    {
                        if (Rule.action == "allow")
                        {
                            if (Rule.os == null) bWindows = true;
                            else if (Rule.os.name == null || Rule.os.name == "windows") bWindows = true;
                        }
                        if (Rule.action == "disallow" && Rule.os.name == "windows") bWindows = false;
                    }
                    if (bWindows == false) continue;
                }

                string[] LibName = Lib.name.Split(':');
                string[] LibOrg = LibName[0].Split('.');
                string sFileName = null;

                // create package directories
                foreach (string LibOrgPart in LibOrg)
                {
                    DownLoadURL += "/" + LibOrgPart;
                    LocalPath += @"\" + LibOrgPart;
                    if (!Directory.Exists(LocalPath)) Directory.CreateDirectory(LocalPath);
                }

                // create name directory
                DownLoadURL += "/" + LibName[1];
                LocalPath += @"\" + LibName[1];
                if (!Directory.Exists(LocalPath)) Directory.CreateDirectory(LocalPath);

                // create version directory
                DownLoadURL += "/" + LibName[2];
                LocalPath += @"\" + LibName[2];
                if (!Directory.Exists(LocalPath)) Directory.CreateDirectory(LocalPath);

                // filename
                if (Lib.natives != null)
                {
                    sFileName = LibName[1] + "-" + LibName[2] + "-" + Lib.natives.windows + ".jar";
                    sFileName = sFileName.Replace("${arch}", "64");
                }
                else sFileName = LibName[1] + "-" + LibName[2] + ".jar";

                // build URL and pathes
                DownLoadURL += "/" + sFileName;
                LocalPath += @"\" + sFileName;

                // download file if needed
                if (!File.Exists(LocalPath))
                {
                    if (bar.Visible == false) bar.Show();
                    bar.setLabel(sFileName);
                    Downloader.DownloadFileAsync(new Uri(DownLoadURL), LocalPath);
                    Application.DoEvents();
                    while (Downloader.IsBusy)
                        Application.DoEvents();
                }

                // extract pack if needed
                if (Lib.extract != null)
                {
                    if (!Directory.Exists(UglyLauncher.AppPathes.sNativesDir + @"\" + MC.id)) Directory.CreateDirectory(UglyLauncher.AppPathes.sNativesDir + @"\" + MC.id);
                    ExtractZipFile(LocalPath, UglyLauncher.AppPathes.sNativesDir + @"\" + MC.id, "");

                }
            }
            if (bar.Visible == true) bar.Hide();
        }

        void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            bar.update_bar(e.ProgressPercentage);
        }

        // install Pack
        public void InstallPack(string sPackName, string sPackVersion)
        {
            // Delete old Pack (version) if exists
            this.PackDelete(sPackName);

            // if recommended version, getting the version from available packs
            if (sPackVersion == "recommended") sPackVersion = GetRecommendedVersion(sPackName);

            // Download and unzip the Pack
            WebClient wc = new WebClient();
            Stream data = wc.OpenRead(this.sPackServer + "/packs/" + sPackName + "/" + sPackName + "-" + sPackVersion + ".zip");
            this.UnzipFromStream(data,UglyLauncher.AppPathes.sPacksDir);
        }





        private MCGameStructure MergeObjects(MCGameStructure oPack, MCGameStructure oMojang)
        {

            return oMojang; // for vanilla ok :)
        }


        private void PackDelete(string sPackname)
        {
            string PackDir = UglyLauncher.AppPathes.sPacksDir + @"\" + sPackname;

            if (!Directory.Exists(PackDir)) return; // Pack did not exist
            // Delete Directories
            if (Directory.Exists(PackDir + @"\minecraft\logs")) Directory.Delete(PackDir + @"\minecraft\logs");
            if (Directory.Exists(PackDir + @"\minecraft\mods")) Directory.Delete(PackDir + @"\minecraft\mods");
            if (Directory.Exists(PackDir + @"\minecraft\config")) Directory.Delete(PackDir + @"\minecraft\config");
            if (Directory.Exists(PackDir + @"\minecraft\stats")) Directory.Delete(PackDir + @"\minecraft\stats");
            if (Directory.Exists(PackDir + @"\minecraft\crash-reports")) Directory.Delete(PackDir + @"\minecraft\crash-reports");
            if (Directory.Exists(PackDir + @"\minecraft\resourcepacks")) Directory.Delete(PackDir + @"\minecraft\resourcepacks");
            if (Directory.Exists(PackDir + @"\minecraft\CustomDISkins")) Directory.Delete(PackDir + @"\minecraft\CustomDISkins");

            // Deleting .log Files
            foreach (FileInfo f in new DirectoryInfo(PackDir + @"\minecraft").GetFiles("*.log"))
                f.Delete();

            // Deleting .lck Files
            foreach (FileInfo f in new DirectoryInfo(PackDir + @"\minecraft").GetFiles("*.lck"))
                f.Delete();

            // Deleting .1 Files
            foreach (FileInfo f in new DirectoryInfo(PackDir + @"\minecraft").GetFiles("*.1"))
                f.Delete();
            
            // Deleting minecraft\options.txt
            if (File.Exists(PackDir + @"\minecraft\options.txt")) File.Delete(PackDir + @"\minecraft\options.txt");

            // Deleting pack.json
            if (File.Exists(PackDir + @"\pack.json")) File.Delete(PackDir + @"\pack.json");

            // Deleting version
            if (File.Exists(PackDir + @"\version")) File.Delete(PackDir + @"\version");

            // Deleting selected
            if (File.Exists(PackDir + @"\selected")) File.Delete(PackDir + @"\selected");
        }

        private void UnzipFromStream(Stream zipStream, string outFolder)
        {

            ZipInputStream zipInputStream = new ZipInputStream(zipStream);
            ZipEntry zipEntry = zipInputStream.GetNextEntry();
            while (zipEntry != null)
            {
                String entryFileName = zipEntry.Name;
                // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                // Optionally match entrynames against a selection list here to skip as desired.
                // The unpacked length is available in the zipEntry.Size property.

                byte[] buffer = new byte[4096];     // 4K is optimum

                // Manipulate the output filename here as desired.
                String fullZipToPath = Path.Combine(outFolder, entryFileName);
                string directoryName = Path.GetDirectoryName(fullZipToPath);
                if (directoryName.Length > 0)
                    Directory.CreateDirectory(directoryName);

                
                if(!zipEntry.IsDirectory)
                {
                    // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                    // of the file, but does not waste memory.
                    // The "using" will close the stream even if an exception occurs.
                    using (FileStream streamWriter = File.Create(fullZipToPath))
                    {
                        StreamUtils.Copy(zipInputStream, streamWriter, buffer);
                    }
                }
                zipEntry = zipInputStream.GetNextEntry();
            }
        }

        public void ExtractZipFile(string archiveFilenameIn, string outFolder, string exclude)
        {
            ZipFile zf = null;
            try
            {
                FileStream fs = File.OpenRead(archiveFilenameIn);
                zf = new ZipFile(fs);
                foreach (ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile)
                    {
                        continue;           // Ignore directories
                    }
                    String entryFileName = zipEntry.Name;
                    // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                    // Optionally match entrynames against a selection list here to skip as desired.
                    // The unpacked length is available in the zipEntry.Size property.

                    byte[] buffer = new byte[4096];     // 4K is optimum
                    Stream zipStream = zf.GetInputStream(zipEntry);

                    // Manipulate the output filename here as desired.
                    String fullZipToPath = Path.Combine(outFolder, entryFileName);
                    string directoryName = Path.GetDirectoryName(fullZipToPath);
                    if (directoryName.Length > 0)
                        Directory.CreateDirectory(directoryName);

                    if (!zipEntry.IsDirectory)
                    {
                        // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                        // of the file, but does not waste memory.
                        // The "using" will close the stream even if an exception occurs.
                        using (FileStream streamWriter = File.Create(fullZipToPath))
                        {
                            StreamUtils.Copy(zipStream, streamWriter, buffer);
                        }
                    }
                }
            }
            finally
            {
                if (zf != null)
                {
                    zf.IsStreamOwner = true; // Makes close also shut the underlying stream
                    zf.Close(); // Ensure we release resources
                }
            }
        }




        public string GetRecommendedVersion(string sPackName)
        {
            int iPackId = -1;
            for (int i = 0; i < Statics.PacksAvailable.packs.Count; i++)
                if (Statics.PacksAvailable.packs[i].name == sPackName) iPackId = i;

            MCPacksAvailable.pack oAvailablePack = new MCPacksAvailable.pack();
            oAvailablePack = Statics.PacksAvailable.packs[iPackId];
            return oAvailablePack.recommended_version;
        }



        public void Start(string args)
        {
            string tmpArgs = "\"" + args + "\"";

            ProcessStartInfo processInfo = new ProcessStartInfo("cmd", "/c " + tmpArgs);
            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = false;
            processInfo.WindowStyle = ProcessWindowStyle.Normal;
            processInfo.WorkingDirectory = UglyLauncher.AppPathes.sDataDir;
            Process.Start(processInfo);


        }



    }
}
