using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MahjongTournamentSuite.Model
{
    class DBTable {

        [Key, Column(Order = 0)]
        public int TournamentId { get; set; }

        [Key, Column(Order = 1)]
        public int RoundId { get; set; }

        [Key, Column(Order = 2)]
        public int Id { get; set; }

        public int PlayerEastId { get; set; }

        public int PlayerSouthId { get; set; }

        public int PlayerWestId { get; set; }

        public int PlayerNorthId { get; set; }

        public DBTable() {}

        public DBTable(int tournamentId, int roundId, int id,
            int playerEastId, int playerSouthId, int playerWestId, int playerNorthId)
        {
            TournamentId = tournamentId;
            RoundId = roundId;
            Id = id;
            PlayerEastId = playerEastId;
            PlayerSouthId = playerSouthId;
            PlayerWestId = playerWestId;
            PlayerNorthId = playerNorthId;
        }
    }
}
