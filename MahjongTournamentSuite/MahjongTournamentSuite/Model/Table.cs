namespace MahjongTournamentSuite.Model
{
    class Table
    {
        public int RoundId { get; set; }

        public int TableId { get; set; }

        public int Player1Id { get; set; }

        public int Player2Id { get; set; }

        public int Player3Id { get; set; }

        public int Player4Id { get; set; }

        public string Player1Name { get; set; }

        public string Player2Name { get; set; }

        public string Player3Name { get; set; }

        public string Player4Name { get; set; }

        public string Player1Team { get; set; }

        public string Player2Team { get; set; }

        public string Player3Team { get; set; }

        public string Player4Team { get; set; }

        public string Player1Country { get; set; }

        public string Player2Country { get; set; }

        public string Player3Country { get; set; }

        public string Player4Country { get; set; }

        public Table() { }

        public Table(int roundId, int tableId,
            int player1Id, int player2Id, int player3Id, int player4Id,
            string player1Name, string player2Name, string player3Name, string player4Name,
            string player1Team, string player2Team, string player3Team, string player4Team,
            string player1Country, string player2Country, string player3Country, string player4Country)
        {
            RoundId = roundId;
            TableId = tableId;
            Player1Id = player1Id;
            Player2Id = player2Id;
            Player3Id = player3Id;
            Player4Id = player4Id;
            Player1Name = player1Name;
            Player2Name = player2Name;
            Player3Name = player3Name;
            Player4Name = player4Name;
            Player1Team = player1Team;
            Player2Team = player2Team;
            Player3Team = player3Team;
            Player4Team = player4Team;
            Player1Country = player1Country;
            Player2Country = player2Country;
            Player3Country = player3Country;
            Player4Country = player4Country;
        }
    }
}
