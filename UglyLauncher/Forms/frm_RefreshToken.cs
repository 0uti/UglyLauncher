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
    public partial class frm_RefreshToken : Form
    {
        public frm_RefreshToken(string sUsername)
        {
            InitializeComponent();
            txt_user.Text = sUsername;
            txt_user.Enabled = false;
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

                // Load users.xml
                UserManager U = new UserManager();
                MCUser storedAccounts = new MCUser();
                storedAccounts = U.LoadUserListO();

                int XmlId = U.GetProfileXmlId_O(txt_user.Text.ToString().Trim());
                // save users.xml with new account
                MCUserAccount myAccount = storedAccounts.accounts[XmlId];
                myAccount.accessToken = AuthData.accessToken;
                myAccount.clientToken = AuthData.clientToken;
                storedAccounts.accounts[XmlId] = myAccount;
                U.SaveUserList(storedAccounts);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        
    }
}
