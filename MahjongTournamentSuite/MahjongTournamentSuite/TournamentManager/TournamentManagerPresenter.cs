using MahjongTournamentSuite.Model;
using System.Collections.Generic;
using System;
using MahjongTournamentSuiteDataLayer.Model;
using MahjongTournamentSuiteDataLayer.Data;
using System.Linq;
using MahjongTournamentSuite.Resources;

namespace MahjongTournamentSuite.TournamentManager
{
    class TournamentManagerPresenter : ITournamentManagerPresenter
    {
        #region Fields

        private ITournamentManagerForm _form;
        private IDBManager _db;
        private DBTournament _tournament;
        private List<DBTeam> _teams;
        private List<DBPlayer> _players;
        private List<DBTable> _tables;
        private List<DBHand> _hands;
        private bool isRoundsSelected = false;
        private int roundSelected = 0;
        private int tableSelected = 0;
        private List<PlayerRanking> _playersRankings; 
        private List<TeamRanking> _teamsRankings;
        private List<ChickenHandRanking> _chickenHandsRankings;

        #endregion

        #region Constructor

        public TournamentManagerPresenter(ITournamentManagerForm form)
        {
            _form = form;
            _db = Injector.provideDBManager();
        }

        #endregion

        #region ITournamentManagerPresenter

        public void LoadTournament(int tournamentId)
        {
            _tournament = _db.GetTournament(tournamentId);
            _players = _db.GetTournamentPlayers(tournamentId);
            _tables = _db.GetTournamentTables(tournamentId);
            _hands = _db.GetTournamentHands(tournamentId);
            if(_tournament.IsTeams)
            {
                _teams = _db.GetTournamentTeams(tournamentId);
                _form.ShowButtonTeams();
                List<WrongTeam> wrongTeams = GetWrongTeams();
                if (wrongTeams.Count > 0)
                {
                    _form.HideButtonRounds();
                    _form.ShowButtonPlayersBorder();
                    ButtonTeamsClicked();
                    return;
                }
            }
            ButtonRoundsClicked();
        }

        public void OnFormResized()
        {
            _form.ShowWaitCursor();
            _form.CenterMainButtons();
            if (isRoundsSelected)
            {
                _form.EmptyPanelRoundsButtons();
                _form.AddRoundsSubButtons(_tournament.NumRounds);
                _form.EmptyPanelRoundTablesButtons();
                _form.AddRoundTablesButtons(roundSelected, _tournament.NumPlayers / 4);
                _form.SelectRoundButton(roundSelected);
                if(tableSelected > 0)
                    _form.SelectRoundTableButton(tableSelected);
                _form.ShowDefaultCursor();
            }
        }

        public void ButtonTeamsClicked()
        {
            _form.ShowWaitCursor();
            UnselectTable(tableSelected);
            UnselectRound(roundSelected);
            UnselectRounds();
            UnselectPlayers();
            _form.SelectTeamsButton();
            _form.HideRoundsButtonsAndTablesPanel();
            _form.ShowDGV();
            _form.FillDGVWithTeams(_teams);
            _form.ShowDefaultCursor();
        }

        public void ButtonPlayersClicked()
        {
            _form.ShowWaitCursor();
            UnselectTable(tableSelected);
            UnselectRound(roundSelected);
            UnselectRounds();
            UnselectTeams();
            _form.SelectPlayersButton();
            _form.HideRoundsButtonsAndTablesPanel();
            _form.ShowDGV();
            _form.FillDGVWithPlayers(GetDataGridPlayers(), _tournament.IsTeams);
            if (_tournament.IsTeams)
            {
                List<WrongTeam> wrongTeams = GetWrongTeams();
                if (wrongTeams.Count > 0)
                {
                    _form.HideButtonRounds();
                    _form.ShowButtonPlayersBorder();
                    _form.MarkWrongTeamsPlayers(wrongTeams);
                    _form.ShowWrongNumberOfPlayersPerTeamMessage(wrongTeams);
                }
                else
                {
                    _form.HideButtonPlayersBorder();
                    _form.ShowButtonRounds();
                }
            }
            _form.ShowDefaultCursor();
        }

