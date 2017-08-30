using MahjongTournamentSuiteDataLayer.Model;
using MahjongTournamentSuitePresentationLayer.Model;
using System.Collections.Generic;

namespace MahjongTournamentSuitePresentationLayer.Home
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