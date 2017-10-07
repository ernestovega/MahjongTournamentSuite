using System.Collections.Generic;

namespace MahjongTournamentSuite.EmaReport
{
    interface IEmaReportController
    {
        void LoadForm(List<DGVPlayerEma> dgvEmaPlayers);
    }
}
