using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.DirectX.AudioVideoPlayback;

namespace CSGO_MusicManager
{
    public partial class SongPlayer : UserControl
    {
        private List<string> SongList = new List<string>();
        private List<string> PlayedList = new List<string>();

        private int _volume;
        [Category("Behavior")]
        [Description("Expected volume level")]
        public int Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                _volume = value;
                if(CurrentSong != null)
                    CurrentSong.Volume = (25 * _volume) - 5000;
            }
        }

        public bool Replay { get; set; } = false;

        private Audio CurrentSong;

        public SongPlayer()
        {
            InitializeComponent();
        }

        public void AddSong(string Song)
        {
            SongList.Add(Song);
        }

        public void AddSongs(string[] Songs)
        {
            SongList.AddRange(Songs);
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            Point targetPoint = progressBar1.PointToClient(MousePosition);
            double percent = ((double)targetPoint.X) / ((double)progressBar1.Size.Width);
            if (CurrentSong != null)
            {
                CurrentSong.CurrentPosition = CurrentSong.SeekCurrentPosition(CurrentSong.Duration * percent, SeekPositionFlags.AbsolutePositioning);
                progressBar1.Value = (int)(percent * 100);
            }
        }

        private void ShuffleBox_Click(object sender, EventArgs e)
        {

        }

        private void PreviousSongBox_Click(object sender, EventArgs e)
        {
            var lastitem = PlayedList.Count - 1;
            if (lastitem >= 0)
            {
                SongList.Insert(0, PlayedList[lastitem]);
                PlayedList.RemoveAt(lastitem);
                NextStopBox_Click(sender, e);
            }
            else if(CurrentSong != null)
            {
                CurrentSong.Stop();
                CurrentSong.Play();
            }
        }

        private void PlayPauseButton_Click(object sender, EventArgs e)
        {
            if (CurrentSong == null && SongList.Count > 0)
                SetSong(SongList.First());

            if (CurrentSong != null)
            {
                if (CurrentSong.Playing)
                    CurrentSong.Pause();
                else if (CurrentSong.Paused || CurrentSong.Stopped)
                {
                    CurrentSong.Play();
                }
            }
        }

        private void NextStopBox_Click(object sender, EventArgs e)
        {
            if(SongList.Count > 0) // TODO: Rewind state check 
            {
                string Song = SongList.First();
                SetSong(Song);
                CurrentSong.Play();
                PlayedList.Add(Song);
                SongList.RemoveAt(0);
            }
        }

        private void ModeButton_Click(object sender, EventArgs e)
        {
            Replay = !Replay;
        }

        private void SetSong(string Song)
        {
            if (CurrentSong == null)
            {
                CurrentSong = new Audio(Song);
                CurrentSong.Volume = (25 * _volume) - 5000;
                CurrentSong.Starting += OnSongPlay;
                CurrentSong.Pausing += OnSongPause;
                CurrentSong.Stopping += OnSongPause;
                CurrentSong.Ending += OnSongEnd;
            }
            else
                CurrentSong.Open(Song);
        }

        private void OnSongPlay(object sender, EventArgs e)
        {
            PlayPauseButton.Image = Properties.Resources.Pause;
            timer1.Start();
        }

        private void OnSongPause(object sender, EventArgs e)
        {
            PlayPauseButton.Image = Properties.Resources.Play;
            timer1.Stop();
        }

        private void OnSongEnd(object sender, EventArgs e)
        {
            CurrentSong.Stop();
            if (Replay)
                CurrentSong.Play();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = (int)((CurrentSong.CurrentPosition / CurrentSong.Duration) * 100);
        }
        
        public void Stop()
        {
            CurrentSong?.Stop();
        }
    }
}
