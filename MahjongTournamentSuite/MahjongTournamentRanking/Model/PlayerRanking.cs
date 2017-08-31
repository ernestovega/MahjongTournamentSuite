namespace MahjongTournamentRanking.Model
{
    public class PlayerRanking
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int PlayerPoints { get; set; }
        public int PlayerScore { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public PlayerRanking() { }

        public PlayerRanking(int playerId, string playerName, int teamId, 
            string teamName, int countryId, string countryName)
        {
            PlayerId = playerId;
            PlayerName = playerName;
            PlayerPoints = 0;
            PlayerScore = 0;
            TeamId = teamId;
            TeamName = teamName;
            CountryId = countryId;
            CountryName = countryName;
        }
    }
}
