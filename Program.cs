using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sandstone_Launcher
{
    static class Program
    {
        static public string AppVersionString = "1.0.0 rc-1.4";
        static public int AppVersion = 3;
        static public ComputerInfo pcInfo = new ComputerInfo();

        static public Settings settings = new Settings();
        static string[] CommonFlags = new string[] { "-XX:+AlwaysPreTouch", "-Dfml.ignoreInvalidMinecraftCertificates=true", "-Dfml.ignorePatchDiscrepancies=true", "-Djava.net.useSystemProxies=true", "-Dfile.encoding=UTF-8" };

        static public Dictionary<string, NameClass> NamedClasses = new Dictionary<string, NameClass> {
            { "none", new NameClass { Id = "none", Name = "None" } },
            { "default", new NameClass { Id = "default", Name = "Default" } },
            { "onlaunch_hide", new NameClass { Name = "Hide launcher" } },
            { "onlaunch_close", new NameClass { Name = "Close launcher" } },
            { "onlaunch_none", new NameClass { Name = "Do nothing" } }
        };

        static public Language Lang = Languages.AllLanguages[0];

        static public BindingList<User> Users = new BindingList<User>();
        static public BindingList<Instance> Instances = new BindingList<Instance>();

        static public readonly object AccountLock = new object();

        static public bool Launching { get; private set; }
        static bool WaitingForTasks;
        static bool saveSetting = true;
        static bool saveUser = true;
        static bool saveInstance = true;
        static public JsonSerializerOptions defaultJsonOptions = new JsonSerializerOptions { AllowTrailingCommas = true, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };

        static public Process GameProcess;

        static public HomeWindow homeWindow;
        static public AccountDialog accountDialog;
        static public InstanceDialog instanceDialog;

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Contains("console")) Conhost.ShowConsole();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            if (File.Exists("sl_settings.json")) try
                {
                    settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText("sl_settings.json"), defaultJsonOptions);
                    saveSetting = true;
                }
                catch (Exception ex)
                {
                    Logger.Warn($"Couldn't load settings: {ex.Message}");
                    saveSetting = false;
                }
            else
                MessageBox.Show("Hello! Thanks for using my launcher <3\nIt's still in the early stages of development so feedback is needed!\nThere's probably alot of bugs so... Catch them all!");

            if (settings.console) Conhost.ShowConsole();
            Languages.LoadLangs();

            homeWindow = new HomeWindow();
            Lang = Languages.AllLanguages.FirstOrDefault(v => v.lang_id == settings.lang);

            accountDialog = new AccountDialog();
            instanceDialog = new InstanceDialog();

            //LauncherLib.OnAssetUpdate += (index, count) => InvokeUI(() => homeWindow.info_text.Text = string.Format(Lang?.down_asset ?? "Checking assets ({0}/{1})", index, count));
            //LauncherLib.OnClientUpdate += (index, count) => InvokeUI(() => homeWindow.info_text.Text = string.Format(Lang?.down_client ?? "Checking client ({0}/{1})", index, count));
            //LauncherLib.OnJavaUpdate += (index, count) => InvokeUI(() => homeWindow.info_text.Text = string.Format(Lang?.down_java ?? "Checking java ({0}/{1})", index, count));
            LauncherLib.OnDownUpdate += (index, count) => InvokeUI(() => homeWindow.info_text.Text = SharedMethods.ReplaceFormat(Lang?.down_status ?? "Downloading files ({0}/{1})", index, count));
            Accounts.OnAccountFinished += (_) => InvokeUI(() => { homeWindow.info_text.Text = string.Empty; });
            Accounts.OnAccountBegin += (t) => {
                AccountType AccType = Accounts.accountTypes.FirstOrDefault(v => v.id == t);
                InvokeUI(() => { homeWindow.info_text.Text = SharedMethods.ReplaceFormat(Lang?.blogin_info ?? "Logging into {0}...", AccType?.name ?? "Account"); });
            };

            if (settings.check_upd) CheckForUpdates();

            LoadAll();
            Application.Run(homeWindow);
        }

        #region Load/Save
        static public void LoadInstances()
        {
            string InstancesPath = Path.Combine(LauncherLib.GameDir, "sl_instances.json");
            if (File.Exists(InstancesPath)) try
                {
                    List<Instance> LoadUsers = JsonSerializer.Deserialize<List<Instance>>(File.ReadAllText(InstancesPath), defaultJsonOptions);
                    foreach (Instance inst in LoadUsers)
                        if (!Instances.Any(v => v.uuid == inst.uuid))
                            Instances.Add(inst);
                    saveInstance = true;
                }
                catch (Exception ex)
                {
                    Logger.Log($"Couldn't load instances: {ex.Message}");
                    saveInstance = false;
                }
            else
            {
                Instances.Add(new Instance { uuid = "latest-release", name = "Latest Release", version = "latest-release" });
                Instances.Add(new Instance { uuid = "latest-snapshot", name = "Latest Snapshot", version = "latest-snapshot" });
            }
        }
        static public void LoadUsers() {
            string UsersPath = Path.Combine(LauncherLib.GameDir, "sl_users.json");
            if (File.Exists(UsersPath)) try
                {
                    List<User> LoadUsers = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(UsersPath), defaultJsonOptions);
                    foreach (User user in LoadUsers)
                        if (!Users.Any(v => v.uuid == user.uuid))
                            Users.Add(user);
                    saveUser = true;
                }
                catch (Exception ex)
                {
                    Logger.Log($"Couldn't load users: {ex.Message}");
                    saveUser = false;
                }
        }
        static void LoadAll()
        {
            // Set GameDir
            if (!string.IsNullOrWhiteSpace(settings.gamedir)) LauncherLib.SetGameDir(settings.gamedir);

            //Load Both
            LoadInstances();
            LoadUsers();

            GCFlags.LoadCustomFlags();
            Backgrounds.LoadBackgrounds();
            Languages.ApplyLang(Lang, homeWindow, instanceDialog, accountDialog);

            // Load Settings
            homeWindow.gamedir_box.Text = settings.gamedir;
            homeWindow.fullscreen_box.Checked = settings.fullscreen;
            homeWindow.mcarg_box.Text = settings.mc_args;
            homeWindow.jvmarg_box.Text = settings.java_args;
            homeWindow.jre_box.Text = settings.java_path;
            homeWindow.console_box.Checked = settings.console;
            homeWindow.fullargs_box.Checked = settings.show_args;
            homeWindow.asset_box.Checked = settings.check_assets;
            homeWindow.hash_box.Checked = settings.check_hashes;
            homeWindow.updates_box.Checked = settings.check_upd;
            homeWindow.authlib_box.Checked = settings.use_authinjector;
            homeWindow.resx_box.Value = Math.Max(homeWindow.resx_box.Minimum, Math.Min(settings.width, homeWindow.resx_box.Maximum));
            homeWindow.resy_box.Value = Math.Max(homeWindow.resy_box.Minimum, Math.Min(settings.height, homeWindow.resy_box.Maximum));
            homeWindow.ram_bar.Value = Math.Max(homeWindow.ram_bar.Minimum, Math.Min(settings.memory, homeWindow.ram_bar.Maximum));
            homeWindow.ram_box.Value = Math.Max(homeWindow.ram_box.Minimum, Math.Min(settings.memory, homeWindow.ram_box.Maximum));

            // GC Box
            GCTemplate[] GCArray = GCFlags.GCTemplates.ToArray();
            homeWindow.gc_box.Items.AddRange(GCArray);
            instanceDialog.gc_box.Items.AddRange(GCArray);

            if (settings.gc_preset != null)
                homeWindow.gc_box.SelectedItem = GCFlags.GCTemplates.FirstOrDefault(v => v.id == settings.gc_preset);
            else
                homeWindow.gc_box.SelectedItem = NamedClasses["none"];
            homeWindow.lang_box.SelectedItem = Lang;
            homeWindow.account_box.SelectedItem = Users.FirstOrDefault(v => v.uuid == settings.user);
            homeWindow.instance_box.SelectedItem = Instances.FirstOrDefault(v => v.uuid == settings.instance);
            homeWindow.bg_box.SelectedItem = Backgrounds.AllBackgrounds.FirstOrDefault(v => v.Name == settings.bg);
            if (settings.on_launch >= 0 && settings.on_launch < homeWindow.onlaunch_box.Items.Count)
                homeWindow.onlaunch_box.SelectedIndex = settings.on_launch;
            else
                homeWindow.onlaunch_box.SelectedItem = NamedClasses["onlaunch_hide"];
        }
        static public void LoadUsersList()
        {
            
            homeWindow.account_list.Items.Clear();
            homeWindow.account_edit.Enabled = false;
            homeWindow.account_delete.Enabled = false;
            foreach (User user in Users)
                homeWindow.account_list.Items.Add(new ListViewItem
                {
                    ImageKey = user.usertype ?? "offline",
                    Tag = user,
                    Text = user.username
                });
        }
        static public void LoadInstanceList()
        {
            homeWindow.instance_list.Items.Clear();
            homeWindow.instance_edit.Enabled = false;
            homeWindow.instance_remove.Enabled = false;
            foreach (Instance inst in Instances)
                homeWindow.instance_list.Items.Add(new ListViewItem
                {
                    ImageKey = "Grass",
                    Tag = inst,
                    Text = inst.name
                });
        }


        static public void SaveInstances()
        {
            if (!saveInstance) return;
            string InstancesPath = Path.Combine(LauncherLib.GameDir, "sl_instances.json");
            try { Directory.CreateDirectory(LauncherLib.GameDir); File.WriteAllText(InstancesPath, JsonSerializer.Serialize(Instances, defaultJsonOptions)); }
            catch (Exception ex) { Logger.Log($"Couldn't save instances: {ex.Message}"); }
        }
        static public void SaveUsers()
        {
            if (!saveUser) return;
            string UsersPath = Path.Combine(LauncherLib.GameDir, "sl_users.json");
            try { Directory.CreateDirectory(LauncherLib.GameDir); File.WriteAllText(UsersPath, JsonSerializer.Serialize(Users, defaultJsonOptions)); }
            catch (Exception ex) { Logger.Log($"Couldn't save users: {ex.Message}"); }
        }
        static public void SaveSettings()
        {
            if (!saveSetting) return;
            settings.gamedir = LauncherLib.GameDir;
            settings.fullscreen = homeWindow.fullscreen_box.Checked;
            settings.memory = (int)homeWindow.ram_box.Value;
            settings.width = (int)homeWindow.resx_box.Value;
            settings.height = (int)homeWindow.resy_box.Value;
            settings.mc_args = homeWindow.mcarg_box.Text;
            settings.java_args = homeWindow.jvmarg_box.Text;
            settings.java_path = homeWindow.jre_box.Text;
            settings.console = homeWindow.console_box.Checked;
            settings.show_args = homeWindow.fullargs_box.Checked;
            settings.check_assets = homeWindow.asset_box.Checked;
            settings.check_hashes = homeWindow.hash_box.Checked;
            settings.check_upd = homeWindow.updates_box.Checked;
            settings.use_authinjector = homeWindow.authlib_box.Checked;

            settings.on_launch = homeWindow.onlaunch_box.SelectedIndex;
            settings.gc_preset = (homeWindow.gc_box.SelectedItem as GCTemplate)?.id;
            settings.lang = (homeWindow.lang_box.SelectedItem as Language)?.lang_id;
            settings.bg = (homeWindow.bg_box.SelectedItem as Background)?.Name;
            settings.instance = (homeWindow.instance_box.SelectedItem as Instance)?.uuid;
            settings.user = (homeWindow.account_box.SelectedItem as User)?.uuid;
            try { File.WriteAllText("sl_settings.json", JsonSerializer.Serialize(settings, defaultJsonOptions)); } catch (Exception ex) { Logger.Err($"Couldn't save settings: {ex.Message}"); }
        }
        static public void SaveExit()
        {
            SaveSettings();
            SaveUsers();
            SaveInstances();
        }
        #endregion

        static public void CheckForUpdates()
        {
            try
            {
                using (var BWC = new BetterWebClient { DownloadTimeout = 1000 * 30, ConnectTimeout = 1000 * 10 })
                {
                    BWC.Headers.Add("User-Agent", "CSharp/7.3");
                    var Latest = JsonNode.Parse(BWC.DownloadString("https://api.github.com/repos/Lomik8908/Launshell/releases/latest"));
                    if (Latest?["assets"] != null && int.TryParse(Latest?["tag_name"]?.ToString() ?? "0", out int repoVer) && repoVer > AppVersion)
                    {
                        homeWindow.openupd.Text = SharedMethods.ReplaceFormat(Lang?.upd_version ?? "New Version: {0}", Latest?["name"]);
                        homeWindow.openupd.Visible = true;
                        return;
                    }
                }
            }
            catch (Exception ex) { Logger.Warn($"Couldn't check for updates: {ex.Message}"); }
        }
        static public bool HasMSAccount()
        {
            return Users.Any(v => v.usertype == "msa");
        }
        static public void SetGameDir(string sPath)
        {
            LauncherLib.SetGameDir(sPath);
            homeWindow.gamedir_box.Text = LauncherLib.GameDir;
            LoadInstances();
            LoadUsers();
        }
        static public void DownloadFiles(string gameVersion, string gameDir, bool checkHash, bool checkJre, Instance instance)
        {
            if (string.IsNullOrWhiteSpace(gameDir))
                gameDir = LauncherLib.GameDir;

            if (gameVersion == "latest-release")
            {
                gameVersion = LauncherLib.GetVersionsManifest()?["latest"]?["release"]?.ToString() ?? instance.last_version;
                instance.last_version = gameVersion;
            }
            else if (gameVersion == "latest-snapshot")
            {
                gameVersion = LauncherLib.GetVersionsManifest()?["latest"]?["snapshot"]?.ToString() ?? instance.last_version;
                instance.last_version = gameVersion;
            }

            JsonNode mfJson = LauncherLib.GetManifestOf(gameVersion);
            if (mfJson == null) { return; }

            if (mfJson["inheritsFrom"] != null)
            {
                JsonNode ManifestTo = LauncherLib.GetManifestOf(mfJson["inheritsFrom"].ToString());
                if (ManifestTo != null)
                {
                    LauncherLib.MergeNode(ManifestTo, mfJson);
                    string verJson = Path.Combine(LauncherLib.GameDir, "versions", gameVersion, gameVersion + ".json");
                    if (ManifestTo is JsonObject JObject) JObject.Remove("inheritsFrom");
                    try { File.WriteAllText(verJson, JsonSerializer.Serialize(ManifestTo, defaultJsonOptions)); }
                    catch (Exception ex) { Logger.Warn($"Couldn't save manifest: {ex.Message}"); }
                    mfJson = ManifestTo;
                }
            }

            InvokeUI(() => { homeWindow.info_text.Text = Lang?.checking_file ?? "Checking files..."; });
            if (checkJre)
                LauncherLib.CheckJava(mfJson["javaVersion"]?["component"]?.ToString() ?? "jre-legacy", false, checkHash);
            LauncherLib.CheckClasses(mfJson, gameVersion, false, checkHash);
            LauncherLib.CheckAssets(mfJson, gameDir, false, checkHash);
            LauncherLib.DownloadAll();
            LauncherLib.ExtractAll();
            InvokeUI(() => { homeWindow.info_text.Text = string.Empty; });
        }

        static void InvokeUI(Action action) { if (!homeWindow.IsDisposed) homeWindow.Invoke(action); }
        static void EndLaunch(bool keepText = false) {
            Launching = false;
            if (!homeWindow.IsDisposed)
                homeWindow.Invoke(new Action(() => {
                    homeWindow.launch.Text = Lang?.play ?? "Play!";
                    homeWindow.launch.Enabled = true;
                    if (!keepText) homeWindow.info_text.Text = string.Empty;
                    homeWindow.Show();
                }));
        }
        static public void Launch()
        {
            if (WaitingForTasks)
            {
                homeWindow.launch.Enabled = false;
                Launching = false;
                WaitingForTasks = false;
                return;
            }
            if (Launching) {
                homeWindow.launch.Enabled = false;
                Launching = false;
                WaitingForTasks = false;
                LauncherLib.StopOperation();
                return;
            }
            if (!(homeWindow.account_box.SelectedItem is User user))
            {
                MessageBox.Show(Users.Count <= 0 ? Lang.cresel_acc ?? "Create and select an account!" : Lang.sel_acc ?? "Select an account!", "Sandstone Launcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            };
            if (!(homeWindow.instance_box.SelectedItem is Instance instance)) {
                MessageBox.Show(Instances.Count <= 0 ? Lang.cresel_inst ?? "Create and select an instance!" : Lang.sel_inst ?? "Select an instance!", "Sandstone Launcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Launching = true;
            homeWindow.launch.Text = Lang.stop ?? "Stop!";
            //GetValues
            bool checkHash = homeWindow.hash_box.Checked;
            bool checkAsset = homeWindow.asset_box.Checked;
            bool reCli = homeWindow.updateclient_box.Checked;
            bool reJava = homeWindow.updatejava_box.Checked;
            bool reAsset = homeWindow.updateassets_box.Checked;
            bool printArgs = homeWindow.fullargs_box.Checked;
            string custJrePath = homeWindow.jre_box.Text;
            bool isFullscreen = homeWindow.fullscreen_box.Checked;
            bool useInject = homeWindow.authlib_box.Checked;

            int resX = (int)homeWindow.resx_box.Value;
            int resY = (int)homeWindow.resy_box.Value;

            string jvmArgs = homeWindow.jvmarg_box.Text;
            string mcArgs = homeWindow.mcarg_box.Text;

            string gameVersion = instance.version;
            int gameMemory = instance.memory ?? (int)homeWindow.ram_box.Value;
            GCTemplate gc = null;
            if (instance.gc_preset == "default")
                gc = homeWindow.gc_box.SelectedItem as GCTemplate;
            else if (GCFlags.GCTemplates.Any(v => v.id == instance.gc_preset))
                gc = GCFlags.GCTemplates.FirstOrDefault(v => v.id == instance.gc_preset);
            string GameDir = instance.gamedir ?? LauncherLib.GameDir;

            Task.Run(() => {
                if (LauncherLib.OperationRunning)
                {
                    WaitingForTasks = true;
                    while (LauncherLib.OperationRunning && Launching) { Thread.Sleep(200); }
                    if (!Launching) { EndLaunch(true); return; }
                }

                if (gameVersion == "latest-release")
                {
                    gameVersion = LauncherLib.GetVersionsManifest()?["latest"]?["release"]?.ToString() ?? instance.last_version;
                    instance.last_version = gameVersion;
                }
                else if (gameVersion == "latest-snapshot")
                {
                    gameVersion = LauncherLib.GetVersionsManifest()?["latest"]?["snapshot"]?.ToString() ?? instance.last_version;
                    instance.last_version = gameVersion;
                }
                Logger.Log($"Beggining '{gameVersion}' launch");
                JsonNode mfJson = LauncherLib.GetManifestOf(gameVersion, reCli);
                if (mfJson == null || !Launching) { EndLaunch(); return; }

                if (mfJson["inheritsFrom"] != null)
                {
                    JsonNode ManifestTo = LauncherLib.GetManifestOf(mfJson["inheritsFrom"].ToString());
                    if (ManifestTo != null)
                    {
                        LauncherLib.MergeNode(ManifestTo, mfJson);
                        string verJson = Path.Combine(LauncherLib.GameDir, "versions", gameVersion, gameVersion + ".json");
                        if (ManifestTo is JsonObject JObject) JObject.Remove("inheritsFrom");
                        try { File.WriteAllText(verJson, JsonSerializer.Serialize(ManifestTo, defaultJsonOptions)); }
                        catch (Exception ex) { Logger.Warn($"Couldn't save manifest: {ex.Message}"); }
                        mfJson = ManifestTo;
                    }
                }

                bool connected = false;
                if (user.usertype == "msa")
                    connected = WebHelper.IsAvailable("xbox.com");
                else if (user.usertype == "ely")
                    connected = WebHelper.IsAvailable("account.ely.by");

                Logger.Log($"Is connected: {connected}");

                if (connected && !Accounts.CheckValid(user))
                {
                    if (user.usertype == "msa")
                        Accounts.MSBackground(user.refreshToken, user);
                    else if (user.usertype == "ely")
                        Accounts.RefreshEly(user);
                }

                InvokeUI(() => { homeWindow.info_text.Text = Lang?.checking_file ?? "Checking files..."; });
                string JavaExec;
                if (File.Exists(instance.java_path))
                    JavaExec = instance.java_path;
                else if (File.Exists(custJrePath))
                    JavaExec = custJrePath;
                else
                {
                    JavaExec = LauncherLib.CheckJava(mfJson["javaVersion"]?["component"]?.ToString() ?? "jre-legacy", reJava, checkHash);
                    InvokeUI(() => { homeWindow.updatejava_box.Checked = false; });
                }

                if (!File.Exists(JavaExec) || !Launching) { EndLaunch(); return; }

                string ALJPath = Path.Combine(LauncherLib.GameDir, "libraries", "authlib-injector.jar");
                if (useInject && connected && user.usertype == "ely") Accounts.DownloadAuthLibInjector(ALJPath, reCli, checkHash);
                string ClassPath = LauncherLib.CheckClasses(mfJson, gameVersion, reCli, checkHash);
                InvokeUI(() => { homeWindow.updateclient_box.Checked = false; });
                if (!Launching) { EndLaunch(); return; }

                if (checkAsset || reAsset)
                {
                    LauncherLib.CheckAssets(mfJson, GameDir, reAsset, checkHash);
                    InvokeUI(() => { homeWindow.updateassets_box.Checked = false; });
                }
                if (!Launching) { EndLaunch(); return; }

                LauncherLib.DownloadAll();
                LauncherLib.ExtractAll();
                if (!Launching) { EndLaunch(); return; }

                InvokeUI(() => { homeWindow.info_text.Text = string.Empty; });

                List<string> JvmAdditional = new List<string>(CommonFlags);

                //Main
                if (gc?.arguments != null) JvmAdditional.AddRange(gc.arguments);
                if (!string.IsNullOrWhiteSpace(jvmArgs)) JvmAdditional.AddRange(ArgSplitter.Split(jvmArgs));
                if (!string.IsNullOrWhiteSpace(instance.java_args)) JvmAdditional.AddRange(ArgSplitter.Split(instance.java_args));
                //ALJ
                if (useInject && connected && user.usertype == "ely" && File.Exists(ALJPath)) JvmAdditional.AddRange(new string[] { $"-javaagent:{ALJPath}=ely.by", "-Dauthlibinjector.noShowServerName" });

                List<string> JvmArgs = LauncherLib.GetJavaArguments(mfJson, new JvmArguments
                {
                    Classpath = ClassPath,
                    Xmx = gameMemory.ToString(),
                    Xms = Math.Min(gameMemory, 2048).ToString(),
                    Natives = Path.Combine(LauncherLib.GameDir, "versions", gameVersion, "natives"),
                    Additional = JvmAdditional
                });

                List<string> GameAdditional = new List<string>();

                if (!string.IsNullOrWhiteSpace(mcArgs))
                    GameAdditional.AddRange(ArgSplitter.Split(mcArgs));

                if (!string.IsNullOrWhiteSpace(instance.mc_args))
                    GameAdditional.AddRange(ArgSplitter.Split(instance.mc_args));

                List<string> GameArgs = LauncherLib.GetGameArguments(mfJson, new GameArguments
                {
                    Username = user.username,
                    UserToken = user.accessToken,
                    ClientId = settings.clientId,
                    XboxId = user.xuid,
                    UserId = user.uuid,
                    Width = instance.width ?? resX,
                    Height = instance.height ?? resY,
                    Fullscreen = isFullscreen,
                    GameDir = GameDir,
                    Additional = GameAdditional
                });

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = JavaExec,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    WorkingDirectory = GameDir,
                    Arguments = $"{string.Join(" ", JvmArgs.Select(a => $"\"{a}\""))} {mfJson["mainClass"]} {string.Join(" ", GameArgs.Select(a => $"\"{a}\""))}"
                };

                if (printArgs)
                    Logger.Log($"Full command: {JavaExec} {startInfo.Arguments}");

                try
                {
                    GC.Collect();
                    Directory.CreateDirectory(GameDir);
                    Logger.Log("Launching game!");
                    using (var gameProcess = new Process { StartInfo = startInfo })
                    {
                        GameProcess = gameProcess;
                        gameProcess.Start();
                        gameProcess.OutputDataReceived += (_, e) => Console.Out.WriteLine(e.Data);
                        gameProcess.ErrorDataReceived += (_, e) => Logger.ErrorLine(e.Data);
                        gameProcess.BeginErrorReadLine();
                        gameProcess.BeginOutputReadLine();
                        InvokeUI(() => {
                            if (homeWindow.onlaunch_box.SelectedIndex == 0) homeWindow.Hide();
                            if (homeWindow.onlaunch_box.SelectedIndex == 1) homeWindow.Close();
                        });

                        gameProcess.WaitForExit();
                        Logger.Log($"Game exited with code: {gameProcess.ExitCode}");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Err($"Game Process error: {ex.Message}");
                }
                EndLaunch();
            });
        }

        internal static void StopLaunch()
        {
            Launching = false;
        }
    }

    class NameClass {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    class Settings
    {
        public string clientId { get; set; } = Guid.NewGuid().ToString();
        public string instance { get; set; }
        public string user { get; set; }
        public string gamedir { get; set; } = LauncherLib.GameDir;
        public int width { get; set; } = (int)(Screen.PrimaryScreen.Bounds.Width * 0.6f);
        public int height { get; set; } = (int)(Screen.PrimaryScreen.Bounds.Height * 0.6f);
        public bool fullscreen { get; set; }
        public int memory { get; set; } = Math.Min(1024, (int)(Program.pcInfo.TotalPhysicalMemory/(1024*1024)));
        public string gc_preset { get; set; } = "m_g1gc";
        public string mc_args { get; set; }
        public string java_args { get; set; }
        public int on_launch { get; set; } = 0;
        public bool console { get; set; }
        public bool show_args { get; set; }
        public bool check_assets { get; set; } = true;
        public bool check_hashes { get; set; } = true;
        public bool check_upd { get; set; } = true;
        public string lang { get; set; } = "en_us";
        public string bg { get; set; } = "Burberry";
        public bool use_authinjector { get; set; } = true;
        public string java_path { get; set; }
    }
    public class Instance
    {
        public string name { get; set; }
        public string uuid { get; set; }
        public string version { get; set; }
        public string last_version { get; set; }
        public string gamedir { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public int? memory { get; set; }
        public string gc_preset { get; set; } = "default";
        public string mc_args { get; set; }
        public string java_args { get; set; }
        public string java_path { get; set; }
    }
    public class User
    {
        public string username { get; set; }
        public string uuid { get; set; }
        public string xuid { get; set; }
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public string usertype { get; set; }
        public override string ToString()
        {
            if (usertype == "msa")
                return $"[MS] {username}";
            if (usertype == "ely")
                return $"[ELY] {username}";
            if (usertype == "custom")
                return $"[>] {username}";
            return username;
        }
    }
}
