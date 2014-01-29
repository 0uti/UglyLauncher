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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.java_perm_gen = new UglyLauncher.NumericUpDownEx();
            this.java_max_mem = new UglyLauncher.NumericUpDownEx();
            this.java_min_mem = new UglyLauncher.NumericUpDownEx();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.java_perm_gen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.java_max_mem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.java_min_mem)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(265, 175);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Abbrechen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(184, 175);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Speichern";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 48);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Konsole";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(7, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(119, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Konsole einblenden";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.java_perm_gen);
            this.groupBox2.Controls.Add(this.java_max_mem);
            this.groupBox2.Controls.Add(this.java_min_mem);
            this.groupBox2.Location = new System.Drawing.Point(12, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(328, 103);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Java Arbeitspeicher";
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
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Maximaler Arbeitspeicher:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Minimaler Arbeitspeicher:";
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
            // 
            // frm_settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 204);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private NumericUpDownEx java_perm_gen;
        private NumericUpDownEx java_max_mem;
        private NumericUpDownEx java_min_mem;
    }
}