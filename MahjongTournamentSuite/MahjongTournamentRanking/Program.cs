using MahjongTournamentRanking.Main;
using System;
using System.Windows.Forms;

namespace MahjongTournamentRanking
{
    public class Program
    {
        private static int _tournamentId;

        public Program (int tournamentId)
        {
            _tournamentId = tournamentId;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(_tournamentId));
        }

        public string returnExecutablePath()
        {
            return string.Format("{0}\\{1}", Application.StartupPath, "MahjongTournamentRanking.exe");
        }
    }
}
