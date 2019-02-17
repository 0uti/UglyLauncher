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
using UglyLauncher.AccountManager;
using UglyLauncher.Internet;
using UglyLauncher.Minecraft.Files;
using UglyLauncher.Minecraft.Files.Json.GameVersion;
using UglyLauncher.Minecraft.Json.AvailablePacks;
using UglyLauncher.Minecraft.Json.Pack;
using UglyLauncher.Settings;

namespace UglyLauncher.Minecraft
{
    class Launcher
    {
        // events
        public event EventHandler<FormWindowStateEventArgs> RestoreWindow;
        // objects
        private FrmConsole _console;
        private static MCAvailablePacks PacksAvailable = new MCAvailablePacks();
        private static MCPacksInstalled PacksInstalled = new MCPacksInstalled();
        // Strings
        public static readonly string _sPackServer = "http://uglylauncher.de";
        public static readonly string _sDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.UglyLauncher";
        public static readonly string _sPacksDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.UglyLauncher\packs";
        public readonly string _sAssetsDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.minecraft\assets";
        public readonly string _sLibraryDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.minecraft\libraries";
        public readonly string _sVersionDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.minecraft\versions";
        public readonly string _sNativesDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.UglyLauncher\natives";
        private string forgeVersion;
        // bool
        private readonly bool Offline = false;
        private bool isForge = false;
        // Lists
        

        private DownloadHelper dhelper;


        // constructor
        public Launcher(bool OfflineMode)
        {
            dhelper = new DownloadHelper();
            Offline = OfflineMode;
        }

