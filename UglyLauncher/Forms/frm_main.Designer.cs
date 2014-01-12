namespace UglyLauncher
{
    partial class main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.launcherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmb_packversions = new System.Windows.Forms.ToolStripComboBox();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.lvImages = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.txt_default_account = new System.Windows.Forms.ToolStripStatusLabel();
            this.web_packdetails = new System.Windows.Forms.WebBrowser();
            this.startup_worker = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.launcherToolStripMenuItem,
            this.accountsToolStripMenuItem,
            this.cmb_packversions,
            this.playToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(683, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // launcherToolStripMenuItem
            // 
            this.launcherToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.beendenToolStripMenuItem});
            this.launcherToolStripMenuItem.Name = "launcherToolStripMenuItem";
            this.launcherToolStripMenuItem.Size = new System.Drawing.Size(68, 23);
            this.launcherToolStripMenuItem.Text = "Launcher";
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.beendenToolStripMenuItem.Text = "Beenden";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // accountsToolStripMenuItem
            // 
            this.accountsToolStripMenuItem.Name = "accountsToolStripMenuItem";
            this.accountsToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.accountsToolStripMenuItem.Size = new System.Drawing.Size(69, 23);
            this.accountsToolStripMenuItem.Text = "Accounts";
            this.accountsToolStripMenuItem.Click += new System.EventHandler(this.accountsToolStripMenuItem_Click);
            // 
            // cmb_packversions
            // 
            this.cmb_packversions.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmb_packversions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_packversions.Margin = new System.Windows.Forms.Padding(1, 0, 2, 0);
            this.cmb_packversions.Name = "cmb_packversions";
            this.cmb_packversions.Size = new System.Drawing.Size(150, 23);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(41, 23);
            this.playToolStripMenuItem.Text = "Play";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.playToolStripMenuItem_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.HideSelection = false;
            this.listView1.LabelWrap = false;
            this.listView1.LargeImageList = this.lvImages;
            this.listView1.Location = new System.Drawing.Point(3, 30);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(264, 357);
            this.listView1.TabIndex = 0;
            this.listView1.TabStop = false;
            this.listView1.TileSize = new System.Drawing.Size(260, 50);
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Tile;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // lvImages
            // 
            this.lvImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.lvImages.ImageSize = new System.Drawing.Size(48, 48);
            this.lvImages.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.txt_default_account});
            this.statusStrip1.Location = new System.Drawing.Point(0, 390);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(683, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(55, 17);
            this.toolStripStatusLabel1.Text = "Account:";
            // 
            // txt_default_account
            // 
            this.txt_default_account.AutoSize = false;
            this.txt_default_account.Name = "txt_default_account";
            this.txt_default_account.Size = new System.Drawing.Size(200, 17);
            this.txt_default_account.Text = "none";
            this.txt_default_account.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // web_packdetails
            // 
            this.web_packdetails.AllowWebBrowserDrop = false;
            this.web_packdetails.IsWebBrowserContextMenuEnabled = false;
            this.web_packdetails.Location = new System.Drawing.Point(270, 30);
            this.web_packdetails.MinimumSize = new System.Drawing.Size(20, 20);
            this.web_packdetails.Name = "web_packdetails";
            this.web_packdetails.Size = new System.Drawing.Size(413, 357);
            this.web_packdetails.TabIndex = 3;
            this.web_packdetails.WebBrowserShortcutsEnabled = false;
            // 
            // startup_worker
            // 
            this.startup_worker.WorkerReportsProgress = true;
            this.startup_worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.startup_check);
            this.startup_worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.startup_worker_ProgressChanged);
            this.startup_worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.startup_worker_RunWorkerCompleted);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 412);
            this.Controls.Add(this.web_packdetails);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "UglyLauncher";
            this.Load += new System.EventHandler(this.main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem accountsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem launcherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox cmb_packversions;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel txt_default_account;
        private System.Windows.Forms.ImageList lvImages;
        private System.Windows.Forms.WebBrowser web_packdetails;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker startup_worker;
    }
}

