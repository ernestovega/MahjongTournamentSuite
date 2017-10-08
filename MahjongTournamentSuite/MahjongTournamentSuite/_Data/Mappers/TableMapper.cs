using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;

namespace MahjongTableSuite._Data.Mappers
{
    public class TableMapper
    {
        public static List<VTable> GetViewModel(List<DBTable> dbTables)
        {
            List<VTable> vTables = new List<VTable>(dbTables.Count);
            foreach(DBTable dbTable in dbTables)
                vTables.Add(GetViewModel(dbTable));

            return vTables;
        }

        public static VTable GetViewModel(DBTable dbTable)
        {
            return new VTable(
                dbTable.TableTournamentId,
                dbTable.TableRoundId,
                dbTable.TableId,
                dbTable.Player1Id,
                dbTable.Player2Id,
                dbTable.Player3Id,
                dbTable.Player4Id,
                dbTable.PlayerEastId,
                dbTable.PlayerSouthId,
                dbTable.PlayerWestId,
                dbTable.PlayerNorthId,
                dbTable.PlayerEastScore,
                dbTable.PlayerSouthScore,
                dbTable.PlayerWestScore,
                dbTable.PlayerNorthScore,
                dbTable.PlayerEastPoints,
                dbTable.PlayerSouthPoints,
                dbTable.PlayerWestPoints,
                dbTable.PlayerNorthPoints,
                dbTable.IsCompleted,
                dbTable.UseTotalsOnly);
        }

        public static List<DBTable> GetDataModel(List<VTable> vTables)
        {
            List<DBTable> dbTables = new List<DBTable>(vTables.Count);
            foreach (VTable vTable in vTables)
                dbTables.Add(GetDataModel(vTable));

            return dbTables;
        }

        public static DBTable GetDataModel(VTable vTable)
        {
            return new DBTable(
                vTable.TableTournamentId,
                vTable.TableRoundId,
                vTable.TableId,
                vTable.Player1Id,
                vTable.Player2Id,
                vTable.Player3Id,
                vTable.Player4Id,
                vTable.PlayerEastId,
                vTable.PlayerSouthId,
                vTable.PlayerWestId,
                vTable.PlayerNorthId,
                vTable.PlayerEastScore,
                vTable.PlayerSouthScore,
                vTable.PlayerWestScore,
                vTable.PlayerNorthScore,
                vTable.PlayerEastPoints,
                vTable.PlayerSouthPoints,
                vTable.PlayerWestPoints,
                vTable.PlayerNorthPoints,
                vTable.IsCompleted,
                vTable.UseTotalsOnly);
        }
    }
}
