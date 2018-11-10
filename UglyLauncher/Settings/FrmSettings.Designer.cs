namespace UglyLauncher.Settings
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ChkKeepConsole = new System.Windows.Forms.CheckBox();
            this.ChkShowConsole = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ChkUseGC = new System.Windows.Forms.CheckBox();
            this.CmbJavaVersion = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.NumJavaMaxMemory = new NumericUpDownEx();
            this.NumJavaMinMemory = new NumericUpDownEx();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ChkKeepLauncher = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumJavaMaxMemory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumJavaMinMemory)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(398, 457);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(112, 35);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "Abbrechen";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(276, 457);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(112, 35);
            this.BtnSave.TabIndex = 2;
            this.BtnSave.Text = "Speichern";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChkKeepConsole);
            this.groupBox1.Controls.Add(this.ChkShowConsole);
            this.groupBox1.Location = new System.Drawing.Point(18, 94);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(492, 108);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Konsole";
            // 
            // ChkKeepConsole
            // 
            this.ChkKeepConsole.AutoSize = true;
            this.ChkKeepConsole.Location = new System.Drawing.Point(10, 68);
            this.ChkKeepConsole.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChkKeepConsole.Name = "ChkKeepConsole";
            this.ChkKeepConsole.Size = new System.Drawing.Size(411, 24);
            this.ChkKeepConsole.TabIndex = 1;
            this.ChkKeepConsole.Text = "Konsole nach Beenden von Minecraft geöffnet halten";
            this.ChkKeepConsole.UseVisualStyleBackColor = true;
            // 
            // ChkShowConsole
            // 
            this.ChkShowConsole.AutoSize = true;
            this.ChkShowConsole.Location = new System.Drawing.Point(10, 31);
            this.ChkShowConsole.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChkShowConsole.Name = "ChkShowConsole";
            this.ChkShowConsole.Size = new System.Drawing.Size(174, 24);
            this.ChkShowConsole.TabIndex = 0;
            this.ChkShowConsole.Text = "Konsole einblenden";
            this.ChkShowConsole.UseVisualStyleBackColor = true;
            this.ChkShowConsole.CheckedChanged += new System.EventHandler(this.ChkShowConsole_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ChkUseGC);
            this.groupBox2.Controls.Add(this.CmbJavaVersion);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.NumJavaMaxMemory);
            this.groupBox2.Controls.Add(this.NumJavaMinMemory);
            this.groupBox2.Location = new System.Drawing.Point(18, 211);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(492, 237);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Java";
            // 
            // ChkUseGC
            // 
            this.ChkUseGC.AutoSize = true;
            this.ChkUseGC.Location = new System.Drawing.Point(8, 203);
            this.ChkUseGC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChkUseGC.Name = "ChkUseGC";
            this.ChkUseGC.Size = new System.Drawing.Size(228, 24);
            this.ChkUseGC.TabIndex = 6;
            this.ChkUseGC.Text = "Benutze Garbage Collector";
            this.ChkUseGC.UseVisualStyleBackColor = true;
            // 
            // CmbJavaVersion
            // 
            this.CmbJavaVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbJavaVersion.FormattingEnabled = true;
            this.CmbJavaVersion.Items.AddRange(new object[] {
            "auto"});
            this.CmbJavaVersion.Location = new System.Drawing.Point(258, 29);
            this.CmbJavaVersion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CmbJavaVersion.Name = "CmbJavaVersion";
            this.CmbJavaVersion.Size = new System.Drawing.Size(212, 28);
            this.CmbJavaVersion.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 34);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Version";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 114);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Maximaler Arbeitsspeicher:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 74);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Minimaler Arbeitsspeicher:";
            // 
            // NumJavaMaxMemory
            // 
            this.NumJavaMaxMemory.Increment = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.NumJavaMaxMemory.Location = new System.Drawing.Point(258, 111);
            this.NumJavaMaxMemory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NumJavaMaxMemory.Maximum = new decimal(new int[] {
            16384,
            0,
            0,
            0});
            this.NumJavaMaxMemory.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.NumJavaMaxMemory.Name = "NumJavaMaxMemory";
            this.NumJavaMaxMemory.Size = new System.Drawing.Size(214, 26);
            this.NumJavaMaxMemory.TabIndex = 1;
            this.NumJavaMaxMemory.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // NumJavaMinMemory
            // 
            this.NumJavaMinMemory.Increment = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.NumJavaMinMemory.Location = new System.Drawing.Point(258, 71);
            this.NumJavaMinMemory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NumJavaMinMemory.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
            this.NumJavaMinMemory.Minimum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.NumJavaMinMemory.Name = "NumJavaMinMemory";
            this.NumJavaMinMemory.Size = new System.Drawing.Size(214, 26);
            this.NumJavaMinMemory.TabIndex = 0;
            this.NumJavaMinMemory.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.NumJavaMinMemory.ValueChanged += new System.EventHandler(this.NumJavaMinMemory_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ChkKeepLauncher);
            this.groupBox3.Location = new System.Drawing.Point(18, 18);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(492, 66);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Launcher";
            // 
            // ChkKeepLauncher
            // 
            this.ChkKeepLauncher.AutoSize = true;
            this.ChkKeepLauncher.Location = new System.Drawing.Point(10, 31);
            this.ChkKeepLauncher.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChkKeepLauncher.Name = "ChkKeepLauncher";
            this.ChkKeepLauncher.Size = new System.Drawing.Size(428, 24);
            this.ChkKeepLauncher.TabIndex = 0;
            this.ChkKeepLauncher.Text = "Launcher nach Beenden von Minecraft wieder anzeigen";
            this.ChkKeepLauncher.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 503);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.BtnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Text = "Einstellungen";
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumJavaMaxMemory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumJavaMinMemory)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ChkShowConsole;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private NumericUpDownEx NumJavaMaxMemory;
        private NumericUpDownEx NumJavaMinMemory;
        private System.Windows.Forms.CheckBox ChkKeepConsole;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox ChkKeepLauncher;
        private System.Windows.Forms.ComboBox CmbJavaVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ChkUseGC;
    }
}