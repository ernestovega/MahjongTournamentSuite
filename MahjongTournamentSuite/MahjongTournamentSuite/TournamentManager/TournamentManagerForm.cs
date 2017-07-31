using System.Diagnostics;
using System.Windows.Forms;

namespace MahjongTournamentSuite.TournamentManager
{
    public partial class TournamentManagerForm : Form, ITournamentManagerForm
    {
        #region

        private ITournamentManagerPresenter _presenter;
        private int _tournamentId;

        #endregion

        #region Constructor

        public TournamentManagerForm(int tournamentId)
        {
            InitializeComponent();
            _tournamentId = tournamentId;
            _presenter = Injector.provideTournamentManagerPresenter(this);
            _presenter.loadTournament(_tournamentId);
        }

        #endregion

        #region Events

        private void btnTimer_Click(object sender, System.EventArgs e)
        {
            var mahjongTournamentTimer = new MahjongTournamentTimer.Program();
            Process.Start(mahjongTournamentTimer.returnExecutablePath());
        }

        private void btnRanking_Click(object sender, System.EventArgs e)
        {

        }

        #endregion

        #region ITournamentManagerForm

        #endregion

        #region Private

        #endregion
    }
}
