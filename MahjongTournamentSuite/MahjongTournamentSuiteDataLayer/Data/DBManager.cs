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
            dbTable.PlayerEastTotalScore = table.PlayerEastTotalScore;
            dbTable.PlayerSouthTotalScore = table.PlayerSouthTotalScore;
            dbTable.PlayerWestTotalScore = table.PlayerWestTotalScore;
            dbTable.PlayerNorthTotalScore = table.PlayerNorthTotalScore;
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

        private void EnsureThereAreCountries()
        {
            //http://www.geonames.org/countries/

            if (_db.Countries.Count() == 0)
            {
                _db.Countries.Add(new DBCountry(1, "Andorra"));
                _db.Countries.Add(new DBCountry(2, "United Arab Emirates"));
                _db.Countries.Add(new DBCountry(3, "Afghanistan"));
                _db.Countries.Add(new DBCountry(4, "Antigua and Barbuda"));
                _db.Countries.Add(new DBCountry(5, "Anguilla"));
                _db.Countries.Add(new DBCountry(6, "Albania"));
                _db.Countries.Add(new DBCountry(7, "Armenia"));
                _db.Countries.Add(new DBCountry(8, "Netherlands Antilles"));
                _db.Countries.Add(new DBCountry(9, "Angola"));
                _db.Countries.Add(new DBCountry(10, "Antarctica"));
                _db.Countries.Add(new DBCountry(11, "Argentina"));
                _db.Countries.Add(new DBCountry(12, "American Samoa"));
                _db.Countries.Add(new DBCountry(13, "Austria"));
                _db.Countries.Add(new DBCountry(14, "Australia"));
                _db.Countries.Add(new DBCountry(15, "Aruba"));
                _db.Countries.Add(new DBCountry(16, "Åland"));
                _db.Countries.Add(new DBCountry(17, "Azerbaijan"));
                _db.Countries.Add(new DBCountry(18, "Bosnia and Herzegovina"));
                _db.Countries.Add(new DBCountry(19, "Barbados"));
                _db.Countries.Add(new DBCountry(20, "Bangladesh"));
                _db.Countries.Add(new DBCountry(21, "Belgium"));
                _db.Countries.Add(new DBCountry(22, "Burkina Faso"));
                _db.Countries.Add(new DBCountry(23, "Bulgaria"));
                _db.Countries.Add(new DBCountry(24, "Bahrain"));
                _db.Countries.Add(new DBCountry(25, "Burundi"));
                _db.Countries.Add(new DBCountry(26, "Benin"));
                _db.Countries.Add(new DBCountry(27, "Saint Barthélemy"));
                _db.Countries.Add(new DBCountry(28, "Bermuda"));
                _db.Countries.Add(new DBCountry(29, "Brunei"));
                _db.Countries.Add(new DBCountry(30, "Bolivia"));
                _db.Countries.Add(new DBCountry(31, "Bonaire"));
                _db.Countries.Add(new DBCountry(32, "Brazil"));
                _db.Countries.Add(new DBCountry(33, "Bahamas"));
                _db.Countries.Add(new DBCountry(34, "Bhutan"));
                _db.Countries.Add(new DBCountry(35, "Bouvet Island"));
                _db.Countries.Add(new DBCountry(36, "Botswana"));
                _db.Countries.Add(new DBCountry(37, "Belarus"));
                _db.Countries.Add(new DBCountry(38, "Belize"));
                _db.Countries.Add(new DBCountry(39, "Canada"));
                _db.Countries.Add(new DBCountry(40, "Cocos [Keeling] Islands"));
                _db.Countries.Add(new DBCountry(41, "Democratic Republic of the Congo"));
                _db.Countries.Add(new DBCountry(42, "Central African Republic"));
                _db.Countries.Add(new DBCountry(43, "Republic of the Congo"));
                _db.Countries.Add(new DBCountry(44, "Switzerland"));
                _db.Countries.Add(new DBCountry(45, "Ivory Coast"));
                _db.Countries.Add(new DBCountry(46, "Cook Islands"));
                _db.Countries.Add(new DBCountry(47, "Chile"));
                _db.Countries.Add(new DBCountry(48, "Cameroon"));
                _db.Countries.Add(new DBCountry(49, "China"));
                _db.Countries.Add(new DBCountry(50, "Colombia"));
                _db.Countries.Add(new DBCountry(51, "Costa Rica"));
                _db.Countries.Add(new DBCountry(52, "Serbia and Montenegro"));
                _db.Countries.Add(new DBCountry(53, "Cuba"));
                _db.Countries.Add(new DBCountry(54, "Cape Verde"));
                _db.Countries.Add(new DBCountry(55, "Curacao"));
                _db.Countries.Add(new DBCountry(56, "Christmas Island"));
                _db.Countries.Add(new DBCountry(57, "Cyprus"));
                _db.Countries.Add(new DBCountry(58, "Czechia"));
                _db.Countries.Add(new DBCountry(59, "Germany"));
                _db.Countries.Add(new DBCountry(60, "Djibouti"));
                _db.Countries.Add(new DBCountry(61, "Denmark"));
                _db.Countries.Add(new DBCountry(62, "Dominica"));
                _db.Countries.Add(new DBCountry(63, "Dominican Republic"));
                _db.Countries.Add(new DBCountry(64, "Algeria"));
                _db.Countries.Add(new DBCountry(65, "Ecuador"));
                _db.Countries.Add(new DBCountry(66, "Estonia"));
                _db.Countries.Add(new DBCountry(67, "Egypt"));
                _db.Countries.Add(new DBCountry(68, "Western Sahara"));
                _db.Countries.Add(new DBCountry(69, "Eritrea"));
                _db.Countries.Add(new DBCountry(70, "Spain"));
                _db.Countries.Add(new DBCountry(71, "Ethiopia"));
                _db.Countries.Add(new DBCountry(72, "Finland"));
                _db.Countries.Add(new DBCountry(73, "Fiji"));
                _db.Countries.Add(new DBCountry(74, "Falkland Islands"));
                _db.Countries.Add(new DBCountry(75, "Micronesia"));
                _db.Countries.Add(new DBCountry(76, "Faroe Islands"));
                _db.Countries.Add(new DBCountry(77, "France"));
                _db.Countries.Add(new DBCountry(78, "Gabon"));
                _db.Countries.Add(new DBCountry(79, "United Kingdom"));
                _db.Countries.Add(new DBCountry(80, "Grenada"));
                _db.Countries.Add(new DBCountry(81, "Georgia"));
                _db.Countries.Add(new DBCountry(82, "French Guiana"));
                _db.Countries.Add(new DBCountry(83, "Guernsey"));
                _db.Countries.Add(new DBCountry(84, "Ghana"));
                _db.Countries.Add(new DBCountry(85, "Gibraltar"));
                _db.Countries.Add(new DBCountry(86, "Greenland"));
                _db.Countries.Add(new DBCountry(87, "Gambia"));
                _db.Countries.Add(new DBCountry(88, "Guinea"));
                _db.Countries.Add(new DBCountry(89, "Guadeloupe"));
                _db.Countries.Add(new DBCountry(90, "Equatorial Guinea"));
                _db.Countries.Add(new DBCountry(91, "Greece"));
                _db.Countries.Add(new DBCountry(92, "South Georgia and the South Sandwich Islands"));
                _db.Countries.Add(new DBCountry(93, "Guatemala"));
                _db.Countries.Add(new DBCountry(94, "Guam"));
                _db.Countries.Add(new DBCountry(95, "Guinea-Bissau"));
                _db.Countries.Add(new DBCountry(96, "Guyana"));
                _db.Countries.Add(new DBCountry(97, "Hong Kong"));
                _db.Countries.Add(new DBCountry(98, "Heard Island and McDonald Islands"));
                _db.Countries.Add(new DBCountry(99, "Honduras"));
                _db.Countries.Add(new DBCountry(100, "Croatia"));
                _db.Countries.Add(new DBCountry(101, "Haiti"));
                _db.Countries.Add(new DBCountry(102, "Hungary"));
                _db.Countries.Add(new DBCountry(103, "Indonesia"));
                _db.Countries.Add(new DBCountry(104, "Ireland"));
                _db.Countries.Add(new DBCountry(105, "Israel"));
                _db.Countries.Add(new DBCountry(106, "Isle of Man"));
                _db.Countries.Add(new DBCountry(107, "India"));
                _db.Countries.Add(new DBCountry(108, "British Indian Ocean Territory"));
                _db.Countries.Add(new DBCountry(109, "Iraq"));
                _db.Countries.Add(new DBCountry(110, "Iran"));
                _db.Countries.Add(new DBCountry(111, "Iceland"));
                _db.Countries.Add(new DBCountry(112, "Italy"));
                _db.Countries.Add(new DBCountry(113, "Jersey"));
                _db.Countries.Add(new DBCountry(114, "Jamaica"));
                _db.Countries.Add(new DBCountry(115, "Jordan"));
                _db.Countries.Add(new DBCountry(116, "Japan"));
                _db.Countries.Add(new DBCountry(117, "Kenya"));
                _db.Countries.Add(new DBCountry(118, "Kyrgyzstan"));
                _db.Countries.Add(new DBCountry(119, "Cambodia"));
                _db.Countries.Add(new DBCountry(120, "Kiribati"));
                _db.Countries.Add(new DBCountry(121, "Comoros"));
                _db.Countries.Add(new DBCountry(122, "Saint Kitts and Nevis"));
                _db.Countries.Add(new DBCountry(123, "North Korea"));
                _db.Countries.Add(new DBCountry(124, "South Korea"));
                _db.Countries.Add(new DBCountry(125, "Kuwait"));
                _db.Countries.Add(new DBCountry(126, "Cayman Islands"));
                _db.Countries.Add(new DBCountry(127, "Kazakhstan"));
                _db.Countries.Add(new DBCountry(128, "Laos"));
                _db.Countries.Add(new DBCountry(129, "Lebanon"));
                _db.Countries.Add(new DBCountry(130, "Saint Lucia"));
                _db.Countries.Add(new DBCountry(131, "Liechtenstein"));
                _db.Countries.Add(new DBCountry(132, "Sri Lanka"));
                _db.Countries.Add(new DBCountry(133, "Liberia"));
                _db.Countries.Add(new DBCountry(134, "Lesotho"));
                _db.Countries.Add(new DBCountry(135, "Lithuania"));
                _db.Countries.Add(new DBCountry(136, "Luxembourg"));
                _db.Countries.Add(new DBCountry(137, "Latvia"));
                _db.Countries.Add(new DBCountry(138, "Libya"));
                _db.Countries.Add(new DBCountry(139, "Morocco"));
                _db.Countries.Add(new DBCountry(140, "Monaco"));
                _db.Countries.Add(new DBCountry(141, "Moldova"));
                _db.Countries.Add(new DBCountry(142, "Montenegro"));
                _db.Countries.Add(new DBCountry(143, "Saint Martin"));
                _db.Countries.Add(new DBCountry(144, "Madagascar"));
                _db.Countries.Add(new DBCountry(145, "Marshall Islands"));
                _db.Countries.Add(new DBCountry(146, "Macedonia"));
                _db.Countries.Add(new DBCountry(147, "Mali"));
                _db.Countries.Add(new DBCountry(148, "Myanmar [Burma]"));
                _db.Countries.Add(new DBCountry(149, "Mongolia"));
                _db.Countries.Add(new DBCountry(150, "Macao"));
                _db.Countries.Add(new DBCountry(151, "Northern Mariana Islands"));
                _db.Countries.Add(new DBCountry(152, "Martinique"));
                _db.Countries.Add(new DBCountry(153, "Mauritania"));
                _db.Countries.Add(new DBCountry(154, "Montserrat"));
                _db.Countries.Add(new DBCountry(155, "Malta"));
                _db.Countries.Add(new DBCountry(156, "Mauritius"));
                _db.Countries.Add(new DBCountry(157, "Maldives"));
                _db.Countries.Add(new DBCountry(158, "Malawi"));
                _db.Countries.Add(new DBCountry(159, "Mexico"));
                _db.Countries.Add(new DBCountry(160, "Malaysia"));
                _db.Countries.Add(new DBCountry(161, "Mozambique"));
                _db.Countries.Add(new DBCountry(162, "Namibia"));
                _db.Countries.Add(new DBCountry(163, "New Caledonia"));
                _db.Countries.Add(new DBCountry(164, "Niger"));
                _db.Countries.Add(new DBCountry(165, "Norfolk Island"));
                _db.Countries.Add(new DBCountry(166, "Nigeria"));
                _db.Countries.Add(new DBCountry(167, "Nicaragua"));
                _db.Countries.Add(new DBCountry(168, "Netherlands"));
                _db.Countries.Add(new DBCountry(169, "Norway"));
                _db.Countries.Add(new DBCountry(170, "Nepal"));
                _db.Countries.Add(new DBCountry(171, "Nauru"));
                _db.Countries.Add(new DBCountry(172, "Niue"));
                _db.Countries.Add(new DBCountry(173, "New Zealand"));
                _db.Countries.Add(new DBCountry(174, "Oman"));
                _db.Countries.Add(new DBCountry(175, "Panama"));
                _db.Countries.Add(new DBCountry(176, "Peru"));
                _db.Countries.Add(new DBCountry(177, "French Polynesia"));
                _db.Countries.Add(new DBCountry(178, "Papua New Guinea"));
                _db.Countries.Add(new DBCountry(179, "Philippines"));
                _db.Countries.Add(new DBCountry(180, "Pakistan"));
                _db.Countries.Add(new DBCountry(181, "Poland"));
                _db.Countries.Add(new DBCountry(182, "Saint Pierre and Miquelon"));
                _db.Countries.Add(new DBCountry(183, "Pitcairn Islands"));
                _db.Countries.Add(new DBCountry(184, "Puerto Rico"));
                _db.Countries.Add(new DBCountry(185, "Palestine"));
                _db.Countries.Add(new DBCountry(186, "Portugal"));
                _db.Countries.Add(new DBCountry(187, "Palau"));
                _db.Countries.Add(new DBCountry(188, "Paraguay"));
                _db.Countries.Add(new DBCountry(189, "Qatar"));
                _db.Countries.Add(new DBCountry(190, "Réunion"));
                _db.Countries.Add(new DBCountry(191, "Romania"));
                _db.Countries.Add(new DBCountry(192, "Serbia"));
                _db.Countries.Add(new DBCountry(193, "Russia"));
                _db.Countries.Add(new DBCountry(194, "Rwanda"));
                _db.Countries.Add(new DBCountry(195, "Saudi Arabia"));
                _db.Countries.Add(new DBCountry(196, "Solomon Islands"));
                _db.Countries.Add(new DBCountry(197, "Seychelles"));
                _db.Countries.Add(new DBCountry(198, "Sudan"));
                _db.Countries.Add(new DBCountry(199, "Sweden"));
                _db.Countries.Add(new DBCountry(200, "Singapore"));
                _db.Countries.Add(new DBCountry(201, "Saint Helena"));
                _db.Countries.Add(new DBCountry(202, "Slovenia"));
                _db.Countries.Add(new DBCountry(203, "Svalbard and Jan Mayen"));
                _db.Countries.Add(new DBCountry(204, "Slovakia"));
                _db.Countries.Add(new DBCountry(205, "Sierra Leone"));
                _db.Countries.Add(new DBCountry(206, "San Marino"));
                _db.Countries.Add(new DBCountry(207, "Senegal"));
                _db.Countries.Add(new DBCountry(208, "Somalia"));
                _db.Countries.Add(new DBCountry(209, "Suriname"));
                _db.Countries.Add(new DBCountry(210, "South Sudan"));
                _db.Countries.Add(new DBCountry(211, "São Tomé and Príncipe"));
                _db.Countries.Add(new DBCountry(212, "El Salvador"));
                _db.Countries.Add(new DBCountry(213, "Sint Maarten"));
                _db.Countries.Add(new DBCountry(214, "Syria"));
                _db.Countries.Add(new DBCountry(215, "Swaziland"));
                _db.Countries.Add(new DBCountry(216, "Turks and Caicos Islands"));
                _db.Countries.Add(new DBCountry(217, "Chad"));
                _db.Countries.Add(new DBCountry(218, "French Southern Territories"));
                _db.Countries.Add(new DBCountry(219, "Togo"));
                _db.Countries.Add(new DBCountry(220, "Thailand"));
                _db.Countries.Add(new DBCountry(221, "Tajikistan"));
                _db.Countries.Add(new DBCountry(222, "Tokelau"));
                _db.Countries.Add(new DBCountry(223, "East Timor"));
                _db.Countries.Add(new DBCountry(224, "Turkmenistan"));
                _db.Countries.Add(new DBCountry(225, "Tunisia"));
                _db.Countries.Add(new DBCountry(226, "Tonga"));
                _db.Countries.Add(new DBCountry(227, "Turkey"));
                _db.Countries.Add(new DBCountry(228, "Trinidad and Tobago"));
                _db.Countries.Add(new DBCountry(229, "Tuvalu"));
                _db.Countries.Add(new DBCountry(230, "Taiwan"));
                _db.Countries.Add(new DBCountry(231, "Tanzania"));
                _db.Countries.Add(new DBCountry(232, "Ukraine"));
                _db.Countries.Add(new DBCountry(233, "Uganda"));
                _db.Countries.Add(new DBCountry(234, "U.S. Minor Outlying Islands"));
                _db.Countries.Add(new DBCountry(235, "United States"));
                _db.Countries.Add(new DBCountry(236, "Uruguay"));
                _db.Countries.Add(new DBCountry(237, "Uzbekistan"));
                _db.Countries.Add(new DBCountry(238, "Vatican City"));
                _db.Countries.Add(new DBCountry(239, "Saint Vincent and the Grenadines"));
                _db.Countries.Add(new DBCountry(240, "Venezuela"));
                _db.Countries.Add(new DBCountry(241, "British Virgin Islands"));
                _db.Countries.Add(new DBCountry(242, "U.S. Virgin Islands"));
                _db.Countries.Add(new DBCountry(243, "Vietnam"));
                _db.Countries.Add(new DBCountry(244, "Vanuatu"));
                _db.Countries.Add(new DBCountry(245, "Wallis and Futuna"));
                _db.Countries.Add(new DBCountry(246, "Samoa"));
                _db.Countries.Add(new DBCountry(247, "Kosovo"));
                _db.Countries.Add(new DBCountry(248, "Yemen"));
                _db.Countries.Add(new DBCountry(249, "Mayotte"));
                _db.Countries.Add(new DBCountry(250, "South Africa"));
                _db.Countries.Add(new DBCountry(251, "Zambia"));
                _db.Countries.Add(new DBCountry(252, "Zimbabwe"));

                _db.SaveChanges();
            }
        }

        #endregion
    }
}
