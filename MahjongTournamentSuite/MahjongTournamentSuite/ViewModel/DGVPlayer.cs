using MahjongTournamentSuite._Data.DataModel;
using System.Drawing;

namespace MahjongTournamentSuite.ViewModel
{
    public class DGVPlayer : VPlayer
    {
        public static readonly string COLUMN_PLAYERS_TEAM_NAME = "PlayerTeamName";
        public static readonly string COLUMN_PLAYERS_COUNTRY_FLAG = "PlayerCountryFlag";

        public string PlayerTeamName { get; set; }
        public Image PlayerCountryFlag { get; set; }

        public DGVPlayer() {}

        public DGVPlayer(VPlayer dbPlayer, string teamName, Image playerCountryFlag) : 
            base(dbPlayer.PlayerTournamentId, dbPlayer.PlayerId, dbPlayer.PlayerName, 
                dbPlayer.PlayerTeamId, dbPlayer.PlayerCountryName)
        {
            PlayerTeamName = teamName;
            PlayerCountryFlag = playerCountryFlag;
        }
    }
}
