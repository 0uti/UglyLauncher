using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;


namespace UglyLauncher
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Init Structure
            AppPathes.CheckDirectories();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new main());
        }
    }


    public static class AppPathes
    {
        public static string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string sDataDir = appData + @"\.UglyLauncher";
        public static string sLibraryDir = appData + @"\.UglyLauncher\libraries";
        public static string sAssetsDir = appData + @"\.UglyLauncher\assets";
        public static string sVersionDir = appData + @"\.UglyLauncher\versions";
        public static string sPacksDir = appData + @"\.UglyLauncher\packs";

        public static void CheckDirectories()
        {
            if (!Directory.Exists(sDataDir)) Directory.CreateDirectory(sDataDir);
            if (!Directory.Exists(sLibraryDir)) Directory.CreateDirectory(UglyLauncher.AppPathes.sLibraryDir);
            if (!Directory.Exists(sAssetsDir)) Directory.CreateDirectory(UglyLauncher.AppPathes.sAssetsDir);
            if (!Directory.Exists(sVersionDir)) Directory.CreateDirectory(UglyLauncher.AppPathes.sVersionDir);
            if (!Directory.Exists(sPacksDir)) Directory.CreateDirectory(UglyLauncher.AppPathes.sPacksDir);
        }
    }

    
}
