using System.Collections.Generic;
using System.Linq;
using System;
using static MahjongTournamentSuite._Data.DBContext;
using MahjongTournamentSuite._Data.DataModel;
using MahjongTournamentSuite._Data.Mappers;
using MahjongPlayerSuite._Data.Mappers;
using MahjongTableSuite._Data.Mappers;
using MahjongTeamSuite._Data.Mappers;

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

        public void RefreshPlayers(int tournamentId)
        {
            List<DBTeam> teams = _db.Teams.ToList().FindAll(x => x.TeamTournamentId == tournamentId);
            foreach (DBTeam team in teams)
                _db.Entry(team).Reload();
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

        public void RefreshTable(int tournamentId, int roundId, int tableId)
        {
            DBTable table = _db.Tables.ToList().Find(x => x.TableTournamentId == tournamentId
            && x.TableRoundId == roundId && x.TableId == tableId);
            _db.Entry(table).Reload();
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

        public void RefreshTeams(int tournamentId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Countries

        public List<VCountry> GetCountries()
        {
            EnsureThereAreCountries();
            List<DBCountry> dbCountries = _db.Countries.OrderBy(x => x.CountryName).ToList();
            return CountryMapper.GetViewModel(dbCountries);
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
                _db.Countries.Add(new DBCountry("No Country", string.Empty));
                _db.Countries.Add(new DBCountry("Andorra", string.Empty));
                _db.Countries.Add(new DBCountry("United Arab Emirates", string.Empty));
                _db.Countries.Add(new DBCountry("Afghanistan", string.Empty));
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
    }
}
