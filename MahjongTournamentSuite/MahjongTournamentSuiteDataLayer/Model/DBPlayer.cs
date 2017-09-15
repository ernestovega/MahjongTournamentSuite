using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MahjongTournamentSuiteDataLayer.Model
{
    public class DBPlayer
    {
        #region Constants

        public static readonly string COLUMN_PLAYERS_TOURNAMENT_ID = "PlayerTournamentId";
        public static readonly string COLUMN_PLAYERS_ID = "PlayerId";
        public static readonly string COLUMN_PLAYERS_NAME = "PlayerName";
        public static readonly string COLUMN_PLAYERS_TEAM = "PlayerTeamId";
        public static readonly string COLUMN_PLAYERS_COUNTRY_NAME = "PlayerCountryName";

        #endregion

        #region Properties

        [Key, Column(Order = 0)]
        public int PlayerTournamentId { get; set; }
        [Key, Column(Order = 1)]
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int PlayerTeamId { get; set; }
        public string PlayerCountryName { get; set; }

        #endregion

        #region Constructors

        public DBPlayer() { }

        public DBPlayer(int tournamentId, int id, string name, int teamId, string countryName)
        {
            PlayerTournamentId = tournamentId;
            PlayerId = id;
            PlayerName = name;
            PlayerTeamId = teamId;
            PlayerCountryName = countryName;
        }

        #endregion
    }
}
