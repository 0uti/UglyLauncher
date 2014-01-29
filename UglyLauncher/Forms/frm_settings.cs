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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_settings_Load(object sender, EventArgs e)
        {
            this.java_min_mem.Value = C.MinimumMemory;
            this.java_max_mem.Value = C.MaximumMemory;
            this.java_perm_gen.Value = C.PermGen;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            C.MinimumMemory = (int) this.java_min_mem.Value;
            C.MaximumMemory = (int) this.java_max_mem.Value;
            C.PermGen = (int)this.java_perm_gen.Value;
            this.Close();
        }
    }
}
