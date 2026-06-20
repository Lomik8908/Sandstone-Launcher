using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Sandstone_Launcher
{
    static class Languages
    {
        static public BindingList<Language> AllLanguages = new BindingList<Language> {
            new Language()
        };
        static public void LoadLangs() {
            if (Directory.Exists("Languages"))
                foreach (var PossibleLang in Directory.GetFiles("Languages"))
                    if (Path.GetExtension(PossibleLang).ToLowerInvariant() == ".json")
                        try {
                            Language Lang = JsonSerializer.Deserialize<Language>(File.ReadAllText(PossibleLang), new JsonSerializerOptions { AllowTrailingCommas = true });
                            if (!AllLanguages.Any(v => v.lang_id == Lang.lang_id)) AllLanguages.Add(Lang);
                        } catch (Exception ex) {
                            Logger.Log($"Bad language {Path.GetFileName(PossibleLang)}: {ex.Message}");
                        }
        }
        static public void ApplyLang(Language language, HomeWindow homeWindow, InstanceDialog instanceDialog, AccountDialog accountDialog)
        {
            if (language == null) return;
            Program.NamedClasses["none"].Name = language.none;
            Program.NamedClasses["default"].Name = language.defaults;
            Program.NamedClasses["onlaunch_hide"].Name = language.launch_hide;
            Program.NamedClasses["onlaunch_close"].Name = language.launch_close;
            Program.NamedClasses["onlaunch_none"].Name = language.launch_none;
            if (homeWindow != null) {
                homeWindow.account_label.Text = language.acc_box;
                homeWindow.instance_label.Text = language.inst_box;
                homeWindow.launch.Text = language.play;
                homeWindow.OpenFolder.Text = language.open_gamedir;
                homeWindow.OpenSettings.Text = language.settings;
                homeWindow.settings_label.Text = language.settings;
                homeWindow.OpenInstances.Text = language.instances;
                homeWindow.instances_label.Text = language.instances;
                homeWindow.OpenAccounts.Text = language.accounts;
                homeWindow.accounts_label.Text = language.accounts;
                homeWindow.file_update.Text = language.updating;
                homeWindow.checkUpdates.Text = language.check_update;
                homeWindow.updateclient_box.Text = language.upd_client;
                homeWindow.updateassets_box.Text = language.upd_assets;
                homeWindow.updatejava_box.Text = language.upd_java;
                homeWindow.open_minecraft.Text = language.open_mcfol;
                homeWindow.open_instance.Text = language.open_infol;
                homeWindow.game_group.Text = language.game;
                homeWindow.gamedir_label.Text = language.gamefol;
                homeWindow.gamedir_button.Text = language.browses;
                homeWindow.res_label.Text = language.resolution;
                homeWindow.fullscreen_box.Text = language.fullscreen;
                homeWindow.ram_label.Text = language.ram;
                homeWindow.mib_label.Text = language.mib;
                homeWindow.gc_label.Text = language.gc_flags;
                homeWindow.mcarg_label.Text = language.mc_args;
                homeWindow.jvmarg_label.Text = language.jv_args;
                homeWindow.jre_label.Text = language.custom_java;
                homeWindow.launcher_group.Text = language.launcher;
                homeWindow.onlaunch_label.Text = language.onlaunch_mc;
                homeWindow.console_box.Text = language.show_console;
                homeWindow.fullargs_box.Text = language.show_launcharg;
                homeWindow.asset_box.Text = language.check_asset;
                homeWindow.hash_box.Text = language.check_hash;
                homeWindow.updates_box.Text = language.auto_upd;
                homeWindow.lang_label.Text = language.langs;
                homeWindow.bg_label.Text = language.bg;
                homeWindow.bgs_label.Text = language.custom_bgs;
                homeWindow.bg_folder.Text = language.open_bg;
                homeWindow.bg_button.Text = language.browses;
                homeWindow.jre_button.Text = language.browses;
                homeWindow.authlib_box.Text = language.use_authinj;
                homeWindow.load_instances.Text = language.load_inst;
                homeWindow.load_users.Text = language.load_user;

                homeWindow.onlaunch_box.DisplayMember = null;
                homeWindow.onlaunch_box.DisplayMember = "Name";

                homeWindow.gc_box.DisplayMember = null;
                homeWindow.gc_box.DisplayMember = "Name";
            }
            if (instanceDialog != null)
            {
                instanceDialog.instname_label.Text = language.inst_name;
                instanceDialog.version_label.Text = language.game_ver;

                instanceDialog.gamedir_button.Text = language.browses;
                instanceDialog.gamedir_label.Text = language.gamefol;
                instanceDialog.res_label.Text = language.resolution;
                instanceDialog.ram_label.Text = language.ram;
                instanceDialog.mib_label.Text = language.mib;
                instanceDialog.gc_label.Text = language.gc_flags;
                instanceDialog.mcarg_label.Text = language.mc_args;
                instanceDialog.jvmarg_label.Text = language.jv_args;

                instanceDialog.installed_only.Text = language.installed_only;
                instanceDialog.show_snapshots.Text = language.show_snap;

                instanceDialog.save.Text = language.save;
                instanceDialog.cancel.Text = language.cancel;

                instanceDialog.gc_box.DisplayMember = null;
                instanceDialog.gc_box.DisplayMember = "Name";
            }

            AccountType offline = Accounts.accountTypes.FirstOrDefault(v => v.id == "offline");
            if (offline != null) offline.name = language.offline;
            if (accountDialog != null)
            {
                accountDialog.username_label.Text = language.username;
                accountDialog.usertype_label.Text = language.user_type;
                accountDialog.save.Text = language.save;
                accountDialog.cancel.Text = language.cancel;

                accountDialog.usertype_box.DisplayMember = null;
                accountDialog.usertype_box.DisplayMember = "Name";
            }
        }
    }

    class Language {
        public string lang_id { get; set; } = "en_us";
        public string lang_name { get; set; } = "English (US)";
        public string acc_box { get; set; } = "Account:";
        public string inst_box { get; set; } = "Instance:";
        public string play { get; set; } = "Play!";
        public string cancel { get; set; } = "Cancel";
        public string open_gamedir { get; set; } = "Open Game Folder";
        public string settings { get; set; } = "Settings";
        public string instances { get; set; } = "Instances";
        public string accounts { get; set; } = "Accounts";
        public string updating { get; set; } = "Updating";
        public string check_update { get; set; } = "Check for updates";
        public string upd_client { get; set; } = "Update Client";
        public string upd_assets { get; set; } = "Update Assets";
        public string upd_java { get; set; } = "Update Java";
        public string open_mcfol { get; set; } = "Open .minecraft";
        public string open_infol { get; set; } = "Open instance folder";
        public string game { get; set; } = "Game";
        public string gamefol { get; set; } = "Game Folder";
        public string browses { get; set; } = "Browse...";
        public string resolution { get; set; } = "Resolution";
        public string fullscreen { get; set; } = "Fullscreen";
        public string ram { get; set; } = "Allocated Memory";
        public string mib { get; set; } = "MiB";
        public string gc_flags { get; set; } = "Garbage Collector Flags";
        public string mc_args { get; set; } = "Minecraft Arguments";
        public string jv_args { get; set; } = "Java Arguments";
        public string custom_java { get; set; } = "Custom Runtime";
        public string launcher { get; set; } = "Launcher";
        public string none { get; set; } = "None";
        public string onlaunch_mc { get; set; } = "When minecraft launches";
        public string launch_none { get; set; } = "Do nothing";
        public string launch_hide { get; set; } = "Hide Launcher";
        public string launch_close { get; set; } = "Close Launcher";
        public string show_console { get; set; } = "Show Console";
        public string show_launcharg { get; set; } = "Show launch arguments";
        public string check_asset { get; set; } = "Check Assets";
        public string check_hash { get; set; } = "Check Hashes";
        public string auto_upd { get; set; } = "Check launcher updates";
        public string langs { get; set; } = "Language";
        public string bg { get; set; } = "Background";
        public string save { get; set; } = "Save";
        public string inst_name { get; set; } = "Instance Name";
        public string game_ver { get; set; } = "Game Version";
        public string edit_inst { get; set; } = "Editing an Instance";
        public string add_inst { get; set; } = "Adding an Instance";
        public string username { get; set; } = "Username";
        public string add_acc { get; set; } = "Adding an Account";
        public string edit_acc { get; set; } = "Editing an Account";
        public string del_user { get; set; } = "Remove account \"{0}\"?";
        public string del_inst { get; set; } = "Remove instance \"{0}\"?";
        public string defaults { get; set; } = "Default";
        public string make_username { get; set; } = "Make a username!";
        public string username_warn { get; set; } = "While using this username you might encounter:\nGame Crashes\nServer Bugs\nUnexpected Behaviour\nUse this username at your own risk!";
        public string sel_ver_warn { get; set; } = "Select the game version for this instance!";
        public string make_name_warn { get; set; } = "Create a name for this instance!";
        public string open_bg { get; set; } = "Folder";
        public string custom_bgs { get; set; } = "Custom Backgrounds:";
        public string down_asset { get; set; } = "Checking assets ({0}/{1})";
        public string down_client { get; set; } = "Checking client ({0}/{1})";
        public string down_java { get; set; } = "Checking java ({0}/{1})";
        public string login_into { get; set; } = "Login into {0}";
        public string stop { get; set; } = "Stop!";
        public string blogin_info { get; set; } = "Logging into {0}...";
        public string sel_acc { get; set; } = "Select an account!";
        public string sel_inst { get; set; } = "Select an instance!";
        public string cresel_acc { get; set; } = "Create and select an account!";
        public string cresel_inst { get; set; } = "Create and select an instance!";
        public string user_type { get; set; } = "Account Type";
        public string offline { get; set; } = "Offline";
        public string password { get; set; } = "Password";
        public string login { get; set; } = "Login";
        public string tfa { get; set; } = "2-Factor Authentication";
        public string enter2fa { get; set; } = "Enter the 2FA code below";
        public string register { get; set; } = "You don't have an account? Register one!";
        public string type_userpass { get; set; } = "Type in your username and password!";
        public string ely_err { get; set; } = "Couldn't add an Ely.by account!\nCheck if the info was valid!";
        public string use_authinj { get; set; } = "Use AuthLib Injector";
        public string installed_only { get; set; } = "Installed Only";
        public string show_snap { get; set; } = "Show Snapshots";
        public string load_user { get; set; } = "Load Missing Accounts";
        public string load_inst { get; set; } = "Load Missing Instances";
    }
}