        public void ButtonRoundsClicked()
        {
            _form.ShowWaitCursor();
            UnselectTable(tableSelected);
            UnselectRound(roundSelected);
            UnselectPlayers();
            UnselectTeams();
            isRoundsSelected = true;
            _form.SelectRoundsButton();
            _form.HideDGV();
            _form.ShowRoundsButtonsAndTablesPanel();
            _form.EmptyPanelRoundsButtons();
            _form.AddRoundsSubButtons(_tournament.NumRounds);
            _form.EmptyPanelRoundTablesButtons();
            roundSelected = 1;
            _form.AddRoundTablesButtons(roundSelected, _tournament.NumPlayers / 4);
            _form.SelectRoundButton(roundSelected);
            _form.ShowDefaultCursor();
        }

        public void ButtonRoundClicked(int roundId)
        {
            UnselectTable(tableSelected);
            UnselectRound(roundSelected);
            roundSelected = roundId;
            _form.SelectRoundButton(roundId);
            _form.EmptyPanelRoundTablesButtons();
            _form.AddRoundTablesButtons(roundId, _tournament.NumPlayers / 4);
        }

        public void ButtonRoundTableClicked(int tableId)
        {
            _form.ShowWaitCursor();
            UnselectTable(tableSelected);
            tableSelected = tableId;
            _form.SelectRoundTableButton(tableId);
            _form.GoToTableManager(roundSelected, tableId);
            _form.ShowDefaultCursor();
        }

        public void TeamNameChanged(int teamId, string newName)
        {
            _form.ShowWaitCursor();
            int ownerTeamId = GetOwnerTeamNameId(newName);
            if (ownerTeamId > 0)
            {
                _form.DGVCancelEdit();
                _form.ShowMessageTeamNameInUse(newName, ownerTeamId);
                _form.ShowDefaultCursor();
                return;
            }
            _db.UpdateTeamName(_tournament.TournamentId, teamId, newName);
            _form.ShowDefaultCursor();
        }

        public void PlayerNameChanged(int playerId, string newName)
        {
            _form.ShowWaitCursor();
            int ownerPlayerId = GetOwnerPlayerNameId(newName);
            if (ownerPlayerId == 0)
                _db.UpdatePlayerName(_tournament.TournamentId, playerId, newName);
            else
            {
                _form.DGVCancelEdit();
                _form.ShowMessagePlayerNameInUse(newName, ownerPlayerId);
                _form.ShowDefaultCursor();
                return;
            }
            _form.ShowDefaultCursor();
        }

        public int SaveNewPlayerTeam(int playerId, string newTeamName)
        {
            _form.ShowWaitCursor();
            int newTeamId = _db.GetTeamId(_tournament.TournamentId, newTeamName);
            if (newTeamId > 0)
            {
                _db.UpdatePlayerTeam(_tournament.TournamentId, playerId, newTeamId);
                _form.ShowDefaultCursor();
                return newTeamId;
            }
            else
            {
                _form.ShowMessageTeamError();
                _form.ShowDefaultCursor();
                return 0;
            }
        }

        public void PlayerTeamChanged()
        {
            _form.ShowWaitCursor();
            List<WrongTeam> wrongTeams = GetWrongTeams();
            if (wrongTeams.Count > 0)
                ButtonPlayersClicked();
            else
            {
                _form.CleanWrongTeamsPlayers();
                _form.HideButtonPlayersBorder();
                _form.ShowButtonRounds();
            }
            _form.ShowDefaultCursor();
        }

        public int SaveNewPlayerCountry(int playerId, string newCountryName)
        {
            _form.ShowWaitCursor();
            int newCountryId = _db.GetCountryId(newCountryName);
            if (newCountryId > 0)
            {
                _db.UpdatePlayerCountry(_tournament.TournamentId, playerId, newCountryId);
                _form.ShowDefaultCursor();
                return newCountryId;
            }
            else
            {
                _form.ShowMessageCountryError();
                _form.ShowDefaultCursor();
                return 0;
            }
        }

