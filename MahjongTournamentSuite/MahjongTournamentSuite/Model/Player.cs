﻿using System.ComponentModel.DataAnnotations;

namespace MahjongTournamentSuite.Model
{
    class Player
    {
        [Key]
        public int Id { get; set; }

        public int TournamentId { get; set; }

        public string Name { get; set; }

        public string Team { get; set; }

        public string Country { get; set; }

        public Player() { }

        public Player(int id, int tournamentId, string name, string team, string country)
        {
            Id = id;
            TournamentId = tournamentId;
            Name = name;
            Team = team;
            Country = country;
        }
    }
}
