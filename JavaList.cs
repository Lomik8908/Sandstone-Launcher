using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows.Forms;

namespace Sandstone_Launcher
{
    public partial class JavaList : Form
    {
        public JavaList(string SelectVer = null)
        {
            InitializeComponent();
            LoadJavas(SelectVer);
            Text = Program.Lang?.java_list ?? "Java List";
            cancel.Text = Program.Lang?.cancel ?? "Cancel";
            select.Text = Program.Lang?.select ?? "Select";
            DarkModeTitle.SetDarkMode(Handle, true);
        }

        private void LoadJavas(string SelectVer = null)
        {
            list.Items.Clear();
            list.Items.Add(Program.NamedClasses["default"]);
            list.SelectedIndex = 0;

            JsonNode javaList = LauncherLib.GetJavaManifest();
            string osArch = Environment.Is64BitOperatingSystem ? "x64" : "x86";
            if (javaList?["gamecore"] != null)
                foreach (var Java in javaList["gamecore"].AsObject())
                {
                    if (Java.Key == "minecraft-java-exe") continue;
                    JsonNode jvList = javaList[$"windows-x86"]?[Java.Key];
                    if (jvList?.GetValueKind() == JsonValueKind.Array && jvList.AsArray().Count > 0)
                    {
                        JsonNode jvObj = jvList[0];
                        int idx = list.Items.Add(new NameClass { 
                            Id = Java.Key,
                            Name = $"Java {jvObj["version"]?["name"]?.ToString() ?? Java.Key} ({Java.Key})"
                        });
                        if (SelectVer != null && SelectVer == Java.Key)
                            list.SelectedIndex = idx;
                    }
                }
        }

        private void select_Click(object sender, EventArgs e)
        {
            if (list.SelectedItem != null)
                DialogResult = DialogResult.OK;
            else
                MessageBox.Show(Program.Lang?.sel_java ?? "Select a java version!", "Sandstone Launcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
