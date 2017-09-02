using MahjongTournamentSuite.Home;
using MahjongTournamentSuite.Splash;
using System;
using System.Threading;
using System.Windows.Forms;

namespace MahjongTournamentSuite
{
    static class Program
    {
        private static SplashForm splashForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //show splash
            Thread splashThread = new Thread(new ThreadStart(
                delegate
                {
                    splashForm = new SplashForm();
                    Application.Run(splashForm);
                }
                ));

            splashThread.SetApartmentState(ApartmentState.STA);
            splashThread.Start();

            //run form - time taking operation
            HomeForm homeForm = new HomeForm();
            homeForm.Load += new EventHandler(HomeForm_Load);
            homeForm.Show();
            Application.Run();
        }

        static void HomeForm_Load(object sender, EventArgs e)
        {
            //close splash
            if (splashForm == null)
                return;
            splashForm.Invoke(new Action(splashForm.Close));
            splashForm.Dispose();
            splashForm = null;
        }
    }
}
