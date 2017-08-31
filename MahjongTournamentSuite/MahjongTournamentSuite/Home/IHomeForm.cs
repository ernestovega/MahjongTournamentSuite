﻿using MahjongTournamentSuiteDataLayer.Model;
using MahjongTournamentSuite.Model;
using System.Collections.Generic;

namespace MahjongTournamentSuite.Home
{
    internal interface IHomeForm
    {
        void FillDGVTournaments(List<DBTournament> tournaments);

        string GetCurrentTournamentName();

        int GetCurrentTournamentId();

        string RequestNewTournamentName();

        bool RequestDeleteTournamentConfirmation();

        void EnableResumeAndDeleteButton();

        void DisableResumeAndDeleteButton();
    }
}