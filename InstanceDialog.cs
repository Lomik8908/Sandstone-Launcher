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
        NameClass Snapshot = new NameClass { Id = "latest-snapshot", Name = "Latest Snapshot" };
        NameClass Release = new NameClass { Id = "latest-release", Name = "Latest Release" };
        public InstanceDialog()
        {
            InitializeComponent();
            gc_box.Items.Add(Program.NamedClasses["none"]);
            gc_box.Items.Add(Program.NamedClasses["default"]);
            ram_bar.Maximum = (int)(Program.pcInfo.TotalPhysicalMemory / (1024 * 1024));
            LoadGameVers();

            gc_box.MouseWheel += SharedMethods.HandleScroll;
        }

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
        }
        public void LoadGameVers() {
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
                MessageBox.Show(Program.Lang?.sel_ver_warn ?? "Select the game version for this instance!", "Sandstone Launcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void installed_only_CheckedChanged(object sender, EventArgs e) => LoadGameVers();
        private void show_snapshots_CheckedChanged(object sender, EventArgs e) => LoadGameVers();

        private void gamedir_button_Click(object sender, EventArgs e)
        {
            string Path = Program.homeWindow.SelectFolder();
            if (Path != null)
                gamedir_box.Text = Path;
        }

        private void jre_button_Click(object sender, EventArgs e)
        {
            string EXE = Program.homeWindow.SelectFile("Executable Files (*.exe)|*.exe");
            if (File.Exists(EXE))
                jre_box.Text = EXE;
        }
    }
}
