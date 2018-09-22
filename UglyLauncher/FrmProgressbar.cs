using System;
using System.Windows.Forms;

namespace UglyLauncher
{
    public partial class FrmProgressbar : Form
    {
        public FrmProgressbar()
        {
            InitializeComponent();
        }

        public void UpdateBar(int percent)
        {
            pbar_progress.BeginInvoke(
                new Action(() =>
                {
                    pbar_progress.Value = percent;
                }
            ));
        }

        public void SetLabel(string text)
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
