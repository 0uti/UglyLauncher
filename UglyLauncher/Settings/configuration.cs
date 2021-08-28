using Microsoft.Win32;
using System.Collections.Generic;

namespace UglyLauncher.Settings
{
    class Configuration
    {
        private readonly string sRegPath = "Software\\Minestar\\UglyLauncher";

        private int iMinMemory = -1;
        private int iMaxMemory = -1;
        private int iConsole_Show = -1;
        private int iConsole_Keep = -1;
        private int iCloseLauncher = -1;
        private int iUseGC = -1;

        // contructor
        public Configuration()
        {
            iMinMemory = GetRegInt("min_memory");
            iMaxMemory = GetRegInt("max_memory");
            iConsole_Show = GetRegInt("show_console");
            iConsole_Keep = GetRegInt("keep_console");
            iCloseLauncher = GetRegInt("close_Launcher");
            iUseGC = GetRegInt("use_gc");
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
