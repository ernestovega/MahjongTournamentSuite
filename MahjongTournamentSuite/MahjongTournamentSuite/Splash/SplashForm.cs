using MahjongTournamentSuite.Home;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MahjongTournamentSuite.Splash
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
        }

        private void imgLogoMM_Click(object sender, EventArgs e)
        {
            ShowWaitCursor();
            ProcessStartInfo sInfo = new ProcessStartInfo("http://www.mahjongmadrid.com/");
            Process.Start(sInfo);
            ShowDefaultCursor();
        }

        private void imgLogoEMA_Click(object sender, EventArgs e)
        {
            ShowWaitCursor();
            ProcessStartInfo sInfo = new ProcessStartInfo("http://mahjong-europe.org/portal/");
            Process.Start(sInfo);
            ShowDefaultCursor();
        }

        private void ShowWaitCursor()
        {
            Cursor = Cursors.WaitCursor;
        }

        private void ShowDefaultCursor()
        {
            Cursor = Cursors.Default;
        }
    }
}
