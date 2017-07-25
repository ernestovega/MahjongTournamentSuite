namespace MahjongTournamentSuite.OldTournaments
{
    internal class OldTournamentsPresenter : IOldTournamentsPresenter
    {
        private OldTournamentsForm _oldTournamentsForm;

        public OldTournamentsPresenter(OldTournamentsForm oldTournamentsForm)
        {
            this._oldTournamentsForm = oldTournamentsForm;
        }
    }
}