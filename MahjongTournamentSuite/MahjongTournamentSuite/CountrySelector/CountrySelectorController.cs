using System.Collections.Generic;

namespace MahjongTournamentSuite.CountrySelector
{
    class CountrySelectorController : ICountrySelectorController
    {
        #region Fields

        private ICountrySelectorForm _form;
        private ICountrySelectorDataManager _data;
        private List<string> _countriesNames;

        #endregion

        #region Constructor

        public CountrySelectorController(ICountrySelectorForm countrySelectorForm)
        {
            _form = countrySelectorForm;
            _data = Injector.provideDataManager();
        }

        #endregion

        #region ICountrySelectorController implementation

        public void LoadForm(bool loadOnlyWhichHaveHtmlFlag)
        {
            _countriesNames = new List<string>();
            _countriesNames.Add("No country");
            _countriesNames.AddRange(loadOnlyWhichHaveHtmlFlag ? 
                _data.GetCountriesNamesWichHaveImageUrl() : _data.GetCountriesNames());
            _form.FillLbCountries(_countriesNames);
        }

        #endregion
    }
}
