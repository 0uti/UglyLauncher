using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UglyLauncher.Minecraft;
using UglyLauncher.Minecraft.Json.MCAuthenticateResponse;

namespace UglyLauncher.AccountManager
{
    public partial class FrmAddUser : Form
    {
        public FrmAddUser()
        {
            InitializeComponent();
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
                catch (MCInvalidCredentialsException ex)
                {
                    MessageBox.Show(this, ex.Message.ToString(), "Fehlermeldung von Minecraft.net", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_pass.Focus();
                    txt_pass.SelectAll();

                    return;
                }
                catch (MCUserMigratedException ex)
                {
                    MessageBox.Show(this, ex.Message.ToString(), "Fehlermeldung von Minecraft.net", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_pass.Focus();
                    txt_pass.SelectAll();

                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString(), "Verbindungsfehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_pass.Focus();
                    txt_pass.SelectAll();

                    return;
                }

                MCUserAccount newAcc = new MCUserAccount();
                newAcc.guid = Guid.NewGuid();
                newAcc.profiles = new List<MCUserAccountProfile>();

                newAcc.accessToken = AuthData.AccessToken;
                newAcc.clientToken = AuthData.ClientToken;
                newAcc.username = txt_user.Text.ToString().Trim();
                newAcc.activeProfile = AuthData.SelectedProfile.Id;

                for (int i = 0; i < AuthData.AvailableProfiles.Length; i++)
                {
                    MCUserAccountProfile newprofile = new MCUserAccountProfile
                    {
                        id = AuthData.AvailableProfiles[i].Id,
                        name = AuthData.AvailableProfiles[i].Name,
                        legacy = AuthData.AvailableProfiles[i].Legacy
                    };
                    newAcc.profiles.Add(newprofile);
                }

                Manager U = new Manager();
                U.AddAccount(newAcc);

                // set default user if only one given
                MCUser user = U.GetAccounts();
                if (U.GetNumAccounts() == 1) U.SetDefault(newAcc.guid);

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void txt_user_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txt_pass.Focus();
        }

        private void txt_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button2_Click(sender, e);
        }
    }
}
