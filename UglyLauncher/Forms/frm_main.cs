using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UglyLauncher
{
    public partial class main : Form
    {
        public delegate void startup();
        public main()
        {
            InitializeComponent();
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void accountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frm_UserAccounts().ShowDialog();
            
            UserManager U = new UserManager();
            txt_default_account.Text = U.GetDefault();
        }

        private void main_Load(object sender, EventArgs e)
        {
            BeginInvoke(new startup(startup_check));
        }

        private void startup_check()
        {
            UserManager U = new UserManager();
            txt_default_account.Text = U.GetDefault();
            if (U.GetDefault() != "none")
            {
                // get Profile XML ID
                int XmlID = U.GetProfileXmlId(U.GetDefault());
                if (XmlID != -1)
                {
                    users UserObj = new users();
                    UserObj = U.LoadUserList();
                    users.account myAccount = UserObj.accounts[XmlID];
                    string AccessToken = myAccount.accessToken;
                    string ClientToken = myAccount.clientToken;

                    // do MC refresh
                    Minecraft.Authentication A = new Minecraft.Authentication();
                    try
                    {
                        myAccount.accessToken = A.Refresh(myAccount.accessToken, myAccount.clientToken);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "Invalid token.")
                        {
                            frm_RefreshToken fTokRefresh = new frm_RefreshToken(myAccount.username);
                            DialogResult res = fTokRefresh.ShowDialog();
                            if (res == DialogResult.Cancel)
                            {
                                // Clear default user
                                U.SetDefault("none");
                                txt_default_account.Text = U.GetDefault();
                            }
                        }
                        else MessageBox.Show(this, ex.Message.ToString(), "Fehlermeldung von Minecraft.net", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // save new Account
                    UserObj.accounts[XmlID] = myAccount;
                    U.SaveUserList(UserObj);
                }
            }
        }
    }
}
