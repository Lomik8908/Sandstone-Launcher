using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json.Nodes;
using System.Windows.Forms;

namespace Sandstone_Launcher
{
    public partial class InstanceDialog : Form
    {
        readonly NameClass Snapshot = new NameClass { Id = "latest-snapshot", Name = "Latest Snapshot" };
        readonly NameClass Release = new NameClass { Id = "latest-release", Name = "Latest Release" };
        private Language CurrentLang = Languages.DefaultLanguage;
        public InstanceDialog()
        {
            InitializeComponent();
            gc_box.Items.Add(Program.NamedClasses["none"]);
            gc_box.Items.Add(Program.NamedClasses["default"]);
            ram_bar.Maximum = (int)(Program.pcInfo.TotalPhysicalMemory / (1024 * 1024));
            LoadGameVers();

            version_box.MouseWheel += SharedMethods.HandleScroll;
            ram_bar.MouseWheel += SharedMethods.HandleScroll;
            ram_box.MouseWheel += SharedMethods.HandleScroll;
            resx_box.MouseWheel += SharedMethods.HandleScroll;
            resy_box.MouseWheel += SharedMethods.HandleScroll;
            gc_box.MouseWheel += SharedMethods.HandleScroll;
            DarkModeTitle.SetDarkMode(Handle, true);
        }

        public void SetLanguage(Language Lang, string Title = null)
        {
            if (!(Lang is Language)) return;
            instname_label.Text = Lang.inst_name;
            version_label.Text = Lang.game_ver;

            gamedir_button.Text = Lang.browses;
            gamedir_label.Text = Lang.gamefol;
            res_label.Text = Lang.resolution;
            ram_label.Text = Lang.ram;
            mib_label.Text = Lang.mib;
            gc_label.Text = Lang.gc_flags;
            mcarg_label.Text = Lang.mc_args;
            jvmarg_label.Text = Lang.jv_args;

            installed_only.Text = Lang.installed_only;
            show_snapshots.Text = Lang.show_snap;

            predown.Text = Lang.predown_files;
            save.Text = Lang.save;
            cancel.Text = Lang.cancel;

            gc_box.DisplayMember = null;
            gc_box.DisplayMember = "Name";

            javalist_btn.Text = Lang.open_javas;
            jre_label.Text = Lang.custom_java;
            jre_button.Text = Lang.browses;

            Text = Title ?? Program.AppName;
            CurrentLang = Lang;
        }

