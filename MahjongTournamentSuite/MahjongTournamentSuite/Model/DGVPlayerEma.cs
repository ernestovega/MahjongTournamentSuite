namespace MahjongTournamentSuite.EmaReport
{
    public class DGVPlayerEma
    {
        public static readonly string COLUMN_PLAYER_NAME = "PlayerName";
        //public static readonly string COLUMN_PLAYER_LAST_NAME = "PlayerLastName";
        //public static readonly string COLUMN_PLAYER_EMA_NUMBER = "PlayerEmaNumber";
        //public static readonly string COLUMN_PLAYER_COUNTRY_NAME = "PlayerCountryName";
        public static readonly string COLUMN_PLAYER_TABLE_POINTS = "PlayerTablePoints";
        public static readonly string COLUMN_PLAYER_SCORE = "PlayerScore";

        public string PlayerFirstName { get; set; }
        //public string PlayerLastName { get; set; }
        //public int PlayerEmaNumber { get; set; }
        //public string PlayerCountryName { get; set; }
        public string PlayerTablePoints { get; set; }
        public string PlayerScore { get; set; }

        public DGVPlayerEma(string playerFirstName/*, string playerLastName, int playerEmaNumber, 
            string playerCountryName*/, string playerTablePoints, string playerScore)
        {
            PlayerFirstName = playerFirstName;
            //PlayerLastName = playerLastName;
            //PlayerEmaNumber = playerEmaNumber;
            //PlayerCountryName = playerCountryName;
            PlayerTablePoints = playerTablePoints;
            PlayerScore = playerScore;
        }
    }
}