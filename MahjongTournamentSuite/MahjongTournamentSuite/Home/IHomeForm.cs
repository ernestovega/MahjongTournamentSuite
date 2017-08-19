using MahjongTournamentSuite.Model;
using System.Collections.Generic;

namespace MahjongTournamentSuite.Home
{
    internal interface IHomeForm
    {
        void FillDataGridTournaments(List<DBTournament> tournaments);

        string GetCurrentTournamentName();

        int GetCurrentTournamentId();

        string RequestNewTournamentName();

        bool RequestDeleteTournamentConfirmation();

        void EnableResumeAndDeleteButton();

        void DisableResumeAndDeleteButton();
    }
}