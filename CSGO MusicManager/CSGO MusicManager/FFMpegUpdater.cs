using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using SchwabenCode.QuickIO;

namespace CSGO_MusicManager
{
    class FFMpegUpdater
    {
        string Version
        {
            get
            {
                return Environment.Is64BitOperatingSystem ? "win64" : "win32";
            }
        }
        public FFMpegUpdater()
        {
            var URL = "https://ffmpeg.zeranoe.com/builds/" + Version + "/static/ffmpeg-latest-" + Version + "-static.zip";
            var cl = new WebClient();
            cl.DownloadFileCompleted += OnDLComplete;
            cl.DownloadFileAsync(new Uri(URL), Path.Combine(Path.GetTempPath(), "ffmpeg.zip"));
        }
        void OnDLComplete(object s, object e)
        {
            string tmp = Path.GetTempPath();
            var arch = Path.Combine(tmp, "ffmpeg.zip");
            var dir = Path.Combine(tmp, "ffmpeg");
            if (QuickIODirectory.Exists(dir))
                QuickIODirectory.Delete(dir, true);
            ZipFile.ExtractToDirectory(arch, dir);
            QuickIOFile.Copy(Path.Combine(tmp, @"ffmpeg\ffmpeg-latest-" + Version + @"-static\bin\ffmpeg.exe"), "ffmpeg.exe", true);
        }
    }
}
