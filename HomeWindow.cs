using SandstoneControls;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Sandstone_Launcher
{
    public partial class HomeWindow : BackgroundForm
    {
        //public static string JavaRegex = "^\\w+ version \"(.+)\"$";
        public ImageList AccountImages = new ImageList { ImageSize = new Size(24, 24), ColorDepth = ColorDepth.Depth32Bit };
        public ImageList InstanceImages = new ImageList { ImageSize = new Size(32, 32), ColorDepth = ColorDepth.Depth32Bit };
        private Language CurrentLang = Languages.DefaultLanguage;
        private readonly Color DefaultColor = Color.FromArgb(40, 40, 40);
        public HomeWindow()
        {
            InitializeComponent();
            AccountImages.Images.Add("offline", Properties.Resources.key);
            AccountImages.Images.Add("ely", Properties.Resources.ely);
            AccountImages.Images.Add("msa", Properties.Resources.msa);
            InstanceImages.Images.Add("Grass", Properties.Resources.grass);

            OpenAbout.Text = Program.AppVersionString;

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

        public void SetLanguage(Language Lang)
        {
            if (!(Lang is Language)) return;
            account_label.Text = Lang.acc_box;
            instance_label.Text = Lang.inst_box;
            launch.Text = Lang.play;
            OpenFolder.Text = Lang.open_gamedir;
            OpenSettings.Text = Lang.settings;
            settings_label.Text = Lang.settings;
            OpenInstances.Text = Lang.instances;
            instances_label.Text = Lang.instances;
            OpenAccounts.Text = Lang.accounts;
            accounts_label.Text = Lang.accounts;
            file_update.Text = Lang.updating;
            updateclient_box.Text = Lang.upd_client;
            updateassets_box.Text = Lang.upd_assets;
            updatejava_box.Text = Lang.upd_java;
            open_minecraft.Text = Lang.open_mcfol;
            open_instance.Text = Lang.open_infol;
            game_group.Text = Lang.game;
            gamedir_label.Text = Lang.gamefol;
            gamedir_button.Text = Lang.browses;
            res_label.Text = Lang.resolution;
            fullscreen_box.Text = Lang.fullscreen;
            ram_label.Text = Lang.ram;
            mib_label.Text = Lang.mib;
            gc_label.Text = Lang.gc_flags;
            mcarg_label.Text = Lang.mc_args;
            jvmarg_label.Text = Lang.jv_args;
            jre_label.Text = Lang.custom_java;
            launcher_group.Text = Lang.launcher;
            onlaunch_label.Text = Lang.onlaunch_mc;
            console_box.Text = Lang.show_console;
            fullargs_box.Text = Lang.show_launcharg;
            asset_box.Text = Lang.check_asset;
            hash_box.Text = Lang.check_hash;
            updates_box.Text = Lang.auto_upd;
            lang_label.Text = Lang.langs;
            bg_label.Text = Lang.bg;
            bg_folder.Text = Lang.open_bg;
            bg_button.Text = Lang.add_bg;
            jre_button.Text = Lang.browses;
            authlib_box.Text = Lang.use_authinj;
            load_instances.Text = Lang.load_inst;
            //load_users.Text = Lang.load_user;
            stop_instance.Text = Lang.stop_instance;
            stop_instances.Text = Lang.stop_instances;
            stop_operations.Text = Lang.stop_operations;
            other_label.Text = Lang.other;

            onlaunch_box.DisplayMember = null;
            onlaunch_box.DisplayMember = "Name";

            gc_box.DisplayMember = null;
            gc_box.DisplayMember = "Name";

            javalist_btn.Text = Lang.open_javas;
            separateVers.Text = Lang.separate_vers;

            UpdateJavaLabel();
            CurrentLang = Lang;
        }
        public string FolderOpenDialog(string SelectedFolder = null, string Description = null)
        {
            using (var Dialog = new FolderBrowserDialog { Description = Description, SelectedPath = SelectedFolder?.Replace("/", "\\") })
            {
                DialogResult result = Dialog.ShowDialog();
                if (result == DialogResult.OK)
                    return Dialog.SelectedPath;
            }
            return null;
        }
        public string FileOpenDialog(string Filter = "All Files (*.*)|*.*")
        {
            using (var Dialog = new OpenFileDialog { Filter = Filter })
            {
                DialogResult result = Dialog.ShowDialog();
                if (result == DialogResult.OK)
                    return Dialog.FileName;
            }
            return null;
        }
        public void UpdateJavaLabel()
        {
            if (string.IsNullOrEmpty(Program.settings.java_type))
                java_ver.Text = SharedMethods.ReplaceFormat(CurrentLang.java_ver, Program.NamedClasses["default"].Name); // ?? "Version: {0}"
            else
                java_ver.Text = SharedMethods.ReplaceFormat(CurrentLang.java_ver, Program.settings.java_type);// ?? "Version: {0}"
        }
        public void OpenMenu(int Screen)
        {
            settings_box.Visible = Screen == 1;
            instances_box.Visible = Screen == 2;
            accounts_box.Visible = Screen == 3;
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
            string Path = FolderOpenDialog(Program.settings.gamedir, CurrentLang.gamedir_desc);
            if (Path is string)
                Program.SetGameDir(Path);
        }
        private void launch_Click(object sender, EventArgs e) => Program.Launch();
        private void OpenFolder_Click(object sender, EventArgs e)
        {
            if (instance_box.SelectedItem is Instance inst && (Directory.Exists(inst.gamedir) || (separateVers.Checked && Directory.Exists(Path.Combine(LauncherLib.GameDir, "instances", inst.version)))))
                AskOpenFolder.Show(more, Point.Empty);
            else
                Process.Start("explorer", LauncherLib.GameDir);
        }
        private void open_minecraft_Click(object sender, EventArgs e) => Process.Start("explorer", LauncherLib.GameDir);
        private void open_instance_Click(object sender, EventArgs e)
        {
            if (instance_box.SelectedItem is Instance inst)
            {
                if (Directory.Exists(inst.gamedir))
                    Process.Start("explorer", inst.gamedir);
                else
                {
                    string openDir = Path.Combine(LauncherLib.GameDir, "instances", inst.version);
                    if (Directory.Exists(openDir))
                        Process.Start("explorer", openDir);
                }
            }
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
                DialogResult confirm = MessageBox.Show(SharedMethods.ReplaceFormat(CurrentLang.del_user, selectedUser.username), Program.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);// ?? "Remove account \"{0}\"?"
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

            if (selectedUser != null && selectedUser.usertype == "offline")
            {
                using (var Dialog = new AccountDialog())
                {
                    Dialog.SetLanguage(CurrentLang, CurrentLang.edit_acc);
                    Dialog.usertype_box.Enabled = false;
                    Dialog.username_box.Text = selectedUser.username;
                    Dialog.Text = CurrentLang?.edit_acc ?? "Editing an Account";
                    DialogResult confirm = Dialog.ShowDialog();
                    if (confirm == DialogResult.OK)
                    {
                        selectedUser.username = Dialog.username_box.Text;
                        account_box.DataSource = null;
                        account_box.DataSource = Program.Users;
                        Program.LoadUsersList();
                        Program.SaveUsers();
                    }
                }
            }
        }
        private void account_add_Click(object sender, EventArgs e)
        {
            using (var Dialog = new AccountDialog())
            {
                Dialog.SetLanguage(CurrentLang, CurrentLang.add_acc);
                Dialog.usertype_box.Enabled = Program.CanUseOtherServices();
                Dialog.Text = CurrentLang?.add_acc ?? "Adding an Account";
                DialogResult confirm = Dialog.ShowDialog();
                if (confirm == DialogResult.OK)
                {
                    lock (Program.AccountLock)
                        Program.Users.Add(new User
                        {
                            username = Dialog.username_box.Text,
                            usertype = "offline",
                            accessToken = "0",
                            uuid = Guid.NewGuid().ToString("N")
                        });
                    Program.LoadUsersList();
                    Program.SaveUsers();
                }
            }
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
                instance_clone.Enabled = selectedInst != null;
            }
        }
        private void instance_remove_Click(object sender, EventArgs e)
        {
            Instance selectedInst = null;
            if (instance_list.SelectedItems.Count > 0)
                selectedInst = instance_list.SelectedItems[0].Tag as Instance;
            if (selectedInst != null)
            {
                DialogResult confirm = MessageBox.Show(SharedMethods.ReplaceFormat(CurrentLang.del_inst, selectedInst.name), Program.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2); //  ?? "Remove instance \"{0}\"?"
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
                using (var Dialog = new InstanceDialog())
                {
                    Dialog.SetLanguage(CurrentLang, CurrentLang.edit_inst);
                    Dialog.NoFilters();
                    Dialog.SetValues(selectedInst);
                    DialogResult confirm = Dialog.ShowDialog();
                    if (confirm == DialogResult.OK)
                    {
                        Dialog.EditInstance(selectedInst);
                        instance_box.DisplayMember = null;
                        instance_box.DisplayMember = "name";
                        if (Dialog.predown.Checked)
                            Program.DownloadFiles(selectedInst);
                        Program.LoadInstanceList();
                        Program.SaveInstances();
                    }
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
                using (var Dialog = new InstanceDialog())
                {
                    Dialog.SetLanguage(CurrentLang, CurrentLang.clone_inst);
                    Dialog.NoFilters();
                    Dialog.SetValues(selectedInst);
                    DialogResult confirm = Dialog.ShowDialog();
                    if (confirm == DialogResult.OK)
                    {
                        Instance NewInst = Dialog.NewInstance();
                        if (NewInst != null)
                        {
                            Program.Instances.Add(NewInst);
                            if (Dialog.predown.Checked)
                                Program.DownloadFiles(NewInst);
                            Program.LoadInstanceList();
                            Program.SaveInstances();
                        }
                    }
                }
            }
        }
        private void instance_add_Click(object sender, EventArgs e)
        {
            using (var Dialog = new InstanceDialog())
            {
                Dialog.SetLanguage(CurrentLang, CurrentLang.add_inst);
                Dialog.NoFilters();
                Dialog.SetValues();
                DialogResult confirm = Dialog.ShowDialog();
                if (confirm == DialogResult.OK)
                {
                    Instance NewInst = Dialog.NewInstance();
                    if (NewInst != null)
                    {
                        Program.Instances.Add(NewInst);
                        if (Dialog.predown.Checked)
                            Program.DownloadFiles(NewInst);
                        Program.LoadInstanceList();
                        Program.SaveInstances();
                    }
                }
            }
        }

        private void bg_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bg_box.SelectedItem is Background bg)
                BackgroundImage = bg.Image?.Value;
        }
        private void lang_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            Languages.ApplyLang(lang_box.SelectedItem as Language, this); //, Program.instanceDialog, Program.accountDialog
            Program.Lang = lang_box.SelectedItem as Language ?? Languages.DefaultLanguage;
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
            string FilePath = FileOpenDialog(Backgrounds.AllowedExtString);
            if (File.Exists(FilePath))
            {
                FileInfo BGInfo = new FileInfo(FilePath);
                Directory.CreateDirectory("Backgrounds");
                File.Copy(FilePath, Path.Combine("Backgrounds", BGInfo.Name));
                Backgrounds.LoadBackgrounds();
            }
        }
        private void jre_button_Click(object sender, EventArgs e)
        {
            string Path = FileOpenDialog("Executable Files (*.exe)|*.exe");
            if (File.Exists(Path))
                jre_box.Text = Path;
        }

        private void load_instances_Click(object sender, EventArgs e) => Program.LoadInstances();
        //We are encrypting users now...
        //private void load_users_Click(object sender, EventArgs e) => Program.LoadUsers();
        private void openupd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://github.com/Lomik8908/Sandstone-Launcher/releases");

        private void console_box_CheckedChanged(object sender, EventArgs e)
        {
            if (console_box.Checked)
                Conhost.ShowConsole();
            else
                Conhost.HideConsole();
        }

        private void javalist_btn_Click(object sender, EventArgs e)
        {
            using (var javaWindow = new JavaList(Program.settings.java_type))
            {
                javaWindow.SetLanguage(CurrentLang);
                DialogResult result = javaWindow.ShowDialog();
                if (result == DialogResult.OK && javaWindow.list.SelectedItem is NameClass java)
                {
                    java_ver.Text = SharedMethods.ReplaceFormat(CurrentLang.java_ver, java.Name); // ?? "Version: {0}"
                    if (java.Id == Program.NamedClasses["default"].Id)
                        Program.settings.java_type = null;
                    else
                        Program.settings.java_type = java.Id;
                }
            }
        }

        private void stop_operations_Click(object sender, EventArgs e)
        {
            LauncherLib.StopOperation();
            Program.StopLaunch();
        }

        private void stop_instances_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show(CurrentLang.stopping_all, Program.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Result == DialogResult.Yes)
            {
                lock (Program.GameProcLock)
                    foreach (var Launched in Program.GameProcesses)
                    {
                        try
                        {
                            Launched.Value.Refresh();
                            if (!Launched.Value.HasExited) Launched.Value.Kill();
                        }
                        catch { }
                    }
            }
        }

        private void stop_instance_Click(object sender, EventArgs e)
        {
            if (instance_box.SelectedItem is Instance Inst)
            {
                lock (Program.GameProcLock)
                {
                    if (Program.GameProcesses.ContainsKey(Inst.uuid))
                    {
                        try
                        {
                            Process Proc = Program.GameProcesses[Inst.uuid];
                            Proc.Refresh();
                            if (!Proc.HasExited) Proc.Kill();
                        }
                        catch { }
                    }
                }
            }
        }

        private void bgcolor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DialogResult result = ColorPick.ShowDialog();
                if (result == DialogResult.OK)
                {
                    BackColor = ColorPick.Color;
                    bgcolor.BackColor = ColorPick.Color;
                    Program.settings.bg_color = ColorTranslator.ToHtml(ColorPick.Color);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                DialogResult result = MessageBox.Show(CurrentLang.revert_bgcolor, Program.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    BackColor = DefaultColor;
                    bgcolor.BackColor = DefaultColor;
                    Program.settings.bg_color = null;
                }
            }
        }
    }
}
