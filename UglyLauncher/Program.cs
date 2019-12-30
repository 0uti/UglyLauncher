using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Windows.Forms;

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
            System.Diagnostics.Process[] localByName = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location));
            foreach (System.Diagnostics.Process _pr in localByName)
            {
                if (_pr.Id != System.Diagnostics.Process.GetCurrentProcess().Id)
                {
                    MessageBox.Show("Der UglyLauncher läuft bereits.", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            // eae8cecc-253f-4cf3-90a8-f37d31b74252

            AppCenter.Start("eae8cecc-253f-4cf3-90a8-f37d31b74252",typeof(Analytics), typeof(Crashes));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
