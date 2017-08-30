using System;
using System.ComponentModel.DataAnnotations;

namespace MahjongTournamentSuiteDataLayer.Model
{
    public class DBTournament
    {
        [Key]
        public int TournamentId { get; set; }

        public DateTime CreationDate { get; set; }

        public string TournamentName { get; set; }

        public int NumPlayers { get; set; }

        public int NumRounds  { get; set; }

        public bool IsTeams { get; set; }

        public DBTournament() {}

        public DBTournament(int id, DateTime creationDate, int numPlayers, int numRounds, bool isTeams, string name)
        {
            TournamentId = id;
            CreationDate = creationDate;
            TournamentName = name;
            NumPlayers = numPlayers;
            NumRounds = numRounds;
            IsTeams = isTeams;
        }
    }
}
