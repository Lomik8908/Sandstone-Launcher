using Sandstone_Launcher.Misc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Sandstone_Launcher
{
    static class Accounts
    {
        private class Urls
        {
            static public readonly string MSFlowEndUri = "https://login.live.com/oauth20_desktop.srf";
            static public readonly string MSFlowUri = $"https://login.microsoftonline.com/consumers/oauth2/v2.0/authorize?client_id=00000000402b5328&response_type=code&redirect_uri={MSFlowEndUri}&scope=service::user.auth.xboxlive.com::MBI_SSL";
            static public readonly string MSTokenUri = "https://login.live.com/oauth20_token.srf";
            static public readonly string XLiveLoginUri = "https://user.auth.xboxlive.com/user/authenticate";
            static public readonly string XSTSLoginUri = "https://xsts.auth.xboxlive.com/xsts/authorize";
            static public readonly string MCLoginUri = "https://api.minecraftservices.com/authentication/login_with_xbox";
            static public readonly string MSOwnership = "https://api.minecraftservices.com/entitlements/mcstore";
            static public readonly string MCProfile = "https://api.minecraftservices.com/minecraft/profile";

            static public readonly string ElyAuthUri = "https://authserver.ely.by/auth/authenticate";
            static public readonly string ElyRefreshUri = "https://authserver.ely.by/auth/refresh";
            static public readonly string ElyValidUri = "https://authserver.ely.by/auth/validate";
        }
        static private readonly HttpClient httpClient = new HttpClient();
        static public Action<string> OnAccountFinished;
        static public Action<string> OnAccountBegin;
        static public readonly List<AccountType> accountTypes = new List<AccountType> {
            new AccountType { id = "msa", name = "Microsoft" },
            new AccountType { id = "ely", name = "Ely.by" },
            new AccountType { id = "offline", name = "Offline" }
        };

        static public void MSBeginFlow() {
            using (var BrowserWindow = new AccountFlowWindow { LoginUrl = Urls.MSFlowUri, StopOnUrl = Urls.MSFlowEndUri, Text = SharedMethods.ReplaceFormat(Program.Lang?.login_into ?? "Login via {0}", "Microsoft") })
            {
                BrowserWindow.OnCodeReceived += (code) => MSBackground(code);
                BrowserWindow.ShowDialog();
            }
        }
        static public void ElyBeginFlow()
        {
            OnAccountBegin?.Invoke("ely");
            using (var Window = new ElyFlow { Text = SharedMethods.ReplaceFormat(Program.Lang?.login_into ?? "Login via {0}", "Ely.by") })
            {
                DialogResult result = Window.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ElyContinue(Window.username.Text, Window.password.Text);
                    Window.username.Text = "";
                    Window.password.Text = "";
                }
            }
            OnAccountFinished?.Invoke("ely");
        }

        static private void ElyContinue(string Username, string Pass, string TFA = null)
        {
            try
            {
                string Secret = Pass;
                if (!string.IsNullOrEmpty(TFA)) Secret += ":" + TFA;
                var ElyResponse = httpClient.PostAsync(Urls.ElyAuthUri, new StringContent(JsonSerializer.Serialize(new { username = Username, password = Secret, clientToken = Program.settings.clientId }), Encoding.UTF8, "application/json")).Result;
                
                var JNode = JsonNode.Parse(ElyResponse.Content.ReadAsStringAsync().Result);
                if (JNode?["errorMessage"]?.ToString() == "Account protected with two factor auth.")
                {
                    string code = GetElyCode();
                    if (code == null) return;
                    ElyContinue(Username, Pass, code);
                }
                else if (ElyResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized && !string.IsNullOrEmpty(TFA))
                {
                    string code = GetElyCode();
                    if (code == null) return;
                    ElyContinue(Username, Pass, code);
                }
                else if (JNode?["accessToken"] != null)
                {
                    lock (Program.AccountLock)
                        Program.Users.Add(new User
                        {
                            username = JNode?["selectedProfile"]?["name"]?.ToString(),
                            uuid = JNode?["selectedProfile"]?["id"]?.ToString(),
                            accessToken = JNode?["accessToken"]?.ToString(),
                            usertype = "ely"
                        });
                    Program.LoadUsersList();
                }
                else
                    MessageBox.Show(Program.Lang?.ely_err ?? "Couldn't add an Ely.by account!\nCheck if the info was valid!", "Sandstone Launcher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) { Logger.Err($"Error occured during Ely.by login: {ex.Message}"); }
        }
        static public void RefreshEly(User user) {
            if (user.usertype == "ely") try
                {
                    var ElyResponse = httpClient.PostAsync(Urls.ElyAuthUri, new StringContent(JsonSerializer.Serialize(new { user.accessToken, clientToken = Program.settings.clientId }), Encoding.UTF8, "application/json")).Result;
                    if (ElyResponse.IsSuccessStatusCode)
                    {
                        var JNode = JsonNode.Parse(ElyResponse.Content.ReadAsStringAsync().Result);
                        if (JNode?["accessToken"] != null)
                        {
                            user.username = JNode?["selectedProfile"]?["name"]?.ToString();
                            user.uuid = JNode?["selectedProfile"]?["id"]?.ToString();
                            user.accessToken = JNode?["accessToken"]?.ToString();
                            Program.LoadUsersList();
                        }
                    }
                }
                catch (Exception ex) { Logger.Warn($"Couldn't refresh Ely.by token: {ex.Message}"); }
        }
        static private string GetElyCode()
        {
            string code = null;
            using (var win = new Ely2Step())
            {
                win.tfa_box.TextChanged += (_, __) =>
                {
                    if (win.tfa_box.Text.Length >= 6 && Regex.IsMatch(win.tfa_box.Text, "\\d*"))
                    {
                        win.Close();
                        code = win.tfa_box.Text.Substring(0, 6);
                        win.tfa_box.Text = "";
                    }
                };
                win.ShowDialog();
            }
            return code;
        }

        static async public void MSBackground(string code, User refresh = null) {
            OnAccountBegin?.Invoke("msa");
            try
            {
                //Begin by getting MS AccessToken
                var MSResponse = await httpClient.PostAsync(Urls.MSTokenUri,
                    new FormUrlEncodedContent(new Dictionary<string, string> {
                { "client_id", "00000000402b5328" },
                { refresh != null ? "refresh_token" : "code", code },
                { "grant_type", refresh != null ? "refresh_token" : "authorization_code" },
                { "redirect_uri", Urls.MSFlowEndUri },
                { "scope", "service::user.auth.xboxlive.com::MBI_SSL" }
                }));

                if (!MSResponse.IsSuccessStatusCode)
                {
                    Logger.Log($"[MS] Couldn't get MS Access Token: {MSResponse.StatusCode}");
                    OnAccountFinished?.Invoke("msa");
                    return;
                }

                // Continue with getting XBL AccessToken
                var MSRespond = JsonNode.Parse(await MSResponse.Content.ReadAsStringAsync());
                var XBLResponse = await httpClient.PostAsync(
                    Urls.XLiveLoginUri,
                    new StringContent(JsonSerializer.Serialize(new { Properties = new { AuthMethod = "RPS", SiteName = "user.auth.xboxlive.com", RpsTicket = MSRespond?["access_token"]?.ToString() }, RelyingParty = "http://auth.xboxlive.com", TokenType = "JWT" }), Encoding.UTF8, "application/json")
                );

                if (!XBLResponse.IsSuccessStatusCode)
                {
                    Logger.Log($"[MS] Couldn't get Xbox Live Token: {XBLResponse.StatusCode}");
                    OnAccountFinished?.Invoke("msa");
                    return;
                }

                // Continue by getting XSTS Token
                var XBLRespond = JsonNode.Parse(await XBLResponse.Content.ReadAsStringAsync());
                var XSTSResponse = await httpClient.PostAsync(
                    Urls.XSTSLoginUri,
                    new StringContent(JsonSerializer.Serialize(new { Properties = new { SandboxId = "RETAIL", UserTokens = new string[] { XBLRespond?["Token"]?.ToString() } }, RelyingParty = "rp://api.minecraftservices.com/", TokenType = "JWT" }), Encoding.UTF8, "application/json")
                );

                if (!XBLResponse.IsSuccessStatusCode)
                {
                    OnAccountFinished?.Invoke("msa");
                    if (XBLResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        JsonNode Err = null;
                        try { Err = JsonNode.Parse(await XSTSResponse.Content.ReadAsStringAsync()); }
                        catch { }
                        if (Err?["XErr"] != null)
                            switch ((uint?)Err["XErr"])
                            {
                                case 2148916227:
                                    Logger.Log("[XSTS] This account is banned from Xbox.");
                                    return;
                                case 2148916233:
                                    Logger.Log("[XSTS] This account doesn't have an Xbox Account!");
                                    return;
                                case 2148916235:
                                    Logger.Log("[XSTS] Xbox Live is not available in this country!");
                                    return;
                                case 2148916236:
                                    Logger.Log("[XSTS] Account needs age verification!");
                                    return;
                                case 2148916237:
                                    Logger.Log("[XSTS] Account needs age verification!");
                                    return;
                                case 2148916238:
                                    Logger.Log("[XSTS] The account must be added to a Family!");
                                    return;
                                default:
                                    Logger.Log("[XSTS] Unknown Xerror!");
                                    return;
                            }
                    }
                    else
                        Logger.Log($"[MS] Unknown XSTS Error: {XSTSResponse.StatusCode}");
                    return;
                }

                // And continue again by getting minecraft access token.
                var XSTSRespond = JsonNode.Parse(await XSTSResponse.Content.ReadAsStringAsync());
                string XUserHash = XSTSRespond?["DisplayClaims"]?["xui"]?[0]?["uhs"]?.ToString();
                var MCResponse = await httpClient.PostAsync(
                    Urls.MCLoginUri,
                    new StringContent(JsonSerializer.Serialize(new { identityToken = $"XBL3.0 x={XUserHash};{XSTSRespond?["Token"]}" }), Encoding.UTF8, "application/json")
                );

                if (!MCResponse.IsSuccessStatusCode)
                {
                    Logger.Log($"[MS] Couldn't get MC Token: {MCResponse.StatusCode}");
                    OnAccountFinished?.Invoke("msa");
                    return;
                }

                // Then continue again by checking ownership (FINALLY something else than post)
                var MCRespond = JsonNode.Parse(await MCResponse.Content.ReadAsStringAsync());
                var OwnershipMessage = new HttpRequestMessage { RequestUri = new Uri(Urls.MSOwnership), Method = HttpMethod.Get };
                OwnershipMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MCRespond?["access_token"]?.ToString() ?? "None");
                var StoreResponse = await httpClient.SendAsync(OwnershipMessage);

                if (!StoreResponse.IsSuccessStatusCode)
                {
                    Logger.Log($"[MS] Couldn't get Store Items: {StoreResponse.StatusCode}");
                    OnAccountFinished?.Invoke("msa");
                    return;
                }

                var StoreRespond = JsonNode.Parse(await StoreResponse.Content.ReadAsStringAsync());
                if (StoreRespond?["items"] != null && StoreRespond["items"].AsArray().Any(v => v["name"].ToString() == "game_minecraft"))
                {
                    // FINALLY WE END THE PAIN
                    var MCProfileMessage = new HttpRequestMessage { RequestUri = new Uri(Urls.MCProfile), Method = HttpMethod.Get };
                    MCProfileMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MCRespond?["access_token"]?.ToString() ?? "None");
                    var ProfileResponse = await httpClient.SendAsync(MCProfileMessage);
                    if (!ProfileResponse.IsSuccessStatusCode)
                    {
                        Logger.Log($"[MS] Couldn't get Store Items: {ProfileResponse.StatusCode}");
                        OnAccountFinished?.Invoke("msa");
                        return;
                    }

                    var JProfile = JsonNode.Parse(await ProfileResponse.Content.ReadAsStringAsync());
                    if (JProfile?["id"] != null && JProfile?["name"] != null)
                    {
                        if (refresh != null)
                        {
                            refresh.username = JProfile["name"].ToString();
                            refresh.uuid = JProfile["id"].ToString();
                            refresh.xuid = XUserHash;
                            refresh.accessToken = MCRespond?["access_token"]?.ToString();
                            refresh.refreshToken = MSRespond?["refresh_token"]?.ToString();
                        }
                        else
                            lock (Program.AccountLock)
                                Program.Users.Add(new User
                                {
                                    username = JProfile["name"].ToString(),
                                    uuid = JProfile["id"].ToString(),
                                    xuid = XUserHash,
                                    accessToken = MCRespond?["access_token"]?.ToString(),
                                    refreshToken = MSRespond?["refresh_token"]?.ToString(),
                                    usertype = "msa"
                                });
                        Program.LoadUsersList();
                    }
                    else if (JProfile?["error"] != null)
                        Logger.Err($"[MS] MC Profile error: {JProfile["error"]}");
                    else
                        Logger.Warn($"[MS] No profile data?");
                }
            } catch (Exception ex) {
                Logger.Err($"Error during background MS login: {ex.Message}");
            }
            OnAccountFinished?.Invoke("msa");
        }

        static public bool CheckValid(User user)
        {
            if (user.usertype == "msa")
                try
                {
                    var HttpMessage = new HttpRequestMessage { RequestUri = new Uri(Urls.MCProfile), Method = HttpMethod.Get };
                    HttpMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", user.accessToken ?? "None");
                    HttpResponseMessage Response = httpClient.SendAsync(HttpMessage).Result;
                    if (Response.IsSuccessStatusCode)
                    {
                        var JProfile = JsonNode.Parse(Response.Content.ReadAsStringAsync().Result);
                        if (JProfile?["id"] != null && JProfile?["name"] != null)
                            return true;
                    }
                }
                catch (Exception ex) { Logger.Warn($"[MS] Error validating token: {ex.Message}"); }
            else if (user.usertype == "ely")
                try
                {
                    HttpResponseMessage Response = httpClient.PostAsync(Urls.ElyValidUri, new StringContent(JsonSerializer.Serialize(new { user.accessToken }), Encoding.UTF8, "application/json")).Result;
                    if (Response.IsSuccessStatusCode)
                        return true;
                }
                catch (Exception ex) { Logger.Warn($"[Ely] Error validating token: {ex.Message}"); }
            else
                return true;
            return false;
        }

        static public void DownloadAuthLibInjector(string path, bool Rewrite = false, bool CheckHash = false)
        {
            try
            {
                using (var BWC = new BetterWebClient { DownloadTimeout = 1000 * 30, ConnectTimeout = 1000 * 10 })
                {
                    BWC.Headers.Add("User-Agent", "CSharp/7.3");
                    var Latest = JsonNode.Parse(BWC.DownloadString("https://api.github.com/repos/yushijinhun/authlib-injector/releases/latest"));
                    if (Latest?["assets"] != null)
                    {
                        foreach (var Asset in Latest["assets"].AsArray())
                        {
                            if (Asset?["content_type"]?.ToString() == "application/java-archive")
                            {
                                if (!File.Exists(path) || Rewrite || (CheckHash && HashDoRewrite256(Asset["digest"]?.ToString()?.Replace("sha256:", ""), path)))
                                {
                                    //Directory.CreateDirectory(Directory.GetParent(path).FullName);
                                    LauncherLib.AddDownload(Asset["browser_download_url"].ToString(), path);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Logger.Warn($"Couldn't download authlib injector: {ex.Message}"); }
        }
        static public bool HashDoRewrite256(string hash, string FilePath)
        {
            return hash != null && File.Exists(FilePath) && ComputeSHA256(FilePath) != hash;
        }
        static public string ComputeSHA256(string Path)
        {
            if (!File.Exists(Path)) return null;
            using (var Hash = SHA256.Create())
            using (var Filed = File.OpenRead(Path))
                return BitConverter.ToString(Hash.ComputeHash(Filed)).Replace("-", "").ToLowerInvariant();
        }
    }

    public class AccountType
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}