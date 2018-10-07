using MahjongTournamentSuite._Data.DataModel;
using System.Drawing;

namespace MahjongTournamentSuite.EmaReport
{
    public class DGVEmaPlayer : VEmaPlayer
    {
        public static readonly string COLUMN_EMA_PLAYER_TABLE_POINTS = "EmaPlayerTablePoints";
        public static readonly string COLUMN_EMA_PLAYER_SCORE = "EmaPlayerScore";
        public static readonly string COLUMN_EMA_PLAYER_COUNTRY_FLAG = "EmaPlayerCountryFlag";
        
        public string EmaPlayerTablePoints { get; set; }
        public string EmaPlayerScore { get; set; }
        public Image EmaPlayerCountryFlag { get; set; }

        public DGVEmaPlayer() {}

        public DGVEmaPlayer(VEmaPlayer vEmaPlayer, string emaPlayerTablePoints, string emaPlayerScore, Image emaPlayerCountryFlag) :
            base(vEmaPlayer.EmaPlayerEmaNumber, vEmaPlayer.EmaPlayerLastName, 
                vEmaPlayer.EmaPlayerName, vEmaPlayer.EmaPlayerCountryName)
        {
            EmaPlayerTablePoints = emaPlayerTablePoints;
            EmaPlayerScore = emaPlayerScore;
            EmaPlayerCountryFlag = emaPlayerCountryFlag;
        }
    }
}