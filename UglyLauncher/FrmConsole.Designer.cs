namespace UglyLauncher
{
    partial class FrmConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsole));
            this.BtnClose = new System.Windows.Forms.Button();
            this.TxtConsole = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnClose.Location = new System.Drawing.Point(1186, 771);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(112, 35);
            this.BtnClose.TabIndex = 0;
            this.BtnClose.Text = "Schließen";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // TxtConsole
            // 
            this.TxtConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtConsole.Location = new System.Drawing.Point(0, 0);
            this.TxtConsole.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtConsole.Name = "TxtConsole";
            this.TxtConsole.ReadOnly = true;
            this.TxtConsole.Size = new System.Drawing.Size(1297, 759);
            this.TxtConsole.TabIndex = 1;
            this.TxtConsole.Text = "";
            this.TxtConsole.WordWrap = false;
            this.TxtConsole.TextChanged += new System.EventHandler(this.TxtConsole_TextChanged);
            // 
            // FrmConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 806);
            this.Controls.Add(this.TxtConsole);
            this.Controls.Add(this.BtnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmConsole";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Console";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.RichTextBox TxtConsole;
    }
}