﻿using System.Collections.Generic;

namespace MahjongTournamentSuite.EmaReport
{
    public interface IEmaReportForm
    {
        void LoadDgv(List<DGVPlayerEma> dgvEmaPlayers);
    }
}