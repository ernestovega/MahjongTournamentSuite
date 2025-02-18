﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MahjongTournamentSuite._Data.DataModel
{
    public class DBHand
    {
        [Key, Column(Order = 0)]
        public int HandTournamentId { get; set; }
        [Key, Column(Order = 1)]
        public int HandRoundId { get; set; }
        [Key, Column(Order = 2)]
        public int HandTableId { get; set; }
        [Key, Column(Order = 3)]
        public int HandId { get; set; }        
        public string PlayerWinnerId { get; set; }
        public string PlayerLooserId { get; set; }
        public string HandScore { get; set; }
        public bool IsChickenHand { get; set; }
        public string PlayerEastPenalty { get; set; }
        public string PlayerSouthPenalty { get; set; }
        public string PlayerWestPenalty { get; set; }
        public string PlayerNorthPenalty { get; set; }



        public DBHand() {}

        public DBHand(int tournamentId, int roundId, int tableId, int id,
            string playerWinnerId, string playerLooserId, string handScore, bool isChickenHand, 
            string playerEastPenalty, string playerSouthPenalty, 
            string playerWestPenalty, string playerNorthPenalty)
        {
            HandTournamentId = tournamentId;
            HandRoundId = roundId;
            HandTableId = tableId;
            HandId = id;
            PlayerWinnerId = playerWinnerId;
            PlayerLooserId = playerLooserId;
            HandScore = handScore;
            IsChickenHand = isChickenHand;
            PlayerEastPenalty = playerEastPenalty;
            PlayerSouthPenalty = playerSouthPenalty;
            PlayerWestPenalty = playerWestPenalty;
            PlayerNorthPenalty = playerNorthPenalty;
        }
    }
}
