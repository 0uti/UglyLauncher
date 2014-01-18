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
        // contructor
        public frm_UserAccounts()
        {
            InitializeComponent();
            // load user in listview
            RefreshUsers();
        }

        // Close this form
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close(); ;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            // Call the add user dialog
            DialogResult res = new frm_AddUser().ShowDialog();
            // fetching result
            if (res != DialogResult.Cancel)
            {
                // refresh user listview
                this.RefreshUsers();
            }
        }

        // set default user
        private void btn_default_Click(object sender, EventArgs e)
        {
            // Check if a user is selected
            if (this.lst_accounts.SelectedItems.Count == 0)
            {
                // MessageBox with Error
                MessageBox.Show(this, "Kein Account ausgewählt", "Account als Standard setzen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // create object
            UserManager U = new UserManager();
            U.SetDefault(this.lst_accounts.SelectedItems[0].Text);
            // refresh user listview
            RefreshUsers();
        }

        // refresh user listview
        private void RefreshUsers()
        {
            // Init Object
            UserManager U = new UserManager();
            // Clear listview
            this.lst_accounts.Items.Clear();
            // Get all users
            MCUser users = U.GetAccounts();
            // put all users into listview
            foreach (MCUserAccount Account in users.accounts)
            {
                // Create Object
                ListViewItem AccItem = new ListViewItem();
                // Set Account name (login)
                AccItem.Text = Account.username;
                // Set Font to bold if default user
                if (Account.username == users.activeAccount) AccItem.Font = new Font(this.lst_accounts.Font,FontStyle.Bold);
                // Add Item to ListView
                this.lst_accounts.Items.Add(AccItem);
            }
        }

        // delete user
        private void btn_delete_Click(object sender, EventArgs e)
        {
            // chech if user selected
            if (this.lst_accounts.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Kein Account ausgewählt", "Account Löschen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // create object
            UserManager U = new UserManager();
            // delete user
            U.DeleteAccount(this.lst_accounts.SelectedItems[0].Text);
            // refresh user listview
            this.RefreshUsers();
        }
    }
}
