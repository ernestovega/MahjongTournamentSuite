using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MahjongTournamentSuite.Home
{
    public partial class HomeForm : Form, IHomeForm
    {
        #region Fields

        private IHomePresenter _presenter;

        #endregion

        #region Constructor

        public HomeForm()
        {
            InitializeComponent();
            _presenter = new HomePresenter(this);
            _presenter.LoadOldTournamentsList();
        }

        #endregion

        #region Events

        public void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        public void btnTimer_Click(object sender, EventArgs e)
        {
            _presenter.TimerClicked();
        }

        public void btnNew_Click(object sender, EventArgs e)
        {
            _presenter.NewClicked();
        }

        public void btnResume_Click(object sender, EventArgs e)
        {
            //_presenter.ResumeClicked(int.Parse(lvOldTournaments.SelectedItems[0].SubItems[0].Text));
        }

        public void lvOldTournaments_DoubleClick(object sender, EventArgs e)
        {
            //_presenter.ResumeClicked(int.Parse(lvOldTournaments.SelectedItems[0].SubItems[0].Text));
        }

        #endregion

        #region IHomeForm implementation

        public void LoadList(List<ListViewItem> tournamentItems)
        {
            //foreach (var item in tournamentItems)
            //{
            //    lvOldTournaments.Items.Add(item);
            //}
        }

        #endregion
    }
}
