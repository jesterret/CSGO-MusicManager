using Microsoft.DirectX.AudioVideoPlayback;
using Microsoft.DirectX;
using System;
using SlimDX.DirectSound;
using System.Windows.Forms;
using System.IO;
using SlimDX.Multimedia;
using SchwabenCode.QuickIO;
using System.Threading.Tasks;

namespace CSGO_MusicManager
{
    public partial class SongEditForm : Form
    {
        string FilePath;
        string OriginalPath;
        Keys Key;
        Audio xx;

        public SongEditForm(string Path, Keys SongPlayKey)
        {
            InitializeComponent();
            OriginalPath = Path;
            Keys temp = Keys.None;
            if (MusicManager.pObj.KeyToNode.TryGetBySecond(Path, out temp))
            {
                Key = temp;
                textBox1.Text = Key.ToString();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            ValueLabel.Text = VoiceTrackbar.Value.ToString();
            UpdateVolume();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if(VoiceTrackbar.Value != 100 && VoiceTrackbar.Value != 0)
                new WaveConverter(FilePath, OriginalPath, (VoiceTrackbar.Value * 1.0f) / 100).Convert();

            if(Key != Keys.None)
            {
                MusicManager.pObj.KeyToNode.TryRemoveBySecond(FilePath);
                MusicManager.pObj.KeyToNode.TryAdd(Key, FilePath);
            }
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            textBox1.Text = e.KeyCode.ToString();
            Key = e.KeyCode;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void UpdateVolume()
        {
            xx.Volume = (25 * VoiceTrackbar.Value) - 5000;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (xx?.Playing == true)
                xx.Stop();

            xx = new Audio(FilePath, false);
            UpdateVolume();

            xx.Play();
		}

        private void SongEditForm_Load(object sender, EventArgs e)
        {
            FilePath = Path.GetTempFileName() + Path.GetExtension(OriginalPath);
            new WaveConverter(OriginalPath, FilePath, 2).ConvertAsync().ContinueWith((Task x) => 
                {
                    x.Wait();
                    button1.Invoke((Action)(() => 
                    {
                        button1.Enabled = true;
                    }));
                }
                );
        }

        private void SongEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            xx?.Stop();
        }
    }
}
