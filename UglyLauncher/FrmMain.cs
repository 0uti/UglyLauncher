using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using UglyLauncher.Minecraft;
using UglyLauncher.AccountManager;
using UglyLauncher.Settings;
using System.Collections.Generic;
using UglyLauncher.Minecraft.Json.AvailablePacks;

namespace UglyLauncher
{
    public partial class FrmMain : Form
    {
        public delegate void startup();
        public FrmProgressbar bar = new FrmProgressbar();
        public bool Offline = false;


        public FrmMain()
        {
            InitializeComponent();
        }

        // close the launcher
        private void mnu_exit_program_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        // show useraccounts
        private void accountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmUserAccounts().ShowDialog();
            DoInit();
        }

        // form_load event
        private void FrmMain_Load(object sender, EventArgs e)
        {
            Text += " " + Application.ProductVersion;
            cmb_packversions.Items.Clear();
            cmb_packversions.Items.Add("Kein Pack gewählt");
            cmb_packversions.SelectedIndex = 0;

            //Versions test = new Versions();
            //List<string> test2 = test.GetVersions(true,true,true);

            //test.GetVersion("1.13.1");

            btn_start.Enabled = false;
            mnu_edit_Pack.Enabled = false;
            cmb_packversions.Enabled = false;
        }

        //form_shown event
        void FrmMain_Shown(object sender, EventArgs e)
        {
            DoInit();
        }

