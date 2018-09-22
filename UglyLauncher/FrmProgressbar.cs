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
            ProgressBar.BeginInvoke(
                new Action(() =>
                {
                    ProgressBar.Value = percent;
                }
            ));
        }

        public void SetLabel(string text)
        {
            LblFileName.BeginInvoke(
                new Action(() =>
                {
                    LblFileName.Text = text;
                }
            ));
        }
    }
}
