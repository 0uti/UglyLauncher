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
    public partial class frm_console : Form
    {
        public frm_console()
        {
            InitializeComponent();
        }

        public void addline(string line)
        {
            txt_console.BeginInvoke(
                new Action(() =>
                {
                    txt_console.AppendText(line + Environment.NewLine);
                    txt_console.ScrollToCaret();
                }
            ));
            
        }

        public void hideme()
        {
            this.BeginInvoke(new Action(() =>
                {
                    this.Hide();
                }
            ));
        }

        public void clearcon()
        {
            txt_console.Clear();
        }
    }
}
