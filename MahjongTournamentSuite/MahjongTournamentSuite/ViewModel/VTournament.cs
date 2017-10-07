using System;
using System.ComponentModel.DataAnnotations;

namespace MahjongTournamentSuite._Data.DataModel
{
    public class VTournament
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
        
        public int TournamentId { get; set; }
        public DateTime CreationDate { get; set; }
        public string TournamentName { get; set; }
        public int NumPlayers { get; set; }
        public int NumRounds  { get; set; }
        public bool IsTeams { get; set; }

        #endregion

        #region Constructors

        public VTournament() { }

        public VTournament(int tournamentId, DateTime creationDate, string tournamentName, int numPlayers, int numRounds, bool isTeams)
        {
            TournamentId = tournamentId;
            CreationDate = creationDate;
            TournamentName = tournamentName;
            NumPlayers = numPlayers;
            NumRounds = numRounds;
            IsTeams = isTeams;
        }

        #endregion
    }
}