        public void ShowRankingsClicked()
        {
            GenerateRankings();
            Rankings rankings = new Rankings(_playersRankings, _teamsRankings, 
                _chickenHandsRankings, _tournament.IsTeams);
            _form.GoToRankings(rankings);
        }

        public void ExportTournamentToExcel()
        {
            //GenerateTablesWhitAll();
            //GenerateSTablesWithNames();
            //GenerateSTablesWithIds();
            //GenerateTablesByPlayer();
            //GenerateRivalsByPlayer();

            //ExportTournament();
            //ExportScoreTables();
        }

        public void ExportRankingsToHTML()
        {
            GenerateRankings();
            string playersHtmlRanking = GeneratePlayersHTMLRanking();            
            string teamsHtmlRanking = GenerateTeamsHTMLRanking();
            string chickenHandsHtmlRanking = GenerateChickenHandsHTMLRanking();
            HTMLRankings htmlRankings = new HTMLRankings(playersHtmlRanking, teamsHtmlRanking,
                chickenHandsHtmlRanking, _tournament.IsTeams);
            _form.GoToHTMLViewer(htmlRankings);
        }

        #endregion

        #region Private

        /// <summary>
        /// Si los nombres de los jugadores no son números enteros, es que están rellenos.
        /// </summary>
        /// <returns></returns>
        private bool IsPlayersFilledByUser()
        {
            foreach (DBPlayer player in _players)
            {
                int parseResult;
                if (int.TryParse(player.PlayerName, out parseResult))
                    return false;
            }
            return true;
        }
        
        private List<WrongTeam> GetWrongTeams()
        {
            List<WrongTeam> wrongTeams = new List<WrongTeam>();
            foreach (DBTeam team in _teams)
            {
                List<DBPlayer> teamPlayers = _players.FindAll(
                    x => x.PlayerTournamentId == _tournament.TournamentId
                    && x.PlayerTeamId == team.TeamId);
                if (teamPlayers.Count == 0)
                    wrongTeams.Add(new WrongTeam(team.TeamId, team.TeamName, 0));
                else if (teamPlayers.Count != 4)
                    wrongTeams.Add(new WrongTeam(team.TeamId, team.TeamName, teamPlayers.Count));
            }
            return wrongTeams;
        }

        /// <summary>
        /// Look for a team that were using the same name. 
        /// </summary>
        /// <param name="newName">Name to search</param>
        /// <returns>0 if doesn´t find it, the TeamId if yes.</returns>
        private int GetOwnerTeamNameId(string newName)
        {
            DBTeam ownerTeam = _teams.Find(x => x.TeamName.Equals(newName, 
                StringComparison.InvariantCulture));
            if (ownerTeam == null)
                return 0;
            else
                return ownerTeam.TeamId;
        }

        /// <summary>
        /// Look for a player that were using the same name. 
        /// </summary>
        /// <param name="newName">Name to search</param>
        /// <returns>0 if doesn´t find it, the PlayerId if yes.</returns>
        private int GetOwnerPlayerNameId(string newName)
        {
            DBPlayer ownerPlayer = _players.Find(x => x.PlayerName.Equals(newName, 
                StringComparison.InvariantCulture));
            if (ownerPlayer == null)
                return 0;
            else
                return ownerPlayer.PlayerId;
        }

        private List<DGVPlayer> GetDataGridPlayers()
        {
            List<DGVPlayer> dgPlayers = new List<DGVPlayer>(_players.Count);
            foreach (DBPlayer player in _players)
            {
                dgPlayers.Add(new DGVPlayer(player, 
                    getPlayerTeamName(player.PlayerTeamId), getPlayerCountryName(player.PlayerCountryId)));
            }
            return dgPlayers;
        }

        private string getPlayerTeamName(int teamId)
        {
            if (_tournament.IsTeams && teamId > 0)
                return _teams.Find(x => x.TeamId == teamId).TeamName;
            else
                return "";
        }

        private string getPlayerCountryName(int countryId)
        {
            return _db.GetCountryImageUrl(countryId);
        }

        private void UnselectTeams()
        {
            _form.UnselectTeamsButton();
        }

