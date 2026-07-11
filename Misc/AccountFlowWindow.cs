using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Sandstone_Launcher
{
    class AccountFlowWindow : Form
    {
        public WebView2 webView;
        public string LoginUrl;
        public string StopOnUrl;
        public Action<string> OnCodeReceived;
        public AccountFlowWindow()
        {
            InitializeComponent();
            Shown += AccountFlow_Shown;
            FormClosing += AccountFlowWindow_FormClosing;
            DarkModeTitle.SetDarkMode(Handle, true);
        }

        private void AccountFlowWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            webView.Dispose();
        }

        private async void AccountFlow_Shown(object sender, EventArgs _)
        {
            await webView.EnsureCoreWebView2Async();
            webView.CoreWebView2.Settings.AreDevToolsEnabled = false;
            webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            webView.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = false;
            webView.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
            webView.NavigationStarting += async (a, e) =>
            {
                if (StopOnUrl != null && e.Uri.StartsWith(StopOnUrl))
                {
                    webView.Stop();
                    Uri Url = new Uri(e.Uri);
                    Dictionary<string, string> QueryList = WebHelper.GetQueries(Url.Query);
                    if (QueryList.ContainsKey("code"))
                    {
                        OnCodeReceived?.Invoke(QueryList["code"]);
                    }
                    else if (QueryList.ContainsKey("error"))
                        Logger.Log($"OAuth error: {QueryList["error"]}");
                    else
                        Logger.Log($"No code! Query: {Url.Query}");
                    await webView.CoreWebView2.Profile.ClearBrowsingDataAsync(CoreWebView2BrowsingDataKinds.AllProfile);
                    Close();
                }
            };
            webView.CoreWebView2.Navigate(LoginUrl);
        }

        private void CoreWebView2_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.Handled = true;
            Process.Start(e.Uri);
        }

        private void InitializeComponent()
        {
            this.webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.webView)).BeginInit();
            this.SuspendLayout();
            // 
            // webView
            // 
            this.webView.AllowExternalDrop = false;
            this.webView.CreationProperties = null;
            this.webView.DefaultBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.webView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView.ForeColor = System.Drawing.Color.White;
            this.webView.Location = new System.Drawing.Point(0, 0);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(600, 650);
            this.webView.TabIndex = 0;
            this.webView.ZoomFactor = 1D;
            // 
            // AccountFlowWindow
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(600, 650);
            this.Controls.Add(this.webView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Sandstone_Launcher.Properties.Resources.sandstone;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountFlowWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Login via Microsoft";
            ((System.ComponentModel.ISupportInitialize)(this.webView)).EndInit();
            this.ResumeLayout(false);

        }
    }
}