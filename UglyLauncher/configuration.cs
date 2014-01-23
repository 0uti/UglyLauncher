using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace UglyLauncher
{
    class configuration
    {
        private string sJavaPath = null;
        private string sJavaArch = null;

        public configuration()
        {
            // Get 64bit Java
            this.GetJavaPath64();
            // if 64bit not found, look for 32bit Java
            if (this.sJavaPath == null) this.GetJavaPath32();
            // if still no Java Found -> Bullshit
        }

        public string GetJavaArch()
        {
            return this.sJavaArch;
        }

        public string GetJavaPath()
        {
            return this.sJavaPath;
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
    }
}
