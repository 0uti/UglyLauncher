using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UglyLauncher.Settings
{
    public partial class Settings : Form
    {
        private readonly Configuration C = new Configuration();

        public Settings()
        {
            InitializeComponent();
        }

        private void BtnCancel_click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            NumJavaMinMemory.Value = C.MinimumMemory;
            NumJavaMaxMemory.Minimum = C.MinimumMemory;

            NumJavaMaxMemory.Value = C.MaximumMemory;
            NumJavaMinMemory.Maximum = C.MaximumMemory;

            if (C.KeepConsole == 1) ChkKeepConsole.Checked = true;
            else ChkKeepConsole.Checked = false;

            if (C.ShowConsole == 1)
            {
                ChkShowConsole.Checked = true;
                ChkKeepConsole.Enabled = true;
            }
            else
            {
                ChkShowConsole.Checked = false;
                ChkKeepConsole.Enabled = false;
            }


            if (C.UseGC == 1) ChkUseGC.Checked = true;
            else ChkUseGC.Checked = false;

            if (C.CloseLauncher == 1) ChkKeepLauncher.Checked = false;
            else ChkKeepLauncher.Checked = true;


            CmbJavaVersion.Items.Clear();
            CmbJavaVersion.Items.Add("Automatisch");

            List<String> lVersions = C.GetJavaVersions();
            foreach (string version in lVersions)
            {
                CmbJavaVersion.Items.Add(version);
            }
            if (C.JavaVersion == "auto")
            {
                CmbJavaVersion.SelectedIndex = CmbJavaVersion.FindStringExact("Automatisch");
            }
            else
            {
                CmbJavaVersion.SelectedIndex = CmbJavaVersion.FindStringExact(C.JavaVersion);
            }
        }

        private void BtnSave_click(object sender, EventArgs e)
        {
            C.MinimumMemory = (int)NumJavaMinMemory.Value;

            C.MaximumMemory = (int)NumJavaMaxMemory.Value;

            if (ChkShowConsole.Checked == true) C.ShowConsole = 1;
            else C.ShowConsole = 0;

            if (ChkKeepConsole.Checked == true) C.KeepConsole = 1;
            else C.KeepConsole = 0;

            if (ChkKeepLauncher.Checked == false) C.CloseLauncher = 1;
            else C.CloseLauncher = 0;

            if (CmbJavaVersion.SelectedItem.ToString() == "Automatisch") C.JavaVersion = "auto";
            else C.JavaVersion = CmbJavaVersion.SelectedItem.ToString();

            if (ChkUseGC.Checked == true) C.UseGC = 1;
            else C.UseGC = 0;

            Close();
        }

        private void NumJavaMinMemory_ValueChanged(object sender, EventArgs e)
        {
            NumJavaMaxMemory.Minimum = NumJavaMinMemory.Value;
        }

        private void ChkShowConsole_CheckedChanged(object sender, EventArgs e)
        {
            ChkKeepConsole.Enabled = ChkShowConsole.Checked;
        }
    }
}
