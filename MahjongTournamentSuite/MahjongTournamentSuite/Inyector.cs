using System;
using MahjongTournamentSuite.Data;
using MahjongTournamentSuite.Home;

namespace MahjongTournamentSuite
{
    class Injector
    {
        internal static IDBManager provideTournamentsDBManager()
        {
            return new DBManager();
        }

        internal static IHomePresenter provideHomePresenter(HomeForm homeForm)
        {
            return new HomePresenter(homeForm);
        }
    }
}
