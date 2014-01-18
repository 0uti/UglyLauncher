﻿using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Drawing;

using UglyLauncher.Internet;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using System.Web.Script.Serialization;
using System.Diagnostics;

namespace UglyLauncher.Minecraft
{
    class Launcher
    {
        // objects
        private UglyLauncher.frm_progressbar bar = new UglyLauncher.frm_progressbar();
        private WebClient Downloader = new WebClient();

        // Statics
        private static MCPacksAvailable PacksAvailable = new MCPacksAvailable();
        private static MCPacksInstalled PacksInstalled = new MCPacksInstalled();
        public static string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string sDataDir = appData + @"\.UglyLauncher";
        public static string sLibraryDir = appData + @"\.UglyLauncher\libraries";
        public static string sAssetsDir = appData + @"\.UglyLauncher\assets";
        public static string sVersionDir = appData + @"\.UglyLauncher\versions";
        public static string sPacksDir = appData + @"\.UglyLauncher\packs";
        public static string sNativesDir = appData + @"\.UglyLauncher\natives";

        // Strings
        public string sPackServer = "http://outi-networks.de/UglyLauncher";
        public string sVersionServer = "http://s3.amazonaws.com/Minecraft.Download/versions";
        public string sLibraryServer = "https://libraries.minecraft.net";
        public string sAssetsIndexServer = "https://s3.amazonaws.com/Minecraft.Download/indexes";
        public string sAssetsFileServer = "http://resources.download.minecraft.net";

        // Lists
        private List<string> lLibraries = new List<string>();

        // constructor
        public Launcher()
        {
            this.Downloader.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
        }

        // Progress event from downloader
        private void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            this.bar.update_bar(e.ProgressPercentage);
        }

        // download file if needed
        private void DownloadFileTo(string sRemotePath, string sLocalPath,bool bShowBar = false,string sBarDisplayText = null)
        {
            if (!File.Exists(sLocalPath))
            {
                if (bShowBar == true)
                {
                    if (this.bar.Visible == false) this.bar.Show();
                    if (sBarDisplayText == null) this.bar.setLabel(sRemotePath.Substring(sRemotePath.LastIndexOf('/') + 1));
                    else this.bar.setLabel(sBarDisplayText);
                }
                this.Downloader.DownloadFileAsync(new Uri(sRemotePath), sLocalPath);
                Application.DoEvents();
                while (this.Downloader.IsBusy)
                    Application.DoEvents();
            }
        }

        // Check Directories
        public void CheckDirectories()
        {
            if (!Directory.Exists(sDataDir)) Directory.CreateDirectory(sDataDir);
            if (!Directory.Exists(sLibraryDir)) Directory.CreateDirectory(sLibraryDir);
            if (!Directory.Exists(sAssetsDir)) Directory.CreateDirectory(sAssetsDir);
            if (!Directory.Exists(sAssetsDir + @"\indexes")) Directory.CreateDirectory(sAssetsDir + @"\indexes");
            if (!Directory.Exists(sAssetsDir + @"\objects")) Directory.CreateDirectory(sAssetsDir + @"\objects");
            if (!Directory.Exists(sAssetsDir + @"\virtual")) Directory.CreateDirectory(sAssetsDir + @"\virtual");
            if (!Directory.Exists(sVersionDir)) Directory.CreateDirectory(sVersionDir);
            if (!Directory.Exists(sPacksDir)) Directory.CreateDirectory(sPacksDir);
            if (!Directory.Exists(sNativesDir)) Directory.CreateDirectory(sNativesDir);
        }

        // load Packlist from server
        public void LoadAvailablePacks(string sPlayerName)
        {
            string sPackListJson = Http.GET(this.sPackServer + @"/packs.php?player=" + sPlayerName);
            PacksAvailable = JsonHelper.JsonDeserializer<MCPacksAvailable>(sPackListJson);
        }

        // Get packe liste
        public MCPacksAvailable GetAvailablePacks()
        {
            return PacksAvailable;
        }

