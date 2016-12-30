using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;

namespace CSGO_MusicManager
{
    class UpdateChecker
    {
        WebClient Client;

        public UpdateChecker()
        {
            Client = new WebClient();
            Client.Headers.Add("User-Agent", "Music Manager");
            Client.Headers.Add("Content-Type", "application/json");
            Client.DownloadStringCompleted += OnCheckCompletion;
            Client.DownloadString("https://api.github.com/repos/jesterret/CSGO-MusicManager/releases/latest");
        }

        private void OnCheckCompletion(object sender, DownloadStringCompletedEventArgs e)
        {
            var AppVersion = new Version(Application.ProductVersion);
            var responseType = new { tag_name = "", name = "", published_at = new DateTime(), assets = new[] { new { browser_download_url = "", size = 0, created_at = new DateTime() } } };
            var results = JsonConvert.DeserializeAnonymousType(e.Result, responseType);
            var Version = new Version(results.tag_name);
            if (Version > AppVersion && results.assets.Length > 0)
            {
                var result = MessageBox.Show("There's an update '" + results.name + "' available. Do you want to download it?", "Update available", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Client.DownloadFileAsync(new Uri(results.assets[0].browser_download_url), "update.zip");
                    Client.DownloadFileCompleted += OnFileCompletion;
                }
            }
        }

        private void OnFileCompletion(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled && e.Error != null)
                MessageBox.Show(e.Error.Message);
        }
    }
}
