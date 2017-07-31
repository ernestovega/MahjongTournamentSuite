using System.Windows.Forms;

namespace MahjongTournamentSuite.TournamentManager
{
    public partial class TournamentManagerForm : Form, ITournamentManagerForm
    {
        #region

        private ITournamentManagerPresenter _presenter;

        #endregion

        #region Constructor

        public TournamentManagerForm()
        {
            InitializeComponent();
            _presenter = Injector.provideTournamentManagerPresenter(this);
        }

        #endregion

        #region ITournamentManagerForm

        #endregion

        #region Private

        #endregion
    }
}
