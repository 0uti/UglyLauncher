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
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();

            UserManager U = new UserManager();
            txt_default_account.Text = U.GetDefault();
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void accountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frm_UserAccounts().ShowDialog();
            
            UserManager U = new UserManager();
            txt_default_account.Text = U.GetDefault();
            
        }
    }
}
