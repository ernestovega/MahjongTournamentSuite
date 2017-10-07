using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MahjongTournamentSuite._Data.DataModel
{
    public class DBTournament
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TournamentId { get; set; }
        public DateTime CreationDate { get; set; }
        public string TournamentName { get; set; }
        public int NumPlayers { get; set; }
        public int NumRounds  { get; set; }
        public bool IsTeams { get; set; }




        public DBTournament() { }

        public DBTournament(int tournamentId, DateTime creationDate, string tournamentName, int numPlayers, int numRounds, bool isTeams)
        {
            TournamentId = tournamentId;
            CreationDate = creationDate;
            TournamentName = tournamentName;
            NumPlayers = numPlayers;
            NumRounds = numRounds;
            IsTeams = isTeams;
        }
    }
}
