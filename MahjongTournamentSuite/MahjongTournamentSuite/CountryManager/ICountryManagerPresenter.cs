namespace MahjongTournamentSuite.CountryManager
{
    interface ICountryManagerPresenter
    {
        void LoadForm();

        void CountryImageURLChanged(string countryName, string newValue);
    }
}
