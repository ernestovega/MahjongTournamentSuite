using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MahjongTournamentSuite._Data.DataModel
{
    public class VTeam
    {
        #region Constants

        public static readonly string COLUMN_TEAMS_TOURNAMENT_ID = "TeamTournamentId";
        public static readonly string COLUMN_TEAMS_ID = "TeamId";
        public static readonly string COLUMN_TEAMS_NAME = "TeamName";

        #endregion

        #region Properties

        public int TeamTournamentId { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }

        #endregion

        #region Constructors

        public VTeam() { }

        public VTeam(int tournamentId, int id, string name)
        {
            TeamTournamentId = tournamentId;
            TeamId = id;
            TeamName = name;
        }

        #endregion
    }
}
