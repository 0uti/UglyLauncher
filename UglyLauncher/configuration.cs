using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace UglyLauncher
{
    class configuration
    {
        private string sRegPath = "Software\\Minestar\\UglyLauncher";
        private string sJavaPath = null;
        private string sJavaArch = null;

        private int iMinMemory = -1;
        private int iMaxMemory = -1;
        private int iPermGen = -1;
        private int iConsole = -1;


        // contructor
        public configuration()
        {
            this.iMinMemory = this.GetRegInt("min_memory");
            this.iMaxMemory = this.GetRegInt("max_memory");
            this.iPermGen = this.GetRegInt("perm_gen");
            this.iConsole = this.GetRegInt("show_console");
            this.GetJavaPathAuto();
        }

        // Minimum memoryusage
        public int MinimumMemory
        {
            get
            {
                if (this.iMinMemory != -1) return this.iMinMemory;
                else return this.SetRegInt("min_memory", 1024);
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
                else return this.SetRegInt("perm_gen", 128);
            }
            set
            {
                this.iPermGen = value;
                this.SetRegInt("perm_gen", this.iPermGen);
            }
        }

        // console
        public int ShowConsole
        {
            get
            {
                if (this.iConsole != -1) return this.iConsole;
                else return this.SetRegInt("show_console", 0);
            }
            set
            {
                this.iConsole = value;
                this.SetRegInt("show_console",this.iConsole);
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


        private void GetJavaPathAuto()
        {
            // Get 64bit Java
            this.GetJavaPath64();
            // if 64bit not found, look for 32bit Java
            if (this.sJavaPath == null) this.GetJavaPath32();
            // if still no Java Found -> Bullshit
        }



        private void GetJavaPath64()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment");
            if (key == null) return;  // no java 64 found
            string sCurrentVersion = key.GetValue("CurrentVersion",null) as String;

            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment\" + sCurrentVersion);
            if (key == null) return ;  // no java 64 found
            this.sJavaPath = key.GetValue("JavaHome", null) as String;
            // append executable
            this.sJavaPath += @"\bin\java";
            this.sJavaArch = "64";
        }

        private void GetJavaPath32()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\JavaSoft\Java Runtime Environment");
            if (key == null) return;  // no java 32 found
            string sCurrentVersion = key.GetValue("CurrentVersion", null) as String;

            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\JavaSoft\Java Runtime Environment\" + sCurrentVersion);
            if (key == null) return;  // no java 32 found
            this.sJavaPath = key.GetValue("JavaHome", null) as String;
            // append executable
            this.sJavaPath += @"\bin\java";
            this.sJavaArch = "32";
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