        private void UnselectPlayers()
        {
            _form.UnselectPlayersButton();
        }

        private void UnselectRounds()
        {
            _form.UnselectRoundsButton();
            isRoundsSelected = false;
        }

        private void UnselectRound(int roundId)
        {
            if (roundId > 0)
            {
                _form.UnselectRoundButton(roundId);
                roundSelected = 0;
            }
        }

        private void UnselectTable(int tableId)
        {
            if (tableId > 0)
            {
                _form.UnselectTableButton(tableId);
                tableSelected = 0;
            }
        }

        #region Rankings

        private void GenerateRankings()
        {
            _tables = _db.GetTournamentTables(_tournament.TournamentId);
            _hands = _db.GetTournamentHands(_tournament.TournamentId);

            CalculateAndSortPlayersScores();
            if (_tournament.IsTeams)
                CalculateAndSortTeamsScores();
            CalculateAndSortPlayersChickenHands();
        }

        private void CalculateAndSortPlayersScores()
        {
            _playersRankings = new List<PlayerRanking>(_players.Count);
            foreach (DBPlayer player in _players)
            {
                string teamName = string.Empty;
                if (_tournament.IsTeams)
                    teamName = _teams.Find(x => x.TeamId == player.PlayerTeamId).TeamName;
                string countryName = _db.GetCountryName(player.PlayerCountryId);
                string countryImageUrl = _db.GetCountryImageUrl(player.PlayerCountryId);
                PlayerRanking playerRanking = new PlayerRanking(player.PlayerId, player.PlayerName,
                    player.PlayerTeamId, teamName, player.PlayerCountryId, countryName, countryImageUrl);

                List<DBTable> playerTables = _tables.FindAll(x =>
                player.PlayerId == x.Player1Id || player.PlayerId == x.Player2Id ||
                player.PlayerId == x.Player3Id || player.PlayerId == x.Player4Id);

                foreach (DBTable table in playerTables)
                {
                    if (player.PlayerId.ToString().Equals(table.PlayerEastId))
                    {
                        playerRanking.PlayerPoints += table.PlayerEastPoints.Equals(string.Empty) ? 0 : int.Parse(table.PlayerEastPoints);
                        playerRanking.PlayerScore += table.PlayerEastScore.Equals(string.Empty) ? 0 : int.Parse(table.PlayerEastScore);
                    }
                    else if (player.PlayerId.ToString().Equals(table.PlayerSouthId))
                    {
                        playerRanking.PlayerPoints += table.PlayerSouthPoints.Equals(string.Empty) ? 0 : int.Parse(table.PlayerSouthPoints);
                        playerRanking.PlayerScore += table.PlayerSouthScore.Equals(string.Empty) ? 0 : int.Parse(table.PlayerSouthScore);
                    }
                    else if (player.PlayerId.ToString().Equals(table.PlayerWestId))
                    {
                        playerRanking.PlayerPoints += table.PlayerWestPoints.Equals(string.Empty) ? 0 : int.Parse(table.PlayerWestPoints);
                        playerRanking.PlayerScore += table.PlayerWestScore.Equals(string.Empty) ? 0 : int.Parse(table.PlayerWestScore);
                    }
                    else
                    {
                        playerRanking.PlayerPoints += table.PlayerNorthPoints.Equals(string.Empty) ? 0 : int.Parse(table.PlayerNorthPoints);
                        playerRanking.PlayerScore += table.PlayerNorthScore.Equals(string.Empty) ? 0 : int.Parse(table.PlayerNorthScore);
                    }
                }
                _playersRankings.Add(playerRanking);
            }
            _playersRankings = _playersRankings.OrderByDescending(x => x.PlayerPoints).ThenByDescending(x => x.PlayerScore).ToList();
            for (int i = 0; i < _playersRankings.Count; i++)
                _playersRankings[i].Order = i + 1;
        }