        private void DoInit(bool refreshUser = true)
        {
            BackgroundWorker Worker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            Worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
            Worker.ProgressChanged += new ProgressChangedEventHandler(Worker_ProgressChanged);
            bar.Show();
            Worker.RunWorkerAsync(refreshUser);
            while (Worker.IsBusy)
                Application.DoEvents();
            bar.Hide();

            // Check if Users available
            Manager U = new Manager();
            if (U.GetNumAccounts() == 0)
            {
                DialogResult MBres = MessageBox.Show("Es ist noch kein Account vorhanden.\nWollen Sie jetzt einen anlegen?", "Kein Account angelegt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (MBres == DialogResult.Yes)
                {
                    new FrmUserAccounts().ShowDialog();
                    DoInit();
                }
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            bool refreshUser = (bool)e.Argument;   // the 'argument' parameter resurfaces here


            BackgroundWorker worker = sender as BackgroundWorker;
            
            // Check Environment
            Launcher L = new Launcher(Offline);
            Manager U = new Manager();
            L.CheckDirectories();
            
            // Test User
            string MCPlayerName = null;
            string UserAccount = "none";
            string MCUID = null;
            worker.ReportProgress(25);


            if (refreshUser == true)
            {
                if (U.GetDefault() != Guid.Empty)
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
                        FrmRefreshToken fTokRefresh = new FrmRefreshToken(Account);
                        DialogResult res = fTokRefresh.ShowDialog();
                        if (res == DialogResult.Cancel) U.SetDefault(Guid.Empty);
                    }
                    catch (Exception)
                    {
                        DialogResult res = DialogResult.No;
                        Invoke(new Action(() =>
                        {
                            res = MessageBox.Show(this, "In Offlinemodus wechseln?", "Verbindungsfehler zu Mojang!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        }));

                        if (res == DialogResult.Yes)
                        {
                            Offline = true;
                            Invoke(new Action(() =>
                            {
                                mnu_accounts.Enabled = false;
                            }));
                        }
                        else U.SetDefault(Guid.Empty);
                    }
                }
            }

            if (U.GetDefault() != Guid.Empty)
            {
                MCPlayerName = U.GetPlayerName(U.GetDefault());
                MCUID = U.GetMCProfileID(U.GetDefault());
                UserAccount = U.GetAccount(U.GetDefault()).username;
            }
            // set statusbar
            Invoke(new Action(() =>
            {
                lbl_default_account.Text = UserAccount;
                lst_packs.Clear();
                lst_packs_images.Images.Clear();
            }));

            worker.ReportProgress(50);
            
            // Get Packs from Server
            try
            {
                if (Offline == false)
                {
                    L.LoadAvailablePacks(MCPlayerName, MCUID);
                    MCAvailablePacks packs = L.GetAvailablePacks();
                    if (packs.Packs != null)
                    {
                        foreach (MCAvailablePack Pack in packs.Packs)
                        {
                            ListViewItem LvItem = new ListViewItem(Pack.Name, Pack.Name)
                            {
                                Font = new Font("Thaoma", 16, FontStyle.Bold)
                            };
                            Invoke(new Action(() =>
                            {
                                lst_packs_images.Images.Add(Pack.Name, L.GetPackIcon(Pack));
                                lst_packs.Items.Add(LvItem);
                            }));
                        }
                    }
                }
                else
                {
                    L.LoadInstalledPacks();
                    MCPacksInstalled Packs = L.GetInstalledPacks();
                    if (Packs.packs != null)
                    {
                        foreach (MCPacksInstalledPack Pack in Packs.packs)
                        {
                            ListViewItem LvItem = new ListViewItem(Pack.Name, Pack.Name)
                            {
                                Font = new Font("Thaoma", 16, FontStyle.Bold)
                            };
                            Invoke(new Action(() =>
                            {
                                if(L.GetPackIconOffline(Pack) != null)
                                {
                                    lst_packs_images.Images.Add(Pack.Name, L.GetPackIconOffline(Pack));
                                }
                                lst_packs.Items.Add(LvItem);
                            }));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show(this, ex.Message.ToString(), "Verbindungsfehler zu Minestar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                Launcher L = new Launcher(Offline);
                // Clear dropdown
                cmb_packversions.Items.Clear();
                if (Offline == false)
                {
                    MCAvailablePack APack = L.GetAvailablePack(lst_packs.SelectedItems[0].Text);
                    
                    
                    cmb_packversions.Items.Add("Recommended (" + APack.RecommendedVersion + ")");
                    // Load Versions in Dropdown
                    foreach (string sPackVersion in APack.Versions)
                        cmb_packversions.Items.Add(sPackVersion);

                    // select version in combo depend on if pack is installed and version number
                    if (L.IsPackInstalled(APack.Name) == true)
                    {
                        MCPacksInstalledPack IPack = L.GetInstalledPack(APack.Name);
                        if (IPack.SelectedVersion == "recommended") cmb_packversions.SelectedIndex = 0;
                        else cmb_packversions.SelectedIndex = cmb_packversions.FindStringExact(IPack.CurrentVersion);
                    }
                    else cmb_packversions.SelectedIndex = 0;
                    web_packdetails.Navigate(L._sPackServer + @"/packs/" + APack.Name + @"/" + APack.Name + @".html");
                    downloadToolStripMenuItem.Enabled = true;
                }
                else
                {
                    MCPacksInstalledPack IPack = L.GetInstalledPack(lst_packs.SelectedItems[0].Text);
                    cmb_packversions.Items.Add(IPack.CurrentVersion);
                    cmb_packversions.SelectedIndex = 0;
                    downloadToolStripMenuItem.Enabled = false;
                }
                btn_start.Enabled = true;
                mnu_edit_Pack.Enabled = true;
                cmb_packversions.Enabled = true;
            }
            else
            {
                cmb_packversions.Items.Clear();
                cmb_packversions.Items.Add("Kein Pack gewählt");
                cmb_packversions.SelectedIndex = 0;
                web_packdetails.Navigate("about:blank");
                btn_start.Enabled = false;
                mnu_edit_Pack.Enabled = false;
                cmb_packversions.Enabled = false;
                downloadToolStripMenuItem.Enabled = false;
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            bar.UpdateBar(e.ProgressPercentage);
        }

        private void Startpack()
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
            Launcher L = new Launcher(Offline);
            // get event
            L.RestoreWindow += new EventHandler<Launcher.FormWindowStateEventArgs>(L_restoreWindow);
            // disable Startbutton
            btn_start.Enabled = false;
            lst_packs.Enabled = false;
            // start minecraft
            L.StartPack(sSelectedPack, sSelectedVersion);
        }


        private void Downloadpack()
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
            Launcher L = new Launcher(Offline);
            // download minecraft
            L.StartPack(sSelectedPack, sSelectedVersion);
        }



        void L_restoreWindow(object sender, Launcher.FormWindowStateEventArgs e)
        {

            Configuration C = new Configuration();

            if (C.CloseLauncher == 1 && e.MCExitCode == 0)
            {
                Application.Exit();
                return;
            }

            BeginInvoke(new Action(() =>
                {
                    WindowState = e.WindowState;
                    if (e.WindowState == FormWindowState.Minimized) ShowInTaskbar = false;
                    else ShowInTaskbar = true;
                    btn_start.Enabled = true;
                    lst_packs.Enabled = true;
                }
            ));
        }

        private void einstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Settings.Settings().ShowDialog();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmAbout().ShowDialog();
        }

        private void packBearbeitenToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            Launcher L = new Launcher(Offline);
            if (L.IsPackInstalled(sSelectedPack, sSelectedVersion) == false)
            {
                MessageBox.Show(this, "Dieses Pack ist nicht installiert oder liegt in einer anderen Version vor.\r\nBitte dieses Pack starten und danach bearbeiten.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            new FrmEditPack(sSelectedPack).ShowDialog();
        }

        private void lst_packs_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
            if (e.Clicks == 2) Startpack();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            Startpack();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Startpack();
        }

        private void öffneVerzeichnissToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sSelectedPack = lst_packs.SelectedItems[0].Text;
            Launcher L = new Launcher(Offline);
            L.OpenPackFolder(sSelectedPack);

        }

        private void mnu_refreshPacketList_Click(object sender, EventArgs e)
        {
            DoInit(false);
        }
    }
}
