using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;

namespace MahjongPlayerSuite._Data.Mappers
{
    public class EmaPlayerMapper
    {
        public static List<VEmaPlayer> GetViewModel(List<DBEmaPlayer> dbEmaPlayers)
        {
            List<VEmaPlayer> emaPlayers = new List<VEmaPlayer>(dbEmaPlayers.Count);
            foreach(DBEmaPlayer dbEmaPlayer in dbEmaPlayers)
                emaPlayers.Add(GetViewModel(dbEmaPlayer));

            return emaPlayers;
        }

        public static VEmaPlayer GetViewModel(DBEmaPlayer dbEmaPlayer)
        {
            VEmaPlayer vEmaPlayer = new VEmaPlayer(
                dbEmaPlayer.EmaPlayerEmaNumber,
                dbEmaPlayer.EmaPlayerLastName,
                dbEmaPlayer.EmaPlayerName,
                dbEmaPlayer.EmaPlayerCountryName);
            return vEmaPlayer;
        }

        public static List<DBEmaPlayer> GetDataModel(List<VEmaPlayer> emaPlayers)
        {
            List<DBEmaPlayer> dbEmaPlayers = new List<DBEmaPlayer>(emaPlayers.Count);
            foreach (VEmaPlayer emaPlayer in emaPlayers)
                dbEmaPlayers.Add(GetDataModel(emaPlayer));

            return dbEmaPlayers;
        }

        public static DBEmaPlayer GetDataModel(VEmaPlayer emaPlayer)
        {
            return new DBEmaPlayer(
                emaPlayer.EmaPlayerEmaNumber,
                emaPlayer.EmaPlayerLastName,
                emaPlayer.EmaPlayerName,
                emaPlayer.EmaPlayerCountryName);
        }
    }
}