        public MCPacksAvailablePack GetAvailablePack(string sPackName)
        {
            foreach (MCPacksAvailablePack Pack in PacksAvailable.packs)
                if (Pack.name == sPackName) return Pack;
            return null;
        }


        // get pack icon
        public Image GetPackIcon(MCPacksAvailablePack Pack)
        {
            MemoryStream ms = new MemoryStream();
            ms = Http.DownloadToStream(this.sPackServer + @"/packs/" + Pack.name + @"/" + Pack.name + @".png");
            return Image.FromStream(ms);
        }

        // Get installes packages
        public void LoadInstalledPacks()
        {
            List<string> dirs = new List<string>(Directory.EnumerateDirectories(sPacksDir));

            foreach (var dir in dirs)
            {
                if (File.Exists(dir + @"\version") && File.Exists(dir + @"\pack.json"))
                {
                    MCPacksInstalledPack pack = new MCPacksInstalledPack();
                    pack.name = dir.Substring(dir.LastIndexOf("\\") + 1);
                    // Get versions files
                    pack.current_version = File.ReadAllText(dir + @"\version").Trim();
                    if (File.Exists(dir + @"\selected")) pack.selected_version = File.ReadAllText(dir + @"\selected").Trim();
                    else pack.selected_version = "recommended";
                    PacksInstalled.packs.Add(pack);
                }
            }
        }

        // Get pack liste
        public MCPacksInstalled GetInstalledPacks()
        {
            return PacksInstalled;
        }

        public MCPacksInstalledPack GetInstalledPack(string sPackName)
        {
            foreach (MCPacksInstalledPack Pack in PacksInstalled.packs)
                if (Pack.name == sPackName) return Pack;
            return null;
        }

        // Check if Pack is Installed (and the right version)
        public bool IsPackInstalled(string sPackName, string sPackVersion = null)
        {
            // get pack
            MCPacksInstalledPack Pack = this.GetInstalledPack(sPackName);
            // return false if pack not found
            if (Pack == null) return false;
            // return true if no version is given
            if (sPackVersion == null) return true;
            // check version of installed Pack
            // if recommended version, getting the version from available packs
            if (sPackVersion == "recommended") sPackVersion = this.GetRecommendedVersion(sPackName);
            // check if version is installed
            if (Pack.current_version != sPackVersion) return false;
            // Pack is fine :)
            return true;
        }

        public string GetRecommendedVersion(string sPackName)
        {
            MCPacksAvailablePack Pack = this.GetAvailablePack(sPackName);
            return Pack.recommended_version;
        }

        public string GetInstalledPackVersion(string sPackName)
        {
            MCPacksInstalledPack Pack = this.GetInstalledPack(sPackName);
            return Pack.current_version;
        }

