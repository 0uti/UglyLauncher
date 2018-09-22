using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

using UglyLauncher.Minecraft;
using UglyLauncher.Minecraft.Json.MCAuthenticateResponse;

namespace UglyLauncher.AccountManager
{
    public partial class FrmRefreshToken : Form
    {
        private MCUserAccount oUser;


        public FrmRefreshToken(MCUserAccount user)
        {
            InitializeComponent();
            oUser = user;
            txt_user.Text = user.username;
            txt_user.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txt_user.Text == "" || txt_pass.Text == "")
            {
                MessageBox.Show(this, "Eines der Felder ist leer.", "Fehlerhafte Eingabe!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Check LoginData
                Authentication Auth = new Authentication();
                MCAuthenticateResponse AuthData = new MCAuthenticateResponse();
                try
                {
                    AuthData = Auth.Authenticate(txt_user.Text.ToString().Trim(), txt_pass.Text.ToString().Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString(),"Fehlermeldung von Minecraft.net",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txt_pass.Focus();
                    txt_pass.SelectAll();
                    return;
                }

                // Load users.xml
                Manager U = new Manager();
                oUser.accessToken = AuthData.AccessToken;
                oUser.clientToken = AuthData.ClientToken;
                U.SaveAccount(oUser);

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void txt_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button2_Click(sender,e);
        }
    }
}
