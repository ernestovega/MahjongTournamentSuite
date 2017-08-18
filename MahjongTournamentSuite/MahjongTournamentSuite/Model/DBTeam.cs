using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MahjongTournamentSuite.Model
{
    public class DBTeam
    {
        [Key, Column(Order = 0)]
        public int TournamentId { get; set; }

        [Key, Column(Order = 1)]
        public int Id { get; set; }

        public string Name { get; set; }

        public DBTeam() { }

        public DBTeam(int tournamentId, int id, string name)
        {
            TournamentId = tournamentId;
            Id = id;
            Name = name;
        }
    }
}
