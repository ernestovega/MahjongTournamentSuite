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

        public DGVPlayer(VPlayer vPlayer, string teamName, Image playerCountryFlag) : 
            base(vPlayer.PlayerTournamentId, vPlayer.PlayerId, vPlayer.PlayerName, 
                vPlayer.PlayerTeamId, vPlayer.PlayerCountryName)
        {
            PlayerTeamName = teamName;
            PlayerCountryFlag = playerCountryFlag;
        }
    }
}
