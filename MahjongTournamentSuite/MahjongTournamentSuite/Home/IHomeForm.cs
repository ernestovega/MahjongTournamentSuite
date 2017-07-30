using System.Collections.Generic;
using System.Windows.Forms;

namespace MahjongTournamentSuite.Home
{
    internal interface IHomeForm
    {
        void ReloadDataGridTournaments();

        string GetCurrentTournamentName();

        int GetCurrentTournamentId();

        string RequestNewTournamentName();

        bool RequestDeleteTournamentConfirmation();
    }
}