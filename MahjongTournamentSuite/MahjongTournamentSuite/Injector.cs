using MahjongTournamentSuite.CountrySelector;
using MahjongTournamentSuite.Home;
using MahjongTournamentSuite.HTMLViewer;
using MahjongTournamentSuite.NewTournament;
using MahjongTournamentSuite.Ranking;
using MahjongTournamentSuite.TableManager;
using MahjongTournamentSuite.TeamSelector;
using MahjongTournamentSuite.TournamentManager;
using MahjongTournamentSuiteDataLayer.Data;
using MahjongTournamentSuite.CountryManager;
using MahjongTournamentSuite.PlayersManager;
using MahjongTournamentSuite.TeamsManager;
using MahjongTournamentSuite.PlayersTables;
using MahjongTournamentSuite.EmaReport;

namespace MahjongTournamentSuite
{
    class Injector
    {
        internal static IDBManager provideDBManager()
        {
            return new DBManager();
        }

        internal static IHomePresenter provideHomePresenter(
            IHomeForm homeForm)
        {
            return new HomePresenter(homeForm);
        }

        internal static INewTournamentPresenter provideNewTournamentPresenter(
            INewTournamentForm newTournamentForm)
        {
            return new NewTournamentPresenter(newTournamentForm);
        }

        internal static ITournamentManagerPresenter provideTournamentManagerPresenter(
            ITournamentManagerForm tournamentManagerForm)
        {
            return new TournamentManagerPresenter(tournamentManagerForm);
        }

        internal static ITeamSelectorPresenter provideTeamSelectorPresenter(
            ITeamSelectorForm teamSelectorForm)
        {
            return new TeamSelectorPresenter(teamSelectorForm);
        }

        internal static ICountrySelectorPresenter provideCountrySelectorPresenter(
            ICountrySelectorForm countrySelectorForm)
        {
            return new CountrySelectorPresenter(countrySelectorForm);
        }

        internal static ITableManagerPresenter provideTableManagerPresenter(
            ITableManagerForm tableManagerForm)
        {
            return new TableManagerPresenter(tableManagerForm);
        }

        internal static IRankingPresenter provideRankingPresenter(
            IRankingForm rankingForm)
        {
            return new RankingPresenter(rankingForm);
        }

        internal static IHTMLViewerPresenter provideHTMLViewerPresenter(
            IHTMLViewerForm htmlViewerForm)
        {
            return new HTMLViewerPresenter(htmlViewerForm);
        }

        internal static ICountryManagerPresenter provideCountryManagerPresenter(
            ICountryManagerForm countryManagerForm)
        {
            return new CountryManagerPresenter(countryManagerForm);
        }

        internal static IPlayersManagerPresenter providePlayersManagerPresenter(
            IPlayersManagerForm playersManagerForm)
        {
            return new PlayersManagerPresenter(playersManagerForm);
        }

        internal static ITeamsManagerPresenter provideTeamsManagerPresenter(ITeamsManagerForm teamsManagerForm)
        {
            return new TeamsManagerPresenter(teamsManagerForm);
        }

        internal static IPlayersTablesPresenter providePlayersTablesPresenter(IPlayersTablesForm playersTablesForm)
        {
            return new PlayersTablesPresenter(playersTablesForm);
        }

        internal static IEmaReportPresenter provideEmaReportPresenter(IEmaReportForm emaReportForm)
        {
            return new EmaReportPresenter(emaReportForm);
        }
    }
}
