namespace UglyLauncher
{
    partial class frm_UserAccounts
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "acc 1",
            "lol"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Empty, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("acc 2");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("acc 3");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_UserAccounts));
            this.lst_accounts = new System.Windows.Forms.ListView();
            this.lst_account_items = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_default = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lst_accounts
            // 
            this.lst_accounts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lst_account_items});
            this.lst_accounts.FullRowSelect = true;
            this.lst_accounts.GridLines = true;
            listViewItem1.StateImageIndex = 0;
            this.lst_accounts.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.lst_accounts.Location = new System.Drawing.Point(12, 12);
            this.lst_accounts.MultiSelect = false;
            this.lst_accounts.Name = "lst_accounts";
            this.lst_accounts.Size = new System.Drawing.Size(322, 177);
            this.lst_accounts.TabIndex = 0;
            this.lst_accounts.UseCompatibleStateImageBehavior = false;
            this.lst_accounts.View = System.Windows.Forms.View.Details;
            // 
            // lst_account_items
            // 
            this.lst_account_items.Text = "Account";
            this.lst_account_items.Width = 310;
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(340, 12);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 23);
            this.btn_add.TabIndex = 1;
            this.btn_add.Text = "Hinzufügen";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Location = new System.Drawing.Point(340, 41);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(75, 23);
            this.btn_delete.TabIndex = 2;
            this.btn_delete.Text = "Löschen";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_default
            // 
            this.btn_default.Location = new System.Drawing.Point(340, 166);
            this.btn_default.Name = "btn_default";
            this.btn_default.Size = new System.Drawing.Size(75, 23);
            this.btn_default.TabIndex = 3;
            this.btn_default.Text = "Standard";
            this.btn_default.UseVisualStyleBackColor = true;
            this.btn_default.Click += new System.EventHandler(this.btn_default_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(340, 195);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 4;
            this.btn_close.Text = "Schließen";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // frm_UserAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 226);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_default);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.lst_accounts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_UserAccounts";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Accountverwaltung";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lst_accounts;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_default;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.ColumnHeader lst_account_items;
    }
}