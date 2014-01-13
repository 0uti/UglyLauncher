using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;

namespace Minecraft
{
    class Launcher
    {
        public string sPackServer = "http://outi-networks.de/UglyLauncher";
        public string sVersionServer = "http://s3.amazonaws.com/Minecraft.Download/versions";
        public string sLibraryServer = "https://libraries.minecraft.net/";

        public string sStartupLibs = null;

        // Get Package liste from Server
        public MCPacksAvailable GetClientPackList(string MCPlayerName)
        {
            MCPacksAvailable Packs = new MCPacksAvailable();
            string jsonString = Internet.Http.GET(sPackServer + @"/packs.php?player=" + MCPlayerName);

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
            // Getting pack.json
            string sPackJson = File.ReadAllText(UglyLauncher.AppPathes.sPacksDir + @"\" + sPackName + @"\pack.json").Trim();
            MCGameStructure MCLocal = UglyLauncher.JsonHelper.JsonDeserializer<MCGameStructure>(sPackJson);

            // Getting Mojang Version json
            string sVersionJson = Internet.Http.GET(sVersionServer + "/" + MCLocal.id + "/" + MCLocal.id + ".json");
            MCGameStructure MCMojang = UglyLauncher.JsonHelper.JsonDeserializer<MCGameStructure>(sVersionJson);

            // Merging Objects
            MCMojang = this.MergeObjects(MCLocal, MCMojang);

            // Clearing Vars
            sStartupLibs = null;

            // beginn cheking and downloading files
            foreach (MCGameStructure.lib Lib in MCMojang.libraries)
            {
                if (Lib.rules != null)
                {
                    bool bWindows = false;
                    foreach (MCGameStructure.rule Rule in Lib.rules)
                    {
                        if (Rule.action == "allow")
                        {
                            // look at OS;
                            if (Rule.os.name == null || Rule.os.name == "windows") bWindows = true;
                        }
                        if (Rule.action == "disallow")
                        {
                            // look at OS;
                            if (Rule.os.name == "windows") bWindows = false;
                        }

                    }
                    if (bWindows == false) continue;
                }

                

                string[] LibName = Lib.name.Split(':');
                string[] LibOrg = LibName[0].Split('.');
                string sFileName;

                string DownLoadURL = this.sVersionServer;
                string LocalPath = UglyLauncher.AppPathes.sLibraryDir;

                // Appending folders to DownloadURL and create local Folders

                // Package
                foreach (string LibOrgPart in LibOrg)
                {
                    DownLoadURL = DownLoadURL + "/" + LibOrgPart;
                    LocalPath = LocalPath + @"\" + LibOrgPart;
                    if (!Directory.Exists(LocalPath)) Directory.CreateDirectory(LocalPath);
                }

                // Name
                DownLoadURL = DownLoadURL + "/" + LibName[1];
                LocalPath = LocalPath + @"\" + LibName[1];
                if (!Directory.Exists(LocalPath)) Directory.CreateDirectory(LocalPath);

                // version
                DownLoadURL = DownLoadURL + "/" + LibName[2];
                LocalPath = LocalPath + @"\" + LibName[2];
                if (!Directory.Exists(LocalPath)) Directory.CreateDirectory(LocalPath);

                // Filename
                if (Lib.natives.windows == null) sFileName = LibName[1] + "-" + LibName[2] + ".jar";
                else sFileName = LibName[1] + "-" + LibName[2] + "-" + Lib.natives.windows + ".jar";

                // build URL
                DownLoadURL = DownLoadURL + "/" + sFileName;

                // save Lib for Startup
                if (Lib.extract.exclude != null)
                {
                    if (sStartupLibs == null) sStartupLibs = LocalPath + @"\" + sFileName;
                    else sStartupLibs = sStartupLibs + "," + LocalPath + @"\" + sFileName;
                }

                // Download
                

            }








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

                
                if(zipEntry.IsDirectory == false)
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

        public string GetRecommendedVersion(string sPackName)
        {
            int iPackId = -1;
            for (int i = 0; i < Statics.PacksAvailable.packs.Count; i++)
                if (Statics.PacksAvailable.packs[i].name == sPackName) iPackId = i;

            MCPacksAvailable.pack oAvailablePack = new MCPacksAvailable.pack();
            oAvailablePack = Statics.PacksAvailable.packs[iPackId];
            return oAvailablePack.recommended_version;
        }
    }
}
