using MahjongTournamentSuite.Model;
using System.Collections.Generic;
using MahjongTournamentSuiteDataLayer.Model;
using MahjongTournamentSuiteDataLayer.Data;
using System.Linq;
using MahjongTournamentSuite.Resources;
using MahjongTournamentSuite.Resources.flags;
using System;
using MahjongTournamentSuite.EmaReport;

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
        private int roundSelected = 1;
        private int tableSelected = 0;
        private List<PlayerRanking> _playersRankings; 
        private List<TeamRanking> _teamsRankings;
        private List<ChickenHandRanking> _chickenHandsRankings;
        private int _tournamentId;
        private Rankings _rankings;

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
            _tournamentId = tournamentId;
            _tournament = _db.GetTournament(tournamentId);
            _tables = _db.GetTournamentTables(tournamentId);
            if (_tournament.IsTeams)
            {
                _players = _db.GetTournamentPlayers(_tournament.TournamentId);
                _teams = _db.GetTournamentTeams(_tournament.TournamentId);
                _form.ShowButtonTeams();
                if (IsWrongPlayersTeams())
                {
                    ButtonPlayersClicked();
                    return;
                }
            }
            GenerateRoundsAndTablesButtons();
        }

        public void OnFormResized(int tournamentId)
        {
            if (_tournament == null) {
                LoadTournament(tournamentId);
                return;
            }
            _form.RemoveRoundsButtons();
            _form.AddRoundsButtons(_tournament.NumRounds);
            _form.RemoveTablesButtons();
            _form.AddTablesButtons(_tournament.NumPlayers / 4);
            _form.SelectRoundButton(roundSelected);
            if(tableSelected > 0)
                _form.SelectTableButton(tableSelected);
        }

        public void PlayersTablesClicked()
        {
            _form.GoToPlayersTables(_tournament.TournamentId);
        }

        public void ButtonTeamsClicked()
        {
            _form.GoToTeamsManager();
        }

        public void TeamsManagerFormClosed()
        {
            _db.RefreshTeams(_tournament.TournamentId);
            _teams = _db.GetTournamentTeams(_tournament.TournamentId);
        }

        public void ButtonPlayersClicked()
        {
            _form.GoToPlayersManager();
        }

        public void PlayersManagerFormClosed(bool isWrongTeams)
        {
            if (!isWrongTeams)
                ButtonPlayersClicked();
            else
            {
                _db.RefreshPlayers(_tournament.TournamentId);
                GenerateRoundsAndTablesButtons();
            }
        }

        public void TableManagerFormClosed()
        {
            _db.RefreshTable(_tournament.TournamentId, roundSelected, tableSelected);
            _tables = _db.GetTournamentTables(_tournament.TournamentId);
            _form.SelectRoundButton(roundSelected);
            _form.SelectTableButton(tableSelected);
        }

        public void ButtonRoundClicked(int roundId)
        {
            UnselectTable(tableSelected);
            UnselectRound(roundSelected);
            roundSelected = roundId;
            _form.SelectRoundButton(roundId);
            _form.RemoveTablesButtons();
            _form.AddTablesButtons(_tournament.NumPlayers / 4);
        }

        public void ButtonTableClicked(int tableId)
        {
            UnselectTable(tableSelected);
            tableSelected = tableId;
            _form.GoToTableManager(roundSelected, tableId);
        }

        public void ShowRankingsClicked()
        {
            _rankings = GenerateRankings();
            _form.GoToRankings(_rankings);
        }

        public void EmaReportClicked()
        {
            //GenerateRankings();
            //List<DGVPlayerEma> dgvEmaPlayers = new List<DGVPlayerEma>(_players.Count);
            //foreach (var item in _players)
            //{

            //}
            //_form.GoToEmaReport(dgvEmaPlayers);
        }

        public bool IsRoundCompleted(int roundId)
        {
            List<DBTable> roundTables = _tables.FindAll(x => x.TableRoundId == roundSelected);
            foreach (DBTable table in roundTables)
            {
                if (!table.IsCompleted)
                    return false;
            }
            return true;
        }

        public bool IsTableCompleted(int tableId)
        {
            return _tables.Find(x => x.TableRoundId == roundSelected && x.TableId == tableId).IsCompleted;
        }

        #endregion

        #region Private

        private bool IsWrongPlayersTeams()
        {
            List<WrongTeam> wrongTeams = GetWrongTeams();
            return wrongTeams.Count > 0;
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

        private void GenerateRoundsAndTablesButtons()
        {
            _tables = _db.GetTournamentTables(_tournament.TournamentId);
            _form.AddRoundsButtons(_tournament.NumRounds);
            _form.AddTablesButtons(_tournament.NumPlayers / 4);
            _form.SelectRoundButton(roundSelected);
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

        private Rankings GenerateRankings()
        {
            _players = _db.GetTournamentPlayers(_tournamentId);
            _hands = _db.GetTournamentHands(_tournamentId);
            if (_tournament.IsTeams) _teams = _db.GetTournamentTeams(_tournamentId);
            _tables = _db.GetTournamentTables(_tournamentId);
            _hands = _db.GetTournamentHands(_tournamentId);

            CalculateAndSortPlayersScores();
            if (_tournament.IsTeams) CalculateAndSortTeamsScores();
            CalculateAndSortPlayersChickenHands();
            return new Rankings(_playersRankings, _teamsRankings,
                 _chickenHandsRankings, _tournament.IsTeams);
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
                        playerRanking.PlayerPoints += table.PlayerEastPoints.Equals(string.Empty) ? 0 : float.Parse(table.PlayerEastPoints);
                        playerRanking.PlayerScore += table.PlayerEastScore.Equals(string.Empty) ? 0 : int.Parse(table.PlayerEastScore);
                    }
                    else if (player.PlayerId.ToString().Equals(table.PlayerSouthId))
                    {
                        playerRanking.PlayerPoints += table.PlayerSouthPoints.Equals(string.Empty) ? 0 : float.Parse(table.PlayerSouthPoints);
                        playerRanking.PlayerScore += table.PlayerSouthScore.Equals(string.Empty) ? 0 : int.Parse(table.PlayerSouthScore);
                    }
                    else if (player.PlayerId.ToString().Equals(table.PlayerWestId))
                    {
                        playerRanking.PlayerPoints += table.PlayerWestPoints.Equals(string.Empty) ? 0 : float.Parse(table.PlayerWestPoints);
                        playerRanking.PlayerScore += table.PlayerWestScore.Equals(string.Empty) ? 0 : int.Parse(table.PlayerWestScore);
                    }
                    else
                    {
                        playerRanking.PlayerPoints += table.PlayerNorthPoints.Equals(string.Empty) ? 0 : float.Parse(table.PlayerNorthPoints);
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
                    playerRanking.PlayerCountryName, playerRanking.PlayerCountryHtmlFlagUrl, 
                    playerRanking.PlayerCountryFlag);

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

        #region HTML export

        private string GeneratePlayersHTMLRanking()
        {
            string htmlPlayersRanking = string.Format("\n\n{0}\n{1}{2}{3}{4}{5}{6}{7}{8}{9}\n{10}\n{11}\n{12}", 
                MyConstants.HTML_PLAYERS_TABLE_TITLE,
                MyConstants.HTML_OPEN_TABLE_PLAYERS, MyConstants.HTML_OPEN_COLGROUP, MyConstants.HTML_COL_ORDER, 
                MyConstants.HTML_COL_NAME, MyConstants.HTML_COL_POINTS, MyConstants.HTML_COL_SCORE, 
                MyConstants.HTML_COL_TEAM, MyConstants.HTML_COL_COUNTRY, MyConstants.HTML_CLOSE_COLGROUP, 
                MyConstants.HTML_OPEN_TBODY, MyConstants.HTML_PLAYERS_HEADERS_TR, 
                MyConstants.HTML_OPEN_TR_HEADER_BOTTOM_SEPARATOR);

            foreach (PlayerRanking playerRanking in _playersRankings)
            {
                htmlPlayersRanking = string.Format("{0}\n{1}", htmlPlayersRanking, MyConstants.HTML_OPEN_TR);

                htmlPlayersRanking = string.Format("{0}\n{1}{2}{3}", htmlPlayersRanking, 
                    MyConstants.HTML_OPEN_TD_BOLD, playerRanking.Order, MyConstants.HTML_CLOSE_TD);

                htmlPlayersRanking = string.Format("{0}\n{1}{2}{3}", htmlPlayersRanking,
                    MyConstants.HTML_OPEN_TD, playerRanking.PlayerName, MyConstants.HTML_CLOSE_TD);

                htmlPlayersRanking = string.Format("{0}\n{1}{2}{3}", htmlPlayersRanking,
                    MyConstants.HTML_OPEN_TD, playerRanking.PlayerPoints, MyConstants.HTML_CLOSE_TD);

                htmlPlayersRanking = string.Format("{0}\n{1}{2}{3}", htmlPlayersRanking,
                    MyConstants.HTML_OPEN_TD, playerRanking.PlayerScore, MyConstants.HTML_CLOSE_TD);

                htmlPlayersRanking = string.Format("{0}\n{1}{2}{3}", htmlPlayersRanking,
                    MyConstants.HTML_OPEN_TD, playerRanking.PlayerTeamName, MyConstants.HTML_CLOSE_TD);

                htmlPlayersRanking = string.Format("{0}\n{1}<img class=\"alignnone size-full wp-image-665 aligncenter\" src=\"{2}\" alt=\"{3}\" width=\"32\" height=\"32\"/>{4}", htmlPlayersRanking,
                    MyConstants.HTML_OPEN_TD, playerRanking.PlayerCountryHtmlFlagUrl, playerRanking.PlayerCountryName, MyConstants.HTML_CLOSE_TD);

                htmlPlayersRanking = string.Format("{0}\n{1}", htmlPlayersRanking, MyConstants.HTML_CLOSE_TR);
            }

            htmlPlayersRanking = string.Format("{0}\n{1}\n{2}", htmlPlayersRanking, MyConstants.HTML_CLOSE_TBODY,
                MyConstants.HTML_CLOSE_TABLE);

            return htmlPlayersRanking;
        }

        private string GenerateTeamsHTMLRanking()
        {
            string htmlTeamsRanking = string.Format("{0}\n{1}{2}{3}{4}{5}{6}{7}\n{8}\n{9}\n{10}",
                MyConstants.HTML_TEAMS_TABLE_TITLE,
                MyConstants.HTML_OPEN_TABLE_TEAMS, MyConstants.HTML_OPEN_COLGROUP, MyConstants.HTML_COL_ORDER, 
                MyConstants.HTML_COL_NAME, MyConstants.HTML_COL_POINTS, MyConstants.HTML_COL_SCORE, 
                MyConstants.HTML_CLOSE_COLGROUP, MyConstants.HTML_OPEN_TBODY, 
                MyConstants.HTML_TEAMS_HEADERS_TR, MyConstants.HTML_OPEN_TR_HEADER_BOTTOM_SEPARATOR);

            foreach (TeamRanking teamRanking in _teamsRankings)
            {
                htmlTeamsRanking = string.Format("{0}\n{1}", htmlTeamsRanking, MyConstants.HTML_OPEN_TR_TEAMS);

                htmlTeamsRanking = string.Format("{0}\n{1}{2}{3}", htmlTeamsRanking,
                    MyConstants.HTML_OPEN_TD_BOLD, teamRanking.Order, MyConstants.HTML_CLOSE_TD);

                htmlTeamsRanking = string.Format("{0}\n{1}{2}{3}", htmlTeamsRanking,
                    MyConstants.HTML_OPEN_TD, teamRanking.TeamName, MyConstants.HTML_CLOSE_TD);

                htmlTeamsRanking = string.Format("{0}\n{1}{2}{3}", htmlTeamsRanking,
                    MyConstants.HTML_OPEN_TD, teamRanking.TeamPoints, MyConstants.HTML_CLOSE_TD);

                htmlTeamsRanking = string.Format("{0}\n{1}{2}{3}", htmlTeamsRanking,
                    MyConstants.HTML_OPEN_TD, teamRanking.TeamScore, MyConstants.HTML_CLOSE_TD);

                htmlTeamsRanking = string.Format("{0}\n{1}", htmlTeamsRanking, MyConstants.HTML_CLOSE_TR);
            }

            htmlTeamsRanking = string.Format("{0}\n{1}\n{2}", htmlTeamsRanking, MyConstants.HTML_CLOSE_TBODY,
                MyConstants.HTML_CLOSE_TABLE);

            return htmlTeamsRanking;
        }

        private string GenerateChickenHandsHTMLRanking()
        {
            string htmlChickenHandsRanking = string.Format("{0}\n{1}{2}{3}{4}{5}{6}{7}{8}{9}\n{10}\n{11}\n{12}",
                MyConstants.HTML_CHICKEN_HANDS_TABLE_TITLE,
                MyConstants.HTML_OPEN_TABLE_CHICKEN_HANDS, MyConstants.HTML_OPEN_COLGROUP, MyConstants.HTML_COL_ORDER, 
                   MyConstants.HTML_COL_NAME, MyConstants.HTML_COL_POINTS, MyConstants.HTML_COL_POINTS, 
                   MyConstants.HTML_COL_SCORE, MyConstants.HTML_COL_COUNTRY, MyConstants.HTML_CLOSE_COLGROUP, 
                   MyConstants.HTML_OPEN_TBODY, MyConstants.HTML_CHICKEN_HANDS_HEADERS_TR, 
                   MyConstants.HTML_OPEN_TR_HEADER_BOTTOM_SEPARATOR);

            foreach (ChickenHandRanking chickenHandRanking in _chickenHandsRankings)
            {
                htmlChickenHandsRanking = string.Format("{0}\n{1}", htmlChickenHandsRanking, MyConstants.HTML_OPEN_TR);

                htmlChickenHandsRanking = string.Format("{0}\n{1}{2}{3}", htmlChickenHandsRanking,
                    MyConstants.HTML_OPEN_TD_BOLD, chickenHandRanking.Order, MyConstants.HTML_CLOSE_TD);

                htmlChickenHandsRanking = string.Format("{0}\n{1}{2}{3}", htmlChickenHandsRanking,
                    MyConstants.HTML_OPEN_TD, chickenHandRanking.PlayerName, MyConstants.HTML_CLOSE_TD);

                htmlChickenHandsRanking = string.Format("{0}\n{1}{2}{3}", htmlChickenHandsRanking,
                    MyConstants.HTML_OPEN_TD, chickenHandRanking.PlayerNumChickenHands, MyConstants.HTML_CLOSE_TD);

                htmlChickenHandsRanking = string.Format("{0}\n{1}{2}{3}", htmlChickenHandsRanking,
                    MyConstants.HTML_OPEN_TD, chickenHandRanking.PlayerPoints, MyConstants.HTML_CLOSE_TD);

                htmlChickenHandsRanking = string.Format("{0}\n{1}{2}{3}", htmlChickenHandsRanking,
                    MyConstants.HTML_OPEN_TD, chickenHandRanking.PlayerScore, MyConstants.HTML_CLOSE_TD);

                htmlChickenHandsRanking = string.Format("{0}\n{1}<img class=\"alignnone size-full wp-image-665 aligncenter\" src=\"{2}\" alt=\"{3}\" width=\"32\" height=\"32\"/>{4}", htmlChickenHandsRanking,
                    MyConstants.HTML_OPEN_TD, chickenHandRanking.PlayerCountryHtmlFlagUrl, chickenHandRanking.PlayerCountryName, MyConstants.HTML_CLOSE_TD);

                htmlChickenHandsRanking = string.Format("{0}\n{1}", htmlChickenHandsRanking, MyConstants.HTML_CLOSE_TR);
            }

            htmlChickenHandsRanking = string.Format("{0}\n{1}\n{2}\n\n", htmlChickenHandsRanking, MyConstants.HTML_CLOSE_TBODY,
                MyConstants.HTML_CLOSE_TABLE);

            return htmlChickenHandsRanking;
        }

        #endregion

        #endregion
    }
}
