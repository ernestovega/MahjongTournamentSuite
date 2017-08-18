using System;
using System.ComponentModel.DataAnnotations;

namespace MahjongTournamentSuite.Model
{
    public class DBTournament
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public string Name { get; set; }

        public int NumPlayers { get; set; }

        public int NumRounds  { get; set; }

        public bool IsTeams { get; set; }

        public DBTournament() {}

        public DBTournament(int id, DateTime creationDate, int numPlayers, int numRounds, bool isTeams, string name)
        {
            Id = id;
            CreationDate = creationDate;
            Name = name;
            NumPlayers = numPlayers;
            NumRounds = numRounds;
            IsTeams = isTeams;
        }
    }
}
