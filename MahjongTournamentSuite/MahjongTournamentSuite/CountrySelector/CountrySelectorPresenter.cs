﻿using MahjongTournamentSuiteDataLayer.Data;
using System.Collections.Generic;

namespace MahjongTournamentSuitePresentationLayer.CountrySelector
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
