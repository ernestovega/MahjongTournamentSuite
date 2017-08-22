using System;
using System.Collections.Generic;
using MahjongTournamentSuite.Data;
using MahjongTournamentSuite.Model;

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

        public void LoadCountries()
        {
            _countriesNames = _db.GetCountriesNames();
            _form.FillLbCountries(_countriesNames);
        }

        #endregion
    }
}
