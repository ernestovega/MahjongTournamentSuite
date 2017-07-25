using System;
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
        }

        #endregion

        #region Events

        private void imgLogoMM_Click(object sender, EventArgs e)
        {

        }

        private void imgLogoEMA_Click(object sender, EventArgs e)
        {

        }
        
        private void btnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
