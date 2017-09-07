using MahjongTournamentSuiteDataLayer.Data;
using MahjongTournamentSuiteDataLayer.Model;
using System.Collections.Generic;

namespace MahjongTournamentSuite.CountryManager
{
    class CountryManagerPresenter : ICountryManagerPresenter
    {
        #region Fields

        private ICountryManagerForm _form;
        private IDBManager _db;
        private List<DBCountry> _countries;

        #endregion

        #region Constructor

        public CountryManagerPresenter(ICountryManagerForm countryManagerForm)
        {
            _form = countryManagerForm;
            _db = Injector.provideDBManager();
        }

        #endregion

        #region ICountryManagerPresenter implementation

        public void LoadForm()
        {
            _countries = _db.GetCountries();
            _form.FillDGV(_countries);
        }

        public void CountryImageURLChanged(int countryId, string newValue)
        {
            _db.UpdateCountryImageURL(countryId, newValue);
        }

        #endregion

        #region Private

        #endregion
    }
}
