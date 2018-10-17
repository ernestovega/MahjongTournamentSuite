using MahjongTournamentSuite.ViewModel;
using System.Collections.Generic;
using MahjongTournamentSuite._Data.DataModel;
using System.Linq;
using MahjongTournamentSuite.Resources;
using MahjongTournamentSuite.Resources.flags;
using System;

namespace MahjongTournamentSuite.TournamentManager
{
    class TournamentManagerController : ITournamentManagerController
    {
        #region Fields

        private readonly int MAX_NUM_BEST_HANDS = 10;
        private ITournamentManagerForm _form;
        private ITournamentManagerDataManager _data;
        private VTournament _tournament;
        private List<VTeam> _teams;
        private List<VPlayer> _players;
        private List<VTable> _tables;
        private List<VHand> _hands;
        private int roundSelected = 1;
        private int tableSelected = 0;
        private List<PlayerRanking> _playersRankings; 
        private List<TeamRanking> _teamsRankings;
        private List<ChickenHandRanking> _chickenHandsRankings;
        private List<BestHandRanking> _bestHandsRankings;
        private int _tournamentId;
        private Rankings _rankings;
        private bool loadCompleted;

        #endregion

        #region Constructor

        public TournamentManagerController(ITournamentManagerForm form)
        {
            _form = form;
            _data = Injector.provideDataManager();
        }

        #endregion

        #region ITournamentManagerController

        public void LoadTournament(int tournamentId)
        {
            _form.CenterMainButtons();
            _tournamentId = tournamentId;
            _tournament = _data.GetTournament(tournamentId);
            _form.SetTournamentName(_tournament.TournamentName);
            _tables = _data.GetTournamentTables(tournamentId);
            if (_tournament.IsTeams)
            {
                _players = _data.GetTournamentPlayers(_tournament.TournamentId);
                _teams = _data.GetTournamentTeams(_tournament.TournamentId);
                _form.ShowButtonTeams();
                if (IsWrongPlayersTeams())
                {
                    ButtonPlayersClicked();
                    return;
                }
            }
            GenerateRoundsAndTablesButtons();
            loadCompleted = true;
        }

        public void OnFormResized(int tournamentId)
        {
            if (loadCompleted)
            {
                _form.CenterMainButtons();
                _form.RemoveRoundsButtons();
                _form.AddRoundsButtons(_tournament.NumRounds);
                _form.RemoveTablesButtons();
                _form.AddTablesButtons(_tournament.NumPlayers / 4);
                _form.SelectRoundButton(roundSelected);
                if (tableSelected > 0)
                    _form.SelectTableButton(tableSelected);
            }
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
            _data.RefreshTeams(_tournament.TournamentId);
            _teams = _data.GetTournamentTeams(_tournament.TournamentId);
        }

        public void ButtonPlayersClicked()
        {
            _form.GoToPlayersManager();
        }

        public void PlayersManagerFormClosed(bool isWrongTeams)
        {
            if (isWrongTeams)
                ButtonPlayersClicked();
            else
            {
                _data.RefreshPlayers(_tournament.TournamentId);
                GenerateRoundsAndTablesButtons();
            }
        }

