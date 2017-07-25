using System;
using System.Windows.Forms;

namespace MahjongTournamentSuite.NewTournament
{
    public partial class NewTournamentForm : Form, INewTournamentForm
    {
        #region Fields

        private INewTournamentPresenter _presenter;

        #endregion

        #region Constructor

        public NewTournamentForm()
        {
            InitializeComponent();
            _presenter = new NewTournamentPresenter(this);
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

        private void btnStart_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
