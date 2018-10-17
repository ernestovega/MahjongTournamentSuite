namespace MahjongTournamentSuite.CountryManager
{
    interface ICountryManagerController
    {
        void LoadForm();

        void CountryImageURLChanged(string countryName, string newValue);

        void CountryShortNameChanged(string countryName, string newValue);
    }
}
