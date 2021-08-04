using System;
using System.Windows.Forms;
using UglyLauncher.Minecraft.Authentication;
using UglyLauncher.Minecraft.Authentication.Json.AuthenticateResponse;

namespace UglyLauncher.AccountManager
{
    public partial class FrmRefreshToken : Form
    {
        private readonly MCUserAccount oUser;

        public FrmRefreshToken(MCUserAccount user)
        {
            InitializeComponent();
            oUser = user;
            TxtUser.Text = user.username;
            TxtUser.Enabled = false;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (TxtUser.Text.Length == 0 || TxtPass.Text.Length == 0)
            {
                MessageBox.Show(this, "Eines der Felder ist leer.", "Fehlerhafte Eingabe!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Check LoginData
                AuthHandler Auth = new AuthHandler();
                AuthenticateResponse AuthData;
                try
                {
                    AuthData = Auth.Authenticate(TxtUser.Text.ToString().Trim(), TxtPass.Text.ToString().Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString(), "Fehlermeldung von Minecraft.net", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtPass.Focus();
                    TxtPass.SelectAll();
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

        private void TxtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnConfirm_Click(sender, e);
        }
    }
}
