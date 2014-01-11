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
    public partial class frm_UserAccounts : Form
    {
        public frm_UserAccounts()
        {
            InitializeComponent();
            LoadUsersIntoList();
        }

   

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close(); ;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            DialogResult res = new frm_AddUser().ShowDialog();

            if (res != DialogResult.Cancel)
            {
                this.lst_accounts.Items.Clear();
                LoadUsersIntoList();
            }
        }

        private void btn_default_Click(object sender, EventArgs e)
        {
            if (this.lst_accounts.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Kein Account ausgewählt", "Account als Standard setzen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                UserManager U = new UserManager();
                U.SetDefault(this.lst_accounts.SelectedItems[0].Text);
                LoadUsersIntoList();
            }
        }

        private void LoadUsersIntoList()
        {
            UserManager U = new UserManager();
            users UserObj = U.LoadUserList();

            this.lst_accounts.Items.Clear();

            for (int i = 0; i < UserObj.accounts.Count; i++)
            {
                ListViewItem account = new ListViewItem();
                account.Text = UserObj.accounts[i].username;
                if (UserObj.accounts[i].username == UserObj.activeAccount)
                {
                    account.Font = new Font(this.lst_accounts.Font, FontStyle.Bold);
                }
                this.lst_accounts.Items.Add(account);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (this.lst_accounts.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Kein Account ausgewählt", "Account Löschen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                UserManager U = new UserManager();
                users UserObj = U.LoadUserList();
                if (this.lst_accounts.SelectedItems[0].Text == UserObj.activeAccount) UserObj.activeAccount = "none";
                for (int i = 0; i < UserObj.accounts.Count; i++)
                {
                    if (UserObj.accounts[i].username == this.lst_accounts.SelectedItems[0].Text)
                    {
                        UserObj.accounts.RemoveAt(i);
                    }
                }
                U.SaveUserList(UserObj);
                LoadUsersIntoList();
            }
        }


    }
}
