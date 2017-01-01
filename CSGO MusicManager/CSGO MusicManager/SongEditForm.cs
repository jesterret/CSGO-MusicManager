using System;
using System.Windows.Forms;
using System.IO;
using SchwabenCode.QuickIO;
using System.Threading.Tasks;

namespace CSGO_MusicManager
{
    public partial class SongEditForm : Form
    {
        string FilePath;
        string OriginalPath;
        Keys Key;

        public SongEditForm(string Path)
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

        private void SongEditForm_Load(object sender, EventArgs e)
        {
            Text = Path.GetFileNameWithoutExtension(OriginalPath);
            FilePath = Path.GetTempFileName() + Path.GetExtension(OriginalPath);
            new WaveConverter(OriginalPath, FilePath, 2).ConvertAsync().ContinueWith((Task x) =>
            {
                x.Wait();
                songPlayer1.AddSong(FilePath);
            }
                );
        }

        private void SongEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            songPlayer1.Stop();
            QuickIOFile.DeleteAsync(FilePath);
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (VoiceTrackbar.Value != 100 && VoiceTrackbar.Value != 0)
            {
                QuickIOFile.Copy(OriginalPath, OriginalPath + ".bak");
                new WaveConverter(FilePath, OriginalPath, (VoiceTrackbar.Value * 1.0f) / 100).Convert();
            }

            if(Key != Keys.None)
            {
                MusicManager.pObj.KeyToNode.TryRemoveBySecond(OriginalPath);
                MusicManager.pObj.KeyToNode.TryAdd(Key, OriginalPath);
            }
            Close();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            ValueLabel.Text = VoiceTrackbar.Value.ToString();
            UpdateVolume();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateVolume()
        {
            songPlayer1.Volume = VoiceTrackbar.Value;
        }

        #region Blocking Key Input
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            textBox1.Text = e.KeyCode.ToString();
            Key = e.KeyCode;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
#endregion
    }
}
