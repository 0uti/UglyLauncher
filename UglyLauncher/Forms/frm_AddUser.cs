using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

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
                Minecraft.Authentication Auth = new Minecraft.Authentication();
                Minecraft.MCAuthenticate_Response AuthData = new Minecraft.MCAuthenticate_Response();
                try
                {
                    AuthData = Auth.Authenticate(this.txt_user.Text.ToString().Trim(), this.txt_pass.Text.ToString().Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString(),"Fehlermeldung von Minecraft.net",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    this.txt_pass.Focus();
                    this.txt_pass.SelectAll();

                    return;
                }

                MCUserAccount newacc = new MCUserAccount();
                newacc.profiles = new List<MCUserAccountProfile>();

                newacc.accessToken = AuthData.accessToken;
                newacc.clientToken = AuthData.clientToken;
                newacc.username = txt_user.Text.ToString().Trim();
                newacc.activeProfile = AuthData.selectedProfile.id;

                for (int i = 0; i < AuthData.availableProfiles.Count; i++)
                {
                    MCUserAccountProfile newprofile = new MCUserAccountProfile();
                    newprofile.id = AuthData.availableProfiles[i].id;
                    newprofile.name = AuthData.availableProfiles[i].name;
                    newprofile.legacy = AuthData.availableProfiles[i].legacy;
                    newacc.profiles.Add(newprofile);
                }
                
                // Load users.xml
                UserManager U = new UserManager();
                MCUser storedAccounts = U.LoadUserListO();
                
                // save users.xml with new account
                storedAccounts.accounts.Add(newacc);
                U.SaveUserList(storedAccounts);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
