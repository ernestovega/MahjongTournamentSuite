
namespace MahjongTournamentSuite.PlayersTables
{
    interface IPlayersTablesPresenter
    {
        void LoadForm(int tournamentId);

        void ButtonPlayerClicked(int playerId);
    }
}
