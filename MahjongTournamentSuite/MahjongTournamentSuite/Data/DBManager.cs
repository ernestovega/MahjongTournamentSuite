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
            _db.Hands.RemoveRange(_db.Hands.ToList().FindAll( x => x.HandTournamentId == tournamentId));
            _db.Tables.RemoveRange(_db.Tables.ToList().FindAll( x => x.TableTournamentId == tournamentId));
            _db.Players.RemoveRange(_db.Players.ToList().FindAll( x => x.PlayerTournamentId == tournamentId));
            DBTournament tournament = _db.Tournaments.ToList().Find( x => x.TournamentId == tournamentId);
            if (tournament.IsTeams)
                _db.Teams.RemoveRange(_db.Teams.ToList().FindAll( x => x.TeamTournamentId == tournamentId));
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
            DBTable table = _db.Tables.ToList().Find(
                x => x.TableTournamentId == tournamentId && 
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

        public void UpdateTablePlayersPositions(DBTable table)
        {
            DBTable dbTable = _db.Tables.ToList().Find(x => x.TableTournamentId == table.TableTournamentId && x.TableId == table.TableId);
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
            DBHand dbHand = _db.Hands.ToList().Find(x => x.HandTournamentId == hand.HandTournamentId
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
                _db.Countries.AddRange(GetCountriesList());
                _db.SaveChanges();
            }
            return _db.Countries.ToList();
        }
        
        #endregion

        #region Countries

        private List<DBCountry> GetCountriesList()
        {
            //http://www.geonames.org/countries/
            List<DBCountry> countriesList = new List<DBCountry>();
            countriesList.Add(new DBCountry(1, "Andorra"));
            countriesList.Add(new DBCountry(2, "United Arab Emirates"));
            countriesList.Add(new DBCountry(3, "Afghanistan"));
            countriesList.Add(new DBCountry(4, "Antigua and Barbuda"));
            countriesList.Add(new DBCountry(5, "Anguilla"));
            countriesList.Add(new DBCountry(6, "Albania"));
            countriesList.Add(new DBCountry(7, "Armenia"));
            countriesList.Add(new DBCountry(8, "Netherlands Antilles"));
            countriesList.Add(new DBCountry(9, "Angola"));
            countriesList.Add(new DBCountry(10, "Antarctica"));
            countriesList.Add(new DBCountry(11, "Argentina"));
            countriesList.Add(new DBCountry(12, "American Samoa"));
            countriesList.Add(new DBCountry(13, "Austria"));
            countriesList.Add(new DBCountry(14, "Australia"));
            countriesList.Add(new DBCountry(15, "Aruba"));
            countriesList.Add(new DBCountry(16, "Åland"));
            countriesList.Add(new DBCountry(17, "Azerbaijan"));
            countriesList.Add(new DBCountry(18, "Bosnia and Herzegovina"));
            countriesList.Add(new DBCountry(19, "Barbados"));
            countriesList.Add(new DBCountry(20, "Bangladesh"));
            countriesList.Add(new DBCountry(21, "Belgium"));
            countriesList.Add(new DBCountry(22, "Burkina Faso"));
            countriesList.Add(new DBCountry(23, "Bulgaria"));
            countriesList.Add(new DBCountry(24, "Bahrain"));
            countriesList.Add(new DBCountry(25, "Burundi"));
            countriesList.Add(new DBCountry(26, "Benin"));
            countriesList.Add(new DBCountry(27, "Saint Barthélemy"));
            countriesList.Add(new DBCountry(28, "Bermuda"));
            countriesList.Add(new DBCountry(29, "Brunei"));
            countriesList.Add(new DBCountry(30, "Bolivia"));
            countriesList.Add(new DBCountry(31, "Bonaire"));
            countriesList.Add(new DBCountry(32, "Brazil"));
            countriesList.Add(new DBCountry(33, "Bahamas"));
            countriesList.Add(new DBCountry(34, "Bhutan"));
            countriesList.Add(new DBCountry(35, "Bouvet Island"));
            countriesList.Add(new DBCountry(36, "Botswana"));
            countriesList.Add(new DBCountry(37, "Belarus"));
            countriesList.Add(new DBCountry(38, "Belize"));
            countriesList.Add(new DBCountry(39, "Canada"));
            countriesList.Add(new DBCountry(40, "Cocos [Keeling] Islands"));
            countriesList.Add(new DBCountry(41, "Democratic Republic of the Congo"));
            countriesList.Add(new DBCountry(42, "Central African Republic"));
            countriesList.Add(new DBCountry(43, "Republic of the Congo"));
            countriesList.Add(new DBCountry(44, "Switzerland"));
            countriesList.Add(new DBCountry(45, "Ivory Coast"));
            countriesList.Add(new DBCountry(46, "Cook Islands"));
            countriesList.Add(new DBCountry(47, "Chile"));
            countriesList.Add(new DBCountry(48, "Cameroon"));
            countriesList.Add(new DBCountry(49, "China"));
            countriesList.Add(new DBCountry(50, "Colombia"));
            countriesList.Add(new DBCountry(51, "Costa Rica"));
            countriesList.Add(new DBCountry(52, "Serbia and Montenegro"));
            countriesList.Add(new DBCountry(53, "Cuba"));
            countriesList.Add(new DBCountry(54, "Cape Verde"));
            countriesList.Add(new DBCountry(55, "Curacao"));
            countriesList.Add(new DBCountry(56, "Christmas Island"));
            countriesList.Add(new DBCountry(57, "Cyprus"));
            countriesList.Add(new DBCountry(58, "Czechia"));
            countriesList.Add(new DBCountry(59, "Germany"));
            countriesList.Add(new DBCountry(60, "Djibouti"));
            countriesList.Add(new DBCountry(61, "Denmark"));
            countriesList.Add(new DBCountry(62, "Dominica"));
            countriesList.Add(new DBCountry(63, "Dominican Republic"));
            countriesList.Add(new DBCountry(64, "Algeria"));
            countriesList.Add(new DBCountry(65, "Ecuador"));
            countriesList.Add(new DBCountry(66, "Estonia"));
            countriesList.Add(new DBCountry(67, "Egypt"));
            countriesList.Add(new DBCountry(68, "Western Sahara"));
            countriesList.Add(new DBCountry(69, "Eritrea"));
            countriesList.Add(new DBCountry(70, "Spain"));
            countriesList.Add(new DBCountry(71, "Ethiopia"));
            countriesList.Add(new DBCountry(72, "Finland"));
            countriesList.Add(new DBCountry(73, "Fiji"));
            countriesList.Add(new DBCountry(74, "Falkland Islands"));
            countriesList.Add(new DBCountry(75, "Micronesia"));
            countriesList.Add(new DBCountry(76, "Faroe Islands"));
            countriesList.Add(new DBCountry(77, "France"));
            countriesList.Add(new DBCountry(78, "Gabon"));
            countriesList.Add(new DBCountry(79, "United Kingdom"));
            countriesList.Add(new DBCountry(80, "Grenada"));
            countriesList.Add(new DBCountry(81, "Georgia"));
            countriesList.Add(new DBCountry(82, "French Guiana"));
            countriesList.Add(new DBCountry(83, "Guernsey"));
            countriesList.Add(new DBCountry(84, "Ghana"));
            countriesList.Add(new DBCountry(85, "Gibraltar"));
            countriesList.Add(new DBCountry(86, "Greenland"));
            countriesList.Add(new DBCountry(87, "Gambia"));
            countriesList.Add(new DBCountry(88, "Guinea"));
            countriesList.Add(new DBCountry(89, "Guadeloupe"));
            countriesList.Add(new DBCountry(90, "Equatorial Guinea"));
            countriesList.Add(new DBCountry(91, "Greece"));
            countriesList.Add(new DBCountry(92, "South Georgia and the South Sandwich Islands"));
            countriesList.Add(new DBCountry(93, "Guatemala"));
            countriesList.Add(new DBCountry(94, "Guam"));
            countriesList.Add(new DBCountry(95, "Guinea-Bissau"));
            countriesList.Add(new DBCountry(96, "Guyana"));
            countriesList.Add(new DBCountry(97, "Hong Kong"));
            countriesList.Add(new DBCountry(98, "Heard Island and McDonald Islands"));
            countriesList.Add(new DBCountry(99, "Honduras"));
            countriesList.Add(new DBCountry(100, "Croatia"));
            countriesList.Add(new DBCountry(101, "Haiti"));
            countriesList.Add(new DBCountry(102, "Hungary"));
            countriesList.Add(new DBCountry(103, "Indonesia"));
            countriesList.Add(new DBCountry(104, "Ireland"));
            countriesList.Add(new DBCountry(105, "Israel"));
            countriesList.Add(new DBCountry(106, "Isle of Man"));
            countriesList.Add(new DBCountry(107, "India"));
            countriesList.Add(new DBCountry(108, "British Indian Ocean Territory"));
            countriesList.Add(new DBCountry(109, "Iraq"));
            countriesList.Add(new DBCountry(110, "Iran"));
            countriesList.Add(new DBCountry(111, "Iceland"));
            countriesList.Add(new DBCountry(112, "Italy"));
            countriesList.Add(new DBCountry(113, "Jersey"));
            countriesList.Add(new DBCountry(114, "Jamaica"));
            countriesList.Add(new DBCountry(115, "Jordan"));
            countriesList.Add(new DBCountry(116, "Japan"));
            countriesList.Add(new DBCountry(117, "Kenya"));
            countriesList.Add(new DBCountry(118, "Kyrgyzstan"));
            countriesList.Add(new DBCountry(119, "Cambodia"));
            countriesList.Add(new DBCountry(120, "Kiribati"));
            countriesList.Add(new DBCountry(121, "Comoros"));
            countriesList.Add(new DBCountry(122, "Saint Kitts and Nevis"));
            countriesList.Add(new DBCountry(123, "North Korea"));
            countriesList.Add(new DBCountry(124, "South Korea"));
            countriesList.Add(new DBCountry(125, "Kuwait"));
            countriesList.Add(new DBCountry(126, "Cayman Islands"));
            countriesList.Add(new DBCountry(127, "Kazakhstan"));
            countriesList.Add(new DBCountry(128, "Laos"));
            countriesList.Add(new DBCountry(129, "Lebanon"));
            countriesList.Add(new DBCountry(130, "Saint Lucia"));
            countriesList.Add(new DBCountry(131, "Liechtenstein"));
            countriesList.Add(new DBCountry(132, "Sri Lanka"));
            countriesList.Add(new DBCountry(133, "Liberia"));
            countriesList.Add(new DBCountry(134, "Lesotho"));
            countriesList.Add(new DBCountry(135, "Lithuania"));
            countriesList.Add(new DBCountry(136, "Luxembourg"));
            countriesList.Add(new DBCountry(137, "Latvia"));
            countriesList.Add(new DBCountry(138, "Libya"));
            countriesList.Add(new DBCountry(139, "Morocco"));
            countriesList.Add(new DBCountry(140, "Monaco"));
            countriesList.Add(new DBCountry(141, "Moldova"));
            countriesList.Add(new DBCountry(142, "Montenegro"));
            countriesList.Add(new DBCountry(143, "Saint Martin"));
            countriesList.Add(new DBCountry(144, "Madagascar"));
            countriesList.Add(new DBCountry(145, "Marshall Islands"));
            countriesList.Add(new DBCountry(146, "Macedonia"));
            countriesList.Add(new DBCountry(147, "Mali"));
            countriesList.Add(new DBCountry(148, "Myanmar [Burma]"));
            countriesList.Add(new DBCountry(149, "Mongolia"));
            countriesList.Add(new DBCountry(150, "Macao"));
            countriesList.Add(new DBCountry(151, "Northern Mariana Islands"));
            countriesList.Add(new DBCountry(152, "Martinique"));
            countriesList.Add(new DBCountry(153, "Mauritania"));
            countriesList.Add(new DBCountry(154, "Montserrat"));
            countriesList.Add(new DBCountry(155, "Malta"));
            countriesList.Add(new DBCountry(156, "Mauritius"));
            countriesList.Add(new DBCountry(157, "Maldives"));
            countriesList.Add(new DBCountry(158, "Malawi"));
            countriesList.Add(new DBCountry(159, "Mexico"));
            countriesList.Add(new DBCountry(160, "Malaysia"));
            countriesList.Add(new DBCountry(161, "Mozambique"));
            countriesList.Add(new DBCountry(162, "Namibia"));
            countriesList.Add(new DBCountry(163, "New Caledonia"));
            countriesList.Add(new DBCountry(164, "Niger"));
            countriesList.Add(new DBCountry(165, "Norfolk Island"));
            countriesList.Add(new DBCountry(166, "Nigeria"));
            countriesList.Add(new DBCountry(167, "Nicaragua"));
            countriesList.Add(new DBCountry(168, "Netherlands"));
            countriesList.Add(new DBCountry(169, "Norway"));
            countriesList.Add(new DBCountry(170, "Nepal"));
            countriesList.Add(new DBCountry(171, "Nauru"));
            countriesList.Add(new DBCountry(172, "Niue"));
            countriesList.Add(new DBCountry(173, "New Zealand"));
            countriesList.Add(new DBCountry(174, "Oman"));
            countriesList.Add(new DBCountry(175, "Panama"));
            countriesList.Add(new DBCountry(176, "Peru"));
            countriesList.Add(new DBCountry(177, "French Polynesia"));
            countriesList.Add(new DBCountry(178, "Papua New Guinea"));
            countriesList.Add(new DBCountry(179, "Philippines"));
            countriesList.Add(new DBCountry(180, "Pakistan"));
            countriesList.Add(new DBCountry(181, "Poland"));
            countriesList.Add(new DBCountry(182, "Saint Pierre and Miquelon"));
            countriesList.Add(new DBCountry(183, "Pitcairn Islands"));
            countriesList.Add(new DBCountry(184, "Puerto Rico"));
            countriesList.Add(new DBCountry(185, "Palestine"));
            countriesList.Add(new DBCountry(186, "Portugal"));
            countriesList.Add(new DBCountry(187, "Palau"));
            countriesList.Add(new DBCountry(188, "Paraguay"));
            countriesList.Add(new DBCountry(189, "Qatar"));
            countriesList.Add(new DBCountry(190, "Réunion"));
            countriesList.Add(new DBCountry(191, "Romania"));
            countriesList.Add(new DBCountry(192, "Serbia"));
            countriesList.Add(new DBCountry(193, "Russia"));
            countriesList.Add(new DBCountry(194, "Rwanda"));
            countriesList.Add(new DBCountry(195, "Saudi Arabia"));
            countriesList.Add(new DBCountry(196, "Solomon Islands"));
            countriesList.Add(new DBCountry(197, "Seychelles"));
            countriesList.Add(new DBCountry(198, "Sudan"));
            countriesList.Add(new DBCountry(199, "Sweden"));
            countriesList.Add(new DBCountry(200, "Singapore"));
            countriesList.Add(new DBCountry(201, "Saint Helena"));
            countriesList.Add(new DBCountry(202, "Slovenia"));
            countriesList.Add(new DBCountry(203, "Svalbard and Jan Mayen"));
            countriesList.Add(new DBCountry(204, "Slovakia"));
            countriesList.Add(new DBCountry(205, "Sierra Leone"));
            countriesList.Add(new DBCountry(206, "San Marino"));
            countriesList.Add(new DBCountry(207, "Senegal"));
            countriesList.Add(new DBCountry(208, "Somalia"));
            countriesList.Add(new DBCountry(209, "Suriname"));
            countriesList.Add(new DBCountry(210, "South Sudan"));
            countriesList.Add(new DBCountry(211, "São Tomé and Príncipe"));
            countriesList.Add(new DBCountry(212, "El Salvador"));
            countriesList.Add(new DBCountry(213, "Sint Maarten"));
            countriesList.Add(new DBCountry(214, "Syria"));
            countriesList.Add(new DBCountry(215, "Swaziland"));
            countriesList.Add(new DBCountry(216, "Turks and Caicos Islands"));
            countriesList.Add(new DBCountry(217, "Chad"));
            countriesList.Add(new DBCountry(218, "French Southern Territories"));
            countriesList.Add(new DBCountry(219, "Togo"));
            countriesList.Add(new DBCountry(220, "Thailand"));
            countriesList.Add(new DBCountry(221, "Tajikistan"));
            countriesList.Add(new DBCountry(222, "Tokelau"));
            countriesList.Add(new DBCountry(223, "East Timor"));
            countriesList.Add(new DBCountry(224, "Turkmenistan"));
            countriesList.Add(new DBCountry(225, "Tunisia"));
            countriesList.Add(new DBCountry(226, "Tonga"));
            countriesList.Add(new DBCountry(227, "Turkey"));
            countriesList.Add(new DBCountry(228, "Trinidad and Tobago"));
            countriesList.Add(new DBCountry(229, "Tuvalu"));
            countriesList.Add(new DBCountry(230, "Taiwan"));
            countriesList.Add(new DBCountry(231, "Tanzania"));
            countriesList.Add(new DBCountry(232, "Ukraine"));
            countriesList.Add(new DBCountry(233, "Uganda"));
            countriesList.Add(new DBCountry(234, "U.S. Minor Outlying Islands"));
            countriesList.Add(new DBCountry(235, "United States"));
            countriesList.Add(new DBCountry(236, "Uruguay"));
            countriesList.Add(new DBCountry(237, "Uzbekistan"));
            countriesList.Add(new DBCountry(238, "Vatican City"));
            countriesList.Add(new DBCountry(239, "Saint Vincent and the Grenadines"));
            countriesList.Add(new DBCountry(240, "Venezuela"));
            countriesList.Add(new DBCountry(241, "British Virgin Islands"));
            countriesList.Add(new DBCountry(242, "U.S. Virgin Islands"));
            countriesList.Add(new DBCountry(243, "Vietnam"));
            countriesList.Add(new DBCountry(244, "Vanuatu"));
            countriesList.Add(new DBCountry(245, "Wallis and Futuna"));
            countriesList.Add(new DBCountry(246, "Samoa"));
            countriesList.Add(new DBCountry(247, "Kosovo"));
            countriesList.Add(new DBCountry(248, "Yemen"));
            countriesList.Add(new DBCountry(249, "Mayotte"));
            countriesList.Add(new DBCountry(250, "South Africa"));
            countriesList.Add(new DBCountry(251, "Zambia"));
            countriesList.Add(new DBCountry(252, "Zimbabwe"));
            return countriesList;
        }

        #endregion
    }
}
