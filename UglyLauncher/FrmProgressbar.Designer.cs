namespace UglyLauncher
{
    partial class FrmProgressbar
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
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.LblFileName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(18, 69);
            this.ProgressBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(489, 35);
            this.ProgressBar.TabIndex = 0;
            // 
            // LblFileName
            // 
            this.LblFileName.Location = new System.Drawing.Point(18, 14);
            this.LblFileName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblFileName.Name = "LblFileName";
            this.LblFileName.Size = new System.Drawing.Size(477, 35);
            this.LblFileName.TabIndex = 1;
            this.LblFileName.Text = "Initialisierung (Account/Packlist)";
            this.LblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmProgressbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 123);
            this.ControlBox = false;
            this.Controls.Add(this.LblFileName);
            this.Controls.Add(this.ProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProgressbar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bitte Warten";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label LblFileName;
    }
}