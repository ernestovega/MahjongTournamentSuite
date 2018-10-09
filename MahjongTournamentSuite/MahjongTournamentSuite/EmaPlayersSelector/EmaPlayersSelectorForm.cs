using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MahjongTournamentSuite.EmaPlayersSelector
{
    public partial class EmaPlayersSelectorForm : Form, IEmaPlayersSelectorForm
    {
        #region Fields

        private IEmaPlayersSelectorController _controller;
        private int _tournamentId;
        public string ReturnValue { get; set; }

        #endregion

        #region Constructor

        public EmaPlayersSelectorForm(int tournamentId)
        {
            InitializeComponent();
            _controller = Injector.provideEmaPlayersSelectorController(this);
            _tournamentId = tournamentId;
        }

        #endregion

        #region Events

        private void EmaPlayersSelectorForm_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CancelButton = btnCancel;
            AcceptButton = btnOk;
            _controller.LoadForm(_tournamentId);
            Cursor = Cursors.Default;
        }

        private void lbEmaPlayers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CloseReturningValue();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lbEmaPlayers.SelectedIndex >= 0)
                CloseReturningValue();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region IEmaPlayersSelectorForm implementation

        public void FillLbEmaPlayersNames(List<string> emaPlayersNames)
        {
            lbEmaPlayers.DataSource = emaPlayersNames;
            lbEmaPlayers.SelectedIndex = 0;
        }

        #endregion

        #region Private

        public void CloseReturningValue()
        {
            ReturnValue = ((string)lbEmaPlayers.SelectedItem);
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion
    }
}
