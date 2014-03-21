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

namespace UglyLauncher
{
    public partial class frm_AddUser : Form
    {
        public frm_AddUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.txt_user.Text == "" || this.txt_pass.Text == "")
            {
                MessageBox.Show(this, "Eines der Felder ist leer.", "Fehlerhafte Eingabe!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Check LoginData
                Authentication Auth = new Authentication();
                MCAuthenticate_Response AuthData = new MCAuthenticate_Response();
                try
                {
                    AuthData = Auth.Authenticate(this.txt_user.Text.ToString().Trim(), this.txt_pass.Text.ToString().Trim());
                }
                catch (MCInvalidCredentialsException ex)
                {
                    MessageBox.Show(this, ex.Message.ToString(), "Fehlermeldung von Minecraft.net", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.txt_pass.Focus();
                    this.txt_pass.SelectAll();

                    return;
                }
                catch (MCUserMigratedException ex)
                {
                    MessageBox.Show(this, ex.Message.ToString(), "Fehlermeldung von Minecraft.net", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.txt_pass.Focus();
                    this.txt_pass.SelectAll();

                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString(), "Verbindungsfehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.txt_pass.Focus();
                    this.txt_pass.SelectAll();

                    return;
                }

                MCUserAccount newAcc = new MCUserAccount();
                newAcc.profiles = new List<MCUserAccountProfile>();

                newAcc.accessToken = AuthData.accessToken;
                newAcc.clientToken = AuthData.clientToken;
                newAcc.username = txt_user.Text.ToString().Trim();
                newAcc.activeProfile = AuthData.selectedProfile.id;

                for (int i = 0; i < AuthData.availableProfiles.Count; i++)
                {
                    MCUserAccountProfile newprofile = new MCUserAccountProfile();
                    newprofile.id = AuthData.availableProfiles[i].id;
                    newprofile.name = AuthData.availableProfiles[i].name;
                    newprofile.legacy = AuthData.availableProfiles[i].legacy;
                    newAcc.profiles.Add(newprofile);
                }

                UserManager U = new UserManager();
                U.AddAccount(newAcc);

                // set default user if only one given
                MCUser user = U.GetAccounts();
                if (U.GetNumAccounts() == 1) U.SetDefault(newAcc.username);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
