using System.Collections.Generic;
using System.Linq;
using System;
using static MahjongTournamentSuite._Data.DBContext;
using MahjongTournamentSuite._Data.DataModel;
using MahjongTournamentSuite._Data.Mappers;
using MahjongPlayerSuite._Data.Mappers;
using MahjongTableSuite._Data.Mappers;
using MahjongTeamSuite._Data.Mappers;
using MahjongTournamentSuite.TeamsManager;

namespace MahjongTournamentSuite._Data
{
    public class DBManager : IDBManager
    {
        TournamentSuiteDB _db = new TournamentSuiteDB();

        public DBManager() {}

        #region Tournaments

        public List<VTournament> GetTournaments()
        {
            List<DBTournament> dbTournaments = _db.Tournaments.OrderByDescending(
                x => x.CreationDate).ToList();
            return TournamentMapper.GetViewModel(dbTournaments);
        }

        public VTournament GetTournament(int tournamentId)
        {
            DBTournament dbTournament = _db.Tournaments.ToList().Find(
                x => x.TournamentId == tournamentId);
            return TournamentMapper.GetViewModel(dbTournament);
        }

        public string GetTournamentName(int tournamentId)
        {
            return _db.Tournaments.ToList().Find(
                x => x.TournamentId == tournamentId).TournamentName;
        }

        public int GetIdForNewTournament()
        {
            if (_db.Tournaments.ToList().Count == 0)
                return 1;
            else
            {
                int maxId = _db.Tournaments.ToList().Max(x => x.TournamentId);
                return maxId + 1;
            }
        }

        public bool ExistTournamentByName(string tournamentName)
        {
            return _db.Tournaments.ToList().Find(
                x => x.TournamentName.Equals(tournamentName, 
                StringComparison.Ordinal)) != null;
        }

        public void AddTournament(VTournament vTournament)
        {
            DBTournament dbTournament = TournamentMapper.GetDataModel(vTournament);
            _db.Tournaments.Add(dbTournament);
            _db.SaveChanges();
        }

        public void UpdateTournamentName(int tournamentId, string newName)
        {
            _db.Tournaments.ToList().Find(
                x => x.TournamentId == tournamentId).TournamentName = newName;
            _db.SaveChanges();
        }

        public void DeleteTournament(int tournamentId)
        {
            DeleteHands(_db.Hands.ToList().FindAll(x => x.HandTournamentId == tournamentId));
            DeleteTables(_db.Tables.ToList().FindAll(x => x.TableTournamentId == tournamentId));
            DeletePlayers(_db.Players.ToList().FindAll(x => x.PlayerTournamentId == tournamentId));
            DBTournament dbTournament = _db.Tournaments.ToList().Find( x => x.TournamentId == tournamentId);
            if (dbTournament.IsTeams)
                DeleteTeams(_db.Teams.ToList().FindAll( x => x.TeamTournamentId == tournamentId));
            _db.Tournaments.Remove(dbTournament);
            _db.SaveChanges();
        }

        #region Private

        private void DeleteHands(List<DBHand> hands)
        {
            foreach (DBHand hand in hands)
                _db.Hands.Remove(hand);
        }

        private void DeleteTables(List<DBTable> tables)
        {
            foreach (DBTable table in tables)
                _db.Tables.Remove(table);
        }

        private void DeletePlayers(List<DBPlayer> players)
        {
            foreach (DBPlayer player in players)
                _db.Players.Remove(player);
        }

        private void DeleteTeams(List<DBTeam> teams)
        {
            foreach (DBTeam team in teams)
                _db.Teams.Remove(team);
        }

        #endregion

        #endregion

        #region Player

        public List<VPlayer> GetTournamentPlayers(int tournamentId)
        {
            List<DBPlayer> dbPlayers = _db.Players.ToList().FindAll(x => x.PlayerTournamentId == tournamentId);
            return PlayerMapper.GetViewModel(dbPlayers);
        }

        public List<VPlayer> GetTablePlayers(int tournamentId, int roundId, int tableId)
        {
            DBTable dbTable = _db.Tables.ToList().Find(x => x.TableTournamentId == tournamentId && 
                x.TableRoundId == roundId && x.TableId == tableId);
            List<DBPlayer> dbTablePlayers = new List<DBPlayer>(4);
            dbTablePlayers.Add(_db.Players.ToList().Find(
                x => x.PlayerTournamentId == tournamentId && 
                x.PlayerId == dbTable.Player1Id));
            dbTablePlayers.Add(_db.Players.ToList().Find(
                x => x.PlayerTournamentId == tournamentId && 
                x.PlayerId == dbTable.Player2Id));
            dbTablePlayers.Add(_db.Players.ToList().Find(
                x => x.PlayerTournamentId == tournamentId && 
                x.PlayerId == dbTable.Player3Id));
            dbTablePlayers.Add(_db.Players.ToList().Find(
                x => x.PlayerTournamentId == tournamentId && 
                x.PlayerId == dbTable.Player4Id));
            return PlayerMapper.GetViewModel(dbTablePlayers);
        }

        public void AddPlayers(List<VPlayer> vPlayers)
        {
            _db.Players.AddRange(PlayerMapper.GetDataModel(vPlayers));
            _db.SaveChanges();
        }

        public void UpdatePlayerName(int tournamentId, int playerId, string newName)
        {
            _db.Players.ToList().Find(x => x.PlayerTournamentId == tournamentId && x.PlayerId == playerId).PlayerName = newName;
            _db.SaveChanges();
        }

        public void UpdatePlayerTeam(int tournamentId, int playerId, int newTeamId)
        {
            _db.Players.ToList()
                .Find(x => x.PlayerTournamentId == tournamentId && x.PlayerId == playerId)
                .PlayerTeamId = newTeamId;
            _db.SaveChanges();
        }

        public void UpdatePlayerCountry(int tournamentId, int playerId, string newCountryName)
        {
            _db.Players.ToList()
                .Find(x => x.PlayerTournamentId == tournamentId && x.PlayerId == playerId)
                .PlayerCountryName = newCountryName;
            _db.SaveChanges();
        }

        public void AssignNewEmaPlayer(int tournamentId, int playerId, string emaNumber)
        {
            DBPlayer dbPlayer = _db.Players.ToList().Find(x => x.PlayerTournamentId == tournamentId && x.PlayerId == playerId);
            DBEmaPlayer dbEmaPlayer = _db.EmaPlayers.ToList().Find(x => x.EmaPlayerEmaNumber == emaNumber);
            if (dbEmaPlayer != null)
            {
                dbPlayer.PlayerEmaNumber = dbEmaPlayer.EmaPlayerEmaNumber;
                dbPlayer.PlayerName = dbEmaPlayer.GetFullName();
                dbPlayer.PlayerCountryName = dbEmaPlayer.EmaPlayerCountryName;
            }
            _db.SaveChanges();
        }

        public void UnassignEmaPlayer(int tournamentId, int playerId)
        {
            DBPlayer player = _db.Players.ToList().Find(x => x.PlayerTournamentId == tournamentId && x.PlayerId == playerId);
            player.PlayerEmaNumber = string.Empty;
            player.PlayerName = string.Format("Player {0}", player.PlayerId);
            player.PlayerCountryName = string.Empty;
            _db.SaveChanges();
        }

        public List<string> GetAvailableTeamPlayersNames(int tournamentId, int teamId)
        {
            List<DBPlayer> availableTeamPlayers = _db.Players.ToList().FindAll(
                x => x.PlayerTournamentId == tournamentId && x.PlayerTeamId != teamId);
            List<string> availableTeamPlayersNames = new List<string>(availableTeamPlayers.Count);
            foreach (DBPlayer dbPlayer in availableTeamPlayers)
            {
                availableTeamPlayersNames.Add(string.Format("{0} - {1}", dbPlayer.PlayerName, dbPlayer.PlayerId));
            }
            return (availableTeamPlayersNames);
        }

        public void AssignTeamPlayer(int tournamentId, int teamPlayerId, int teamId)
        {
            DBPlayer dbPlayer = _db.Players.ToList().Find(x => x.PlayerTournamentId == tournamentId && x.PlayerId == teamPlayerId);
            dbPlayer.PlayerTeamId = teamId;
            _db.SaveChanges();
        }

        public void UnassignTeamPlayer(int tournamentId, int teamPlayerId)
        {
            DBPlayer dbPlayer = _db.Players.ToList().Find(x => x.PlayerTournamentId == tournamentId && x.PlayerId == teamPlayerId);
            dbPlayer.PlayerTeamId = 0;
            _db.SaveChanges();
        }

        public void RefreshPlayers(int tournamentId)
        {
            List<DBPlayer> players = _db.Players.ToList().FindAll(x => x.PlayerTournamentId == tournamentId);
            foreach (DBPlayer player in players)
                _db.Entry(player).Reload();
        }

        #endregion

        #region Table

        public List<VTable> GetTournamentTables(int tournamentId)
        {
            List<DBTable> dbTables = _db.Tables.ToList().FindAll(x => x.TableTournamentId == tournamentId);
            return TableMapper.GetViewModel(dbTables);
        }

