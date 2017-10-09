using System.Threading;
using MahjongTournamentSuite.ViewModel;

namespace MahjongTournamentSuite.Ranking
{
    class RankingController : IRankingController
    {
        #region Constants

        public const int DEFAULT_NUM_ROWS_PER_SCREEN = 20;
        private static readonly int MAX_PAGE_SHOW_TIME = 7;

        #endregion

        #region Fields
        
        private IRankingForm _form;
        private Rankings _rankings;
        private ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        private ManualResetEvent _pauseEvent = new ManualResetEvent(true);
        private Thread _showRankingThread;
        private int _numRowsPerScreen;
        private int _showTime = MAX_PAGE_SHOW_TIME;

        #endregion

        #region Constructor

        public RankingController(IRankingForm mainForm)
        {
            _form = mainForm;
        }

        #endregion

        #region IMainController implementation

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

            _form.CloseForm();
        }

        public void IncrementShowingTime()
        {
            _showTime++;
            _form.SetNumSecondsLabel(_showTime.ToString());
            if (_showTime == 2)
                _form.ShowButtonSecondsDown();
            
        }

        public void DecrementShowingTime()
        {
            _showTime--;
            _form.SetNumSecondsLabel(_showTime.ToString());
            if (_showTime == 1)
                _form.HideButtonSecondsDown();
        }

        public void IncrementShowingRows()
        {
            _numRowsPerScreen++;
            _form.SetNumRowsLabel(_numRowsPerScreen.ToString());
            if (_numRowsPerScreen == 9)
                _form.ShowButtonRowsDown();

        }

        public void DecrementShowingRows()
        {
            _numRowsPerScreen--;
            _form.SetNumRowsLabel(_numRowsPerScreen.ToString());
            if (_numRowsPerScreen == 8)
                _form.HideButtonRowsDown();
        }

        public void PlayClicked()
        {
            _pauseEvent.Set();
            _form.HideButtonPlay();
            _form.ShowButtonPause();
            _form.HideButtonRowsUp();
            _form.HideButtonRowsDown();
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
            int rowsRange;

            while (true)
            {
                if (_shutdownEvent.WaitOne(0))
                    break;

                _pauseEvent.WaitOne(Timeout.Infinite);

                #region Show
                if (showTeams)
                {
                    if (_rankings.IsTeams)
                    {
                        rowsRange = _numRowsPerScreen;
                        if ((startIndex + rowsRange) > _rankings.TeamsRankings.Count)
                            rowsRange -= (startIndex + rowsRange) - _rankings.TeamsRankings.Count;

                        if (_shutdownEvent.WaitOne(0))
                            break;

                        _pauseEvent.WaitOne(Timeout.Infinite);

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
                    rowsRange = _numRowsPerScreen;
                    if ((startIndex + rowsRange) > _rankings.PlayersRankings.Count)
                        rowsRange -= (startIndex + rowsRange) - _rankings.PlayersRankings.Count;

                    if (_shutdownEvent.WaitOne(0))
                        break;

                    _pauseEvent.WaitOne(Timeout.Infinite);

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
                        rowsRange = _numRowsPerScreen;
                        if ((startIndex + rowsRange) > _rankings.PlayersChickenHandsRankings.Count)
                            rowsRange -= (startIndex + rowsRange) - _rankings.PlayersChickenHandsRankings.Count;

                        if (_shutdownEvent.WaitOne(0))
                            break;

                        _pauseEvent.WaitOne(Timeout.Infinite);

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
                #endregion
            }
        }

        private void SleepRankingPage()
        {
            for (int i = 0; i <= _showTime; i++)
            {
                if (_shutdownEvent.WaitOne(0))
                    break;

                _pauseEvent.WaitOne(Timeout.Infinite);

                _form.UpdateProgressFromThread((_showTime - i).ToString());

                Thread.Sleep(990);
            }
        }

        #endregion
    }
}
