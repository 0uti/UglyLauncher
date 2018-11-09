using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UglyLauncher.Minecraft;
using UglyLauncher.Minecraft.Authentication;
using UglyLauncher.Minecraft.Authentication.Json.AuthenticateResponse;

namespace UglyLauncher.AccountManager
{
    public partial class FrmAddUser : Form
    {
        public FrmAddUser()
        {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (TxtUser.Text == "" || TxtPass.Text == "")
            {
                MessageBox.Show(this, "Eines der Felder ist leer.", "Fehlerhafte Eingabe!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Check LoginData
                Authentication Auth = new Authentication();
                AuthenticateResponse AuthData = new AuthenticateResponse();
                try
                {
                    AuthData = Auth.Authenticate(TxtUser.Text.ToString().Trim(), TxtPass.Text.ToString().Trim());
                }
                catch (MCInvalidCredentialsException ex)
                {
                    MessageBox.Show(this, ex.Message.ToString(), "Fehlermeldung von Minecraft.net", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtPass.Focus();
                    TxtPass.SelectAll();

                    return;
                }
                catch (MCUserMigratedException ex)
                {
                    MessageBox.Show(this, ex.Message.ToString(), "Fehlermeldung von Minecraft.net", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtPass.Focus();
                    TxtPass.SelectAll();

                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString(), "Verbindungsfehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtPass.Focus();
                    TxtPass.SelectAll();

                    return;
                }

                MCUserAccount newAcc = new MCUserAccount
                {
                    guid = Guid.NewGuid(),
                    profiles = new List<MCUserAccountProfile>(),

                    accessToken = AuthData.AccessToken,
                    clientToken = AuthData.ClientToken,
                    username = TxtUser.Text.ToString().Trim(),
                    activeProfile = AuthData.SelectedProfile.Id
                };

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

        private void TxtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                TxtPass.Focus();
        }

        private void TxtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnSave_Click(sender, e);
        }
    }
}
