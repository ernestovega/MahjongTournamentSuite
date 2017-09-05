using MahjongTournamentSuite.Model;
using System.Windows.Forms;
using System;

namespace MahjongTournamentSuite.HTMLViewer
{
    public partial class HTMLViewerForm : Form, IHTMLViewerForm
    {
        #region FIELDS

        private IHTMLViewerPresenter _presenter;
        private HTMLRankings _htmlRankings;

        #endregion

        #region Constructor

        public HTMLViewerForm(HTMLRankings htmlRankings)
        {
            InitializeComponent();
            _htmlRankings = htmlRankings;
            _presenter = Injector.provideHTMLViewerPresenter(this);
        }

        #endregion

        #region Events

        private void HTMLViewerForm_Load(object sender, System.EventArgs e)
        {
            _presenter.LoadForm(_htmlRankings);
        }

        private void btnCopyPlayers_Click(object sender, EventArgs e)
        {
            _presenter.CopyPlayersClicked();
        }

        private void btnCopyTeams_Click(object sender, EventArgs e)
        {
            _presenter.CopyTeamsClicked();
        }

        private void btnCopyChickenHands_Click(object sender, EventArgs e)
        {
            _presenter.CopyChickenHandsClicked();
        }

        private void btnCopyAll_Click(object sender, EventArgs e)
        {
            _presenter.CopyAllClicked();
        }

        #endregion

        #region IHTMLViewerForm implementation

        public void SetPlayersRankingHTMLText(string playersRanking)
        {
            tbPlayers.Text = playersRanking;
        }

        public void SetTeamsRankingHTMLText(string teamsRanking)
        {
            tbTeams.Text = teamsRanking;
        }

        public void SetChickenHandsRankingHTMLText(string playersChickenHandsRanking)
        {
            tbChickenHands.Text = playersChickenHandsRanking;
        }

        public void SelectPlayersHTMLText()
        {
            tbPlayers.SelectAll();
        }

        public void SelectTeamsHTMLText()
        {
            tbTeams.SelectAll();
        }

        public void SelectChickenHandsHTMLText()
        {
            tbChickenHands.SelectAll();
        }

        public void DisableTeamsControls()
        {
            tbTeams.Enabled = false;
            btnCopyTeams.Enabled = false;
        }

        #endregion
    }
}
