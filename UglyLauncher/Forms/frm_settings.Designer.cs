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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.java_perm_gen = new UglyLauncher.NumericUpDownEx();
            this.java_max_mem = new UglyLauncher.NumericUpDownEx();
            this.java_min_mem = new UglyLauncher.NumericUpDownEx();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chk_keep_launcher = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.java_perm_gen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.java_max_mem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.java_min_mem)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(265, 246);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Text = "Abbrechen";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(184, 246);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 2;
            this.btn_save.Text = "Speichern";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_keep_console);
            this.groupBox1.Controls.Add(this.chk_console);
            this.groupBox1.Location = new System.Drawing.Point(12, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 70);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Konsole";
            // 
            // chk_keep_console
            // 
            this.chk_keep_console.AutoSize = true;
            this.chk_keep_console.Location = new System.Drawing.Point(7, 44);
            this.chk_keep_console.Name = "chk_keep_console";
            this.chk_keep_console.Size = new System.Drawing.Size(279, 17);
            this.chk_keep_console.TabIndex = 1;
            this.chk_keep_console.Text = "Konsole nach Beenden von Minecraft geöffnet halten";
            this.chk_keep_console.UseVisualStyleBackColor = true;
            // 
            // chk_console
            // 
            this.chk_console.AutoSize = true;
            this.chk_console.Location = new System.Drawing.Point(7, 20);
            this.chk_console.Name = "chk_console";
            this.chk_console.Size = new System.Drawing.Size(119, 17);
            this.chk_console.TabIndex = 0;
            this.chk_console.Text = "Konsole einblenden";
            this.chk_console.UseVisualStyleBackColor = true;
            this.chk_console.CheckedChanged += new System.EventHandler(this.chk_console_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.java_perm_gen);
            this.groupBox2.Controls.Add(this.java_max_mem);
            this.groupBox2.Controls.Add(this.java_min_mem);
            this.groupBox2.Location = new System.Drawing.Point(12, 137);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(328, 103);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Java Arbeitsspeicher";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "PermGen:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Maximaler Arbeitsspeicher:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Minimaler Arbeitsspeicher:";
            // 
            // java_perm_gen
            // 
            this.java_perm_gen.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.java_perm_gen.Location = new System.Drawing.Point(172, 73);
            this.java_perm_gen.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.java_perm_gen.Minimum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.java_perm_gen.Name = "java_perm_gen";
            this.java_perm_gen.Size = new System.Drawing.Size(143, 20);
            this.java_perm_gen.TabIndex = 2;
            this.java_perm_gen.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // java_max_mem
            // 
            this.java_max_mem.Increment = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.java_max_mem.Location = new System.Drawing.Point(172, 47);
            this.java_max_mem.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
            this.java_max_mem.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.java_max_mem.Name = "java_max_mem";
            this.java_max_mem.Size = new System.Drawing.Size(143, 20);
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
            this.java_min_mem.Location = new System.Drawing.Point(172, 20);
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
            this.java_min_mem.Size = new System.Drawing.Size(143, 20);
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
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(328, 43);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Launcher";
            // 
            // chk_keep_launcher
            // 
            this.chk_keep_launcher.AutoSize = true;
            this.chk_keep_launcher.Location = new System.Drawing.Point(7, 20);
            this.chk_keep_launcher.Name = "chk_keep_launcher";
            this.chk_keep_launcher.Size = new System.Drawing.Size(292, 17);
            this.chk_keep_launcher.TabIndex = 0;
            this.chk_keep_launcher.Text = "Launcher nach Beenden von Minecraft wieder anzeigen";
            this.chk_keep_launcher.UseVisualStyleBackColor = true;
            // 
            // frm_settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 276);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_settings";
            this.Text = "Einstellungen";
            this.Load += new System.EventHandler(this.frm_settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.java_perm_gen)).EndInit();
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private NumericUpDownEx java_perm_gen;
        private NumericUpDownEx java_max_mem;
        private NumericUpDownEx java_min_mem;
        private System.Windows.Forms.CheckBox chk_keep_console;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chk_keep_launcher;
    }
}