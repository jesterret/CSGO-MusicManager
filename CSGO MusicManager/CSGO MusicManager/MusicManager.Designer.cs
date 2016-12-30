namespace CSGO_MusicManager
{
    partial class MusicManager
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EditSong = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.CurrentSongLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.AddSongButton = new System.Windows.Forms.ToolStripStatusLabel();
            this.RandomizeSongButton = new System.Windows.Forms.ToolStripStatusLabel();
            this.SearchSongButton = new System.Windows.Forms.ToolStripStatusLabel();
            this.SettingsGearButton = new System.Windows.Forms.ToolStripStatusLabel();
            this.SongTree = new CSGO_MusicManager.NoHScrollTreeView();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditSong});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            this.contextMenuStrip1.Click += new System.EventHandler(this.contextMenuStrip1_Click);
            // 
            // EditSong
            // 
            this.EditSong.Name = "EditSong";
            this.EditSong.Size = new System.Drawing.Size(124, 22);
            this.EditSong.Text = "Edit Song";
            this.EditSong.Click += new System.EventHandler(this.EditSong_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AllowMerge = false;
            this.statusStrip1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CurrentSongLabel,
            this.AddSongButton,
            this.RandomizeSongButton,
            this.SearchSongButton,
            this.SettingsGearButton});
            this.statusStrip1.Location = new System.Drawing.Point(0, 298);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(300, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // CurrentSongLabel
            // 
            this.CurrentSongLabel.AutoToolTip = true;
            this.CurrentSongLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CurrentSongLabel.Name = "CurrentSongLabel";
            this.CurrentSongLabel.Size = new System.Drawing.Size(190, 17);
            this.CurrentSongLabel.Spring = true;
            this.CurrentSongLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CurrentSongLabel.ToolTipText = "Currently chosen song";
            // 
            // AddSongButton
            // 
            this.AddSongButton.AutoToolTip = true;
            this.AddSongButton.Image = global::CSGO_MusicManager.Properties.Resources.AddIcon;
            this.AddSongButton.Name = "AddSongButton";
            this.AddSongButton.Size = new System.Drawing.Size(16, 17);
            this.AddSongButton.ToolTipText = "Download song from youtube";
            this.AddSongButton.Click += new System.EventHandler(this.AddSongButton_Click);
            // 
            // RandomizeSongButton
            // 
            this.RandomizeSongButton.AutoToolTip = true;
            this.RandomizeSongButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RandomizeSongButton.Image = global::CSGO_MusicManager.Properties.Resources.dice;
            this.RandomizeSongButton.Name = "RandomizeSongButton";
            this.RandomizeSongButton.Size = new System.Drawing.Size(16, 17);
            this.RandomizeSongButton.ToolTipText = "Randomize song";
            this.RandomizeSongButton.Click += new System.EventHandler(this.SelectRandom);
            // 
            // SearchSongButton
            // 
            this.SearchSongButton.AutoToolTip = true;
            this.SearchSongButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SearchSongButton.Image = global::CSGO_MusicManager.Properties.Resources.magnifier;
            this.SearchSongButton.Name = "SearchSongButton";
            this.SearchSongButton.Size = new System.Drawing.Size(16, 17);
            this.SearchSongButton.Text = "Search for song";
            this.SearchSongButton.Click += new System.EventHandler(this.SearchSongButton_Click);
            // 
            // SettingsGearButton
            // 
            this.SettingsGearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SettingsGearButton.Image = global::CSGO_MusicManager.Properties.Resources.Gear;
            this.SettingsGearButton.Name = "SettingsGearButton";
            this.SettingsGearButton.Size = new System.Drawing.Size(16, 17);
            this.SettingsGearButton.Text = "Show Settings";
            this.SettingsGearButton.ToolTipText = "Open settings";
            this.SettingsGearButton.Click += new System.EventHandler(this.SettingsGearButton_Click);
            // 
            // SongTree
            // 
            this.SongTree.AllowDrop = true;
            this.SongTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SongTree.Location = new System.Drawing.Point(13, 13);
            this.SongTree.Name = "SongTree";
            this.SongTree.RightToLeftLayout = true;
            this.SongTree.ShowRootLines = false;
            this.SongTree.Size = new System.Drawing.Size(275, 282);
            this.SongTree.TabIndex = 1;
            this.SongTree.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.SongTree_ItemDrag);
            this.SongTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.noHScrollTreeView1_NodeMouseClick);
            this.SongTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.noHScrollTreeView1_NodeMouseDoubleClick);
            this.SongTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.SongTree_DragDrop);
            this.SongTree.DragEnter += new System.Windows.Forms.DragEventHandler(this.SongTree_DragEnter);
            this.SongTree.DragOver += new System.Windows.Forms.DragEventHandler(this.SongTree_DragOver);
            // 
            // MusicManager
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(300, 320);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.SongTree);
            this.DoubleBuffered = true;
            this.Name = "MusicManager";
            this.Text = "Music Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MusicManager_FormClosing);
            this.Load += new System.EventHandler(this.MusicManager_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.SongTree_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.SongTree_DragEnter);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.SongTree_DragOver);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private NoHScrollTreeView SongTree;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem EditSong;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel CurrentSongLabel;
        private System.Windows.Forms.ToolStripStatusLabel AddSongButton;
        private System.Windows.Forms.ToolStripStatusLabel RandomizeSongButton;
        private System.Windows.Forms.ToolStripStatusLabel SettingsGearButton;
        private System.Windows.Forms.ToolStripStatusLabel SearchSongButton;
    }
}

