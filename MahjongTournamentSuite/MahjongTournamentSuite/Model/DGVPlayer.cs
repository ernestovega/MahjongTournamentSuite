using MahjongTournamentSuiteDataLayer.Model;

namespace MahjongTournamentSuite.Model
{
    public class DGVPlayer : DBPlayer
    {
        public string PlayerTeamName { get; set; }

        public DGVPlayer() {}

        public DGVPlayer(DBPlayer dbPlayer, string teamName) : 
            base(dbPlayer.PlayerTournamentId, dbPlayer.PlayerId, dbPlayer.PlayerName, dbPlayer.PlayerTeamId, dbPlayer.PlayerCountryName)
        {
            PlayerTeamName = teamName;
        }
    }
}
