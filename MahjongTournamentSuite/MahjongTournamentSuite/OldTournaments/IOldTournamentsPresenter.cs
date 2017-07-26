namespace MahjongTournamentSuite.OldTournaments
{
    internal interface IOldTournamentsPresenter
    {
        void UpdateName(int tournamentId, string newName);
        void DeleteTournament(int tournamentId);
    }
}