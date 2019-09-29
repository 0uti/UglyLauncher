using System.Collections.Generic;
using Microsoft.Win32;

namespace UglyLauncher.Settings
{
    class Configuration
    {
        private readonly string sJavaExecutable = @"\bin\javaw";
        private readonly string sRegPath = "Software\\Minestar\\UglyLauncher";
        private string sJavaPath = null;
        private string sJavaArch = null;

        private int iMinMemory = -1;
        private int iMaxMemory = -1;
        private int iConsole_Show = -1;
        private int iConsole_Keep = -1;
        private int iCloseLauncher = -1;
        private int iUseGC = -1;
        private string sJavaVersion = null;


        // contructor
        public Configuration()
        {
            iMinMemory = GetRegInt("min_memory");
            iMaxMemory = GetRegInt("max_memory");
            iConsole_Show = GetRegInt("show_console");
            iConsole_Keep = GetRegInt("keep_console");
            iCloseLauncher = GetRegInt("close_Launcher");
            iUseGC = GetRegInt("use_gc");
            sJavaVersion = GetRegString("java_version");
            GetJavaPathAuto(JavaVersion);
        }

        // Java search methode
        public string JavaVersion
        {
            get
            {
                if (sJavaVersion != null) return sJavaVersion;
                else return SetRegString("java_version", "auto");
            }
            set
            {
                sJavaVersion = value;
                SetRegString("java_version", sJavaVersion);
            }
        }

        // Minimum memoryusage
        public int MinimumMemory
        {
            get
            {
                if (iMinMemory != -1) return iMinMemory;
                else return SetRegInt("min_memory", 512);
            }
            set
            {
                iMinMemory = value;
                SetRegInt("min_memory", iMinMemory);
            }
        }

        // Maximum memoryusage
        public int MaximumMemory
        {
            get
            {
                if (iMaxMemory != -1) return iMaxMemory;
                else return SetRegInt("max_memory", 2048);
            }
            set
            {
                iMaxMemory = value;
                SetRegInt("max_memory", iMaxMemory);
            }
        }

        // Garbage Collector
        public int UseGC
        {
            get
            {
                if (iUseGC != -1) return iUseGC;
                else return SetRegInt("use_gc", 0);
            }
            set
            {
                iUseGC = value;
                SetRegInt("use_gc", iUseGC);
            }
        }

        // show console
        public int ShowConsole
        {
            get
            {
                if (iConsole_Show != -1) return iConsole_Show;
                else return SetRegInt("show_console", 0);
            }
            set
            {
                iConsole_Show = value;
                SetRegInt("show_console", iConsole_Show);
            }
        }

        // keep console
        public int KeepConsole
        {
            get
            {
                if (iConsole_Keep != -1) return iConsole_Keep;
                else return SetRegInt("keep_console", 0);
            }
            set
            {
                iConsole_Keep = value;
                SetRegInt("keep_console", iConsole_Keep);
            }
        }

        // close Launcher
        public int CloseLauncher
        {
            get
            {
                if (iCloseLauncher != -1) return iCloseLauncher;
                else return SetRegInt("close_Launcher", 0);
            }
            set
            {
                iCloseLauncher = value;
                SetRegInt("close_Launcher", iCloseLauncher);
            }
        }
        
        // Old Shit

        public string GetJavaArch()
        {
            return sJavaArch;
        }

        public string GetJavaPath()
        {
            return sJavaPath;
        }


        public List<string> GetJavaVersions()
        {
            List<string> lVersions = new List<string>();
            string[] lSubkeys;
            RegistryKey key;
            RegistryKey hklm64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey hklm32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

            // get 64bit vrsions
            key = hklm64.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment");
            if (key != null)
            {
                lSubkeys = key.GetSubKeyNames();

                foreach (string version in lSubkeys)
                {
                    if (version.Length == 3)
                    {
                        lVersions.Add(version + "_64");
                    }
                }
            }
            // get 32bit versions
            key = hklm32.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment");
            if (key != null)
            {
                lSubkeys = key.GetSubKeyNames();

                foreach (string version in lSubkeys)
                {
                    if (version.Length == 3)
                    {
                        lVersions.Add(version + "_32");
                    }
                }
            }
            return lVersions;
        }


        private void GetJavaPathAuto(string sVersion = "auto")
        {
            if (sVersion == "auto")
            {
                // Get 64bit Java
                GetJavaPath64(sVersion);
                // if 64bit not found, look for 32bit Java
                if (sJavaPath == null) GetJavaPath32(sVersion);
                // if still no Java Found -> Bullshit
            }
            else
            {
                string[] versions = sVersion.Split('_');
                if (versions[1] == "64") GetJavaPath64(versions[0]);
                if (versions[1] == "32") GetJavaPath32(versions[0]);
            }
        }

        private void GetJavaPath64(string sVersion)
        {
            RegistryKey key ;
            RegistryKey hklm64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);

            if (sVersion == "auto")
            {
                key = hklm64.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment");
                if (key == null) return;  // no java 64 found
                sVersion = key.GetValue("CurrentVersion", null) as string;
            }

            key = hklm64.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment\" + sVersion);
            if (key == null) return ;  // no java 64 found
            sJavaPath = key.GetValue("JavaHome", null) as string;
            // append executable
            sJavaPath += sJavaExecutable;
            sJavaArch = "64";
        }

        private void GetJavaPath32(string sVersion)
        {
            RegistryKey key;
            RegistryKey hklm32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

            if (sVersion == "auto")
            {
                key = hklm32.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment");
                if (key == null) return;  // no java 32 found
                sVersion = key.GetValue("CurrentVersion", null) as string;
            }

            key = hklm32.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment\" + sVersion);
            if (key == null) return;  // no java 32 found
            sJavaPath = key.GetValue("JavaHome", null) as string;
            // append executable
            sJavaPath += sJavaExecutable;
            sJavaArch = "32";
        }

        // Registry Handler
        private string GetRegString(string sRegKey)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(sRegPath);
            if (key == null) return null;
            return key.GetValue(sRegKey, null) as string;
        }

        private int GetRegInt(string sRegKey)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(sRegPath);
            if (key == null) return -1;
            return (int)key.GetValue(sRegKey, -1);
        }

        private string SetRegString(string sRegKey, string sRegValue)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(sRegPath);
            key.SetValue(sRegKey, sRegValue, RegistryValueKind.String);
            return sRegValue;
        }

        private int SetRegInt(string sRegKey, int iRegValue)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(sRegPath);
            key.SetValue(sRegKey, iRegValue, RegistryValueKind.DWord);
            return iRegValue;
        }
    }
}
