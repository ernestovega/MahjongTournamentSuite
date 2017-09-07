using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MahjongTournamentSuiteDataLayer.Model
{
    public class DBTeam
    {
        #region Constants

        public static readonly string COLUMN_TEAMS_TOURNAMENT_ID = "TeamTournamentId";
        public static readonly string COLUMN_TEAMS_ID = "TeamId";
        public static readonly string COLUMN_TEAMS_NAME = "TeamName";

        #endregion

        #region Properties

        [Key, Column(Order = 0)] public int TeamTournamentId { get; set; }
        [Key, Column(Order = 1)] public int TeamId { get; set; }
        public string TeamName { get; set; }

        #endregion

        #region Constructors

        public DBTeam() { }

        public DBTeam(int tournamentId, int id, string name)
        {
            TeamTournamentId = tournamentId;
            TeamId = id;
            TeamName = name;
        }

        #endregion
    }
}
