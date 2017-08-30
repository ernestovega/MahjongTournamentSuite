using MahjongTournamentSuitePresentationLayer.CountrySelector;
using MahjongTournamentSuitePresentationLayer.Home;
using MahjongTournamentSuitePresentationLayer.NewTournament;
using MahjongTournamentSuitePresentationLayer.TableManager;
using MahjongTournamentSuitePresentationLayer.TeamSelector;
using MahjongTournamentSuitePresentationLayer.TournamentManager;
using MahjongTournamentSuiteDataLayer.Data;

namespace MahjongTournamentSuitePresentationLayer
{
    class Injector
    {
        internal static IDBManager provideDBManager()
        {
            return new DBManager();
        }

        internal static IHomePresenter provideHomePresenter(
            HomeForm homeForm)
        {
            return new HomePresenter(homeForm);
        }

        internal static INewTournamentPresenter provideNewTournamentPresenter(
            NewTournamentForm newTournamentForm)
        {
            return new NewTournamentPresenter(newTournamentForm);
        }

        internal static ITournamentManagerPresenter provideTournamentManagerPresenter(
            TournamentManagerForm tournamentManagerForm)
        {
            return new TournamentManagerPresenter(tournamentManagerForm);
        }

        internal static ITeamSelectorPresenter provideTeamSelectorPresenter(
            TeamSelectorForm teamSelectorForm)
        {
            return new TeamSelectorPresenter(teamSelectorForm);
        }

        internal static ICountrySelectorPresenter provideCountrySelectorPresenter(
            CountrySelectorForm countrySelectorForm)
        {
            return new CountrySelectorPresenter(countrySelectorForm);
        }

        internal static ITableManagerPresenter provideTableManagerPresenter(
            TableManagerForm tableManagerForm)
        {
            return new TableManagerPresenter(tableManagerForm);
        }        
    }
}
