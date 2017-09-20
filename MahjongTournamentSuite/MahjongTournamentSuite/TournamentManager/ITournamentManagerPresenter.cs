﻿namespace MahjongTournamentSuite.TournamentManager
{
    interface ITournamentManagerPresenter
    {
        void LoadTournament(int tournamentId);

        void OnFormResized();
        
        void ShowRankingsClicked();

        void ButtonTeamsClicked();

        void TeamsManagerFormClosed();

        void ButtonPlayersClicked();

        void PlayersManagerFormClosed(bool isWrongTeams);

        void ButtonRoundClicked(int roundId);

        void ButtonTableClicked(int tableId);

        void PlayersTablesClicked();

        bool IsRoundCompleted(int roundId);

        bool IsTableCompleted(int tableId);
        void TableManagerFormClosed();
    }
}
