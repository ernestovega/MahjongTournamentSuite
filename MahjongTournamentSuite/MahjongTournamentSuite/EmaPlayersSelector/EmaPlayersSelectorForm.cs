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
            ActiveControl = tbFilter;
            _controller.LoadForm(_tournamentId);
            Cursor = Cursors.Default;
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            _controller.FilterList(tbFilter.Text);
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
            if (emaPlayersNames.Count > 0)
                lbEmaPlayers.SelectedIndex = 0;
        }

        #endregion

        #region Private

        private void CloseReturningValue()
        {
            string selectedItem = (string)lbEmaPlayers.SelectedItem;
            if (selectedItem.Equals(string.Empty))
            {
                ReturnValue = string.Empty;
            }
            else
            {
                ReturnValue = selectedItem.Substring(selectedItem.LastIndexOf(" - "))
                                          .Replace(" - ", string.Empty);
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion
    }
}
