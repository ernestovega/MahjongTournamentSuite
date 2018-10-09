using System.Collections.Generic;
using MahjongTournamentSuite.ViewModel;
using MahjongTournamentSuite.EmaReport;

namespace MahjongTournamentSuite.EmaPlayersManager
{
    interface IEmaPlayersManagerForm
    {
        void FillDGV(List<DGVEmaPlayer> emaPlayers);

        void DGVCancelEdit();

        void ShowMessagePlayerEmaNumberInUse(string emaNumberInUse);

        void PlayKoSound();
    }
}
