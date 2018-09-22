using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UglyLauncher.AccountManager
{
    public partial class FrmUserAccounts : Form
    {
        // contructor
        public FrmUserAccounts()
        {
            InitializeComponent();
            // load user in listview
            RefreshUsers();
        }

        // Close this form
        private void btn_close_Click(object sender, EventArgs e)
        {
            Close(); ;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            // Call the add user dialog
            DialogResult res = new FrmAddUser().ShowDialog();
            // fetching result
            if (res != DialogResult.Cancel)
            {
                // refresh user listview
                RefreshUsers();
            }
        }

        // set default user
        private void btn_default_Click(object sender, EventArgs e)
        {
            // Check if a user is selected
            if (lst_accounts.SelectedItems.Count == 0)
            {
                // MessageBox with Error
                MessageBox.Show(this, "Kein Account ausgewählt", "Account als Standard setzen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // create object
            Manager U = new Manager();
            U.SetDefault(Guid.Parse(lst_accounts.SelectedItems[0].Name));
            // refresh user listview
            RefreshUsers();
        }

        // refresh user listview
        private void RefreshUsers()
        {
            // Init Object
            Manager U = new Manager();
            // Clear listview
            lst_accounts.Items.Clear();
            // Get all users
            MCUser users = U.GetAccounts();
            // put all users into listview
            foreach (MCUserAccount Account in users.accounts)
            {
                // Create Object
                ListViewItem AccItem = new ListViewItem();
                // Set Account name (login)
                AccItem.Text = Account.username;
                AccItem.Name = Account.guid.ToString();
                // Set Font to bold if default user
                if (Account.guid == users.activeAccount) AccItem.Font = new Font(lst_accounts.Font,FontStyle.Bold);
                // Add Item to ListView
                lst_accounts.Items.Add(AccItem);
            }
        }

        // delete user
        private void btn_delete_Click(object sender, EventArgs e)
        {
            // chech if user selected
            if (lst_accounts.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Kein Account ausgewählt", "Account Löschen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // create object
            Manager U = new Manager();
            // delete user
            U.DeleteAccount(Guid.Parse(lst_accounts.SelectedItems[0].Name));
            // refresh user listview
            RefreshUsers();
        }
    }
}
