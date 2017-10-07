
namespace MahjongTournamentSuite.PlayersTables
{
    interface IPlayersTablesController
    {
        void LoadForm(int tournamentId);

        void ButtonPlayerClicked(int playerId);
    }
}
