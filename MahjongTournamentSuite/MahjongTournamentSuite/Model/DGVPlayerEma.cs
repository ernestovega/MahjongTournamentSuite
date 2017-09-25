namespace MahjongTournamentSuite.EmaReport
{
    internal class DGVPlayerEma
    {
        public static readonly string COLUMN_PLAYER_NAME = "PlayerName";
        public static readonly string COLUMN_PLAYER_LAST_NAME = "PlayerLastName";
        public static readonly string COLUMN_PLAYER_EMA_NUMBER = "PlayerEmaNumber";
        public static readonly string COLUMN_PLAYER_COUNTRY_NAME = "PlayerCountryName";

        public string PlayerName { get; set; }
        public string PlayerLastName { get; set; }
        public int PlayerEmaNumber { get; set; }
        public string PlayerCountryName { get; set; }

        public DGVPlayerEma(string playerName, string playerLastName, int playerEmaNumber, string playerCountryName)
        {
            PlayerName = playerName;
            PlayerLastName = playerLastName;
            PlayerEmaNumber = playerEmaNumber;
            PlayerCountryName = playerCountryName;
        }
    }
}