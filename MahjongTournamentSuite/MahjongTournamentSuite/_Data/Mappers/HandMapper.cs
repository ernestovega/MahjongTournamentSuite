using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;

namespace MahjongTournamentSuite._Data.Mappers
{
    public class HandMapper
    {
        public static List<VHand> GetViewModel(List<DBHand> dbHands)
        {
            List<VHand> vHands = new List<VHand>(dbHands.Count);
            foreach(DBHand dbHand in dbHands)
                vHands.Add(GetViewModel(dbHand));

            return vHands;
        }

        public static VHand GetViewModel(DBHand dbHand)
        {
            return new VHand(
                dbHand.HandTournamentId,
                dbHand.HandRoundId,
                dbHand.HandTableId,
                dbHand.HandId,
                dbHand.PlayerWinnerId,
                dbHand.PlayerLooserId,
                dbHand.HandScore,
                dbHand.IsChickenHand,
                dbHand.PlayerEastPenalty,
                dbHand.PlayerSouthPenalty,
                dbHand.PlayerWestPenalty,
                dbHand.PlayerNorthPenalty);
        }

        public static List<DBHand> GetDataModel(List<VHand> vHands)
        {
            List<DBHand> dbHands = new List<DBHand>(vHands.Count);
            foreach (VHand vHand in vHands)
                dbHands.Add(GetDataModel(vHand));

            return dbHands;
        }

        public static DBHand GetDataModel(VHand vHand)
        {
            return new DBHand(
                vHand.HandTournamentId,
                vHand.HandRoundId,
                vHand.HandTableId,
                vHand.HandId,
                vHand.PlayerWinnerId,
                vHand.PlayerLooserId,
                vHand.HandScore,
                vHand.IsChickenHand,
                vHand.PlayerEastPenalty,
                vHand.PlayerSouthPenalty,
                vHand.PlayerWestPenalty,
                vHand.PlayerNorthPenalty);
        }
    }
}
