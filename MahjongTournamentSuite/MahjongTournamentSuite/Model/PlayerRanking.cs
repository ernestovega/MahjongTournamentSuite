namespace MahjongTournamentSuite.Model
{
    public class PlayerRanking
    {
        public int Order { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int PlayerPoints { get; set; }
        public int PlayerScore { get; set; }
        public int TeamId { get; set; }
        public string PlayerTeamName { get; set; }
        public int CountryId { get; set; }
        public string PlayerCountryName { get; set; }

        public PlayerRanking() { }

        public PlayerRanking(int playerId, string playerName, int teamId, 
            string teamName, int countryId, string countryName)
        {
            Order = 0;
            PlayerId = playerId;
            PlayerName = playerName;
            PlayerPoints = 0;
            PlayerScore = 0;
            TeamId = teamId;
            PlayerTeamName = teamName;
            CountryId = countryId;
            PlayerCountryName = countryName;
        }
    }
}
