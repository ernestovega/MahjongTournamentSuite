using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;

namespace MahjongPlayerSuite._Data.Mappers
{
    public class PlayerMapper
    {
        public static List<VPlayer> GetViewModel(List<DBPlayer> dbPlayers)
        {
            List<VPlayer> vPlayers = new List<VPlayer>(dbPlayers.Count);
            foreach(DBPlayer dbPlayer in dbPlayers)
                vPlayers.Add(GetViewModel(dbPlayer));

            return vPlayers;
        }

        public static VPlayer GetViewModel(DBPlayer dbPlayer)
        {
            return new VPlayer(
                dbPlayer.PlayerTournamentId,
                dbPlayer.PlayerId,
                dbPlayer.PlayerName,
                dbPlayer.PlayerTeamId,
                dbPlayer.PlayerCountryName);
        }

        public static List<DBPlayer> GetDataModel(List<VPlayer> vPlayers)
        {
            List<DBPlayer> dbPlayers = new List<DBPlayer>(vPlayers.Count);
            foreach (VPlayer vPlayer in vPlayers)
                dbPlayers.Add(GetDataModel(vPlayer));

            return dbPlayers;
        }

        public static DBPlayer GetDataModel(VPlayer vPlayer)
        {
            return new DBPlayer(
                vPlayer.PlayerTournamentId,
                vPlayer.PlayerId,
                vPlayer.PlayerName,
                vPlayer.PlayerTeamId,
                vPlayer.PlayerCountryName);
        }
    }
}
