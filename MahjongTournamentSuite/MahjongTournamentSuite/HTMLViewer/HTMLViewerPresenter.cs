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
        }

        public void CopyAllClicked()
        {
            Clipboard.SetText(_htmlRankings.PlayersRanking);
        }

        public void CopyChickenHandsClicked()
        {
            Clipboard.SetText(_htmlRankings.TeamsRanking);
        }

        public void CopyPlayersClicked()
        {
            Clipboard.SetText(_htmlRankings.PlayersChickenHandsRanking);
        }

        public void CopyTeamsClicked()
        {
            string textToCopy;

            if (_htmlRankings.IsTeams)
            {
                textToCopy = string.Format("{0}\n\n\n{1}\n\n\n{2}",
                    _htmlRankings.PlayersRanking,
                    _htmlRankings.TeamsRanking,
                    _htmlRankings.PlayersChickenHandsRanking);
            }
            else
            {
                textToCopy = string.Format("{0}\n\n\n{1}",
                    _htmlRankings.PlayersRanking,
                    _htmlRankings.PlayersChickenHandsRanking);
            }

            Clipboard.SetText(textToCopy);
        }

        #endregion
    }
}
