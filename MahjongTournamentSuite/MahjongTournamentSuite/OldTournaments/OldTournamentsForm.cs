using MahjongTournamentSuite.Model;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MahjongTournamentSuite.OldTournaments
{
    public partial class OldTournamentsForm : Form, IOldTournamentsForm
    {
        #region Fields

        private IOldTournamentsPresenter _presenter;

        #endregion

        #region Constructor

        public OldTournamentsForm()
        {
            InitializeComponent();
            _presenter = new OldTournamentsPresenter(this);
            _presenter.loadTournaments();
        }

        #endregion

        #region Events

        private void imgLogoMM_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://www.mahjongmadrid.com/");
            Process.Start(sInfo);
        }

        private void imgLogoEMA_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://mahjong-europe.org/portal/");
            Process.Start(sInfo);
        }
        
        private void btnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region IOldTournamentsForm implementation

        public void BindDataGrid()
        {
            //Table table = new Table(1, 1, 1, 2, 3, 4, "a", "b", "c", "d", "a", "b", "c", "d", "a", "b", "c", "d");
        }

        #endregion
    }
}
