using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MahjongTournamentSuite._Data.DataModel
{
    public class DBPlayer
    {
        [Key, Column(Order = 0)]
        public int PlayerTournamentId { get; set; }
        [Key, Column(Order = 1)]
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int PlayerTeamId { get; set; }
        public string PlayerCountryName { get; set; }

        public DBPlayer() { }

        public DBPlayer(int tournamentId, int id, string name, int teamId, string countryName)
        {
            PlayerTournamentId = tournamentId;
            PlayerId = id;
            PlayerName = name;
            PlayerTeamId = teamId;
            PlayerCountryName = countryName;
        }
    }
}
