using MahjongTournamentSuite._Data.Interfaces;
using MahjongTournamentSuite.CountryManager;
using MahjongTournamentSuite.CountrySelector;
using MahjongTournamentSuite.Home;
using MahjongTournamentSuite.PlayersManager;
using MahjongTournamentSuite.PlayersTables;
using MahjongTournamentSuite.TableManager;
using MahjongTournamentSuite.TeamSelector;
using MahjongTournamentSuite.TeamsManager;
using MahjongTournamentSuite.TournamentManager;

namespace MahjongTournamentSuite._Data
{
    public interface IDBManager : 
        IHomeDataManager, 
        INewTournamentDataManager, 
        ICountryManagerDataManager,
        IPlayersManagerDataManager,
        IPlayersTablesDataManager,
        ITableManagerDataManager,
        ICountrySelectorDataManager, 
        ITeamSelectorDataManager,
        ITeamsManagerDataManager,
        ITournamentManagerDataManager {}
}