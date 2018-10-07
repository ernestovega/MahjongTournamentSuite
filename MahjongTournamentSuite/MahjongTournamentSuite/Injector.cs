using MahjongTournamentSuite.CountrySelector;
using MahjongTournamentSuite.Home;
using MahjongTournamentSuite.HTMLViewer;
using MahjongTournamentSuite.NewTournament;
using MahjongTournamentSuite.Ranking;
using MahjongTournamentSuite.TableManager;
using MahjongTournamentSuite.TeamSelector;
using MahjongTournamentSuite.TournamentManager;
using MahjongTournamentSuite.CountryManager;
using MahjongTournamentSuite.PlayersManager;
using MahjongTournamentSuite.TeamsManager;
using MahjongTournamentSuite.PlayersTables;
using MahjongTournamentSuite.EmaReport;
using MahjongTournamentSuite._Data;
using MahjongTournamentSuite.EmaPlayersManager;

namespace MahjongTournamentSuite
{
    class Injector
    {
        internal static DBManager provideDataManager()
        {
            return new DBManager();
        }

        internal static IHomeController provideHomeController(
            IHomeForm homeForm)
        {
            return new HomeController(homeForm);
        }

        internal static INewTournamentController provideNewTournamentController(
            INewTournamentForm newTournamentForm)
        {
            return new NewTournamentController(newTournamentForm);
        }

        internal static ITournamentManagerController provideTournamentManagerController(
            ITournamentManagerForm tournamentManagerForm)
        {
            return new TournamentManagerController(tournamentManagerForm);
        }

        internal static ITeamSelectorController provideTeamSelectorController(
            ITeamSelectorForm teamSelectorForm)
        {
            return new TeamSelectorController(teamSelectorForm);
        }

        internal static ICountrySelectorController provideCountrySelectorController(
            ICountrySelectorForm countrySelectorForm)
        {
            return new CountrySelectorController(countrySelectorForm);
        }

        internal static ITableManagerController provideTableManagerController(
            ITableManagerForm tableManagerForm)
        {
            return new TableManagerController(tableManagerForm);
        }

        internal static IRankingController provideRankingController(
            IRankingForm rankingForm)
        {
            return new RankingController(rankingForm);
        }

        internal static IHTMLViewerController provideHTMLViewerController(
            IHTMLViewerForm htmlViewerForm)
        {
            return new HTMLViewerController(htmlViewerForm);
        }

        internal static ICountryManagerController provideCountryManagerController(
            ICountryManagerForm countryManagerForm)
        {
            return new CountryManagerController(countryManagerForm);
        }

        internal static IPlayersManagerController providePlayersManagerController(IPlayersManagerForm playersManagerForm)
        {
            return new PlayersManagerController(playersManagerForm);
        }

        internal static ITeamsManagerController provideTeamsManagerController(ITeamsManagerForm teamsManagerForm)
        {
            return new TeamsManagerController(teamsManagerForm);
        }

        internal static IPlayersTablesController providePlayersTablesController(IPlayersTablesForm playersTablesForm)
        {
            return new PlayersTablesController(playersTablesForm);
        }

        internal static IEmaReportController provideEmaReportController(IEmaReportForm emaReportForm)
        {
            return new EmaReportController(emaReportForm);
        }

        internal static IEmaPlayersManagerController provideEmaPlayersManagerController(IEmaPlayersManagerForm emaPlayersManagerForm)
        {
            return new EmaPlayersManagerController(emaPlayersManagerForm);
        }
    }
}
