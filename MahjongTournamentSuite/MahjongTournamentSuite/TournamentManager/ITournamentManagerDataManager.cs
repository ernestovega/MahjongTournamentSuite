using System.Collections.Generic;
using MahjongTournamentSuite._Data.DataModel;

namespace MahjongTournamentSuite.TournamentManager
{
    public interface ITournamentManagerDataManager
    {
        VTournament GetTournament(int tournamentId);

        List<VTable> GetTournamentTables(int tournamentId);

        List<VPlayer> GetTournamentPlayers(int tournamentId);

        List<VTeam> GetTournamentTeams(int tournamentId);

        void RefreshTeams(int tournamentId);

        void RefreshPlayers(int tournamentId);

        void RefreshTable(int tournamentId, int roundSelected, int tableSelected);

        List<VHand> GetTournamentHands(int _tournamentId);

        string GetCountryImageUrl(string playerCountryName);

        string GetCountryShortName(string playerCountryName);

        VEmaPlayer GetEmaPlayer(string playerEmaNumber);
    }
}