        private void CalculateAndSortTeamsScores()
        {
            _teamsRankings = new List<TeamRanking>(_teams.Count);
            foreach (DBTeam team in _teams)
            {
                TeamRanking teamRanking = new TeamRanking(team.TeamId, team.TeamName);
                List<PlayerRanking> teamPlayersRankings = _playersRankings.FindAll(x => x.PlayerTeamId == team.TeamId);

                foreach (PlayerRanking pr in teamPlayersRankings)
                {
                    teamRanking.TeamPoints += pr.PlayerPoints;
                    teamRanking.TeamScore += pr.PlayerScore;
                }
                _teamsRankings.Add(teamRanking);
            }
            _teamsRankings = _teamsRankings.OrderByDescending(x => x.TeamPoints).ThenByDescending(x => x.TeamScore).ToList();
            for (int i = 0; i < _teamsRankings.Count; i++)
                _teamsRankings[i].Order = i + 1;
        }

        private void CalculateAndSortPlayersChickenHands()
        {
            _chickenHandsRankings = new List<ChickenHandRanking>();
            foreach (PlayerRanking playerRanking in _playersRankings)
            {
                ChickenHandRanking playerChickenHandRanking = new ChickenHandRanking(playerRanking.PlayerId, playerRanking.PlayerName,
                    playerRanking.PlayerPoints, playerRanking.PlayerScore, playerRanking.PlayerCountryId, playerRanking.PlayerCountryName);

                int numChickenHands = _hands.Count(x => x.IsChickenHand && int.Parse(x.PlayerWinnerId) == playerRanking.PlayerId);
                if (numChickenHands > 0)
                {
                    playerChickenHandRanking.PlayerNumChickenHands = numChickenHands;
                    _chickenHandsRankings.Add(playerChickenHandRanking);
                }
            }
            _chickenHandsRankings = _chickenHandsRankings.OrderByDescending(x => x.PlayerNumChickenHands)
                .ThenByDescending(x => x.PlayerPoints).ThenByDescending(x => x.PlayerScore).ToList();
            for (int i = 0; i < _chickenHandsRankings.Count; i++)
                _chickenHandsRankings[i].Order = i + 1;
        }

        #endregion

        #region Excel export

        //private void GenerateTablesWhitAll()
        //{
        //    for (currentRound = 1; currentRound <= numRounds; currentRound++)
        //    {
        //        for (currentTable = 1; currentTable <= players.Count / 4; currentTable++)
        //        {
        //            TableWithAll tableWithAll = new TableWithAll();
        //            tableWithAll.roundId = currentRound;
        //            tableWithAll.tableId = currentTable;
        //            for (currentTablePlayer = 1; currentTablePlayer <= 4; currentTablePlayer++)
        //            {
        //                switch (currentTablePlayer)
        //                {
        //                    case 1:
        //                        int player1Id = tablePlayers.Find(x => x.round == currentRound &&
        //                        x.table == currentTable && x.player == currentTablePlayer).playerId;
        //                        Player player = players.Find(x => x.id == player1Id);
        //                        tableWithAll.player1Name = player.name;
        //                        tableWithAll.player1Team = player.team;
        //                        tableWithAll.player1Id = player.id;
        //                        break;
        //                    case 2:
        //                        int player2Id = tablePlayers.Find(x => x.round == currentRound &&
        //                        x.table == currentTable && x.player == currentTablePlayer).playerId;
        //                        Player player2 = players.Find(x => x.id == player2Id);
        //                        tableWithAll.player2Name = player2.name;
        //                        tableWithAll.player2Team = player2.team;
        //                        tableWithAll.player2Id = player2.id;
        //                        break;
        //                    case 3:
        //                        int player3Id = tablePlayers.Find(x => x.round == currentRound &&
        //                        x.table == currentTable && x.player == currentTablePlayer).playerId;
        //                        Player player3 = players.Find(x => x.id == player3Id);
        //                        tableWithAll.player3Name = player3.name;
        //                        tableWithAll.player3Team = player3.team;
        //                        tableWithAll.player3Id = player3.id;
        //                        break;
        //                    case 4:
        //                        int player4Id = tablePlayers.Find(x => x.round == currentRound &&
        //                        x.table == currentTable && x.player == currentTablePlayer).playerId;
        //                        Player player4 = players.Find(x => x.id == player4Id);
        //                        tableWithAll.player4Name = player4.name;
        //                        tableWithAll.player4Team = player4.team;
        //                        tableWithAll.player4Id = player4.id;
        //                        break;
        //                }
        //            }
        //            tablesWithAll.Add(tableWithAll);
        //        }
        //    }
        //}

