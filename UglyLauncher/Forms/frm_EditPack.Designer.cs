namespace UglyLauncher
{
    partial class frm_EditPack
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_EditPack));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_enable_selected = new System.Windows.Forms.Button();
            this.btn_enable_all = new System.Windows.Forms.Button();
            this.btn_disable_all = new System.Windows.Forms.Button();
            this.btn_disable_selected = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.lst_availble = new UglyLauncher.ToolTipListBox();
            this.lst_enabled = new UglyLauncher.ToolTipListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(105, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "verfügbare Mods";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(533, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Aktivierte Mods";
            // 
            // btn_enable_selected
            // 
            this.btn_enable_selected.Location = new System.Drawing.Point(351, 117);
            this.btn_enable_selected.Name = "btn_enable_selected";
            this.btn_enable_selected.Size = new System.Drawing.Size(75, 23);
            this.btn_enable_selected.TabIndex = 4;
            this.btn_enable_selected.Text = ">";
            this.btn_enable_selected.UseVisualStyleBackColor = true;
            this.btn_enable_selected.Click += new System.EventHandler(this.btn_enable_selected_Click);
            // 
            // btn_enable_all
            // 
            this.btn_enable_all.Enabled = false;
            this.btn_enable_all.Location = new System.Drawing.Point(351, 146);
            this.btn_enable_all.Name = "btn_enable_all";
            this.btn_enable_all.Size = new System.Drawing.Size(75, 23);
            this.btn_enable_all.TabIndex = 5;
            this.btn_enable_all.Text = ">>";
            this.btn_enable_all.UseVisualStyleBackColor = true;
            this.btn_enable_all.Click += new System.EventHandler(this.btn_enable_all_Click);
            // 
            // btn_disable_all
            // 
            this.btn_disable_all.Enabled = false;
            this.btn_disable_all.Location = new System.Drawing.Point(351, 197);
            this.btn_disable_all.Name = "btn_disable_all";
            this.btn_disable_all.Size = new System.Drawing.Size(75, 23);
            this.btn_disable_all.TabIndex = 6;
            this.btn_disable_all.Text = "<<";
            this.btn_disable_all.UseVisualStyleBackColor = true;
            this.btn_disable_all.Click += new System.EventHandler(this.btn_disable_all_Click);
            // 
            // btn_disable_selected
            // 
            this.btn_disable_selected.Location = new System.Drawing.Point(351, 226);
            this.btn_disable_selected.Name = "btn_disable_selected";
            this.btn_disable_selected.Size = new System.Drawing.Size(75, 23);
            this.btn_disable_selected.TabIndex = 7;
            this.btn_disable_selected.Text = "<";
            this.btn_disable_selected.UseVisualStyleBackColor = true;
            this.btn_disable_selected.Click += new System.EventHandler(this.btn_disable_selected_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(351, 351);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 10;
            this.button6.Text = "Schließen";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // lst_availble
            // 
            this.lst_availble.DisplayMember = "DisplayText";
            this.lst_availble.FormattingEnabled = true;
            this.lst_availble.Location = new System.Drawing.Point(12, 32);
            this.lst_availble.Name = "lst_availble";
            this.lst_availble.Size = new System.Drawing.Size(333, 342);
            this.lst_availble.TabIndex = 11;
            // 
            // lst_enabled
            // 
            this.lst_enabled.DisplayMember = "DisplayText";
            this.lst_enabled.FormattingEnabled = true;
            this.lst_enabled.Location = new System.Drawing.Point(432, 32);
            this.lst_enabled.Name = "lst_enabled";
            this.lst_enabled.Size = new System.Drawing.Size(333, 342);
            this.lst_enabled.TabIndex = 8;
            // 
            // frm_EditPack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 383);
            this.Controls.Add(this.lst_availble);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.lst_enabled);
            this.Controls.Add(this.btn_disable_selected);
            this.Controls.Add(this.btn_disable_all);
            this.Controls.Add(this.btn_enable_all);
            this.Controls.Add(this.btn_enable_selected);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_EditPack";
            this.Text = "Pack bearbeiten";
            this.Shown += new System.EventHandler(this.frm_EditPack_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_enable_selected;
        private System.Windows.Forms.Button btn_enable_all;
        private System.Windows.Forms.Button btn_disable_all;
        private System.Windows.Forms.Button btn_disable_selected;
        private ToolTipListBox lst_enabled;
        private System.Windows.Forms.Button button6;
        private ToolTipListBox lst_availble;
    }
}