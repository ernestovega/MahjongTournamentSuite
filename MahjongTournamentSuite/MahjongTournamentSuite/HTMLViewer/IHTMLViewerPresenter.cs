using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahjongTournamentSuite.Model;

namespace MahjongTournamentSuite.HTMLViewer
{
    interface IHTMLViewerPresenter
    {
        void LoadForm(HTMLRankings htmlRankings);

        void CopyHtmlClicked();
    }
}
