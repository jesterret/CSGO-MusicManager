using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSGO_MusicManager
{
    public partial class SettingsForm : Form
    {
        Properties.Settings Vars;
        DataGridView.HitTestInfo hit;

        public SettingsForm()
        {
            Vars = Properties.Settings.Default;
            InitializeComponent();
            checkBox1.Checked = Vars.CheckUpdates;
            KeyColumn.ValueType = typeof(Keys);
            var y = MusicManager.pObj.KeyToNode.First;
            foreach (var x in y)
            {
                dataGridView1.Rows.Add(new object[] { x.Key, x.Value });
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Vars.CheckUpdates = checkBox1.Checked;
            Close();
        }

        private void deleteBindingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var key = (string)dataGridView1.Rows[hit.RowIndex].Cells[1].Value;
            MusicManager.pObj.KeyToNode.TryRemoveBySecond(key);
            dataGridView1.Rows.RemoveAt(hit.RowIndex);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            var x = dataGridView1.PointToClient(MousePosition);
            hit = dataGridView1.HitTest(x.X, x.Y);
            if (hit.Type != DataGridViewHitTestType.Cell || dataGridView1.Rows[hit.RowIndex].Cells[1].Value == null)
                e.Cancel = true;
        }
    }
}
