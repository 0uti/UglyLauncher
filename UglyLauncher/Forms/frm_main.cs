using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace UglyLauncher
{
    public partial class main : Form
    {
        public delegate void startup();
        public frm_progressbar bar = new frm_progressbar();

        public main()
        {
            InitializeComponent();
            ToolStripLabel lbl_version = new ToolStripLabel("Packversion:");
            lbl_version.Alignment = ToolStripItemAlignment.Right;
            menuStrip1.Items.Add(lbl_version);
            cmb_packversions.Items.Clear();
            cmb_packversions.Items.Add("Kein Pack gewählt");
            cmb_packversions.SelectedIndex = 0;
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
            Shown += new EventHandler(main_Shown);
            
            startup_worker.WorkerReportsProgress = true;
            startup_worker.WorkerSupportsCancellation = true;
        }

        void main_Shown(object sender, EventArgs e)
        {
            // display Progressbar Form
            bar.Show();
            startup_worker.RunWorkerAsync();
        }

        private void startup_check(object sender, DoWorkEventArgs e)
        {
            string MCPlayerName = null;

            // Test User
            UserManager U = new UserManager();
            this.Invoke(new Action(() =>
            {
                txt_default_account.Text = U.GetDefault();

            }));
            startup_worker.ReportProgress(25);
            System.Threading.Thread.Sleep(100);

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
                                this.Invoke(new Action(() =>
                                {
                                    txt_default_account.Text = U.GetDefault();

                                }));
                            }
                        }
                        else MessageBox.Show(this, ex.Message.ToString(), "Fehlermeldung von Minecraft.net", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // save new Account
                    MCPlayerName = myAccount.profiles[0].name;  // More work to do here
                    UserObj.accounts[XmlID] = myAccount;
                    U.SaveUserList(UserObj);
                }
            }

            startup_worker.ReportProgress(50);
            System.Threading.Thread.Sleep(100);

            // Get Packs
            Minecraft.Launcher L = new Minecraft.Launcher();
            Minecraft.staticVars.Packs = L.GetClientPackList(MCPlayerName);

            for (int i = 0; i < Minecraft.staticVars.Packs.packs.Count; i++)
            {
                Minecraft.MCPacks.pack Pack = new Minecraft.MCPacks.pack();
                ListViewItem LvItem = new ListViewItem();
                Pack = Minecraft.staticVars.Packs.packs[i];

                // Get Pack Icon
                MemoryStream ms = new MemoryStream();
                ms = Internet.Http.Download(L.sPackServer + @"/packs/" + Pack.name + @"/" + Pack.name + @".png");

                LvItem.Text = Pack.name;
                LvItem.Font = new Font("Thaoma", 20, FontStyle.Bold);
                LvItem.ImageKey = Pack.name;
                this.Invoke(new Action(() =>
                {
                    lvImages.Images.Add(Pack.name, System.Drawing.Image.FromStream(ms));
                    listView1.Items.Add(LvItem);
                }));
            }
            startup_worker.ReportProgress(75);
            System.Threading.Thread.Sleep(100);

            Minecraft.staticVars.PacksInstalled = L.GetInstalledPacks();



            startup_worker.ReportProgress(100);
            System.Threading.Thread.Sleep(1000);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                // Load Versions in Dropdown
                Minecraft.MCPacks.pack Pack = new Minecraft.MCPacks.pack();
                Pack = Minecraft.staticVars.Packs.packs[listView1.SelectedItems[0].Index];

                cmb_packversions.Items.Clear();
                cmb_packversions.Items.Add("Recommended (" + Pack.recommended_version + ")");
                for (int j = 0; j < Pack.versions.Count; j++)
                {
                    cmb_packversions.Items.Add(Pack.versions[j].ToString());
                }

                bool installed = false;
                // is pack installed ?
                for (int i = 0; i < Minecraft.staticVars.PacksInstalled.packs.Count; i++)
                {
                    if (Minecraft.staticVars.PacksInstalled.packs[i].name == Pack.name)
                    {
                        installed = true;
                        // Seleced Pack is installed.
                        if (Minecraft.staticVars.PacksInstalled.packs[i].selected_version == "recommended")
                        {
                            cmb_packversions.SelectedIndex = 0; // recommended Version is Selected
                        }
                        else
                        {
                            cmb_packversions.SelectedIndex = cmb_packversions.FindStringExact(Minecraft.staticVars.PacksInstalled.packs[i].selected_version);
                        }
                    }
                }
                if(installed == false) cmb_packversions.SelectedIndex = 0;

                Minecraft.Launcher U = new Minecraft.Launcher();
                web_packdetails.Navigate(U.sPackServer + @"/packs/" + Pack.name + @"/" + Pack.name + @".html");
            }
            else
            {
                cmb_packversions.Items.Clear();
                cmb_packversions.Items.Add("Kein Pack gewählt");
                cmb_packversions.SelectedIndex = 0;
                web_packdetails.Navigate("about:blank");
            }
        }

        private void startup_worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bar.Dispose();
        }

        private void startup_worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            bar.update_bar(e.ProgressPercentage);
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txt_default_account.Text == "none")
            {
                MessageBox.Show(this, "Nicht eingeloggt.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listView1.SelectedItems.Count != 1)
            {
                MessageBox.Show(this, "Kein Pack gewählt.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Minecraft.Launcher MCLauncher = new Minecraft.Launcher();

            // gather vars from Gui
            string sSelectedPack = listView1.SelectedItems[0].Text;
            string sSelectedVersion = null;
            if (cmb_packversions.SelectedIndex == 0) sSelectedVersion = "recommended";
            else sSelectedVersion = cmb_packversions.Text;

            //Check if the pack is with the given version install
            if (MCLauncher.IsPackInstalled(sSelectedPack, sSelectedVersion) == false)
            {
                // install the Pack
                MessageBox.Show("ToDo: install Pack");

            }

            // prepare the Pack
            MessageBox.Show("ToDo: starting Pack");




            // Start the Pack
            MessageBox.Show("ToDo: starting Pack");
            this.WindowState = FormWindowState.Minimized;
            

        }
    }
}
