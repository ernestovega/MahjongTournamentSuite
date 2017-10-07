using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MahjongTournamentSuite._Data.DataModel
{
    public class DBTeam
    {
        [Key, Column(Order = 0)] public int TeamTournamentId { get; set; }
        [Key, Column(Order = 1)] public int TeamId { get; set; }
        public string TeamName { get; set; }



        public DBTeam() { }

        public DBTeam(int tournamentId, int id, string name)
        {
            TeamTournamentId = tournamentId;
            TeamId = id;
            TeamName = name;
        }
    }
}
