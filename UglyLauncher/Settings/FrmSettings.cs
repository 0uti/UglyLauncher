using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UglyLauncher.Settings
{
    public partial class Settings : Form
    {
        private Configuration C = new Configuration();

        public Settings()
        {
            InitializeComponent();
        }

        private void btn_cancel_click(object sender, EventArgs e)
        {
            Close();
        }

        private void frm_settings_Load(object sender, EventArgs e)
        {
            java_min_mem.Value = C.MinimumMemory;
            java_max_mem.Minimum = C.MinimumMemory;

            java_max_mem.Value = C.MaximumMemory;
            java_min_mem.Maximum = C.MaximumMemory;

            if (C.KeepConsole == 1) chk_keep_console.Checked = true;
            else chk_keep_console.Checked = false;
          
            if (C.ShowConsole == 1)
            {
                chk_console.Checked = true;
                chk_keep_console.Enabled = true;
            }
            else
            {
                chk_console.Checked = false;
                chk_keep_console.Enabled = false;
            }


            if (C.UseGC == 1) chk_use_gc.Checked = true;
            else chk_use_gc.Checked = false;

            if (C.CloseLauncher == 1) chk_keep_launcher.Checked = false;
            else chk_keep_launcher.Checked = true;


            comboBox1.Items.Clear();
            comboBox1.Items.Add("Automatisch");

            List<String> lVersions = C.GetJavaVersions();
            foreach (string version in lVersions)
            {
                comboBox1.Items.Add(version);
            }
            if (C.JavaVersion == "auto")
            {
                comboBox1.SelectedIndex = comboBox1.FindStringExact("Automatisch");
            }
            else
            {
                comboBox1.SelectedIndex = comboBox1.FindStringExact(C.JavaVersion);
            }
        }

        private void btn_save_click(object sender, EventArgs e)
        {
            C.MinimumMemory = (int)java_min_mem.Value;

            C.MaximumMemory = (int)java_max_mem.Value;

            if (chk_console.Checked == true) C.ShowConsole = 1;
            else C.ShowConsole = 0;

            if (chk_keep_console.Checked == true) C.KeepConsole = 1;
            else C.KeepConsole = 0;

            if (chk_keep_launcher.Checked == false) C.CloseLauncher = 1;
            else C.CloseLauncher = 0;

            if (comboBox1.SelectedItem.ToString() == "Automatisch") C.JavaVersion = "auto";
            else C.JavaVersion = comboBox1.SelectedItem.ToString();

            if (chk_use_gc.Checked == true) C.UseGC = 1;
            else C.UseGC = 0;

            Close();
        }

        private void java_min_mem_ValueChanged(object sender, EventArgs e)
        {
            java_max_mem.Minimum = java_min_mem.Value;
        }

        private void chk_console_CheckedChanged(object sender, EventArgs e)
        {
            chk_keep_console.Enabled = chk_console.Checked;
        }
    }
}