        //private void GenerateTablesByPlayer()
        //{
        //    foreach (Player p in players)
        //    {
        //        tablesByPlayer.AddRange(
        //            tablesWithAll.FindAll(x =>
        //                x.player1Name.Equals(p.name) ||
        //                x.player2Name.Equals(p.name) ||
        //                x.player3Name.Equals(p.name) ||
        //                x.player4Name.Equals(p.name)));
        //    }
        //}

        //private void GenerateRivalsByPlayer()
        //{
        //    foreach (Player p in players)
        //    {
        //        List<TableWithAll> thisPlayerTables = tablesWithAll.FindAll(x =>
        //                x.player1Name.Equals(p.name) ||
        //                x.player2Name.Equals(p.name) ||
        //                x.player3Name.Equals(p.name) ||
        //                x.player4Name.Equals(p.name));
        //        List<string> thisPlayerRivals = new List<string>();
        //        foreach (TableWithAll twa in thisPlayerTables)
        //        {
        //            if (!twa.player1Name.Equals(p.name))
        //                thisPlayerRivals.Add(twa.player1Name);
        //            if (!twa.player2Name.Equals(p.name))
        //                thisPlayerRivals.Add(twa.player2Name);
        //            if (!twa.player3Name.Equals(p.name))
        //                thisPlayerRivals.Add(twa.player3Name);
        //            if (!twa.player4Name.Equals(p.name))
        //                thisPlayerRivals.Add(twa.player4Name);
        //        }
        //        rivalsByPlayer.Add(new Rivals(p.name, thisPlayerRivals.ToArray()));
        //    }
        //}

        //private void GenerateSTablesWithNames()
        //{
        //    foreach (TableWithAll t in tablesWithAll)
        //    {
        //        sTablesNames.Add(new string[] {
        //            t.roundId.ToString(),
        //            t.tableId.ToString(),
        //            t.player1Name.ToString(),
        //            t.player2Name.ToString(),
        //            t.player3Name.ToString(),
        //            t.player4Name.ToString(), });
        //    }
        //}

        //private void GenerateSTablesWithIds()
        //{
        //    foreach (TableWithAll t in tablesWithAll)
        //    {
        //        sTablesIds.Add(new string[] {
        //            t.roundId.ToString(),
        //            t.tableId.ToString(),
        //            t.player1Id.ToString(),
        //            t.player2Id.ToString(),
        //            t.player3Id.ToString(),
        //            t.player4Id.ToString(), });
        //    }
        //}

        #endregion

        #region HTML export

        private string GeneratePlayersHTMLRanking()
        {
            string htmlPlayersRanking = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}\n{9}", MyConstants.HTML_OPEN_TABLE,
                MyConstants.HTML_OPEN_COLGROUP, MyConstants.HTML_COL_ORDER, MyConstants.HTML_COL_NAME,
                MyConstants.HTML_COL_POINTS, MyConstants.HTML_COL_SCORE, MyConstants.HTML_COL_TEAM,
                MyConstants.HTML_COL_COUNTRY, MyConstants.HTML_CLOSE_COLGROUP, MyConstants.HTML_OPEN_TBODY);

