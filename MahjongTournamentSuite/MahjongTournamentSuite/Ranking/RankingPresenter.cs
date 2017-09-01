using MahjongTournamentSuiteDataLayer.Data;
using MahjongTournamentSuiteDataLayer.Model;
using System.Collections.Generic;
using System.Threading;
using MahjongTournamentSuite.Model;
using System.Linq;

namespace MahjongTournamentSuite.Ranking
{
    class RankingPresenter : IRankingPresenter
    {
        #region Constants

        public static readonly int DEFAULT_NUM_ROWS_PER_SCREEN = 20;
        public static readonly int DEFAULT_SLEEP_TIME = 5000;

        #endregion

        #region Fields

        private IDBManager _db;
        private IRankingForm _form;
        private DBTournament _tournament;
        private List<DBPlayer> _players;
        private List<DBTeam> _teams;
        private List<DBTable> _tables;
        private List<DBHand> _hands;
        private Thread _showerThread;
        private List<PlayerRanking> _playersRankings;
        private List<TeamRanking> _teamsRankings;
        private List<PlayerChickenHandRanking> _playersChickenHandsRankings;

        #endregion

        #region Constructor

        public RankingPresenter(IRankingForm mainForm)
        {
            _db = new DBManager();
            _form = mainForm;
        }

        #endregion

        #region IMainPresenter implementation

        public void LoadDataAndStartShowRankingThread(int tournamentId)
        {
            _tournament = _db.GetTournament(tournamentId);
            _players = _db.GetTournamentPlayers(tournamentId);
            if(_tournament.IsTeams)
                _teams = _db.GetTournamentTeams(tournamentId);
            _tables = _db.GetTournamentTables(tournamentId);
            _hands = _db.GetTournamentHands(tournamentId);

            CalculateAndSortPlayersScores();
            if (_tournament.IsTeams)
                CalculateAndSortTeamsScores();
            CalculateAndSortPlayersChickenHands();

            _showerThread = new Thread(new ThreadStart(ShowRanking));
            _showerThread.Start();
        }

        public void StopShowRankingThread()
        {
            _showerThread.Abort();
        }

        #endregion

        #region Private

