namespace MahjongTournamentSuitePresentationLayer.Model
{
    class Rivals
    {
        public string playerName;
        public string[] rivalsNames;

        public Rivals (string playerName, string[] rivalsNames)
        {
            this.playerName = playerName;
            this.rivalsNames = rivalsNames;
        }

        public Rivals()
        {

        }
    }
}
