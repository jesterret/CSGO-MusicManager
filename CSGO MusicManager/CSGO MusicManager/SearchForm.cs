using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace CSGO_MusicManager
{
    public partial class SearchForm : Form
    {
        private TreeView SearchTree;

        public SearchForm(TreeView SongList)
        {
            InitializeComponent();
            SearchTree = SongList;
        }

        private void FindNode(TreeNode Tree)
        {
            foreach(TreeNode node in Tree.Nodes)
            {
                if(node.Text.IndexOf(SearchText.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    FoundNodes.Nodes.Add(node.Clone() as TreeNode);
                }
                FindNode(node);
            }
        }

        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' && SearchTree.Nodes.Count > 0)
            {
                FoundNodes.BeginUpdate();
                FoundNodes.Nodes.Clear();
                var tree = SearchTree.Nodes[0];
                FindNode(tree);
                FoundNodes.Visible = true;
                FoundNodes.EndUpdate();
            }
        }

        private void NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode Node = e.Node;
            if (Node.GetNodeCount(false) != 0 && Directory.Exists(Node.Name))
                MusicManager.CurrentCheckDir = Node.Name;
            else
                MusicManager.pObj.SelectNode(Node, true);
        }

        private void NodeDoubleMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode Node = e.Node;
            if (e.Button != MouseButtons.Left)
                return;

            if (Node.GetNodeCount(false) == 0 && !Directory.Exists(Node.Name))
            {
                MusicManager.CurrentCheckDir = Directory.GetParent(Node.Name).FullName;
                MusicManager.pObj.WriteCSGOFiles(Node.Name, Node.Text);
            }
        }
    }
}
