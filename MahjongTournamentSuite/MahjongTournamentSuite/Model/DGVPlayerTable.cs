namespace MahjongTournamentSuite.Model
{
    public class DGVPlayerTable
    {
        public static readonly string COLUMN_ROUNDID = "RoundId";
        public static readonly string COLUMN_TABLEID = "TableId";

        public int RoundId { get; set; }
        public int TableId { get; set; }

        public DGVPlayerTable(int roundId, int tableId)
        {
            RoundId = roundId;
            TableId = tableId;
        }
    }
}
