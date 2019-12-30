using System;
using System.Drawing;
using System.Windows.Forms;

namespace UglyLauncher
{
    public partial class FrmConsole : Form
    {
        public FrmConsole()
        {
            InitializeComponent();
        }

        public void AddLine(string line, Color color)
        {
            try
            {
                TxtConsole.BeginInvoke(
                    new Action(() =>
                    {
                        TxtConsole.SelectionColor = color;
                        TxtConsole.AppendText(line + "\n");
                        TxtConsole.ScrollToCaret();
                    }
                ));
            }
            catch (Exception)
            {
            }

        }

        public void Clear()
        {
            TxtConsole.Clear();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TxtConsole_TextChanged(object sender, EventArgs e)
        {
            if (TxtConsole.Lines.Length > 500)
            {
                // ToDo: delete old lines
            }
        }
    }
}
