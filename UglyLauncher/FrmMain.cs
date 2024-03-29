﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UglyLauncher.AccountManager;
using UglyLauncher.Minecraft;
using UglyLauncher.Minecraft.Authentication;
using UglyLauncher.Minecraft.Json.AvailablePacks;
using UglyLauncher.Settings;

namespace UglyLauncher
{
    public partial class FrmMain : Form
    {
        public delegate void startup();
        public static FrmProgressbar bar = new FrmProgressbar();
        public bool Offline = false;

        private readonly StartupSide Side;

        public FrmMain(StartupSide startupSide)
        {
            Side = startupSide;
            InitializeComponent();
        }

        // close the launcher
        private void MnuExitProgram_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // show useraccounts
        private void MnuAccounts_Click(object sender, EventArgs e)
        {
            FrmUserAccounts frmUserAccounts = new FrmUserAccounts();
            frmUserAccounts.ShowDialog();
            DoInit();
            frmUserAccounts.Dispose();
        }

        // form_load event
        private void FrmMain_Load(object sender, EventArgs e)
        {
            Text += " " + Application.ProductVersion;
            CmbPackVersions.Items.Clear();
            CmbPackVersions.Items.Add("Kein Pack gewählt");
            CmbPackVersions.SelectedIndex = 0;

            //Versions test = new Versions();
            //List<string> test2 = test.GetVersions(true,true,true);

            //test.GetVersion("1.13.1");

            BtnStart.Enabled = false;
            MnuEditPack.Enabled = false;
            CmbPackVersions.Enabled = false;
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
            {
                Worker.RunWorkerAsync(refreshUser);
            }
            while (Worker.IsBusy)
            {
                Application.DoEvents();
            }

            bar.Hide();
            Worker.Dispose();

            // Check if Users available
            Manager U = new Manager();
            if (U.GetNumAccounts() == 0)
            {
                DialogResult MBres = MessageBox.Show("Es ist noch kein Account vorhanden.\nWollen Sie jetzt einen anlegen?", "Kein Account angelegt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (MBres == DialogResult.Yes)
                {
                    FrmUserAccounts frmUserAccounts = new FrmUserAccounts();
                    frmUserAccounts.ShowDialog();
                    DoInit();
                    frmUserAccounts.Dispose();
                }
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            bool refreshUser = (bool)e.Argument;   // the 'argument' parameter resurfaces here


            BackgroundWorker worker = sender as BackgroundWorker;

            // Check Environment
            Launcher L = new Launcher(Side, Offline);
            Manager U = new Manager();
            {
                L.CheckDirectories();
            }

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
                    AuthHandler A = new AuthHandler();
                    try
                    {
                        Account.accessToken = A.Refresh(Account.accessToken, Account.clientToken);
                        U.SaveAccount(Account);
                    }
                    catch (MCInvalidTokenException)
                    {
                        FrmRefreshToken fTokRefresh = new FrmRefreshToken(Account);
                        DialogResult res = fTokRefresh.ShowDialog();
                        if (res == DialogResult.Cancel)
                        {
                            U.SetDefault(Guid.Empty);
                        }

                        fTokRefresh.Dispose();
                    }
                    catch (Exception ex)
                    {
                        DialogResult res = DialogResult.No;
                        Invoke(new Action(() =>
                        {
#if DEBUG
                        res = MessageBox.Show(this, "In Offlinemodus wechseln?", "Verbindungsfehler zu Mojang!" + Environment.NewLine + ex.Message, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
#else
                        res = MessageBox.Show(this, "In Offlinemodus wechseln?", "Verbindungsfehler zu Mojang!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
#endif

                        }));

                        if (res == DialogResult.Yes)
                        {
                            Offline = true;
                            Invoke(new Action(() =>
                            {
                                MnuAccounts.Enabled = false;
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
                LblDefaultAccount.Text = UserAccount;
                LstPacks.Clear();
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
                                LstPacks.Items.Add(LvItem);
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
                                if (L.GetPackIconOffline(Pack) != null)
                                {
                                    lst_packs_images.Images.Add(Pack.Name, L.GetPackIconOffline(Pack));
                                }
                                LstPacks.Items.Add(LvItem);
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

        private void LstPacks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstPacks.SelectedItems.Count == 1)
            {
                Launcher L = new Launcher(Side, Offline);
                // Clear dropdown
                CmbPackVersions.Items.Clear();
                if (Offline == false)
                {
                    MCAvailablePack APack = L.GetAvailablePack(LstPacks.SelectedItems[0].Text);

                    CmbPackVersions.Items.Add("Recommended (" + APack.RecommendedVersion + ")");
                    // Load Versions in Dropdown
                    foreach (MCAvailablePackVersion version in APack.Versions)
                    {
                        CmbPackVersions.Items.Add(version.Version);
                    }

                    // select version in combo depend on if pack is installed and version number
                    if (L.IsPackInstalled(APack.Name) == true)
                    {
                        MCPacksInstalledPack IPack = L.GetInstalledPack(APack.Name);
                        CmbPackVersions.SelectedIndex = IPack.SelectedVersion == "recommended" ? 0 : CmbPackVersions.FindStringExact(IPack.CurrentVersion);
                    }
                    else CmbPackVersions.SelectedIndex = 0;
                    WebPackDetails.Navigate(Launcher._PackServer + @"/packs/" + APack.Name + @"/" + APack.Name + @".html");
                    MnuDownloadPack.Enabled = true;
                }
                else
                {
                    MCPacksInstalledPack IPack = L.GetInstalledPack(LstPacks.SelectedItems[0].Text);
                    CmbPackVersions.Items.Add(IPack.CurrentVersion);
                    CmbPackVersions.SelectedIndex = 0;
                    MnuDownloadPack.Enabled = false;
                }
                BtnStart.Enabled = true;
                MnuEditPack.Enabled = true;
                CmbPackVersions.Enabled = true;
            }
            else
            {
                CmbPackVersions.Items.Clear();
                CmbPackVersions.Items.Add("Kein Pack gewählt");
                CmbPackVersions.SelectedIndex = 0;
                WebPackDetails.Navigate("about:blank");
                BtnStart.Enabled = false;
                MnuEditPack.Enabled = false;
                CmbPackVersions.Enabled = false;
                MnuDownloadPack.Enabled = false;
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            bar.UpdateBar(e.ProgressPercentage);
        }

        private void Startpack()
        {
            if (LblDefaultAccount.Text == "none")
            {
                MessageBox.Show(this, "Nicht eingeloggt.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (LstPacks.SelectedItems.Count != 1)
            {
                MessageBox.Show(this, "Kein Pack gewählt.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // gather vars from Gui
            string sSelectedPack = LstPacks.SelectedItems[0].Text;
            string sSelectedVersion = CmbPackVersions.SelectedIndex == 0 ? "recommended" : CmbPackVersions.Text;
            Launcher L = new Launcher(Side, Offline);
            // get event
            L.RestoreWindow += new EventHandler<Launcher.FormWindowStateEventArgs>(L_restoreWindow);
            // disable Startbutton
            BtnStart.Enabled = false;
            LstPacks.Enabled = false;
            // start minecraft
            L.StartPack(sSelectedPack, sSelectedVersion);
        }

        private void Downloadpack()
        {
            if (LblDefaultAccount.Text == "none")
            {
                MessageBox.Show(this, "Nicht eingeloggt.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (LstPacks.SelectedItems.Count != 1)
            {
                MessageBox.Show(this, "Kein Pack gewählt.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // gather vars from Gui
            string sSelectedPack = LstPacks.SelectedItems[0].Text;
            string sSelectedVersion = CmbPackVersions.SelectedIndex == 0 ? "recommended" : CmbPackVersions.Text;
            Launcher L = new Launcher(Side, Offline);
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
                    ShowInTaskbar = e.WindowState != FormWindowState.Minimized;
                    BtnStart.Enabled = true;
                    LstPacks.Enabled = true;
                }
            ));
        }

        private void MnuSettings_Click(object sender, EventArgs e)
        {
            Settings.Settings settings = new Settings.Settings();
            settings.ShowDialog();
            settings.Dispose();
        }

        private void MnuInfo_Click(object sender, EventArgs e)
        {
            FrmAbout frmAbout = new FrmAbout();
            frmAbout.ShowDialog();
            frmAbout.Dispose();
        }

        private void MnuEditPack_Click(object sender, EventArgs e)
        {
            if (LstPacks.SelectedItems.Count != 1)
            {
                MessageBox.Show(this, "Kein Pack gewählt.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // gather vars from Gui
            string sSelectedPack = LstPacks.SelectedItems[0].Text;
            string sSelectedVersion = CmbPackVersions.SelectedIndex == 0 ? "recommended" : CmbPackVersions.Text;
            Launcher L = new Launcher(Side, Offline);
            if (L.IsPackInstalled(sSelectedPack, sSelectedVersion) == false)
            {
                MessageBox.Show(this, "Dieses Pack ist nicht installiert oder liegt in einer anderen Version vor.\r\nBitte dieses Pack starten und danach bearbeiten.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FrmEditPack frmEditPack = new FrmEditPack(sSelectedPack);
            frmEditPack.ShowDialog();
            frmEditPack.Dispose();
        }

        private void LstPacks_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (e.Clicks == 2) Startpack();
        }

        private void BtnStart_Click(object sender, EventArgs e) => Startpack();

        private void MnuStartPack_Click(object sender, EventArgs e) => Startpack();

        private void MnuOpenPackFolder_Click(object sender, EventArgs e)
        {
            string sSelectedPack = LstPacks.SelectedItems[0].Text;
            Launcher L = new Launcher(Side, Offline);
            L.OpenPackFolder(sSelectedPack);
        }

        private void MnuRefreshPacketList_Click(object sender, EventArgs e) => DoInit(false);

        private void MnuDownloadPack_Click(object sender, EventArgs e) => Downloadpack();

        private void MnuReDownloadMods_Click(object sender, EventArgs e) => new Launcher(Side, Offline).ReDownloadMods(LstPacks.SelectedItems[0].Text);
    }
}
