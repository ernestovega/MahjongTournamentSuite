using MahjongTournamentSuite.Home;
using MahjongTournamentSuite.Splash;
using System;
using System.Windows.Forms;

namespace MahjongTournamentSuite
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var homeForm = new HomeForm();
            homeForm.FormClosed += new FormClosedEventHandler(FormClosed);
            homeForm.Show();
            Application.Run();
        }

        static void FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= FormClosed;
            if (Application.OpenForms.Count == 0) Application.ExitThread();
            else Application.OpenForms[0].FormClosed += FormClosed;
        }
    }
}
