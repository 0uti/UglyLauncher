using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UglyLauncher
{
    public partial class frm_settings : Form
    {
        private configuration C = new configuration();

        public frm_settings()
        {
            InitializeComponent();
        }

        private void btn_cancel_click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_settings_Load(object sender, EventArgs e)
        {
            this.java_min_mem.Value = C.MinimumMemory;
            this.java_max_mem.Minimum = C.MinimumMemory;

            this.java_max_mem.Value = C.MaximumMemory;
            this.java_min_mem.Maximum = C.MaximumMemory;

            if (C.KeepConsole == 1) this.chk_keep_console.Checked = true;
            else this.chk_keep_console.Checked = false;
          
            if (C.ShowConsole == 1)
            {
                this.chk_console.Checked = true;
                this.chk_keep_console.Enabled = true;
            }
            else
            {
                this.chk_console.Checked = false;
                this.chk_keep_console.Enabled = false;
            }


            if (C.UseGC == 1) this.chk_use_gc.Checked = false;
            else this.chk_use_gc.Checked = true;

            if (C.CloseLauncher == 1) this.chk_keep_launcher.Checked = false;
            else this.chk_keep_launcher.Checked = true;


            this.comboBox1.Items.Clear();
            this.comboBox1.Items.Add("Automatisch");

            List<String> lVersions = C.GetJavaVersions();
            foreach (string version in lVersions)
            {
                this.comboBox1.Items.Add(version);
            }
            if (C.JavaVersion == "auto")
            {
                this.comboBox1.SelectedIndex = this.comboBox1.FindStringExact("Automatisch");
            }
            else
            {
                this.comboBox1.SelectedIndex = this.comboBox1.FindStringExact(C.JavaVersion);
            }
        }

        private void btn_save_click(object sender, EventArgs e)
        {
            C.MinimumMemory = (int) this.java_min_mem.Value;

            C.MaximumMemory = (int) this.java_max_mem.Value;

            if (this.chk_console.Checked == true) C.ShowConsole = 1;
            else C.ShowConsole = 0;

            if (this.chk_keep_console.Checked == true) C.KeepConsole = 1;
            else C.KeepConsole = 0;

            if (this.chk_keep_launcher.Checked == false) C.CloseLauncher = 1;
            else C.CloseLauncher = 0;

            if (this.comboBox1.SelectedItem.ToString() == "Automatisch") C.JavaVersion = "auto";
            else C.JavaVersion = this.comboBox1.SelectedItem.ToString();

            if (this.chk_use_gc.Checked == false) C.UseGC = 1;
            else C.UseGC = 0;

            this.Close();
        }

        private void java_min_mem_ValueChanged(object sender, EventArgs e)
        {
            this.java_max_mem.Minimum = this.java_min_mem.Value;
        }

        private void chk_console_CheckedChanged(object sender, EventArgs e)
        {
            this.chk_keep_console.Enabled = this.chk_console.Checked;
        }
    }
}
