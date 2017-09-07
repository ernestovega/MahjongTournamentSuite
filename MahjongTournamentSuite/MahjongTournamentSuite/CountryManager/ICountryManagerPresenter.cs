namespace MahjongTournamentSuite.CountryManager
{
    interface ICountryManagerPresenter
    {
        void LoadForm();

        void CountryImageURLChanged(int countryId, string newValue);
    }
}
