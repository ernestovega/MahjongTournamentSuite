using System;
using System.Windows.Forms;

namespace MahjongTournamentTimer
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public string returnExecutablePath()
        {
            return string.Format("{0}\\{1}", Application.StartupPath, "MahjongTournamentTimer.exe");
        }
    }
}
