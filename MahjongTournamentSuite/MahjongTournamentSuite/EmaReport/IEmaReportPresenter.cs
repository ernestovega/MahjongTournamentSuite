using System.Collections.Generic;

namespace MahjongTournamentSuite.EmaReport
{
    interface IEmaReportPresenter
    {
        void LoadForm(List<DGVPlayerEma> dgvEmaPlayers);
    }
}
