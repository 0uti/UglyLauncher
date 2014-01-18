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
            pbar_progress.BeginInvoke(
                new Action(() =>
                {
                    pbar_progress.Value = percent;
                }
            ));
        }

        public void setLabel(string text)
        {
            lbl_FileName.BeginInvoke(
                new Action(() =>
                {
                    lbl_FileName.Text = text;
                }
            ));
        }
    }
}
