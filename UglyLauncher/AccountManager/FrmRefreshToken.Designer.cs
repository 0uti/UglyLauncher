namespace UglyLauncher.AccountManager
{
    partial class FrmRefreshToken
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRefreshToken));
            this.lbl_username = new System.Windows.Forms.Label();
            this.lbl_password = new System.Windows.Forms.Label();
            this.TxtUser = new System.Windows.Forms.TextBox();
            this.TxtPass = new System.Windows.Forms.TextBox();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_username
            // 
            this.lbl_username.AutoSize = true;
            this.lbl_username.Location = new System.Drawing.Point(18, 23);
            this.lbl_username.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_username.Name = "lbl_username";
            this.lbl_username.Size = new System.Drawing.Size(87, 20);
            this.lbl_username.TabIndex = 0;
            this.lbl_username.Text = "Username:";
            // 
            // lbl_password
            // 
            this.lbl_password.AutoSize = true;
            this.lbl_password.Location = new System.Drawing.Point(18, 63);
            this.lbl_password.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_password.Name = "lbl_password";
            this.lbl_password.Size = new System.Drawing.Size(78, 20);
            this.lbl_password.TabIndex = 1;
            this.lbl_password.Text = "Passwort:";
            // 
            // TxtUser
            // 
            this.TxtUser.Location = new System.Drawing.Point(122, 18);
            this.TxtUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtUser.Name = "TxtUser";
            this.TxtUser.Size = new System.Drawing.Size(322, 26);
            this.TxtUser.TabIndex = 2;
            // 
            // TxtPass
            // 
            this.TxtPass.Location = new System.Drawing.Point(122, 58);
            this.TxtPass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtPass.Name = "TxtPass";
            this.TxtPass.PasswordChar = '*';
            this.TxtPass.Size = new System.Drawing.Size(322, 26);
            this.TxtPass.TabIndex = 3;
            this.TxtPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPass_KeyDown);
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(210, 98);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(112, 35);
            this.BtnCancel.TabIndex = 4;
            this.BtnCancel.Text = "Abbrechen";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnConfirm
            // 
            this.BtnConfirm.Location = new System.Drawing.Point(332, 98);
            this.BtnConfirm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnConfirm.Name = "BtnConfirm";
            this.BtnConfirm.Size = new System.Drawing.Size(112, 35);
            this.BtnConfirm.TabIndex = 5;
            this.BtnConfirm.Text = "Bestätigen";
            this.BtnConfirm.UseVisualStyleBackColor = true;
            this.BtnConfirm.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // FrmRefreshToken
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 149);
            this.Controls.Add(this.BtnConfirm);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.TxtPass);
            this.Controls.Add(this.TxtUser);
            this.Controls.Add(this.lbl_password);
            this.Controls.Add(this.lbl_username);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRefreshToken";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Account bestätigen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_username;
        private System.Windows.Forms.Label lbl_password;
        private System.Windows.Forms.TextBox TxtUser;
        private System.Windows.Forms.TextBox TxtPass;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnConfirm;
    }
}