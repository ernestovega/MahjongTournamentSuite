using MahjongTournamentSuiteDataLayer.Data;
using System.Collections.Generic;

namespace MahjongTournamentSuite.CountrySelector
{
    class CountrySelectorPresenter : ICountrySelectorPresenter
    {
        #region Fields

        private ICountrySelectorForm _form;
        private IDBManager _db;
        private List<string> _countriesNames;

        #endregion

        #region Constructor

        public CountrySelectorPresenter(ICountrySelectorForm countrySelectorForm)
        {
            _form = countrySelectorForm;
            _db = Injector.provideDBManager();
        }

        #endregion

        #region ICountrySelectorPresenter implementation

        public void LoadForm()
        {
            _countriesNames = new List<string>();
            _countriesNames.Add("No country");
            _countriesNames.AddRange(_db.GetCountriesNamesWichHaveImageUrl());
            _form.FillLbCountries(_countriesNames);
        }

        #endregion
    }
}
