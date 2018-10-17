using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MahjongTournamentSuite._Data.DataModel
{
    public class VPlayer
    {
        #region Constants

        public static readonly string COLUMN_PLAYERS_TOURNAMENT_ID = "PlayerTournamentId";
        public static readonly string COLUMN_PLAYERS_ID = "PlayerId";
        public static readonly string COLUMN_PLAYERS_NAME = "PlayerName";
        public static readonly string COLUMN_PLAYERS_TEAM = "PlayerTeamId";
        public static readonly string COLUMN_PLAYERS_COUNTRY_NAME = "PlayerCountryName"; 
        public static readonly string COLUMN_PLAYERS_EMA_NUMBER = "PlayerEmaNumber";

        #endregion

        #region Properties

        public int PlayerTournamentId { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int PlayerTeamId { get; set; }
        public string PlayerCountryName { get; set; }
        public string PlayerEmaNumber { get; set; }

        #endregion

        #region Constructors

        public VPlayer() { }

        public VPlayer(int tournamentId, int id, string name, int teamId, string countryName, string emaNumber)
        {
            PlayerTournamentId = tournamentId;
            PlayerId = id;
            PlayerName = name;
            PlayerTeamId = teamId;
            PlayerCountryName = countryName;
            PlayerEmaNumber = emaNumber;
        }

        #endregion
    }
}