        public void StartPack(string sPackName, string sPackVersion)
        {
            // check if pack is installed with given version is installed
            if (!this.IsPackInstalled(sPackName, sPackVersion)) this.InstallPack(sPackName, sPackVersion);
            // getting pack version json file
            MCGameStructure MCLocal = JsonHelper.JsonDeserializer<MCGameStructure>(File.ReadAllText(sPacksDir + @"\" + sPackName + @"\pack.json").Trim());
            // download gameversion and json file if needed
            this.DownloadGameJar(MCLocal);
            // getting mojang version json file
            MCGameStructure MCMojang = JsonHelper.JsonDeserializer<MCGameStructure>(File.ReadAllText(sVersionDir + @"\" + MCLocal.id + @"\" + MCLocal.id + ".json").Trim());
            // fix assets
            if (MCMojang.assets == null) MCMojang.assets = "legacy"; 
            // merging both json objects
            MCGameStructure MC = this.MergeObjects(MCLocal, MCMojang);
            // download libraries if needed
            this.DownloadLibraries(MC);
            // download assets if needed
            this.DownloadAssets(MC);
            // start the pack
            this.Start(this.buildArgs(MC,sPackName));
            // close bar if open
            if (this.bar.Visible == true) this.bar.Hide();
        }

        private void Start(string args)
        {
            string tmpArgs = "\"" + args + "\"";

            ProcessStartInfo processInfo = new ProcessStartInfo("cmd", "/c " + tmpArgs + "> log.txt");
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.WindowStyle = ProcessWindowStyle.Normal;
            processInfo.WorkingDirectory = sDataDir;
            Process.Start(processInfo);
        }

        private string buildArgs(MCGameStructure MC,string sPackName)
        {
            string args = null;

            UglyLauncher.UserManager U = new UglyLauncher.UserManager();
            UglyLauncher.MCUserAccount Acc = U.GetAccount(U.GetDefault());
            UglyLauncher.MCUserAccountProfile Profile = U.GetActiveProfile(Acc);

            args += "java";
            args += " -Xms1024m -Xmx2048m -XX:PermSize=128m";
            // Path to natives
            args += " -Djava.library.path=" + Launcher.sNativesDir + @"\" + MC.id;
            // Libs
            args += " -cp ";
            foreach (string Lib in this.lLibraries)
                args += Lib + ";";
            // version .jar
            args += Launcher.sVersionDir + @"\" + MC.id + @"\" + MC.id + ".jar ";
            // startup class
            args += MC.mainClass;
            // minecraft arguments
            string MCArgs = MC.minecraftArguments;
            MCArgs = MCArgs.Replace("${auth_player_name}", Profile.name);
            MCArgs = MCArgs.Replace("${version_name}", MC.id);
            MCArgs = MCArgs.Replace("${game_directory}", Launcher.sPacksDir + @"\" + sPackName + @"\minecraft");
            MCArgs = MCArgs.Replace("${assets_root}", Launcher.sAssetsDir);
            MCArgs = MCArgs.Replace("${game_assets}", Launcher.sAssetsDir + @"\virtual\legacy");
            MCArgs = MCArgs.Replace("${assets_index_name}", MC.assets);
            MCArgs = MCArgs.Replace("${auth_uuid}", Profile.id);
            MCArgs = MCArgs.Replace("${auth_access_token}", Acc.accessToken);
            MCArgs = MCArgs.Replace("${user_properties}", "{}");
            MCArgs = MCArgs.Replace("${user_type}", "Mojang");
            args += " " + MCArgs;
            return args;
        }


        private void DownloadLibraries(MCGameStructure MC)
        {
            foreach (MCGameStructureLib Lib in MC.libraries)
            {
                string DownLoadURL = this.sLibraryServer;
                string LocalPath = sLibraryDir;

                // Custom DownloadURL
                if (Lib.url != null) DownLoadURL = Lib.url;
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
                    sFileName = LibName[1] + "-" + LibName[2] + "-" + Lib.natives.windows;
                    sFileName = sFileName.Replace("${arch}", "64");
                }
                else sFileName = LibName[1] + "-" + LibName[2];

                if (Lib.nameappend != null) sFileName += Lib.nameappend;

                sFileName += ".jar";


                // build URL and pathes
                DownLoadURL += "/" + sFileName;
                LocalPath += @"\" + sFileName;

                // download file if needed
                DownloadFileTo(DownLoadURL, LocalPath);

                // extract pack if needed
                if (Lib.extract != null)
                {
                    if (!Directory.Exists(sNativesDir + @"\" + MC.id)) Directory.CreateDirectory(sNativesDir + @"\" + MC.id);
                    ExtractZipFile(LocalPath, sNativesDir + @"\" + MC.id, "");
                }
                else
                {
                    // files need for startup
                    this.lLibraries.Add(LocalPath);
                }

            }
        }

        private void DownloadGameJar(MCGameStructure MC)
        {
            // create directory if not exists
            if (!Directory.Exists(sVersionDir + @"\" + MC.id)) Directory.CreateDirectory(sVersionDir + @"\" + MC.id);
            // download jar
            DownloadFileTo(this.sVersionServer + "/" + MC.id + "/" + MC.id + ".jar", sVersionDir + @"\" + MC.id + @"\" + MC.id + ".jar");
            // download json
            DownloadFileTo(this.sVersionServer + "/" + MC.id + "/" + MC.id + ".json", sVersionDir + @"\" + MC.id + @"\" + MC.id + ".json");
        }

        private void DownloadAssets(MCGameStructure MC)
        {
            // download asset json if needed
            if (!File.Exists(sAssetsDir + @"\indexes\" + MC.assets + ".json"))
                DownloadFileTo(this.sAssetsIndexServer + "/" + MC.assets + ".json", sAssetsDir + @"\indexes\" + MC.assets + ".json");
            // getting asset jason
            MCAssets Assets = new JavaScriptSerializer().Deserialize<MCAssets>(File.ReadAllText(sAssetsDir + @"\indexes\" + MC.assets + ".json").Trim());

            foreach (KeyValuePair<string, MCAssetsObject> Asset in Assets.objects)
            {
                string sRemotePath = this.sAssetsFileServer + "/";
                string sLocalPath = sAssetsDir + @"\objects\";
                string slegacyPath = sAssetsDir + @"\virtual\legacy\";

                sRemotePath += Asset.Value.hash.Substring(0, 2);
                sRemotePath += "/" + Asset.Value.hash;

                sLocalPath += Asset.Value.hash.Substring(0, 2);
                if (!Directory.Exists(sLocalPath)) Directory.CreateDirectory(sLocalPath);
                sLocalPath += @"\" + Asset.Value.hash;
                DownloadFileTo(sRemotePath, sLocalPath);

                if (MC.assets == "legacy")
                {
                    slegacyPath += Asset.Key.Replace('/', '\\');
                    if (!Directory.Exists(slegacyPath.Substring(0, slegacyPath.LastIndexOf('\\')))) Directory.CreateDirectory(slegacyPath.Substring(0, slegacyPath.LastIndexOf('\\')));
                    File.Copy(sLocalPath, slegacyPath, true);
                }
            }
        }

        private MCGameStructure MergeObjects(MCGameStructure VersionPack, MCGameStructure VersionMojang)
        {
            if (VersionPack.libraries != null)
            {
                foreach (MCGameStructureLib Lib in VersionPack.libraries)
                    VersionMojang.libraries.Add(Lib);
            }
            if (VersionPack.mainClass != null) VersionMojang.mainClass = VersionPack.mainClass;
            if (VersionPack.minecraftArguments != null) VersionMojang.minecraftArguments = VersionPack.minecraftArguments;
            return VersionMojang;
        }

        private void InstallPack(string sPackName, string sPackVersion)
        {
            //delete pack if installed
            if (this.IsPackInstalled(sPackName)) this.DeletePack(sPackName);
            // if recommended version, getting the version from available packs
            if (sPackVersion == "recommended") sPackVersion = GetRecommendedVersion(sPackName);
            //download pack
            this.DownloadFileTo(this.sPackServer + "/packs/" + sPackName + "/" + sPackName + "-" + sPackVersion + ".zip", sPacksDir + @"\" + sPackName + "-" + sPackVersion + ".zip", true, "Downloading Pack " + sPackName);
            this.bar.Hide();
            //unzip pack
            this.ExtractZipFile(sPacksDir + @"\" + sPackName + "-" + sPackVersion + ".zip", sPacksDir);
            //delete zip file
            File.Delete(sPacksDir + @"\" + sPackName + "-" + sPackVersion + ".zip");
        }

        private void DeletePack(string sPackName)
        {
            string PackDir = Launcher.sPacksDir + @"\" + sPackName;

            if (!Directory.Exists(PackDir)) return; // Pack did not exist
            // Delete Directories
            if (Directory.Exists(PackDir + @"\minecraft\logs")) Directory.Delete(PackDir + @"\minecraft\logs", true);
            if (Directory.Exists(PackDir + @"\minecraft\mods")) Directory.Delete(PackDir + @"\minecraft\mods", true);
            if (Directory.Exists(PackDir + @"\minecraft\config")) Directory.Delete(PackDir + @"\minecraft\config", true);
            if (Directory.Exists(PackDir + @"\minecraft\stats")) Directory.Delete(PackDir + @"\minecraft\stats", true);
            if (Directory.Exists(PackDir + @"\minecraft\crash-reports")) Directory.Delete(PackDir + @"\minecraft\crash-reports", true);
            if (Directory.Exists(PackDir + @"\minecraft\resourcepacks")) Directory.Delete(PackDir + @"\minecraft\resourcepacks", true);
            if (Directory.Exists(PackDir + @"\minecraft\CustomDISkins")) Directory.Delete(PackDir + @"\minecraft\CustomDISkins", true);
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

        private void ExtractZipFile(string archiveFilenameIn, string outFolder, string exclude = null)
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





    }



    /*
     * Structures
     */
    
    /// <summary>
    /// The JSON Client Pack construct.
    /// </summary>
    [DataContract]
    public class MCPacksAvailable
    {
        [DataMember]
        public List<MCPacksAvailablePack> packs = new List<MCPacksAvailablePack>();
    }

    [DataContract]
    public class MCPacksAvailablePack
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string recommended_version { get; set; }
        [DataMember]
        public bool autoupdate { get; set; }
        [DataMember]
        public List<String> versions { get; set; }
    }


    /// <summary>
    /// The JSON Client installed Pack construct.
    /// </summary>
    [DataContract]
    public class MCPacksInstalled
    {
        [DataMember]
        public List<MCPacksInstalledPack> packs = new List<MCPacksInstalledPack>();
    }

    [DataContract]
    public class MCPacksInstalledPack
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string current_version { get; set; } // Version of the package
        [DataMember]
        public string selected_version { get; set; } // selected version in Launcher window (recommended check)
    }


    /// <summary>
    /// The JSON Minecraft Game Structure.
    /// </summary>
    [DataContract]
    public class MCGameStructure
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string time { get; set; }
        [DataMember]
        public string releaseTime { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string minecraftArguments { get; set; }
        [DataMember]
        public string minimumLauncherVersion { get; set; }
        [DataMember]
        public string assets { get; set; }
        [DataMember]
        public string mainClass { get; set; }
        [DataMember]
        public List<MCGameStructureLib> libraries = new List<MCGameStructureLib>();
    }

    [DataContract]
    public class MCGameStructureLibExtract
    {
        [DataMember]
        public List<string> exclude;
    }

    [DataContract]
    public class MCGameStructureLibRuleOS
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string version { get; set; }
    }

    [DataContract]
    public class MCGameStructureLibRule
    {
        [DataMember]
        public string action { get; set; }
        [DataMember]
        public MCGameStructureLibRuleOS os;
    }

    [DataContract]
    public class MCGameStructureLibNative
    {
        [DataMember]
        public string linux { get; set; }
        [DataMember]
        public string windows { get; set; }
        [DataMember]
        public string osx { get; set; }
    }

    [DataContract]
    public class MCGameStructureLib
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public string nameappend { get; set; }
        [DataMember]
        public string fullurl { get; set; }
        [DataMember]
        public List<MCGameStructureLibRule> rules;
        [DataMember]
        public MCGameStructureLibNative natives;
        [DataMember]
        public MCGameStructureLibExtract extract;
    }


    [DataContract]
    public class MCAssets
    {
        [DataMember]
        public string @virtual { get; set; }
        [DataMember]
        public Dictionary<string, MCAssetsObject> objects;

    }

    [DataContract]
    public class MCAssetsObject
    {
        [DataMember]
        public string hash { get; set; }
        [DataMember]
        public string size { get; set; }
    }

}