        public void AddTables(List<VTable> vTables, List<VHand> vHands)
        {
            _db.Tables.AddRange(TableMapper.GetDataModel(vTables));
            _db.Hands.AddRange(HandMapper.GetDataModel(vHands));
            _db.SaveChanges();
        }

        public VTable GetTable(int tournamentId, int roundId, int tableId)
        {
            DBTable dbTable = _db.Tables.ToList().Find(x => x.TableTournamentId == tournamentId && 
                x.TableRoundId == roundId && x.TableId == tableId);
            return TableMapper.GetViewModel(dbTable);
        }

        public void UpdateTableAllPlayersPositions(VTable vTable)
        {
            DBTable dbTable = _db.Tables.ToList().Find(x => x.TableTournamentId == vTable.TableTournamentId &&
                    x.TableRoundId == vTable.TableRoundId && x.TableId == vTable.TableId);
            dbTable.PlayerEastId = vTable.PlayerEastId;
            dbTable.PlayerSouthId = vTable.PlayerSouthId;
            dbTable.PlayerWestId = vTable.PlayerWestId;
            dbTable.PlayerNorthId = vTable.PlayerNorthId;
            _db.SaveChanges();
        }

        public void UpdateTableAllPlayersTotalScores(VTable vTable)
        {
            DBTable dbTable = _db.Tables.ToList()
                .Find(x => x.TableTournamentId == vTable.TableTournamentId &&
                    x.TableRoundId == vTable.TableRoundId && x.TableId == vTable.TableId);
            dbTable.PlayerEastScore = vTable.PlayerEastScore;
            dbTable.PlayerSouthScore = vTable.PlayerSouthScore;
            dbTable.PlayerWestScore = vTable.PlayerWestScore;
            dbTable.PlayerNorthScore = vTable.PlayerNorthScore;
            _db.SaveChanges();
        }

        public void UpdateTableAllPlayersPoints(VTable vTable)
        {
            DBTable dbTable = _db.Tables.ToList()
                .Find(x => x.TableTournamentId == vTable.TableTournamentId &&
                    x.TableRoundId == vTable.TableRoundId && x.TableId == vTable.TableId);
            dbTable.PlayerEastPoints = vTable.PlayerEastPoints;
            dbTable.PlayerSouthPoints = vTable.PlayerSouthPoints;
            dbTable.PlayerWestPoints = vTable.PlayerWestPoints;
            dbTable.PlayerNorthPoints = vTable.PlayerNorthPoints;
            _db.SaveChanges();
        }

        public void UpdateTableIsCompleted(VTable vTable)
        {
            DBTable dbTable = _db.Tables.ToList()
                .Find(x => x.TableTournamentId == vTable.TableTournamentId && 
                    x.TableRoundId == vTable.TableRoundId && x.TableId == vTable.TableId);
            dbTable.IsCompleted = vTable.IsCompleted;
            _db.SaveChanges();
        }

        public void UpdateTableUseTotalsOnly(VTable vTable)
        {
            DBTable dbTable = _db.Tables.ToList()
                .Find(x => x.TableTournamentId == vTable.TableTournamentId &&
                    x.TableRoundId == vTable.TableRoundId && x.TableId == vTable.TableId);
            dbTable.UseTotalsOnly = vTable.UseTotalsOnly;
            _db.SaveChanges();
        }

        public void RefreshTable(int tournamentId, int roundId, int tableId)
        {
            DBTable table = _db.Tables.ToList().Find(x => x.TableTournamentId == tournamentId
            && x.TableRoundId == roundId && x.TableId == tableId);
            _db.Entry(table).Reload();
            List<DBHand> hands = _db.Hands.ToList().FindAll(x => x.HandTournamentId == tournamentId 
            && x.HandRoundId == roundId && x.HandTableId == tableId);
            foreach (DBHand hand in hands)
                _db.Entry(hand).Reload();
        }

        #endregion

        #region Hands

        public List<VHand> GetTournamentHands(int tournamentId)
        {
            List<DBHand> dbHands = _db.Hands.ToList().FindAll(x => x.HandTournamentId == tournamentId);
            return HandMapper.GetViewModel(dbHands);
        }

        public List<VHand> GetTableHands(int tournamentId, int roundId, int tableId)
        {
            List<DBHand> dbHands = _db.Hands.ToList().FindAll(x => x.HandTournamentId == tournamentId 
            && x.HandRoundId == roundId && x.HandTableId == tableId);
            return HandMapper.GetViewModel(dbHands);
        }

        public void UpdateHandWinnerId(VHand vHand)
        {
            DBHand dbHand = _db.Hands.ToList().Find(x => x.HandTournamentId == vHand.HandTournamentId
            && x.HandTableId == vHand.HandTableId && x.HandRoundId == vHand.HandRoundId && x.HandId == vHand.HandId);
            dbHand.PlayerWinnerId = vHand.PlayerWinnerId;
            _db.SaveChanges();
        }

        public void UpdateHandLooserId(VHand vHand)
        {
            DBHand dbHand = _db.Hands.ToList().Find(x => x.HandTournamentId == vHand.HandTournamentId
            && x.HandTableId == vHand.HandTableId && x.HandRoundId == vHand.HandRoundId && x.HandId == vHand.HandId);
            dbHand.PlayerLooserId = vHand.PlayerLooserId;
            _db.SaveChanges();
        }

        public void UpdateHandScore(VHand vHand)
        {
            DBHand dbHand = _db.Hands.ToList().Find(x => x.HandTournamentId == vHand.HandTournamentId
            && x.HandTableId == vHand.HandTableId && x.HandRoundId == vHand.HandRoundId && x.HandId == vHand.HandId);
            dbHand.HandScore = vHand.HandScore;
            _db.SaveChanges();
        }

        public void UpdateHandIsChickenHand(VHand vHand)
        {
            DBHand dbHand = _db.Hands.ToList().Find(x => x.HandTournamentId == vHand.HandTournamentId
            && x.HandTableId == vHand.HandTableId  && x.HandRoundId == vHand.HandRoundId  && x.HandId == vHand.HandId);
            dbHand.IsChickenHand = vHand.IsChickenHand;
            _db.SaveChanges();
        }

        public void UpdateHandPlayerEastPenalty(VHand vHand)
        {
            DBHand dbHand = _db.Hands.ToList().Find(x => x.HandTournamentId == vHand.HandTournamentId
            && x.HandTableId == vHand.HandTableId && x.HandRoundId == vHand.HandRoundId && x.HandId == vHand.HandId);
            dbHand.PlayerEastPenalty = vHand.PlayerEastPenalty;
            _db.SaveChanges();
        }

        public void UpdateHandPlayerSouthPenalty(VHand vHand)
        {
            DBHand dbHand = _db.Hands.ToList().Find(x => x.HandTournamentId == vHand.HandTournamentId
            && x.HandTableId == vHand.HandTableId && x.HandRoundId == vHand.HandRoundId && x.HandId == vHand.HandId);
            dbHand.PlayerSouthPenalty = vHand.PlayerSouthPenalty;
            _db.SaveChanges();
        }

        public void UpdateHandPlayerWestPenalty(VHand vHand)
        {
            DBHand dbHand = _db.Hands.ToList().Find(x => x.HandTournamentId == vHand.HandTournamentId
            && x.HandTableId == vHand.HandTableId && x.HandRoundId == vHand.HandRoundId && x.HandId == vHand.HandId);
            dbHand.PlayerWestPenalty = vHand.PlayerWestPenalty;
            _db.SaveChanges();
        }

        public void UpdateHandPlayerNorthPenalty(VHand vHand)
        {
            DBHand dbHand = _db.Hands.ToList().Find(x => x.HandTournamentId == vHand.HandTournamentId
            && x.HandTableId == vHand.HandTableId && x.HandRoundId == vHand.HandRoundId && x.HandId == vHand.HandId);
            dbHand.PlayerNorthPenalty = vHand.PlayerNorthPenalty;
            _db.SaveChanges();
        }

        #endregion

        #region Teams

        public List<VTeam> GetTournamentTeams(int tournamentId)
        {
            List<DBTeam> dbTeams = _db.Teams.ToList().FindAll(x => x.TeamTournamentId == tournamentId);
            return TeamMapper.GetViewModel(dbTeams);
        }

        public List<string> GetTeamsNames(int tournamentId)
        {
            return _db.Teams.ToList().FindAll(x => x.TeamTournamentId == tournamentId)
                .Select(x => x.TeamName).ToList();
        }

        public void AddTeams(List<VTeam> teams)
        {
            _db.Teams.AddRange(TeamMapper.GetDataModel(teams));
            _db.SaveChanges();
        }

        public void UpdateTeamName(int tournamentId, int teamId, string newName)
        {
            _db.Teams.ToList().Find(x => x.TeamTournamentId == tournamentId && x.TeamId == teamId).TeamName = newName;
            _db.SaveChanges();
        }

        public List<VPlayer> GetTeamPlayers(int tournamentId, int teamId)
        {
            List<DBPlayer> dbTeamPlayers = _db.Players.ToList().FindAll(
                x => x.PlayerTournamentId == tournamentId && x.PlayerTeamId == teamId);
            return PlayerMapper.GetViewModel(dbTeamPlayers);
        }

