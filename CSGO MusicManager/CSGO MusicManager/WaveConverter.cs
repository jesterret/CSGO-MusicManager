using SchwabenCode.QuickIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGO_MusicManager
{
    public class WaveConverter
    {
        private string OriginalPath;
        private string TargetPath;
        private float Volume;
        private bool IsTempSrc;
        private TaskCompletionSource<object> processCompletionTask = new TaskCompletionSource<object>();

        public bool IsConvertibleFormat
        {
            get
            {
                var process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    FileName = @"ffmpeg.exe",
                    Arguments = $"-i \"{OriginalPath}\""
                };
                process.Start();

                while (!process.StandardError.EndOfStream)
                {
                    string line = process.StandardError.ReadLine();
                    if (line.Contains("Invalid data found") || line.Contains("Duration: N/A, bitrate: N/A"))
                        return false;
                }

                return true;
            }
        }

        public WaveConverter(string SourceFile, string DestinationFile, float SoundVolume = 1.0f)
        {
            if(SourceFile == DestinationFile)
            {
                OriginalPath = Path.GetTempFileName();
                IsTempSrc = true;
                File.Copy(SourceFile, OriginalPath, true);
            }
            else
                OriginalPath = SourceFile;
            TargetPath = DestinationFile;
            Volume = SoundVolume;
        }

        private static async Task FixFFmpegWavFileHeader(string filepath)
        {
            const int FFmpegHeaderSizeInBytes = 34;

            byte[] data = null;
            using (var file = QuickIOFile.OpenRead(filepath))
            {
                if (file.Length > FFmpegHeaderSizeInBytes)
                {
                    // Load the file into memory
                    data = new byte[file.Length - FFmpegHeaderSizeInBytes];

                    // Read the file into the data buffer, skipping over the FFmpeg header data.
                    await file.ReadAsync(data, 0, 36);
                    file.Seek(FFmpegHeaderSizeInBytes, SeekOrigin.Current);
                    await file.ReadAsync(data, 36, (int)(file.Length - file.Position));

                    // Set the new file size
                    byte[] filesize = BitConverter.GetBytes(data.Length);
                    data[4] = filesize[0];
                    data[5] = filesize[1];
                    data[6] = filesize[2];
                    data[7] = filesize[3];

                }
            }
            // Save
            if (data != null)
                File.WriteAllBytes(filepath, data);
        }

        private async void FixHeader(object sender, object e)
        {
            try
            {
                await FixFFmpegWavFileHeader(TargetPath);
            }
            catch
            {
            }
            finally
            {
                processCompletionTask.SetResult(null);
            }
        }

        public Task ConvertCleanupAsync()
        {
            return Task.Run(() =>
            {
                Convert();
                QuickIOFile.DeleteAsync(OriginalPath);
            });
        }

        public void ConvertCleanup()
        {
            Convert();
            QuickIOFile.DeleteAsync(OriginalPath);
        }

        public Task ConvertAsync()
        {
            return Task.Run(() =>
            {
                Convert();
            });
        }

        public void Convert()
        {
			try
			{
				if (File.Exists("ffmpeg.exe"))
				{
					if (IsConvertibleFormat)
					{
						var process = new Process();
						process.EnableRaisingEvents = true;
                        process.Exited += FixHeader;
                        process.StartInfo = new ProcessStartInfo
                        {
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true,
							FileName = @"ffmpeg.exe",
							Arguments = $"-y -i \"{OriginalPath}\" -map_metadata -1 -codec:a pcm_s16le -q:a 100 -ac 1 -ar 22050 -af \"volume={Volume.ToString("F", CultureInfo.GetCultureInfo("en-us"))}\" \"{TargetPath}\"",
						};

						process.Start();
						processCompletionTask.Task.Wait();
					}
					else
						throw new FormatException("Unsupported file format. " + Environment.NewLine + "File '" + OriginalPath + "' skipped.");
				}
				else
					throw new FileNotFoundException("FFMpeg cannot be found in current working directory." + Environment.NewLine + "File '" + OriginalPath + "' skipped.");
			}
			finally
			{
				if (IsTempSrc)
					QuickIOFile.DeleteAsync(OriginalPath);
			}
        }
    }
}
