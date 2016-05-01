namespace UglyLauncher
{
    partial class frm_main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_main));
            this.mnu_container = new System.Windows.Forms.MenuStrip();
            this.mnu_exit_program = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_accounts = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_edit_Pack = new System.Windows.Forms.ToolStripMenuItem();
            this.einstellungenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lst_packs = new System.Windows.Forms.ListView();
            this.PackListContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lst_packs_images = new System.Windows.Forms.ImageList(this.components);
            this.oStatusBar = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_default_account = new System.Windows.Forms.ToolStripStatusLabel();
            this.web_packdetails = new System.Windows.Forms.WebBrowser();
            this.btn_start = new System.Windows.Forms.Button();
            this.cmb_packversions = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.öffneVerzeichnissToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_container.SuspendLayout();
            this.PackListContext.SuspendLayout();
            this.oStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnu_container
            // 
            this.mnu_container.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_exit_program,
            this.mnu_accounts,
            this.mnu_edit_Pack,
            this.einstellungenToolStripMenuItem,
            this.infoToolStripMenuItem});
            this.mnu_container.Location = new System.Drawing.Point(0, 0);
            this.mnu_container.Name = "mnu_container";
            this.mnu_container.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnu_container.Size = new System.Drawing.Size(744, 24);
            this.mnu_container.TabIndex = 0;
            this.mnu_container.Text = "menuStrip1";
            // 
            // mnu_exit_program
            // 
            this.mnu_exit_program.Name = "mnu_exit_program";
            this.mnu_exit_program.Size = new System.Drawing.Size(65, 20);
            this.mnu_exit_program.Text = "Beenden";
            this.mnu_exit_program.Click += new System.EventHandler(this.mnu_exit_program_Click);
            // 
            // mnu_accounts
            // 
            this.mnu_accounts.Name = "mnu_accounts";
            this.mnu_accounts.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mnu_accounts.Size = new System.Drawing.Size(123, 20);
            this.mnu_accounts.Text = "Accountverwaltung";
            this.mnu_accounts.Click += new System.EventHandler(this.accountsToolStripMenuItem_Click);
            // 
            // mnu_edit_Pack
            // 
            this.mnu_edit_Pack.Name = "mnu_edit_Pack";
            this.mnu_edit_Pack.Size = new System.Drawing.Size(103, 20);
            this.mnu_edit_Pack.Text = "Pack bearbeiten";
            this.mnu_edit_Pack.Click += new System.EventHandler(this.packBearbeitenToolStripMenuItem_Click);
            // 
            // einstellungenToolStripMenuItem
            // 
            this.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
            this.einstellungenToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.einstellungenToolStripMenuItem.Text = "Einstellungen";
            this.einstellungenToolStripMenuItem.Click += new System.EventHandler(this.einstellungenToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // lst_packs
            // 
            this.lst_packs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lst_packs.ContextMenuStrip = this.PackListContext;
            this.lst_packs.HideSelection = false;
            this.lst_packs.LabelWrap = false;
            this.lst_packs.LargeImageList = this.lst_packs_images;
            this.lst_packs.Location = new System.Drawing.Point(0, 30);
            this.lst_packs.MultiSelect = false;
            this.lst_packs.Name = "lst_packs";
            this.lst_packs.Size = new System.Drawing.Size(327, 337);
            this.lst_packs.TabIndex = 0;
            this.lst_packs.TabStop = false;
            this.lst_packs.TileSize = new System.Drawing.Size(260, 50);
            this.lst_packs.UseCompatibleStateImageBehavior = false;
            this.lst_packs.View = System.Windows.Forms.View.Tile;
            this.lst_packs.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.lst_packs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lst_packs_MouseDown);
            // 
            // PackListContext
            // 
            this.PackListContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadToolStripMenuItem,
            this.startToolStripMenuItem,
            this.öffneVerzeichnissToolStripMenuItem});
            this.PackListContext.Name = "contextMenuStrip1";
            this.PackListContext.Size = new System.Drawing.Size(171, 92);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.downloadToolStripMenuItem.Text = "Download";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // lst_packs_images
            // 
            this.lst_packs_images.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.lst_packs_images.ImageSize = new System.Drawing.Size(48, 48);
            this.lst_packs_images.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // oStatusBar
            // 
            this.oStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lbl_default_account});
            this.oStatusBar.Location = new System.Drawing.Point(0, 390);
            this.oStatusBar.Name = "oStatusBar";
            this.oStatusBar.Size = new System.Drawing.Size(744, 22);
            this.oStatusBar.SizingGrip = false;
            this.oStatusBar.TabIndex = 2;
            this.oStatusBar.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(55, 17);
            this.toolStripStatusLabel1.Text = "Account:";
            // 
            // lbl_default_account
            // 
            this.lbl_default_account.AutoSize = false;
            this.lbl_default_account.Name = "lbl_default_account";
            this.lbl_default_account.Size = new System.Drawing.Size(200, 17);
            this.lbl_default_account.Text = "none";
            this.lbl_default_account.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // web_packdetails
            // 
            this.web_packdetails.AllowWebBrowserDrop = false;
            this.web_packdetails.IsWebBrowserContextMenuEnabled = false;
            this.web_packdetails.Location = new System.Drawing.Point(327, 30);
            this.web_packdetails.MinimumSize = new System.Drawing.Size(20, 20);
            this.web_packdetails.Name = "web_packdetails";
            this.web_packdetails.Size = new System.Drawing.Size(417, 360);
            this.web_packdetails.TabIndex = 3;
            this.web_packdetails.WebBrowserShortcutsEnabled = false;
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(263, 367);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(64, 23);
            this.btn_start.TabIndex = 4;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // cmb_packversions
            // 
            this.cmb_packversions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_packversions.FormattingEnabled = true;
            this.cmb_packversions.Location = new System.Drawing.Point(48, 368);
            this.cmb_packversions.Name = "cmb_packversions";
            this.cmb_packversions.Size = new System.Drawing.Size(209, 21);
            this.cmb_packversions.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 372);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Version";
            // 
            // öffneVerzeichnissToolStripMenuItem
            // 
            this.öffneVerzeichnissToolStripMenuItem.Name = "öffneVerzeichnissToolStripMenuItem";
            this.öffneVerzeichnissToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.öffneVerzeichnissToolStripMenuItem.Text = "Öffne Verzeichniss";
            this.öffneVerzeichnissToolStripMenuItem.Click += new System.EventHandler(this.öffneVerzeichnissToolStripMenuItem_Click);
            // 
            // frm_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 412);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_packversions);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.web_packdetails);
            this.Controls.Add(this.lst_packs);
            this.Controls.Add(this.oStatusBar);
            this.Controls.Add(this.mnu_container);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnu_container;
            this.MaximizeBox = false;
            this.Name = "frm_main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UglyLauncher";
            this.Load += new System.EventHandler(this.main_Load);
            this.Shown += new System.EventHandler(this.main_Shown);
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
        private System.Windows.Forms.ToolStripMenuItem mnu_accounts;
        private System.Windows.Forms.ToolStripMenuItem mnu_exit_program;
        private System.Windows.Forms.ListView lst_packs;
        private System.Windows.Forms.StatusStrip oStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lbl_default_account;
        private System.Windows.Forms.ImageList lst_packs_images;
        private System.Windows.Forms.WebBrowser web_packdetails;
        private System.Windows.Forms.ToolStripMenuItem einstellungenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.ComboBox cmb_packversions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem mnu_edit_Pack;
        private System.Windows.Forms.ContextMenuStrip PackListContext;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem öffneVerzeichnissToolStripMenuItem;
    }
}

