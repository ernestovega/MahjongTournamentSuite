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
using MahjongTournamentSuite.CountryManager;
using MahjongTournamentSuite.ManageCountries;
using MahjongTournamentSuite.TemplateTableManager;

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

        internal static ICountryManagerPresenter provideCountryManagerPresenter(
            CountryManagerForm countryManagerForm)
        {
            return new CountryManagerPresenter(countryManagerForm);
        }

        internal static ITemplateTableManagerPresenter provideTemplateTableManagerPresenter(
            TemplateTableManagerForm templateTableManagerForm)
        {
            return new TemplateTableManagerPresenter(templateTableManagerForm);
        }
    }
}
