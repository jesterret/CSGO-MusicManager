﻿using Gameloop.Vdf;
using Microsoft.Win32;
using Newtonsoft.Json;
using SchwabenCode.QuickIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSGO_MusicManager
{
    public partial class MusicManager : Form
    {
        #region Native Functions

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool CreateHardLink(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool DeleteFile(string lpFileName);

        #endregion

        #region Variables

        private string CSGODir;
        public static string MusicDir;
        public static string CurrentCheckDir;

        public BiDictionaryOneToOne<Keys, string> KeyToNode;

        public static MusicManager pObj;
        private bool bInit = true;
        private Random Randomizer;
        private TreeNode HomeNode;
        private KeyboardHook hook;

        private Properties.Settings Vars => Properties.Settings.Default;
        #endregion

        #region Initialization

        public MusicManager()
        {
            pObj = this;
            InitializeComponent();
        }

        private void MusicManager_Load(object sender, EventArgs e)
        {
            Application.ApplicationExit += SaveSettings;

            if(!File.Exists("ffmpeg.exe"))
            {
                new FFMpegUpdater();
            }

            InitializeMusicDirectory();
            InitializeCSGODirectory();
            InitializeRegistryValues();
            InitializeTreeView();
            InitializeVariables();

        }

        private void OnSongKeyUp(object sender, KeyEventArgs e)
        {
            Task.Run(() =>
            {
                string Dir = null;
                if (KeyToNode.TryGetByFirst(e.KeyCode, out Dir))
                {
                    if (File.Exists(Dir))
                        WriteCSGOFiles(Dir, Path.GetFileNameWithoutExtension(Dir));
                    else
                        KeyToNode.RemoveByFirst(e.KeyCode);
                }
            });
        }

        private void InitializeVariables()
        {
            Randomizer = new Random();
            KeyToNode = JsonConvert.DeserializeObject<BiDictionaryOneToOne<Keys, string>>(Vars.KeyToNode);
            if (KeyToNode == null)
                KeyToNode = new BiDictionaryOneToOne<Keys, string>();
            if (Vars.CheckUpdates == true)
                new UpdateChecker();

            hook = new KeyboardHook();
            hook.KeyUp += OnSongKeyUp;
            hook.hook();
        }

        private void InitializeTreeView()
        {
            SongTree.Nodes.Clear();
            HomeNode = SongTree.Nodes.Add(MusicDir, "Sound Directory");
            ParseDirectories(MusicDir, HomeNode);
            bInit = false;
            HomeNode.Expand();
        }

        private void InitializeRegistryValues()
        {
            var RegKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\jesterret");
            RegKey.SetValue("Music Folder", MusicDir);
        }

        private void InitializeMusicDirectory()
        {
            var RegKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\jesterret");
            MusicDir = (string)RegKey.GetValue("Music Folder");
            if (MusicDir == null)
                MusicDir = SetupDirectory("Music");

            CurrentCheckDir = MusicDir;
        }

        private void InitializeCSGODirectory()
        {
            try
            {
                var Key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Valve\\Steam");
                if (Key == null)
                    throw new KeyNotFoundException("Steam registry key doesn't exist.");

                var SteamPath = (string)Key.GetValue("SteamPath");
                string CSGOPath = "/steamapps/common/Counter-Strike Global Offensive/";
                if (!Directory.Exists(SteamPath))
                    throw new DirectoryNotFoundException("Steam directory not found");

                if (Directory.Exists(SteamPath + CSGOPath) && File.Exists(SteamPath + "/steamapps/appmanifest_730.acf"))
                    CSGODir = SteamPath + CSGOPath;
                else
                {
                    var reader = new VdfTextReader(File.OpenText(SteamPath + "/config/config.vdf"));
                    while (reader.ReadToken())
                    {
                        if (reader.Value.StartsWith("BaseInstallFolder_"))
                        {
                            reader.ReadToken();
                            string str = Path.GetFullPath(reader.Value).Replace('\\', '/');
                            if (Directory.Exists(str + CSGOPath) && File.Exists(str + "/steamapps/appmanifest_730.acf"))
                            {
                                CSGODir = str + CSGOPath;
                                break;
                            }
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("You sure you have steam/csgo installed?\n" + e.Message);
            }
        }

        private string SetupDirectory(string FallbackFolder)
        {
            string retn = string.Empty;
            var x = new FolderBrowserDialog();
            if (x.ShowDialog() == DialogResult.OK)
                retn = x.SelectedPath;
            else
                retn = Path.GetFullPath(Directory.CreateDirectory(FallbackFolder).ToString());

            return retn;
        }

        public static void RefreshTreeView()
        {
            pObj.InitializeTreeView();
        }

        private void SaveSettings(object sender, object e)
        {
            hook.Dispose();
            Vars.KeyToNode = JsonConvert.SerializeObject(KeyToNode);
            Vars.Save();
            if(File.Exists("update.zip") && File.Exists("updater.exe"))
            {
                var process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = @"updater.exe"
                };
                process.Start();
            }
        }

        #endregion

        #region File Management

        private void SelectRandom(object sender, EventArgs e)
        {
            var files = Directory.GetFiles(CurrentCheckDir, "*.wav", CurrentCheckDir == MusicDir ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories);
            var RandFile = files[Randomizer.Next(0, files.Length)];
            var arr = SongTree.Nodes.Find(RandFile, true);
            if (arr.Length == 1)
            {
                SelectNode(arr[0]);
                CurrentSongLabel.ToolTipText = CurrentSongLabel.Text = arr[0].Text;
                SongTree.Focus();
            }
            WriteCSGOFiles(RandFile, Path.GetFileNameWithoutExtension(RandFile));
        }

        private void ParseDirectories(string Dir, TreeNode Node)
        {
            var entries = QuickIODirectory.EnumerateFileSystemEntries(Dir, enumerateOptions: QuickIOEnumerateOptions.SuppressAllExceptions).OrderByDescending(entry => entry.Value == QuickIOFileSystemEntryType.Directory);
            foreach (var x in entries)
            {
                if (x.Value == QuickIOFileSystemEntryType.Directory)
                {
                    var Tree = Node.Nodes.Add(x.Key.FullName, Path.GetFileNameWithoutExtension(x.Key.Name));
                    ParseDirectories(x.Key.FullName, Tree);
                }
                else
                    ParseFile(x.Key, Node);
            }
        }

        private void ParseFile(QuickIOPathInfo file, TreeNode Node)
        {
            TreeNode Tree = null;
            var extension = Path.GetExtension(file.FullName);
            if (extension != ".wav")
            {
                if (extension == ".bak")
                {
                    var x = (bInit) ? QuickIOFile.DeleteAsync(file.FullName) : null;
                    return;
                }

                try
                {
                    string outFile = Path.Combine(Path.GetDirectoryName(file.FullName), Path.GetFileNameWithoutExtension(file.FullName) + ".wav");
                    new WaveConverter(file.FullName, outFile).ConvertCleanup();
                    if (outFile != null)
                    {
                        Tree = Node.Nodes.Add(outFile, Path.GetFileNameWithoutExtension(file.Name));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error on file conversion: " + file.FullName + "." + Environment.NewLine + ex.Message);
                }
            }
            else
                Tree = Node.Nodes.Add(file.FullName, Path.GetFileNameWithoutExtension(file.Name));

            if (Tree != null)
                Tree.ContextMenuStrip = contextMenuStrip1;
        }

        public void WriteCSGOFiles(string FilePath, string FileName)
        {
            try
            {
                if (CSGODir != null && Directory.Exists(CSGODir) && File.Exists(FilePath))
                {
                    string VoiceFile = Path.Combine(CSGODir, "voice_input.wav");
                    if (Path.GetPathRoot(FilePath) == Path.GetPathRoot(CSGODir))
                    {
                        if (File.Exists(VoiceFile))
                            DeleteFile(VoiceFile);

                        CreateHardLink(VoiceFile, FilePath, IntPtr.Zero);
                    }
                    else
                        QuickIOFile.CopyAsync(FilePath, VoiceFile, true);

                    QuickIOFile.WriteAllText(Path.Combine(CSGODir, "csgo/cfg/GlobalBroadcast.cfg") , $"say \"{FileName}\"");
                    QuickIOFile.WriteAllText(Path.Combine(CSGODir, "csgo/cfg/TeamBroadcast.cfg"), $"say_team \"{FileName}\"");
                }
            }
            catch (Exception)
            {
            }
        }

        public void SelectNode(TreeNode Node, bool bSearch = false)
        {
            if (bSearch)
            {
                var arr = SongTree.Nodes.Find(Node.Name, true);
                if (arr.Length > 0)
                    SongTree.SelectedNode = arr[0];
            }
            else
                SongTree.SelectedNode = Node;
        }

        private void SongTree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        private void SongTree_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null)
            {
                foreach (var file in files)
                {
                    QuickIOFile.Move(file, Path.Combine(MusicDir, Path.GetFileName(file)));
                }
                InitializeTreeView();
            }
            else
            {
                TreeNode TargetNode = SongTree.SelectedNode;
                TreeNode DraggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
                if (TargetNode != null && DraggedNode != null)
                {
                    if (TargetNode.GetNodeCount(false) == 0)
                        TargetNode = TargetNode.Parent;

                    if (TargetNode != null && TargetNode != DraggedNode && TargetNode != DraggedNode.Parent)
                    {
                        Keys Key = Keys.None;
                        if(KeyToNode.TryGetBySecond(DraggedNode.Name, out Key))
                        {
                            KeyToNode.RemoveByFirst(Key);
                            KeyToNode.Add(Key, TargetNode.Name);
                        }
                        var dir1 = DraggedNode.Name;
                        var dir2 = Path.Combine(TargetNode.Name, Path.GetFileName(DraggedNode.Text));
                        QuickIOFile.Move(dir1, dir2);
                        InitializeTreeView();
                    }
                }
            }
        }

        private void SongTree_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = e.AllowedEffect;
        }

        private void SongTree_DragOver(object sender, DragEventArgs e)
        {
            Point targetPoint = SongTree.PointToClient(new Point(e.X, e.Y));
            SelectNode(SongTree.GetNodeAt(targetPoint));
        }

        #endregion

        #region Events

        private void EditSong_Click(object sender, EventArgs e)
        {
            new SongEditForm(SongTree.SelectedNode.Name).ShowDialog();
        }

        private void AddSongButton_Click(object sender, EventArgs e)
        {
            new YoutubeForm().ShowDialog();
        }

        private void SearchSongButton_Click(object sender, EventArgs e)
        {
            new SearchForm(SongTree).ShowDialog();
        }

        private void SettingsGearButton_Click(object sender, EventArgs e)
        {
            new SettingsForm().ShowDialog();
        }

        private void noHScrollTreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode Node = e.Node;
            if (e.Button != MouseButtons.Left)
                return;

            if (Node.GetNodeCount(false) == 0 && !Directory.Exists(Node.Name))
            {
                CurrentSongLabel.ToolTipText = CurrentSongLabel.Text = Node.Text;
                WriteCSGOFiles(Node.Name, Node.Text);
            }
        }

        private void MusicManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings(sender, e);
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {

        }

        private void noHScrollTreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                SelectNode(e.Node);
            }
        }

        #endregion
    }
}
