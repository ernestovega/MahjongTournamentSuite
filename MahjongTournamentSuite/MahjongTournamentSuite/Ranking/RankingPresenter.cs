using MahjongTournamentSuiteDataLayer.Data;
using MahjongTournamentSuiteDataLayer.Model;
using System.Collections.Generic;
using System.Threading;
using MahjongTournamentSuite.Model;
using System.Linq;
using System;

namespace MahjongTournamentSuite.Ranking
{
    class RankingPresenter : IRankingPresenter
    {
        #region Constants

        public const int DEFAULT_NUM_ROWS_PER_SCREEN = 20;
        private static readonly int MAX_PAGE_SHOW_TIME = 7; //Seconds

        #endregion

        #region Fields

        private IDBManager _db;
        private IRankingForm _form;
        private Rankings _rankings;
        private Thread _showerThread;
        private int _numRowsPerScreen;
        private bool _stopShow = false;
        private int _showTime = MAX_PAGE_SHOW_TIME;

        #endregion

        #region Constructor

        public RankingPresenter(IRankingForm mainForm)
        {
            _db = new DBManager();
            _form = mainForm;
        }

        #endregion

        #region IMainPresenter implementation

        public void LoadData(Rankings rankings)
        {
            _rankings = rankings;
            _numRowsPerScreen = _rankings.PlayersRankings.Count < DEFAULT_NUM_ROWS_PER_SCREEN ? 
                _rankings.PlayersRankings.Count : DEFAULT_NUM_ROWS_PER_SCREEN;
            _form.SetNumRowsPerScreen(_numRowsPerScreen);
            _showerThread = new Thread(new ThreadStart(ShowRankings));
        }

        public void StartShowRankingThread()
        {
            _showerThread.Start();
        }

        public void StopShowRankingThread()
        {
            _stopShow = true;
        }

        public void AbortShowRankingThreadIfAlive()
        {
            if(_showerThread.IsAlive)
                _showerThread.Abort();
        }

        public void IncrementShowingTimeInOneSecond()
        {
            _showTime++;
            _form.SetSecondsLabel(_showTime.ToString());
            if (_showTime == 2)
                _form.ShowButtonSecondsDown();
            
        }

        public void DecrementShowingTimeInOneSecond()
        {
            _showTime--;
            _form.SetSecondsLabel(_showTime.ToString());
            if (_showTime == 1)
                _form.HideButtonSecondsDown();
        }

        #endregion

        #region Private

        private void ShowRankings()
        {
            bool showTeams = true;
            bool showPlayers = false;
            int startIndex = 0;
            int rowsRange = _numRowsPerScreen;

            while (!_stopShow)
            {
                if (showTeams)
                {
                    if (_rankings.IsTeams)
                    {
                        if ((startIndex + rowsRange) > _rankings.TeamsRankings.Count)
                            rowsRange -= (startIndex + rowsRange) - _rankings.TeamsRankings.Count;

                        _form.FillDGVTeamsFromThread(_rankings.TeamsRankings.GetRange(startIndex, rowsRange));
                        SleepRanking();

                        if ((startIndex + _numRowsPerScreen) < _rankings.TeamsRankings.Count)
                            startIndex += _numRowsPerScreen;
                        else
                        {
                            showTeams = false;
                            showPlayers = true;
                            startIndex = 0;
                            rowsRange = _numRowsPerScreen;
                        }
                    }
                    else
                    {
                        showTeams = false;
                        showPlayers = true;
                        startIndex = 0;
                        rowsRange = _numRowsPerScreen;
                    }
                }
                else if (showPlayers)
                {
                    if ((startIndex + rowsRange) > _rankings.PlayersRankings.Count)
                        rowsRange -= (startIndex + rowsRange) - _rankings.PlayersRankings.Count;

                    _form.FillDGVPlayersFromThread(_rankings.PlayersRankings.GetRange(startIndex, rowsRange), _rankings.IsTeams);
                    SleepRanking();

                    if ((startIndex + _numRowsPerScreen) < _rankings.PlayersRankings.Count)
                        startIndex += _numRowsPerScreen;
                    else
                    {
                        showPlayers = false;
                        startIndex = 0;
                        rowsRange = _numRowsPerScreen;
                    }
                }
                else
                {
                    if (_rankings.PlayersChickenHandsRankings.Count > 0)
                    {
                        if ((startIndex + rowsRange) > _rankings.PlayersChickenHandsRankings.Count)
                            rowsRange -= (startIndex + rowsRange) - _rankings.PlayersChickenHandsRankings.Count;

                        _form.FillDGVPlayersChickenHandsFromThread(_rankings.PlayersChickenHandsRankings.GetRange(startIndex, rowsRange));
                        SleepRanking();

                        if ((startIndex + _numRowsPerScreen) < _rankings.PlayersChickenHandsRankings.Count)
                            startIndex += _numRowsPerScreen;
                        else
                        {
                            showTeams = true;
                            startIndex = 0;
                            rowsRange = _numRowsPerScreen;
                        }
                    }
                    else
                    {
                        showTeams = true;
                        startIndex = 0;
                        rowsRange = _numRowsPerScreen;
                    }
                }
            }
            _form.CloseFormFromThread();
            return;
        }

        private void SleepRanking()
        {
            for (int i = 0; i < _showTime * 2; i++)
            {
                Thread.Sleep(500); //0.5 seconds
                if (_stopShow)
                    break;
            }
        }

        #endregion
    }
}
