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
        private ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        private ManualResetEvent _pauseEvent = new ManualResetEvent(true);
        private Thread _showRankingThread;
        private int _numRowsPerScreen;
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
            _showRankingThread = new Thread(ShowRankings);
            _showRankingThread.Start();
            _pauseEvent.Reset();
        }

        public void StopShowRankingThread()
        {
            // Signal the shutdown event
            _shutdownEvent.Set();

            // Make sure to resume any paused threads
            _pauseEvent.Set();

            // Wait for the thread to exit
            _showRankingThread.Join();

            _form.CloseFormFromThread();
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

        public void PlayClicked()
        {
            _pauseEvent.Set();
            _form.HideButtonPlay();
            _form.ShowButtonPause();
        }

        public void PauseClicked()
        {
            _pauseEvent.Reset();
            _form.HideButtonPause();
            _form.ShowButtonPlay();
        }

        #endregion

        #region Private

        private void ShowRankings()
        {
            bool showTeams = true;
            bool showPlayers = false;
            int startIndex = 0;
            int rowsRange = _numRowsPerScreen;

            while (true)
            {
                if (_shutdownEvent.WaitOne(0))
                    break;

                _pauseEvent.WaitOne(Timeout.Infinite);

                if (showTeams)
                {
                    if (_rankings.IsTeams)
                    {
                        if ((startIndex + rowsRange) > _rankings.TeamsRankings.Count)
                            rowsRange -= (startIndex + rowsRange) - _rankings.TeamsRankings.Count;

                        _form.FillDGVTeamsFromThread(_rankings.TeamsRankings.GetRange(startIndex, rowsRange));
                        SleepRankingPage();

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
                    SleepRankingPage();

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
                        SleepRankingPage();

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
        }

        private void SleepRankingPage()
        {
            for (int i = 100; i < _showTime * 1000; i = i + 100)
            {
                if (_shutdownEvent.WaitOne(0))
                    return;
                Thread.Sleep(i);
            }
        }

        #endregion
    }
}
