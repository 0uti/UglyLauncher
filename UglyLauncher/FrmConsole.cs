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

        public void AddLine(string line,Color color)
        {
            try
            {
                txt_console.BeginInvoke(
                    new Action(() =>
                    {
                        txt_console.SelectionColor = color;
                        txt_console.AppendText(line + "\n");
                        txt_console.ScrollToCaret();
                    }
                ));
            }
            catch (Exception)
            {
            }
            
        }

        public void Clear()
        {
            txt_console.Clear();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txt_console_TextChanged(object sender, EventArgs e)
        {
            if (txt_console.Lines.Length > 500)
            {
                
            }
        }
    }
}
