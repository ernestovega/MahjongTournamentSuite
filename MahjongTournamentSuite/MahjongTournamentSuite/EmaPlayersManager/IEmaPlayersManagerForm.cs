using System.Collections.Generic;
using MahjongTournamentSuite.ViewModel;
using MahjongTournamentSuite.EmaReport;

namespace MahjongTournamentSuite.EmaPlayersManager
{
    interface IEmaPlayersManagerForm
    {
        void FillDGV(List<DGVEmaPlayer> emaPlayers);

        void DGVCancelEdit();

        void ShowMessagePlayerNameInUse(string newName, int ownerPlayerId);

        void PlayKoSound();
    }
}
