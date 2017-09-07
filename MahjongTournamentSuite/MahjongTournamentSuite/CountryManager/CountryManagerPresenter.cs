namespace MahjongTournamentSuite.CountryManager
{
    class CountryManagerPresenter : ICountryManagerPresenter
    {
        #region Fields

        private ICountryManagerForm _form;

        #endregion

        #region Constructor

        public CountryManagerPresenter(ICountryManagerForm countryManagerForm)
        {
            _form = countryManagerForm;
        }

        #endregion 

        #region ICountryManagerPresenter implementation

        #endregion 

        #region Private

        #endregion 
    }
}
