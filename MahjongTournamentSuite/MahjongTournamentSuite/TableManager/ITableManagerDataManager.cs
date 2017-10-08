using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;

namespace MahjongTournamentSuite.TableManager
{
    public interface ITableManagerDataManager
    {
        VTable GetTable(int tournamentId, int roundId, int tableId);

        List<VPlayer> GetTablePlayers(int tournamentId, int roundId, int tableId);

        List<VHand> GetTableHands(int tournamentId, int roundId, int tableId);

        string GetTournamentName(int tournamentId);

        void UpdateHandWinnerId(VHand hand);

        void UpdateHandLooserId(VHand hand);

        void UpdateHandScore(VHand hand);

        void UpdateHandIsChickenHand(VHand hand);

        void UpdateHandPlayerEastPenalty(VHand hand);

        void UpdateHandPlayerSouthPenalty(VHand hand);

        void UpdateHandPlayerWestPenalty(VHand hand);

        void UpdateHandPlayerNorthPenalty(VHand hand);

        void UpdateTableIsCompleted(VTable table);

        void UpdateTableUseTotalsOnly(VTable table);

        void UpdateTableAllPlayersPositions(VTable table);

        void UpdateTableAllPlayersTotalScores(VTable table);

        void UpdateTableAllPlayersPoints(VTable table);
    }
}