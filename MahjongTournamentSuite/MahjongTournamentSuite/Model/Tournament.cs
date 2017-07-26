using System;

namespace MahjongTournamentSuite.Model
{
    class Tournament
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumPlayers { get; set; }

        public int NumRounds  { get; set; }

        public DateTime CreationDate { get; set; }

        public Tournament() {}

        public Tournament(int id, int numPlayers, int numRounds, string name, DateTime creationDate)
        {
            Id = id;
            Name = name;
            NumPlayers = numPlayers;
            NumRounds = numRounds;
            CreationDate = creationDate;
        }
    }
}
