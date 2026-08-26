using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows.Forms;

namespace Sandstone_Launcher
{
    public partial class JavaList : Form
    {
        private Language CurrentLang = Languages.DefaultLanguage;
        public JavaList(string SelectVer = null)
        {
            InitializeComponent();
            LoadJavas(SelectVer);
            DarkModeTitle.SetDarkMode(Handle, true);
        }

        public void SetLanguage(Language Lang)
        {
            if (!(Lang is Language)) return;
            Text = Lang.java_list;
            cancel.Text = Lang.cancel;
            select.Text = Lang.select;

            CurrentLang = Lang;
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
                    JsonNode jvList = javaList[$"windows-{osArch}"]?[Java.Key];
                    if (jvList?.GetValueKind() == JsonValueKind.Array && jvList.AsArray().Count > 0)
                    {
                        JsonNode jvObj = jvList[0];
                        int idx = list.Items.Add(new NameClass
                        {
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
                MessageBox.Show(CurrentLang.sel_java, Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information); //  ?? "Select a java version!"
        }
    }
}
