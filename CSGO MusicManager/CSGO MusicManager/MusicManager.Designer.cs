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
            this.editSongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.CurrentSongLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.AddSongButton = new System.Windows.Forms.ToolStripStatusLabel();
            this.SearchSongButton = new System.Windows.Forms.ToolStripStatusLabel();
            this.SettingsGearButton = new System.Windows.Forms.ToolStripStatusLabel();
            this.noHScrollTreeView1 = new CSGO_MusicManager.NoHScrollTreeView();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editSongToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            this.contextMenuStrip1.Click += new System.EventHandler(this.contextMenuStrip1_Click);
            // 
            // editSongToolStripMenuItem
            // 
            this.editSongToolStripMenuItem.Name = "editSongToolStripMenuItem";
            this.editSongToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.editSongToolStripMenuItem.Text = "Edit Song";
            this.editSongToolStripMenuItem.Click += new System.EventHandler(this.editSongToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AllowMerge = false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CurrentSongLabel,
            this.AddSongButton,
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
            this.CurrentSongLabel.Size = new System.Drawing.Size(206, 17);
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
            this.AddSongButton.ToolTipText = "Download song from youtube link";
            this.AddSongButton.Click += new System.EventHandler(this.AddSongButton_Click);
            // 
            // SearchSongButton
            // 
            this.SearchSongButton.AutoToolTip = true;
            this.SearchSongButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SearchSongButton.Image = global::CSGO_MusicManager.Properties.Resources.magnifier;
            this.SearchSongButton.Name = "SearchSongButton";
            this.SearchSongButton.Size = new System.Drawing.Size(16, 17);
            this.SearchSongButton.ToolTipText = "Search for song";
            this.SearchSongButton.Click += new System.EventHandler(this.SearchSongButton_Click);
            // 
            // SettingsGearButton
            // 
            this.SettingsGearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SettingsGearButton.Image = global::CSGO_MusicManager.Properties.Resources.Gear;
            this.SettingsGearButton.Name = "SettingsGearButton";
            this.SettingsGearButton.Size = new System.Drawing.Size(16, 17);
            this.SettingsGearButton.Text = "Show Settings";
            this.SettingsGearButton.Click += new System.EventHandler(this.SettingsGearButton_Click);
            // 
            // noHScrollTreeView1
            // 
            this.noHScrollTreeView1.Location = new System.Drawing.Point(13, 13);
            this.noHScrollTreeView1.Name = "noHScrollTreeView1";
            this.noHScrollTreeView1.RightToLeftLayout = true;
            this.noHScrollTreeView1.ShowRootLines = false;
            this.noHScrollTreeView1.Size = new System.Drawing.Size(275, 282);
            this.noHScrollTreeView1.TabIndex = 1;
            this.noHScrollTreeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.noHScrollTreeView1_NodeMouseClick);
            this.noHScrollTreeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.noHScrollTreeView1_NodeMouseDoubleClick);
            // 
            // MusicManager
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(300, 320);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.noHScrollTreeView1);
            this.DoubleBuffered = true;
            this.Name = "MusicManager";
            this.Text = "Music Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MusicManager_FormClosing);
            this.Load += new System.EventHandler(this.MusicManager_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private NoHScrollTreeView noHScrollTreeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editSongToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel CurrentSongLabel;
        private System.Windows.Forms.ToolStripStatusLabel AddSongButton;
        private System.Windows.Forms.ToolStripStatusLabel SearchSongButton;
        private System.Windows.Forms.ToolStripStatusLabel SettingsGearButton;
    }
}

