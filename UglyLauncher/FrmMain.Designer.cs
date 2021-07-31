namespace UglyLauncher
{
    partial class FrmMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.mnu_container = new System.Windows.Forms.MenuStrip();
            this.MnuLauncher = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuRefreshPacketList = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuExitProgram = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuAccounts = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuEditPack = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.LstPacks = new System.Windows.Forms.ListView();
            this.PackListContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MnuDownloadPack = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuStartPack = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuOpenPackFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.lst_packs_images = new System.Windows.Forms.ImageList(this.components);
            this.oStatusBar = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.LblDefaultAccount = new System.Windows.Forms.ToolStripStatusLabel();
            this.WebPackDetails = new System.Windows.Forms.WebBrowser();
            this.BtnStart = new System.Windows.Forms.Button();
            this.CmbPackVersions = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MnuReDownloadMods = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_container.SuspendLayout();
            this.PackListContext.SuspendLayout();
            this.oStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnu_container
            // 
            this.mnu_container.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.mnu_container.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mnu_container.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuLauncher,
            this.MnuAccounts,
            this.MnuEditPack,
            this.MnuSettings,
            this.MnuInfo});
            this.mnu_container.Location = new System.Drawing.Point(0, 0);
            this.mnu_container.Name = "mnu_container";
            this.mnu_container.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnu_container.Size = new System.Drawing.Size(1116, 35);
            this.mnu_container.TabIndex = 0;
            this.mnu_container.Text = "menuStrip1";
            // 
            // MnuLauncher
            // 
            this.MnuLauncher.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuRefreshPacketList,
            this.MnuExitProgram});
            this.MnuLauncher.Name = "MnuLauncher";
            this.MnuLauncher.Size = new System.Drawing.Size(98, 29);
            this.MnuLauncher.Text = "Launcher";
            // 
            // MnuRefreshPacketList
            // 
            this.MnuRefreshPacketList.Name = "MnuRefreshPacketList";
            this.MnuRefreshPacketList.Size = new System.Drawing.Size(277, 34);
            this.MnuRefreshPacketList.Text = "Packetliste neu laden";
            this.MnuRefreshPacketList.Click += new System.EventHandler(this.MnuRefreshPacketList_Click);
            // 
            // MnuExitProgram
            // 
            this.MnuExitProgram.Name = "MnuExitProgram";
            this.MnuExitProgram.Size = new System.Drawing.Size(277, 34);
            this.MnuExitProgram.Text = "Beenden";
            this.MnuExitProgram.Click += new System.EventHandler(this.MnuExitProgram_Click);
            // 
            // MnuAccounts
            // 
            this.MnuAccounts.Name = "MnuAccounts";
            this.MnuAccounts.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MnuAccounts.Size = new System.Drawing.Size(180, 29);
            this.MnuAccounts.Text = "Accountverwaltung";
            this.MnuAccounts.Click += new System.EventHandler(this.MnuAccounts_Click);
            // 
            // MnuEditPack
            // 
            this.MnuEditPack.Name = "MnuEditPack";
            this.MnuEditPack.Size = new System.Drawing.Size(152, 29);
            this.MnuEditPack.Text = "Pack bearbeiten";
            this.MnuEditPack.Click += new System.EventHandler(this.MnuEditPack_Click);
            // 
            // MnuSettings
            // 
            this.MnuSettings.Name = "MnuSettings";
            this.MnuSettings.Size = new System.Drawing.Size(132, 29);
            this.MnuSettings.Text = "Einstellungen";
            this.MnuSettings.Click += new System.EventHandler(this.MnuSettings_Click);
            // 
            // MnuInfo
            // 
            this.MnuInfo.Name = "MnuInfo";
            this.MnuInfo.Size = new System.Drawing.Size(60, 29);
            this.MnuInfo.Text = "Info";
            this.MnuInfo.Click += new System.EventHandler(this.MnuInfo_Click);
            // 
            // LstPacks
            // 
            this.LstPacks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LstPacks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LstPacks.ContextMenuStrip = this.PackListContext;
            this.LstPacks.HideSelection = false;
            this.LstPacks.LabelWrap = false;
            this.LstPacks.LargeImageList = this.lst_packs_images;
            this.LstPacks.Location = new System.Drawing.Point(0, 40);
            this.LstPacks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LstPacks.MultiSelect = false;
            this.LstPacks.Name = "LstPacks";
            this.LstPacks.Size = new System.Drawing.Size(488, 522);
            this.LstPacks.TabIndex = 0;
            this.LstPacks.TabStop = false;
            this.LstPacks.TileSize = new System.Drawing.Size(260, 50);
            this.LstPacks.UseCompatibleStateImageBehavior = false;
            this.LstPacks.View = System.Windows.Forms.View.Tile;
            this.LstPacks.SelectedIndexChanged += new System.EventHandler(this.LstPacks_SelectedIndexChanged);
            this.LstPacks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LstPacks_MouseDown);
            // 
            // PackListContext
            // 
            this.PackListContext.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.PackListContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuDownloadPack,
            this.MnuStartPack,
            this.MnuOpenPackFolder,
            this.MnuReDownloadMods});
            this.PackListContext.Name = "contextMenuStrip1";
            this.PackListContext.Size = new System.Drawing.Size(292, 165);
            // 
            // MnuDownloadPack
            // 
            this.MnuDownloadPack.Name = "MnuDownloadPack";
            this.MnuDownloadPack.Size = new System.Drawing.Size(291, 32);
            this.MnuDownloadPack.Text = "Download";
            this.MnuDownloadPack.Click += new System.EventHandler(this.MnuDownloadPack_Click);
            // 
            // MnuStartPack
            // 
            this.MnuStartPack.Name = "MnuStartPack";
            this.MnuStartPack.Size = new System.Drawing.Size(291, 32);
            this.MnuStartPack.Text = "Start";
            this.MnuStartPack.Click += new System.EventHandler(this.MnuStartPack_Click);
            // 
            // MnuOpenPackFolder
            // 
            this.MnuOpenPackFolder.Name = "MnuOpenPackFolder";
            this.MnuOpenPackFolder.Size = new System.Drawing.Size(291, 32);
            this.MnuOpenPackFolder.Text = "Öffne Verzeichniss";
            this.MnuOpenPackFolder.Click += new System.EventHandler(this.MnuOpenPackFolder_Click);
            // 
            // lst_packs_images
            // 
            this.lst_packs_images.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.lst_packs_images.ImageSize = new System.Drawing.Size(48, 48);
            this.lst_packs_images.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // oStatusBar
            // 
            this.oStatusBar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.oStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.LblDefaultAccount});
            this.oStatusBar.Location = new System.Drawing.Point(0, 602);
            this.oStatusBar.Name = "oStatusBar";
            this.oStatusBar.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.oStatusBar.Size = new System.Drawing.Size(1116, 32);
            this.oStatusBar.SizingGrip = false;
            this.oStatusBar.TabIndex = 2;
            this.oStatusBar.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(81, 25);
            this.toolStripStatusLabel1.Text = "Account:";
            // 
            // LblDefaultAccount
            // 
            this.LblDefaultAccount.AutoSize = false;
            this.LblDefaultAccount.Name = "LblDefaultAccount";
            this.LblDefaultAccount.Size = new System.Drawing.Size(200, 25);
            this.LblDefaultAccount.Text = "none";
            this.LblDefaultAccount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WebPackDetails
            // 
            this.WebPackDetails.AllowWebBrowserDrop = false;
            this.WebPackDetails.IsWebBrowserContextMenuEnabled = false;
            this.WebPackDetails.Location = new System.Drawing.Point(490, 40);
            this.WebPackDetails.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.WebPackDetails.MinimumSize = new System.Drawing.Size(30, 31);
            this.WebPackDetails.Name = "WebPackDetails";
            this.WebPackDetails.Size = new System.Drawing.Size(626, 560);
            this.WebPackDetails.TabIndex = 3;
            this.WebPackDetails.WebBrowserShortcutsEnabled = false;
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(394, 565);
            this.BtnStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(96, 35);
            this.BtnStart.TabIndex = 4;
            this.BtnStart.Text = "Start";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // CmbPackVersions
            // 
            this.CmbPackVersions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPackVersions.FormattingEnabled = true;
            this.CmbPackVersions.Location = new System.Drawing.Point(72, 566);
            this.CmbPackVersions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CmbPackVersions.Name = "CmbPackVersions";
            this.CmbPackVersions.Size = new System.Drawing.Size(312, 28);
            this.CmbPackVersions.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 572);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Version";
            // 
            // MnuReDownloadMods
            // 
            this.MnuReDownloadMods.Name = "MnuReDownloadMods";
            this.MnuReDownloadMods.Size = new System.Drawing.Size(291, 32);
            this.MnuReDownloadMods.Text = "Mods erneut Downloaden";
            this.MnuReDownloadMods.Click += new System.EventHandler(this.MnuReDownloadMods_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 634);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CmbPackVersions);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.WebPackDetails);
            this.Controls.Add(this.LstPacks);
            this.Controls.Add(this.oStatusBar);
            this.Controls.Add(this.mnu_container);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnu_container;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UglyLauncher";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.mnu_container.ResumeLayout(false);
            this.mnu_container.PerformLayout();
            this.PackListContext.ResumeLayout(false);
            this.oStatusBar.ResumeLayout(false);
            this.oStatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnu_container;
        private System.Windows.Forms.ToolStripMenuItem MnuAccounts;
        private System.Windows.Forms.ListView LstPacks;
        private System.Windows.Forms.StatusStrip oStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel LblDefaultAccount;
        private System.Windows.Forms.ImageList lst_packs_images;
        private System.Windows.Forms.WebBrowser WebPackDetails;
        private System.Windows.Forms.ToolStripMenuItem MnuSettings;
        private System.Windows.Forms.ToolStripMenuItem MnuInfo;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.ComboBox CmbPackVersions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem MnuEditPack;
        private System.Windows.Forms.ContextMenuStrip PackListContext;
        private System.Windows.Forms.ToolStripMenuItem MnuDownloadPack;
        private System.Windows.Forms.ToolStripMenuItem MnuStartPack;
        private System.Windows.Forms.ToolStripMenuItem MnuOpenPackFolder;
        private System.Windows.Forms.ToolStripMenuItem MnuLauncher;
        private System.Windows.Forms.ToolStripMenuItem MnuRefreshPacketList;
        private System.Windows.Forms.ToolStripMenuItem MnuExitProgram;
        private System.Windows.Forms.ToolStripMenuItem MnuReDownloadMods;
    }
}