        public void TableManagerFormClosed()
        {
            _data.RefreshTable(_tournament.TournamentId, roundSelected, tableSelected);
            _tables = _data.GetTournamentTables(_tournament.TournamentId);
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
            List<VTable> roundTables = _tables.FindAll(x => x.TableRoundId == roundId);
            foreach (VTable table in roundTables)
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

        public bool IsTableUsingTotalsOnly(int tableId)
        {
            return _tables.Find(x => x.TableRoundId == roundSelected && x.TableId == tableId).UseTotalsOnly;
        }
        
        public int GetNumRounds()
        {
            return _tournament.NumRounds;
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
            foreach (VTeam team in _teams)
            {
                List<VPlayer> teamPlayers = _players.FindAll(
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
            _tables = _data.GetTournamentTables(_tournament.TournamentId);
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
            if (_tournament.IsTeams) _teams = _data.GetTournamentTeams(_tournamentId);
            _players = _data.GetTournamentPlayers(_tournamentId);
            _tables = _data.GetTournamentTables(_tournamentId);
            _hands = _data.GetTournamentHands(_tournamentId);

            CalculateAndSortPlayersScores();
            if (_tournament.IsTeams) CalculateAndSortTeamsScores();
            CalculateAndSortPlayersChickenHands();
            CalculateAndSortPlayersBestHands();
            return new Rankings(_playersRankings, _teamsRankings, _chickenHandsRankings, _bestHandsRankings, _tournament.IsTeams);
        }

        private void CalculateAndSortPlayersScores()
        {
            _playersRankings = new List<PlayerRanking>(_players.Count);
            foreach (VPlayer player in _players)
            {
                string teamName = string.Empty;
                if (_tournament.IsTeams)
                    teamName = _teams.Find(x => x.TeamId == player.PlayerTeamId).TeamName;
                string countryImageUrl = _data.GetCountryImageUrl(player.PlayerCountryName);
                PlayerRanking playerRanking = new PlayerRanking(player.PlayerId, player.PlayerName,
                    player.PlayerTeamId, teamName, player.PlayerCountryName, countryImageUrl, 
                    CountryFlags.GetFlagImage(player.PlayerCountryName));

                List<VTable> playerTables = _tables.FindAll(x =>
                player.PlayerId == x.Player1Id || player.PlayerId == x.Player2Id ||
                player.PlayerId == x.Player3Id || player.PlayerId == x.Player4Id);

                foreach (VTable table in playerTables)
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
            foreach (VTeam team in _teams)
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

        private void CalculateAndSortPlayersBestHands()
        {
            List<VHand> bestHands = _hands.OrderByDescending(
                x => x.HandScore.Length == 0 ? 0 : int.Parse(x.HandScore))
                .ToList().GetRange(0, MAX_NUM_BEST_HANDS);
            _bestHandsRankings = new List<BestHandRanking>();
            foreach (VHand bestHand in bestHands)
            {
                string playerWinnerId = bestHand.PlayerWinnerId;
                if (playerWinnerId.Equals(string.Empty)) continue;
                VPlayer bestHandPlayer = _players.Find(x => x.PlayerId == int.Parse(playerWinnerId));
                PlayerRanking bestHandPlayerRanking = _playersRankings.Find(x => x.PlayerId == int.Parse(bestHand.PlayerWinnerId));
                BestHandRanking playerBestHandRanking = new BestHandRanking(
                    bestHandPlayer.PlayerId, bestHandPlayer.PlayerName,
                    int.Parse(bestHand.HandScore), bestHandPlayerRanking.PlayerPoints, 
                    bestHandPlayerRanking.PlayerScore, bestHandPlayer.PlayerCountryName, 
                    bestHandPlayerRanking.PlayerCountryHtmlFlagUrl, bestHandPlayerRanking.PlayerCountryFlag);
                _bestHandsRankings.Add(playerBestHandRanking);
            }
            for (int i = 0; i < _bestHandsRankings.Count; i++)
                _bestHandsRankings[i].Order = i + 1;
        }

        #endregion

        #region HTML export

        private string GeneratePlayersHTMLRanking()
        {
            string htmlPlayersRanking = string.Format("\n\n{0}\n{1}{2}{3}{4}{5}{6}{7}{8}{9}\n{10}\n{11}\n{12}", 
                HtmlConstants.HTML_PLAYERS_TABLE_TITLE,
                HtmlConstants.HTML_OPEN_TABLE_PLAYERS, HtmlConstants.HTML_OPEN_COLGROUP, HtmlConstants.HTML_COL_ORDER, 
                HtmlConstants.HTML_COL_NAME, HtmlConstants.HTML_COL_POINTS, HtmlConstants.HTML_COL_SCORE, 
                HtmlConstants.HTML_COL_TEAM, HtmlConstants.HTML_COL_COUNTRY, HtmlConstants.HTML_CLOSE_COLGROUP, 
                HtmlConstants.HTML_OPEN_TBODY, HtmlConstants.HTML_PLAYERS_HEADERS_TR, 
                HtmlConstants.HTML_OPEN_TR_HEADER_BOTTOM_SEPARATOR);

            foreach (PlayerRanking playerRanking in _playersRankings)
            {
                htmlPlayersRanking = string.Format("{0}\n{1}", htmlPlayersRanking, HtmlConstants.HTML_OPEN_TR);

                htmlPlayersRanking = string.Format("{0}\n{1}{2}{3}", htmlPlayersRanking, 
                    HtmlConstants.HTML_OPEN_TD_BOLD, playerRanking.Order, HtmlConstants.HTML_CLOSE_TD);

                htmlPlayersRanking = string.Format("{0}\n{1}{2}{3}", htmlPlayersRanking,
                    HtmlConstants.HTML_OPEN_TD, playerRanking.PlayerName, HtmlConstants.HTML_CLOSE_TD);

                htmlPlayersRanking = string.Format("{0}\n{1}{2}{3}", htmlPlayersRanking,
                    HtmlConstants.HTML_OPEN_TD, playerRanking.PlayerPoints, HtmlConstants.HTML_CLOSE_TD);

                htmlPlayersRanking = string.Format("{0}\n{1}{2}{3}", htmlPlayersRanking,
                    HtmlConstants.HTML_OPEN_TD, playerRanking.PlayerScore, HtmlConstants.HTML_CLOSE_TD);

                htmlPlayersRanking = string.Format("{0}\n{1}{2}{3}", htmlPlayersRanking,
                    HtmlConstants.HTML_OPEN_TD, playerRanking.PlayerTeamName, HtmlConstants.HTML_CLOSE_TD);

                htmlPlayersRanking = string.Format("{0}\n{1}<img class=\"alignnone size-full wp-image-665 aligncenter\" src=\"{2}\" alt=\"{3}\" width=\"32\" height=\"32\"/>{4}", htmlPlayersRanking,
                    HtmlConstants.HTML_OPEN_TD, playerRanking.PlayerCountryHtmlFlagUrl, playerRanking.PlayerCountryName, HtmlConstants.HTML_CLOSE_TD);

                htmlPlayersRanking = string.Format("{0}\n{1}", htmlPlayersRanking, HtmlConstants.HTML_CLOSE_TR);
            }

            htmlPlayersRanking = string.Format("{0}\n{1}\n{2}", htmlPlayersRanking, HtmlConstants.HTML_CLOSE_TBODY,
                HtmlConstants.HTML_CLOSE_TABLE);

            return htmlPlayersRanking;
        }

        private string GenerateTeamsHTMLRanking()
        {
            string htmlTeamsRanking = string.Format("{0}\n{1}{2}{3}{4}{5}{6}{7}\n{8}\n{9}\n{10}",
                HtmlConstants.HTML_TEAMS_TABLE_TITLE,
                HtmlConstants.HTML_OPEN_TABLE_TEAMS, HtmlConstants.HTML_OPEN_COLGROUP, HtmlConstants.HTML_COL_ORDER, 
                HtmlConstants.HTML_COL_NAME, HtmlConstants.HTML_COL_POINTS, HtmlConstants.HTML_COL_SCORE, 
                HtmlConstants.HTML_CLOSE_COLGROUP, HtmlConstants.HTML_OPEN_TBODY, 
                HtmlConstants.HTML_TEAMS_HEADERS_TR, HtmlConstants.HTML_OPEN_TR_HEADER_BOTTOM_SEPARATOR);

            foreach (TeamRanking teamRanking in _teamsRankings)
            {
                htmlTeamsRanking = string.Format("{0}\n{1}", htmlTeamsRanking, HtmlConstants.HTML_OPEN_TR_TEAMS);

                htmlTeamsRanking = string.Format("{0}\n{1}{2}{3}", htmlTeamsRanking,
                    HtmlConstants.HTML_OPEN_TD_BOLD, teamRanking.Order, HtmlConstants.HTML_CLOSE_TD);

                htmlTeamsRanking = string.Format("{0}\n{1}{2}{3}", htmlTeamsRanking,
                    HtmlConstants.HTML_OPEN_TD, teamRanking.TeamName, HtmlConstants.HTML_CLOSE_TD);

                htmlTeamsRanking = string.Format("{0}\n{1}{2}{3}", htmlTeamsRanking,
                    HtmlConstants.HTML_OPEN_TD, teamRanking.TeamPoints, HtmlConstants.HTML_CLOSE_TD);

                htmlTeamsRanking = string.Format("{0}\n{1}{2}{3}", htmlTeamsRanking,
                    HtmlConstants.HTML_OPEN_TD, teamRanking.TeamScore, HtmlConstants.HTML_CLOSE_TD);

                htmlTeamsRanking = string.Format("{0}\n{1}", htmlTeamsRanking, HtmlConstants.HTML_CLOSE_TR);
            }

            htmlTeamsRanking = string.Format("{0}\n{1}\n{2}", htmlTeamsRanking, HtmlConstants.HTML_CLOSE_TBODY,
                HtmlConstants.HTML_CLOSE_TABLE);

            return htmlTeamsRanking;
        }

        private string GenerateChickenHandsHTMLRanking()
        {
            string htmlChickenHandsRanking = string.Format("{0}\n{1}{2}{3}{4}{5}{6}{7}{8}{9}\n{10}\n{11}\n{12}",
                HtmlConstants.HTML_CHICKEN_HANDS_TABLE_TITLE,
                HtmlConstants.HTML_OPEN_TABLE_CHICKEN_HANDS, HtmlConstants.HTML_OPEN_COLGROUP, HtmlConstants.HTML_COL_ORDER, 
                   HtmlConstants.HTML_COL_NAME, HtmlConstants.HTML_COL_POINTS, HtmlConstants.HTML_COL_POINTS, 
                   HtmlConstants.HTML_COL_SCORE, HtmlConstants.HTML_COL_COUNTRY, HtmlConstants.HTML_CLOSE_COLGROUP, 
                   HtmlConstants.HTML_OPEN_TBODY, HtmlConstants.HTML_CHICKEN_HANDS_HEADERS_TR, 
                   HtmlConstants.HTML_OPEN_TR_HEADER_BOTTOM_SEPARATOR);

            foreach (ChickenHandRanking chickenHandRanking in _chickenHandsRankings)
            {
                htmlChickenHandsRanking = string.Format("{0}\n{1}", htmlChickenHandsRanking, HtmlConstants.HTML_OPEN_TR);

                htmlChickenHandsRanking = string.Format("{0}\n{1}{2}{3}", htmlChickenHandsRanking,
                    HtmlConstants.HTML_OPEN_TD_BOLD, chickenHandRanking.Order, HtmlConstants.HTML_CLOSE_TD);

                htmlChickenHandsRanking = string.Format("{0}\n{1}{2}{3}", htmlChickenHandsRanking,
                    HtmlConstants.HTML_OPEN_TD, chickenHandRanking.PlayerName, HtmlConstants.HTML_CLOSE_TD);

                htmlChickenHandsRanking = string.Format("{0}\n{1}{2}{3}", htmlChickenHandsRanking,
                    HtmlConstants.HTML_OPEN_TD, chickenHandRanking.PlayerNumChickenHands, HtmlConstants.HTML_CLOSE_TD);

                htmlChickenHandsRanking = string.Format("{0}\n{1}{2}{3}", htmlChickenHandsRanking,
                    HtmlConstants.HTML_OPEN_TD, chickenHandRanking.PlayerPoints, HtmlConstants.HTML_CLOSE_TD);

                htmlChickenHandsRanking = string.Format("{0}\n{1}{2}{3}", htmlChickenHandsRanking,
                    HtmlConstants.HTML_OPEN_TD, chickenHandRanking.PlayerScore, HtmlConstants.HTML_CLOSE_TD);

                htmlChickenHandsRanking = string.Format("{0}\n{1}<img class=\"alignnone size-full wp-image-665 aligncenter\" src=\"{2}\" alt=\"{3}\" width=\"32\" height=\"32\"/>{4}", htmlChickenHandsRanking,
                    HtmlConstants.HTML_OPEN_TD, chickenHandRanking.PlayerCountryHtmlFlagUrl, chickenHandRanking.PlayerCountryName, HtmlConstants.HTML_CLOSE_TD);

                htmlChickenHandsRanking = string.Format("{0}\n{1}", htmlChickenHandsRanking, HtmlConstants.HTML_CLOSE_TR);
            }

            htmlChickenHandsRanking = string.Format("{0}\n{1}\n{2}\n\n", htmlChickenHandsRanking, HtmlConstants.HTML_CLOSE_TBODY,
                HtmlConstants.HTML_CLOSE_TABLE);

            return htmlChickenHandsRanking;
        }

        private string GenerateBestHandsHTMLRanking()
        {
            string htmlBestHandsRanking = string.Format("{0}\n{1}{2}{3}{4}{5}{6}{7}{8}\n{9}\n{10}\n{11}",
                HtmlConstants.HTML_BEST_HANDS_TABLE_TITLE,
                HtmlConstants.HTML_OPEN_TABLE_BEST_HANDS, 
                HtmlConstants.HTML_OPEN_COLGROUP, 
                HtmlConstants.HTML_COL_ORDER,
                   HtmlConstants.HTML_COL_NAME, HtmlConstants.HTML_COL_POINTS,
                   HtmlConstants.HTML_COL_SCORE, HtmlConstants.HTML_COL_COUNTRY, HtmlConstants.HTML_CLOSE_COLGROUP,
                   HtmlConstants.HTML_OPEN_TBODY, HtmlConstants.HTML_BEST_HANDS_HEADERS_TR,
                   HtmlConstants.HTML_OPEN_TR_HEADER_BOTTOM_SEPARATOR);

            foreach (BestHandRanking bestHandRanking in _bestHandsRankings)
            {
                htmlBestHandsRanking = string.Format("{0}\n{1}", htmlBestHandsRanking, HtmlConstants.HTML_OPEN_TR);

                htmlBestHandsRanking = string.Format("{0}\n{1}{2}{3}", htmlBestHandsRanking,
                    HtmlConstants.HTML_OPEN_TD_BOLD, bestHandRanking.Order, HtmlConstants.HTML_CLOSE_TD);

                htmlBestHandsRanking = string.Format("{0}\n{1}{2}{3}", htmlBestHandsRanking,
                    HtmlConstants.HTML_OPEN_TD, bestHandRanking.PlayerName, HtmlConstants.HTML_CLOSE_TD);

                htmlBestHandsRanking = string.Format("{0}\n{1}{2}{3}", htmlBestHandsRanking,
                    HtmlConstants.HTML_OPEN_TD, bestHandRanking.PlayerPoints, HtmlConstants.HTML_CLOSE_TD);

                htmlBestHandsRanking = string.Format("{0}\n{1}{2}{3}", htmlBestHandsRanking,
                    HtmlConstants.HTML_OPEN_TD, bestHandRanking.PlayerScore, HtmlConstants.HTML_CLOSE_TD);

                htmlBestHandsRanking = string.Format("{0}\n{1}<img class=\"alignnone size-full wp-image-665 aligncenter\" src=\"{2}\" alt=\"{3}\" width=\"32\" height=\"32\"/>{4}", htmlBestHandsRanking,
                    HtmlConstants.HTML_OPEN_TD, bestHandRanking.PlayerCountryHtmlFlagUrl, bestHandRanking.PlayerCountryName, HtmlConstants.HTML_CLOSE_TD);

                htmlBestHandsRanking = string.Format("{0}\n{1}", htmlBestHandsRanking, HtmlConstants.HTML_CLOSE_TR);
            }

            htmlBestHandsRanking = string.Format("{0}\n{1}\n{2}\n\n", htmlBestHandsRanking, HtmlConstants.HTML_CLOSE_TBODY,
                HtmlConstants.HTML_CLOSE_TABLE);

            return htmlBestHandsRanking;
        }

        #endregion

        #endregion
    }
}
