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
            components = new System.ComponentModel.Container();
            ScrollablePanel = new System.Windows.Forms.Panel();
            jvmarg_box = new System.Windows.Forms.TextBox();
            mcarg_box = new System.Windows.Forms.TextBox();
            jvmarg_label = new System.Windows.Forms.Label();
            mcarg_label = new System.Windows.Forms.Label();
            gc_box = new System.Windows.Forms.ComboBox();
            gc_label = new System.Windows.Forms.Label();
            mib_label = new System.Windows.Forms.Label();
            ram_box = new System.Windows.Forms.NumericUpDown();
            ram_bar = new System.Windows.Forms.TrackBar();
            ram_label = new System.Windows.Forms.Label();
            x_label = new System.Windows.Forms.Label();
            resy_box = new System.Windows.Forms.NumericUpDown();
            resx_box = new System.Windows.Forms.NumericUpDown();
            res_label = new System.Windows.Forms.Label();
            gamedir_button = new System.Windows.Forms.Button();
            gamedir_box = new System.Windows.Forms.TextBox();
            gamedir_label = new System.Windows.Forms.Label();
            filter_button = new System.Windows.Forms.Button();
            version_box = new System.Windows.Forms.ComboBox();
            version_label = new System.Windows.Forms.Label();
            name_box = new System.Windows.Forms.TextBox();
            instname_label = new System.Windows.Forms.Label();
            cancel = new System.Windows.Forms.Button();
            save = new System.Windows.Forms.Button();
            filter_menu = new System.Windows.Forms.ContextMenuStrip(components);
            installed_only = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            show_snapshots = new System.Windows.Forms.ToolStripMenuItem();
            ScrollablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(ram_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(ram_bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(resy_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(resx_box)).BeginInit();
            filter_menu.SuspendLayout();
            SuspendLayout();
            // 
            // ScrollablePanel
            // 
            ScrollablePanel.AutoScroll = true;
            ScrollablePanel.Controls.Add(jvmarg_box);
            ScrollablePanel.Controls.Add(mcarg_box);
            ScrollablePanel.Controls.Add(jvmarg_label);
            ScrollablePanel.Controls.Add(mcarg_label);
            ScrollablePanel.Controls.Add(gc_box);
            ScrollablePanel.Controls.Add(gc_label);
            ScrollablePanel.Controls.Add(mib_label);
            ScrollablePanel.Controls.Add(ram_box);
            ScrollablePanel.Controls.Add(ram_bar);
            ScrollablePanel.Controls.Add(ram_label);
            ScrollablePanel.Controls.Add(x_label);
            ScrollablePanel.Controls.Add(resy_box);
            ScrollablePanel.Controls.Add(resx_box);
            ScrollablePanel.Controls.Add(res_label);
            ScrollablePanel.Controls.Add(gamedir_button);
            ScrollablePanel.Controls.Add(gamedir_box);
            ScrollablePanel.Controls.Add(gamedir_label);
            ScrollablePanel.Controls.Add(filter_button);
            ScrollablePanel.Controls.Add(version_box);
            ScrollablePanel.Controls.Add(version_label);
            ScrollablePanel.Controls.Add(name_box);
            ScrollablePanel.Controls.Add(instname_label);
            ScrollablePanel.Location = new System.Drawing.Point(0, 0);
            ScrollablePanel.Name = "ScrollablePanel";
            ScrollablePanel.Size = new System.Drawing.Size(320, 200);
            ScrollablePanel.TabIndex = 2;
            // 
            // jvmarg_box
            // 
            jvmarg_box.Location = new System.Drawing.Point(10, 329);
            jvmarg_box.Name = "jvmarg_box";
            jvmarg_box.Size = new System.Drawing.Size(286, 20);
            jvmarg_box.TabIndex = 22;
            // 
            // mcarg_box
            // 
            mcarg_box.Location = new System.Drawing.Point(10, 289);
            mcarg_box.Name = "mcarg_box";
            mcarg_box.Size = new System.Drawing.Size(290, 20);
            mcarg_box.TabIndex = 20;
            // 
            // jvmarg_label
            // 
            jvmarg_label.Location = new System.Drawing.Point(10, 314);
            jvmarg_label.Name = "jvmarg_label";
            jvmarg_label.Size = new System.Drawing.Size(284, 15);
            jvmarg_label.TabIndex = 21;
            jvmarg_label.Text = "Java Arguments";
            jvmarg_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mcarg_label
            // 
            mcarg_label.Location = new System.Drawing.Point(10, 274);
            mcarg_label.Name = "mcarg_label";
            mcarg_label.Size = new System.Drawing.Size(284, 15);
            mcarg_label.TabIndex = 19;
            mcarg_label.Text = "Minecraft Arguments";
            mcarg_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gc_box
            // 
            gc_box.DisplayMember = "name";
            gc_box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            gc_box.FormattingEnabled = true;
            gc_box.Location = new System.Drawing.Point(10, 248);
            gc_box.Name = "gc_box";
            gc_box.Size = new System.Drawing.Size(290, 21);
            gc_box.TabIndex = 18;
            // 
            // gc_label
            // 
            gc_label.Location = new System.Drawing.Point(13, 232);
            gc_label.Name = "gc_label";
            gc_label.Size = new System.Drawing.Size(284, 15);
            gc_label.TabIndex = 17;
            gc_label.Text = "Garbage Collector Flags";
            gc_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mib_label
            // 
            mib_label.Location = new System.Drawing.Point(195, 207);
            mib_label.Name = "mib_label";
            mib_label.Size = new System.Drawing.Size(30, 20);
            mib_label.TabIndex = 16;
            mib_label.Text = "MiB";
            mib_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ram_box
            // 
            ram_box.Location = new System.Drawing.Point(120, 207);
            ram_box.Name = "ram_box";
            ram_box.Size = new System.Drawing.Size(75, 20);
            ram_box.TabIndex = 15;
            ram_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            ram_box.ValueChanged += new System.EventHandler(ram_box_ValueChanged);
            // 
            // ram_bar
            // 
            ram_bar.AutoSize = false;
            ram_bar.LargeChange = 512;
            ram_bar.Location = new System.Drawing.Point(10, 187);
            ram_bar.Maximum = 131072;
            ram_bar.Name = "ram_bar";
            ram_bar.Size = new System.Drawing.Size(290, 20);
            ram_bar.SmallChange = 256;
            ram_bar.TabIndex = 14;
            ram_bar.TickFrequency = 1024;
            ram_bar.TickStyle = System.Windows.Forms.TickStyle.None;
            ram_bar.Scroll += new System.EventHandler(ram_bar_Scroll);
            // 
            // ram_label
            // 
            ram_label.Location = new System.Drawing.Point(10, 172);
            ram_label.Name = "ram_label";
            ram_label.Size = new System.Drawing.Size(290, 15);
            ram_label.TabIndex = 13;
            ram_label.Text = "Allocated Memory";
            ram_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // x_label
            // 
            x_label.Location = new System.Drawing.Point(145, 145);
            x_label.Name = "x_label";
            x_label.Size = new System.Drawing.Size(20, 20);
            x_label.TabIndex = 10;
            x_label.Text = "X";
            x_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // resy_box
            // 
            resy_box.Location = new System.Drawing.Point(170, 145);
            resy_box.Name = "resy_box";
            resy_box.Size = new System.Drawing.Size(130, 20);
            resy_box.TabIndex = 11;
            resy_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // resx_box
            // 
            resx_box.Location = new System.Drawing.Point(10, 145);
            resx_box.Name = "resx_box";
            resx_box.Size = new System.Drawing.Size(130, 20);
            resx_box.TabIndex = 9;
            resx_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // res_label
            // 
            res_label.Location = new System.Drawing.Point(10, 130);
            res_label.Name = "res_label";
            res_label.Size = new System.Drawing.Size(290, 15);
            res_label.TabIndex = 8;
            res_label.Text = "Resolution";
            res_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gamedir_button
            // 
            gamedir_button.BackColor = System.Drawing.Color.DimGray;
            gamedir_button.FlatAppearance.BorderSize = 0;
            gamedir_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            gamedir_button.Location = new System.Drawing.Point(230, 105);
            gamedir_button.Name = "gamedir_button";
            gamedir_button.Size = new System.Drawing.Size(70, 20);
            gamedir_button.TabIndex = 7;
            gamedir_button.Text = "Browse...";
            gamedir_button.UseVisualStyleBackColor = false;
            gamedir_button.Click += new System.EventHandler(gamedir_button_Click);
            // 
            // gamedir_box
            // 
            gamedir_box.Location = new System.Drawing.Point(10, 105);
            gamedir_box.Name = "gamedir_box";
            gamedir_box.Size = new System.Drawing.Size(215, 20);
            gamedir_box.TabIndex = 6;
            // 
            // gamedir_label
            // 
            gamedir_label.Location = new System.Drawing.Point(10, 90);
            gamedir_label.Name = "gamedir_label";
            gamedir_label.Size = new System.Drawing.Size(290, 15);
            gamedir_label.TabIndex = 5;
            gamedir_label.Text = "Game Folder";
            gamedir_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // filter_button
            // 
            filter_button.BackColor = System.Drawing.Color.DimGray;
            filter_button.FlatAppearance.BorderSize = 0;
            filter_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            filter_button.Image = global::Sandstone_Launcher.Properties.Resources.more;
            filter_button.Location = new System.Drawing.Point(275, 65);
            filter_button.Name = "filter_button";
            filter_button.Size = new System.Drawing.Size(25, 20);
            filter_button.TabIndex = 4;
            filter_button.UseVisualStyleBackColor = false;
            filter_button.Click += new System.EventHandler(filter_button_Click);
            // 
            // version_box
            // 
            version_box.DisplayMember = "Name";
            version_box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            version_box.FormattingEnabled = true;
            version_box.Location = new System.Drawing.Point(10, 65);
            version_box.Name = "version_box";
            version_box.Size = new System.Drawing.Size(260, 21);
            version_box.TabIndex = 3;
            // 
            // version_label
            // 
            version_label.Location = new System.Drawing.Point(10, 50);
            version_label.Name = "version_label";
            version_label.Size = new System.Drawing.Size(290, 15);
            version_label.TabIndex = 2;
            version_label.Text = "Game Version";
            version_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // name_box
            // 
            name_box.Location = new System.Drawing.Point(10, 25);
            name_box.Name = "name_box";
            name_box.Size = new System.Drawing.Size(290, 20);
            name_box.TabIndex = 1;
            // 
            // instname_label
            // 
            instname_label.Location = new System.Drawing.Point(10, 10);
            instname_label.Name = "instname_label";
            instname_label.Size = new System.Drawing.Size(290, 15);
            instname_label.TabIndex = 0;
            instname_label.Text = "Instance Name";
            instname_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cancel
            // 
            cancel.BackColor = System.Drawing.Color.DimGray;
            cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancel.FlatAppearance.BorderSize = 0;
            cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cancel.Location = new System.Drawing.Point(5, 230);
            cancel.Name = "cancel";
            cancel.Size = new System.Drawing.Size(310, 23);
            cancel.TabIndex = 0;
            cancel.Text = "Cancel";
            cancel.UseVisualStyleBackColor = false;
            // 
            // save
            // 
            save.BackColor = System.Drawing.Color.DimGray;
            save.FlatAppearance.BorderSize = 0;
            save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            save.Location = new System.Drawing.Point(5, 205);
            save.Name = "save";
            save.Size = new System.Drawing.Size(310, 23);
            save.TabIndex = 1;
            save.Text = "Save";
            save.UseVisualStyleBackColor = false;
            save.Click += new System.EventHandler(save_Click);
            // 
            // filter_menu
            // 
            filter_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            installed_only,
            toolStripSeparator1,
            show_snapshots});
            filter_menu.Name = "filter_menu";
            filter_menu.Size = new System.Drawing.Size(161, 54);
            // 
            // installed_only
            // 
            installed_only.CheckOnClick = true;
            installed_only.Name = "installed_only";
            installed_only.Size = new System.Drawing.Size(160, 22);
            installed_only.Text = "Installed Only";
            installed_only.CheckedChanged += new System.EventHandler(installed_only_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // show_snapshots
            // 
            show_snapshots.CheckOnClick = true;
            show_snapshots.Name = "show_snapshots";
            show_snapshots.Size = new System.Drawing.Size(160, 22);
            show_snapshots.Text = "Show Snapshots";
            show_snapshots.CheckedChanged += new System.EventHandler(show_snapshots_CheckedChanged);
            // 
            // InstanceDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            ClientSize = new System.Drawing.Size(320, 260);
            Controls.Add(save);
            Controls.Add(cancel);
            Controls.Add(ScrollablePanel);
            ForeColor = System.Drawing.Color.White;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = global::Sandstone_Launcher.Properties.Resources.sandstone;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InstanceDialog";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Instance Action";
            ScrollablePanel.ResumeLayout(false);
            ScrollablePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(ram_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(ram_bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(resy_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(resx_box)).EndInit();
            filter_menu.ResumeLayout(false);
            ResumeLayout(false);

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
    }
}