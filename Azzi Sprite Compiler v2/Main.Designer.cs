namespace Azzi_Sprite_Compiler_v2
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.consolelog = new System.Windows.Forms.TextBox();
            this.Configs = new System.Windows.Forms.Button();
            this.About = new System.Windows.Forms.Button();
            this.Donate = new System.Windows.Forms.Button();
            this.Compile = new System.Windows.Forms.Button();
            this.Extract = new System.Windows.Forms.Button();
            this.labelTempo = new System.Windows.Forms.Label();
            this.selectversion = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // consolelog
            // 
            this.consolelog.BackColor = System.Drawing.Color.Black;
            this.consolelog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.consolelog.ForeColor = System.Drawing.Color.Lime;
            this.consolelog.Location = new System.Drawing.Point(12, 12);
            this.consolelog.Multiline = true;
            this.consolelog.Name = "consolelog";
            this.consolelog.ReadOnly = true;
            this.consolelog.Size = new System.Drawing.Size(445, 237);
            this.consolelog.TabIndex = 90;
            this.consolelog.Text = "Welcome to Azzi Sprite Decompiler...";
            // 
            // Configs
            // 
            this.Configs.Location = new System.Drawing.Point(207, 255);
            this.Configs.Name = "Configs";
            this.Configs.Size = new System.Drawing.Size(59, 21);
            this.Configs.TabIndex = 15;
            this.Configs.Text = "Config";
            this.Configs.UseVisualStyleBackColor = true;
            // 
            // About
            // 
            this.About.Location = new System.Drawing.Point(269, 255);
            this.About.Name = "About";
            this.About.Size = new System.Drawing.Size(59, 21);
            this.About.TabIndex = 16;
            this.About.Text = "About";
            this.About.UseVisualStyleBackColor = true;
            this.About.Click += new System.EventHandler(this.About_Click);
            // 
            // Donate
            // 
            this.Donate.Location = new System.Drawing.Point(142, 255);
            this.Donate.Name = "Donate";
            this.Donate.Size = new System.Drawing.Size(59, 21);
            this.Donate.TabIndex = 14;
            this.Donate.Text = "Donate";
            this.Donate.UseVisualStyleBackColor = true;
            this.Donate.Click += new System.EventHandler(this.Donate_Click);
            // 
            // Compile
            // 
            this.Compile.Location = new System.Drawing.Point(77, 255);
            this.Compile.Name = "Compile";
            this.Compile.Size = new System.Drawing.Size(59, 21);
            this.Compile.TabIndex = 13;
            this.Compile.Text = "Compile";
            this.Compile.UseVisualStyleBackColor = true;
            this.Compile.Click += new System.EventHandler(this.Compile_Click);
            // 
            // Extract
            // 
            this.Extract.Location = new System.Drawing.Point(12, 255);
            this.Extract.Name = "Extract";
            this.Extract.Size = new System.Drawing.Size(59, 21);
            this.Extract.TabIndex = 12;
            this.Extract.Text = "Extract";
            this.Extract.UseVisualStyleBackColor = true;
            this.Extract.Click += new System.EventHandler(this.Extract_Click);
            // 
            // labelTempo
            // 
            this.labelTempo.AutoSize = true;
            this.labelTempo.Location = new System.Drawing.Point(422, 259);
            this.labelTempo.Name = "labelTempo";
            this.labelTempo.Size = new System.Drawing.Size(34, 13);
            this.labelTempo.TabIndex = 17;
            this.labelTempo.Text = "00:00";
            // 
            // selectversion
            // 
            this.selectversion.BackColor = System.Drawing.SystemColors.Window;
            this.selectversion.DisplayMember = "1";
            this.selectversion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectversion.FormattingEnabled = true;
            this.selectversion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.selectversion.Items.AddRange(new object[] {
            "9.6 to ...",
            "7.0 to 9.5",
            "3.1 to 6.5"});
            this.selectversion.Location = new System.Drawing.Point(334, 254);
            this.selectversion.Name = "selectversion";
            this.selectversion.Size = new System.Drawing.Size(78, 21);
            this.selectversion.TabIndex = 18;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 284);
            this.Controls.Add(this.selectversion);
            this.Controls.Add(this.labelTempo);
            this.Controls.Add(this.Configs);
            this.Controls.Add(this.About);
            this.Controls.Add(this.Donate);
            this.Controls.Add(this.Compile);
            this.Controls.Add(this.Extract);
            this.Controls.Add(this.consolelog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Main";
            this.Text = "Azzi Sprite Decompiler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox consolelog;
        private System.Windows.Forms.Button Configs;
        private System.Windows.Forms.Button About;
        private System.Windows.Forms.Button Donate;
        private System.Windows.Forms.Button Compile;
        private System.Windows.Forms.Button Extract;
        private System.Windows.Forms.Label labelTempo;
        private System.Windows.Forms.ComboBox selectversion;
    }
}

