﻿using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

using UglyLauncher.Internet;
using UglyLauncher.Minecraft;

namespace UglyLauncher
{
    public partial class frm_main : Form
    {
        public delegate void startup();
        public frm_progressbar bar = new frm_progressbar();

        public frm_main()
        {
            InitializeComponent();
            ToolStripLabel lbl_version = new ToolStripLabel("Packversion:");
            lbl_version.Alignment = ToolStripItemAlignment.Right;
            mnu_container.Items.Add(lbl_version);
            cmb_packversions.Items.Clear();
            cmb_packversions.Items.Add("Kein Pack gewählt");
            cmb_packversions.SelectedIndex = 0;
        }

        // close the launcher
        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // show useraccounts
        private void accountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frm_UserAccounts().ShowDialog();
            lbl_default_account.Text = new UserManager().GetDefault();
        }

        // form_load event
        private void main_Load(object sender, EventArgs e)
        {
            Shown += new EventHandler(main_Shown);
        }

        //form_shown event
        void main_Shown(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            this.bar.Show();
            worker.RunWorkerAsync();
            while (worker.IsBusy)
                Application.DoEvents();

            this.bar.Hide();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            // Check Environment
            Launcher L = new Launcher();
            L.CheckDirectories();

            // Test User
            string MCPlayerName = null;
            worker.ReportProgress(25);

            UserManager U = new UserManager();
            if (U.GetDefault() != "none")
            {
                // Get Account
                MCUserAccount Account = U.GetAccount(U.GetDefault());

                // Validate Account
                Authentication A = new Authentication();
                try
                {
                    Account.accessToken = A.Refresh(Account.accessToken, Account.clientToken);
                    U.SaveAccount(Account);
                }
                catch (MCInvalidTokenException)
                {
                    frm_RefreshToken fTokRefresh = new frm_RefreshToken(Account.username);
                    DialogResult res = fTokRefresh.ShowDialog();
                    if (res == DialogResult.Cancel)
                    {
                        // Clear default user
                        U.SetDefault("none");
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show(this, ex.Message.ToString(), "Verbindungsfehler!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                    U.SetDefault("none");
                }
            }
            if (U.GetDefault() != "none") MCPlayerName = U.GetPlayerName(U.GetDefault());
            // set statusbar
            this.Invoke(new Action(() =>
            {
                lbl_default_account.Text = U.GetDefault();
            }));

            worker.ReportProgress(50);
            
            // Get Packs from Server
            L.LoadAvailablePacks(MCPlayerName);
            MCPacksAvailable Packs = L.GetAvailablePacks();
            foreach (MCPacksAvailablePack Pack in Packs.packs)
            {
                ListViewItem LvItem = new ListViewItem(Pack.name, Pack.name);
                LvItem.Font = new Font("Thaoma", 20, FontStyle.Bold);
                this.Invoke(new Action(() =>
                {
                    lst_packs_images.Images.Add(Pack.name, L.GetPackIcon(Pack));
                    lst_packs.Items.Add(LvItem);
                }));
            }

            worker.ReportProgress(75);

            // Load installed Packs
            L.LoadInstalledPacks();

            // end worker
            worker.ReportProgress(100);
            System.Threading.Thread.Sleep(500);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_packs.SelectedItems.Count == 1)
            {
                Launcher L = new Launcher();
                MCPacksAvailablePack APack = L.GetAvailablePack(lst_packs.SelectedItems[0].Text);
                // Clear dropdown
                cmb_packversions.Items.Clear();
                cmb_packversions.Items.Add("Recommended (" + APack.recommended_version + ")");
                // Load Versions in Dropdown
                foreach (string sPackVersion in APack.versions)
                    cmb_packversions.Items.Add(sPackVersion);

                // select version in combo depend on if pack is installed and version number
                if (L.IsPackInstalled(APack.name) == true)
                {
                    MCPacksInstalledPack IPack = L.GetInstalledPack(APack.name);
                    if(IPack.selected_version == "recommended") cmb_packversions.SelectedIndex = 0;
                    else cmb_packversions.SelectedIndex = cmb_packversions.FindStringExact(IPack.current_version);
                }
                else cmb_packversions.SelectedIndex = 0;
                web_packdetails.Navigate(L.sPackServer + @"/packs/" + APack.name + @"/" + APack.name + @".html");
            }
            else
            {
                cmb_packversions.Items.Clear();
                cmb_packversions.Items.Add("Kein Pack gewählt");
                cmb_packversions.SelectedIndex = 0;
                web_packdetails.Navigate("about:blank");
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.bar.update_bar(e.ProgressPercentage);
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbl_default_account.Text == "none")
            {
                MessageBox.Show(this, "Nicht eingeloggt.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (lst_packs.SelectedItems.Count != 1)
            {
                MessageBox.Show(this, "Kein Pack gewählt.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            // gather vars from Gui
            string sSelectedPack = lst_packs.SelectedItems[0].Text;
            string sSelectedVersion = null;
            if (cmb_packversions.SelectedIndex == 0) sSelectedVersion = "recommended";
            else sSelectedVersion = cmb_packversions.Text;
            Launcher L = new Launcher();
            // get event
            L.restoreWindow += new EventHandler(L_restoreWindow);
            // start minecraft
            L.StartPack(sSelectedPack, sSelectedVersion);
        }

        void L_restoreWindow(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() =>
                {
                    this.WindowState = FormWindowState.Normal;
                    this.Focus();
                }
            ));
        }
    }
}
