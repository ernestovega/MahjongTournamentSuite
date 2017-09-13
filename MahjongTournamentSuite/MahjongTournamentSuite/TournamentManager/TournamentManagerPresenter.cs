using MahjongTournamentSuite.Model;
using System.Collections.Generic;
using System;
using MahjongTournamentSuiteDataLayer.Model;
using MahjongTournamentSuiteDataLayer.Data;
using System.Linq;
using MahjongTournamentSuite.Resources;
using MahjongTournamentSuite.Resources.flags;

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
            _tables = _db.GetTournamentTables(tournamentId);
            if(_tournament.IsTeams)
                _form.ShowButtonTeams();
            _form.AddRoundsButtons(_tournament.NumRounds);
            roundSelected = 1;
            _form.AddTablesButtons(roundSelected, _tournament.NumPlayers / 4);
            _form.SelectRoundButton(roundSelected);
        }

        public void OnFormResized()
        {
            _form.CenterMainButtons();
            _form.RemoveRoundsButtons();
            _form.AddRoundsButtons(_tournament.NumRounds);
            _form.RemoveTablesButtons();
            _form.AddTablesButtons(roundSelected, _tournament.NumPlayers / 4);
            _form.SelectRoundButton(roundSelected);
            if(tableSelected > 0)
                _form.SelectTableButton(tableSelected);
        }

        public void ExportTournamentToExcelClicked()
        {
            //GenerateTablesWhitAll();
            //GenerateSTablesWithNames();
            //GenerateSTablesWithIds();
            //GenerateTablesByPlayer();
            //GenerateRivalsByPlayer();

            //ExportTournament();
            //ExportScoreTables();
        }

        public void ExportRankingsToHTMLClicked()
        {
            GenerateRankings();
            HTMLRankings htmlRankings = new HTMLRankings(GeneratePlayersHTMLRanking(),
                GenerateTeamsHTMLRanking(), GenerateChickenHandsHTMLRanking(), _tournament.IsTeams);
            _form.GoToHTMLViewer(htmlRankings);
        }
        
        public void ButtonRoundClicked(int roundId)
        {
            UnselectTable(tableSelected);
            UnselectRound(roundSelected);
            roundSelected = roundId;
            _form.SelectRoundButton(roundId);
            _form.RemoveTablesButtons();
            _form.AddTablesButtons(roundId, _tournament.NumPlayers / 4);
        }

        public void ButtonTableClicked(int tableId)
        {
            UnselectTable(tableSelected);
            tableSelected = tableId;
            _form.SelectTableButton(tableId);
            _form.GoToTableManager(roundSelected, tableId);
        }

        public void ShowRankingsClicked()
        {
            GenerateRankings();
            Rankings rankings = new Rankings(_playersRankings, _teamsRankings, 
                _chickenHandsRankings, _tournament.IsTeams);
            _form.GoToRankings(rankings);
        }
        
        #endregion

        #region Private
        
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
            _players = _db.GetTournamentPlayers(_tournament.TournamentId);
            _hands = _db.GetTournamentHands(_tournament.TournamentId);
            if (_tournament.IsTeams)
                _teams = _db.GetTournamentTeams(_tournament.TournamentId);
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
                string countryImageUrl = _db.GetCountryImageUrl(player.PlayerCountryName);
                PlayerRanking playerRanking = new PlayerRanking(player.PlayerId, player.PlayerName,
                    player.PlayerTeamId, teamName, player.PlayerCountryName, countryImageUrl, 
                    CountryFlags.GetFlagImage(player.PlayerCountryName));

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
                ChickenHandRanking playerChickenHandRanking = new ChickenHandRanking(playerRanking.PlayerId, 
                    playerRanking.PlayerName, playerRanking.PlayerPoints, playerRanking.PlayerScore, 
                    playerRanking.PlayerCountryName, playerRanking.PlayerCountryImageUrl, 
                    playerRanking.PlayerCountryFlagImage);

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
            string htmlChickenHandsRanking = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}\n{8}{9}", MyConstants.HTML_OPEN_TABLE,
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
