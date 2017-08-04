using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MahjongTournamentSuite.Model
{
    class DBPlayer
    {
        [Key, Column(Order = 0)]
        public int TournamentId { get; set; }

        [Key, Column(Order = 1)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Team { get; set; }

        public string Country { get; set; }

        public DBPlayer() { }

        public DBPlayer(int tournamentId, int id, string name, string team, string country)
        {
            TournamentId = tournamentId;
            Id = id;
            Name = name;
            Team = team;
            Country = country;
        }
    }
}