        public void RefreshTeams(int tournamentId)
        {
            List<DBTeam> teams = _db.Teams.ToList().FindAll(x => x.TeamTournamentId == tournamentId);
            foreach (DBTeam team in teams)
                _db.Entry(team).Reload();
        }

        #endregion

        #region Countries

        public List<VCountry> GetCountries()
        {
            EnsureThereAreCountries();
            List<DBCountry> dbCountries = _db.Countries.OrderBy(x => x.CountryName).ToList();
            return CountryMapper.GetViewModel(dbCountries);
        }

        public List<string> GetCountriesNames()
        {
            EnsureThereAreCountries();
            return _db.Countries.Select(x => x.CountryName).ToList();
        }

        public List<string> GetCountriesNamesWichHaveImageUrl()
        {
            return _db.Countries.ToList().FindAll(x => !x.CountryHtmlImageUrl.Equals(string.Empty)).Select(x => x.CountryName).ToList();
        }

        public string GetCountryImageUrl(string countryName)
        {
            DBCountry country = _db.Countries.ToList().Find(x => x.CountryName.Equals(countryName));
            if (country == null)
                return "";
            else
                return country.CountryHtmlImageUrl;
        }

        public void UpdateCountryImageURL(string countryName, string newValue)
        {
            _db.Countries.ToList().Find(x => x.CountryName.Equals(countryName)).CountryHtmlImageUrl = newValue;
            _db.SaveChanges();
        }

        #region Private

