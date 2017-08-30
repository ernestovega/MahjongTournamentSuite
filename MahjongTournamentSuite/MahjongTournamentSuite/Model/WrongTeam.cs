namespace MahjongTournamentSuitePresentationLayer.Model
{
    public class WrongTeam
    {
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public int NumPlayers { get; set; }

        public WrongTeam() {}

        public WrongTeam(int teamId, string teamName, int numPlayers)
        {
            TeamId = teamId;
            TeamName = teamName;
            NumPlayers = numPlayers;
        }
        
    }
}
