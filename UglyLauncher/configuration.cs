using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace UglyLauncher
{
    class configuration
    {
        public string GetJavaPath()
        {
            // Get 64bit Java
            string sJavaPath = this.GetJavaPath64();
            // if 64bit not found, look for 32bit Java
            if (sJavaPath == null) sJavaPath = this.GetJavaPath32();
            // if still no Java Found -> Bullshit!
            return sJavaPath;
        }


        private string GetJavaPath64()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment");
            if (key == null) return null;  // no java 64 found
            string sCurrentVersion = key.GetValue("CurrentVersion",null) as String;

            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment\" + sCurrentVersion);
            if (key == null) return null;  // no java 64 found
            string sJavaPath = key.GetValue("JavaHome", null) as String;
            // append executable
            sJavaPath += @"\bin\java";

            return sJavaPath;
        }

        private string GetJavaPath32()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\JavaSoft\Java Runtime Environment");
            if (key == null) return null;  // no java 32 found
            string sCurrentVersion = key.GetValue("CurrentVersion", null) as String;

            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\JavaSoft\Java Runtime Environment\" + sCurrentVersion);
            if (key == null) return null;  // no java 32 found
            string sJavaPath = key.GetValue("JavaHome", null) as String;
            // append executable
            sJavaPath += @"\bin\java";

            return sJavaPath;
        }

    }
}
