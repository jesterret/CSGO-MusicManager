using System.Windows.Forms;

namespace CSGO_MusicManager
{
    public class NoHScrollTreeView : TreeView
    {
        public NoHScrollTreeView() : base() { }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x8000; // TVS_NOHSCROLL
                return cp;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
            //    return true;

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}