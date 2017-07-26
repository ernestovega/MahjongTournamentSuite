namespace MahjongTournamentSuite.Model
{
    class Player
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Team { get; set; }

        public string Country { get; set; }

        public Player() { }

        public Player(int id, string name, string team, string country)
        {
            Id = id;
            Name = name;
            Team = team;
            Country = country;
        }
    }
}
