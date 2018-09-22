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
            this.pbar_progress = new System.Windows.Forms.ProgressBar();
            this.lbl_FileName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pbar_progress
            // 
            this.pbar_progress.Location = new System.Drawing.Point(12, 45);
            this.pbar_progress.Name = "pbar_progress";
            this.pbar_progress.Size = new System.Drawing.Size(326, 23);
            this.pbar_progress.TabIndex = 0;
            // 
            // lbl_FileName
            // 
            this.lbl_FileName.Location = new System.Drawing.Point(12, 9);
            this.lbl_FileName.Name = "lbl_FileName";
            this.lbl_FileName.Size = new System.Drawing.Size(318, 23);
            this.lbl_FileName.TabIndex = 1;
            this.lbl_FileName.Text = "Initialisierung (Account/Packlist)";
            this.lbl_FileName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frm_progressbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 80);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_FileName);
            this.Controls.Add(this.pbar_progress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_progressbar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bitte Warten";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbar_progress;
        private System.Windows.Forms.Label lbl_FileName;
    }
}