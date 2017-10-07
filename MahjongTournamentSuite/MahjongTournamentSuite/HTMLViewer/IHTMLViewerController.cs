using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahjongTournamentSuite.ViewModel;

namespace MahjongTournamentSuite.HTMLViewer
{
    interface IHTMLViewerController
    {
        void LoadForm(HTMLRankings htmlRankings);

        void CopyHtmlClicked();
    }
}