            foreach (PlayerRanking playerRanking in _playersRankings)
            {
                htmlPlayersRanking = string.Format("{0}\n{1}", htmlPlayersRanking, MyConstants.HTML_OPEN_TR);

                htmlPlayersRanking = string.Format("{0}\n{1}{2}{3}{4}{5}", htmlPlayersRanking, 
                    MyConstants.HTML_OPEN_TD, MyConstants.HTML_OPEN_STRONG, playerRanking.Order,
                    MyConstants.HTML_CLOSE_STRONG, MyConstants.HTML_CLOSE_TD);

                htmlPlayersRanking = string.Format("{0}\n{1}{2}{3}", htmlPlayersRanking,
                    MyConstants.HTML_OPEN_TD, playerRanking.PlayerName, MyConstants.HTML_CLOSE_TD);

                htmlPlayersRanking = string.Format("{0}\n{1}{2}{3}{4}{5}", htmlPlayersRanking,
                    MyConstants.HTML_OPEN_TD, MyConstants.HTML_OPEN_STRONG, playerRanking.PlayerPoints,
                    MyConstants.HTML_CLOSE_STRONG, MyConstants.HTML_CLOSE_TD);

                htmlPlayersRanking = string.Format("{0}\n{1}{2}{3}{4}{5}", htmlPlayersRanking,
                    MyConstants.HTML_OPEN_TD, MyConstants.HTML_OPEN_STRONG, playerRanking.PlayerScore,
                    MyConstants.HTML_CLOSE_STRONG, MyConstants.HTML_CLOSE_TD);

                htmlPlayersRanking = string.Format("{0}\n{1}{2}{3}", htmlPlayersRanking,
                    MyConstants.HTML_OPEN_TD, playerRanking.PlayerTeamName, MyConstants.HTML_CLOSE_TD);

                htmlPlayersRanking = string.Format("{0}\n{1}<img class=\"alignnone size-full wp-image-665 aligncenter\" src=\"{2}\" alt=\"{3}\" width=\"32\" height=\"32\"/>{4}", htmlPlayersRanking,
                    MyConstants.HTML_OPEN_TD, playerRanking.PlayerCountryImageUrl, playerRanking.PlayerCountryName, MyConstants.HTML_CLOSE_TD);

                htmlPlayersRanking = string.Format("{0}\n{1}", htmlPlayersRanking, MyConstants.HTML_CLOSE_TR);
            }

            htmlPlayersRanking = string.Format("{0}\n{1}\n{2}", htmlPlayersRanking, MyConstants.HTML_CLOSE_TBODY,
                MyConstants.HTML_CLOSE_TABLE);

