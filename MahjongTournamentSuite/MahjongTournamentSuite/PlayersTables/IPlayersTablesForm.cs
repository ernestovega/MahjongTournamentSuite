
using System.Collections.Generic;

namespace MahjongTournamentSuite.PlayersTables
{
    interface IPlayersTablesForm
    {
        void GeneratePlayersButtons(int numPlayers);

        void ShowMessageBoxTablesPlayer(int playerId, List<int> tablesPlayer);
    }
}
