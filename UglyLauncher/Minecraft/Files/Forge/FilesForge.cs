﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using UglyLauncher.Internet;
using UglyLauncher.Minecraft.Files.Mojang.GameVersion;
using UglyLauncher.Settings;

namespace UglyLauncher.Minecraft.Files.Forge
{
    class FilesForge
    {
        private readonly DownloadHelper dhelper;
        private readonly StartupSide side;
        private readonly string _sForgeTree = "/net/minecraftforge/forge/";
        private readonly string _sForgeMaven = "https://files.minecraftforge.net/maven";
        public string LibraryDir { get; set; }
        public string VersionDir { get; set; }
        public bool OfflineMode { get; set; }
        public string JavaPath { get; set; }

        private string sForgeVersion;
        private string sForgeTempDir;
        private string sForgeInstallerFile;
        private string sForgeVersionDir;

        private bool post_1_13;

        public FilesForge(DownloadHelper dhelper, StartupSide side)
        {
            this.dhelper = dhelper;
            this.side = side;
        }

        public Dictionary<string, string> InstallForge(string sForgeVersion)
        {
            this.sForgeVersion = sForgeVersion;

            Dictionary<string, string> ClassPath = new Dictionary<string, string>(); // Library list for startup
            sForgeVersionDir = LibraryDir + _sForgeTree.Replace('/', '\\') + sForgeVersion;
            sForgeInstallerFile = sForgeVersionDir + @"\forge-" + sForgeVersion + "-installer.jar";
            string remoteFile = _sForgeMaven + _sForgeTree + sForgeVersion + "/forge-" + sForgeVersion + "-installer.jar";

            //check if file exists
            if (!File.Exists(sForgeInstallerFile))
            {
                dhelper.DownloadFileTo(remoteFile, sForgeInstallerFile, true, null);
            }

            // always extract files
            List<string> extractList = new List<string>
            {
                "install_profile.json",
                "version.json"
            };
            dhelper.ExtractZipFiles(sForgeInstallerFile, sForgeVersionDir, extractList);

            if (File.Exists(LibraryDir + _sForgeTree.Replace('/', '\\') + sForgeVersion + @"\version.json"))
            {
                post_1_13 = true;
            }

            // post 1.13 files
            if (post_1_13 == true)
            {
                post_1_13 = true;
                ForgeVersion.ForgeVersion MCForge = ForgeVersion.ForgeVersion.FromJson(File.ReadAllText(LibraryDir + _sForgeTree.Replace('/', '\\') + sForgeVersion + @"\version.json").Trim());
                // download Forge libraries
                ClassPath = DownloadForgeLibraries(MCForge);

                //extract forge Jar only for 1.13.x to 1.16.x
                if (sForgeVersion.StartsWith("1.13") || sForgeVersion.StartsWith("1.14") || sForgeVersion.StartsWith("1.15") || sForgeVersion.StartsWith("1.16"))
                {
                    // extract Forge Jar
                    extractList = new List<string>
                    {
                        "maven/net/minecraftforge/forge/"+ sForgeVersion +"/forge-" + sForgeVersion + ".jar"
                    };
                    dhelper.ExtractZipFiles(sForgeInstallerFile, sForgeVersionDir, extractList, false);
                    // append Forge to classpath
                    ClassPath.Add("net.minecraftforge:forge", sForgeVersionDir + @"\forge-" + sForgeVersion + ".jar");
                }

                // build client.jar
                BuildForgeClientJar();
            }

            // pre 1.13 files
            else if (post_1_13 == false)
            {
                post_1_13 = false;
                ForgeInstaller.ForgeInstaller MCForge = ForgeInstaller.ForgeInstaller.FromJson(File.ReadAllText(LibraryDir + _sForgeTree.Replace('/', '\\') + sForgeVersion + @"\install_profile.json").Trim());
                // download Forge libraries
                ClassPath = DownloadForgeLibraries(MCForge);

                // Extract Universal Jar
                extractList = new List<string>
                {
                    "forge-" + sForgeVersion + "-universal.jar"
                };
                dhelper.ExtractZipFiles(sForgeInstallerFile, sForgeVersionDir, extractList);

                // append Forge to classpath
                ClassPath.Add("net.minecraftforge:forge", sForgeVersionDir + @"\forge-" + sForgeVersion + "-universal.jar");
            }

            return ClassPath;
        }