        string CurrentJavaVer = null;
        public void SetValues(Instance inst = null)
        {
            name_box.Text = inst?.name;
            if (inst?.version == Release.Id) version_box.SelectedItem = Release;
            else if (inst?.version == Snapshot.Id)
            {
                show_snapshots.Checked = true;
                version_box.SelectedItem = Snapshot;
            }
            else if (inst?.version != null)
            {
                if (!version_box.Items.Contains(inst.version))
                {
                    JsonArray Vers = LauncherLib.GetVersionsManifest()?["versions"]?.AsArray();
                    if (Vers?.Any(v => v["id"]?.ToString() == inst.version && v["type"]?.ToString() == "snapshot") == true)
                        show_snapshots.Checked = true;
                }
                version_box.SelectedItem = inst?.version;
            }
            else version_box.SelectedItem = null;
            gamedir_box.Text = inst?.gamedir;
            resx_box.Value = inst?.width ?? 0;
            resy_box.Value = inst?.height ?? 0;
            ram_bar.Value = inst?.memory ?? 0;
            ram_box.Value = inst?.memory ?? 0;
            GCTemplate GC = GCFlags.GCTemplates.FirstOrDefault(v => v.id == inst?.gc_preset);
            if (inst?.gc_preset == null || inst?.gc_preset == "default" || GC == null) gc_box.SelectedItem = Program.NamedClasses["default"];
            else if (inst?.gc_preset == "none") gc_box.SelectedItem = Program.NamedClasses["none"];
            else gc_box.SelectedItem = GC;
            mcarg_box.Text = inst?.mc_args;
            jvmarg_box.Text = inst?.java_args;
            jre_box.Text = inst?.java_path;
            predown.Checked = false;

            if (string.IsNullOrEmpty(inst?.java_type))
                java_ver.Text = SharedMethods.ReplaceFormat(CurrentLang.java_ver, Program.NamedClasses["default"].Name); // ?? "Version: {0}"
            else
                java_ver.Text = SharedMethods.ReplaceFormat(CurrentLang.java_ver, inst.java_type); // ?? "Version: {0}"

            CurrentJavaVer = inst?.java_type;
        }
        public void NoFilters()
        {
            installed_only.Checked = false;
            show_snapshots.Checked = false;
        }
        public Instance NewInstance()
        {
            if (version_box.SelectedItem == null)
                return null;
            return new Instance
            {
                name = string.IsNullOrWhiteSpace(name_box.Text) ? version_box.SelectedItem as string ?? (version_box.SelectedItem as NameClass)?.Name : name_box.Text,
                version = version_box.SelectedItem as string ?? (version_box.SelectedItem as NameClass)?.Id,
                gamedir = string.IsNullOrWhiteSpace(gamedir_box.Text) ? null : gamedir_box.Text,
                width = resx_box.Value > 0 ? (int?)resx_box.Value : null,
                height = resy_box.Value > 0 ? (int?)resy_box.Value : null,
                memory = ram_box.Value > 0 ? (int?)ram_box.Value : null,
                gc_preset = (gc_box.SelectedItem as GCTemplate)?.id ?? (gc_box.SelectedItem as NameClass)?.Id ?? "default",
                mc_args = string.IsNullOrWhiteSpace(mcarg_box.Text) ? null : mcarg_box.Text,
                java_args = string.IsNullOrWhiteSpace(jvmarg_box.Text) ? null : jvmarg_box.Text,
                java_path = string.IsNullOrWhiteSpace(jre_box.Text) ? null : jre_box.Text,
                java_type = CurrentJavaVer,
                uuid = Guid.NewGuid().ToString()
            };
        }
        public void EditInstance(Instance inst)
        {
            if (version_box.SelectedItem == null)
                return;
            inst.name = string.IsNullOrWhiteSpace(name_box.Text) ? version_box.SelectedItem as string ?? (version_box.SelectedItem as NameClass)?.Name : name_box.Text;
            inst.version = version_box.SelectedItem as string ?? (version_box.SelectedItem as NameClass)?.Id;
            inst.gamedir = string.IsNullOrWhiteSpace(gamedir_box.Text) ? null : gamedir_box.Text;
            inst.width = resx_box.Value > 0 ? (int?)resx_box.Value : null;
            inst.height = resy_box.Value > 0 ? (int?)resy_box.Value : null;
            inst.memory = ram_box.Value > 0 ? (int?)ram_box.Value : null;
            if (gc_box.SelectedItem is NameClass)
                inst.gc_preset = (gc_box.SelectedItem as NameClass)?.Id;
            else
                inst.gc_preset = (gc_box.SelectedItem as GCTemplate)?.id;
            inst.mc_args = string.IsNullOrWhiteSpace(mcarg_box.Text) ? null : mcarg_box.Text;
            inst.java_args = string.IsNullOrWhiteSpace(jvmarg_box.Text) ? null : jvmarg_box.Text;
            inst.java_path = string.IsNullOrWhiteSpace(jre_box.Text) ? null : jre_box.Text;
            inst.java_type = CurrentJavaVer;
        }
        public void LoadGameVers()
        {
            string Version = version_box.SelectedItem as string;
            version_box.Items.Clear();
            int InsertAt = 1;
            version_box.Items.Add(Release);
            if (show_snapshots.Checked || Version == Snapshot.Id)
            {
                version_box.Items.Add(Snapshot);
                InsertAt = 2;
            }
            if (!installed_only.Checked)
            {
                JsonArray Vers = LauncherLib.GetVersionsManifest()?["versions"]?.AsArray();
                if (Vers != null)
                    foreach (JsonNode Ver in Vers)
                    {
                        if (Ver["type"].ToString() == "snapshot" && !show_snapshots.Checked) continue;
                        int Index = version_box.Items.Add(Ver["id"].ToString());
                        if (Ver["id"].ToString() == Version)
                            version_box.SelectedIndex = Index;
                    }
            }
            foreach (string Ver in LauncherLib.GetInstalledVersions())
            {
                if (version_box.Items.Contains(Ver)) continue;
                version_box.Items.Insert(InsertAt, Ver);
                if (Ver == Version)
                    version_box.SelectedIndex = 0;
            }
        }

        private void filter_button_Click(object sender, EventArgs e) => filter_menu.Show(filter_button, Point.Empty);
        private void ram_bar_Scroll(object sender, EventArgs e) => ram_box.Value = Math.Max(ram_box.Minimum, Math.Min(ram_bar.Value, ram_box.Maximum));
        private void ram_box_ValueChanged(object sender, EventArgs e) => ram_bar.Value = (int)Math.Max(ram_bar.Minimum, Math.Min(ram_box.Value, ram_bar.Maximum));
        private void save_Click(object sender, EventArgs e)
        {
            if (version_box.SelectedItem != null)
                DialogResult = DialogResult.OK;
            else
                MessageBox.Show(CurrentLang.sel_ver_warn, Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information); // ?? "Select the game version for this instance!"
        }
        private void installed_only_CheckedChanged(object sender, EventArgs e) => LoadGameVers();
        private void show_snapshots_CheckedChanged(object sender, EventArgs e) => LoadGameVers();

        private void gamedir_button_Click(object sender, EventArgs e)
        {
            string Path = Program.homeWindow.FolderOpenDialog(!Directory.Exists(gamedir_box.Text) ? LauncherLib.GameDir : gamedir_box.Text, CurrentLang.gamedir_desc);
            if (Path is string)
                gamedir_box.Text = Path;
        }

        private void jre_button_Click(object sender, EventArgs e)
        {
            string Path = Program.homeWindow.FileOpenDialog("Executable Files (*.exe)|*.exe");
            if (File.Exists(Path))
                jre_box.Text = Path;
        }

        private void javalist_btn_Click(object sender, EventArgs e)
        {
            using (var javaWindow = new JavaList(CurrentJavaVer))
            {
                DialogResult result = javaWindow.ShowDialog();
                if (result == DialogResult.OK && javaWindow.list.SelectedItem is NameClass java)
                {
                    java_ver.Text = SharedMethods.ReplaceFormat(CurrentLang.java_ver, java.Name); // ?? "Version: {0}"
                    if (java.Id == Program.NamedClasses["default"].Id)
                        CurrentJavaVer = null;
                    else
                        CurrentJavaVer = java.Id;
                }
            }
        }
    }
}
