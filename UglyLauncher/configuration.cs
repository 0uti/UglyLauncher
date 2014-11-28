using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Globalization;

namespace UglyLauncher
{
    class configuration
    {
        private string sRegPath = "Software\\Minestar\\UglyLauncher";
        private string sJavaPath = null;
        private string sJavaArch = null;
        private double dJava_version = 0;

        private int iMinMemory = -1;
        private int iMaxMemory = -1;
        private int iPermGen = -1;
        private int iConsole_Show = -1;
        private int iConsole_Keep = -1;
        private int iCloseLauncher = -1;
        private int iUseGC = -1;
        private string sJavaVersion = null;


        // contructor
        public configuration()
        {
            this.iMinMemory = this.GetRegInt("min_memory");
            this.iMaxMemory = this.GetRegInt("max_memory");
            this.iPermGen = this.GetRegInt("perm_gen");
            this.iConsole_Show = this.GetRegInt("show_console");
            this.iConsole_Keep = this.GetRegInt("keep_console");
            this.iCloseLauncher = this.GetRegInt("close_Launcher");
            this.iUseGC = this.GetRegInt("use_gc");
            this.sJavaVersion = this.GetRegString("java_version");
            this.GetJavaPathAuto(this.JavaVersion);
        }

        // java version double
        public double dJavaVesion
        {
            get
            {
                return this.dJava_version;
            }
        }

        // Java search methode
        public string JavaVersion
        {
            get
            {
                if (this.sJavaVersion != null) return this.sJavaVersion;
                else return this.SetRegString("java_version", "auto");
            }
            set
            {
                this.sJavaVersion = value;
                this.SetRegString("java_version", this.sJavaVersion);
            }
        }

        // Minimum memoryusage
        public int MinimumMemory
        {
            get
            {
                if (this.iMinMemory != -1) return this.iMinMemory;
                else return this.SetRegInt("min_memory", 512);
            }
            set
            {
                this.iMinMemory = value;
                this.SetRegInt("min_memory", this.iMinMemory);
            }
        }

        // Maximum memoryusage
        public int MaximumMemory
        {
            get
            {
                if (this.iMaxMemory != -1) return this.iMaxMemory;
                else return this.SetRegInt("max_memory", 2048);
            }
            set
            {
                this.iMaxMemory = value;
                this.SetRegInt("max_memory", this.iMaxMemory);
            }
        }

        // PermGen
        public int PermGen
        {
            get
            {
                if (this.iPermGen != -1) return this.iPermGen;
                else return this.SetRegInt("perm_gen", 256);
            }
            set
            {
                this.iPermGen = value;
                this.SetRegInt("perm_gen", this.iPermGen);
            }
        }

        // Garbage Collector
        public int UseGC
        {
            get
            {
                if (this.iUseGC != -1) return this.iUseGC;
                else return this.SetRegInt("use_gc", 0);
            }
            set
            {
                this.iUseGC = value;
                this.SetRegInt("use_gc", this.iUseGC);
            }
        }

        // show console
        public int ShowConsole
        {
            get
            {
                if (this.iConsole_Show != -1) return this.iConsole_Show;
                else return this.SetRegInt("show_console", 0);
            }
            set
            {
                this.iConsole_Show = value;
                this.SetRegInt("show_console",this.iConsole_Show);
            }
        }

        // keep console
        public int KeepConsole
        {
            get
            {
                if (this.iConsole_Keep != -1) return this.iConsole_Keep;
                else return this.SetRegInt("keep_console", 0);
            }
            set
            {
                this.iConsole_Keep = value;
                this.SetRegInt("keep_console", this.iConsole_Keep);
            }
        }

        // close Launcher
        public int CloseLauncher
        {
            get
            {
                if (this.iCloseLauncher != -1) return this.iCloseLauncher;
                else return this.SetRegInt("close_Launcher", 0);
            }
            set
            {
                this.iCloseLauncher = value;
                this.SetRegInt("close_Launcher", this.iCloseLauncher);
            }
        }
        
        // Old Shit

        public string GetJavaArch()
        {
            return this.sJavaArch;
        }

        public string GetJavaPath()
        {
            return this.sJavaPath;
        }


        public List<string> GetJavaVersions()
        {
            List<string> lVersions = new List<string>();
            string[] lSubkeys;
            RegistryKey key;

            // get 64bit vrsions
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment");
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
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\JavaSoft\Java Runtime Environment");
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
                this.GetJavaPath64(sVersion);
                // if 64bit not found, look for 32bit Java
                if (this.sJavaPath == null) this.GetJavaPath32(sVersion);
                // if still no Java Found -> Bullshit
            }
            else
            {
                string[] versions = sVersion.Split('_');
                if (versions[1] == "64") this.GetJavaPath64(versions[0]);
                if (versions[1] == "32") this.GetJavaPath32(versions[0]);
            }
        }

        private void GetJavaPath64(string sVersion)
        {
            RegistryKey key ;

            if (sVersion == "auto")
            {
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment");
                if (key == null) return;  // no java 64 found
                sVersion = key.GetValue("CurrentVersion", null) as String;
            }

            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment\" + sVersion);
            if (key == null) return ;  // no java 64 found
            this.sJavaPath = key.GetValue("JavaHome", null) as String;
            // append executable
            this.sJavaPath += @"\bin\java";
            this.sJavaArch = "64";
            this.dJava_version = Double.Parse(sVersion, CultureInfo.InvariantCulture);
        }

        private void GetJavaPath32(string sVersion)
        {
            RegistryKey key;

            if (sVersion == "auto")
            {
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\JavaSoft\Java Runtime Environment");
                if (key == null) return;  // no java 32 found
                sVersion = key.GetValue("CurrentVersion", null) as String;
            }

            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\JavaSoft\Java Runtime Environment\" + sVersion);
            if (key == null) return;  // no java 32 found
            this.sJavaPath = key.GetValue("JavaHome", null) as String;
            // append executable
            this.sJavaPath += @"\bin\java";
            this.sJavaArch = "32";
            this.dJava_version = Double.Parse(sVersion, CultureInfo.InvariantCulture);
        }

        // Registry Handler
        private string GetRegString(string sRegKey)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(this.sRegPath);
            if (key == null) return null;
            return key.GetValue(sRegKey, null) as String;
        }

        private int GetRegInt(string sRegKey)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(this.sRegPath);
            if (key == null) return -1;
            return (int)key.GetValue(sRegKey, -1);
        }

        private string SetRegString(string sRegKey, string sRegValue)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(this.sRegPath);
            key.SetValue(sRegKey, sRegValue, RegistryValueKind.String);
            return sRegValue;
        }

        private int SetRegInt(string sRegKey, int iRegValue)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(this.sRegPath);
            key.SetValue(sRegKey, iRegValue, RegistryValueKind.DWord);
            return iRegValue;
        }
    }
}
