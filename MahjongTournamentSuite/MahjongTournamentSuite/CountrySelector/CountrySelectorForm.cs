using MahjongTournamentSuite.CountryManager;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MahjongTournamentSuite.CountrySelector
{
    public partial class CountrySelectorForm : Form, ICountrySelectorForm
    {
        #region Fields

        private ICountrySelectorPresenter _presenter;
        public string ReturnValue { get; set; }

        #endregion

        #region Constructor

        public CountrySelectorForm()
        {
            InitializeComponent();
            _presenter = Injector.provideCountrySelectorPresenter(this);
        }

        #endregion

        #region Events

        private void CountrySelectorForm_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CancelButton = btnCancel;
            AcceptButton = btnOk;
            _presenter.LoadForm();
            Cursor = Cursors.Default;
        }

        private void lbCountries_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CloseReturningValue();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lbCountries.SelectedIndex >= 0)
                CloseReturningValue();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region ICountrySelectorForm implementation

        public void FillLbCountries(List<string> countries)
        {
            lbCountries.DataSource = countries;

            int heightIncrement = (countries.Count * lbCountries.ItemHeight) - lbCountries.Height;
            if (Height + heightIncrement > 600)
                Height = 600;
            else
                Height += heightIncrement;

            lbCountries.SelectedIndex = 0;
        }

        #endregion

        #region Private

        public void CloseReturningValue()
        {
            ReturnValue = (string)lbCountries.SelectedItem;
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion
    }
}
