namespace Sandstone_Launcher
{
    partial class InstanceDialog
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
            this.components = new System.ComponentModel.Container();
            this.ScrollablePanel = new System.Windows.Forms.Panel();
            this.javalist_btn = new System.Windows.Forms.Button();
            this.jre_button = new System.Windows.Forms.Button();
            this.jre_label = new System.Windows.Forms.Label();
            this.jre_box = new System.Windows.Forms.TextBox();
            this.jvmarg_box = new System.Windows.Forms.TextBox();
            this.mcarg_box = new System.Windows.Forms.TextBox();
            this.jvmarg_label = new System.Windows.Forms.Label();
            this.mcarg_label = new System.Windows.Forms.Label();
            this.gc_box = new System.Windows.Forms.ComboBox();
            this.gc_label = new System.Windows.Forms.Label();
            this.mib_label = new System.Windows.Forms.Label();
            this.ram_box = new System.Windows.Forms.NumericUpDown();
            this.ram_bar = new System.Windows.Forms.TrackBar();
            this.ram_label = new System.Windows.Forms.Label();
            this.x_label = new System.Windows.Forms.Label();
            this.resy_box = new System.Windows.Forms.NumericUpDown();
            this.resx_box = new System.Windows.Forms.NumericUpDown();
            this.res_label = new System.Windows.Forms.Label();
            this.gamedir_button = new System.Windows.Forms.Button();
            this.gamedir_box = new System.Windows.Forms.TextBox();
            this.gamedir_label = new System.Windows.Forms.Label();
            this.filter_button = new System.Windows.Forms.Button();
            this.version_box = new System.Windows.Forms.ComboBox();
            this.version_label = new System.Windows.Forms.Label();
            this.name_box = new System.Windows.Forms.TextBox();
            this.instname_label = new System.Windows.Forms.Label();
            this.cancel = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.filter_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.installed_only = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.show_snapshots = new System.Windows.Forms.ToolStripMenuItem();
            this.predown = new System.Windows.Forms.CheckBox();
            this.java_ver = new System.Windows.Forms.Label();
            this.ScrollablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ram_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ram_bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resy_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resx_box)).BeginInit();
            this.filter_menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScrollablePanel
            // 
            this.ScrollablePanel.AutoScroll = true;
            this.ScrollablePanel.Controls.Add(this.java_ver);
            this.ScrollablePanel.Controls.Add(this.javalist_btn);
            this.ScrollablePanel.Controls.Add(this.jre_button);
            this.ScrollablePanel.Controls.Add(this.jre_label);
            this.ScrollablePanel.Controls.Add(this.jre_box);
            this.ScrollablePanel.Controls.Add(this.jvmarg_box);
            this.ScrollablePanel.Controls.Add(this.mcarg_box);
            this.ScrollablePanel.Controls.Add(this.jvmarg_label);
            this.ScrollablePanel.Controls.Add(this.mcarg_label);
            this.ScrollablePanel.Controls.Add(this.gc_box);
            this.ScrollablePanel.Controls.Add(this.gc_label);
            this.ScrollablePanel.Controls.Add(this.mib_label);
            this.ScrollablePanel.Controls.Add(this.ram_box);
            this.ScrollablePanel.Controls.Add(this.ram_bar);
            this.ScrollablePanel.Controls.Add(this.ram_label);
            this.ScrollablePanel.Controls.Add(this.x_label);
            this.ScrollablePanel.Controls.Add(this.resy_box);
            this.ScrollablePanel.Controls.Add(this.resx_box);
            this.ScrollablePanel.Controls.Add(this.res_label);
            this.ScrollablePanel.Controls.Add(this.gamedir_button);
            this.ScrollablePanel.Controls.Add(this.gamedir_box);
            this.ScrollablePanel.Controls.Add(this.gamedir_label);
            this.ScrollablePanel.Controls.Add(this.filter_button);
            this.ScrollablePanel.Controls.Add(this.version_box);
            this.ScrollablePanel.Controls.Add(this.version_label);
            this.ScrollablePanel.Controls.Add(this.name_box);
            this.ScrollablePanel.Controls.Add(this.instname_label);
            this.ScrollablePanel.Location = new System.Drawing.Point(0, 0);
            this.ScrollablePanel.Name = "ScrollablePanel";
            this.ScrollablePanel.Size = new System.Drawing.Size(320, 200);
            this.ScrollablePanel.TabIndex = 2;
            // 
            // javalist_btn
            // 
            this.javalist_btn.BackColor = System.Drawing.Color.DimGray;
            this.javalist_btn.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.javalist_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.javalist_btn.Location = new System.Drawing.Point(10, 394);
            this.javalist_btn.Name = "javalist_btn";
            this.javalist_btn.Size = new System.Drawing.Size(290, 25);
            this.javalist_btn.TabIndex = 26;
            this.javalist_btn.Text = "Open Java List";
            this.javalist_btn.UseVisualStyleBackColor = false;
            this.javalist_btn.Click += new System.EventHandler(this.javalist_btn_Click);
            // 
            // jre_button
            // 
            this.jre_button.BackColor = System.Drawing.Color.DimGray;
            this.jre_button.FlatAppearance.BorderSize = 0;
            this.jre_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.jre_button.Location = new System.Drawing.Point(230, 369);
            this.jre_button.Name = "jre_button";
            this.jre_button.Size = new System.Drawing.Size(70, 20);
            this.jre_button.TabIndex = 25;
            this.jre_button.Text = "Browse...";
            this.jre_button.UseVisualStyleBackColor = false;
            this.jre_button.Click += new System.EventHandler(this.jre_button_Click);
            // 
            // jre_label
            // 
            this.jre_label.Location = new System.Drawing.Point(9, 354);
            this.jre_label.Name = "jre_label";
            this.jre_label.Size = new System.Drawing.Size(290, 15);
            this.jre_label.TabIndex = 24;
            this.jre_label.Text = "Custom Runtime";
            this.jre_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // jre_box
            // 
            this.jre_box.BackColor = System.Drawing.Color.DimGray;
            this.jre_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.jre_box.ForeColor = System.Drawing.Color.White;
            this.jre_box.Location = new System.Drawing.Point(10, 369);
            this.jre_box.Name = "jre_box";
            this.jre_box.Size = new System.Drawing.Size(217, 20);
            this.jre_box.TabIndex = 23;
            // 
            // jvmarg_box
            // 
            this.jvmarg_box.BackColor = System.Drawing.Color.DimGray;
            this.jvmarg_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.jvmarg_box.ForeColor = System.Drawing.Color.White;
            this.jvmarg_box.Location = new System.Drawing.Point(10, 329);
            this.jvmarg_box.Name = "jvmarg_box";
            this.jvmarg_box.Size = new System.Drawing.Size(292, 20);
            this.jvmarg_box.TabIndex = 22;
            // 
            // mcarg_box
            // 
            this.mcarg_box.BackColor = System.Drawing.Color.DimGray;
            this.mcarg_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mcarg_box.ForeColor = System.Drawing.Color.White;
            this.mcarg_box.Location = new System.Drawing.Point(10, 289);
            this.mcarg_box.Name = "mcarg_box";
            this.mcarg_box.Size = new System.Drawing.Size(290, 20);
            this.mcarg_box.TabIndex = 20;
            // 
            // jvmarg_label
            // 
            this.jvmarg_label.Location = new System.Drawing.Point(10, 314);
            this.jvmarg_label.Name = "jvmarg_label";
            this.jvmarg_label.Size = new System.Drawing.Size(290, 15);
            this.jvmarg_label.TabIndex = 21;
            this.jvmarg_label.Text = "Java Arguments";
            this.jvmarg_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mcarg_label
            // 
            this.mcarg_label.Location = new System.Drawing.Point(10, 274);
            this.mcarg_label.Name = "mcarg_label";
            this.mcarg_label.Size = new System.Drawing.Size(284, 15);
            this.mcarg_label.TabIndex = 19;
            this.mcarg_label.Text = "Minecraft Arguments";
            this.mcarg_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gc_box
            // 
            this.gc_box.BackColor = System.Drawing.Color.DimGray;
            this.gc_box.DisplayMember = "name";
            this.gc_box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gc_box.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gc_box.ForeColor = System.Drawing.Color.White;
            this.gc_box.FormattingEnabled = true;
            this.gc_box.Location = new System.Drawing.Point(10, 248);
            this.gc_box.Name = "gc_box";
            this.gc_box.Size = new System.Drawing.Size(290, 21);
            this.gc_box.TabIndex = 18;
            // 
            // gc_label
            // 
            this.gc_label.Location = new System.Drawing.Point(13, 232);
            this.gc_label.Name = "gc_label";
            this.gc_label.Size = new System.Drawing.Size(284, 15);
            this.gc_label.TabIndex = 17;
            this.gc_label.Text = "Garbage Collector Flags";
            this.gc_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mib_label
            // 
            this.mib_label.Location = new System.Drawing.Point(195, 207);
            this.mib_label.Name = "mib_label";
            this.mib_label.Size = new System.Drawing.Size(30, 20);
            this.mib_label.TabIndex = 16;
            this.mib_label.Text = "MiB";
            this.mib_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ram_box
            // 
            this.ram_box.Location = new System.Drawing.Point(120, 207);
            this.ram_box.Name = "ram_box";
            this.ram_box.Size = new System.Drawing.Size(75, 20);
            this.ram_box.TabIndex = 15;
            this.ram_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ram_box.ValueChanged += new System.EventHandler(this.ram_box_ValueChanged);
            // 
            // ram_bar
            // 
            this.ram_bar.AutoSize = false;
            this.ram_bar.LargeChange = 512;
            this.ram_bar.Location = new System.Drawing.Point(10, 187);
            this.ram_bar.Maximum = 131072;
            this.ram_bar.Name = "ram_bar";
            this.ram_bar.Size = new System.Drawing.Size(290, 20);
            this.ram_bar.SmallChange = 256;
            this.ram_bar.TabIndex = 14;
            this.ram_bar.TickFrequency = 1024;
            this.ram_bar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ram_bar.Scroll += new System.EventHandler(this.ram_bar_Scroll);
            // 
            // ram_label
            // 
            this.ram_label.Location = new System.Drawing.Point(10, 172);
            this.ram_label.Name = "ram_label";
            this.ram_label.Size = new System.Drawing.Size(290, 15);
            this.ram_label.TabIndex = 13;
            this.ram_label.Text = "Allocated Memory";
            this.ram_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // x_label
            // 
            this.x_label.Location = new System.Drawing.Point(145, 145);
            this.x_label.Name = "x_label";
            this.x_label.Size = new System.Drawing.Size(20, 20);
            this.x_label.TabIndex = 10;
            this.x_label.Text = "X";
            this.x_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // resy_box
            // 
            this.resy_box.Location = new System.Drawing.Point(170, 145);
            this.resy_box.Name = "resy_box";
            this.resy_box.Size = new System.Drawing.Size(130, 20);
            this.resy_box.TabIndex = 11;
            this.resy_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // resx_box
            // 
            this.resx_box.Location = new System.Drawing.Point(10, 145);
            this.resx_box.Name = "resx_box";
            this.resx_box.Size = new System.Drawing.Size(130, 20);
            this.resx_box.TabIndex = 9;
            this.resx_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // res_label
            // 
            this.res_label.Location = new System.Drawing.Point(10, 130);
            this.res_label.Name = "res_label";
            this.res_label.Size = new System.Drawing.Size(290, 15);
            this.res_label.TabIndex = 8;
            this.res_label.Text = "Resolution";
            this.res_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gamedir_button
            // 
            this.gamedir_button.BackColor = System.Drawing.Color.DimGray;
            this.gamedir_button.FlatAppearance.BorderSize = 0;
            this.gamedir_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gamedir_button.Location = new System.Drawing.Point(230, 105);
            this.gamedir_button.Name = "gamedir_button";
            this.gamedir_button.Size = new System.Drawing.Size(70, 20);
            this.gamedir_button.TabIndex = 7;
            this.gamedir_button.Text = "Browse...";
            this.gamedir_button.UseVisualStyleBackColor = false;
            this.gamedir_button.Click += new System.EventHandler(this.gamedir_button_Click);
            // 
            // gamedir_box
            // 
            this.gamedir_box.BackColor = System.Drawing.Color.DimGray;
            this.gamedir_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gamedir_box.ForeColor = System.Drawing.Color.White;
            this.gamedir_box.Location = new System.Drawing.Point(10, 105);
            this.gamedir_box.Name = "gamedir_box";
            this.gamedir_box.Size = new System.Drawing.Size(215, 20);
            this.gamedir_box.TabIndex = 6;
            // 
            // gamedir_label
            // 
            this.gamedir_label.Location = new System.Drawing.Point(10, 90);
            this.gamedir_label.Name = "gamedir_label";
            this.gamedir_label.Size = new System.Drawing.Size(290, 15);
            this.gamedir_label.TabIndex = 5;
            this.gamedir_label.Text = "Game Folder";
            this.gamedir_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // filter_button
            // 
            this.filter_button.BackColor = System.Drawing.Color.DimGray;
            this.filter_button.FlatAppearance.BorderSize = 0;
            this.filter_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filter_button.Image = global::Sandstone_Launcher.Properties.Resources.more;
            this.filter_button.Location = new System.Drawing.Point(275, 65);
            this.filter_button.Name = "filter_button";
            this.filter_button.Size = new System.Drawing.Size(25, 20);
            this.filter_button.TabIndex = 4;
            this.filter_button.UseVisualStyleBackColor = false;
            this.filter_button.Click += new System.EventHandler(this.filter_button_Click);
            // 
            // version_box
            // 
            this.version_box.BackColor = System.Drawing.Color.DimGray;
            this.version_box.DisplayMember = "Name";
            this.version_box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.version_box.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.version_box.ForeColor = System.Drawing.Color.White;
            this.version_box.FormattingEnabled = true;
            this.version_box.Location = new System.Drawing.Point(10, 65);
            this.version_box.Name = "version_box";
            this.version_box.Size = new System.Drawing.Size(260, 21);
            this.version_box.TabIndex = 3;
            // 
            // version_label
            // 
            this.version_label.Location = new System.Drawing.Point(10, 50);
            this.version_label.Name = "version_label";
            this.version_label.Size = new System.Drawing.Size(290, 15);
            this.version_label.TabIndex = 2;
            this.version_label.Text = "Game Version";
            this.version_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // name_box
            // 
            this.name_box.BackColor = System.Drawing.Color.DimGray;
            this.name_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.name_box.ForeColor = System.Drawing.Color.White;
            this.name_box.Location = new System.Drawing.Point(10, 25);
            this.name_box.Name = "name_box";
            this.name_box.Size = new System.Drawing.Size(290, 20);
            this.name_box.TabIndex = 1;
            // 
            // instname_label
            // 
            this.instname_label.Location = new System.Drawing.Point(10, 10);
            this.instname_label.Name = "instname_label";
            this.instname_label.Size = new System.Drawing.Size(290, 15);
            this.instname_label.TabIndex = 0;
            this.instname_label.Text = "Instance Name";
            this.instname_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cancel
            // 
            this.cancel.BackColor = System.Drawing.Color.DimGray;
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancel.Location = new System.Drawing.Point(10, 225);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(145, 25);
            this.cancel.TabIndex = 0;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = false;
            // 
            // save
            // 
            this.save.BackColor = System.Drawing.Color.DimGray;
            this.save.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.save.Location = new System.Drawing.Point(165, 225);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(145, 25);
            this.save.TabIndex = 1;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = false;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // filter_menu
            // 
            this.filter_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.installed_only,
            this.toolStripSeparator1,
            this.show_snapshots});
            this.filter_menu.Name = "filter_menu";
            this.filter_menu.Size = new System.Drawing.Size(161, 54);
            // 
            // installed_only
            // 
            this.installed_only.CheckOnClick = true;
            this.installed_only.Name = "installed_only";
            this.installed_only.Size = new System.Drawing.Size(160, 22);
            this.installed_only.Text = "Installed Only";
            this.installed_only.CheckedChanged += new System.EventHandler(this.installed_only_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // show_snapshots
            // 
            this.show_snapshots.CheckOnClick = true;
            this.show_snapshots.Name = "show_snapshots";
            this.show_snapshots.Size = new System.Drawing.Size(160, 22);
            this.show_snapshots.Text = "Show Snapshots";
            this.show_snapshots.CheckedChanged += new System.EventHandler(this.show_snapshots_CheckedChanged);
            // 
            // predown
            // 
            this.predown.Location = new System.Drawing.Point(10, 205);
            this.predown.Name = "predown";
            this.predown.Size = new System.Drawing.Size(300, 19);
            this.predown.TabIndex = 3;
            this.predown.Text = "Predownload files";
            this.predown.UseVisualStyleBackColor = true;
            // 
            // java_ver
            // 
            this.java_ver.Location = new System.Drawing.Point(10, 424);
            this.java_ver.Name = "java_ver";
            this.java_ver.Size = new System.Drawing.Size(290, 13);
            this.java_ver.TabIndex = 24;
            this.java_ver.Text = "Version: Default";
            this.java_ver.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // InstanceDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(320, 260);
            this.Controls.Add(this.predown);
            this.Controls.Add(this.save);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ScrollablePanel);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Sandstone_Launcher.Properties.Resources.sandstone;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InstanceDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Instance Action";
            this.ScrollablePanel.ResumeLayout(false);
            this.ScrollablePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ram_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ram_bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resy_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resx_box)).EndInit();
            this.filter_menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ScrollablePanel;
        public System.Windows.Forms.TextBox name_box;
        public System.Windows.Forms.Label instname_label;
        public System.Windows.Forms.Button filter_button;
        public System.Windows.Forms.ComboBox version_box;
        public System.Windows.Forms.Label version_label;
        public System.Windows.Forms.Button gamedir_button;
        public System.Windows.Forms.Label gamedir_label;
        public System.Windows.Forms.Label res_label;
        public System.Windows.Forms.Label x_label;
        public System.Windows.Forms.NumericUpDown resy_box;
        public System.Windows.Forms.NumericUpDown resx_box;
        public System.Windows.Forms.TrackBar ram_bar;
        public System.Windows.Forms.Label ram_label;
        public System.Windows.Forms.Label mib_label;
        public System.Windows.Forms.TextBox jvmarg_box;
        public System.Windows.Forms.Button cancel;
        public System.Windows.Forms.Button save;
        public System.Windows.Forms.TextBox mcarg_box;
        public System.Windows.Forms.Label jvmarg_label;
        public System.Windows.Forms.Label mcarg_label;
        public System.Windows.Forms.ComboBox gc_box;
        public System.Windows.Forms.NumericUpDown ram_box;
        public System.Windows.Forms.Label gc_label;
        public System.Windows.Forms.TextBox gamedir_box;
        public System.Windows.Forms.ContextMenuStrip filter_menu;
        public System.Windows.Forms.ToolStripMenuItem installed_only;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripMenuItem show_snapshots;
        public System.Windows.Forms.CheckBox predown;
        public System.Windows.Forms.Label jre_label;
        public System.Windows.Forms.TextBox jre_box;
        public System.Windows.Forms.Button jre_button;
        private System.Windows.Forms.Label java_ver;
        public System.Windows.Forms.Button javalist_btn;
    }
}