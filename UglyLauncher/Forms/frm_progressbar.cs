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
    public partial class frm_progressbar : Form
    {
        public frm_progressbar()
        {
            InitializeComponent();
        }

        public void update_bar(int percent)
        {
            progressBar1.BeginInvoke(
                new Action(() =>
                {
                    progressBar1.Value = percent;
                }
            ));
        }

        public void setLabel(string text)
        {
            label1.BeginInvoke(
                new Action(() =>
                {
                    label1.Text = text;
                }
            ));
        }
    }
}
