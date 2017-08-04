using MahjongTournamentSuite.Data;
using MahjongTournamentSuite.Home;
using MahjongTournamentSuite.NewTournament;
using MahjongTournamentSuite.TableManager;
using MahjongTournamentSuite.TournamentManager;

namespace MahjongTournamentSuite
{
    class Injector
    {
        internal static IDBManager provideDBManager()
        {
            return new DBManager();
        }

        internal static IHomePresenter provideHomePresenter(HomeForm homeForm)
        {
            return new HomePresenter(homeForm);
        }

        internal static INewTournamentPresenter provideNewTournamentPresenter(NewTournamentForm newTournamentForm)
        {
            return new NewTournamentPresenter(newTournamentForm);
        }

        internal static ITournamentManagerPresenter provideTournamentManagerPresenter(TournamentManagerForm tournamentManagerForm)
        {
            return new TournamentManagerPresenter(tournamentManagerForm);
        }

        internal static ITableManagerPresenter provideTableManagerPresenter(TableManagerForm tableManagerForm)
        {
            return new TableManagerPresenter(tableManagerForm);
        }        
    }
}
