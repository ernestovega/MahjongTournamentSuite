using MahjongTournamentSuite.Model;
using System.Windows.Forms;

namespace MahjongTournamentSuite.HTMLViewer
{
    class HTMLViewerPresenter : IHTMLViewerPresenter
    {
        #region FIELDS

        private IHTMLViewerForm _form;
        private HTMLRankings _htmlRankings;

        #endregion

        #region Constructor

        public HTMLViewerPresenter(IHTMLViewerForm form)
        {
            _form = form;
        }

        #endregion

        #region IHTMLViewerPresenter implementation

        public void LoadForm(HTMLRankings htmlRankings)
        {
            _htmlRankings = htmlRankings;
            _form.SetPlayersRankingHTMLText(_htmlRankings.PlayersRanking);
            if (_htmlRankings.IsTeams)
                _form.SetTeamsRankingHTMLText(_htmlRankings.TeamsRanking);
            else
                _form.DisableTeamsControls();
            _form.SetChickenHandsRankingHTMLText(_htmlRankings.PlayersChickenHandsRanking);
            CopyPlayersClicked();
        }

        public void CopyPlayersClicked()
        {
            Clipboard.Clear();
            Clipboard.SetText(_htmlRankings.PlayersRanking);
            _form.SelectPlayersHTMLText();
        }

        public void CopyTeamsClicked()
        {
            Clipboard.Clear();
            Clipboard.SetText(_htmlRankings.TeamsRanking);
            _form.SelectTeamsHTMLText();
        }

        public void CopyChickenHandsClicked()
        {
            Clipboard.Clear();
            Clipboard.SetText(_htmlRankings.PlayersChickenHandsRanking);
            _form.SelectChickenHandsHTMLText();
        }

        #endregion
    }
}
