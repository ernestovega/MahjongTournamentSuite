using System;
using MahjongTournamentSuite.CountrySelector;
using MahjongTournamentSuite.Home;
using MahjongTournamentSuite.HTMLViewer;
using MahjongTournamentSuite.NewTournament;
using MahjongTournamentSuite.Ranking;
using MahjongTournamentSuite.TableManager;
using MahjongTournamentSuite.TeamSelector;
using MahjongTournamentSuite.TournamentManager;
using MahjongTournamentSuiteDataLayer.Data;

namespace MahjongTournamentSuite
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

        internal static IRankingPresenter provideRankingPresenter(
            RankingForm rankingForm)
        {
            return new RankingPresenter(rankingForm);
        }

        internal static IHTMLViewerPresenter provideHTMLViewerPresenter(
            HTMLViewerForm htmlViewerForm)
        {
            return new HTMLViewerPresenter(htmlViewerForm);
        }
    }
}
