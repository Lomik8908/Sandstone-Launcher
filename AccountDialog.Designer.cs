namespace Sandstone_Launcher
{
    partial class AccountDialog
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
            this.username_label = new System.Windows.Forms.Label();
            this.username_box = new System.Windows.Forms.TextBox();
            this.save = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.usertype_box = new System.Windows.Forms.ComboBox();
            this.usertype_label = new System.Windows.Forms.Label();
            this.login_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // username_label
            // 
            this.username_label.ForeColor = System.Drawing.Color.White;
            this.username_label.Location = new System.Drawing.Point(10, 50);
            this.username_label.Name = "username_label";
            this.username_label.Size = new System.Drawing.Size(270, 15);
            this.username_label.TabIndex = 37;
            this.username_label.Text = "Username";
            this.username_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // username_box
            // 
            this.username_box.Location = new System.Drawing.Point(10, 65);
            this.username_box.Name = "username_box";
            this.username_box.Size = new System.Drawing.Size(270, 20);
            this.username_box.TabIndex = 39;
            // 
            // save
            // 
            this.save.BackColor = System.Drawing.Color.DimGray;
            this.save.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.save.ForeColor = System.Drawing.Color.White;
            this.save.Location = new System.Drawing.Point(150, 90);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(130, 25);
            this.save.TabIndex = 38;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = false;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // cancel
            // 
            this.cancel.BackColor = System.Drawing.Color.DimGray;
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancel.ForeColor = System.Drawing.Color.White;
            this.cancel.Location = new System.Drawing.Point(10, 90);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(130, 25);
            this.cancel.TabIndex = 36;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = false;
            // 
            // usertype_box
            // 
            this.usertype_box.DisplayMember = "name";
            this.usertype_box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.usertype_box.FormattingEnabled = true;
            this.usertype_box.Location = new System.Drawing.Point(10, 25);
            this.usertype_box.Name = "usertype_box";
            this.usertype_box.Size = new System.Drawing.Size(270, 21);
            this.usertype_box.TabIndex = 40;
            this.usertype_box.SelectedIndexChanged += new System.EventHandler(this.usertype_box_SelectedIndexChanged);
            // 
            // usertype_label
            // 
            this.usertype_label.ForeColor = System.Drawing.Color.White;
            this.usertype_label.Location = new System.Drawing.Point(10, 10);
            this.usertype_label.Name = "usertype_label";
            this.usertype_label.Size = new System.Drawing.Size(270, 15);
            this.usertype_label.TabIndex = 41;
            this.usertype_label.Text = "User Type";
            this.usertype_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // login_button
            // 
            this.login_button.BackColor = System.Drawing.Color.DimGray;
            this.login_button.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.login_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.login_button.ForeColor = System.Drawing.Color.White;
            this.login_button.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.login_button.Location = new System.Drawing.Point(10, 50);
            this.login_button.Name = "login_button";
            this.login_button.Size = new System.Drawing.Size(270, 35);
            this.login_button.TabIndex = 42;
            this.login_button.Text = "Login via Microsoft";
            this.login_button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.login_button.UseVisualStyleBackColor = false;
            this.login_button.Click += new System.EventHandler(this.login_button_Click);
            // 
            // AccountDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(290, 125);
            this.Controls.Add(this.login_button);
            this.Controls.Add(this.usertype_label);
            this.Controls.Add(this.usertype_box);
            this.Controls.Add(this.save);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.username_box);
            this.Controls.Add(this.username_label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Sandstone_Launcher.Properties.Resources.sandstone;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Account Action";
            this.Shown += new System.EventHandler(this.AccountDialog_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label username_label;
        public System.Windows.Forms.TextBox username_box;
        public System.Windows.Forms.Button save;
        public System.Windows.Forms.Button cancel;
        public System.Windows.Forms.Label usertype_label;
        public System.Windows.Forms.Button login_button;
        public System.Windows.Forms.ComboBox usertype_box;
    }
}