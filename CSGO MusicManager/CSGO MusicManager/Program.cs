using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSGO_MusicManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isNew = false;
            var mut = new Mutex(true, "CSGo MusicManager", out isNew);
            if (isNew)
            {
                GC.KeepAlive(mut);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MusicManager());
                mut.ReleaseMutex();
            }
            else
                MessageBox.Show("CSGO Music Manager already running. Exiting...");
        }
    }
}
