using System;
using System.ComponentModel.DataAnnotations;

namespace MahjongTournamentSuite.Model
{
    public class DBTournament
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumPlayers { get; set; }

        public int NumRounds  { get; set; }

        public bool IsTeams { get; set; }

        public DateTime CreationDate { get; set; }

        public DBTournament() {}

        public DBTournament(int id, int numPlayers, int numRounds, bool isTeams, string name, DateTime creationDate)
        {
            Id = id;
            Name = name;
            NumPlayers = numPlayers;
            NumRounds = numRounds;
            IsTeams = isTeams;
            CreationDate = creationDate;
        }
    }
}
