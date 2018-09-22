namespace UglyLauncher
{
    partial class FrmEditPack
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditPack));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnEnableSelected = new System.Windows.Forms.Button();
            this.BtnEnableAll = new System.Windows.Forms.Button();
            this.BtnDisableAll = new System.Windows.Forms.Button();
            this.BtnDisableSelected = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.LstAvailbleMods = new UglyLauncher.ToolTipListBox();
            this.LstEnabledMods = new UglyLauncher.ToolTipListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(158, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "verfügbare Mods";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(800, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "Aktivierte Mods";
            // 
            // BtnEnableSelected
            // 
            this.BtnEnableSelected.Location = new System.Drawing.Point(526, 180);
            this.BtnEnableSelected.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnEnableSelected.Name = "BtnEnableSelected";
            this.BtnEnableSelected.Size = new System.Drawing.Size(112, 35);
            this.BtnEnableSelected.TabIndex = 4;
            this.BtnEnableSelected.Text = ">";
            this.BtnEnableSelected.UseVisualStyleBackColor = true;
            this.BtnEnableSelected.Click += new System.EventHandler(this.BtnEnableSelected_Click);
            // 
            // BtnEnableAll
            // 
            this.BtnEnableAll.Enabled = false;
            this.BtnEnableAll.Location = new System.Drawing.Point(526, 225);
            this.BtnEnableAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnEnableAll.Name = "BtnEnableAll";
            this.BtnEnableAll.Size = new System.Drawing.Size(112, 35);
            this.BtnEnableAll.TabIndex = 5;
            this.BtnEnableAll.Text = ">>";
            this.BtnEnableAll.UseVisualStyleBackColor = true;
            this.BtnEnableAll.Click += new System.EventHandler(this.BtnEnableAll_Click);
            // 
            // BtnDisableAll
            // 
            this.BtnDisableAll.Enabled = false;
            this.BtnDisableAll.Location = new System.Drawing.Point(526, 303);
            this.BtnDisableAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnDisableAll.Name = "BtnDisableAll";
            this.BtnDisableAll.Size = new System.Drawing.Size(112, 35);
            this.BtnDisableAll.TabIndex = 6;
            this.BtnDisableAll.Text = "<<";
            this.BtnDisableAll.UseVisualStyleBackColor = true;
            this.BtnDisableAll.Click += new System.EventHandler(this.BtnDisableAll_Click);
            // 
            // BtnDisableSelected
            // 
            this.BtnDisableSelected.Location = new System.Drawing.Point(526, 348);
            this.BtnDisableSelected.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnDisableSelected.Name = "BtnDisableSelected";
            this.BtnDisableSelected.Size = new System.Drawing.Size(112, 35);
            this.BtnDisableSelected.TabIndex = 7;
            this.BtnDisableSelected.Text = "<";
            this.BtnDisableSelected.UseVisualStyleBackColor = true;
            this.BtnDisableSelected.Click += new System.EventHandler(this.BtnDisableSelected_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(526, 540);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(112, 35);
            this.BtnClose.TabIndex = 10;
            this.BtnClose.Text = "Schließen";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // LstAvailbleMods
            // 
            this.LstAvailbleMods.DisplayMember = "DisplayText";
            this.LstAvailbleMods.FormattingEnabled = true;
            this.LstAvailbleMods.ItemHeight = 20;
            this.LstAvailbleMods.Location = new System.Drawing.Point(18, 49);
            this.LstAvailbleMods.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LstAvailbleMods.Name = "LstAvailbleMods";
            this.LstAvailbleMods.Size = new System.Drawing.Size(498, 524);
            this.LstAvailbleMods.TabIndex = 11;
            // 
            // LstEnabledMods
            // 
            this.LstEnabledMods.DisplayMember = "DisplayText";
            this.LstEnabledMods.FormattingEnabled = true;
            this.LstEnabledMods.ItemHeight = 20;
            this.LstEnabledMods.Location = new System.Drawing.Point(648, 49);
            this.LstEnabledMods.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LstEnabledMods.Name = "LstEnabledMods";
            this.LstEnabledMods.Size = new System.Drawing.Size(498, 524);
            this.LstEnabledMods.TabIndex = 8;
            // 
            // FrmEditPack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 589);
            this.Controls.Add(this.LstAvailbleMods);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.LstEnabledMods);
            this.Controls.Add(this.BtnDisableSelected);
            this.Controls.Add(this.BtnDisableAll);
            this.Controls.Add(this.BtnEnableAll);
            this.Controls.Add(this.BtnEnableSelected);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditPack";
            this.Text = "Pack bearbeiten";
            this.Shown += new System.EventHandler(this.FrmEditPack_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnEnableSelected;
        private System.Windows.Forms.Button BtnEnableAll;
        private System.Windows.Forms.Button BtnDisableAll;
        private System.Windows.Forms.Button BtnDisableSelected;
        private ToolTipListBox LstEnabledMods;
        private System.Windows.Forms.Button BtnClose;
        private ToolTipListBox LstAvailbleMods;
    }
}