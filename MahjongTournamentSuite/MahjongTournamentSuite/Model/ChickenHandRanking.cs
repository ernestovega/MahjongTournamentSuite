namespace MahjongTournamentSuite.Model
{
    public class ChickenHandRanking
    {
        public int Order { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int NumChickenHands { get; set; }
        public int PlayerPoints { get; set; }
        public int PlayerScore { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public ChickenHandRanking() { }

        public ChickenHandRanking(int playerId, string playerName, 
            int playerPoints, int playerScore, int countryId, string countryName)
        {
            Order = 0;
            PlayerId = playerId;
            PlayerName = playerName;
            NumChickenHands = 0;
            PlayerPoints = playerPoints;
            PlayerScore = playerScore;
            CountryId = countryId;
            CountryName = countryName;
        }
    }
}
