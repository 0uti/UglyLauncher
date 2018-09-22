namespace UglyLauncher.AccountManager
{
    partial class FrmUserAccounts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserAccounts));
            this.LstAccounts = new System.Windows.Forms.ListView();
            this.lst_account_items = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnSetDefault = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LstAccounts
            // 
            this.LstAccounts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lst_account_items});
            this.LstAccounts.FullRowSelect = true;
            this.LstAccounts.GridLines = true;
            listViewItem1.StateImageIndex = 0;
            this.LstAccounts.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.LstAccounts.Location = new System.Drawing.Point(18, 18);
            this.LstAccounts.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LstAccounts.MultiSelect = false;
            this.LstAccounts.Name = "LstAccounts";
            this.LstAccounts.Size = new System.Drawing.Size(481, 270);
            this.LstAccounts.TabIndex = 0;
            this.LstAccounts.UseCompatibleStateImageBehavior = false;
            this.LstAccounts.View = System.Windows.Forms.View.Details;
            // 
            // lst_account_items
            // 
            this.lst_account_items.Text = "Account";
            this.lst_account_items.Width = 310;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(510, 18);
            this.BtnAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(112, 35);
            this.BtnAdd.TabIndex = 1;
            this.BtnAdd.Text = "Hinzufügen";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(510, 63);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(112, 35);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Text = "Löschen";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSetDefault
            // 
            this.BtnSetDefault.Location = new System.Drawing.Point(510, 255);
            this.BtnSetDefault.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnSetDefault.Name = "BtnSetDefault";
            this.BtnSetDefault.Size = new System.Drawing.Size(112, 35);
            this.BtnSetDefault.TabIndex = 3;
            this.BtnSetDefault.Text = "Standard";
            this.BtnSetDefault.UseVisualStyleBackColor = true;
            this.BtnSetDefault.Click += new System.EventHandler(this.BtnSetDefault_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(510, 300);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(112, 35);
            this.BtnClose.TabIndex = 4;
            this.BtnClose.Text = "Schließen";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // FrmUserAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 348);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnSetDefault);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.LstAccounts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUserAccounts";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Accountverwaltung";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView LstAccounts;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnSetDefault;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.ColumnHeader lst_account_items;
    }
}