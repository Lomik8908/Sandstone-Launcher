using System.Windows.Forms;

namespace Sandstone_Launcher.Misc
{
    public partial class Ely2Step : Form
    {
        public Label tfa_label;
        public Button cancel;
        public TextBox tfa_box;

        public Ely2Step()
        {
            InitializeComponent();
            Text = Program.Lang?.tfa ?? "2-Factor Authentication";
            tfa_label.Text = Program.Lang?.enter2fa ?? "Enter the 2FA code below";
            DarkModeTitle.SetDarkMode(Handle, true);
        }

        private void InitializeComponent()
        {
            this.tfa_label = new System.Windows.Forms.Label();
            this.tfa_box = new System.Windows.Forms.TextBox();
            this.cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tfa_label
            // 
            this.tfa_label.Location = new System.Drawing.Point(10, 10);
            this.tfa_label.Name = "tfa_label";
            this.tfa_label.Size = new System.Drawing.Size(240, 15);
            this.tfa_label.TabIndex = 1;
            this.tfa_label.Text = "Enter the 2FA code below";
            this.tfa_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tfa_box
            // 
            this.tfa_box.Location = new System.Drawing.Point(10, 30);
            this.tfa_box.Name = "tfa_box";
            this.tfa_box.Size = new System.Drawing.Size(240, 20);
            this.tfa_box.TabIndex = 2;
            this.tfa_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cancel
            // 
            this.cancel.BackColor = System.Drawing.Color.DimGray;
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.FlatAppearance.BorderSize = 0;
            this.cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancel.Location = new System.Drawing.Point(10, 55);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(240, 23);
            this.cancel.TabIndex = 6;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = false;
            // 
            // Ely2Step
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(260, 90);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.tfa_box);
            this.Controls.Add(this.tfa_label);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Sandstone_Launcher.Properties.Resources.sandstone;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ely2Step";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "2-Factor Authentication";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
