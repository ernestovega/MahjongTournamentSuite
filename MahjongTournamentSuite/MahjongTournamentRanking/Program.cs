using System;
using System.Windows.Forms;

namespace MahjongTournamentRanking
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main.MainForm(2));//int.Parse(args[0])));
        }

        public string returnExecutablePath()
        {
            return string.Format("{0}\\{1}", Application.StartupPath, "MahjongTournamentRanking.exe");
        }
    }
}
