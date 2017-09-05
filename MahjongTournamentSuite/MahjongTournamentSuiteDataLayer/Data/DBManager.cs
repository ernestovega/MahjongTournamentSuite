using System.Collections.Generic;
using System.Linq;
using System;
using static MahjongTournamentSuiteDataLayer.Data.DBContext;
using MahjongTournamentSuiteDataLayer.Model;

namespace MahjongTournamentSuiteDataLayer.Data
{
    public class DBManager : IDBManager
    {
        #region Fields

        TournamentSuiteDB _db = new TournamentSuiteDB();

        #endregion

        #region Constructor

        public DBManager() { }

        #endregion

        #region Tournaments

        public List<DBTournament> GetTournaments()
        {
            return _db.Tournaments.OrderByDescending(
                x => x.CreationDate).ToList();
        }

        public DBTournament GetTournament(int tournamentId)
        {
            return _db.Tournaments.ToList().Find(
                x => x.TournamentId == tournamentId);
        }

        public int GetExistingMaxTournamentId()
        {
            if (_db.Tournaments.Count() == 0)
                return 0;
            else
                return _db.Tournaments.Max(x => x.TournamentId);
        }

        public string GetTournamentName(int tournamentId)
        {
            return _db.Tournaments.ToList().Find(
                x => x.TournamentId == tournamentId).TournamentName;
        }

        public bool ExistTournament(string tournamentName)
        {
            return _db.Tournaments.ToList().Find(
                x => x.TournamentName.Equals(tournamentName, 
                StringComparison.Ordinal)) != null;
        }

        public void AddTournament(DBTournament tournament)
        {
            _db.Tournaments.Add(tournament);
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
            DBTournament tournament = _db.Tournaments.ToList().Find( x => x.TournamentId == tournamentId);
            if (tournament.IsTeams)
                DeleteTeams(_db.Teams.ToList().FindAll( x => x.TeamTournamentId == tournamentId));
            _db.Tournaments.Remove(tournament);
            _db.SaveChanges();
        }

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

        #region Player

        public List<DBPlayer> GetTournamentPlayers(int tournamentId)
        {
            return _db.Players.ToList().FindAll(x => x.PlayerTournamentId == tournamentId);
        }

