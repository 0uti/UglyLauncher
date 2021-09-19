using UglyLauncher.Minecraft;
using System;
using System.Windows.Forms;

namespace UglyLauncher
{
    static class Program
    {
        public static StartupSide Side = StartupSide.Server;

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Diagnostics.Process[] localByName = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location));
            foreach (System.Diagnostics.Process _pr in localByName)
            {
                if (_pr.Id != System.Diagnostics.Process.GetCurrentProcess().Id)
                {
                    MessageBox.Show("Der UglyLauncher läuft bereits.", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain(Side));
        }
    }
}
