using MahjongTournamentSuite.Model;
using MahjongTournamentSuite.Resources.flags;
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
        List<DGVCountry> _dgvCountries;

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
            _dgvCountries = new List<DGVCountry>(_countries.Count);
            foreach (DBCountry country in _countries)
            {
                _dgvCountries.Add(new DGVCountry(country, 
                    CountryFlags.GetFlagImage(country.CountryName)));
            }
            _form.FillDGV(_dgvCountries);
        }

        public void CountryImageURLChanged(string countryName, string newValue)
        {
            _db.UpdateCountryImageURL(countryName, newValue);
        }

        #endregion
    }
}
