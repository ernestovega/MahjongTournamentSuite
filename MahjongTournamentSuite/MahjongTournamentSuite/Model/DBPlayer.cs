﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MahjongTournamentSuite.Model
{
    public class DBPlayer
    {
        [Key, Column(Order = 0)]
        public int TournamentId { get; set; }

        [Key, Column(Order = 1)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string TeamId { get; set; }

        public string CountryId { get; set; }

        public DBPlayer() { }

        public DBPlayer(int tournamentId, int id, string name, string teamId, string countryId)
        {
            TournamentId = tournamentId;
            Id = id;
            Name = name;
            TeamId = teamId;
            CountryId = countryId;
        }
    }
}
