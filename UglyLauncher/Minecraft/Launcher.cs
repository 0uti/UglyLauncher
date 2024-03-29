﻿using UglyLauncher.AccountManager;
using UglyLauncher.Internet;
using UglyLauncher.Minecraft.Files.CurseForge;
using UglyLauncher.Minecraft.Files.Forge;
using UglyLauncher.Minecraft.Files.Mojang;
using UglyLauncher.Minecraft.Files.Mojang.GameVersion;
using UglyLauncher.Minecraft.Json.AvailablePacks;
using UglyLauncher.Minecraft.Json.Pack;
using UglyLauncher.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace UglyLauncher.Minecraft
{
    class Launcher : IDisposable
    {
        // events
        public event EventHandler<FormWindowStateEventArgs> RestoreWindow;
        public event EventHandler MinecraftStarted;
        public event EventHandler MinecraftExited;
        public event DataReceivedEventHandler MinecraftDataReceived;
        // objects
        private static Process minecraft;
        private FrmConsole _console;
        private static MCAvailablePacks PacksAvailable = new MCAvailablePacks();
        private static MCPacksInstalled PacksInstalled = new MCPacksInstalled();
        // Strings
        public const string _PackServer = "http://uglylauncher.de";
        public static readonly string _DataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + ".UglyLauncher";
        public static readonly string _PacksDir = _DataDir + Path.DirectorySeparatorChar + "packs";
        public readonly string _JavaDir = _DataDir + Path.DirectorySeparatorChar + "java";
        public static readonly string _McDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + ".minecraft";
        public readonly string _McAssetsDir = _McDir + Path.DirectorySeparatorChar + "assets";
        public readonly string _McLibraryDir = _McDir + Path.DirectorySeparatorChar + "libraries";
        public readonly string _McVersionDir = _McDir + Path.DirectorySeparatorChar + "versions";
        public readonly string _McNativesDir = _McDir + Path.DirectorySeparatorChar + "natives";

        private string _JavaPath;
        private int _JavaVersion;

        // bool
        private readonly bool Offline = false;
        private readonly StartupSide side;

        private readonly DownloadHelper dhelper;

        // constructor
        public Launcher(StartupSide side, bool OfflineMode)
        {
            this.side = side;
            dhelper = new DownloadHelper();
            Offline = OfflineMode;
        }

        // Open Pack folder
        public void OpenPackFolder(string sSelectedPack)
        {
            if (Directory.Exists(_PacksDir + Path.DirectorySeparatorChar + sSelectedPack)) Process.Start(_PacksDir + Path.DirectorySeparatorChar + sSelectedPack);
        }

        // Check Directories
        public void CheckDirectories()
        {
            // .UglyLauncher
            if (!Directory.Exists(_DataDir)) Directory.CreateDirectory(_DataDir);
            if (!Directory.Exists(_PacksDir)) Directory.CreateDirectory(_PacksDir);
            if (!Directory.Exists(_McLibraryDir)) Directory.CreateDirectory(_McLibraryDir);
            if (!Directory.Exists(_JavaDir)) Directory.CreateDirectory(_JavaDir);
            // .Minecraft
            if (!Directory.Exists(_McAssetsDir)) Directory.CreateDirectory(_McAssetsDir);
            if (!Directory.Exists(_McAssetsDir + Path.DirectorySeparatorChar + "indexes")) Directory.CreateDirectory(_McAssetsDir + Path.DirectorySeparatorChar + "indexes");
            if (!Directory.Exists(_McAssetsDir + Path.DirectorySeparatorChar + "objects")) Directory.CreateDirectory(_McAssetsDir + Path.DirectorySeparatorChar + "objects");
            if (!Directory.Exists(_McAssetsDir + Path.DirectorySeparatorChar + "virtual")) Directory.CreateDirectory(_McAssetsDir + Path.DirectorySeparatorChar + "virtual");
            if (!Directory.Exists(_McVersionDir)) Directory.CreateDirectory(_McVersionDir);
            if (!Directory.Exists(_McNativesDir)) Directory.CreateDirectory(_McNativesDir);
        }

        // load Packlist from server
        public void LoadAvailablePacks(string sPlayerName, string sMCUID)
        {
            try
            {
                string sPackListJson = Http.GET(_PackServer + @"/packs.php?player=" + sPlayerName + @"&uid=" + sMCUID);
                PacksAvailable = MCAvailablePacks.FromJson(sPackListJson);
            }
            catch (WebException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get packe liste
        public MCAvailablePacks GetAvailablePacks() => PacksAvailable;

        public MCAvailablePack GetAvailablePack(string sPackName)
        {
            foreach (MCAvailablePack Pack in PacksAvailable.Packs)
            {
                if (Pack.Name == sPackName)
                {
                    return Pack;
                }
            }
            return null;
        }

        // get pack icon
        public Image GetPackIcon(MCAvailablePack Pack)
        {
            MemoryStream ms = Http.DownloadToStream(_PackServer + "/packs/" + Pack.Name + "/" + Pack.Name + ".png");

            if (Directory.Exists(_PacksDir + Path.DirectorySeparatorChar + Pack.Name))
            {
                FileStream file = new FileStream(_PacksDir + Path.DirectorySeparatorChar + Pack.Name + Path.DirectorySeparatorChar + Pack.Name + ".png", FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
            }

            return Image.FromStream(ms);
        }

        // get pack icon
        public Image GetPackIconOffline(MCPacksInstalledPack Pack)
        {
            if (!File.Exists(_PacksDir + Path.DirectorySeparatorChar + Pack.Name + Path.DirectorySeparatorChar + Pack.Name + ".png")) return null;

            MemoryStream ms = new MemoryStream();

            using (FileStream fileStream = File.OpenRead(_PacksDir + Path.DirectorySeparatorChar + Pack.Name + Path.DirectorySeparatorChar + Pack.Name + ".png"))
            {
                ms.SetLength(fileStream.Length);
                //read file to MemoryStream
                fileStream.Read(ms.GetBuffer(), 0, (int)fileStream.Length);
            }
            return Image.FromStream(ms);
        }

        // Get installes packages
        public void LoadInstalledPacks()
        {
            List<string> dirs = new List<string>(Directory.EnumerateDirectories(_PacksDir));
            PacksInstalled = new MCPacksInstalled();
            foreach (string dir in dirs)
            {
                if (File.Exists(dir + @"\version") && File.Exists(dir + @"\pack.json"))
                {
                    MCPacksInstalledPack pack = new MCPacksInstalledPack
                    {
                        Name = dir.Substring(dir.LastIndexOf("\\") + 1),
                        // Get versions files
                        CurrentVersion = File.ReadAllText(dir + @"\version").Trim()
                    };
                    if (File.Exists(dir + @"\selected")) pack.SelectedVersion = File.ReadAllText(dir + @"\selected").Trim();
                    else pack.SelectedVersion = "recommended";
                    PacksInstalled.packs.Add(pack);
                }
            }
        }

        // Get pack liste
        public MCPacksInstalled GetInstalledPacks()
        {
            return PacksInstalled;
        }

        // get installed Pack
        public MCPacksInstalledPack GetInstalledPack(string sPackName)
        {
            foreach (MCPacksInstalledPack Pack in PacksInstalled.packs)
                if (Pack.Name == sPackName) return Pack;
            return null;
        }

        // Check if Pack is Installed (and the right version)
        public bool IsPackInstalled(string sPackName, string sPackVersion = null)
        {
            // get pack
            MCPacksInstalledPack Pack = GetInstalledPack(sPackName);
            // return false if pack not found
            if (Pack == null) return false;
            // return true if no version is given
            if (sPackVersion == null) return true;
            // check version of installed Pack
            // if recommended version, getting the version from available packs
            if (sPackVersion == "recommended") sPackVersion = GetRecommendedVersion(sPackName);
            // check if version is installed
            if (Pack.CurrentVersion != sPackVersion) return false;
            // Pack is fine :)
            return true;
        }

        // Get ModFolderContents
        public List<string> GetModFolderContents(string sPackname, IEnumerable<string> sFileExtensions)
        {
            List<string> Mods = new List<string>();
            try
            {
                string sModsPath = string.Format(@"{0}\{1}\minecraft\mods\", _PacksDir, sPackname);
                Mods = Directory.EnumerateFiles(sModsPath, "*.*")
                    .Where(f => sFileExtensions.Contains(Path.GetExtension(f).ToLower()))
                    .ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Kein Mod Verzeichniss gefunden. Vanilla ?", "kein Modverzeichniss", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

            return Mods;
        }

        // Get mcmod.info file as string
        public string GetMcModInfo(string sFileName)
        {
            try
            {
                using (ZipArchive zip = ZipFile.OpenRead(sFileName))
                {
                    ZipArchiveEntry entry = zip.GetEntry("META-INF/MANIFEST.MF");
                    string text;
                    using (StreamReader reader = new StreamReader(entry.Open()))
                    {
                        text = reader.ReadToEnd();
                    }
                    return text;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetRecommendedVersion(string sPackName)
        {
            if (Offline == false)
            {
                MCAvailablePack Pack = GetAvailablePack(sPackName);
                return Pack.RecommendedVersion;
            }

            else
            {
                MCPacksInstalledPack Pack = GetInstalledPack(sPackName);
                return Pack.CurrentVersion;
            }

        }

        public void SetSelectedVersion(string sPackName, string sVersion)
        {
            File.WriteAllText(_PacksDir + Path.DirectorySeparatorChar + sPackName + Path.DirectorySeparatorChar + "selected", sVersion);
            Application.DoEvents(); // wait a little bit :)
            LoadInstalledPacks();
        }

        public string GetInstalledPackVersion(string sPackName)
        {
            MCPacksInstalledPack Pack = GetInstalledPack(sPackName);
            return Pack.CurrentVersion;
        }

        public void StartPack(string sPackName, string sPackVersion)
        {
            FilesMojang MCMojangFiles = new FilesMojang(dhelper)
            {
                LibraryDir = _McLibraryDir,
                VersionDir = _McVersionDir,
                NativesDir = _McNativesDir,
                AssetsDir = _McAssetsDir,
                OfflineMode = Offline
            };

            // check if pack is installed with given version
            if (!IsPackInstalled(sPackName, sPackVersion))
            {
                InstallPack(sPackName, sPackVersion);
            }

            // getting pack json file
            MCPack pack = MCPack.FromJson(File.ReadAllText(_PacksDir + Path.DirectorySeparatorChar + sPackName + Path.DirectorySeparatorChar + "pack.json").Trim());
            // vanilla Minecraft
            MCMojangFiles.DownloadVersionJson(pack.MCVersion);
            GameVersion MCMojang = GameVersion.FromJson(File.ReadAllText(_McVersionDir + Path.DirectorySeparatorChar + pack.MCVersion + Path.DirectorySeparatorChar + pack.MCVersion + ".json").Trim());
            // download game jar
            if (side == StartupSide.Client)
            {
                MCMojangFiles.DownloadClientJar(MCMojang);
            }

            if (side == StartupSide.Server)
            {
                MCMojangFiles.DownloadServerJar(MCMojang);
            }
            // download libraries if needed
            Dictionary<string, string> ClassPath = MCMojangFiles.DownloadClientLibraries(MCMojang);
            // download assets if needed
            if (side == StartupSide.Client)
            {
                MCMojangFiles.DownloadClientAssets(MCMojang);
            }
                
            // set Java version
            if (!InstallJava(MCMojang.javaVersion))
            {
                return;
            }

            // additional things for forge
            if (pack.Type.Equals("forge"))
            {
                FilesForge MCForgeFiles = new FilesForge(dhelper, side)
                {
                    LibraryDir = _McLibraryDir,
                    VersionDir = _McVersionDir,
                    OfflineMode = Offline,
                    JavaPath = _JavaPath
                };

                // Install Forge
                Dictionary<string, string> ForgeClassPath = MCForgeFiles.InstallForge(pack.ForgeVersion);

                //Merge Classpath
                foreach (KeyValuePair<string, string> entry in ForgeClassPath)
                {
                    if (ClassPath.ContainsKey(entry.Key))
                    {
                        ClassPath.Remove(entry.Key);
                    }
                    ClassPath.Add(entry.Key, entry.Value);
                }

                // Merge startup parameter
                MCMojang = MCForgeFiles.MergeArguments(MCMojang);
            }

            // set selected version
            SetSelectedVersion(sPackName, sPackVersion);
            // start the pack
            Start(BuildArgs(MCMojang, sPackName, ClassPath), sPackName);
            // close bar if open
            if (dhelper.IsBarVisible() == true) dhelper.HideBar();
            //if (_bar.Visible == true) _bar.Hide();
        }

        private bool InstallJava(JavaVersion javaVersion)
        {
            if (!IsJavaInstalled(javaVersion))
            {   // version not installed
                if (!DownloadJava(javaVersion))
                {
                    MessageBox.Show("unknown Java version in Mojang JSON file.", "Missing Java");

                    return false;
                }
            }

            _JavaPath = _JavaDir + Path.DirectorySeparatorChar + javaVersion.MajorVersion.ToString() + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "java.exe";
            _JavaVersion = javaVersion.MajorVersion;
            return true;
        }

        private bool IsJavaInstalled(JavaVersion javaVersion)
        {
            return File.Exists(_JavaDir + Path.DirectorySeparatorChar + javaVersion.MajorVersion.ToString() + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "java.exe");
        }

        private bool DownloadJava(JavaVersion javaVersion)
        {
            // download pack
            dhelper.DownloadFileTo(_PackServer + "/java/" + javaVersion.MajorVersion.ToString() + ".zip", _JavaDir + Path.DirectorySeparatorChar + javaVersion.MajorVersion.ToString() + ".zip", true, "Downloading Java " + javaVersion.MajorVersion.ToString());

            // Hide Bar
            dhelper.HideBar();

            // unzip pack
            dhelper.ExtractZipFiles(_JavaDir + Path.DirectorySeparatorChar + javaVersion.MajorVersion.ToString() + ".zip", _JavaDir);

            // delete zip file
            File.Delete(_JavaDir + Path.DirectorySeparatorChar + javaVersion.MajorVersion.ToString() + ".zip");

            return IsJavaInstalled(javaVersion);
        }

        private void Start(string args, string sPackName)
        {
            Configuration C = new Configuration();
            minecraft = new Process();
            // check for "minecraft" folder
            if (!Directory.Exists(_PacksDir + Path.DirectorySeparatorChar + sPackName + +Path.DirectorySeparatorChar + "minecraft")) Directory.CreateDirectory(_PacksDir + Path.DirectorySeparatorChar + sPackName + Path.DirectorySeparatorChar + "minecraft");

            minecraft.StartInfo.FileName = _JavaPath;
            minecraft.StartInfo.WorkingDirectory = _PacksDir + Path.DirectorySeparatorChar + sPackName + Path.DirectorySeparatorChar + "minecraft";
            minecraft.StartInfo.Arguments = args;
            minecraft.StartInfo.RedirectStandardOutput = true;
            minecraft.StartInfo.RedirectStandardError = true;
            minecraft.StartInfo.RedirectStandardInput = true;
            minecraft.StartInfo.UseShellExecute = false;
            minecraft.StartInfo.CreateNoWindow = true;
            minecraft.OutputDataReceived += new DataReceivedEventHandler(Minecraft_Process_OutputDataReceived);
            minecraft.ErrorDataReceived += new DataReceivedEventHandler(Minecraft_Process_ErrorDataReceived);
            minecraft.Exited += new EventHandler(Minecraft_Process_Exited);
            minecraft.EnableRaisingEvents = true;

            // load console
            if (C.ShowConsole == 1)
            {
                CloseOldConsole();
                _console = new FrmConsole();
                _console.Show();
                _console.Clear();
                _console.AddLine(String.Format("UglyLauncher-Version: {0}", Application.ProductVersion), Color.Blue);
                //_console.AddLine("Using Java-Version: " + C.GetJavaPath() + " (64bit)", Color.Blue);
                _console.AddLine("Startparameter:" + args, Color.Blue);
            }

            // start minecraft
            minecraft.Start();
            minecraft.BeginOutputReadLine();
            minecraft.BeginErrorReadLine();
            // raise event
            EventHandler<FormWindowStateEventArgs> handler = RestoreWindow;
            FormWindowStateEventArgs args2 = new FormWindowStateEventArgs
            {
                WindowState = FormWindowState.Minimized,
                MCExitCode = -1
            };
            handler?.Invoke(this, args2);
            MinecraftStarted?.Invoke(this, EventArgs.Empty);
        }

        private void CloseOldConsole()
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Name == "FrmConsole")
                {
                    frm.Close();
                    return;
                }
            }
        }

        private void Minecraft_Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                try
                {
                    _console.AddLine(e.Data, Color.Red);
                }
                catch (Exception)
                {
                }
            }
        }

        private void Minecraft_Process_Exited(object sender, EventArgs e)
        {
            Configuration C = new Configuration();
            Process minecraft = sender as Process;

            if (C.KeepConsole == 0 && minecraft.ExitCode == 0)
            {
                try
                {
                    _console.BeginInvoke(new Action(() =>
                        {
                            _console.Dispose();
                        }
                    ));
                }
                catch (Exception)
                {
                }
            }

            // raise event
            EventHandler<FormWindowStateEventArgs> handler = RestoreWindow;
            FormWindowStateEventArgs args = new FormWindowStateEventArgs
            {
                WindowState = FormWindowState.Normal,
                MCExitCode = minecraft.ExitCode
            };
            handler?.Invoke(this, args);
            MinecraftExited?.Invoke(sender, e);
        }

        private void Minecraft_Process_OutputDataReceived(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!string.IsNullOrEmpty(outLine.Data))
            {
                try
                {
                    _console.AddLine(outLine.Data, Color.Black);
                    MinecraftDataReceived?.Invoke(sendingProcess, outLine);
                }
                catch (Exception)
                {
                }
            }
        }

        public static void SendToMinecraft(string command)
        {
            if (minecraft != null && !minecraft.HasExited)
            {
                StreamWriter myStreamWriter = minecraft.StandardInput;
                myStreamWriter.WriteLine(command);
            }
        }

        public static void RenameWorld(string packName)
        {
            int unixtime = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            string worldFolder = _PacksDir + Path.DirectorySeparatorChar + packName + Path.DirectorySeparatorChar + "minecraft" + Path.DirectorySeparatorChar + "world";
            
            if (Directory.Exists(worldFolder))
            {
                Directory.Move(worldFolder, worldFolder + "-" + unixtime.ToString());
            }
        }

        private string BuildArgs(GameVersion MC, string sPackName, Dictionary<string, string> ClassPath)
        {
            string args = null;
            Configuration C = new Configuration();
            
            // Garbage Collector
            if (C.UseGC == 1 && _JavaVersion == 8) args += " -XX:SurvivorRatio=2 -XX:+DisableExplicitGC -XX:+UseConcMarkSweepGC -XX:+AggressiveOpts";

            // force 64bit
            if (_JavaVersion == 8) args += " -d64";

            // Java Memory
            args += string.Format(" -Xms{0}m -Xmx{1}m -Xmn128m", C.MinimumMemory, C.MaximumMemory);

            if (side == StartupSide.Client)
            {
                args += BuildArgsClient(MC,sPackName,ClassPath);
            }

            if (side == StartupSide.Server)
            {
                args += BuildArgsServer(MC,sPackName,ClassPath);
            }

            return args;
        }

        private string BuildArgsClient(GameVersion MC, string sPackName, Dictionary<string, string> ClassPath)
        {
            Manager U = new Manager();
            MCUserAccount Acc = U.GetAccount(U.GetDefault());
            MCUserAccountProfile Profile = U.GetActiveProfile(Acc);

            string args = null;
            string classpath = null;

            if (MC.Arguments != null)
            {
                // JVM
                foreach (JvmElement jvme in MC.Arguments.Jvm)
                {
                    if (jvme.JvmClass != null)
                    {
                        // skip non windows libraries
                        if (jvme.JvmClass.Rules != null)
                        {
                            bool bWindows = false;
                            foreach (JvmRule Rule in jvme.JvmClass.Rules)
                            {
                                if (Rule.Action == "allow")
                                {
                                    if (Rule.Os == null) bWindows = true;
                                    else if (Rule.Os.Name == null || Rule.Os.Name == "windows")
                                    {
                                        bWindows = true;
                                        // check version
                                        if (Rule.Os.Version != null)
                                        {
                                            string text = Environment.OSVersion.Version.ToString();
                                            Regex r = new Regex(Rule.Os.Version, RegexOptions.IgnoreCase);
                                            Match m = r.Match(text);
                                            if (!m.Success) bWindows = false;
                                        }

                                        //check Arch
                                        if (Rule.Os.Arch != null)
                                        {
                                            if (Rule.Os.Arch == "x86") bWindows = false;
                                        }
                                    }
                                }
                                if (Rule.Action == "disallow" && Rule.Os.Name == "windows") bWindows = false;
                            }
                            if (bWindows == false) continue;
                        }

                        // one value
                        if (jvme.JvmClass.Value.String != null)
                        {
                            args += " " + jvme.JvmClass.Value.String;
                        }

                        // multiple values

                        if (jvme.JvmClass.Value.StringArray != null)
                        {
                            foreach (string value in jvme.JvmClass.Value.StringArray)
                            {
                                // fix spaces in Json path
                                if (value.Split('=').Last().Contains(" "))
                                {
                                    args += " " + value.Split('=').First() + "=\"" + value.Split('=').Last() + "\"";
                                }
                                else
                                {
                                    args += " " + value;
                                }
                            }
                        }
                    }
                    else
                    {
                        args += " " + jvme.String;
                    }
                }

                // startup class
                args += " " + MC.MainClass;

                // Game
                foreach (GameElement ge in MC.Arguments.Game)
                {
                    if (ge.GameClass != null)
                    {

                    }
                    else
                    {
                        args += " " + ge.String;
                    }
                }
            }
            else
            {
                // fucking Mojang drivers Hack
                args += " -XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump";
                // Tweaks (forge)
                args += " -Dfml.ignoreInvalidMinecraftCertificates=true -Dfml.ignorePatchDiscrepancies=true";
                // Path to natives
                args += " -Djava.library.path=${natives_directory}";
                // Libs
                args += " -cp ${classpath}";
                // startup class
                args += " " + MC.MainClass;
                // minecraft arguments
                args += " " + MC.MinecraftArguments;
            }

            // libraries
            foreach (KeyValuePair<string, string> entry in ClassPath)
            {
                classpath += string.Format("\"{0}\";", entry.Value);
            }

            // version .jar
            classpath += string.Format("\"{0}\\{1}\\{1}.jar\" ", _McVersionDir, MC.Id);

            // fill placeholders
            args = args.Replace("${auth_player_name}", Profile.name);
            args = args.Replace("${version_name}", MC.Id);
            args = args.Replace("${game_directory}", string.Format("\"{0}\\{1}\\minecraft\"", _PacksDir, sPackName));
            args = args.Replace("${assets_root}", string.Format("\"{0}\"", _McAssetsDir));
            args = args.Replace("${game_assets}", string.Format("\"{0}\\virtual\\legacy\"", _McAssetsDir));
            args = args.Replace("${assets_index_name}", MC.Assets);
            args = args.Replace("${auth_uuid}", Profile.id);
            args = args.Replace("${auth_access_token}", Acc.accessToken);
            args = args.Replace("${auth_session}", string.Format("token:{0}:{1}", Acc.accessToken, Profile.id));
            args = args.Replace("${user_properties}", "{}");
            args = args.Replace("${user_type}", "Mojang");
            args = args.Replace("${version_type}", MC.Type);
            args = args.Replace("${natives_directory}", "\"" + _McNativesDir + @"\" + MC.Id + "\"");
            args = args.Replace("${classpath}", classpath);
            args = args.Replace("${launcher_name}", Application.ProductName);
            args = args.Replace("${launcher_version}", Application.ProductVersion);
            args = args.Replace("${library_directory}", _McLibraryDir);
            args = args.Replace("${classpath_separator}", ";");

            // do we have a mod_list_client.json?
            string modListClient = string.Format("{0}\\{1}\\mod_list_client.json", _PacksDir, sPackName);
            if (File.Exists(modListClient))
            {
                args += " --modListFile=\"" + modListClient + "\"";
            }

            return args;
        }

        private string BuildArgsServer(GameVersion MC, string sPackName, Dictionary<string, string> ClassPath)
        {
            string args = null;

            // is server
            args += " -server";

            // server jar
            args += " -jar " + string.Format("\"{0}\\{1}\\minecraft_server.{1}.jar\" ", _McVersionDir, MC.Id);

            // no gui
            args += " nogui";

            return args;
        }


        private MCAvailablePackVersion GetAvailablePackVersion(string sPackName, string sPackVersion)
        {
            MCAvailablePack pack = GetAvailablePack(sPackName);

            foreach (MCAvailablePackVersion version in pack.Versions)
            {
                if (version.Version.Equals(sPackVersion))
                {
                    return version;
                }
            }

            return new MCAvailablePackVersion();
        }

        private void InstallPack(string sPackName, string sPackVersion)
        {
            // delete pack if installed
            if (IsPackInstalled(sPackName))
            {
                DeletePack(sPackName);
            }

            // if recommended version, getting the version from available packs
            if (sPackVersion == "recommended")
            {
                sPackVersion = GetRecommendedVersion(sPackName);
            }

            // delete old download
            if (File.Exists(_PacksDir + Path.DirectorySeparatorChar + sPackName + "-" + sPackVersion + ".zip"))
            {
                File.Delete(_PacksDir + Path.DirectorySeparatorChar + sPackName + "-" + sPackVersion + ".zip");
            }

            MCAvailablePackVersion version = GetAvailablePackVersion(sPackName, sPackVersion);

            MCPack pack;
            if (version.DownloadZip == true)
            {
                // download pack
                dhelper.DownloadFileTo(_PackServer + "/packs/" + sPackName + "/" + sPackName + "-" + sPackVersion + ".zip", _PacksDir + Path.DirectorySeparatorChar + sPackName + "-" + sPackVersion + ".zip", true, "Downloading Pack " + sPackName);

                // Hide Bar
                dhelper.HideBar();

                // unzip pack
                dhelper.ExtractZipFiles(_PacksDir + Path.DirectorySeparatorChar + sPackName + "-" + sPackVersion + ".zip", _PacksDir);

                // delete zip file
                File.Delete(_PacksDir + Path.DirectorySeparatorChar + sPackName + "-" + sPackVersion + ".zip");

                pack = MCPack.FromJson(File.ReadAllText(_PacksDir + Path.DirectorySeparatorChar + sPackName + Path.DirectorySeparatorChar + "pack.json").Trim());
            }
            else
            {
                // write pack.json file
                pack = new MCPack
                {
                    Type = "vanilla",
                    MCVersion = version.Version
                };

                if (!Directory.Exists(_PacksDir + Path.DirectorySeparatorChar + sPackName))
                {
                    Directory.CreateDirectory(_PacksDir + Path.DirectorySeparatorChar + sPackName);
                }

                File.WriteAllText(_PacksDir + Path.DirectorySeparatorChar + sPackName + Path.DirectorySeparatorChar + "pack.json", pack.ToJson());

                // write version file
                File.WriteAllText(_PacksDir + Path.DirectorySeparatorChar + sPackName + Path.DirectorySeparatorChar + "version", version.Version);
            }

            // check for "minecraft" folder
            if (!Directory.Exists(_PacksDir + Path.DirectorySeparatorChar + sPackName + Path.DirectorySeparatorChar + "minecraft"))
            {
                Directory.CreateDirectory(_PacksDir + Path.DirectorySeparatorChar + sPackName + Path.DirectorySeparatorChar + "minecraft");
            }

            if (pack == null || pack.CurseFiles == null)
            {
                return;
            }
            // download cuseFiles
            FilesCurseForge CurseFiles = new FilesCurseForge(side, dhelper)
            {
                ModsDir = _PacksDir + Path.DirectorySeparatorChar + sPackName + Path.DirectorySeparatorChar + "minecraft" + Path.DirectorySeparatorChar + "mods"
            };
            CurseFiles.DownloadModFiles(pack.CurseFiles);
        }

        public void ReDownloadMods(string sPackName)
        {
            MCPack mcPack = MCPack.FromJson(File.ReadAllText(_PacksDir + Path.DirectorySeparatorChar.ToString() + sPackName + Path.DirectorySeparatorChar.ToString() + "pack.json").Trim());
            if (mcPack != null && mcPack.CurseFiles != null)
            {
                new FilesCurseForge(side, dhelper)
                {
                    ModsDir = _PacksDir + Path.DirectorySeparatorChar.ToString() + sPackName + Path.DirectorySeparatorChar.ToString() + "minecraft" + Path.DirectorySeparatorChar.ToString() + "mods"
                }.DownloadModFiles(mcPack.CurseFiles);
            }

            if (!dhelper.IsBarVisible())
            {
                return;
            }

            dhelper.HideBar();
        }

        private void DeletePack(string sPackName)
        {
            string PackDir = _PacksDir + Path.DirectorySeparatorChar + sPackName;

            if (!Directory.Exists(PackDir)) return; // Pack did not exist
            // Delete Directories

            string McDir = PackDir + Path.DirectorySeparatorChar + "minecraft";

            if (Directory.Exists(McDir + Path.DirectorySeparatorChar + "logs")) Directory.Delete(McDir + Path.DirectorySeparatorChar + "logs", true);
            if (Directory.Exists(McDir + Path.DirectorySeparatorChar + "mods")) Directory.Delete(McDir + Path.DirectorySeparatorChar + "mods", true);
            if (Directory.Exists(McDir + Path.DirectorySeparatorChar + "modclasses")) Directory.Delete(McDir + Path.DirectorySeparatorChar + "modclasses", true);
            if (Directory.Exists(McDir + Path.DirectorySeparatorChar + "config")) Directory.Delete(McDir + Path.DirectorySeparatorChar + "config", true);
            if (Directory.Exists(McDir + Path.DirectorySeparatorChar + "stats")) Directory.Delete(McDir + Path.DirectorySeparatorChar + "stats", true);
            if (Directory.Exists(McDir + Path.DirectorySeparatorChar + "crash-reports")) Directory.Delete(McDir + Path.DirectorySeparatorChar + "crash-reports", true);
            // Deleting .log Files
            foreach (FileInfo f in new DirectoryInfo(McDir).GetFiles("*.log"))
                f.Delete();
            // Deleting .lck Files
            foreach (FileInfo f in new DirectoryInfo(McDir).GetFiles("*.lck"))
                f.Delete();
            // Deleting .1 Files
            foreach (FileInfo f in new DirectoryInfo(McDir).GetFiles("*.1"))
                f.Delete();
            // Deleting pack.json
            if (File.Exists(PackDir + Path.DirectorySeparatorChar + "pack.json")) File.Delete(PackDir + Path.DirectorySeparatorChar + "pack.json");
            // Deleting version
            if (File.Exists(PackDir + Path.DirectorySeparatorChar + "version")) File.Delete(PackDir + Path.DirectorySeparatorChar + "version");
        }

        public void Dispose()
        {
            dhelper.Dispose();
            _console.Dispose();
        }

        public class FormWindowStateEventArgs : EventArgs
        {
            public FormWindowState WindowState { get; set; }
            public int MCExitCode { get; set; }
        }
    }

    /*
     * Structures
     */

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
        public string Name { get; set; }
        [DataMember]
        public string CurrentVersion { get; set; } // Version of the package
        [DataMember]
        public string SelectedVersion { get; set; } // selected version in Launcher window (recommended check)
    }

}