        public GameVersion MergeArguments(GameVersion MCMojang)
        {
            if (post_1_13 == true)
            {
                ForgeVersion.ForgeVersion MCForge = ForgeVersion.ForgeVersion.FromJson(File.ReadAllText(LibraryDir + _sForgeTree.Replace('/', '\\') + sForgeVersion + @"\version.json").Trim());
                // replace vanilla values
                MCMojang.MainClass = MCForge.MainClass;
                // append forge arguments
                List<GameElement> itemList = MCMojang.Arguments.Game.ToList();
                List<GameElement> moreItems = MCForge.Arguments.Game.ToList();
                itemList.AddRange(moreItems);
                MCMojang.Arguments.Game = itemList.ToArray();

                if (MCForge.Arguments.Jvm != null)
                {
                    List<JvmElement> JvmItemList = MCMojang.Arguments.Jvm.ToList();
                    List<JvmElement> JvmMoreItems = MCForge.Arguments.Jvm.ToList();
                    JvmItemList.AddRange(JvmMoreItems);
                    MCMojang.Arguments.Jvm = JvmItemList.ToArray();
                }
            }
            else
            {
                ForgeInstaller.ForgeInstaller MCForge = ForgeInstaller.ForgeInstaller.FromJson(File.ReadAllText(LibraryDir + _sForgeTree.Replace('/', '\\') + sForgeVersion + @"\install_profile.json").Trim());
                // replace vanilla settings
                MCMojang.MainClass = MCForge.VersionInfo.MainClass;
                MCMojang.MinecraftArguments = MCForge.VersionInfo.MinecraftArguments;
            }
            return MCMojang;
        }

        private void DownloadForgeProcessorLibraries(ForgeProcessor.ForgeProcessor Forge)
        {
            foreach (Library lib in Forge.Libraries)
            {
                string[] sLibName = lib.Name.Split(':');
                VersionJsonDownload download = lib.Downloads.Artifact;

                // dont download Forge itself (only 1.13 to 1.16)
                if (sLibName[0].Equals("net.minecraftforge") && sLibName[1].Equals("forge"))
                {
                    if (download.Url == null)
                    { // no download, extract file from archive
                        List<string> extractList = new List<string>
                        {
                            "maven/"+lib.Downloads.Artifact.Path
                        };
                        dhelper.ExtractZipFiles(sForgeInstallerFile, sForgeVersionDir, extractList, false);
                        continue;
                    }
                }

                download.Path = LibraryDir + @"\" + download.Path.Replace("/", @"\");

                // fix for typesafe libraries
                if (sLibName[0].Contains("org.apache.logging.log4j"))
                {
                    download.Url = new Uri("http://central.maven.org/maven2/" + sLibName[0].Replace('.', '/') + "/" + sLibName[1] + "/" + sLibName[2] + "/" + sLibName[1] + "-" + sLibName[2] + ".jar");
                }
                dhelper.DownloadFileTo(download.Url, download.Path);
            }
        }

