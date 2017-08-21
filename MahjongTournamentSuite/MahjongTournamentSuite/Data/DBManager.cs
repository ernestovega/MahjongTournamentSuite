using MahjongTournamentSuite.Model;
using System.Collections.Generic;
using System.Linq;
using static MahjongTournamentSuite.Data.DBContext;
using System;

namespace MahjongTournamentSuite.Data
{
    class DBManager : IDBManager
    {
        #region Fields

        TournamentSuiteDB _db = new TournamentSuiteDB();

        #endregion

        #region Constructor

        public DBManager()
        {
            
        }

        #endregion

        #region Tournaments

        public int GetExistingMaxTournamentId()
        {
            if (_db.Tournaments.Count() == 0)
                return 0;
            else
                return _db.Tournaments.Max(x => x.Id);
        }

        public string GetTournamentName(int tournamentId)
        {
            return _db.Tournaments.ToList().FirstOrDefault(x => x.Id == tournamentId).Name;
        }

        public DBTournament GetTournament(int tournamentId)
        {
            return _db.Tournaments.ToList().FirstOrDefault(x => x.Id == tournamentId);
        }

        public List<DBTournament> GetTournaments()
        {
            return _db.Tournaments.OrderByDescending(x => x.CreationDate).ToList();
        }

        public void AddTournament(DBTournament tournament)
        {
            _db.Tournaments.Add(tournament);
            _db.SaveChanges();
        }

        public void UpdateTournamentName(int tournamentId, string newName)
        {
            _db.Tournaments.ToList().FirstOrDefault(x => x.Id == tournamentId).Name = newName;
            _db.SaveChanges();
        }

        public void DeleteTournament(int tournamentId)
        {
            DeleteTournamentHands(tournamentId);
            DeleteTournamentTables(tournamentId);
            DeleteTournamentPlayers(tournamentId);
            _db.Tournaments.Remove(_db.Tournaments.ToList().FirstOrDefault(x => x.Id == tournamentId));
            if(_db.Tournaments.ToList().Find(x => x.Id == tournamentId).IsTeams)
                DeleteTournamentTeams(tournamentId);
            _db.SaveChanges();
        }

        #endregion

        #region Player

        public List<DBPlayer> GetTournamentPlayers(int tournamentId)
        {
            return _db.Players.ToList().FindAll(x => x.TournamentId == tournamentId);
        }

        public List<DBPlayer> GetTablePlayers(int tournamentId, int roundId, int tableId)
        {
            DBTable table = _db.Tables.ToList().FirstOrDefault(x => x.TournamentId == tournamentId && x.RoundId == roundId && x.Id == tableId);
            List<DBPlayer> tablePlayers = new List<DBPlayer>(4);
            tablePlayers.Add(_db.Players.ToList().FirstOrDefault(x => x.TournamentId == tournamentId && x.Id == table.Player1Id));
            tablePlayers.Add(_db.Players.ToList().FirstOrDefault(x => x.TournamentId == tournamentId && x.Id == table.Player2Id));
            tablePlayers.Add(_db.Players.ToList().FirstOrDefault(x => x.TournamentId == tournamentId && x.Id == table.Player3Id));
            tablePlayers.Add(_db.Players.ToList().FirstOrDefault(x => x.TournamentId == tournamentId && x.Id == table.Player4Id));
            return tablePlayers;
        }

        public void AddPlayers(List<DBPlayer> players)
        {
            _db.Players.AddRange(players);
            _db.SaveChanges();
        }

        public void UpdatePlayerName(int tournamentId, int playerId, string newName)
        {
            _db.Players.ToList().Find(x => x.TournamentId == tournamentId && x.Id == playerId).Name = newName;
            _db.SaveChanges();
        }

        public void DeleteTournamentPlayers(int tournamentId)
        {
            _db.Players.RemoveRange(_db.Players.ToList().FindAll(x => x.TournamentId == tournamentId));
            _db.SaveChanges();
        }

        #endregion

        #region Table

        public void AddTables(List<DBTable> tables)
        {
            _db.Tables.AddRange(tables);
            _db.SaveChanges();
        }

        public DBTable GetTable(int tournamentId, int roundId, int tableId)
        {
            return _db.Tables.ToList().FirstOrDefault(x => x.TournamentId == tournamentId && x.RoundId == roundId && x.Id == tableId);
        }

        public void UpdateTablePlayersPositions(DBTable table)
        {
            DBTable dbTable = _db.Tables.ToList().FirstOrDefault(x => x.TournamentId == table.TournamentId && x.Id == table.Id);
            dbTable.PlayerEastId = table.PlayerEastId;
            dbTable.PlayerSouthId = table.PlayerSouthId;
            dbTable.PlayerWestId = table.PlayerWestId;
            dbTable.PlayerNorthId = table.PlayerNorthId;
            _db.SaveChanges();
        }

        public void DeleteTournamentTables(int tournamentId)
        {
            _db.Tables.RemoveRange(_db.Tables.ToList().FindAll(x => x.TournamentId == tournamentId));
            _db.SaveChanges();
        }

        #endregion

        #region Hands

        public List<DBHand> GetTableHands(int tournamentId, int roundId, int tableId)
        {
            return _db.Hands.ToList().FindAll(x => x.TournamentId == tournamentId && x.RoundId == roundId && x.TableId == tableId);
        }

        public void AddHands(List<DBHand> hands)
        {
            _db.Hands.AddRange(hands);
            _db.SaveChanges();
        }

        public void UpdateHand(DBHand hand)
        {
            DBHand dbHand = _db.Hands.ToList().FirstOrDefault(x => x.TournamentId == hand.TournamentId
            && x.TableId == hand.TableId && x.RoundId == hand.RoundId && x.Id == hand.Id);
            dbHand.PlayerWinnerId = hand.PlayerWinnerId;
            dbHand.PlayerLooserId = hand.PlayerLooserId;
            dbHand.Score = hand.Score;
            dbHand.IsChickenHand = hand.IsChickenHand;
            _db.SaveChanges();
        }

        public void DeleteTournamentHands(int tournamentId)
        {
            _db.Hands.RemoveRange(_db.Hands.ToList().FindAll(x => x.TournamentId == tournamentId));
            _db.SaveChanges();
        }

        #endregion

        #region Teams

        public List<DBTeam> GetTournamentTeams(int tournamentId)
        {
            return _db.Teams.ToList().FindAll(x => x.TournamentId == tournamentId);
        }

        public void AddTeams(List<DBTeam> teams)
        {
            _db.Teams.AddRange(teams);
            _db.SaveChanges();
        }

        public void UpdateTeamName(int tournamentId, int teamId, string newName)
        {
            _db.Teams.ToList().Find(x => x.TournamentId == tournamentId && x.Id == teamId).Name = newName;
            _db.SaveChanges();
        }

        public void DeleteTournamentTeams(int tournamentId)
        {
            _db.Teams.RemoveRange(_db.Teams.ToList().FindAll(x => x.TournamentId == tournamentId));
            _db.SaveChanges();
        }

        #endregion
    }
}
