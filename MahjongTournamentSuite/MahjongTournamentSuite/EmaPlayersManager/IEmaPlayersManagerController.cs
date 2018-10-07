namespace MahjongTournamentSuite.EmaPlayersManager
{
    interface IEmaPlayersManagerController
    {
        void LoadForm();

        void EmaPlayerEmaNumberChanged(string oldEmaPlayerEmaNumber, string newEmaPlayerEmaNumber);

        void EmaPlayerLastNameChanged(string emaPlayerEmaNumber, string newEmaPlayerLastName);

        void EmaPlayerNameChanged(string emaPlayerEmaNumber, string newEmaPlayerName);

        void EmaPlayerCountryChanged(string emaPlayerEmaNumber, string newEmaPlayerCountryName);
    }
}
