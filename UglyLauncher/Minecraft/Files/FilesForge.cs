using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UglyLauncher.Internet;
using UglyLauncher.Minecraft.Files.Json.GameVersion;
using UglyLauncher.Minecraft.Files.Json.ForgeInstaller;
using UglyLauncher.Minecraft.Files.Json.ForgeVersion;

namespace UglyLauncher.Minecraft.Files
{
    class FilesForge
    {
        private DownloadHelper dhelper;

        private readonly string _sForgeTree = "/net/minecraftforge/forge/";
        private readonly string _sForgeMaven = "https://files.minecraftforge.net/maven";
        public string LibraryDir { get; set; }
        public bool OfflineMode { get; set; }

        private string sForgeVersion;

        private bool post_1_13;

        public FilesForge(DownloadHelper dhelper)
        {
            this.dhelper = dhelper;
        }

        public Dictionary<string, string> InstallForge(string sForgeVersion)
        {
            this.sForgeVersion = sForgeVersion;

            Dictionary<string, string> ClassPath = new Dictionary<string, string>(); // Library list for startup
            string localPath = LibraryDir + _sForgeTree.Replace('/', '\\') + sForgeVersion;
            string localFile = localPath + @"\forge-" + sForgeVersion + "-installer.jar";
            string remoteFile = _sForgeMaven + _sForgeTree + sForgeVersion + "/forge-" + sForgeVersion + "-installer.jar";

            //check if file exists
            if (!File.Exists(localFile))
            {
                dhelper.DownloadFileTo(remoteFile, localFile, true, null);
            }

            // always extract files
            List<string> extractList = new List<string>
            {
                "install_profile.json",
                "version.json",
                "forge-" + sForgeVersion + "-universal.jar"
            };
            dhelper.ExtractZipFiles(localFile, localPath, extractList);

            // post 1.13 files
            if (File.Exists(LibraryDir + _sForgeTree.Replace('/', '\\') + sForgeVersion + @"\version.json"))
            {
                post_1_13 = true;
                ForgeVersion MCForge = ForgeVersion.FromJson(File.ReadAllText(LibraryDir + _sForgeTree.Replace('/', '\\') + sForgeVersion + @"\version.json").Trim());
                // download Forge libraries
                ClassPath = DownloadForgeLibraries(MCForge);
            }
            // pre 1.13 files
            else if (File.Exists(LibraryDir + _sForgeTree.Replace('/', '\\') + sForgeVersion + @"\install_profile.json"))
            {
                post_1_13 = false;
                ForgeInstaller MCForge = ForgeInstaller.FromJson(File.ReadAllText(LibraryDir + _sForgeTree.Replace('/', '\\') + sForgeVersion + @"\install_profile.json").Trim());
                // download Forge libraries
                ClassPath = DownloadForgeLibraries(MCForge);
            }

            // append Forge to classpath
            ClassPath.Add("net.minecraftforge:forge", localPath + @"\forge-" + sForgeVersion + "-universal.jar");

            return ClassPath;
        }

        public GameVersion MergeArguments(GameVersion MCMojang)
        {
            if (post_1_13 == true)
            {
                ForgeVersion MCForge = ForgeVersion.FromJson(File.ReadAllText(LibraryDir + _sForgeTree.Replace('/', '\\') + sForgeVersion + @"\version.json").Trim());
                // replace vanilla values
                MCMojang.MainClass = MCForge.MainClass;
                // append forge arguments
                List<GameElement> itemList = MCMojang.Arguments.Game.ToList();
                List<GameElement> moreItems = MCForge.Arguments.Game.ToList();
                itemList.AddRange(moreItems);
                MCMojang.Arguments.Game = itemList.ToArray();
            }
            else
            {
                ForgeInstaller MCForge = ForgeInstaller.FromJson(File.ReadAllText(LibraryDir + _sForgeTree.Replace('/', '\\') + sForgeVersion + @"\install_profile.json").Trim());
                // replace vanilla settings
                MCMojang.MainClass = MCForge.VersionInfo.MainClass;
                MCMojang.MinecraftArguments = MCForge.VersionInfo.MinecraftArguments;
            }
            return MCMojang;
        }
        
        private Dictionary<string, string> DownloadForgeLibraries(ForgeInstaller Forge)
        {
            Dictionary<string, string> ClassPath = new Dictionary<string, string>(); // Library list for startup

            foreach (Json.ForgeInstaller.Library Lib in Forge.VersionInfo.Libraries)
            {
                string sLocalPath = LibraryDir;
                string sRemotePath = "https://libraries.minecraft.net/";
                string sLibPath = null;
                string[] sLibName = Lib.Name.Split(':');

                // use download url from Forge
                if (Lib.Url != null) sRemotePath = Lib.Url.ToString();

                // fix for typesafe libraries
                if (sLibName[0].Contains("com.typesafe"))
                {
                    sRemotePath = "http://central.maven.org/maven2/";
                }

                sLibPath = string.Format("{0}/{1}/{2}/{1}-{2}.jar", sLibName[0].Replace('.', '/'), sLibName[1], sLibName[2]);
                sLocalPath += @"\" + sLibPath.Replace('/', '\\');
                sRemotePath += sLibPath;

                // dont download Forge itself
                if (!sLibName[0].Equals("net.minecraftforge") || !sLibName[1].Equals("forge"))
                {
                    dhelper.DownloadFileTo(sRemotePath, sLocalPath, true);

                    // add to classpath (replace)
                    if (ClassPath.ContainsKey(sLibName[0] + ":" + sLibName[1]))
                    {
                        ClassPath.Remove(sLibName[0] + ":" + sLibName[1]);
                    }
                    ClassPath.Add(sLibName[0] + ":" + sLibName[1], sLocalPath);
                }
            }
            return ClassPath;
        }

        private Dictionary<string, string> DownloadForgeLibraries(ForgeVersion forge)
        {
            Dictionary<string, string> ClassPath = new Dictionary<string, string>(); // Library list for startup

            foreach (Files.Json.GameVersion.Library lib in forge.Libraries)
            {
                string[] sLibName = lib.Name.Split(':');
                VersionJsonDownload download;

                // dont download Forge itself
                if (sLibName[0].Equals("net.minecraftforge") && sLibName[1].Equals("forge")) continue;

                download = lib.Downloads.Artifact;
                download.Path = LibraryDir + @"\" + download.Path.Replace("/", @"\");

                // fix for typesafe libraries
                if (sLibName[0].Contains("org.apache.logging.log4j"))
                {
                    download.Url = new Uri("http://central.maven.org/maven2/" + sLibName[0].Replace('.', '/') + "/" + sLibName[1] + "/" + sLibName[2] + "/" + sLibName[1] + "-" + sLibName[2] + ".jar");
                }
                dhelper.DownloadFileTo(download.Url, download.Path);

                // fix for modlauncher (api)
                if (sLibName.Length == 4 && sLibName[0].Equals("cpw.mods") && sLibName[1].Equals("modlauncher") && sLibName[3].Equals("api"))
                {
                    sLibName[1] += "-api";
                    continue;
                }

                // add to classpath (replace)
                ClassPath.Add(sLibName[0] + ":" + sLibName[1], download.Path);
            }
            return ClassPath;
        }
    }
}
