namespace MahjongTournamentSuite.CountryManager
{
    interface ICountryManagerController
    {
        void LoadForm();

        void CountryImageURLChanged(string countryName, string newValue);
    }
}
