﻿using System;
using MahjongTournamentSuiteDataLayer.Data;
using MahjongTournamentSuiteDataLayer.Model;
using System.Collections.Generic;
using System.Threading;
using MahjongTournamentRanking.Model;
using System.Linq;

namespace MahjongTournamentRanking.Main
{
    class MainPresenter : IMainPresenter
    {
        #region Constants

        private static readonly int NUM_ROWS_PER_SCREEN = 20;
        private static readonly int SLEEP_TIME = 5000;

        #endregion

        #region Fields

        private IDBManager _db;
        private IMainForm _form;
        private DBTournament _tournament;
        private List<DBPlayer> _players;
        private List<DBTeam> _teams;
        private List<DBTable> _tables;
        private Thread showerThread;
        private List<PlayerRanking> _playersRankings;
        private List<TeamRanking> _teamsRankings;

        #endregion

        #region Constructor

        public MainPresenter(IMainForm mainForm)
        {
            _db = new DBManager();
            _form = mainForm;
        }

        #endregion

        #region IMainPresenter implementation

        public void LoadRanking(int tournamentId)
        {
            _tournament = _db.GetTournament(tournamentId);
            _players = _db.GetTournamentPlayers(tournamentId);
            _teams = _db.GetTournamentTeams(tournamentId);
            _tables = _db.GetTournamentTables(tournamentId);

            CalculateAndSortPlayersScores();
            CalculateAndSortTeamsScores();

            showerThread = new Thread(new ThreadStart(ShowRanking));
            showerThread.Start();
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
            _playersRankings.OrderBy(x => x.PlayerPoints).ThenBy(x => x.PlayerScore);
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
            _teamsRankings.OrderBy(x => x.TeamPoints).ThenBy(x => x.TeamScore);
        }
        
        private void ShowRanking()
        {
            bool showTeams = false;
            int start = 0, end = NUM_ROWS_PER_SCREEN;
            while (true)
            {
                if (showTeams)
                {
                    _form.FillDGVTeamsFromThread(_teamsRankings.GetRange(start, end));
                    if (end < _teamsRankings.Count)
                    {
                        start += NUM_ROWS_PER_SCREEN;
                        end += NUM_ROWS_PER_SCREEN;
                    }
                    else
                    {
                        showTeams = false;
                        start = 0;
                        end = NUM_ROWS_PER_SCREEN;
                    }
                }
                else
                {
                    _form.FillDGVPlayersFromThread(_playersRankings.GetRange(start, end));
                    if (end < _playersRankings.Count)
                    {
                        start += NUM_ROWS_PER_SCREEN;
                        end += NUM_ROWS_PER_SCREEN;
                    }
                    else
                    {
                        showTeams = true;
                        start = 0;
                        end = NUM_ROWS_PER_SCREEN;
                    }
                }

                Thread.Sleep(SLEEP_TIME);
            }
        }

        #endregion
    }
}
