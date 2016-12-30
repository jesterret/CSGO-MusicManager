namespace CSGO_MusicManager
{
    partial class SongPlayer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.PreviousSongBox = new System.Windows.Forms.PictureBox();
            this.ModeButton = new System.Windows.Forms.PictureBox();
            this.NextStopBox = new System.Windows.Forms.PictureBox();
            this.PlayPauseButton = new System.Windows.Forms.PictureBox();
            this.ShuffleBox = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PreviousSongBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ModeButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NextStopBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayPauseButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShuffleBox)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.InfoText;
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.progressBar1.Location = new System.Drawing.Point(0, 0);
            this.progressBar1.MarqueeAnimationSpeed = 1;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(230, 5);
            this.progressBar1.Step = 1000;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 7;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // PreviousSongBox
            // 
            this.PreviousSongBox.Image = global::CSGO_MusicManager.Properties.Resources.Previous;
            this.PreviousSongBox.Location = new System.Drawing.Point(49, 11);
            this.PreviousSongBox.Name = "PreviousSongBox";
            this.PreviousSongBox.Size = new System.Drawing.Size(40, 40);
            this.PreviousSongBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PreviousSongBox.TabIndex = 6;
            this.PreviousSongBox.TabStop = false;
            this.PreviousSongBox.Click += new System.EventHandler(this.PreviousSongBox_Click);
            // 
            // ModeButton
            // 
            this.ModeButton.Image = global::CSGO_MusicManager.Properties.Resources.Replay;
            this.ModeButton.Location = new System.Drawing.Point(187, 11);
            this.ModeButton.Name = "ModeButton";
            this.ModeButton.Size = new System.Drawing.Size(40, 40);
            this.ModeButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ModeButton.TabIndex = 5;
            this.ModeButton.TabStop = false;
            this.ModeButton.Click += new System.EventHandler(this.ModeButton_Click);
            // 
            // NextStopBox
            // 
            this.NextStopBox.Image = global::CSGO_MusicManager.Properties.Resources.Next;
            this.NextStopBox.Location = new System.Drawing.Point(141, 11);
            this.NextStopBox.Name = "NextStopBox";
            this.NextStopBox.Size = new System.Drawing.Size(40, 40);
            this.NextStopBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.NextStopBox.TabIndex = 4;
            this.NextStopBox.TabStop = false;
            this.NextStopBox.Click += new System.EventHandler(this.NextStopBox_Click);
            // 
            // PlayPauseButton
            // 
            this.PlayPauseButton.Image = global::CSGO_MusicManager.Properties.Resources.Play;
            this.PlayPauseButton.Location = new System.Drawing.Point(95, 11);
            this.PlayPauseButton.Name = "PlayPauseButton";
            this.PlayPauseButton.Size = new System.Drawing.Size(40, 40);
            this.PlayPauseButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PlayPauseButton.TabIndex = 3;
            this.PlayPauseButton.TabStop = false;
            this.PlayPauseButton.Click += new System.EventHandler(this.PlayPauseButton_Click);
            // 
            // ShuffleBox
            // 
            this.ShuffleBox.Image = global::CSGO_MusicManager.Properties.Resources.Shuffle;
            this.ShuffleBox.Location = new System.Drawing.Point(3, 11);
            this.ShuffleBox.Name = "ShuffleBox";
            this.ShuffleBox.Size = new System.Drawing.Size(40, 40);
            this.ShuffleBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ShuffleBox.TabIndex = 2;
            this.ShuffleBox.TabStop = false;
            this.ShuffleBox.Click += new System.EventHandler(this.ShuffleBox_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // SongPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.PreviousSongBox);
            this.Controls.Add(this.ModeButton);
            this.Controls.Add(this.NextStopBox);
            this.Controls.Add(this.PlayPauseButton);
            this.Controls.Add(this.ShuffleBox);
            this.Name = "SongPlayer";
            this.Size = new System.Drawing.Size(230, 56);
            ((System.ComponentModel.ISupportInitialize)(this.PreviousSongBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ModeButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NextStopBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayPauseButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShuffleBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox ShuffleBox;
        private System.Windows.Forms.PictureBox PlayPauseButton;
        private System.Windows.Forms.PictureBox NextStopBox;
        private System.Windows.Forms.PictureBox ModeButton;
        private System.Windows.Forms.PictureBox PreviousSongBox;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
    }
}