            return htmlPlayersRanking;
        }

        private string GenerateTeamsHTMLRanking()
        {
            string htmlTeamsRanking = string.Format("{0}{1}{2}{3}{4}{5}{6}\n{7}", MyConstants.HTML_OPEN_TABLE,
                   MyConstants.HTML_OPEN_COLGROUP, MyConstants.HTML_COL_ORDER, MyConstants.HTML_COL_NAME,
                   MyConstants.HTML_COL_POINTS, MyConstants.HTML_COL_SCORE, MyConstants.HTML_CLOSE_COLGROUP, 
                   MyConstants.HTML_OPEN_TBODY);

            foreach (TeamRanking teamRanking in _teamsRankings)
            {
                htmlTeamsRanking = string.Format("{0}\n{1}", htmlTeamsRanking, MyConstants.HTML_OPEN_TR);

                htmlTeamsRanking = string.Format("{0}\n{1}{2}{3}{4}{5}", htmlTeamsRanking,
                    MyConstants.HTML_OPEN_TD, MyConstants.HTML_OPEN_STRONG, teamRanking.Order,
                    MyConstants.HTML_CLOSE_STRONG, MyConstants.HTML_CLOSE_TD);

                htmlTeamsRanking = string.Format("{0}\n{1}{2}{3}", htmlTeamsRanking,
                    MyConstants.HTML_OPEN_TD, teamRanking.TeamName, MyConstants.HTML_CLOSE_TD);

                htmlTeamsRanking = string.Format("{0}\n{1}{2}{3}{4}{5}", htmlTeamsRanking,
                    MyConstants.HTML_OPEN_TD, MyConstants.HTML_OPEN_STRONG, teamRanking.TeamPoints,
                    MyConstants.HTML_CLOSE_STRONG, MyConstants.HTML_CLOSE_TD);

                htmlTeamsRanking = string.Format("{0}\n{1}{2}{3}{4}{5}", htmlTeamsRanking,
                    MyConstants.HTML_OPEN_TD, MyConstants.HTML_OPEN_STRONG, teamRanking.TeamScore,
                    MyConstants.HTML_CLOSE_STRONG, MyConstants.HTML_CLOSE_TD);

                htmlTeamsRanking = string.Format("{0}\n{1}", htmlTeamsRanking, MyConstants.HTML_CLOSE_TR);
            }

            htmlTeamsRanking = string.Format("{0}\n{1}\n{2}", htmlTeamsRanking, MyConstants.HTML_CLOSE_TBODY,
                MyConstants.HTML_CLOSE_TABLE);

            return htmlTeamsRanking;
        }

        private string GenerateChickenHandsHTMLRanking()
        {
            string htmlChickenHandsRanking = string.Format("{0}{1}{2}{3}{4}{5}{6}\n{7}", MyConstants.HTML_OPEN_TABLE,
                   MyConstants.HTML_OPEN_COLGROUP, MyConstants.HTML_COL_ORDER, MyConstants.HTML_COL_NAME,
                   MyConstants.HTML_COL_POINTS, MyConstants.HTML_COL_POINTS, MyConstants.HTML_COL_SCORE,
                   MyConstants.HTML_COL_COUNTRY, MyConstants.HTML_CLOSE_COLGROUP, MyConstants.HTML_OPEN_TBODY);

            foreach (ChickenHandRanking chickenHandRanking in _chickenHandsRankings)
            {
                htmlChickenHandsRanking = string.Format("{0}\n{1}", htmlChickenHandsRanking, MyConstants.HTML_OPEN_TR);

                htmlChickenHandsRanking = string.Format("{0}\n{1}{2}{3}{4}{5}", htmlChickenHandsRanking,
                    MyConstants.HTML_OPEN_TD, MyConstants.HTML_OPEN_STRONG, chickenHandRanking.Order,
                    MyConstants.HTML_CLOSE_STRONG, MyConstants.HTML_CLOSE_TD);

                htmlChickenHandsRanking = string.Format("{0}\n{1}{2}{3}", htmlChickenHandsRanking,
                    MyConstants.HTML_OPEN_TD, chickenHandRanking.PlayerName, MyConstants.HTML_CLOSE_TD);

                htmlChickenHandsRanking = string.Format("{0}\n{1}{2}{3}{4}{5}", htmlChickenHandsRanking,
                    MyConstants.HTML_OPEN_TD, MyConstants.HTML_OPEN_STRONG, chickenHandRanking.PlayerNumChickenHands,
                    MyConstants.HTML_CLOSE_STRONG, MyConstants.HTML_CLOSE_TD);

                htmlChickenHandsRanking = string.Format("{0}\n{1}{2}{3}{4}{5}", htmlChickenHandsRanking,
                    MyConstants.HTML_OPEN_TD, MyConstants.HTML_OPEN_STRONG, chickenHandRanking.PlayerPoints,
                    MyConstants.HTML_CLOSE_STRONG, MyConstants.HTML_CLOSE_TD);

                htmlChickenHandsRanking = string.Format("{0}\n{1}{2}{3}{4}{5}", htmlChickenHandsRanking,
                    MyConstants.HTML_OPEN_TD, MyConstants.HTML_OPEN_STRONG, chickenHandRanking.PlayerScore,
                    MyConstants.HTML_CLOSE_STRONG, MyConstants.HTML_CLOSE_TD);

                htmlChickenHandsRanking = string.Format("{0}\n{1}{2}{3}", htmlChickenHandsRanking,
                    MyConstants.HTML_OPEN_TD, chickenHandRanking.PlayerCountryName, MyConstants.HTML_CLOSE_TD);

                htmlChickenHandsRanking = string.Format("{0}\n{1}", htmlChickenHandsRanking, MyConstants.HTML_CLOSE_TR);
            }

            htmlChickenHandsRanking = string.Format("{0}\n{1}\n{2}", htmlChickenHandsRanking, MyConstants.HTML_CLOSE_TBODY,
                MyConstants.HTML_CLOSE_TABLE);

            return htmlChickenHandsRanking;
        }

        #endregion

        #endregion
    }
}
