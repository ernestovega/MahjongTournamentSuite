using MahjongTournamentSuite.Model;
using System.Collections.Generic;

namespace MahjongTournamentSuite.TableManager
{
    interface ITableManagerForm
    {
        void SetTournamentName(string tournamentName);

        void SetRoundId(int roundId);

        void SetTableId(int tableId);

        void FillCombosPlayers(List<ComboItem> comboPlayers);

        void FillDataGridHands(List<DBHand> hands);
    }
}
