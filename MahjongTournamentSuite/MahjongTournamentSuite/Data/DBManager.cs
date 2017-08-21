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
                return _db.Tournaments.Max(x => x.TournamentId);
        }

        public string GetTournamentName(int tournamentId)
        {
            return _db.Tournaments.ToList().FirstOrDefault(x => x.TournamentId == tournamentId).TournamentName;
        }

        public DBTournament GetTournament(int tournamentId)
        {
            return _db.Tournaments.ToList().FirstOrDefault(x => x.TournamentId == tournamentId);
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
            _db.Tournaments.ToList().FirstOrDefault(x => x.TournamentId == tournamentId).TournamentName = newName;
            _db.SaveChanges();
        }

        public void DeleteTournament(int tournamentId)
        {
            _db.Hands.RemoveRange(_db.Hands.ToList().FindAll(x => x.HandTournamentId == tournamentId));
            _db.Tables.RemoveRange(_db.Tables.ToList().FindAll(x => x.TableTournamentId == tournamentId));
            _db.Players.RemoveRange(_db.Players.ToList().FindAll(x => x.PlayerTournamentId == tournamentId));
            DBTournament tournament = _db.Tournaments.ToList().FirstOrDefault(x => x.TournamentId == tournamentId);
            if (tournament.IsTeams)
                _db.Teams.RemoveRange(_db.Teams.ToList().FindAll(x => x.TeamTournamentId == tournamentId));
            _db.Tournaments.Remove(tournament);
            _db.SaveChanges();
        }

        #endregion

        #region Player

        public List<DBPlayer> GetTournamentPlayers(int tournamentId)
        {
            return _db.Players.ToList().FindAll(x => x.PlayerTournamentId == tournamentId);
        }

        public List<DBPlayer> GetTablePlayers(int tournamentId, int roundId, int tableId)
        {
            DBTable table = _db.Tables.ToList().FirstOrDefault(x => x.TableTournamentId == tournamentId && x.TableRoundId == roundId && x.TableId == tableId);
            List<DBPlayer> tablePlayers = new List<DBPlayer>(4);
            tablePlayers.Add(_db.Players.ToList().FirstOrDefault(x => x.PlayerTournamentId == tournamentId && x.PlayerId == table.Player1Id));
            tablePlayers.Add(_db.Players.ToList().FirstOrDefault(x => x.PlayerTournamentId == tournamentId && x.PlayerId == table.Player2Id));
            tablePlayers.Add(_db.Players.ToList().FirstOrDefault(x => x.PlayerTournamentId == tournamentId && x.PlayerId == table.Player3Id));
            tablePlayers.Add(_db.Players.ToList().FirstOrDefault(x => x.PlayerTournamentId == tournamentId && x.PlayerId == table.Player4Id));
            return tablePlayers;
        }

        public void AddPlayers(List<DBPlayer> players)
        {
            _db.Players.AddRange(players);
            _db.SaveChanges();
        }

        public void UpdatePlayerName(int tournamentId, int playerId, string newName)
        {
            _db.Players.ToList().Find(x => x.PlayerTournamentId == tournamentId && x.PlayerId == playerId).PlayerName = newName;
            _db.SaveChanges();
        }
        
        #endregion

        #region Table

        public void AddTables(List<DBTable> tables, List<DBHand> hands)
        {
            _db.Tables.AddRange(tables);
            _db.Hands.AddRange(hands);
            _db.SaveChanges();
        }

        public DBTable GetTable(int tournamentId, int roundId, int tableId)
        {
            return _db.Tables.ToList().FirstOrDefault(x => x.TableTournamentId == tournamentId && x.TableRoundId == roundId && x.TableId == tableId);
        }

        public void UpdateTablePlayersPositions(DBTable table)
        {
            DBTable dbTable = _db.Tables.ToList().FirstOrDefault(x => x.TableTournamentId == table.TableTournamentId && x.TableId == table.TableId);
            dbTable.PlayerEastId = table.PlayerEastId;
            dbTable.PlayerSouthId = table.PlayerSouthId;
            dbTable.PlayerWestId = table.PlayerWestId;
            dbTable.PlayerNorthId = table.PlayerNorthId;
            _db.SaveChanges();
        }
        
        #endregion

        #region Hands

        public List<DBHand> GetTableHands(int tournamentId, int roundId, int tableId)
        {
            return _db.Hands.ToList().FindAll(x => x.HandTournamentId == tournamentId && x.HandRoundId == roundId && x.HandTableId == tableId);
        }
        
        public void UpdateHand(DBHand hand)
        {
            DBHand dbHand = _db.Hands.ToList().FirstOrDefault(x => x.HandTournamentId == hand.HandTournamentId
            && x.HandTableId == hand.HandTableId && x.HandRoundId == hand.HandRoundId && x.HandId == hand.HandId);
            dbHand.PlayerWinnerId = hand.PlayerWinnerId;
            dbHand.PlayerLooserId = hand.PlayerLooserId;
            dbHand.HandScore = hand.HandScore;
            dbHand.IsChickenHand = hand.IsChickenHand;
            _db.SaveChanges();
        }
        
        #endregion

        #region Teams

        public List<DBTeam> GetTournamentTeams(int tournamentId)
        {
            return _db.Teams.ToList().FindAll(x => x.TeamTournamentId == tournamentId);
        }

        public List<string> GetTeamsNamesSortedList(int tournamentId)
        {
            List<string> teamNames = _db.Teams.ToList().FindAll(x => x.TeamTournamentId == tournamentId)
                .Select(x => x.TeamName).ToList();
            teamNames.Sort();
            return teamNames;
        }

        public void AddTeams(List<DBTeam> teams)
        {
            _db.Teams.AddRange(teams);
            _db.SaveChanges();
        }

        public void UpdateTeamName(int tournamentId, int teamId, string newName)
        {
            _db.Teams.ToList().Find(x => x.TeamTournamentId == tournamentId && x.TeamId == teamId).TeamName = newName;
            _db.SaveChanges();
        }
        
        #endregion

        #region Countries

        public List<DBCountry> GetCountries()
        {
            if (_db.Countries.Count() == 0)
            {
                _db.Countries.Add(new DBCountry(1, "Spain"));
                _db.Countries.Add(new DBCountry(2, "France"));
                _db.Countries.Add(new DBCountry(3, "Italy"));
                _db.Countries.Add(new DBCountry(4, "Denmark"));
                _db.Countries.Add(new DBCountry(5, "Switzerland"));
                _db.Countries.Add(new DBCountry(6, "Germany"));
                _db.Countries.Add(new DBCountry(7, "Russia"));
                _db.Countries.Add(new DBCountry(8, "England"));
                _db.Countries.Add(new DBCountry(9, "Poland"));
                _db.Countries.Add(new DBCountry(10, "Netherlands"));
                _db.SaveChanges();
            }
            return _db.Countries.ToList();
        }

        public List<string> GetCountriesNamesSortedList()
        {
            List<string> countriesNames = _db.Countries.ToList().Select(x => x.CountryName).ToList();
            countriesNames.Sort();
            return countriesNames;
        }

        #endregion
    }
}
