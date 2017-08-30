namespace MahjongTournamentSuitePresentationLayer.Model
{
    public class Player
    {
        public int Id;
        public string Name;
        public string Team;

        public Player(string id, string name, string team)
        {
            Id = int.Parse(string.IsNullOrEmpty(id) ? "0" : id);
            Name = name;
            Team = team;
        }

        public Player()
        {

        }

        internal Player Clone()
        {
            return new Player(Id.ToString(), Name, Team);
        }
    }
}