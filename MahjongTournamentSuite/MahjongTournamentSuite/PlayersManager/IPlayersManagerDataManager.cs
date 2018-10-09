using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;

namespace MahjongTournamentSuite.PlayersManager
{
    public interface IPlayersManagerDataManager
    {
        VTournament GetTournament(int tournamentId);

        List<VPlayer> GetTournamentPlayers(int tournamentId);

        List<VTeam> GetTournamentTeams(int tournamentId);

        List<VCountry> GetCountries();

        void UpdatePlayerName(int tournamentId, int playerId, string newName);

        void UpdatePlayerTeam(int tournamentId, int playerId, int countryId);

        void AssignNewEmaPlayer(int tournamentId, int playerId, string emaNumber);

        void UnassignEmaPlayer(int tournamentId, int playerId);
    }
}