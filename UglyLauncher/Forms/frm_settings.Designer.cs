using UglyLauncher;
namespace UglyLauncher
{
    partial class frm_settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_settings));
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_keep_console = new System.Windows.Forms.CheckBox();
            this.chk_console = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk_use_gc = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.java_max_mem = new UglyLauncher.NumericUpDownEx();
            this.java_min_mem = new UglyLauncher.NumericUpDownEx();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chk_keep_launcher = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.java_max_mem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.java_min_mem)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(398, 457);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(112, 35);
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Text = "Abbrechen";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(276, 457);
            this.btn_save.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(112, 35);
            this.btn_save.TabIndex = 2;
            this.btn_save.Text = "Speichern";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_keep_console);
            this.groupBox1.Controls.Add(this.chk_console);
            this.groupBox1.Location = new System.Drawing.Point(18, 94);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(492, 108);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Konsole";
            // 
            // chk_keep_console
            // 
            this.chk_keep_console.AutoSize = true;
            this.chk_keep_console.Location = new System.Drawing.Point(10, 68);
            this.chk_keep_console.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chk_keep_console.Name = "chk_keep_console";
            this.chk_keep_console.Size = new System.Drawing.Size(411, 24);
            this.chk_keep_console.TabIndex = 1;
            this.chk_keep_console.Text = "Konsole nach Beenden von Minecraft geöffnet halten";
            this.chk_keep_console.UseVisualStyleBackColor = true;
            // 
            // chk_console
            // 
            this.chk_console.AutoSize = true;
            this.chk_console.Location = new System.Drawing.Point(10, 31);
            this.chk_console.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chk_console.Name = "chk_console";
            this.chk_console.Size = new System.Drawing.Size(174, 24);
            this.chk_console.TabIndex = 0;
            this.chk_console.Text = "Konsole einblenden";
            this.chk_console.UseVisualStyleBackColor = true;
            this.chk_console.CheckedChanged += new System.EventHandler(this.chk_console_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk_use_gc);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.java_max_mem);
            this.groupBox2.Controls.Add(this.java_min_mem);
            this.groupBox2.Location = new System.Drawing.Point(18, 211);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(492, 237);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Java";
            // 
            // chk_use_gc
            // 
            this.chk_use_gc.AutoSize = true;
            this.chk_use_gc.Location = new System.Drawing.Point(8, 203);
            this.chk_use_gc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chk_use_gc.Name = "chk_use_gc";
            this.chk_use_gc.Size = new System.Drawing.Size(228, 24);
            this.chk_use_gc.TabIndex = 6;
            this.chk_use_gc.Text = "Benutze Garbage Collector";
            this.chk_use_gc.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "auto"});
            this.comboBox1.Location = new System.Drawing.Point(258, 29);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(212, 28);
            this.comboBox1.TabIndex = 1;
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
            // java_max_mem
            // 
            this.java_max_mem.Increment = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.java_max_mem.Location = new System.Drawing.Point(258, 111);
            this.java_max_mem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.java_max_mem.Maximum = new decimal(new int[] {
            16384,
            0,
            0,
            0});
            this.java_max_mem.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.java_max_mem.Name = "java_max_mem";
            this.java_max_mem.Size = new System.Drawing.Size(214, 26);
            this.java_max_mem.TabIndex = 1;
            this.java_max_mem.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // java_min_mem
            // 
            this.java_min_mem.Increment = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.java_min_mem.Location = new System.Drawing.Point(258, 71);
            this.java_min_mem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.java_min_mem.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
            this.java_min_mem.Minimum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.java_min_mem.Name = "java_min_mem";
            this.java_min_mem.Size = new System.Drawing.Size(214, 26);
            this.java_min_mem.TabIndex = 0;
            this.java_min_mem.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.java_min_mem.ValueChanged += new System.EventHandler(this.java_min_mem_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chk_keep_launcher);
            this.groupBox3.Location = new System.Drawing.Point(18, 18);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(492, 66);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Launcher";
            // 
            // chk_keep_launcher
            // 
            this.chk_keep_launcher.AutoSize = true;
            this.chk_keep_launcher.Location = new System.Drawing.Point(10, 31);
            this.chk_keep_launcher.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chk_keep_launcher.Name = "chk_keep_launcher";
            this.chk_keep_launcher.Size = new System.Drawing.Size(428, 24);
            this.chk_keep_launcher.TabIndex = 0;
            this.chk_keep_launcher.Text = "Launcher nach Beenden von Minecraft wieder anzeigen";
            this.chk_keep_launcher.UseVisualStyleBackColor = true;
            // 
            // frm_settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 503);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_settings";
            this.Text = "Einstellungen";
            this.Load += new System.EventHandler(this.frm_settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.java_max_mem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.java_min_mem)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chk_console;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private NumericUpDownEx java_max_mem;
        private NumericUpDownEx java_min_mem;
        private System.Windows.Forms.CheckBox chk_keep_console;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chk_keep_launcher;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chk_use_gc;
    }
}