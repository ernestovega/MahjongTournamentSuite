using MahjongTournamentSuiteDataLayer.Model;

namespace MahjongTournamentSuitePresentationLayer.Model
{
    public class DGVPlayer : DBPlayer
    {
        public string PlayerTeamName { get; set; }
        
        public string PlayerCountryName { get; set; }

        public DGVPlayer() {}

        public DGVPlayer(DBPlayer dbPlayer, string teamName, string countryName) : 
            base(dbPlayer.PlayerTournamentId, dbPlayer.PlayerId, dbPlayer.PlayerName, dbPlayer.PlayerTeamId, dbPlayer.PlayerCountryId)
        {
            PlayerTeamName = teamName;
            PlayerCountryName = countryName;
        }
    }
}