        private void EnsureThereAreCountries()
        {
            //http://www.geonames.org/countries/

            if (_db.Countries.Count() == 0)
            {
                _db.Countries.Add(new DBCountry("No Country",           string.Empty));
                _db.Countries.Add(new DBCountry("Andorra",              string.Empty));
                _db.Countries.Add(new DBCountry("United Arab Emirates", string.Empty));
                _db.Countries.Add(new DBCountry("Afghanistan",          string.Empty));
                _db.Countries.Add(new DBCountry("Antigua and Barbuda", string.Empty));
                _db.Countries.Add(new DBCountry("Anguilla", string.Empty));
                _db.Countries.Add(new DBCountry("Albania", string.Empty));
                _db.Countries.Add(new DBCountry("Armenia", string.Empty));
                _db.Countries.Add(new DBCountry("Angola", string.Empty));
                _db.Countries.Add(new DBCountry("Argentina", string.Empty));
                _db.Countries.Add(new DBCountry("American Samoa", string.Empty));
                _db.Countries.Add(new DBCountry("Austria", string.Empty));
                _db.Countries.Add(new DBCountry("Australia", string.Empty));
                _db.Countries.Add(new DBCountry("Aruba", string.Empty));
                _db.Countries.Add(new DBCountry("Åland", string.Empty));
                _db.Countries.Add(new DBCountry("Azerbaijan", string.Empty));
                _db.Countries.Add(new DBCountry("Bosnia and Herzegovina", string.Empty));
                _db.Countries.Add(new DBCountry("Barbados", string.Empty));
                _db.Countries.Add(new DBCountry("Bangladesh", string.Empty));
                _db.Countries.Add(new DBCountry("Belgium", "http://www.mahjongmadrid.com/wp-content/uploads/2016/05/Belgium-32.png"));
                _db.Countries.Add(new DBCountry("Burkina Faso", string.Empty));
                _db.Countries.Add(new DBCountry("Bulgaria", string.Empty));
                _db.Countries.Add(new DBCountry("Bahrain", string.Empty));
                _db.Countries.Add(new DBCountry("Burundi", string.Empty));
                _db.Countries.Add(new DBCountry("Benin", string.Empty));
                _db.Countries.Add(new DBCountry("Bermuda", string.Empty));
                _db.Countries.Add(new DBCountry("Brunei", string.Empty));
                _db.Countries.Add(new DBCountry("Bolivia", string.Empty));
                _db.Countries.Add(new DBCountry("Bonaire", string.Empty));
                _db.Countries.Add(new DBCountry("Brazil", string.Empty));
                _db.Countries.Add(new DBCountry("Bahamas", string.Empty));
                _db.Countries.Add(new DBCountry("Bhutan", string.Empty));
                _db.Countries.Add(new DBCountry("Botswana", string.Empty));
                _db.Countries.Add(new DBCountry("Belarus", string.Empty));
                _db.Countries.Add(new DBCountry("Belize", string.Empty));
                _db.Countries.Add(new DBCountry("Canada", string.Empty));
                _db.Countries.Add(new DBCountry("Cocos [Keeling] Islands", string.Empty));
                _db.Countries.Add(new DBCountry("Democratic Republic of the Congo", string.Empty));
                _db.Countries.Add(new DBCountry("Central African Republic", string.Empty));
                _db.Countries.Add(new DBCountry("Republic of the Congo", string.Empty));
                _db.Countries.Add(new DBCountry("Switzerland", "http://www.mahjongmadrid.com/wp-content/uploads/2016/05/Switzerland-32.png"));
                _db.Countries.Add(new DBCountry("Ivory Coast", string.Empty));
                _db.Countries.Add(new DBCountry("Cook Islands", string.Empty));
                _db.Countries.Add(new DBCountry("Chile", string.Empty));
                _db.Countries.Add(new DBCountry("Cameroon", string.Empty));
                _db.Countries.Add(new DBCountry("China", string.Empty));
                _db.Countries.Add(new DBCountry("Colombia", string.Empty));
                _db.Countries.Add(new DBCountry("Costa Rica", string.Empty));
                _db.Countries.Add(new DBCountry("Cuba", string.Empty));
                _db.Countries.Add(new DBCountry("Cape Verde", string.Empty));
                _db.Countries.Add(new DBCountry("Curacao", string.Empty));
                _db.Countries.Add(new DBCountry("Christmas Island", string.Empty));
                _db.Countries.Add(new DBCountry("Cyprus", string.Empty));
                _db.Countries.Add(new DBCountry("Czechia", string.Empty));
                _db.Countries.Add(new DBCountry("Germany", "http://www.mahjongmadrid.com/wp-content/uploads/2016/05/Flag-of-Germany-32.png"));
                _db.Countries.Add(new DBCountry("Djibouti", string.Empty));
                _db.Countries.Add(new DBCountry("Denmark", "http://www.mahjongmadrid.com/wp-content/uploads/2016/05/Denmark-32.png"));
                _db.Countries.Add(new DBCountry("Dominica", string.Empty));
                _db.Countries.Add(new DBCountry("Dominican Republic", string.Empty));
                _db.Countries.Add(new DBCountry("Algeria", string.Empty));
                _db.Countries.Add(new DBCountry("Ecuador", string.Empty));
                _db.Countries.Add(new DBCountry("Estonia", string.Empty));
                _db.Countries.Add(new DBCountry("Egypt", string.Empty));
                _db.Countries.Add(new DBCountry("Eritrea", string.Empty));
                _db.Countries.Add(new DBCountry("Spain", "http://www.mahjongmadrid.com/wp-content/uploads/2016/05/Flag-of-Spain-32.png"));
                _db.Countries.Add(new DBCountry("Ethiopia", string.Empty));
                _db.Countries.Add(new DBCountry("Finland", string.Empty));
                _db.Countries.Add(new DBCountry("Fiji", string.Empty));
                _db.Countries.Add(new DBCountry("Falkland Islands", string.Empty));
                _db.Countries.Add(new DBCountry("Micronesia", string.Empty));
                _db.Countries.Add(new DBCountry("Faroe Islands", string.Empty));
                _db.Countries.Add(new DBCountry("France", "http://www.mahjongmadrid.com/wp-content/uploads/2016/05/France-32.png"));
                _db.Countries.Add(new DBCountry("Gabon", string.Empty));
                _db.Countries.Add(new DBCountry("United Kingdom", string.Empty));
                _db.Countries.Add(new DBCountry("Grenada", string.Empty));
                _db.Countries.Add(new DBCountry("Georgia", string.Empty));
                _db.Countries.Add(new DBCountry("Guernsey", string.Empty));
                _db.Countries.Add(new DBCountry("Ghana", string.Empty));
                _db.Countries.Add(new DBCountry("Gibraltar", string.Empty));
                _db.Countries.Add(new DBCountry("Great Britain", string.Empty));
                _db.Countries.Add(new DBCountry("Greenland", string.Empty));
                _db.Countries.Add(new DBCountry("Gambia", string.Empty));
                _db.Countries.Add(new DBCountry("Guinea", string.Empty));
                _db.Countries.Add(new DBCountry("Equatorial Guinea", string.Empty));
                _db.Countries.Add(new DBCountry("Greece", string.Empty));
                _db.Countries.Add(new DBCountry("Guatemala", string.Empty));
                _db.Countries.Add(new DBCountry("Guam", string.Empty));
                _db.Countries.Add(new DBCountry("Guinea-Bissau", string.Empty));
                _db.Countries.Add(new DBCountry("Hong Kong", string.Empty));
                _db.Countries.Add(new DBCountry("Honduras", string.Empty));
                _db.Countries.Add(new DBCountry("Croatia", string.Empty));
                _db.Countries.Add(new DBCountry("Haiti", string.Empty));
                _db.Countries.Add(new DBCountry("Hungary", string.Empty));
                _db.Countries.Add(new DBCountry("Indonesia", string.Empty));
                _db.Countries.Add(new DBCountry("Ireland", string.Empty));
                _db.Countries.Add(new DBCountry("Israel", string.Empty));
                _db.Countries.Add(new DBCountry("Isle of Man", string.Empty));
                _db.Countries.Add(new DBCountry("India", string.Empty));
                _db.Countries.Add(new DBCountry("British Indian Ocean Territory", string.Empty));
                _db.Countries.Add(new DBCountry("Iraq", string.Empty));
                _db.Countries.Add(new DBCountry("Iran", string.Empty));
                _db.Countries.Add(new DBCountry("Iceland", string.Empty));
                _db.Countries.Add(new DBCountry("Italy", "http://www.mahjongmadrid.com/wp-content/uploads/2016/05/Flag-of-Italy-32.png"));
                _db.Countries.Add(new DBCountry("Jersey", string.Empty));
                _db.Countries.Add(new DBCountry("Jamaica", string.Empty));
                _db.Countries.Add(new DBCountry("Jordan", string.Empty));
                _db.Countries.Add(new DBCountry("Japan", string.Empty));
                _db.Countries.Add(new DBCountry("Kenya", string.Empty));
                _db.Countries.Add(new DBCountry("Kyrgyzstan", string.Empty));
                _db.Countries.Add(new DBCountry("Cambodia", string.Empty));
                _db.Countries.Add(new DBCountry("Kiribati", string.Empty));
                _db.Countries.Add(new DBCountry("Comoros", string.Empty));
                _db.Countries.Add(new DBCountry("Saint Kitts and Nevis", string.Empty));
                _db.Countries.Add(new DBCountry("North Korea", string.Empty));
                _db.Countries.Add(new DBCountry("South Korea", string.Empty));
                _db.Countries.Add(new DBCountry("Kuwait", string.Empty));
                _db.Countries.Add(new DBCountry("Cayman Islands", string.Empty));
                _db.Countries.Add(new DBCountry("Kazakhstan", string.Empty));
                _db.Countries.Add(new DBCountry("Laos", string.Empty));
                _db.Countries.Add(new DBCountry("Lebanon", string.Empty));
                _db.Countries.Add(new DBCountry("Saint Lucia", string.Empty));
                _db.Countries.Add(new DBCountry("Liechtenstein", string.Empty));
                _db.Countries.Add(new DBCountry("Sri Lanka", string.Empty));
                _db.Countries.Add(new DBCountry("Liberia", string.Empty));
                _db.Countries.Add(new DBCountry("Lesotho", string.Empty));
                _db.Countries.Add(new DBCountry("Lithuania", string.Empty));
                _db.Countries.Add(new DBCountry("Luxembourg", string.Empty));
                _db.Countries.Add(new DBCountry("Latvia", string.Empty));
                _db.Countries.Add(new DBCountry("Libya", string.Empty));
                _db.Countries.Add(new DBCountry("Morocco", string.Empty));
                _db.Countries.Add(new DBCountry("Monaco", string.Empty));
                _db.Countries.Add(new DBCountry("Moldova", string.Empty));
                _db.Countries.Add(new DBCountry("Montenegro", string.Empty));
                _db.Countries.Add(new DBCountry("Madagascar", string.Empty));
                _db.Countries.Add(new DBCountry("Marshall Islands", string.Empty));
                _db.Countries.Add(new DBCountry("Macedonia", string.Empty));
                _db.Countries.Add(new DBCountry("Mali", string.Empty));
                _db.Countries.Add(new DBCountry("Myanmar", string.Empty));
                _db.Countries.Add(new DBCountry("Mongolia", string.Empty));
                _db.Countries.Add(new DBCountry("Macao", string.Empty));
                _db.Countries.Add(new DBCountry("Northern Mariana Islands", string.Empty));
                _db.Countries.Add(new DBCountry("Martinique", string.Empty));
                _db.Countries.Add(new DBCountry("Mauritania", string.Empty));
                _db.Countries.Add(new DBCountry("Montserrat", string.Empty));
                _db.Countries.Add(new DBCountry("Malta", string.Empty));
                _db.Countries.Add(new DBCountry("Mauritius", string.Empty));
                _db.Countries.Add(new DBCountry("Maldives", string.Empty));
                _db.Countries.Add(new DBCountry("Malawi", string.Empty));
                _db.Countries.Add(new DBCountry("Mexico", string.Empty));
                _db.Countries.Add(new DBCountry("Malaysia", string.Empty));
                _db.Countries.Add(new DBCountry("Mozambique", string.Empty));
                _db.Countries.Add(new DBCountry("Namibia", string.Empty));
                _db.Countries.Add(new DBCountry("Niger", string.Empty));
                _db.Countries.Add(new DBCountry("Norfolk Island", string.Empty));
                _db.Countries.Add(new DBCountry("Nigeria", string.Empty));
                _db.Countries.Add(new DBCountry("Nicaragua", string.Empty));
                _db.Countries.Add(new DBCountry("Netherlands", "http://www.mahjongmadrid.com/wp-content/uploads/2016/05/Netherlands-32.png"));
                _db.Countries.Add(new DBCountry("Norway", string.Empty));
                _db.Countries.Add(new DBCountry("Nepal", string.Empty));
                _db.Countries.Add(new DBCountry("Nauru", string.Empty));
                _db.Countries.Add(new DBCountry("Niue", string.Empty));
                _db.Countries.Add(new DBCountry("New Zealand", string.Empty));
                _db.Countries.Add(new DBCountry("Oman", string.Empty));
                _db.Countries.Add(new DBCountry("Panama", string.Empty));
                _db.Countries.Add(new DBCountry("Peru", string.Empty));
                _db.Countries.Add(new DBCountry("French Polynesia", string.Empty));
                _db.Countries.Add(new DBCountry("Papua New Guinea", string.Empty));
                _db.Countries.Add(new DBCountry("Philippines", string.Empty));
                _db.Countries.Add(new DBCountry("Pakistan", string.Empty));
                _db.Countries.Add(new DBCountry("Poland", string.Empty));
                _db.Countries.Add(new DBCountry("Pitcairn Islands", string.Empty));
                _db.Countries.Add(new DBCountry("Puerto Rico", string.Empty));
                _db.Countries.Add(new DBCountry("Palestine", string.Empty));
                _db.Countries.Add(new DBCountry("Portugal", "http://www.mahjongmadrid.com/wp-content/uploads/2016/05/Portugal-32.png"));
                _db.Countries.Add(new DBCountry("Palau", string.Empty));
                _db.Countries.Add(new DBCountry("Paraguay", string.Empty));
                _db.Countries.Add(new DBCountry("Qatar", string.Empty));
                _db.Countries.Add(new DBCountry("Romania", string.Empty));
                _db.Countries.Add(new DBCountry("Serbia", string.Empty));
                _db.Countries.Add(new DBCountry("Russia", "http://www.mahjongmadrid.com/wp-content/uploads/2017/03/rsz_russia.png"));
                _db.Countries.Add(new DBCountry("Rwanda", string.Empty));
                _db.Countries.Add(new DBCountry("Saudi Arabia", string.Empty));
                _db.Countries.Add(new DBCountry("Solomon Islands", string.Empty));
                _db.Countries.Add(new DBCountry("Seychelles", string.Empty));
                _db.Countries.Add(new DBCountry("Sudan", string.Empty));
                _db.Countries.Add(new DBCountry("Sweden", string.Empty));
                _db.Countries.Add(new DBCountry("Singapore", string.Empty));
                _db.Countries.Add(new DBCountry("Slovenia", string.Empty));
                _db.Countries.Add(new DBCountry("Slovakia", string.Empty));
                _db.Countries.Add(new DBCountry("Sierra Leone", string.Empty));
                _db.Countries.Add(new DBCountry("San Marino", string.Empty));
                _db.Countries.Add(new DBCountry("Senegal", string.Empty));
                _db.Countries.Add(new DBCountry("Somalia", string.Empty));
                _db.Countries.Add(new DBCountry("Suriname", string.Empty));
                _db.Countries.Add(new DBCountry("South Sudan", string.Empty));
                _db.Countries.Add(new DBCountry("São Tomé and Príncipe", string.Empty));
                _db.Countries.Add(new DBCountry("El Salvador", string.Empty));
                _db.Countries.Add(new DBCountry("Sint Maarten", string.Empty));
                _db.Countries.Add(new DBCountry("Syria", string.Empty));
                _db.Countries.Add(new DBCountry("Swaziland", string.Empty));
                _db.Countries.Add(new DBCountry("Turks and Caicos Islands", string.Empty));
                _db.Countries.Add(new DBCountry("Chad", string.Empty));
                _db.Countries.Add(new DBCountry("Togo", string.Empty));
                _db.Countries.Add(new DBCountry("Thailand", string.Empty));
                _db.Countries.Add(new DBCountry("Tajikistan", string.Empty));
                _db.Countries.Add(new DBCountry("Tokelau", string.Empty));
                _db.Countries.Add(new DBCountry("East Timor", string.Empty));
                _db.Countries.Add(new DBCountry("Turkmenistan", string.Empty));
                _db.Countries.Add(new DBCountry("Tunisia", string.Empty));
                _db.Countries.Add(new DBCountry("Tonga", string.Empty));
                _db.Countries.Add(new DBCountry("Turkey", string.Empty));
                _db.Countries.Add(new DBCountry("Trinidad and Tobago", string.Empty));
                _db.Countries.Add(new DBCountry("Tuvalu", string.Empty));
                _db.Countries.Add(new DBCountry("Taiwan", string.Empty));
                _db.Countries.Add(new DBCountry("Tanzania", string.Empty));
                _db.Countries.Add(new DBCountry("Ukraine", string.Empty));
                _db.Countries.Add(new DBCountry("Uganda", string.Empty));
                _db.Countries.Add(new DBCountry("United States", string.Empty));
                _db.Countries.Add(new DBCountry("Uruguay", string.Empty));
                _db.Countries.Add(new DBCountry("Uzbekistan", string.Empty));
                _db.Countries.Add(new DBCountry("Vatican City", string.Empty));
                _db.Countries.Add(new DBCountry("Saint Vincent and the Grenadines", string.Empty));
                _db.Countries.Add(new DBCountry("Venezuela", string.Empty));
                _db.Countries.Add(new DBCountry("British Virgin Islands", string.Empty));
                _db.Countries.Add(new DBCountry("U.S. Virgin Islands", string.Empty));
                _db.Countries.Add(new DBCountry("Vietnam", string.Empty));
                _db.Countries.Add(new DBCountry("Vanuatu", string.Empty));
                _db.Countries.Add(new DBCountry("Samoa", string.Empty));
                _db.Countries.Add(new DBCountry("Kosovo", string.Empty));
                _db.Countries.Add(new DBCountry("Yemen", string.Empty));
                _db.Countries.Add(new DBCountry("South Africa", string.Empty));
                _db.Countries.Add(new DBCountry("Zambia", string.Empty));
                _db.Countries.Add(new DBCountry("Zimbabwe", string.Empty));

                _db.SaveChanges();
            }
        }

