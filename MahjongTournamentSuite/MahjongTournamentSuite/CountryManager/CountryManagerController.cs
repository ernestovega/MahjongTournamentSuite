using MahjongTournamentSuite.ViewModel;
using MahjongTournamentSuite.Resources.flags;
using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;

namespace MahjongTournamentSuite.CountryManager
{
    class CountryManagerController : ICountryManagerController
    {
        #region Fields

        private ICountryManagerForm _form;
        private ICountryManagerDataManager _data;
        private List<VCountry> _countries;
        List<DGVCountry> _dgvCountries;

        #endregion

        #region Constructor

        public CountryManagerController(ICountryManagerForm countryManagerForm)
        {
            _form = countryManagerForm;
            _data = Injector.provideDataManager();
        }

        #endregion

        #region ICountryManagerController implementation

        public void LoadForm()
        {
            _countries = _data.GetCountries();
            _dgvCountries = new List<DGVCountry>(_countries.Count);
            foreach (VCountry country in _countries)
            {
                _dgvCountries.Add(new DGVCountry(country, 
                    CountryFlags.GetFlagImage(country.CountryName)));
            }
            _form.FillDGV(_dgvCountries);
        }

        public void CountryImageURLChanged(string countryName, string newValue)
        {
            _data.UpdateCountryImageURL(countryName, newValue);
        }

        public void CountryShortNameChanged(string countryName, string newValue)
        {
            _data.UpdateCountryShortName(countryName, newValue);
        }

        #endregion
    }
}
