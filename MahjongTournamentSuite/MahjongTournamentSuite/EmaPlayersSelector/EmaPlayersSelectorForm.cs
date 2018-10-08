using MahjongTournamentSuite.CountrySelector;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MahjongTournamentSuite.EmaPlayersSelector
{
    public partial class EmaPlayersSelectorForm : Form, IEmaPlayersSelectorForm
    {
        #region Fields

        private IEmaPlayersSelectorController _controller;
        public string ReturnValue { get; set; }

        #endregion

        #region Constructor

        public EmaPlayersSelectorForm()
        {
            InitializeComponent();
            _controller = Injector.provideEmaPlayersSelectorController(this);
        }

        #endregion

        #region Events

        private void CountrySelectorForm_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CancelButton = btnCancel;
            AcceptButton = btnOk;
            _controller.LoadForm();
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

        #region ICountrySelectorForm implementation

        public void FillLbEmaPlayersNames(List<string> emaPlayersNames)
        {
            lbEmaPlayers.DataSource = emaPlayersNames;

            int heightIncrement = (emaPlayersNames.Count * lbEmaPlayers.ItemHeight) - lbEmaPlayers.Height;
            if (Height + heightIncrement > 600)
                Height = 600;
            else
                Height += heightIncrement;

            lbEmaPlayers.SelectedIndex = 0;
        }

        #endregion

        #region Private

        public void CloseReturningValue()
        {
            ReturnValue = ((string)lbEmaPlayers.SelectedItem).Split('-')[0];
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion
    }
}
