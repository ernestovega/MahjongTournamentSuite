
using MahjongTournamentSuite.ViewModel;
using System.Collections.Generic;

namespace MahjongTournamentSuite.PlayersTables
{
    interface IPlayersTablesForm
    {
        void GeneratePlayersButtons(int numPlayers);

        void ShowPlayerTables(PlayerTables playerTables);
    }
}