        #endregion

        #endregion

        #region EMA Player

        public List<VEmaPlayer> GetEmaPlayers()
        {
            EnsureThereAreEmaPlayers();
            List<DBEmaPlayer> dbEmaPlayer = _db.EmaPlayers.OrderBy(x => x.EmaPlayerEmaNumber).ToList();
            return EmaPlayerMapper.GetViewModel(dbEmaPlayer);
        }

        public List<string> GetAvailableEmaPlayersNames(int tournamentId)
        {
            EnsureThereAreEmaPlayers();
            List<DBEmaPlayer> dbEmaPlayers = _db.EmaPlayers.ToList();
            List<DBPlayer> dbPlayersInUse = _db.Players.ToList().FindAll(x => x.PlayerTournamentId == tournamentId);
            List<DBEmaPlayer> dbEmaPlayersInUse = new List<DBEmaPlayer>(dbPlayersInUse.Count);
            foreach (DBEmaPlayer dbEmaPlayer in dbEmaPlayers)
            {
                foreach (DBPlayer dbEmaPlayerInUse in dbPlayersInUse)
                {
                    if (dbEmaPlayerInUse.PlayerEmaNumber.Equals(dbEmaPlayer.EmaPlayerEmaNumber))
                        dbEmaPlayersInUse.Add(dbEmaPlayer);
                }
            }
            foreach (DBEmaPlayer dbEmaPlayerInUse in dbEmaPlayersInUse)
            {
                dbEmaPlayers.Remove(dbEmaPlayerInUse);
            }

            List<string> emaPlayersNames = new List<string>();
            foreach(DBEmaPlayer dbEmaPlayer in dbEmaPlayers)
            {
                emaPlayersNames.Add(string.Format("{0}, {1} - {2}", 
                    dbEmaPlayer.EmaPlayerLastName,
                    dbEmaPlayer.EmaPlayerName, 
                    dbEmaPlayer.EmaPlayerEmaNumber));
            }
            return emaPlayersNames;
        }

        public void AddEmaPlayer(VEmaPlayer emaPlayer)
        {
            _db.EmaPlayers.Add(EmaPlayerMapper.GetDataModel(emaPlayer));
            _db.SaveChanges();
        }

        public void UpdateEmaPlayerEmaNumber(string oldEmaPlayerEmaNumber, string newEmaPlayerEmaNumber)
        {
            _db.EmaPlayers.ToList().Find(x => x.EmaPlayerEmaNumber.Equals(oldEmaPlayerEmaNumber)).EmaPlayerEmaNumber = newEmaPlayerEmaNumber;
            _db.SaveChanges();
        }

        public void UpdateEmaPlayerName(string emaPlayerEmaNumber, string newName)
        {
            _db.EmaPlayers.ToList().Find(x => x.EmaPlayerEmaNumber.Equals(emaPlayerEmaNumber)).EmaPlayerName = newName;
            _db.SaveChanges();
        }

        public void UpdateEmaPlayerLastName(string emaPlayerEmaNumber, string newLastName)
        {
            _db.EmaPlayers.ToList().Find(x => x.EmaPlayerEmaNumber.Equals(emaPlayerEmaNumber)).EmaPlayerLastName = newLastName;
            _db.SaveChanges();
        }

        public void UpdateEmaPlayerCountry(string emaPlayerEmaNumber, string newCountryName)
        {
            _db.EmaPlayers.ToList()
                .Find(x => x.EmaPlayerEmaNumber.Equals(emaPlayerEmaNumber))
                .EmaPlayerCountryName = newCountryName;
            _db.SaveChanges();
        }

        #region Private

