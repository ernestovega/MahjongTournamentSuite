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
        private readonly int MAX_PAGE_SHOW_TIME = 5; //Seconds

        #endregion

        #region Fields

        private IDBManager _db;
        private IRankingForm _form;
        private Rankings _rankings;
        private Thread _showerThread;
        private int _numRowsPerScreen;
        private bool _stopShow = false;

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

        #endregion

        #region Private

        private void ShowRankings()
        {
            bool showPlayers = true;
            bool showTeams = false;
            int startIndex = 0;
            int rowsRange = _numRowsPerScreen;

            while (!_stopShow)
            {
                if (showPlayers)
                {
                    if ((startIndex + rowsRange) > _rankings.PlayersRankings.Count)
                        rowsRange -= (startIndex + rowsRange) - _rankings.PlayersRankings.Count;

                    _form.FillDGVPlayersFromThread(_rankings.PlayersRankings.GetRange(startIndex, rowsRange), _rankings.IsTeams);

                    if ((startIndex + _numRowsPerScreen) < _rankings.PlayersRankings.Count)
                        startIndex += _numRowsPerScreen;
                    else
                    {
                        showPlayers = false;
                        showTeams = true;
                        startIndex = 0;
                        rowsRange = _numRowsPerScreen;
                    }
                }
                else if(showTeams)
                {
                    if (_rankings.IsTeams)
                    {
                        if ((startIndex + rowsRange) > _rankings.TeamsRankings.Count)
                            rowsRange -= (startIndex + rowsRange) - _rankings.TeamsRankings.Count;

                        _form.FillDGVTeamsFromThread(_rankings.TeamsRankings.GetRange(startIndex, rowsRange));

                        if ((startIndex + _numRowsPerScreen) < _rankings.TeamsRankings.Count)
                            startIndex += _numRowsPerScreen;
                        else
                        {
                            showTeams = false;
                            startIndex = 0;
                            rowsRange = _numRowsPerScreen;
                        }
                    }
                    else
                    {
                        showTeams = false;
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

                        if ((startIndex + _numRowsPerScreen) < _rankings.PlayersChickenHandsRankings.Count)
                            startIndex += _numRowsPerScreen;
                        else
                        {
                            showPlayers = true;
                            startIndex = 0;
                            rowsRange = _numRowsPerScreen;
                        }
                    }
                    else
                    {
                        showPlayers = true;
                        startIndex = 0;
                        rowsRange = _numRowsPerScreen;
                    }
                }
                for (int i = 0; i < MAX_PAGE_SHOW_TIME * 2; i++)
                {
                    Thread.Sleep(500); //0.5 seconds
                    if (_stopShow)
                        break;
                }
            }
            _form.CloseFormFromThread();
        }

        #endregion
    }
}
