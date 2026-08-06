using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Sandstone_Launcher.Misc
{
    public partial class ElyFlow : Form
    {
        public Button login;
        public Label user_label;
        public TextBox username;
        public TextBox password;
        public Button cancel;
        private LinkLabel register_button;
        public Label pass_label;

        public ElyFlow()
        {
            InitializeComponent();
            user_label.Text = Program.Lang?.username ?? "Username";
            pass_label.Text = Program.Lang?.password ?? "Password";
            login.Text = Program.Lang?.login ?? "Login";
            cancel.Text = Program.Lang?.cancel ?? "Cancel";
            register_button.Text = Program.Lang?.register ?? "You don't have an account? Register one!";
        }

        private void InitializeComponent()
        {
            this.login = new System.Windows.Forms.Button();
            this.user_label = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.pass_label = new System.Windows.Forms.Label();
            this.cancel = new System.Windows.Forms.Button();
            this.register_button = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // login
            // 
            this.login.BackColor = System.Drawing.Color.DimGray;
            this.login.FlatAppearance.BorderSize = 0;
            this.login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.login.Location = new System.Drawing.Point(10, 105);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(280, 23);
            this.login.TabIndex = 0;
            this.login.Text = "Login";
            this.login.UseVisualStyleBackColor = false;
            this.login.Click += new System.EventHandler(this.login_Click);
            // 
            // user_label
            // 
            this.user_label.Location = new System.Drawing.Point(10, 10);
            this.user_label.Name = "user_label";
            this.user_label.Size = new System.Drawing.Size(280, 15);
            this.user_label.TabIndex = 1;
            this.user_label.Text = "Username";
            this.user_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(10, 25);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(280, 20);
            this.username.TabIndex = 2;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(10, 65);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(280, 20);
            this.password.TabIndex = 4;
            this.password.UseSystemPasswordChar = true;
            // 
            // pass_label
            // 
            this.pass_label.Location = new System.Drawing.Point(10, 50);
            this.pass_label.Name = "pass_label";
            this.pass_label.Size = new System.Drawing.Size(280, 15);
            this.pass_label.TabIndex = 3;
            this.pass_label.Text = "Password";
            this.pass_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cancel
            // 
            this.cancel.BackColor = System.Drawing.Color.DimGray;
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.FlatAppearance.BorderSize = 0;
            this.cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancel.Location = new System.Drawing.Point(10, 130);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(280, 23);
            this.cancel.TabIndex = 5;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = false;
            // 
            // register_button
            // 
            this.register_button.ActiveLinkColor = System.Drawing.Color.Lime;
            this.register_button.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.register_button.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.register_button.Location = new System.Drawing.Point(10, 85);
            this.register_button.Name = "register_button";
            this.register_button.Size = new System.Drawing.Size(280, 20);
            this.register_button.TabIndex = 6;
            this.register_button.TabStop = true;
            this.register_button.Text = "You don\'t have an account? Register one!";
            this.register_button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.register_button.VisitedLinkColor = System.Drawing.Color.Aqua;
            this.register_button.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.register_button_LinkClicked);
            // 
            // ElyFlow
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(300, 165);
            this.Controls.Add(this.register_button);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.password);
            this.Controls.Add(this.pass_label);
            this.Controls.Add(this.username);
            this.Controls.Add(this.user_label);
            this.Controls.Add(this.login);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Sandstone_Launcher.Properties.Resources.sandstone;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ElyFlow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Login into Ely.by";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void login_Click(object sender, EventArgs e)
        {
            if (username.Text.Length > 0 && password.Text.Length > 0)
                DialogResult = DialogResult.OK;
            else
                MessageBox.Show(Program.Lang?.type_userpass ?? "Type in your username and password!", "Sandstone Launcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void register_button_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://account.ely.by/register");
        }
    }
}
