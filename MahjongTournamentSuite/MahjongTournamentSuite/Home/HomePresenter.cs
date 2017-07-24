using System;
using System.Windows.Forms;
using MahjongTournamentTimer;
using System.Diagnostics;
using System.Collections.Generic;

namespace MahjongTournamentSuite.Home
{
    class HomePresenter : IHomePresenter
    {
        #region Fields

        private IHomeForm _form;

        #endregion

        #region Constructor

        public HomePresenter(IHomeForm homeForm)
        {
            _form = homeForm;
        }

        #endregion

        #region IHomePresenter implementation

        public void LoadOldTournamentsList()
        {
            var items = new List<ListViewItem>();
            items.Add(new ListViewItem(new string[] { "1", "Torneo Prueba 1", new DateTime().ToLocalTime().ToString() }));
            items.Add(new ListViewItem(new string[] { "2", "Torneo Prueba 2", new DateTime().ToLocalTime().ToString() }));
            items.Add(new ListViewItem(new string[] { "3", "Torneo Prueba 3", new DateTime().ToLocalTime().ToString() }));
            items.Add(new ListViewItem(new string[] { "4", "Torneo Prueba 4", new DateTime().ToLocalTime().ToString() }));
            items.Add(new ListViewItem(new string[] { "5", "Torneo Prueba 5", new DateTime().ToLocalTime().ToString() }));
            _form.LoadList(items);
        }

        public void TimerClicked()
        {
            var mahjongTournamentTimer = new MahjongTournamentTimer.Program();
            Process.Start(mahjongTournamentTimer.returnPath());
        }

        public void NewClicked()
        {
            
        }

        public void ResumeClicked(int tournamentId)
        {
            
        }

        #endregion
    }
}
