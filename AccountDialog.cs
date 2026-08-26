using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Sandstone_Launcher
{
    public partial class AccountDialog : Form
    {
        private Language CurrentLang = Languages.DefaultLanguage;
        public AccountDialog()
        {
            InitializeComponent();
            usertype_box.DataSource = Accounts.accountTypes;
            login_button.ImageList = Program.homeWindow.AccountImages;
            DarkModeTitle.SetDarkMode(Handle, true);
        }

        public void SetLanguage(Language Lang, string Title = null)
        {
            if (!(Lang is Language)) return;
            username_label.Text = Lang.username;
            usertype_label.Text = Lang.user_type;
            save.Text = Lang.save;
            cancel.Text = Lang.cancel;

            usertype_box.DisplayMember = null;
            usertype_box.DisplayMember = "Name";

            Text = Title ?? Program.AppName;
            CurrentLang = Lang;

            AccountType AccType = (AccountType)usertype_box.SelectedItem;
            if (AccType?.id == "msa" || AccType?.id == "ely")
                login_button.Text = SharedMethods.ReplaceFormat(CurrentLang.login_into, AccType.name); // ?? "Login into {0}"
        }

        private void save_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(username_box.Text)) { MessageBox.Show(CurrentLang.make_username, Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information); return; } // ?? "Make a username!"
            if (username_box.Text.Length > 16 || !Regex.IsMatch(username_box.Text, "^[A-Za-z0-9_]+$"))
            {
                MessageBox.Show(CurrentLang.username_warn, Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);// ?? "While using this username you might encounter:\nGame Crashes\nServer Bugs\nUnexpected Behaviour\nUse this username at your own risk!"
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
                login_button.Text = SharedMethods.ReplaceFormat(CurrentLang.login_into, AccType.name); // ?? "Login into {0}"
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
    }
}