        private void EnsureThereAreEmaPlayers()
        {
            //http://mahjong-europe.org/ranking/mcr.html

            if (_db.EmaPlayers.Count() == 0)
            {
                _db.EmaPlayers.Add(new DBEmaPlayer("1000001", "SCHEICHENBAUER", "Martin", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000009", "TSCHINKEL", "Norbert", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000013", "DOPPELHOFER", "Alexander", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000015", "GLASER", "Ernest", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000016", "MYSLIVEC", "Otto", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000022", "ALDRIAN", "Maria", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000024", "STOLLWITZER", "Margarete", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000025", "JARITZ", "Gerti", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000027", "SALLMUTTER", "Gabriele", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000030", "RIHA", "Eberhard", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000038", "FRISCHENSCHLAGER", "Elisabeth", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000039", "POPRAT", "Günter", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000045", "GINTSBERGER", "Günter", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000046", "GINTSBERGER", "Elvira", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000059", "SALLMUTTER", "Doris", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000060", "DIVJAK", "Felix", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000061", "DIVJAK", "Romana", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000069", "RIEGLER", "Helmut", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000071", "MARESCH", "Edeltraud", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000073", "KANDORFER", "Heinz", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000088", "VOLLMANN - GAO", "Qian", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000092", "ALEXEJEW", "Monika", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000093", "FELSBERGER", "Hermi", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000105", "BERNKOPF", "Johanna", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000108", "TRINKL", "Matthias", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000109", "RIHA", "Andreas", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("1000110", "NEUWIRTH", "Franz", "Austria"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010001", "MICHIELS", "Yannick", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010014", "VANDERBIST", "Wenda", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010016", "MOUS", "Linda", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010021", "VAN DAMME", "Peter", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010022", "MAES", "Joke", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010024", "MICHIELS", "Irma", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010025", "KOPMANIS", "Rudy", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010028", "DEWOLF", "Marleen", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010031", "MOUWEN", "Emma", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010033", "VERHEYDEN", "Marieke", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010036", "KAM", "Benjamin", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010038", "DE ROOCK", "Chris", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010039", "DERMOUT", "Jos", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010040", "RENSON", "Tijs", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010041", "PEVERNAGE", "Hilde", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010042", "KEMPER", "Sophie", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010043", "WECKHUYZEN", "Tine", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010044", "DERMOUT", "Kristien", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010045", "MORRENS", "Arnaud", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010049", "VERMEERSCH", "Jonas", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010050", "VAN HOOF", "Anita", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010052", "NAERT", "Lies", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("2010053", "GEUDENS", "Linda", "Belgium"));
                _db.EmaPlayers.Add(new DBEmaPlayer("3000003", "CHRISTENSEN", "Tina", "Denmark"));
                _db.EmaPlayers.Add(new DBEmaPlayer("3000005", "STIG NIELSEN", "Jeppe", "Denmark"));
                _db.EmaPlayers.Add(new DBEmaPlayer("3000022", "LETH", "Henrik", "Denmark"));
                _db.EmaPlayers.Add(new DBEmaPlayer("3000049", "CHRISTIANSEN", "Freddy", "Denmark"));
                _db.EmaPlayers.Add(new DBEmaPlayer("3000123", "IVERSEN", "Kim", "Denmark"));
                _db.EmaPlayers.Add(new DBEmaPlayer("3000126", "THERKELSEN", "Lars", "Denmark"));
                _db.EmaPlayers.Add(new DBEmaPlayer("3000140", "CHEN KOLD", "Shi Hua", "Denmark"));
                _db.EmaPlayers.Add(new DBEmaPlayer("3000142", "ASKJæR-FRIIS", "Mikkel", "Denmark"));
                _db.EmaPlayers.Add(new DBEmaPlayer("3000148", "ROSTVED", "Frank", "Denmark"));
                _db.EmaPlayers.Add(new DBEmaPlayer("3000149", "BAHIANO STEENHOLM", "Isabel", "Denmark"));
                _db.EmaPlayers.Add(new DBEmaPlayer("3000176", "NØHR", "Jesper", "Denmark"));
                _db.EmaPlayers.Add(new DBEmaPlayer("3000192", "LAVALLEE", "Sebastian", "Denmark"));
                _db.EmaPlayers.Add(new DBEmaPlayer("3000215", "OLSEN", "Ting Fang", "Denmark"));
                _db.EmaPlayers.Add(new DBEmaPlayer("3000313", "PETERSEN", "Jacob", "Denmark"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4010007", "UGO", "Jean Claude", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4010011", "CHAMPENOIS", "Jean-Luc", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4010015", "DIFERNAND", "Renéta", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4010016", "BONMALAIS", "Eric", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4010020", "ROBERT", "Frédéric", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4010025", "ENAULT", "Gilbert", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4010026", "PAYET", "Olivier", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4010027", "PAYET", "Ghislaine", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4010028", "TSONG CHIN CHUEN", "Marcel", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4010039", "LARINIER", "Pascale", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4010040", "SAUNIE", "Nicolas", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4010052", "BRONES", "Félicia", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4010054", "EBLE", "Philippe", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4010055", "EBLE", "Catherine", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4010061", "TAYLOR", "Christine", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4010066", "PAUSE", "Annick", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4010076", "CADI", "Johanna", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4030003", "AUBERVAL", "Jean Pascal", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4030004", "TAI LEUNG", "Emmanuel", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4030005", "AURE", "David Gérard", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4030006", "AURE", "Suzy", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4030022", "MORISSE", "Jean Michel", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4030023", "GRONDIN", "Frédérique", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4030028", "ORREGIA", "Marie-Josée", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4030045", "DORIS", "Reine-Marie", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4030047", "RODULFO", "Jeannine", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4030048", "RODULFO", "Antoine", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4030062", "JOURDAIN", "David", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4030067", "LAW-THO", "Marie-Claire", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4030074", "BARBET", "Jean Louis", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4030087", "COSTEY", "Jonathan", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4030091", "JANAC", "Wilson Nathanaël", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4030093", "MC DONALD", "James", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4030100", "MORISSE", "Valérie", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4030103", "RIVIERE", "Mickaël", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4030109", "FONTAINE", "Jean-Philippe", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4030124", "BARRET", "Cécile", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4040010", "BOIVIN", "Olivier", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4040017", "SAHAL", "Jérome", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4040026", "MESSI FOUDA", "Benoit", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4040034", "LEGAIE", "Lionel", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4040036", "D ANGELO", "Christiane", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4040047", "BONDOIN", "Sandra", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4040057", "RATSIMANDRESY", "Joel", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4040058", "LEFEBVRE", "Christophe", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4040083", "VIGNAUD", "Hubert", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4040105", "EA", "Anthony", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4040110", "ROBLIN", "Thérèse", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4040170", "RAVEL", "Judith", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4040187", "BARBET", "Jérémie", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4040189", "PETIT", "Frédéric", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4040205", "PFEIFFER", "Ji Li", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4040212", "GAO", "François", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4040217", "LAO", "Bernard", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4040238", "GAUCHER", "Fabien", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4050007", "VILLEPOU", "Caroline", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4050022", "BALAGOUROU", "André", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4060010", "LAM WAN SHUM", "Alfred", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4060011", "LAM WAN SHUM", "Emilie", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4060014", "METRO", "Claudia", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4060017", "BENEGEAN", "Monique", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4060020", "AH-LEUNG", "Liliane", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4060021", "GUERIN", "Michele", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4060022", "AH-PINE", "Monique", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4060023", "CHANE YUM", "Antoine", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4060025", "THIM SIONG", "Régine", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4060026", "POTHIN", "Gilette", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4060027", "LAFON", "Jacqueline", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4060031", "GAUTRON", "Fabienne", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4060033", "GONTHIER", "Françoise", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4060037", "BAS", "Micheline", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4060042", "SANGUY", "Mauricette", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4060049", "PAUGAM", "Colette", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4060050", "MAISTRY", "Saaddyia", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4060051", "ROSTIN", "Christine", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4060061", "BAUD", "Patrick", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4060067", "PERROT", "Brigitte", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4080001", "BOIDIN", "Héléne", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4080002", "BALORIN", "Pascal", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4080004", "ENAULT", "Christian", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4080005", "LETOULLEC", "René", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4080007", "GAUVIN", "Annick", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4080010", "BOYER", "Stéphanie Aline", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4080018", "GIGANT", "Sylvie", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4080019", "BARRATAULT", "Maria", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4080021", "DUBOZ", "Simone", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4090008", "FONGUE", "Simon Bounkéo", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4090016", "RAK", "Agnès", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4090024", "MANZO", "Annie", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4090025", "MANZO", "Bruno", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4090026", "RAK", "Cyrille", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4090035", "DRAUX", "Apolline", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4090041", "DELBOS", "Josianne", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4090045", "BOUVET", "Aymeric", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4090059", "MOREAU", "Mélanie", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4090060", "ARNAUD", "Caroline", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4090071", "VENTURINI", "Patrick", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4090075", "AUBRUN", "Michèle", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4090080", "AGUERRE", "Cédric", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4090083", "SVAY", "Christina", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4090088", "FERRANTE", "Sébastien", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4090090", "BONNEAU", "Anne-Sophie", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4090091", "LARRIGAUDIERE", "Nathalie", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4090093", "GUERIN", "Guillaume", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4090098", "ROYET", "Anne", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4100018", "GIRDARY", "Marie Lucie", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4120026", "MOTHE", "Norbert", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4120060", "HUANG", "Shuyao", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4120070", "DESCAMPS", "Florent", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4130003", "MATHERN", "Claire", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4130004", "GUTSCHE", "Sven-Hendrik", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4130005", "VALOGNES", "Sylvie", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4130015", "TOUSSAINT", "Kevin", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4130019", "LEROY", "Florine", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4130025", "CLAUDEL", "Laurent", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4130026", "LAPLAGNE", "Fanette", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4130027", "HERDIER", "Romain", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4130032", "XANTHOPOULOS", "Catherine", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4130036", "TOUSSAINT", "Florence", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4130055", "XANTHOPOULOS", "Tim", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4130073", "DES ROBERT", "Dominique", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4130092", "SIMON", "Alexandre", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4130106", "DEMICHEL", "Eva", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4140001", "GEFFRIAUD", "Christophe", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4140002", "GEFFRIAUD", "Evi", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4140006", "RICHOMME", "Christophe", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4140034", "TCHERNYCHEV", "Konstantin", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4140040", "HERMIER", "Sarah", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4140049", "HOCHIN", "Michèle", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4140057", "MIALIN", "Monique", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4140059", "CHARRIER", "Françoise", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4160002", "AZOUGAGH", "Aziz", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4180001", "HEDO", "Joëlle", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4180005", "CHEUNG-AH-SEUNG", "Lucile", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4180009", "TECHER", "Daniel", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4210013", "BLOTTIN", "Maeva", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4210015", "ROY", "Olivier", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4210018", "TOUCAS", "Elizabeth", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4210023", "BOISSON", "Véronique", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4240011", "KIEFFER", "Marie", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4240012", "ZINNIGER", "Christophe", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4280001", "TACHE", "Olivier", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4280004", "DUBEDOUT", "Astrid", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4280006", "LE MORVAN", "Éric", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4280011", "CROCCO", "Sébastien", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4290001", "BERTHOMMIER", "Sandra", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4290006", "HUGOT", "Emmanuelle", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4290008", "PARRIAUD", "Jean-François", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4290023", "SANTOS", "Manuel", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4290030", "DE KERGOMMEAUX", "Erwan", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4290031", "DE KERGOMMEAUX", "Loic", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4290032", "EREAU", "Baptiste", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4390003", "VAITILINGOM", "Micheline", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4390007", "HOARAU", "Mathieu", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4390008", "DE PALMAS", "Joël", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4990015", "CLAUDEL", "Thierry", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4990024", "DI DOMENICO", "Annabel", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4990025", "DI DOMENICO", "Corine", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4990036", "AUBRUN", "Daniel", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4990054", "YOU-SEEN", "Vincent", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("4990062", "PFEIFFER", "Matthieu", "France"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100016", "JENJAHN", "Monika", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100017", "GERDES", "Erika", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100018", "PÜNJER", "Elke", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100020", "MEIER", "Marion", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100024", "GERDES", "Friedrich", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100033", "DUHME", "Stefanie", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100056", "SCHÄFER", "Heike", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100057", "SCHÄFER", "Anne", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100058", "FISCHER", "Dagmar", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100067", "EICKSCHEN", "Ulla", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100077", "LUDWIG", "Heidi", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100078", "LUDWIG", "Heinz", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100086", "BOHLMANN", "Fred", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100092", "CAPITO", "Cornelia", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100100", "MANTEN", "Sabine", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100101", "DOMANN", "Heiko", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100124", "BEESE", "Peter", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100127", "MÜLLER", "Robert", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100141", "NOLTING", "Bino", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100142", "BRINKMEIER", "Elke", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100143", "CHEN", "Wei", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100153", "HAHN", "Timur", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100164", "GONG", "Yixin", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5100165", "SCHNIER", "Jürgen", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5900002", "KÖNIG", "Nadine", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5900003", "ZAHRADNIK", "Michael", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5900008", "BANSEMER", "Silke", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("5900010", "PEKAU", "Uwe", "Germany"));
                _db.EmaPlayers.Add(new DBEmaPlayer("6010002", "NYULASI", "Zsolt", "Hungary"));
                _db.EmaPlayers.Add(new DBEmaPlayer("6010008", "BOGARNÈ NAGY", "Magdolna", "Hungary"));
                _db.EmaPlayers.Add(new DBEmaPlayer("6010011", "HARGITAI", "Ildikó", "Hungary"));
                _db.EmaPlayers.Add(new DBEmaPlayer("6010021", "TAKÁCS", "Sándor", "Hungary"));
                _db.EmaPlayers.Add(new DBEmaPlayer("6010022", "TAKÁCSNÉ LOWINGER", "Klára", "Hungary"));
                _db.EmaPlayers.Add(new DBEmaPlayer("6010025", "VÁRNAI", "Eszter", "Hungary"));
                _db.EmaPlayers.Add(new DBEmaPlayer("6010031", "MÁTRAI", "Jánosné", "Hungary"));
                _db.EmaPlayers.Add(new DBEmaPlayer("6020003", "HALÁSZ", "Pál", "Hungary"));
                _db.EmaPlayers.Add(new DBEmaPlayer("6020014", "BERI", "Márta Rózsa", "Hungary"));
                _db.EmaPlayers.Add(new DBEmaPlayer("6020016", "SPRECHER", "Károly", "Hungary"));
                _db.EmaPlayers.Add(new DBEmaPlayer("6020017", "ELEK", "David", "Hungary"));
                _db.EmaPlayers.Add(new DBEmaPlayer("6020019", "SZEPESVARI", "Szilvia", "Hungary"));
                _db.EmaPlayers.Add(new DBEmaPlayer("6020021", "FÓRIZS", "Ilona", "Hungary"));
                _db.EmaPlayers.Add(new DBEmaPlayer("6990001", "PALCZER-ASCHENBRENNER", "Eti", "Hungary"));
                _db.EmaPlayers.Add(new DBEmaPlayer("6990017", "NEMETH", "Attila", "Hungary"));
                _db.EmaPlayers.Add(new DBEmaPlayer("6990025", "VLADAR", "Ferenc", "Hungary"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000001", "GAVELLI", "Luca", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000002", "BAZZOCCHI", "Marco", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000005", "BUSCARINI", "Patrizia", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000006", "NATALI", "Daniela", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000007", "MILANDRI", "Marco", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000008", "VERPELLI", "Andrea", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000009", "RIJOFF", "Stefano", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000011", "PLEBANI", "Angela", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000013", "PROCOPIO", "Stefania", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000014", "BONALDO", "Rosita", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000017", "BOLLINO", "Michele", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000018", "MONTEBELLI", "Marco", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000020", "PIZZI", "Loretta", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000021", "SAVINI", "Elena", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000024", "PORRATI", "Serena", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000033", "UGUCCIONI", "Letizia", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000034", "COMERCI", "Michele", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000035", "FERRUZZI", "Giacomo", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000042", "BASSI", "Vittorio", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000048", "PIANCASTELLI", "Gianfranco", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000052", "LORENZI", "Marco", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000055", "TRANCHINA", "Massimiliano", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000062", "BENINI", "Paride", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000065", "GORI", "Stefania", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000070", "FOSCHI", "Marco", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000071", "ARGENTIERO", "Giusi", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000072", "GATTA", "Bruna", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000075", "FERRI", "Fabrizio", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000078", "ENFI", "Laura", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000080", "GHERARDI", "Augusto", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000082", "PEREGO", "Roger", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000083", "FOSCHI", "Elisa", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000088", "GARRIDO", "Carmelita", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000089", "CIOL", "Maurizio", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000092", "TARONI", "Piero", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000093", "TASSINARI", "Ombretta", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000095", "CALOSCI", "Rossella", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000096", "MARTINI", "Francesco", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000104", "BATTAGLINI", "Francesca", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000107", "SERIO", "Danilo", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000109", "MARTELLI", "Miria", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000111", "PECE", "Franca", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000114", "ROSI", "Alberto", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000115", "IACOLINO", "Giuseppe", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000119", "PEDRINI", "Enrica", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000124", "PEDRINI", "Roberta", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000127", "MORIGI", "Valentina", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000129", "MARINO", "Demetrio", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000130", "VENTURI", "Alberto", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000131", "BAGNOLI", "Maurizio", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000135", "MOZNICH", "Paolo", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000138", "FOLESANI", "Claudia", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000141", "VALENTINO", "Valentina", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000148", "BONCRISTIANO", "Luca", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000150", "BASCHIROTTO", "Nicoletta", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000151", "FERUGLIO", "Elisa", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000152", "CERRA", "Valentina", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000155", "PALMISANO", "Oscar", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000156", "MAMONE", "Laura", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000158", "RENDA", "Andrea", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000160", "MASTRULLO", "Davide", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000163", "CIVAROLI", "Marco", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000170", "SANSEVIERI", "Domenico", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("7000172", "CRAGNOLIN", "Luca", "Italy"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010039", "KÖSTERS", "Anton", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010022", "ONNINK", "Janco", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010025", "CROEZE", "Marianne", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010166", "HOOGLAND", "Ans", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010599", "VAN DE NIEUWENDIJK", "Marjan", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010412", "LINDEN, VAN DER", "Pauline", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010028", "BROERS", "Eveline", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010430", "OORSOUW, VAN", "Anja", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010021", "HEEMSKERK", "Désirée", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010411", "LINDEN, VAN DER", "Ad", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010667", "NIEUWENDIJK", "Twan", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010003", "OORSCHOT, VAN", "Gerda", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010052", "HEIDE, VAN DER", "Yvonne", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010101", "BALKUM, VAN", "Eric", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010526", "SOMMERS", "Olav", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010121", "MEIJER - KAL", "Wil", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010158", "WESTDIJK", "Diana", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010698", "VAN BALKUM", "Luuk", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010168", "GRINSVEN, VAN", "Dimphy", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010305", "VAN WIJK", "Janine", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010727", "OUDSHOOM", "Jacqueline", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010113", "TEUNISSEN", "Eugenie", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010427", "PASTERKAMP", "Martha", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010557", "VAN THIEL", "Martin", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010609", "HOOGLAND", "Jose", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010295", "PUTTEN, VAN DER", "Ria", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010445", "VOS", "Barry", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010518", "BOEKSTAAF", "Dennis", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010594", "OORSOUW, VAN", "Mattheu", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010137", "JONG, DE", "Zeger", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010026", "CROEZE", "Jaap", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010035", "HEIJN", "Berry", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010187", "BRINK, VAN DEN", "Cees", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010173", "KEYL", "Anneke", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010730", "DEIJ - VAN RIJSWIJK", "Menno", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010732", "VAN MOURIK", "Herma", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010162", "MOREL", "Hanneke", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010633", "JONG, DE", "Joke", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010017", "SCHEFFLER", "Chris", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010731", "VAN WIJNGAARDEN", "Sandra", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010598", "VAN DE NIEUWENDIJK", "Oscar", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010580", "BAKKER", "Foppe", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010036", "JANSSEN", "Leni", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010123", "BIE, VAN DER", "Gerard", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010545", "CORNE", "Neil", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010415", "ARINGANENG", "Nino", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010495", "LIENDEN VAN", "Menno", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010656", "DROST", "Vera", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010613", "LIPS", "Marjoleine", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010128", "KAL", "Harry", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010049", "VEGT, VAN DER", "Gert", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010328", "WARBOUT", "Jan", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010219", "GROOT, DE", "Aty", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010689", "LAMMERS", "Ronald", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010126", "WONG-CHUNG", "Rudy", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010083", "SOHANA", "Ma Prem", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010140", "ELFERINK", "Petra", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010171", "CLAESSEN", "Bert", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010206", "ELFERINK", "Leo", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010486", "BAKKER", "Anneke", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010122", "BIE, VAN DER", "Tonny", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010733", "BOL", "Pieter", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010027", "JANSSEN", "Chris", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010344", "PILGRAM", "Jack lien", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010687", "HEYLIGER", "Jessica", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010505", "MEULENDIJK", "Sonja", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010109", "MEIJER", "Jeroen", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010593", "BOOGAART", "Anja", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010650", "DE VRIES", "Jakko", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010376", "MARSIDI", "Ieneke", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010054", "SCHOLTE", "Piet", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010701", "KOSTER", "Ineke", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010470", "BAZUIN", "Cisca", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010665", "BOSKAMP", "Liesbeth", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010621", "SCHOLTEN", "Rik", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010196", "DONK", "Grea", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010271", "MEIJER", "Trees", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010568", "NOLTEN", "Jannie", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010699", "MALLEE", "Annemiek", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010564", "KITSLAAR", "Annet", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010351", "KUIJPERS", "John", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010510", "WEBER", "Mieke", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010538", "HAGMAN", "Annie", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010047", "BROEDER", "Ingrid", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010706", "CRAAIKAMP", "Anna", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010023", "HEEMSKERK", "Yvonne", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010569", "SCHAADERS", "Leny", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("8010120", "SATRIASAPUTRA", "Dewi", "Netherlands"));
                _db.EmaPlayers.Add(new DBEmaPlayer("9990008", "HENRIËT", "Moa", "Sweden"));
                _db.EmaPlayers.Add(new DBEmaPlayer("9990009", "ÖSTMAN", "Emelie", "Sweden"));
                _db.EmaPlayers.Add(new DBEmaPlayer("9990039", "NORÉN", "Marie", "Sweden"));
                _db.EmaPlayers.Add(new DBEmaPlayer("9990044", "RANEFALL", "Petter", "Sweden"));
                _db.EmaPlayers.Add(new DBEmaPlayer("9990045", "RIDELL", "Annika", "Sweden"));
                _db.EmaPlayers.Add(new DBEmaPlayer("9990047", "GERMEYS", "Jasper", "Sweden"));
                _db.EmaPlayers.Add(new DBEmaPlayer("9990088", "FAHLSTRÖM", "Emma", "Sweden"));
                _db.EmaPlayers.Add(new DBEmaPlayer("9990095", "HJORT", "Mikael", "Sweden"));
                _db.EmaPlayers.Add(new DBEmaPlayer("9990099", "AVENEL", "Christophe", "Sweden"));
                _db.EmaPlayers.Add(new DBEmaPlayer("9990104", "NORÉN", "Caroline", "Sweden"));
                _db.EmaPlayers.Add(new DBEmaPlayer("9990106", "LANDOLSI", "Sara", "Sweden"));
                _db.EmaPlayers.Add(new DBEmaPlayer("9990107", "GABOARDI", "Felipe", "Sweden"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990007", "MORENO MERINO", "José", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990012", "FUENTES CARREÑO", "Diana", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990016", "MAESTRE ROS", "Ivan", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990017", "TÚNEZ HEREDIA", "Samuel", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990018", "ESCASO GIL", "Héctor", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990019", "MARTÍNEZ GARCIA", "David", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990020", "AMADOR LORENTE", "Eduardo", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990021", "GARCÍA GARCÍA", "Domingo", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990022", "VALBUENA MEDINA", "Carmen", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990024", "TÚNEZ PÉREZ", "Maria del Mar", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990025", "CASTELLANO RIVAS", "Maria del Pilar", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990026", "RUBIO RODILLA", "Edgar", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990027", "AYLLÓN", "Antonio", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990028", "MARTÍNEZ FERNÁNDEZ", "Alejandro", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990037", "TÚNEZ HEREDIA", "Katia", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990039", "VEGA DE LA IGLESIA SORIA", "Ernesto", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990043", "RÍOS NAVARRO", "Raúl", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990047", "PASCUAL RAMÍREZ", "Miguel", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990051", "SAN ANTONIO SERRANO", "Marcos", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990055", "MORENO PESQUERA", "José Manuel", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990057", "RUBIO RODRIGUEZ", "Luis Eduardo", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990058", "CIUDAD GÓMEZ", "David", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990060", "BALLESTER CARACENA", "Mario", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990061", "HERMOSIN", "Ezequiel", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990062", "SANCHEZ FUENTES", "Elvira", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990063", "RIQUELME SÁNCHEZ", "Abraham Antonio", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("10990070", "SOLER RECIO", "David", "Spain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("11990005", "FRASER", "Ian", "Great Britain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("11990016", "DUCKWORTH", "John", "Great Britain"));
                _db.EmaPlayers.Add(new DBEmaPlayer("12990002", "MANUEL MACHADO", "Rui", "Portugal"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990005", "STEPANOV", "Vladimir", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990008", "DANILEVSKAYA", "Alla", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990009", "DULENKO", "Galina", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990012", "NOVIKOV", "Vitaly", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990018", "STEPANOVA", "Anna", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990019", "NETREBINA", "Evgenia", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990021", "CHICHIGINA", "Natalya", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990023", "ANOKHIN", "Pavel", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990025", "BABUSHKIN", "Aleksandr", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990026", "GOPTAREVA", "Tatyana", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990027", "LOGVINOV", "Oleg", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990028", "CHICHIGIN", "Andrey", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990040", "KVASHA", "Ilya", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990042", "MANDJIEV", "Arslan", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990070", "SHPILMAN", "Anna", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990072", "NESTER", "Tatyana", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990075", "BIRUKOV", "Igor", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990078", "TEREKHIN", "Vladimir", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990086", "VERDI", "Anton", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990088", "NECHAEV", "Roman", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990091", "RYZHANINA", "Irina", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990092", "DASHKOVA", "Anna", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990093", "USHAKOVA", "Elena", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990095", "DULENKO", "Fedor", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990103", "VASILIEV", "Sergey", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990104", "HLUDENTSOVA", "Evgeniya", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990107", "BOGATIKOV", "Aleksander", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990108", "CHERNYKH", "Anastasiya", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990114", "PETUHOV", "Mikhail", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990117", "BOGACHKOV", "Dmitry", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990128", "PANINA", "Tamara", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990134", "VERDI", "Elena", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990135", "ARZAMASCEV", "Vladimir", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990174", "KUDRYAVTSEVA", "Anastasiya", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990175", "MYSKIN", "Filipp", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990176", "GOLIKOVA", "Natalia", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990177", "ROMANETSKAYA", "Olga", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990191", "ZOLOTAREVA", "Nadezhda", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990213", "CHICHIGIN", "Viacheslav", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990215", "SHPILMAN", "Aleksei", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990216", "VERIK", "Elena", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990233", "MALAKHOV", "Sergey", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990236", "BYKOV", "Dmitriy", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990251", "LUGANNIKOV", "Denis", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990265", "SOLOVJEV", "Anton", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990268", "BELYALOVA", "Maria", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990273", "KIM", "Evgeniy", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990291", "ZINCHENKO", "Vadim", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990315", "MANIAKHIN", "Petr", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990316", "SAPOROVSKII", "Andrei", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990317", "SIMONOVA", "Svetlana", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990318", "KONSTANTINOVA", "Ekaterina", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990324", "IVANOV", "Andrey", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990325", "TOULINA", "Ekaterina", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990327", "LAVRENIUK", "Irina", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990328", "FILIUSHKINA", "Veronika", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990332", "KIRILLOVA", "Anna", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990352", "SVIDRITSKIY", "Oleg", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990353", "TURLINA", "Elena", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("15990354", "NEPOMNYASHCHIKH", "Anastasiya", "Russia"));
                _db.EmaPlayers.Add(new DBEmaPlayer("16000001", "FELDER", "Mei Hwa", "Switzerland"));
                _db.EmaPlayers.Add(new DBEmaPlayer("16000002", "LANG", "Bo", "Switzerland"));
                _db.EmaPlayers.Add(new DBEmaPlayer("16000005", "HUMBERT", "Luc", "Switzerland"));
                _db.EmaPlayers.Add(new DBEmaPlayer("16000006", "PETEREIT", "Oliver", "Switzerland"));
                _db.EmaPlayers.Add(new DBEmaPlayer("16000007", "CHASSOT", "Frédéric", "Switzerland"));
                _db.EmaPlayers.Add(new DBEmaPlayer("16000008", "CANOVA PUTINIER", "Anna", "Switzerland"));
                _db.EmaPlayers.Add(new DBEmaPlayer("16000009", "JACQUART", "Nathalie", "Switzerland"));
                _db.EmaPlayers.Add(new DBEmaPlayer("16000010", "GROUX", "Marie-Hélène", "Switzerland"));
                _db.EmaPlayers.Add(new DBEmaPlayer("16000011", "HÊCHE", "Gérard", "Switzerland"));
                _db.EmaPlayers.Add(new DBEmaPlayer("16000016", "MAYR DELACRÉTAZ", "Tanja", "Switzerland"));
                _db.EmaPlayers.Add(new DBEmaPlayer("16000020", "DELAY", "Patrice", "Switzerland"));
                _db.EmaPlayers.Add(new DBEmaPlayer("16000021", "HOFMANN", "Marc-Antoine", "Switzerland"));
                _db.EmaPlayers.Add(new DBEmaPlayer("16000025", "DELACRETAZ", "Sébastien", "Switzerland"));
                _db.EmaPlayers.Add(new DBEmaPlayer("16000026", "HERNANDEZ", "Esther", "Switzerland"));
                _db.EmaPlayers.Add(new DBEmaPlayer("16000027", "HUNZIKER", "Aurorita", "Switzerland"));
                _db.EmaPlayers.Add(new DBEmaPlayer("16000030", "ALONSO BLANCO", "Marcos", "Switzerland"));
                _db.EmaPlayers.Add(new DBEmaPlayer("16000031", "LEE", "Jun", "Switzerland"));
                _db.EmaPlayers.Add(new DBEmaPlayer("19000080", "CHABELSKA", "Katarzyna", "Poland"));



                _db.SaveChanges();
            }
        }

        #endregion

        #endregion
    }
}
