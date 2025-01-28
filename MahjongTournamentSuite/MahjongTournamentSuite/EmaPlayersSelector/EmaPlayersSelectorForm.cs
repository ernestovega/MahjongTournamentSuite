using MahjongTournamentSuite._Data.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MahjongTournamentSuite.EmaPlayersSelector
{
    public partial class EmaPlayersSelectorForm : Form, IEmaPlayersSelectorForm
    {
        #region Fields

        private IEmaPlayersSelectorController _controller;
        private int _tournamentId;
        private List<string> _allEmaPlayersNames = new List<string>();
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

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            var filteredPlayers = _allEmaPlayersNames.Where(player => player.ToLower().Contains(tbFilter.Text.Trim())).ToList();
            var result = new List<string>();
            result.AddRange(filteredPlayers);
            FillLbEmaPlayersNames(result);
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
            if (_allEmaPlayersNames.Count == 0)
            {
                _allEmaPlayersNames = emaPlayersNames.ToList();
            }
            lbEmaPlayers.DataSource = emaPlayersNames;
            if (emaPlayersNames.Count > 0)
            {
                lbEmaPlayers.SelectedIndex = 0;
                btnOk.Enabled = true;
            } else
            {
                btnOk.Enabled = false;
            }
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
