using System;
using System.ComponentModel.DataAnnotations;

namespace MahjongTournamentSuiteDataLayer.Model
{
    public class DBTournament
    {
        #region Constants

        public static readonly string COLUMN_ID = "TournamentId";
        public static readonly string COLUMN_DATE = "CreationDate";
        public static readonly string COLUMN_NAME = "TournamentName";
        public static readonly string COLUMN_PLAYERS = "NumPlayers";
        public static readonly string COLUMN_ROUNDS = "NumRounds";
        public static readonly string COLUMN_IS_TEAMS = "IsTeams";

        #endregion

        #region Properties

        [Key]
        public int TournamentId { get; set; }
        public DateTime CreationDate { get; set; }
        public string TournamentName { get; set; }
        public int NumPlayers { get; set; }
        public int NumRounds  { get; set; }
        public bool IsTeams { get; set; }

        #endregion

        #region Constructors

        public DBTournament() {}

        public DBTournament(DateTime creationDate, int numPlayers, int numRounds, bool isTeams, string name)
        {
            CreationDate = creationDate;
            TournamentName = name;
            NumPlayers = numPlayers;
            NumRounds = numRounds;
            IsTeams = isTeams;
        }

        #endregion
    }
}
