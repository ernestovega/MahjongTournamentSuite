using MahjongTournamentSuite._Data.DataModel;
using MahjongTournamentSuite.ViewModel;
using System.Collections.Generic;

namespace MahjongTournamentSuite.Home
{
    internal interface IHomeForm
    {
        void FillDGVTournaments(List<VTournament> tournaments);

        string GetCurrentTournamentName();

        int GetCurrentTournamentId();

        string RequestNewTournamentName();

        bool RequestDeleteTournamentConfirmation();

        void EnableResumeAndDeleteButton();

        void DisableResumeAndDeleteButton();
    }
}