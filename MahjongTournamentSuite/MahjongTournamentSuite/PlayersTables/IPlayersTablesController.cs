
namespace MahjongTournamentSuite.PlayersTables
{
    interface IPlayersTablesController
    {
        void LoadForm(int tournamentId);

        void ButtonPlayersCardsClicked();

        void ButtonPlayerClicked(int playerId);
    }
}
