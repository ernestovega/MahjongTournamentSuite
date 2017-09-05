namespace MahjongTournamentSuite.Model
{
    public class ChickenHandRanking
    {
        public int Order { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int PlayerNumChickenHands { get; set; }
        public int PlayerPoints { get; set; }
        public int PlayerScore { get; set; }
        public int PlayerCountryId { get; set; }
        public string PlayerCountryName { get; set; }

        public ChickenHandRanking() { }

        public ChickenHandRanking(int playerId, string playerName, 
            int playerPoints, int playerScore, int playerCountryId, string playerCountryName)
        {
            Order = 0;
            PlayerId = playerId;
            PlayerName = playerName;
            PlayerNumChickenHands = 0;
            PlayerPoints = playerPoints;
            PlayerScore = playerScore;
            PlayerCountryId = playerCountryId;
            PlayerCountryName = playerCountryName;
        }
    }
}
