using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;

namespace MahjongTeamSuite._Data.Mappers
{
    public class TeamMapper
    {
        public static List<VTeam> GetViewModel(List<DBTeam> dbTeams)
        {
            List<VTeam> vTeams = new List<VTeam>(dbTeams.Count);
            foreach(DBTeam dbTeam in dbTeams)
                vTeams.Add(GetViewModel(dbTeam));

            return vTeams;
        }

        public static VTeam GetViewModel(DBTeam dbTeam)
        {
            return new VTeam(
                dbTeam.TeamTournamentId,
                dbTeam.TeamId,
                dbTeam.TeamName);
        }

        public static List<DBTeam> GetDataModel(List<VTeam> vTeams)
        {
            List<DBTeam> dbTeams = new List<DBTeam>(vTeams.Count);
            foreach (VTeam vTeam in vTeams)
                dbTeams.Add(GetDataModel(vTeam));

            return dbTeams;
        }

        public static DBTeam GetDataModel(VTeam vTeam)
        {
            return new DBTeam(
                vTeam.TeamTournamentId,
                vTeam.TeamId,
                vTeam.TeamName);
        }
    }
}