        private Dictionary<string, string> DownloadForgeLibraries(ForgeInstaller.ForgeInstaller Forge)
        {
            Dictionary<string, string> ClassPath = new Dictionary<string, string>(); // Library list for startup

            foreach (ForgeInstaller.Library Lib in Forge.VersionInfo.Libraries)
            {
                string sLocalPath = LibraryDir;
                string sRemotePath = "https://libraries.minecraft.net/";
                string sLibPath;
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

        private Dictionary<string, string> DownloadForgeLibraries(ForgeVersion.ForgeVersion forge)
        {
            Dictionary<string, string> ClassPath = new Dictionary<string, string>(); // Library list for startup

            foreach (Library lib in forge.Libraries)
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

        private void BuildForgeClientJar()
        {
            ForgeProcessor.ForgeProcessor MCForge = ForgeProcessor.ForgeProcessor.FromJson(File.ReadAllText(LibraryDir + _sForgeTree.Replace('/', '\\') + sForgeVersion + @"\install_profile.json").Trim());
            // download needes Libraries
            DownloadForgeProcessorLibraries(MCForge);

            // (re-)create Temp-Dir
            sForgeTempDir = LibraryDir + _sForgeTree.Replace('/', '\\') + sForgeVersion + @"\temp";
            if (!Directory.Exists(sForgeTempDir)) Directory.CreateDirectory(sForgeTempDir);

            // extract data/client.lzma
            List<string> extractList = new List<string>
            {
                "data/client.lzma"
            };
            dhelper.ExtractZipFiles(sForgeInstallerFile, sForgeTempDir, extractList, true);

            if (!File.Exists(LibraryDir + _sForgeTree.Replace('/', '\\') + sForgeVersion + @"\forge-" + sForgeVersion + "-client.jar"))
            {
                // Processors
                foreach (ForgeProcessor.Processor processor in MCForge.Processors)
                {
                    if (processor.Sides == null || processor.Sides.Contains("client"))
                    {
                        RunProccessor(MCForge, processor);
                    }

                    if (processor.Sides != null && processor.Sides.Contains("server"))
                    {   // Do ServerStuff
                        continue;
                    }
                }
            }
        }

        private void RunProccessor(ForgeProcessor.ForgeProcessor Forge, ForgeProcessor.Processor processor)
        {
            Debug.WriteLine("******************************************************");
            Configuration C = new Configuration();

            // get path to jar file
            string jarFile = MavenStringToFilePath(processor.Jar);

            // file exists
            if (!File.Exists(LibraryDir + "/" + jarFile))
            {
                Debug.WriteLine("file: " + jarFile + " not exists");
                return;
            }

            // get main class of Jar file
            string mainClass = GetMainClass(LibraryDir + "/" + jarFile);
            if (mainClass == null)
            {
                Debug.WriteLine("file: " + jarFile + " has no main class");
                return;
            }
            Debug.WriteLine("MainClass: " + mainClass);

            string args = "-cp " + BuildProcessClassPath(processor);
            args += " " + mainClass + " ";
            args += BuildProcArgs(Forge, processor);

            Process proc = new Process();
            proc.StartInfo.FileName = JavaPath;
            proc.StartInfo.WorkingDirectory = sForgeTempDir;
            proc.StartInfo.Arguments = args;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.OutputDataReceived += new DataReceivedEventHandler(Proc_OutputDataReceived);
            proc.ErrorDataReceived += new DataReceivedEventHandler(Proc_OutputDataReceived);
            proc.EnableRaisingEvents = true;
            proc.Start();
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
            proc.WaitForExit();
            proc.Close();
            //proc.Dispose();
        }

        private string BuildProcArgs(ForgeProcessor.ForgeProcessor Forge, ForgeProcessor.Processor processor)
        {
            string args = null;
            foreach (string arg in processor.Args)
            {
                string newarg = arg;

                if (arg.Equals("{MINECRAFT_JAR}"))
                {
                    args += " " + VersionDir + @"\" + Forge.Minecraft + @"\" + Forge.Minecraft + ".jar";
                    continue;
                }

                if (arg.Equals("{SIDE}"))
                {
                    args += " " + side.ToString().ToLower();
                    continue;
                }

                // contains var?
                if (newarg.StartsWith("{") && newarg.EndsWith("}"))
                {
                    // remove chars
                    newarg = newarg.Replace("{", "").Replace("}", "");

                    if (side == StartupSide.Client) newarg = Forge.Data[newarg].Client;
                    if (side == StartupSide.Server) newarg = Forge.Data[newarg].Server;
                }

                // remove leading slash
                if (newarg.StartsWith("/"))
                {
                    newarg = newarg.Remove(0, 1);
                }

                // contains maven string?
                if (newarg.StartsWith("[") && newarg.EndsWith("]"))
                {
                    newarg = LibraryDir + @"\" + MavenStringToFilePath(newarg.Replace("[", "").Replace("]", "")).Replace('/', '\\');
                }
                args += " " + newarg;
            }

            Debug.WriteLine("Args: " + args);
            return args;
        }

        private string BuildProcessClassPath(ForgeProcessor.Processor processor)
        {
            string classPath = null;
            foreach (string jarfile in processor.Classpath)
            {
                if (classPath != null)
                {
                    classPath += ";";
                }
                classPath += "\"" + LibraryDir + @"\" + MavenStringToFilePath(jarfile).Replace('/', '\\') + "\"";
            }

            classPath += ";\"" + LibraryDir + @"\" + MavenStringToFilePath(processor.Jar).Replace('/', '\\') + "\"";
            return classPath;
        }

        private string GetMainClass(string jarFile)
        {
            try
            {
                using (ZipArchive zip = ZipFile.OpenRead(jarFile))
                {
                    ZipArchiveEntry entry = zip.GetEntry("META-INF/MANIFEST.MF");
                    using (StreamReader reader = new StreamReader(entry.Open()))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.StartsWith("Main-Class: "))
                            {
                                return line.Replace("Main-Class: ", "").Trim();
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        private string MavenStringToFilePath(string mavenString)
        {
            string output;
            // file extension
            string extension = ".jar";
            if (mavenString.Contains("@"))
            {
                string[] ex = mavenString.Split('@');
                extension = "." + ex[1];
                mavenString = ex[0];
            }

            string[] sFile = mavenString.Split(':');

            // path
            output = sFile[0].Replace('.', '/') + "/" + sFile[1] + "/" + sFile[2] + "/";

            // File
            output += sFile[1] + "-" + sFile[2];

            // append ?thing?
            if (sFile.Count() == 4)
            {
                output += "-" + sFile[3];
            }

            // File extension
            output += extension;

            return output;
        }

        private void Proc_OutputDataReceived(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!string.IsNullOrEmpty(outLine.Data))
            {
                Debug.WriteLine(outLine.Data);
            }
        }
    }
}
