using MahjongTournamentSuite.Model;
using System.Collections.Generic;

namespace MahjongTournamentSuite.TournamentManager
{
    interface ITournamentManagerPresenter
    {
        void LoadTournament(int tournamentId);

        void OnFormResized();

        void ButtonTeamsClicked();

        void ButtonPlayersClicked();

        void ButtonRoundsClicked();

        void ButtonRoundClicked(int roundId);

        void ButtonRoundTableClicked(int tableId);

        void TeamNameChanged(int teamId, string newValue);

        void PlayerNameChanged(int playerId, string newPlayerName);

        int SaveNewPlayerTeam(int playerId, string newTeamName);

        void PlayerTeamChanged();

        int SaveNewPlayerCountry(int playerId, string newCountryName);

        void ExportTournamentToExcel();
    }
}
