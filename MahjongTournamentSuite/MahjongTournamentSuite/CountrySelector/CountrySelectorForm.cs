using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MahjongTournamentSuitePresentationLayer.CountrySelector
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
            _presenter.LoadCountries();
            CancelButton = btnCancel;
            AcceptButton = btnOk;
        }

        #endregion

        #region Events

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