        // Open Pack folder
        public void OpenPackFolder(string sSelectedPack)
        {
            if(Directory.Exists(_sPacksDir + @"\" + sSelectedPack)) Process.Start(_sPacksDir + @"\" + sSelectedPack);
        }

        // Check Directories
        public void CheckDirectories()
        {
            if (!Directory.Exists(_sDataDir)) Directory.CreateDirectory(_sDataDir);
            if (!Directory.Exists(_sLibraryDir)) Directory.CreateDirectory(_sLibraryDir);
            if (!Directory.Exists(_sAssetsDir)) Directory.CreateDirectory(_sAssetsDir);
            if (!Directory.Exists(_sAssetsDir + @"\indexes")) Directory.CreateDirectory(_sAssetsDir + @"\indexes");
            if (!Directory.Exists(_sAssetsDir + @"\objects")) Directory.CreateDirectory(_sAssetsDir + @"\objects");
            if (!Directory.Exists(_sAssetsDir + @"\virtual")) Directory.CreateDirectory(_sAssetsDir + @"\virtual");
            if (!Directory.Exists(_sVersionDir)) Directory.CreateDirectory(_sVersionDir);
            if (!Directory.Exists(_sPacksDir)) Directory.CreateDirectory(_sPacksDir);
            if (!Directory.Exists(_sNativesDir)) Directory.CreateDirectory(_sNativesDir);
        }

        // load Packlist from server
        public void LoadAvailablePacks(string sPlayerName,string sMCUID)
        {
            try
            {
                string sPackListJson = Http.GET(_sPackServer + @"/packs.php?player=" + sPlayerName + @"&uid=" + sMCUID);
                PacksAvailable = MCAvailablePacks.FromJson(sPackListJson);
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
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
            MemoryStream ms = new MemoryStream();
            ms = Http.DownloadToStream(_sPackServer + @"/packs/" + Pack.Name + @"/" + Pack.Name + @".png");

            if(Directory.Exists(_sPacksDir + @"\" + Pack.Name))
            {
                FileStream file = new FileStream(_sPacksDir + @"\" + Pack.Name + @"\" + Pack.Name + ".png", FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
            }

            return Image.FromStream(ms);
        }

        // get pack icon
        public Image GetPackIconOffline(MCPacksInstalledPack Pack)
        {
            if (!File.Exists(_sPacksDir + @"\" + Pack.Name + @"\" + Pack.Name + ".png")) return null;

            MemoryStream ms = new MemoryStream();

            FileStream fileStream = File.OpenRead(_sPacksDir + @"\" + Pack.Name + @"\" + Pack.Name + ".png");
            ms.SetLength(fileStream.Length);
            //read file to MemoryStream
            fileStream.Read(ms.GetBuffer(), 0, (int)fileStream.Length);
            
            return Image.FromStream(ms);
        }

        // Get installes packages
        public void LoadInstalledPacks()
        {
            List<string> dirs = new List<string>(Directory.EnumerateDirectories(_sPacksDir));
            PacksInstalled = new MCPacksInstalled();
            foreach (var dir in dirs)
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
                string sModsPath = string.Format(@"{0}\{1}\minecraft\mods\", _sPacksDir, sPackname);
                Mods =  Directory.EnumerateFiles(sModsPath, "*.*")
                    .Where(f => sFileExtensions.Contains(Path.GetExtension(f).ToLower()))
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kein Mod Verzeichniss gefunden. Vanilla ?", "kein Modverzeichniss", MessageBoxButtons.OK,MessageBoxIcon.Error);
                throw ex;
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
            File.WriteAllText(_sPacksDir + @"\" + sPackName + @"\selected", sVersion);
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
            Dictionary<string, string> ClassPath = new Dictionary<string, string>(); // Library list for startup

            FilesMojang MCMojangFiles = new FilesMojang(dhelper)
            {
                LibraryDir = _sLibraryDir,
                VersionDir = _sVersionDir,
                NativesDir = _sNativesDir,
                AssetsDir = _sAssetsDir,
                OfflineMode = Offline
            };

            // check if pack is installed with given version
            if (!IsPackInstalled(sPackName, sPackVersion))
            {
                InstallPack(sPackName, sPackVersion);
            }
            
            // getting pack json file
            MCPack pack = MCPack.FromJson(File.ReadAllText(_sPacksDir + @"\" + sPackName + @"\pack.json").Trim());
            // vanilla Minecraft
            MCMojangFiles.DownloadVersionJson(pack.MCVersion);
            GameVersion MCMojang = GameVersion.FromJson(File.ReadAllText(_sVersionDir + @"\" + pack.MCVersion + @"\" + pack.MCVersion + ".json").Trim());
            // download game jar
            MCMojangFiles.DownloadClientJar(MCMojang);
            // download libraries if needed
            ClassPath = MCMojangFiles.DownloadClientLibraries(MCMojang);
            // download assets if needed
            MCMojangFiles.DownloadClientAssets(MCMojang);
            
            // additional things for forge
            if (pack.Type.Equals("forge"))
            {
                isForge = true;
                forgeVersion = pack.ForgeVersion;
                Dictionary<string, string> ForgeClassPath = new Dictionary<string, string>(); // Library list for startup
                FilesForge MCForgeFiles = new FilesForge(dhelper)
                {
                    LibraryDir = _sLibraryDir,
                    VersionDir = _sVersionDir,
                    OfflineMode = Offline
                };

                // Install Forge
                ForgeClassPath = MCForgeFiles.InstallForge(pack.ForgeVersion);

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
        
        private void Start(string args,string sPackName)
        {
            
            Configuration C = new Configuration();
            Process minecraft = new Process();

            // check for "minecraft" folder
            if (!Directory.Exists(_sPacksDir + @"\" + sPackName + @"\minecraft")) Directory.CreateDirectory(_sPacksDir + @"\" + sPackName + @"\minecraft");

            minecraft.StartInfo.FileName = C.GetJavaPath();
            minecraft.StartInfo.WorkingDirectory = _sPacksDir + @"\" + sPackName + @"\minecraft";
            minecraft.StartInfo.Arguments = args;
            minecraft.StartInfo.RedirectStandardOutput = true;
            minecraft.StartInfo.RedirectStandardError = true;
            minecraft.StartInfo.UseShellExecute = false;
            minecraft.StartInfo.CreateNoWindow = true;
            minecraft.OutputDataReceived += new DataReceivedEventHandler(Minecraft_OutputDataReceived);
            minecraft.ErrorDataReceived += new DataReceivedEventHandler(Minecraft_ErrorDataReceived);
            minecraft.Exited += new EventHandler(Minecraft_Exited);
            minecraft.EnableRaisingEvents = true;

            // load console
            if (C.ShowConsole == 1)
            {
                CloseOldConsole();
                _console = new FrmConsole();
                _console.Show();
                _console.Clear();
                _console.AddLine(String.Format("UglyLauncher-Version: {0}", Application.ProductVersion),Color.Blue);
                _console.AddLine("Using Java-Version: " + C.GetJavaPath() + " (" + C.GetJavaArch()+ "bit)", Color.Blue);
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

        private void Minecraft_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                try
                {
                    _console.AddLine(e.Data,Color.Red);
                }
                catch (Exception)
                {
                }
            }
        }

        private void Minecraft_Exited(object sender, EventArgs e)
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
        }

        private void Minecraft_OutputDataReceived(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!string.IsNullOrEmpty(outLine.Data))
            {
                try
                {
                    _console.AddLine(outLine.Data,Color.Black);
                }
                catch (Exception)
                {
                }
            }
        }
        
        private string BuildArgs(GameVersion MC,string sPackName, Dictionary<string, string> ClassPath)
        {
            string args = null;
            string classpath = null;
            Configuration C = new Configuration();
            Manager U = new Manager();
            MCUserAccount Acc = U.GetAccount(U.GetDefault());
            MCUserAccountProfile Profile = U.GetActiveProfile(Acc);

            // Garbage Collector
            if (C.UseGC == 1) args += " -XX:SurvivorRatio=2 -XX:+DisableExplicitGC -XX:+UseConcMarkSweepGC -XX:+AggressiveOpts";

            // force 64bit
            if (C.GetJavaArch() == "64") args += " -d64";

            // Java Memory
            args += string.Format(" -Xms{0}m -Xmx{1}m -Xmn128m", C.MinimumMemory, C.MaximumMemory);

            if (MC.Arguments != null)
            {
                // JVM
                foreach(JvmElement jvme in MC.Arguments.Jvm)
                {
                    if(jvme.JvmClass != null)
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
                                            if (Rule.Os.Arch == "x86" && C.GetJavaArch() == "64") bWindows = false;
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
                            foreach(string value in jvme.JvmClass.Value.StringArray)
                            {
                                // fix spaces in Json path
                                if(value.Split('=').Last().Contains(" "))
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
            classpath += string.Format("\"{0}\\{1}\\{1}.jar\" ", _sVersionDir, MC.Id);

            // fill placeholders
            args = args.Replace("${auth_player_name}", Profile.name);

            if(isForge)
            {
                args = args.Replace("${version_name}", MC.Id+ "-forge" + forgeVersion.Replace(MC.Id,""));
            }
            else
            {
                args = args.Replace("${version_name}", MC.Id);
            }
            args = args.Replace("${game_directory}", string.Format("\"{0}\\{1}\\minecraft\"", _sPacksDir, sPackName));
            args = args.Replace("${assets_root}", string.Format("\"{0}\"", _sAssetsDir));
            args = args.Replace("${game_assets}", string.Format("\"{0}\\virtual\\legacy\"", _sAssetsDir));
            args = args.Replace("${assets_index_name}", MC.Assets);
            args = args.Replace("${auth_uuid}", Profile.id);
            args = args.Replace("${auth_access_token}", Acc.accessToken);
            args = args.Replace("${auth_session}", string.Format("token:{0}:{1}", Acc.accessToken, Profile.id));
            args = args.Replace("${user_properties}", "{}");
            args = args.Replace("${user_type}", "Mojang");
            args = args.Replace("${version_type}", MC.Type);
            args = args.Replace("${natives_directory}", "\"" + _sNativesDir + @"\" + MC.Id +"\"");
            args = args.Replace("${classpath}", classpath);
            args = args.Replace("${launcher_name}", Application.ProductName);
            args = args.Replace("${launcher_version}", Application.ProductVersion);
            
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
            if (File.Exists(_sPacksDir + @"\" + sPackName + "-" + sPackVersion + ".zip"))
            {
                File.Delete(_sPacksDir + @"\" + sPackName + "-" + sPackVersion + ".zip");
            }

            MCAvailablePackVersion version = GetAvailablePackVersion(sPackName, sPackVersion);
            if (version.DownloadZip == true)
            {
                // download pack
                dhelper.DownloadFileTo(_sPackServer + "/packs/" + sPackName + "/" + sPackName + "-" + sPackVersion + ".zip", _sPacksDir + @"\" + sPackName + "-" + sPackVersion + ".zip", true, "Downloading Pack " + sPackName);

                // Hide Bar
                dhelper.HideBar();

                // unzip pack
                dhelper.ExtractZipFiles(_sPacksDir + @"\" + sPackName + "-" + sPackVersion + ".zip", _sPacksDir);

                // delete zip file
                File.Delete(_sPacksDir + @"\" + sPackName + "-" + sPackVersion + ".zip");
            }
            else
            {
                // write pack.json file
                MCPack pack = new MCPack
                {
                    Type = "vanilla",
                    MCVersion = version.Version
                };

                File.WriteAllText(_sPacksDir + @"\" + sPackName +  @"\pack.json", pack.ToJson());

                // write version file
                File.WriteAllText(_sPacksDir + @"\" + sPackName + @"\version", version.Version);
            }

            // check for "minecraft" folder
            if (!Directory.Exists(_sPacksDir + @"\" + sPackName + @"\minecraft"))
            {
                Directory.CreateDirectory(_sPacksDir + @"\" + sPackName + @"\minecraft");
            }
        }

        private void DeletePack(string sPackName)
        {
            string PackDir = _sPacksDir + @"\" + sPackName;

            if (!Directory.Exists(PackDir)) return; // Pack did not exist
            // Delete Directories
            if (Directory.Exists(PackDir + @"\minecraft\logs")) Directory.Delete(PackDir + @"\minecraft\logs", true);
            if (Directory.Exists(PackDir + @"\minecraft\mods")) Directory.Delete(PackDir + @"\minecraft\mods", true);
            if (Directory.Exists(PackDir + @"\minecraft\modclasses")) Directory.Delete(PackDir + @"\minecraft\\modclasses", true);
            if (Directory.Exists(PackDir + @"\minecraft\config")) Directory.Delete(PackDir + @"\minecraft\config", true);
            if (Directory.Exists(PackDir + @"\minecraft\stats")) Directory.Delete(PackDir + @"\minecraft\stats", true);
            if (Directory.Exists(PackDir + @"\minecraft\crash-reports")) Directory.Delete(PackDir + @"\minecraft\crash-reports", true);
            // Deleting .log Files
            foreach (FileInfo f in new DirectoryInfo(PackDir + @"\minecraft").GetFiles("*.log"))
                f.Delete();
            // Deleting .lck Files
            foreach (FileInfo f in new DirectoryInfo(PackDir + @"\minecraft").GetFiles("*.lck"))
                f.Delete();
            // Deleting .1 Files
            foreach (FileInfo f in new DirectoryInfo(PackDir + @"\minecraft").GetFiles("*.1"))
                f.Delete();
            // Deleting pack.json
            if (File.Exists(PackDir + @"\pack.json")) File.Delete(PackDir + @"\pack.json");
            // Deleting version
            if (File.Exists(PackDir + @"\version")) File.Delete(PackDir + @"\version");
        }

        public void Dispose()
        {
            Dispose();
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
