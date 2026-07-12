using SandstoneControls;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sandstone_Launcher
{
    public partial class HomeWindow : BackgroundForm
    {
        public static string JavaRegex = "^\\w+ version \"(.+)\"$";
        public ImageList AccountImages = new ImageList { ImageSize = new Size(24, 24), ColorDepth = ColorDepth.Depth32Bit };
        public ImageList InstanceImages = new ImageList { ImageSize = new Size(32, 32), ColorDepth = ColorDepth.Depth32Bit };
        public HomeWindow()
        {
            InitializeComponent();
            AccountImages.Images.Add("offline", Properties.Resources.key);
            AccountImages.Images.Add("ely", Properties.Resources.ely);
            AccountImages.Images.Add("msa", Properties.Resources.msa);
            InstanceImages.Images.Add("Grass", Properties.Resources.grass);

            OpenAbout.Text = $"SL {Program.AppVersionString}";

            account_box.DataSource = Program.Users;
            instance_box.DataSource = Program.Instances;
            lang_box.DataSource = Languages.AllLanguages;
            bg_box.DataSource = Backgrounds.AllBackgrounds;

            onlaunch_box.DataSource = new NameClass[] {
                Program.NamedClasses["onlaunch_hide"],
                Program.NamedClasses["onlaunch_close"],
                Program.NamedClasses["onlaunch_none"]
            };

            gc_box.Items.Add(Program.NamedClasses["none"]);
            ram_bar.Maximum = (int)(Program.pcInfo.TotalPhysicalMemory / (1024 * 1024));

            UpdateJavaLabel();

            instance_list.LargeImageList = InstanceImages;
            account_list.SmallImageList = AccountImages;
            instance_box.MouseWheel += SharedMethods.HandleScroll;
            account_box.MouseWheel += SharedMethods.HandleScroll;
            lang_box.MouseWheel += SharedMethods.HandleScroll;
            gc_box.MouseWheel += SharedMethods.HandleScroll;
            bg_box.MouseWheel += SharedMethods.HandleScroll;
            onlaunch_box.MouseWheel += SharedMethods.HandleScroll;
            ram_bar.MouseWheel += SharedMethods.HandleScroll;
            ram_box.MouseWheel += SharedMethods.HandleScroll;
            resx_box.MouseWheel += SharedMethods.HandleScroll;
            resy_box.MouseWheel += SharedMethods.HandleScroll;
            DarkModeTitle.SetDarkMode(Handle, true);
        }
        public void UpdateJavaLabel()
        {
            if (string.IsNullOrEmpty(Program.settings.java_type))
                java_ver.Text = SharedMethods.ReplaceFormat(Program.Lang?.java_ver ?? "Version: {0}", Program.NamedClasses["default"].Name);
            else if (Program.JavaIdToVersion.ContainsKey(Program.settings.java_type))
                java_ver.Text = SharedMethods.ReplaceFormat(Program.Lang?.java_ver ?? "Version: {0}", $"{Program.JavaIdToVersion[Program.settings.java_type]} ({Program.settings.java_type})");
            else
                java_ver.Text = SharedMethods.ReplaceFormat(Program.Lang?.java_ver ?? "Version: {0}", Program.settings.java_type);
        }
        public void OpenMenu(int Screen)
        {
            settings_box.Visible = Screen == 1;
            instances_box.Visible = Screen == 2;
            accounts_box.Visible = Screen == 3;
        }
        public string SelectFolder()
        {
            DialogResult result = GameDirBrowse.ShowDialog();
            if (result == DialogResult.OK)
                return GameDirBrowse.SelectedPath;
            return null;
        }
        public string SelectFile(string AllowedExt = "All Files (*.*)|*.*")
        {
            FileDialog.Filter = AllowedExt;
            DialogResult result = FileDialog.ShowDialog();
            if (result == DialogResult.OK)
                return FileDialog.FileName;
            return null;
        }

        private void more_Click(object sender, EventArgs e) => more_menu.Show(more, Point.Empty);
        private void ram_bar_Scroll(object sender, EventArgs e) => ram_box.Value = Math.Max(ram_box.Minimum, Math.Min(ram_bar.Value, ram_box.Maximum));
        private void ram_box_ValueChanged(object sender, EventArgs e) => ram_bar.Value = (int)Math.Max(ram_bar.Minimum, Math.Min(ram_box.Value, ram_bar.Maximum));
        private void sett_back_Click(object sender, EventArgs e) => OpenMenu(0);
        private void inst_back_Click(object sender, EventArgs e) => OpenMenu(0);
        private void accounts_back_Click(object sender, EventArgs e) => OpenMenu(0);
        private void OpenSettings_Click(object sender, EventArgs e) => OpenMenu(1);
        private void OpenInstances_Click(object sender, EventArgs e)
        {
            Program.LoadInstanceList();
            OpenMenu(2);
        }
        private void OpenAccounts_Click(object sender, EventArgs e)
        {
            Program.LoadUsersList();
            OpenMenu(3);
        }
        private void gamedir_box_Leave(object sender, EventArgs e) => Program.SetGameDir(gamedir_box.Text);
        private void gamedir_button_Click(object sender, EventArgs e)
        {
            string Path = SelectFolder();
            if (Path != null)
                Program.SetGameDir(Path);
        }
        private void launch_Click(object sender, EventArgs e) => Program.Launch();
        private void OpenFolder_Click(object sender, EventArgs e)
        {
            if (instance_box.SelectedItem is Instance inst && Directory.Exists(inst.gamedir))
                AskOpenFolder.Show(more, Point.Empty);
            else
                Process.Start("explorer", LauncherLib.GameDir);
        }
        private void open_minecraft_Click(object sender, EventArgs e) => Process.Start("explorer", LauncherLib.GameDir);
        private void open_instance_Click(object sender, EventArgs e)
        {
            if (instance_box.SelectedItem is Instance inst)
                Process.Start("explorer", inst.gamedir);
        }
        private void HomeWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.Launching) Program.StopLaunch();
            if (LauncherLib.OperationRunning) LauncherLib.StopOperation();
            Program.SaveAll();
        }
        private void account_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (account_list.Enabled)
            {
                User selectedUser = null;
                if (account_list.SelectedItems.Count > 0)
                    selectedUser = account_list.SelectedItems[0].Tag as User;
                account_edit.Enabled = selectedUser != null && selectedUser.usertype == "offline";
                account_delete.Enabled = selectedUser != null;
            }
        }
        private void account_delete_Click(object sender, EventArgs e)
        {
            User selectedUser = null;
            if (account_list.SelectedItems.Count > 0)
                selectedUser = account_list.SelectedItems[0].Tag as User;
            if (selectedUser != null)
            {
                DialogResult confirm = MessageBox.Show(SharedMethods.ReplaceFormat(Program.Lang?.del_user ?? "Remove account \"{0}\"?", selectedUser.username), "Sandstone Launcher", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (confirm == DialogResult.Yes)
                {
                    lock (Program.AccountLock)
                        Program.Users.Remove(selectedUser);
                    Program.LoadUsersList();
                    Program.SaveUsers();
                }
            }
        }
        private void account_edit_Click(object sender, EventArgs e)
        {
            User selectedUser = null;
            if (account_list.SelectedItems.Count > 0)
                selectedUser = account_list.SelectedItems[0].Tag as User;

            if (selectedUser != null)
            {
                Program.accountDialog.usertype_box.Enabled = false;
                Program.accountDialog.username_box.Text = selectedUser.username;
                Program.accountDialog.Text = Program.Lang?.edit_acc ?? "Editing an Account";
                DialogResult confirm = Program.accountDialog.ShowDialog();
                if (confirm == DialogResult.OK)
                {
                    selectedUser.username = Program.accountDialog.username_box.Text;
                    account_box.DataSource = null;
                    account_box.DataSource = Program.Users;
                    Program.LoadUsersList();
                    Program.SaveUsers();
                }
                Program.accountDialog.username_box.Text = "";
            }
        }
        private void account_add_Click(object sender, EventArgs e)
        {
            Program.accountDialog.usertype_box.Enabled = true;
            Program.accountDialog.Text = Program.Lang?.add_acc ?? "Adding an Account";
            DialogResult confirm = Program.accountDialog.ShowDialog();
            if (confirm == DialogResult.OK)
            {
                lock (Program.AccountLock)
                    Program.Users.Add(new User
                    {
                        username = Program.accountDialog.username_box.Text,
                        usertype = "offline",
                        accessToken = "0",
                        uuid = Guid.NewGuid().ToString()
                    });
                Program.LoadUsersList();
                Program.SaveUsers();
            }
            Program.accountDialog.username_box.Text = "";
        }
        private void instance_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (account_list.Enabled)
            {
                Instance selectedInst = null;
                if (instance_list.SelectedItems.Count > 0)
                    selectedInst = instance_list.SelectedItems[0].Tag as Instance;
                instance_edit.Enabled = selectedInst != null;
                instance_remove.Enabled = selectedInst != null;
            }
        }
        private void instance_remove_Click(object sender, EventArgs e)
        {
            Instance selectedInst = null;
            if (instance_list.SelectedItems.Count > 0)
                selectedInst = instance_list.SelectedItems[0].Tag as Instance;
            if (selectedInst != null)
            {
                DialogResult confirm = MessageBox.Show(SharedMethods.ReplaceFormat(Program.Lang?.del_inst ?? "Remove instance \"{0}\"?", selectedInst.name), "Sandstone Launcher", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (confirm == DialogResult.Yes)
                {
                    Program.Instances.Remove(selectedInst);
                    Program.LoadInstanceList();
                    Program.SaveInstances();
                }
            }
        }
        private void instance_edit_Click(object sender, EventArgs e)
        {
            Instance selectedInst = null;
            if (instance_list.SelectedItems.Count > 0)
                selectedInst = instance_list.SelectedItems[0].Tag as Instance;

            if (selectedInst != null)
            {
                Program.instanceDialog.NoFilters();
                Program.instanceDialog.SetValues(selectedInst);
                Program.instanceDialog.Text = Program.Lang?.edit_inst ?? "Editing an Instance";
                DialogResult confirm = Program.instanceDialog.ShowDialog();
                if (confirm == DialogResult.OK)
                {
                    Program.instanceDialog.EditInstance(selectedInst);
                    instance_box.DisplayMember = null;
                    instance_box.DisplayMember = "name";
                    if (Program.instanceDialog.predown.Checked) {
                        bool checkJre = !File.Exists(jre_box.Text);
                        bool checkHash = hash_box.Checked;
                        Task.Run(() => Program.DownloadFiles(selectedInst.version, selectedInst.gamedir ?? LauncherLib.GameDir, checkHash, checkJre, selectedInst));
                    }
                    Program.LoadInstanceList();
                    Program.SaveInstances();
                }
            }
        }

        private void instance_clone_Click(object sender, EventArgs e)
        {
            Instance selectedInst = null;
            if (instance_list.SelectedItems.Count > 0)
                selectedInst = instance_list.SelectedItems[0].Tag as Instance;

            if (selectedInst != null)
            {
                Program.instanceDialog.NoFilters();
                Program.instanceDialog.SetValues(selectedInst);
                Program.instanceDialog.Text = Program.Lang?.clone_inst ?? "Cloning an Instance";
                DialogResult confirm = Program.instanceDialog.ShowDialog();
                if (confirm == DialogResult.OK)
                {
                    Instance NewInst = Program.instanceDialog.NewInstance();
                    if (NewInst != null)
                    {
                        Program.Instances.Add(NewInst);
                        if (Program.instanceDialog.predown.Checked)
                        {
                            bool checkJre = !File.Exists(jre_box.Text);
                            bool checkHash = hash_box.Checked;
                            Task.Run(() => Program.DownloadFiles(NewInst.version, NewInst.gamedir ?? LauncherLib.GameDir, checkHash, checkJre, NewInst));
                        }
                        Program.LoadInstanceList();
                        Program.SaveInstances();
                    }
                }
            }
        }
        private void instance_add_Click(object sender, EventArgs e)
        {
            Program.instanceDialog.NoFilters();
            Program.instanceDialog.SetValues();
            Program.instanceDialog.Text = Program.Lang?.add_inst ?? "Adding an Instance";
            DialogResult confirm = Program.instanceDialog.ShowDialog();
            if (confirm == DialogResult.OK)
            {
                Instance NewInst = Program.instanceDialog.NewInstance();
                if (NewInst != null)
                {
                    Program.Instances.Add(NewInst);
                    if (Program.instanceDialog.predown.Checked)
                    {
                        bool checkJre = !File.Exists(jre_box.Text);
                        bool checkHash = hash_box.Checked;
                        Task.Run(() => Program.DownloadFiles(NewInst.version, NewInst.gamedir ?? LauncherLib.GameDir, checkHash, checkJre, NewInst));
                    }
                    Program.LoadInstanceList();
                    Program.SaveInstances();
                }
            }
        }

        private void bg_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bg_box.SelectedItem is Background bg)
                BackgroundImage = bg.Image?.Value;
        }
        private void lang_box_SelectedIndexChanged(object sender, EventArgs e) {
            Languages.ApplyLang(lang_box.SelectedItem as Language, this, Program.instanceDialog, Program.accountDialog);
            Program.Lang = lang_box.SelectedItem as Language ?? Languages.AllLanguages[0];
        }
        private void bg_folder_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory("Backgrounds");
                Process.Start("explorer.exe", "Backgrounds");
            }
            catch { Logger.Log("Couldn't open Backgrounds"); }
        }
        private void bg_button_Click(object sender, EventArgs e)
        {
            string BG = SelectFile(Backgrounds.AllowedExtString);
            if (File.Exists(BG))
            {
                FileInfo BGInfo = new FileInfo(BG);
                File.Copy(BG, Path.Combine("Backgrounds", BGInfo.Name));
                Backgrounds.LoadBackgrounds();
            }
        }
        private void jre_button_Click(object sender, EventArgs e)
        {
            string EXE = SelectFile("Executable Files (*.exe)|*.exe");
            if (File.Exists(EXE))
                jre_box.Text = EXE;
        }

        private void load_instances_Click(object sender, EventArgs e) => Program.LoadInstances();
        private void load_users_Click(object sender, EventArgs e) => Program.LoadUsers();
        private void openupd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://github.com/Lomik8908/Sandstone-Launcher/releases");

        private void console_box_CheckedChanged(object sender, EventArgs e)
        {
            if (console_box.Checked)
                Conhost.ShowConsole();
            else
                Conhost.HideConsole();    
        }

        private void stop_minecraft_Click(object sender, EventArgs e)
        {
            try
            {
                Program.GameProcess?.Refresh();
                if (Program.GameProcess?.HasExited == false) Program.GameProcess?.Kill();
            } catch {}
        }

        private void stop_operations_Click(object sender, EventArgs e)
        {
            LauncherLib.StopOperation();
            Program.StopLaunch();
        }

        private void javalist_btn_Click(object sender, EventArgs e)
        {
            using (var javaWindow = new JavaList(Program.settings.java_type)) {
                DialogResult result = javaWindow.ShowDialog();
                if (result == DialogResult.OK && javaWindow.list.SelectedItem is NameClass java)
                {
                    java_ver.Text = SharedMethods.ReplaceFormat(Program.Lang?.java_ver ?? "Version: {0}", java.Name);
                    if (java.Id == Program.NamedClasses["default"].Id)
                        Program.settings.java_type = null;
                    else
                        Program.settings.java_type = java.Id;
                }
            }
        }

        private void bgcolor_Click(object sender, EventArgs e)
        {
            DialogResult result = ColorPick.ShowDialog();
            if (result == DialogResult.OK)
            {
                BackColor = ColorPick.Color;
                bgcolor.BackColor = ColorPick.Color;
                if (ColorPick.Color == Color.DimGray)
                    Program.settings.bg_color = null;
                else
                    Program.settings.bg_color = ColorTranslator.ToHtml(ColorPick.Color);
            }
        }
    }
}
