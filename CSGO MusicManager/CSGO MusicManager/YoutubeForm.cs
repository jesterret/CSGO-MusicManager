using SchwabenCode.QuickIO;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using YoutubeExtractor;

namespace CSGO_MusicManager
{
    public partial class YoutubeForm : Form
    {
        private string DownloadPath;
        private string TargetPath;

        public YoutubeForm()
        {
            InitializeComponent();
        }

        public static string RemoveInvalidFilenameCharacters(string filename)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            var result = r.Replace(filename, "");
            return result.Trim(' ', '-');
        }

        private void UpdateProgress(object sender, ProgressEventArgs e)
        {
            ProgressBar.Value = (int)e.ProgressPercentage;
            ProgressBar.PerformStep();
        }

        private void FinalizeDownload(object sender, EventArgs e)
        {
            ProgressBar.Value = 0;
            new WaveConverter(DownloadPath, TargetPath).ConvertCleanup();
            MusicManager.RefreshTreeView();
        }

        private void Import(object sender, EventArgs e)
        {
            string Uri = LinkBox.Text;
            LinkBox.Enabled = false;
            CancelBtn.Enabled = false;
            ImportBtn.Enabled = false;
            try
            {
                var videoInfo = DownloadUrlResolver.GetDownloadUrls(Uri ?? "", false);
                var video = videoInfo.OrderByDescending(info => info.AudioBitrate)
                                            .ThenByDescending(info => info.Resolution)
                                            .ElementAt(0);

                if (video.RequiresDecryption)
                    DownloadUrlResolver.DecryptDownloadUrl(video);

                string FileName = RemoveInvalidFilenameCharacters(video.Title);
                DownloadPath = Path.GetTempFileName();
                //                DownloadPath = Path.Combine(Path.GetTempPath(), FileName + video.VideoExtension);
                TargetPath = Path.Combine(MusicManager.MusicDir, FileName + ".wav");

                var vidDownloader = new VideoDownloader(video, DownloadPath);
                vidDownloader.DownloadProgressChanged += UpdateProgress;
                vidDownloader.DownloadFinished += FinalizeDownload;
                vidDownloader.Execute();

            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("No video meeting requirements found. " + ex.Message);
            }
            catch (YoutubeParseException ex)
            {
                MessageBox.Show("Youtube parsing error. " + ex.Message);
            }
            catch (VideoNotAvailableException ex)
            {
                MessageBox.Show("Video is not available. " + ex.Message);
            }
            catch (WebException ex)
            {
                MessageBox.Show("Network error. " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Argument error. " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Close();
            }
        }

        private void Cancel(object sender, EventArgs e)
        {
            Close();
        }
    }
}
