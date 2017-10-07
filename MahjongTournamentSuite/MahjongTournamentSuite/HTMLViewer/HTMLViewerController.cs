using MahjongTournamentSuite.ViewModel;
using System.Windows.Forms;

namespace MahjongTournamentSuite.HTMLViewer
{
    class HTMLViewerController : IHTMLViewerController
    {
        #region Fields

        private IHTMLViewerForm _form;
        private HTMLRankings _htmlRankings;
        private string _sHtmlRankings;

        #endregion

        #region Constructor

        public HTMLViewerController(IHTMLViewerForm form)
        {
            _form = form;
        }

        #endregion

        #region IHTMLViewerController implementation

        public void LoadForm(HTMLRankings htmlRankings)
        {
            _htmlRankings = htmlRankings;
            if (_htmlRankings.IsTeams)
                _sHtmlRankings = string.Format("{0}\n\n{1}\n\n{2}", _htmlRankings.PlayersRanking, _htmlRankings.TeamsRanking, _htmlRankings.PlayersChickenHandsRanking);
            else
                _sHtmlRankings = string.Format("{0}\n\n{1}", _htmlRankings.PlayersRanking, _htmlRankings.PlayersChickenHandsRanking);

            _form.SetRankingHTMLText(_sHtmlRankings);
            CopyHtmlClicked();
        }

        public void CopyHtmlClicked()
        {
            Clipboard.Clear();
            Clipboard.SetText(_sHtmlRankings);
            _form.SelectHTMLText();
        }

        #endregion
    }
}