        public List<DBPlayer> GetTablePlayers(int tournamentId, int roundId, int tableId)
        {
            DBTable table = _db.Tables.ToList().Find(x => x.TableTournamentId == tournamentId && 
                x.TableRoundId == roundId && x.TableId == tableId);
            List<DBPlayer> tablePlayers = new List<DBPlayer>(4);
            tablePlayers.Add(_db.Players.ToList().Find(
                x => x.PlayerTournamentId == tournamentId && 
                x.PlayerId == table.Player1Id));
            tablePlayers.Add(_db.Players.ToList().Find(
                x => x.PlayerTournamentId == tournamentId && 
                x.PlayerId == table.Player2Id));
            tablePlayers.Add(_db.Players.ToList().Find(
                x => x.PlayerTournamentId == tournamentId && 
                x.PlayerId == table.Player3Id));
            tablePlayers.Add(_db.Players.ToList().Find(
                x => x.PlayerTournamentId == tournamentId && 
                x.PlayerId == table.Player4Id));
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

        public void UpdatePlayerTeam(int tournamentId, int playerId, int newTeamId)
        {
            _db.Players.ToList()
                .Find(x => x.PlayerTournamentId == tournamentId && x.PlayerId == playerId)
                .PlayerTeamId = newTeamId;
            _db.SaveChanges();
        }

        public void UpdatePlayerCountry(int tournamentId, int playerId, int countryId)
        {
            _db.Players.ToList()
                .Find(x => x.PlayerTournamentId == tournamentId && x.PlayerId == playerId)
                .PlayerCountryId = countryId;
            _db.SaveChanges();
        }

        #endregion

        #region Table

        public List<DBTable> GetTournamentTables(int tournamentId)
        {
            return _db.Tables.ToList().FindAll(x => x.TableTournamentId == tournamentId);
        }

        public void AddTables(List<DBTable> tables, List<DBHand> hands)
        {
            _db.Tables.AddRange(tables);
            _db.Hands.AddRange(hands);
            _db.SaveChanges();
        }

        public DBTable GetTable(int tournamentId, int roundId, int tableId)
        {
            return _db.Tables.ToList().Find(x => x.TableTournamentId == tournamentId && x.TableRoundId == roundId && x.TableId == tableId);
        }

        public void UpdateTableAllPlayersPositions(DBTable table)
        {
            DBTable dbTable = _db.Tables.ToList()
                .Find(x => x.TableTournamentId == table.TableTournamentId && x.TableId == table.TableId);
            dbTable.PlayerEastId = table.PlayerEastId;
            dbTable.PlayerSouthId = table.PlayerSouthId;
            dbTable.PlayerWestId = table.PlayerWestId;
            dbTable.PlayerNorthId = table.PlayerNorthId;
            _db.SaveChanges();
        }

        public void UpdateTableAllPlayersTotalScores(DBTable table)
        {
            DBTable dbTable = _db.Tables.ToList()
                .Find(x => x.TableTournamentId == table.TableTournamentId && x.TableId == table.TableId);
            dbTable.PlayerEastScore = table.PlayerEastScore;
            dbTable.PlayerSouthScore = table.PlayerSouthScore;
            dbTable.PlayerWestScore = table.PlayerWestScore;
            dbTable.PlayerNorthScore = table.PlayerNorthScore;
            _db.SaveChanges();
        }

        public void UpdateTableAllPlayersPoints(DBTable table)
        {
            DBTable dbTable = _db.Tables.ToList()
                .Find(x => x.TableTournamentId == table.TableTournamentId && x.TableId == table.TableId);
            dbTable.PlayerEastPoints = table.PlayerEastPoints;
            dbTable.PlayerSouthPoints = table.PlayerSouthPoints;
            dbTable.PlayerWestPoints = table.PlayerWestPoints;
            dbTable.PlayerNorthPoints = table.PlayerNorthPoints;
            _db.SaveChanges();
        }

        #endregion

        #region Hands

        public List<DBHand> GetTournamentHands(int tournamentId)
        {
            return _db.Hands.ToList().FindAll(x => x.HandTournamentId == tournamentId);
        }

        public List<DBHand> GetTableHands(int tournamentId, int roundId, int tableId)
        {
            return _db.Hands.ToList().FindAll(x => x.HandTournamentId == tournamentId && x.HandRoundId == roundId && x.HandTableId == tableId);
        }

        public void UpdateHandWinnerId(DBHand hand, string newWinnerPlayerId)
        {
            DBHand dbHand = _db.Hands.ToList().Find(x => x.HandTournamentId == hand.HandTournamentId
            && x.HandTableId == hand.HandTableId && x.HandRoundId == hand.HandRoundId && x.HandId == hand.HandId);
            dbHand.PlayerWinnerId = newWinnerPlayerId;
            _db.SaveChanges();
        }

        public void UpdateHandLooserId(DBHand hand, string newLooserPlayerId)
        {
            DBHand dbHand = _db.Hands.ToList().Find(x => x.HandTournamentId == hand.HandTournamentId
            && x.HandTableId == hand.HandTableId && x.HandRoundId == hand.HandRoundId && x.HandId == hand.HandId);
            dbHand.PlayerLooserId = newLooserPlayerId;
            _db.SaveChanges();
        }

        public void UpdateHandScore(DBHand hand, string newHandScore)
        {
            DBHand dbHand = _db.Hands.ToList().Find(x => x.HandTournamentId == hand.HandTournamentId
            && x.HandTableId == hand.HandTableId && x.HandRoundId == hand.HandRoundId && x.HandId == hand.HandId);
            dbHand.HandScore = newHandScore;
            _db.SaveChanges();
        }

        public void UpdateHandIsChickenHand(DBHand hand, bool newIsChickenHand)
        {
            DBHand dbHand = _db.Hands.ToList().Find(x => x.HandTournamentId == hand.HandTournamentId
            && x.HandTableId == hand.HandTableId  && x.HandRoundId == hand.HandRoundId  && x.HandId == hand.HandId);
            dbHand.IsChickenHand = newIsChickenHand;
            _db.SaveChanges();
        }

        public void UpdateHandPlayerEastPenalty(DBHand hand, string newPlayerEastPenaltyValue)
        {
            DBHand dbHand = _db.Hands.ToList().Find(x => x.HandTournamentId == hand.HandTournamentId
            && x.HandTableId == hand.HandTableId && x.HandRoundId == hand.HandRoundId && x.HandId == hand.HandId);
            dbHand.PlayerEastPenalty = newPlayerEastPenaltyValue;
            _db.SaveChanges();
        }

        public void UpdateHandPlayerSouthPenalty(DBHand hand, string newPlayerSouthPenaltyValue)
        {
            DBHand dbHand = _db.Hands.ToList().Find(x => x.HandTournamentId == hand.HandTournamentId
            && x.HandTableId == hand.HandTableId && x.HandRoundId == hand.HandRoundId && x.HandId == hand.HandId);
            dbHand.PlayerSouthPenalty = newPlayerSouthPenaltyValue;
            _db.SaveChanges();
        }

        public void UpdateHandPlayerWestPenalty(DBHand hand, string newPlayerWestPenaltyValue)
        {
            DBHand dbHand = _db.Hands.ToList().Find(x => x.HandTournamentId == hand.HandTournamentId
            && x.HandTableId == hand.HandTableId && x.HandRoundId == hand.HandRoundId && x.HandId == hand.HandId);
            dbHand.PlayerWestPenalty = newPlayerWestPenaltyValue;
            _db.SaveChanges();
        }

        public void UpdateHandPlayerNorthPenalty(DBHand hand, string newPlayerNorthPenaltyValue)
        {
            DBHand dbHand = _db.Hands.ToList().Find(x => x.HandTournamentId == hand.HandTournamentId
            && x.HandTableId == hand.HandTableId && x.HandRoundId == hand.HandRoundId && x.HandId == hand.HandId);
            dbHand.PlayerNorthPenalty = newPlayerNorthPenaltyValue;
            _db.SaveChanges();
        }

        #endregion

        #region Teams

        public List<DBTeam> GetTournamentTeams(int tournamentId)
        {
            return _db.Teams.ToList().FindAll(x => x.TeamTournamentId == tournamentId);
        }

        public List<string> GetTeamsNames(int tournamentId)
        {
            return _db.Teams.ToList().FindAll(x => x.TeamTournamentId == tournamentId)
                .Select(x => x.TeamName).ToList();
        }

        public string GetTeamName(int tournamentId, int teamId)
        {
            DBTeam team = _db.Teams.ToList().Find(x => x.TeamTournamentId == tournamentId && x.TeamId == teamId);
            if (team == null)
                return "";
            else
                return team.TeamName;
        }

        public int GetTeamId(int tournamentId, string teamName)
        {
            DBTeam team = _db.Teams.ToList().Find(x => x.TeamTournamentId == tournamentId && x.TeamName == teamName);
            if (team == null)
                return 0;
            else
                return team.TeamId;
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

        public List<string> GetCountriesNames()
        {
            EnsureThereAreCountries();
            return _db.Countries.Select(x => x.CountryName).ToList();
        }

        public string GetCountryName(int countryId)
        {
            EnsureThereAreCountries();
            DBCountry country = _db.Countries.ToList().Find(x => x.CountryId == countryId);
            if (country == null)
                return "";
            else
                return country.CountryName;
        }

        public int GetCountryId(string countryName)
        {
            EnsureThereAreCountries();
            DBCountry country = _db.Countries.ToList().Find(x => x.CountryName == countryName);
            if (country == null)
                return 0;
            else
                return country.CountryId;
        }

        public string GetCountryImageUrl(int countryId)
        {
            EnsureThereAreCountries();
            DBCountry country = _db.Countries.ToList().Find(x => x.CountryId == countryId);
            if (country == null)
                return "";
            else
                return country.CountryImageUrl;
        }

        private void EnsureThereAreCountries()
        {
            //http://www.geonames.org/countries/

            if (_db.Countries.Count() == 0)
            {
                _db.Countries.Add(new DBCountry(1, "Andorra", string.Empty));
                _db.Countries.Add(new DBCountry(2, "United Arab Emirates", string.Empty));
                _db.Countries.Add(new DBCountry(3, "Afghanistan", string.Empty));
                _db.Countries.Add(new DBCountry(4, "Antigua and Barbuda", string.Empty));
                _db.Countries.Add(new DBCountry(5, "Anguilla", string.Empty));
                _db.Countries.Add(new DBCountry(6, "Albania", string.Empty));
                _db.Countries.Add(new DBCountry(7, "Armenia", string.Empty));
                _db.Countries.Add(new DBCountry(8, "Netherlands Antilles", string.Empty));
                _db.Countries.Add(new DBCountry(9, "Angola", string.Empty));
                _db.Countries.Add(new DBCountry(10, "Antarctica", string.Empty));
                _db.Countries.Add(new DBCountry(11, "Argentina", string.Empty));
                _db.Countries.Add(new DBCountry(12, "American Samoa", string.Empty));
                _db.Countries.Add(new DBCountry(13, "Austria", string.Empty));
                _db.Countries.Add(new DBCountry(14, "Australia", string.Empty));
                _db.Countries.Add(new DBCountry(15, "Aruba", string.Empty));
                _db.Countries.Add(new DBCountry(16, "Åland", string.Empty));
                _db.Countries.Add(new DBCountry(17, "Azerbaijan", string.Empty));
                _db.Countries.Add(new DBCountry(18, "Bosnia and Herzegovina", string.Empty));
                _db.Countries.Add(new DBCountry(19, "Barbados", string.Empty));
                _db.Countries.Add(new DBCountry(20, "Bangladesh", string.Empty));
                _db.Countries.Add(new DBCountry(21, "Belgium", "http://www.mahjongmadrid.com/wp-content/uploads/2016/05/Belgium-32.png"));
                _db.Countries.Add(new DBCountry(22, "Burkina Faso", string.Empty));
                _db.Countries.Add(new DBCountry(23, "Bulgaria", string.Empty));
                _db.Countries.Add(new DBCountry(24, "Bahrain", string.Empty));
                _db.Countries.Add(new DBCountry(25, "Burundi", string.Empty));
                _db.Countries.Add(new DBCountry(26, "Benin", string.Empty));
                _db.Countries.Add(new DBCountry(27, "Saint Barthélemy", string.Empty));
                _db.Countries.Add(new DBCountry(28, "Bermuda", string.Empty));
                _db.Countries.Add(new DBCountry(29, "Brunei", string.Empty));
                _db.Countries.Add(new DBCountry(30, "Bolivia", string.Empty));
                _db.Countries.Add(new DBCountry(31, "Bonaire", string.Empty));
                _db.Countries.Add(new DBCountry(32, "Brazil", string.Empty));
                _db.Countries.Add(new DBCountry(33, "Bahamas", string.Empty));
                _db.Countries.Add(new DBCountry(34, "Bhutan", string.Empty));
                _db.Countries.Add(new DBCountry(35, "Bouvet Island", string.Empty));
                _db.Countries.Add(new DBCountry(36, "Botswana", string.Empty));
                _db.Countries.Add(new DBCountry(37, "Belarus", string.Empty));
                _db.Countries.Add(new DBCountry(38, "Belize", string.Empty));
                _db.Countries.Add(new DBCountry(39, "Canada", string.Empty));
                _db.Countries.Add(new DBCountry(40, "Cocos [Keeling] Islands", string.Empty));
                _db.Countries.Add(new DBCountry(41, "Democratic Republic of the Congo", string.Empty));
                _db.Countries.Add(new DBCountry(42, "Central African Republic", string.Empty));
                _db.Countries.Add(new DBCountry(43, "Republic of the Congo", string.Empty));
                _db.Countries.Add(new DBCountry(44, "Switzerland", "http://www.mahjongmadrid.com/wp-content/uploads/2016/05/Switzerland-32.png"));
                _db.Countries.Add(new DBCountry(45, "Ivory Coast", string.Empty));
                _db.Countries.Add(new DBCountry(46, "Cook Islands", string.Empty));
                _db.Countries.Add(new DBCountry(47, "Chile", string.Empty));
                _db.Countries.Add(new DBCountry(48, "Cameroon", string.Empty));
                _db.Countries.Add(new DBCountry(49, "China", string.Empty));
                _db.Countries.Add(new DBCountry(50, "Colombia", string.Empty));
                _db.Countries.Add(new DBCountry(51, "Costa Rica", string.Empty));
                _db.Countries.Add(new DBCountry(52, "Serbia and Montenegro", string.Empty));
                _db.Countries.Add(new DBCountry(53, "Cuba", string.Empty));
                _db.Countries.Add(new DBCountry(54, "Cape Verde", string.Empty));
                _db.Countries.Add(new DBCountry(55, "Curacao", string.Empty));
                _db.Countries.Add(new DBCountry(56, "Christmas Island", string.Empty));
                _db.Countries.Add(new DBCountry(57, "Cyprus", string.Empty));
                _db.Countries.Add(new DBCountry(58, "Czechia", string.Empty));
                _db.Countries.Add(new DBCountry(59, "Germany", "http://www.mahjongmadrid.com/wp-content/uploads/2016/05/Flag-of-Germany-32.png"));
                _db.Countries.Add(new DBCountry(60, "Djibouti", string.Empty));
                _db.Countries.Add(new DBCountry(61, "Denmark", "http://www.mahjongmadrid.com/wp-content/uploads/2016/05/Denmark-32.png"));
                _db.Countries.Add(new DBCountry(62, "Dominica", string.Empty));
                _db.Countries.Add(new DBCountry(63, "Dominican Republic", string.Empty));
                _db.Countries.Add(new DBCountry(64, "Algeria", string.Empty));
                _db.Countries.Add(new DBCountry(65, "Ecuador", string.Empty));
                _db.Countries.Add(new DBCountry(66, "Estonia", string.Empty));
                _db.Countries.Add(new DBCountry(67, "Egypt", string.Empty));
                _db.Countries.Add(new DBCountry(68, "Western Sahara", string.Empty));
                _db.Countries.Add(new DBCountry(69, "Eritrea", string.Empty));
                _db.Countries.Add(new DBCountry(70, "Spain", "http://www.mahjongmadrid.com/wp-content/uploads/2016/05/Flag-of-Spain-32.png"));
                _db.Countries.Add(new DBCountry(71, "Ethiopia", string.Empty));
                _db.Countries.Add(new DBCountry(72, "Finland", string.Empty));
                _db.Countries.Add(new DBCountry(73, "Fiji", string.Empty));
                _db.Countries.Add(new DBCountry(74, "Falkland Islands", string.Empty));
                _db.Countries.Add(new DBCountry(75, "Micronesia", string.Empty));
                _db.Countries.Add(new DBCountry(76, "Faroe Islands", string.Empty));
                _db.Countries.Add(new DBCountry(77, "France", "http://www.mahjongmadrid.com/wp-content/uploads/2016/05/France-32.png"));
                _db.Countries.Add(new DBCountry(78, "Gabon", string.Empty));
                _db.Countries.Add(new DBCountry(79, "United Kingdom", string.Empty));
                _db.Countries.Add(new DBCountry(80, "Grenada", string.Empty));
                _db.Countries.Add(new DBCountry(81, "Georgia", string.Empty));
                _db.Countries.Add(new DBCountry(82, "French Guiana", string.Empty));
                _db.Countries.Add(new DBCountry(83, "Guernsey", string.Empty));
                _db.Countries.Add(new DBCountry(84, "Ghana", string.Empty));
                _db.Countries.Add(new DBCountry(85, "Gibraltar", string.Empty));
                _db.Countries.Add(new DBCountry(86, "Greenland", string.Empty));
                _db.Countries.Add(new DBCountry(87, "Gambia", string.Empty));
                _db.Countries.Add(new DBCountry(88, "Guinea", string.Empty));
                _db.Countries.Add(new DBCountry(89, "Guadeloupe", string.Empty));
                _db.Countries.Add(new DBCountry(90, "Equatorial Guinea", string.Empty));
                _db.Countries.Add(new DBCountry(91, "Greece", string.Empty));
                _db.Countries.Add(new DBCountry(92, "South Georgia and the South Sandwich Islands", string.Empty));
                _db.Countries.Add(new DBCountry(93, "Guatemala", string.Empty));
                _db.Countries.Add(new DBCountry(94, "Guam", string.Empty));
                _db.Countries.Add(new DBCountry(95, "Guinea-Bissau", string.Empty));
                _db.Countries.Add(new DBCountry(96, "Guyana", string.Empty));
                _db.Countries.Add(new DBCountry(97, "Hong Kong", string.Empty));
                _db.Countries.Add(new DBCountry(98, "Heard Island and McDonald Islands", string.Empty));
                _db.Countries.Add(new DBCountry(99, "Honduras", string.Empty));
                _db.Countries.Add(new DBCountry(100, "Croatia", string.Empty));
                _db.Countries.Add(new DBCountry(101, "Haiti", string.Empty));
                _db.Countries.Add(new DBCountry(102, "Hungary", string.Empty));
                _db.Countries.Add(new DBCountry(103, "Indonesia", string.Empty));
                _db.Countries.Add(new DBCountry(104, "Ireland", string.Empty));
                _db.Countries.Add(new DBCountry(105, "Israel", string.Empty));
                _db.Countries.Add(new DBCountry(106, "Isle of Man", string.Empty));
                _db.Countries.Add(new DBCountry(107, "India", string.Empty));
                _db.Countries.Add(new DBCountry(108, "British Indian Ocean Territory", string.Empty));
                _db.Countries.Add(new DBCountry(109, "Iraq", string.Empty));
                _db.Countries.Add(new DBCountry(110, "Iran", string.Empty));
                _db.Countries.Add(new DBCountry(111, "Iceland", string.Empty));
                _db.Countries.Add(new DBCountry(112, "Italy", "http://www.mahjongmadrid.com/wp-content/uploads/2016/05/Flag-of-Italy-32.png"));
                _db.Countries.Add(new DBCountry(113, "Jersey", string.Empty));
                _db.Countries.Add(new DBCountry(114, "Jamaica", string.Empty));
                _db.Countries.Add(new DBCountry(115, "Jordan", string.Empty));
                _db.Countries.Add(new DBCountry(116, "Japan", string.Empty));
                _db.Countries.Add(new DBCountry(117, "Kenya", string.Empty));
                _db.Countries.Add(new DBCountry(118, "Kyrgyzstan", string.Empty));
                _db.Countries.Add(new DBCountry(119, "Cambodia", string.Empty));
                _db.Countries.Add(new DBCountry(120, "Kiribati", string.Empty));
                _db.Countries.Add(new DBCountry(121, "Comoros", string.Empty));
                _db.Countries.Add(new DBCountry(122, "Saint Kitts and Nevis", string.Empty));
                _db.Countries.Add(new DBCountry(123, "North Korea", string.Empty));
                _db.Countries.Add(new DBCountry(124, "South Korea", string.Empty));
                _db.Countries.Add(new DBCountry(125, "Kuwait", string.Empty));
                _db.Countries.Add(new DBCountry(126, "Cayman Islands", string.Empty));
                _db.Countries.Add(new DBCountry(127, "Kazakhstan", string.Empty));
                _db.Countries.Add(new DBCountry(128, "Laos", string.Empty));
                _db.Countries.Add(new DBCountry(129, "Lebanon", string.Empty));
                _db.Countries.Add(new DBCountry(130, "Saint Lucia", string.Empty));
                _db.Countries.Add(new DBCountry(131, "Liechtenstein", string.Empty));
                _db.Countries.Add(new DBCountry(132, "Sri Lanka", string.Empty));
                _db.Countries.Add(new DBCountry(133, "Liberia", string.Empty));
                _db.Countries.Add(new DBCountry(134, "Lesotho", string.Empty));
                _db.Countries.Add(new DBCountry(135, "Lithuania", string.Empty));
                _db.Countries.Add(new DBCountry(136, "Luxembourg", string.Empty));
                _db.Countries.Add(new DBCountry(137, "Latvia", string.Empty));
                _db.Countries.Add(new DBCountry(138, "Libya", string.Empty));
                _db.Countries.Add(new DBCountry(139, "Morocco", string.Empty));
                _db.Countries.Add(new DBCountry(140, "Monaco", string.Empty));
                _db.Countries.Add(new DBCountry(141, "Moldova", string.Empty));
                _db.Countries.Add(new DBCountry(142, "Montenegro", string.Empty));
                _db.Countries.Add(new DBCountry(143, "Saint Martin", string.Empty));
                _db.Countries.Add(new DBCountry(144, "Madagascar", string.Empty));
                _db.Countries.Add(new DBCountry(145, "Marshall Islands", string.Empty));
                _db.Countries.Add(new DBCountry(146, "Macedonia", string.Empty));
                _db.Countries.Add(new DBCountry(147, "Mali", string.Empty));
                _db.Countries.Add(new DBCountry(148, "Myanmar [Burma]", string.Empty));
                _db.Countries.Add(new DBCountry(149, "Mongolia", string.Empty));
                _db.Countries.Add(new DBCountry(150, "Macao", string.Empty));
                _db.Countries.Add(new DBCountry(151, "Northern Mariana Islands", string.Empty));
                _db.Countries.Add(new DBCountry(152, "Martinique", string.Empty));
                _db.Countries.Add(new DBCountry(153, "Mauritania", string.Empty));
                _db.Countries.Add(new DBCountry(154, "Montserrat", string.Empty));
                _db.Countries.Add(new DBCountry(155, "Malta", string.Empty));
                _db.Countries.Add(new DBCountry(156, "Mauritius", string.Empty));
                _db.Countries.Add(new DBCountry(157, "Maldives", string.Empty));
                _db.Countries.Add(new DBCountry(158, "Malawi", string.Empty));
                _db.Countries.Add(new DBCountry(159, "Mexico", string.Empty));
                _db.Countries.Add(new DBCountry(160, "Malaysia", string.Empty));
                _db.Countries.Add(new DBCountry(161, "Mozambique", string.Empty));
                _db.Countries.Add(new DBCountry(162, "Namibia", string.Empty));
                _db.Countries.Add(new DBCountry(163, "New Caledonia", string.Empty));
                _db.Countries.Add(new DBCountry(164, "Niger", string.Empty));
                _db.Countries.Add(new DBCountry(165, "Norfolk Island", string.Empty));
                _db.Countries.Add(new DBCountry(166, "Nigeria", string.Empty));
                _db.Countries.Add(new DBCountry(167, "Nicaragua", string.Empty));
                _db.Countries.Add(new DBCountry(168, "Netherlands", "http://www.mahjongmadrid.com/wp-content/uploads/2016/05/Netherlands-32.png"));
                _db.Countries.Add(new DBCountry(169, "Norway", string.Empty));
                _db.Countries.Add(new DBCountry(170, "Nepal", string.Empty));
                _db.Countries.Add(new DBCountry(171, "Nauru", string.Empty));
                _db.Countries.Add(new DBCountry(172, "Niue", string.Empty));
                _db.Countries.Add(new DBCountry(173, "New Zealand", string.Empty));
                _db.Countries.Add(new DBCountry(174, "Oman", string.Empty));
                _db.Countries.Add(new DBCountry(175, "Panama", string.Empty));
                _db.Countries.Add(new DBCountry(176, "Peru", string.Empty));
                _db.Countries.Add(new DBCountry(177, "French Polynesia", string.Empty));
                _db.Countries.Add(new DBCountry(178, "Papua New Guinea", string.Empty));
                _db.Countries.Add(new DBCountry(179, "Philippines", string.Empty));
                _db.Countries.Add(new DBCountry(180, "Pakistan", string.Empty));
                _db.Countries.Add(new DBCountry(181, "Poland", string.Empty));
                _db.Countries.Add(new DBCountry(182, "Saint Pierre and Miquelon", string.Empty));
                _db.Countries.Add(new DBCountry(183, "Pitcairn Islands", string.Empty));
                _db.Countries.Add(new DBCountry(184, "Puerto Rico", string.Empty));
                _db.Countries.Add(new DBCountry(185, "Palestine", string.Empty));
                _db.Countries.Add(new DBCountry(186, "Portugal", "http://www.mahjongmadrid.com/wp-content/uploads/2016/05/Portugal-32.png"));
                _db.Countries.Add(new DBCountry(187, "Palau", string.Empty));
                _db.Countries.Add(new DBCountry(188, "Paraguay", string.Empty));
                _db.Countries.Add(new DBCountry(189, "Qatar", string.Empty));
                _db.Countries.Add(new DBCountry(190, "Réunion", string.Empty));
                _db.Countries.Add(new DBCountry(191, "Romania", string.Empty));
                _db.Countries.Add(new DBCountry(192, "Serbia", string.Empty));
                _db.Countries.Add(new DBCountry(193, "Russia", "http://www.mahjongmadrid.com/wp-content/uploads/2017/03/rsz_russia.png"));
                _db.Countries.Add(new DBCountry(194, "Rwanda", string.Empty));
                _db.Countries.Add(new DBCountry(195, "Saudi Arabia", string.Empty));
                _db.Countries.Add(new DBCountry(196, "Solomon Islands", string.Empty));
                _db.Countries.Add(new DBCountry(197, "Seychelles", string.Empty));
                _db.Countries.Add(new DBCountry(198, "Sudan", string.Empty));
                _db.Countries.Add(new DBCountry(199, "Sweden", string.Empty));
                _db.Countries.Add(new DBCountry(200, "Singapore", string.Empty));
                _db.Countries.Add(new DBCountry(201, "Saint Helena", string.Empty));
                _db.Countries.Add(new DBCountry(202, "Slovenia", string.Empty));
                _db.Countries.Add(new DBCountry(203, "Svalbard and Jan Mayen", string.Empty));
                _db.Countries.Add(new DBCountry(204, "Slovakia", string.Empty));
                _db.Countries.Add(new DBCountry(205, "Sierra Leone", string.Empty));
                _db.Countries.Add(new DBCountry(206, "San Marino", string.Empty));
                _db.Countries.Add(new DBCountry(207, "Senegal", string.Empty));
                _db.Countries.Add(new DBCountry(208, "Somalia", string.Empty));
                _db.Countries.Add(new DBCountry(209, "Suriname", string.Empty));
                _db.Countries.Add(new DBCountry(210, "South Sudan", string.Empty));
                _db.Countries.Add(new DBCountry(211, "São Tomé and Príncipe", string.Empty));
                _db.Countries.Add(new DBCountry(212, "El Salvador", string.Empty));
                _db.Countries.Add(new DBCountry(213, "Sint Maarten", string.Empty));
                _db.Countries.Add(new DBCountry(214, "Syria", string.Empty));
                _db.Countries.Add(new DBCountry(215, "Swaziland", string.Empty));
                _db.Countries.Add(new DBCountry(216, "Turks and Caicos Islands", string.Empty));
                _db.Countries.Add(new DBCountry(217, "Chad", string.Empty));
                _db.Countries.Add(new DBCountry(218, "French Southern Territories", string.Empty));
                _db.Countries.Add(new DBCountry(219, "Togo", string.Empty));
                _db.Countries.Add(new DBCountry(220, "Thailand", string.Empty));
                _db.Countries.Add(new DBCountry(221, "Tajikistan", string.Empty));
                _db.Countries.Add(new DBCountry(222, "Tokelau", string.Empty));
                _db.Countries.Add(new DBCountry(223, "East Timor", string.Empty));
                _db.Countries.Add(new DBCountry(224, "Turkmenistan", string.Empty));
                _db.Countries.Add(new DBCountry(225, "Tunisia", string.Empty));
                _db.Countries.Add(new DBCountry(226, "Tonga", string.Empty));
                _db.Countries.Add(new DBCountry(227, "Turkey", string.Empty));
                _db.Countries.Add(new DBCountry(228, "Trinidad and Tobago", string.Empty));
                _db.Countries.Add(new DBCountry(229, "Tuvalu", string.Empty));
                _db.Countries.Add(new DBCountry(230, "Taiwan", string.Empty));
                _db.Countries.Add(new DBCountry(231, "Tanzania", string.Empty));
                _db.Countries.Add(new DBCountry(232, "Ukraine", string.Empty));
                _db.Countries.Add(new DBCountry(233, "Uganda", string.Empty));
                _db.Countries.Add(new DBCountry(234, "U.S. Minor Outlying Islands", string.Empty));
                _db.Countries.Add(new DBCountry(235, "United States", string.Empty));
                _db.Countries.Add(new DBCountry(236, "Uruguay", string.Empty));
                _db.Countries.Add(new DBCountry(237, "Uzbekistan", string.Empty));
                _db.Countries.Add(new DBCountry(238, "Vatican City", string.Empty));
                _db.Countries.Add(new DBCountry(239, "Saint Vincent and the Grenadines", string.Empty));
                _db.Countries.Add(new DBCountry(240, "Venezuela", string.Empty));
                _db.Countries.Add(new DBCountry(241, "British Virgin Islands", string.Empty));
                _db.Countries.Add(new DBCountry(242, "U.S. Virgin Islands", string.Empty));
                _db.Countries.Add(new DBCountry(243, "Vietnam", string.Empty));
                _db.Countries.Add(new DBCountry(244, "Vanuatu", string.Empty));
                _db.Countries.Add(new DBCountry(245, "Wallis and Futuna", string.Empty));
                _db.Countries.Add(new DBCountry(246, "Samoa", string.Empty));
                _db.Countries.Add(new DBCountry(247, "Kosovo", string.Empty));
                _db.Countries.Add(new DBCountry(248, "Yemen", string.Empty));
                _db.Countries.Add(new DBCountry(249, "Mayotte", string.Empty));
                _db.Countries.Add(new DBCountry(250, "South Africa", string.Empty));
                _db.Countries.Add(new DBCountry(251, "Zambia", string.Empty));
                _db.Countries.Add(new DBCountry(252, "Zimbabwe", string.Empty));

                _db.SaveChanges();
            }
        }

        #endregion
    }
}
