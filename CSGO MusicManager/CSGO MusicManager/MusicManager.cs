using Gameloop.Vdf;
using Microsoft.Win32;
using Newtonsoft.Json;
using SchwabenCode.QuickIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSGO_MusicManager
{
    public partial class MusicManager : Form
    {
        private string CSGODir;
        public static string MusicDir;
        public static string CurrentCheckDir;

        public BiDictionaryOneToOne<Keys, string> KeyToNode;

        public static MusicManager pObj;
        private Random Randomizer;
        private TreeNode HomeNode;
        private KeyboardHook hook;

        private Properties.Settings Vars;

        public MusicManager()
        {
            pObj = this;
            InitializeComponent();
        }

        private void OnSongKeyUp(object sender, KeyEventArgs e)
        {
            Task.Run(() =>
            {
                string Dir = null;
                if (KeyToNode.TryGetByFirst(e.KeyCode, out Dir))
                    WriteCSGOFiles(Dir, Path.GetFileNameWithoutExtension(Dir));
            });
        }

        private void InitializeVariables()
        {
            KeyToNode = JsonConvert.DeserializeObject<BiDictionaryOneToOne<Keys, string>>(Vars.KeyToNode);
            if (KeyToNode == null)
                KeyToNode = new BiDictionaryOneToOne<Keys, string>();
            if (Vars.CheckUpdates == true)
                new UpdateChecker();

            hook = new KeyboardHook();
            hook.KeyUp += OnSongKeyUp;
            hook.hook();
        }

        private void MusicManager_Load(object sender, EventArgs e)
        {
            Vars = Properties.Settings.Default;
            Application.ApplicationExit += SaveSettings;

            Randomizer = new Random();

            InitializeMusicDirectory();
            InitializeCSGODirectory();
            InitializeRegistryValues();
            InitializeTreeView();
            InitializeVariables();

        }

        private void InitializeTreeView()
        {
            noHScrollTreeView1.Nodes.Clear();
            HomeNode = noHScrollTreeView1.Nodes.Add(MusicDir, "Sound Directory");
            ParseDirectories(MusicDir, HomeNode);
            HomeNode.Expand();
        }

        private void ParseDirectories(string Dir, TreeNode Node)
        {
            var entries = QuickIODirectory.EnumerateFileSystemEntries(Dir, enumerateOptions: QuickIOEnumerateOptions.SuppressAllExceptions).OrderByDescending(entry => entry.Value == QuickIOFileSystemEntryType.Directory);
            foreach (var x in entries)
            {
                var Tree = Node.Nodes.Add(x.Key.FullName, Path.GetFileNameWithoutExtension(x.Key.Name));
                if (x.Value == QuickIOFileSystemEntryType.Directory)
                    ParseDirectories(x.Key.FullName, Tree);
                else
                    Tree.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void ParseFiles(IEnumerable<QuickIOFileInfo> files, TreeNode DirNode)
        {
            foreach (var file in files)
            {
                TreeNode Node = null;
                var extension = Path.GetExtension(file.FullName);
                if (extension == ".wav")
                    Node = DirNode.Nodes.Add(file.FullName, Path.GetFileNameWithoutExtension(file.Name));
                else
                {
                    try
                    {
                        string outFile = null; //ConvertToWave(file.FullName);
                        if (outFile != null)
                        {
                            Node = DirNode.Nodes.Add(outFile, Path.GetFileNameWithoutExtension(file.Name));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error on file conversion: " + file.FullName + "." + Environment.NewLine + ex.Message);
                    }
                }
                if (Node != null)
                    Node.ContextMenuStrip = contextMenuStrip1;
            }
        }

        public void WriteCSGOFiles(string FilePath, string FileName)
        {
            try
            {
                if (CSGODir != null && Directory.Exists(CSGODir) && File.Exists(FilePath))
                {
                    QuickIOFile.CopyAsync(FilePath, Path.Combine(CSGODir, "voice_input.wav"), true);
                    QuickIOFile.WriteAllText(Path.Combine(CSGODir, "csgo/cfg/GlobalBroadcast.cfg") , $"say \"{FileName}\"");
                    QuickIOFile.WriteAllText(Path.Combine(CSGODir, "csgo/cfg/TeamBroadcast.cfg"), $"say_team \"{FileName}\"");
                }
            }
            catch (Exception)
            {
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

        private void InitializeRegistryValues()
        {
            var RegKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\jesterret");
            RegKey.SetValue("Music Folder", MusicDir);
        }

        private void editSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SongEditForm(noHScrollTreeView1.SelectedNode.Name, Vars.RapeKey).ShowDialog();
        }

        private void AddSongButton_Click(object sender, EventArgs e)
        {
            new YoutubeForm().ShowDialog();
        }

        private void SearchSongButton_Click(object sender, EventArgs e)
        {
            new SearchForm(noHScrollTreeView1).ShowDialog();
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

        public void SelectNode(TreeNode Node, bool bSearch = false)
        {
            if (bSearch)
            {
                var arr = noHScrollTreeView1.Nodes.Find(Node.Name, true);
                if (arr.Length > 0)
                    noHScrollTreeView1.SelectedNode = arr[0];
            }
            else
                noHScrollTreeView1.SelectedNode = Node;
        }

        private void SaveSettings(object sender, object e)
        {
            Vars.KeyToNode = JsonConvert.SerializeObject(KeyToNode);
            Vars.Save();
        }

        private void MusicManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings(sender, e);
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {

        }

        public static void RefreshTreeView()
        {
            pObj.InitializeTreeView();
        }

        private void noHScrollTreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                SelectNode(e.Node);
            }
        }
    }
}
