namespace MahjongTournamentSuite.Model
{
    public class PlayerRanking
    {
        public int Order { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int PlayerPoints { get; set; }
        public int PlayerScore { get; set; }
        public int PlayerTeamId { get; set; }
        public string PlayerTeamName { get; set; }
        public int PlayerCountryId { get; set; }
        public string PlayerCountryName { get; set; }

        public PlayerRanking() { }

        public PlayerRanking(int playerId, string playerName, int playerTeamId, 
            string playerTeamName, int playerCountryId, string playerCountryName)
        {
            Order = 0;
            PlayerId = playerId;
            PlayerName = playerName;
            PlayerPoints = 0;
            PlayerScore = 0;
            PlayerTeamId = playerTeamId;
            PlayerTeamName = playerTeamName;
            PlayerCountryId = playerCountryId;
            PlayerCountryName = playerCountryName;
        }
    }
}
