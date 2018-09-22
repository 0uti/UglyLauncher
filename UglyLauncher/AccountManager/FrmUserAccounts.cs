using System;
using System.Drawing;
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
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close(); ;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
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
        private void BtnSetDefault_Click(object sender, EventArgs e)
        {
            // Check if a user is selected
            if (LstAccounts.SelectedItems.Count == 0)
            {
                // MessageBox with Error
                MessageBox.Show(this, "Kein Account ausgewählt", "Account als Standard setzen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // create object
            Manager U = new Manager();
            U.SetDefault(Guid.Parse(LstAccounts.SelectedItems[0].Name));
            // refresh user listview
            RefreshUsers();
        }

        // refresh user listview
        private void RefreshUsers()
        {
            // Init Object
            Manager U = new Manager();
            // Clear listview
            LstAccounts.Items.Clear();
            // Get all users
            MCUser users = U.GetAccounts();
            // put all users into listview
            foreach (MCUserAccount Account in users.accounts)
            {
                // Create Object
                ListViewItem AccItem = new ListViewItem
                {
                    // Set Account name (login)
                    Text = Account.username,
                    Name = Account.guid.ToString()
                };
                // Set Font to bold if default user
                if (Account.guid == users.activeAccount) AccItem.Font = new Font(LstAccounts.Font,FontStyle.Bold);
                // Add Item to ListView
                LstAccounts.Items.Add(AccItem);
            }
        }

        // delete user
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            // chech if user selected
            if (LstAccounts.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Kein Account ausgewählt", "Account Löschen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // create object
            Manager U = new Manager();
            // delete user
            U.DeleteAccount(Guid.Parse(LstAccounts.SelectedItems[0].Name));
            // refresh user listview
            RefreshUsers();
        }
    }
}
