using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace Sandstone_Launcher
{
    static class LibUtil
    {
        static public void ExtractNativeJar(string file, string dest, bool rewrite = false)
        {
            if (!File.Exists(file)) return;
            try
            {
                using (ZipArchive zip = ZipFile.OpenRead(file))
                {
                    foreach (ZipArchiveEntry entry in zip.Entries)
                    {
                        string path = Path.Combine(dest, entry.FullName);
                        if (!rewrite && File.Exists(path)) continue;
                        if (entry.FullName.Contains("/")) continue;
                        if (Path.GetExtension(entry.FullName).ToLowerInvariant() != ".dll") continue;
                        try {
                            Directory.CreateDirectory(Directory.GetParent(path).FullName);
                            entry.ExtractToFile(path, true);
                        } catch (Exception ex) { Logger.Warn($"Failed to extract: {ex.Message}"); }
                    }
                }
            }
            catch (Exception ex) { Logger.Warn($"Failed to open zip: {ex.Message}"); }
        }
        static public string ComputeSHA1(string Path)
        {
            if (!File.Exists(Path)) return null;
            using (var Hash = SHA1.Create())
            using (var Filed = File.OpenRead(Path))
                return BitConverter.ToString(Hash.ComputeHash(Filed)).Replace("-", "").ToLowerInvariant();
        }
        static public bool HashDoRewrite(JsonNode item, string FilePath)
        {
            return item?["sha1"] != null && File.Exists(FilePath) && ComputeSHA1(FilePath) != item["sha1"].ToString();
        }
    }
    public class BetterWebClient : WebClient
    {
        public int ConnectTimeout { get; set; } = 1000 * 30;
        public int DownloadTimeout { get; set; } = 1000 * 60;
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request != null) {
                request.Timeout = ConnectTimeout;
                if (request is HttpWebRequest httpRequest)
                    httpRequest.ReadWriteTimeout = DownloadTimeout;
            }
            return request;
        }
    }
    static class LauncherLib
    {
        static string Appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        static BetterWebClient Client = new BetterWebClient();

        static public string SaveFolder = Path.Combine(Appdata, "SandstoneLauncher");
        static public string GameDir { get; private set; } = Path.Combine(Appdata, ".minecraft");
        static public readonly string DefaultGameDir = Path.Combine(Appdata, ".minecraft");
        static public bool OperationRunning { get; private set; } = false;

        static private readonly object downlock = new object();
        static private Dictionary<string, string> PendingDownloads = new Dictionary<string, string>();
        static private readonly object extrlock = new object();
        static private HashSet<ExtractFile> ExtractList = new HashSet<ExtractFile>();

        static public string VersionManifest = "https://piston-meta.mojang.com/mc/game/version_manifest_v2.json";
        static public string JavaManifest = "https://piston-meta.mojang.com/v1/products/java-runtime/2ec0cc96c44e5a76b9c8b7c39df7210883d12871/all.json";

        //static public Action<int, int> OnJavaUpdate;
        //static public Action<int, int> OnClientUpdate;
        //static public Action<int, int> OnAssetUpdate;
        static public Action<int, int> OnDownUpdate;

        static LauncherLib()
        {
 
            Client.Headers.Add("User-Agent", "CSharp/7.3");
            Directory.CreateDirectory(SaveFolder);
        }

        static public void SetGameDir(string path="")
        {
            if (string.IsNullOrWhiteSpace(path))
                GameDir = DefaultGameDir;
            else
                GameDir = path;
            Directory.CreateDirectory(GameDir);
            string Profiles = Path.Combine(GameDir, "launcher_profiles.json");
            if (!File.Exists(Profiles)) File.WriteAllText(Profiles, "{\"profiles\" : {}, \"version\" : 3}");
        }

        //Manifests And Stuff
        static JsonNode VersionManifestNode = null;
        static public JsonNode GetVersionsManifest()
        {
            //string JPath = Path.Combine(SaveFolder, "version_manifest_v2.json");
            //if (!DownloadedVersionManifest) try { Client.DownloadFile(VersionManifest, JPath); DownloadedVersionManifest = true; } catch (Exception ex) { Logger.Err($"Could not download version_manifest_v2.json: {ex.Message}"); }
            //if (File.Exists(JPath)) 
            try
            {
                VersionManifestNode = JsonNode.Parse(Client.DownloadString(VersionManifest));
            }
            catch (Exception ex)
            {
                Logger.Err($"Could not get version manifest: {ex.Message}");
            }
            return VersionManifestNode;
        }
        static public JsonNode GetManifestOf(string Version, bool Rewrite = false)
        {
            if (Version == null) return null;
            string verLoc = Path.Combine(GameDir, "versions", Version);
            string verJson = Path.Combine(verLoc, Version + ".json");
            if (!File.Exists(verJson) || Rewrite)
            {
                JsonArray verArray = GetVersionsManifest()?["versions"]?.AsArray();
                JsonNode ver = verArray?.FirstOrDefault(v => v["id"].ToString() == Version);
                if (ver != null) try
                    {
                        Directory.CreateDirectory(verLoc);
                        Client.DownloadFile(ver["url"].ToString(), verJson);
                    }
                    catch (Exception ex)
                    {
                        Logger.Err($"Could not download {Version}.json: {ex.Message}");
                    }
            }
            if (File.Exists(verJson)) try
                {
                    JsonNode node = JsonNode.Parse(File.ReadAllText(verJson));
                    return node;
                }
                catch (Exception ex)
                {
                    Logger.Err($"{Version} parse error: {ex.Message}");
                }
            return null;
        }
        static JsonNode JavaManifestNode = null;
        static public JsonNode GetJavaManifest()
        {   
            try
            {
                JavaManifestNode = JsonNode.Parse(Client.DownloadString(JavaManifest));
            }
            catch (Exception ex)
            {
                Logger.Err($"Could not get java manifest: {ex.Message}");
            }
            return JavaManifestNode;
        }

        //checking game
        static public string CheckJava(string Version = "jre-legacy", bool Rewrite = false, bool CheckHash = false)
        {
            OperationRunning = true;
            string osArch = Environment.Is64BitOperatingSystem ? "x64" : "x86";
            string mfPath = Path.Combine(SaveFolder, "java", $"{Version}-{osArch}.json");
            if (!File.Exists(mfPath) || Rewrite) try
                {
                    string manifestUri = GetJavaManifest()?[$"windows-{osArch}"]?[Version]?[0]?["manifest"]?["url"]?.ToString();
                    if (manifestUri != null)
                    {
                        //Logger.Log($"Pending {Version} manifest");
                        AddDownload(manifestUri, mfPath);
                    }
                }
                catch (Exception ex) { Logger.Err($"Couldn't download {Version} manifest: {ex.Message}"); }
            if (!File.Exists(mfPath)) { OperationRunning = false; return null; }
            JsonNode jManifest = null;
            try { jManifest = JsonNode.Parse(File.ReadAllText(mfPath)); } catch { }
            if (jManifest?["files"] == null) { OperationRunning = false; return null; }
            JsonObject jFiles = jManifest["files"].AsObject();
            //int index = 0;
            //DateTime lastCall = DateTime.UtcNow;
            foreach (var jFile in jFiles)
            {
                if (!OperationRunning) break;
                //index++;
                //if (DateTime.UtcNow - lastCall > TimeSpan.FromMilliseconds(250) || index == jFiles.Count) { lastCall = DateTime.UtcNow; Task.Run(() => OnJavaUpdate?.Invoke(index, jFiles.Count)); }
                if (jFile.Value?["type"]?.ToString() == "dictionary" || jFile.Value?["downloads"]?["raw"] == null) continue;
                string FilePath = Path.Combine(SaveFolder, "java", Version, jFile.Key);
                if (!File.Exists(FilePath) || Rewrite || (CheckHash && LibUtil.HashDoRewrite(jFile.Value["downloads"]["raw"], FilePath)))
                {
                    //Logger.Log($"Pending {Version}/{jFile.Key}");
                    AddDownload(jFile.Value["downloads"]["raw"]["url"].ToString(), FilePath);
                }
            }
            OperationRunning = false;
            string javaPath = Path.Combine(SaveFolder, "java", Version, "bin", "javaw.exe");
            return File.Exists(javaPath) ? javaPath : null;
        }
        static public string CheckClasses(JsonNode manifest, string Version, bool Rewrite = false, bool CheckHash = false)
        {
            OperationRunning = true;
            string VersionPath = Path.Combine(GameDir, "versions", Version, Version + ".jar");
            string NativeDir = Path.Combine(GameDir, "versions", Version, "natives");
            if (!File.Exists(VersionPath) || Rewrite || (CheckHash && LibUtil.HashDoRewrite(manifest["downloads"]["client"], VersionPath)))
                AddDownload(manifest["downloads"]["client"]["url"].ToString(), VersionPath);
            Directory.CreateDirectory(NativeDir);

            List<string> ClassPath = new List<string>();
            JsonArray allLibs = manifest["libraries"].AsArray();
            //int index = 0;
            //DateTime lastCall = DateTime.UtcNow;
            foreach (JsonNode lib in allLibs)
            {
                if (!OperationRunning) break;
                //index++;
                //if (DateTime.UtcNow - lastCall > TimeSpan.FromMilliseconds(250) || index == allLibs.Count) { lastCall = DateTime.UtcNow; Task.Run(() => OnClientUpdate?.Invoke(index, allLibs.Count)); }
                if (lib["name"] == null) continue;
                if (lib["rules"] != null)
                {
                    bool RuleBad = true;
                    foreach (JsonNode Rule in lib["rules"].AsArray())
                        if (!RuleAllows(Rule)) { RuleBad = false; break; }
                    if (RuleBad) continue;
                }
                //Classic Download
                string[] clibSpl = lib["name"].ToString().Split(':');
                var clibSplit = clibSpl[0].Split('.').Append(clibSpl[1]).Append(clibSpl[2]);
                string clibPath = Path.Combine(Path.Combine(clibSplit.ToArray()), $"{clibSpl[1]}-{clibSpl[2]}.jar");
                //Newer Download
                if (lib["downloads"] != null)
                {
                    if (lib["downloads"]?["artifact"] != null)
                    {
                        string libPath = Path.Combine(GameDir, "libraries", lib["downloads"]["artifact"]["path"]?.ToString() ?? clibPath);
                        if (!File.Exists(libPath) || Rewrite || (CheckHash && LibUtil.HashDoRewrite(lib["downloads"]["artifact"], libPath)))
                        {
                            //Logger.Log($"Pending artifact {lib["name"]}");
                            AddDownload(lib["downloads"]["artifact"]["url"].ToString(), libPath);
                        }
                        if (!ClassPath.Contains(libPath)) ClassPath.Add(libPath);
                    }
                    if (lib["downloads"]?["classifiers"] != null && lib["natives"]?["windows"] != null)
                    {
                        string ClassKey = lib["natives"]["windows"].ToString().Replace("${arch}", Environment.Is64BitOperatingSystem ? "64" : "32");
                        JsonNode Classifier = lib["downloads"]["classifiers"][ClassKey];
                        if (Classifier == null) continue;
                        string libPath = Path.Combine(GameDir, "libraries", Classifier["path"]?.ToString() ?? clibPath);
                        bool ReExtract = Rewrite;
                        if (!File.Exists(libPath) || Rewrite || (CheckHash && LibUtil.HashDoRewrite(Classifier, libPath)))
                        {
                            //Logger.Log($"Pending classifier {lib["name"]}");
                            AddDownload(Classifier["url"].ToString(), libPath);
                            ReExtract = true;
                        }
                        if (lib["extract"] != null) AddExtract(libPath, NativeDir, ReExtract);
                        else if (!ClassPath.Contains(libPath)) ClassPath.Add(libPath);
                    }
                }
                else
                {
                    string libPath = Path.Combine(GameDir, "libraries", clibPath);
                    if ((!File.Exists(libPath) || Rewrite || (CheckHash && LibUtil.HashDoRewrite(lib, libPath))) && lib["url"] != null)
                    {
                        string libUrl = lib["url"].ToString() + clibPath;
                        //Logger.Log($"Pending {lib["name"]}");
                        AddDownload(libUrl, libPath);
                    }
                    if (!ClassPath.Contains(libPath)) ClassPath.Add(libPath);
                }
            }
            OperationRunning = false;
            ClassPath.Add(VersionPath);
            return string.Join(";", ClassPath);
        }
        static public void CheckAssets(JsonNode manifest, string InstanceDir, bool Rewrite = false, bool CheckHash = false)
        {
            OperationRunning = true;
            if (manifest?["assetIndex"] == null) { OperationRunning = false; return; }
            ;
            string assetId = manifest["assetIndex"]["id"].ToString();
            string indexPath = Path.Combine(GameDir, "assets", "indexes", assetId + ".json");
            if (!File.Exists(indexPath) || Rewrite || (CheckHash && LibUtil.HashDoRewrite(manifest["assetIndex"], indexPath)))
            {
                //Logger.Log($"Pending {assetId} assetIndex");
                AddDownload(manifest["assetIndex"]["url"].ToString(), indexPath);
            }
            if (!File.Exists(indexPath)) { OperationRunning = false; return; }
            JsonNode AssetIndex = null;
            try { AssetIndex = JsonNode.Parse(File.ReadAllText(indexPath)); } catch { }
            if (AssetIndex?["objects"] == null) { OperationRunning = false; return; }
            JsonObject objects = AssetIndex["objects"].AsObject();
            //int index = 0;
            //DateTime lastCall = DateTime.UtcNow;
            foreach (var aFile in objects)
            {
                if (!OperationRunning) break;
                //index++;
                //if (DateTime.UtcNow - lastCall > TimeSpan.FromMilliseconds(250) || index == objects.Count) { lastCall = DateTime.UtcNow; Task.Run(() => OnAssetUpdate?.Invoke(index, objects.Count)); }
                string AssetPath;
                string hash = aFile.Value["hash"].ToString();
                string subhash = hash.Substring(0, 2);
                if (((bool?)AssetIndex["map_to_resources"]?.AsValue()) == true)
                {
                    AssetPath = Path.Combine(InstanceDir, "resources", aFile.Key);
                }
                else if (((bool?)AssetIndex["virtual"]?.AsValue()) == true)
                {
                    AssetPath = Path.Combine(GameDir, "assets", "virtual", assetId, aFile.Key);
                }
                else
                {
                    AssetPath = Path.Combine(GameDir, "assets", "objects", subhash, hash);
                }
                if (!File.Exists(AssetPath) || Rewrite || (CheckHash && LibUtil.ComputeSHA1(AssetPath) != hash))
                {
                    //Logger.Log($"Pending asset {aFile.Key}");
                    AddDownload($"https://resources.download.minecraft.net/{subhash}/{hash}", AssetPath);
                }
            }
            OperationRunning = false;
        }

        //Downloading
        static public void AddDownload(string Url, string FilePath)
        {
            lock (downlock)
                if (!PendingDownloads.ContainsKey(FilePath))
                    PendingDownloads.Add(FilePath, Url);
        }
        static public void AddExtract(string Path, string Destination, bool Rewrite)
        {
            lock (extrlock)
                ExtractList.Add(new ExtractFile { Path = Path, Dest = Destination, Rewrite = Rewrite });
        }
        static public void DownloadAll()
        {
            OperationRunning = true;
            int index = 0;
            DateTime lastCall = DateTime.UtcNow;
            lock (downlock)
            {
                foreach (var kp in PendingDownloads)
                {
                    if (!OperationRunning) break;
                    index++;
                    if (DateTime.UtcNow - lastCall > TimeSpan.FromMilliseconds(250) || index == PendingDownloads.Count) { lastCall = DateTime.UtcNow; OnDownUpdate?.Invoke(index, PendingDownloads.Count); }
                    try
                    {
                        Logger.Log($"Downloading file {Path.GetFileName(kp.Key)}");
                        Directory.CreateDirectory(Directory.GetParent(kp.Key).FullName);
                        Client.DownloadFile(kp.Value, kp.Key);
                    }
                    catch (Exception ex) { Logger.Warn($"Couldn't download file '{Path.GetFileName(kp.Key)}': {ex.Message}"); }
                }
                PendingDownloads.Clear();
            }
            OperationRunning = false;
        }
        static public void ExtractAll()
        {
            OperationRunning = true;
            lock (extrlock)
            {
                foreach (ExtractFile extr in ExtractList)
                {
                    if (!OperationRunning) break;
                    LibUtil.ExtractNativeJar(extr.Path, extr.Dest, extr.Rewrite);
                }
                ExtractList.Clear();
            }
            OperationRunning = false;
        }
        static public void ClearDownloads()
        {
            lock (downlock)
                PendingDownloads.Clear();
        }
        static public void ClearExtracts()
        {
            lock (extrlock)
                ExtractList.Clear();
        }

        //Getting
        static public string GetAssetFolder(JsonNode Manifest, string InstanceDir)
        {
            if (Manifest["assetIndex"]?["id"] == null) return Path.Combine(GameDir, "assets");
            string indexPath = Path.Combine(GameDir, "assets", "indexes", Manifest["assetIndex"]["id"].ToString() + ".json");
            if (!File.Exists(indexPath)) return Path.Combine(GameDir, "assets");
            JsonNode AssetIndex = null;
            try { AssetIndex = JsonNode.Parse(File.ReadAllText(indexPath)); } catch { }
            if ((bool?)AssetIndex?["map_to_resources"]?.AsValue() == true)
            {
                return Path.Combine(InstanceDir, "resources");
            }
            else if ((bool?)AssetIndex?["virtual"]?.AsValue() == true)
            {
                return Path.Combine(GameDir, "assets", "virtual", Manifest?["assetIndex"]?["id"]?.ToString());
            }
            return Path.Combine(GameDir, "assets");
        }
        static public List<string> GetJavaArguments(JsonNode Manifest, JvmArguments Args)
        {
            List<string> Arguments = new List<string> { "-Xmx${xmx}M", "-Xms${xms}M" };
            if (Manifest["arguments"]?["jvm"] != null)
            {
                foreach (JsonNode Argument in Manifest["arguments"]["jvm"].AsArray())
                {
                    if (Argument.GetValueKind() == JsonValueKind.String)
                    {
                        if (!string.IsNullOrWhiteSpace(Argument?.ToString()))
                            Arguments.Add(Argument.ToString());
                    }
                    else if (Argument.GetValueKind() == JsonValueKind.Object)
                    {
                        if (Argument["rules"] != null) continue;
                        if (!string.IsNullOrWhiteSpace(Argument["value"]?.ToString()))
                            Arguments.Add(Argument["value"].ToString());
                    }
                }
            }
            else
            {
                Arguments.AddRange(new string[] { "-XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump", "-Djava.library.path=${natives_directory}", "-cp", "${classpath}" });
                if (!Environment.Is64BitOperatingSystem)
                    Arguments.Add("-Xss1M");
                if (Environment.OSVersion.Version.Major == 10)
                    Arguments.AddRange(new string[] { "-Dos.name=Windows 10", "-Dos.version=10.0" });
            }

            if (Args.Additional != null && Args.Additional.Count() > 0)
            {
                Arguments.AddRange(Args.Additional);
            }

            Dictionary<string, string> Replacements = new Dictionary<string, string> {
                { "${xmx}", Args.Xmx ?? "1024" },
                { "${xms}", Args.Xms ?? Args.Xmx ?? "512" },
                { "${natives_directory}", Args.Natives },
                { "${classpath}", Args.Classpath },
                { "${launcher_name}", "sandstone" },
                { "${launcher_version}", Program.AppVersion.ToString() },
                { "${version_name}", Manifest["id"].ToString() },
                { "${library_directory}", Path.Combine(GameDir, "libraries") },
                { "${classpath_separator}", ";" }
            };

            foreach (var Replacement in Replacements)
            {
                for (int i = 0; i < Arguments.Count; i++)
                {
                    Arguments[i] = Arguments[i].Replace(Replacement.Key, Replacement.Value);
                }
            }

            return Arguments;
        }
        static public List<string> GetGameArguments(JsonNode Manifest, GameArguments Args)
        {
            List<string> Arguments = new List<string>();
            if (Manifest["arguments"]?["game"] != null)
            {
                foreach (JsonNode Argument in Manifest["arguments"]["game"].AsArray())
                {
                    if (Argument.GetValueKind() == JsonValueKind.String)
                    {
                        if (!string.IsNullOrWhiteSpace(Argument?.ToString()))
                            Arguments.Add(Argument.ToString());
                    }
                    else if (Argument.GetValueKind() == JsonValueKind.Object)
                    {
                        if (Argument["rules"] != null) continue;
                        if (Argument["value"]?.GetValueKind() == JsonValueKind.String && !string.IsNullOrWhiteSpace(Argument["value"].ToString()))
                            Arguments.Add(Argument["value"].ToString());
                    }
                }
            }
            else if (Manifest["minecraftArguments"] != null)
            {
                Arguments.AddRange(Manifest["minecraftArguments"].ToString().Split(' '));
            }

            if (Args.Additional != null)
            {
                Arguments.AddRange(Args.Additional);
            }

            string AssetsDir = GetAssetFolder(Manifest, Args.GameDir ?? GameDir);
            Dictionary<string, string> Replacements = new Dictionary<string, string> {
                { "${auth_player_name}", Args.Username ?? "Player" },
                { "${version_name}", Manifest["id"].ToString() },
                { "${game_directory}", Args.GameDir ?? GameDir },
                { "${assets_root}", AssetsDir },
                { "${game_assets}", AssetsDir },
                { "${assets_index_name}", Manifest["assetIndex"]?["id"]?.ToString() },
                { "${auth_uuid}", Args.UserId ?? "0" },
                { "${auth_xuid}", Args.XboxId ?? "0" },
                { "${clientid}", Args.ClientId ?? "" },
                { "${auth_session}", Args.UserToken ?? "0" },
                { "${auth_access_token}", Args.UserToken ?? "0" },
                { "${user_type}", "msa" },
                { "${version_type}", Manifest["type"]?.ToString() ?? "unknown" },
                { "${user_properties}", "{}" }
            };

            foreach (var Replacement in Replacements)
            {
                for (int i = 0; i < Arguments.Count; i++)
                {
                    Arguments[i] = Arguments[i].Replace(Replacement.Key, Replacement.Value ?? "");
                }
            }

            if (Args.Fullscreen) Arguments.Add("--fullscreen");
            if (Args.Width != null)
            {
                Arguments.Add("--width");
                Arguments.Add(Args.Width.ToString());
            }
            if (Args.Height != null)
            {
                Arguments.Add("--height");
                Arguments.Add(Args.Height.ToString());
            }
            return Arguments;
        }
        static public List<string> GetInstalledVersions()
        {
            List<string> Versions = new List<string>();
            string VersPath = Path.Combine(GameDir, "versions");
            if (Directory.Exists(VersPath))
                foreach (string Dir in Directory.GetDirectories(VersPath))
                {
                    DirectoryInfo DirInfo = new DirectoryInfo(Dir);
                    string VerPath = Path.Combine(Dir, $"{DirInfo.Name}.json");
                    if (File.Exists(VerPath))
                        Versions.Add(DirInfo.Name);
                }
            return Versions;
        }

        //Other
        static public bool RuleAllows(JsonNode Rule)
        {
            bool Allows = (Rule?["action"]?.ToString() ?? "allow") == "allow";
            if (Rule["os"] != null)
            {
                if (Rule["os"]?["name"] != null && Rule["os"]["name"].ToString() != "windows")
                    if (Rule["os"]?["version"] != null)
                    {
                        if (Regex.IsMatch(Environment.OSVersion.Version.ToString(), Rule["os"]["version"].ToString()))
                            return Allows;
                    }
                    else
                        return Allows;
                if (Rule["os"]?["arch"] != null && Rule["os"]["arch"].ToString() != (Environment.Is64BitOperatingSystem ? "x64" : "x86"))
                    return Allows;
            }
            return !Allows;
        }
        static public void MergeNode(JsonNode To, JsonNode From)
        {
            if (To == null || From == null) return;
            if (From.GetValueKind() == JsonValueKind.Object && To.GetValueKind() == JsonValueKind.Object)
            {
                foreach (var Element in From.AsObject())
                    if (To[Element.Key]?.GetValueKind() == JsonValueKind.Object || To[Element.Key]?.GetValueKind() == JsonValueKind.Array)
                        MergeNode(To[Element.Key], Element.Value);
                    else
                        To[Element.Key] = Element.Value?.DeepClone();
            }
            else if (From.GetValueKind() == JsonValueKind.Array && To.GetValueKind() == JsonValueKind.Array)
            {
                foreach (var Element in From.AsArray())
                    To.AsArray().Add(Element?.DeepClone());
            }
            //if (To == null) return From?.DeepClone();
            //if (From == null) return null;
            //JsonNode Into = To.DeepClone();
            //if (From.GetValueKind() == JsonValueKind.Object && Into.GetValueKind() == JsonValueKind.Object)
            //{
            //    foreach (var Element in From.AsObject())
            //        if (Into[Element.Key]?.GetValueKind() == JsonValueKind.Object || Into[Element.Key]?.GetValueKind() == JsonValueKind.Array)
            //            Into[Element.Key] = MergeNode(Into[Element.Key], Element.Value);
            //        else
            //            Into[Element.Key] = Element.Value?.DeepClone();
            //}
            //else if (From.GetValueKind() == JsonValueKind.Array && Into.GetValueKind() == JsonValueKind.Array)
            //{
            //    foreach (var Element in From.AsArray())
            //        Into.AsArray().Add(Element?.DeepClone());
            //}
            //return Into;
        }
        //static public void WaitForOperations() { while (OperationRunning) Thread.Sleep(200); }
        static public void StopOperation() => OperationRunning = false;

        private class ExtractFile
        {
            public string Path;
            public string Dest;
            public bool Rewrite;
        }
}
public class JvmArguments
    {
        public string Xmx;
        public string Xms;
        public string Natives;
        public string Classpath;
        public IEnumerable<string> Additional;
    }
    
    public class GameArguments
    {
        public string Username;
        public string UserId;
        public string UserToken;
        public string XboxId;
        public string ClientId;
        public string GameDir;
        public bool Fullscreen;
        public int? Width;
        public int? Height;
        public IEnumerable<string> Additional;
    }
}
