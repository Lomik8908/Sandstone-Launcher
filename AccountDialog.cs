using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Sandstone_Launcher
{
    public partial class AccountDialog : Form
    {
        public AccountDialog()
        {
            InitializeComponent();
            usertype_box.DataSource = Accounts.accountTypes;
            login_button.ImageList = Program.homeWindow.AccountImages;
            DarkModeTitle.SetDarkMode(Handle, true);
        }

        private void save_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(username_box.Text)) { MessageBox.Show(Program.Lang?.make_username ?? "Make a username!", "Sandstone Launcher", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (username_box.Text.Length > 16 || !Regex.IsMatch(username_box.Text, "^[A-Za-z0-9_]+$"))
            {
                DialogResult Ask = MessageBox.Show(Program.Lang?.username_warn ?? "While using this username you might encounter:\nGame Crashes\nServer Bugs\nUnexpected Behaviour\nUse this username at your own risk!", "Sandstone Launcher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            DialogResult = DialogResult.OK;
        }

        private void usertype_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            AccountType AccType = (AccountType)usertype_box.SelectedItem;
            if (AccType?.id == "msa" || AccType?.id == "ely")
            {
                username_box.Visible = false;
                username_label.Visible = false;
                save.Enabled = false;
                login_button.Visible = true;
                login_button.ImageKey = AccType.id;
                login_button.Text = SharedMethods.ReplaceFormat(Program.Lang?.login_into ?? "Login into {0}", AccType.name);
            }
            else
            {
                username_box.Visible = true;
                username_label.Visible = true;
                save.Enabled = true;
                login_button.Visible = false;
            }
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            AccountType AccType = (AccountType)usertype_box.SelectedItem;
            if (AccType?.id == "msa")
                Accounts.MSBeginFlow();
            if (AccType?.id == "ely")
                Accounts.ElyBeginFlow();
            Close();
        }

        private void AccountDialog_Shown(object sender, EventArgs e)
        {
            if (!Program.HasMSAccount())
            {
                usertype_box.SelectedIndex = 0;
                usertype_box.Enabled = false;
            }
            else
            {
                usertype_box.Enabled = true;
            }
            usertype_box_SelectedIndexChanged(null, null);
        }
    }
}