        private void CalculateAndSortPlayersScores()
        {
            _playersRankings = new List<PlayerRanking>(_players.Count);
            foreach(DBPlayer player in _players)
            {
                string teamName = _teams.Find(x => x.TeamId == player.PlayerTeamId).TeamName;
                string countryName = _db.GetCountryName(player.PlayerCountryId);
                PlayerRanking playerRanking = new PlayerRanking(player.PlayerId, player.PlayerName, 
                    player.PlayerTeamId, teamName, player.PlayerCountryId, countryName);

                List<DBTable> playerTables = _tables.FindAll(x => 
                player.PlayerId == x.Player1Id || player.PlayerId == x.Player2Id ||
                player.PlayerId == x.Player3Id || player.PlayerId == x.Player4Id);
                
                foreach (DBTable table in playerTables)
                {
                    if (player.PlayerId.ToString().Equals(table.PlayerEastId))
                    {
                        playerRanking.PlayerPoints += table.PlayerEastPoints.Equals(string.Empty) ? 0 : int.Parse(table.PlayerEastPoints);
                        playerRanking.PlayerScore += table.PlayerEastTotalScore.Equals(string.Empty) ? 0 : int.Parse(table.PlayerEastTotalScore);
                    }
                    else if (player.PlayerId.ToString().Equals(table.PlayerSouthId))
                    {
                        playerRanking.PlayerPoints += table.PlayerSouthPoints.Equals(string.Empty) ? 0 : int.Parse(table.PlayerSouthPoints);
                        playerRanking.PlayerScore += table.PlayerSouthTotalScore.Equals(string.Empty) ? 0 : int.Parse(table.PlayerSouthTotalScore);
                    }
                    else if (player.PlayerId.ToString().Equals(table.PlayerWestId))
                    {
                        playerRanking.PlayerPoints += table.PlayerWestPoints.Equals(string.Empty) ? 0 : int.Parse(table.PlayerWestPoints);
                        playerRanking.PlayerScore += table.PlayerWestTotalScore.Equals(string.Empty) ? 0 : int.Parse(table.PlayerWestTotalScore);
                    }
                    else
                    {
                        playerRanking.PlayerPoints += table.PlayerNorthPoints.Equals(string.Empty) ? 0 : int.Parse(table.PlayerNorthPoints);
                        playerRanking.PlayerScore += table.PlayerNorthTotalScore.Equals(string.Empty) ? 0 : int.Parse(table.PlayerNorthTotalScore);
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
                List<PlayerRanking> teamPlayersRankings = _playersRankings.FindAll(x => x.TeamId == team.TeamId);
                
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
            _playersChickenHandsRankings = new List<PlayerChickenHandRanking>();
            foreach (PlayerRanking playerRanking in _playersRankings)
            {
                PlayerChickenHandRanking playerChickenHandRanking = new PlayerChickenHandRanking(playerRanking.PlayerId, playerRanking.PlayerName, 
                    playerRanking.PlayerPoints, playerRanking.PlayerScore, playerRanking.CountryId, playerRanking.CountryName);

                playerChickenHandRanking.NumChickenHands = 
                    _hands.Count(x => x.IsChickenHand && int.Parse(x.PlayerWinnerId) == playerRanking.PlayerId);

                _playersChickenHandsRankings.Add(playerChickenHandRanking);
            }
            _playersChickenHandsRankings = _playersChickenHandsRankings.OrderByDescending(x => x.NumChickenHands)
                .ThenByDescending(x => x.PlayerPoints).ThenByDescending(x => x.PlayerScore).ToList();
            for (int i = 0; i < _playersChickenHandsRankings.Count; i++)
                _playersChickenHandsRankings[i].Order = i + 1;
        }

        private void ShowRanking()
        {
            bool showPlayers = true;
            bool showTeams = false;
            int startIndex = 0;
            int rowsRange = DEFAULT_NUM_ROWS_PER_SCREEN;

            while (true)
            {
                if (showPlayers)
                {
                    if ((startIndex + rowsRange) > _playersRankings.Count)
                        rowsRange -= (startIndex + rowsRange) - _playersRankings.Count;

                    _form.FillDGVPlayersFromThread(_playersRankings.GetRange(startIndex, rowsRange));

                    if ((startIndex + DEFAULT_NUM_ROWS_PER_SCREEN) < _playersRankings.Count)
                    {
                        startIndex += DEFAULT_NUM_ROWS_PER_SCREEN;
                    }
                    else
                    {
                        showPlayers = false;
                        showTeams = true;
                        startIndex = 0;
                        rowsRange = DEFAULT_NUM_ROWS_PER_SCREEN;
                    }
                }
                else if(showTeams)
                {
                    if (_tournament.IsTeams)
                    {
                        if ((startIndex + rowsRange) > _teamsRankings.Count)
                            rowsRange -= (startIndex + rowsRange) - _teamsRankings.Count;

                        _form.FillDGVTeamsFromThread(_teamsRankings.GetRange(startIndex, rowsRange));

                        if ((startIndex + DEFAULT_NUM_ROWS_PER_SCREEN) < _teamsRankings.Count)
                            startIndex += DEFAULT_NUM_ROWS_PER_SCREEN;
                        else
                        {
                            showTeams = false;
                            startIndex = 0;
                            rowsRange = DEFAULT_NUM_ROWS_PER_SCREEN;
                        }
                    }
                    else
                    {
                        showTeams = false;
                        startIndex = 0;
                        rowsRange = DEFAULT_NUM_ROWS_PER_SCREEN;
                    }
                }
                else
                {
                    if (_playersChickenHandsRankings.Count > 0)
                    {
                        if ((startIndex + rowsRange) > _teamsRankings.Count)
                            rowsRange -= (startIndex + rowsRange) - _playersChickenHandsRankings.Count;

                        _form.FillDGVPlayersChickenHandsFromThread(_playersChickenHandsRankings.GetRange(startIndex, rowsRange));

                        if ((startIndex + DEFAULT_NUM_ROWS_PER_SCREEN) < _teamsRankings.Count)
                            startIndex += DEFAULT_NUM_ROWS_PER_SCREEN;
                        else
                        {
                            showPlayers = true;
                            startIndex = 0;
                            rowsRange = DEFAULT_NUM_ROWS_PER_SCREEN;
                        }
                    }
                    else
                    {
                        showPlayers = true;
                        startIndex = 0;
                        rowsRange = DEFAULT_NUM_ROWS_PER_SCREEN;
                    }
                }
                Thread.Sleep(DEFAULT_SLEEP_TIME);
            }
        }

        #endregion
    }
}
