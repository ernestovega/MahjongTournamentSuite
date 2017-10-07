using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;
using System;

namespace MahjongTournamentSuite._Data.Mappers
{
    public class TournamentMapper
    {
        public static List<VTournament> GetViewModel(List<DBTournament> dbTournaments)
        {
            List<VTournament> vTournaments = new List<VTournament>(dbTournaments.Count);
            foreach(DBTournament dbTournament in dbTournaments)
                vTournaments.Add(GetViewModel(dbTournament));

            return vTournaments;
        }

        public static VTournament GetViewModel(DBTournament dbTournament)
        {
            return new VTournament(
                (int)dbTournament.TournamentId,
                dbTournament.CreationDate,
                dbTournament.TournamentName,
                dbTournament.NumPlayers,
                dbTournament.NumRounds,
                dbTournament.IsTeams);
        }

        public static List<DBTournament> GetDataModel(List<VTournament> vTournaments)
        {
            List<DBTournament> dbTournaments = new List<DBTournament>(vTournaments.Count);
            foreach (VTournament vTournament in vTournaments)
                dbTournaments.Add(GetDataModel(vTournament));

            return dbTournaments;
        }

        public static DBTournament GetDataModel(VTournament vTournament)
        {
            return new DBTournament(
                vTournament.TournamentId,
                vTournament.CreationDate,
                vTournament.TournamentName,
                vTournament.NumPlayers,
                vTournament.NumRounds,
                vTournament.IsTeams);
        }
    }
}
