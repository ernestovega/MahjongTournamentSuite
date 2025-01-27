using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;
using MahjongTournamentSuite.ViewModel;

namespace MahjongTournamentSuite.PlayersTables
{
    class PlayersTablesController : IPlayersTablesController
    {
        #region Fields

        private IPlayersTablesForm _form;
        private IPlayersTablesDataManager _data;
        private VTournament _tournament;
        private List<VPlayer> _players;
        private List<VTable> _tables;

        #endregion

        #region Constructor

        public PlayersTablesController(IPlayersTablesForm form)
        {
            _form = form;
            _data = Injector.provideDataManager();
        }

        #endregion

        #region IPlayersTablesController implementation

        public void LoadForm(int tournamentId)
        {
            _tournament = _data.GetTournament(tournamentId);
            _players = _data.GetTournamentPlayers(tournamentId);
            _tables = _data.GetTournamentTables(tournamentId);
            _form.GeneratePlayersButtons(_players.Count);
        }

        public void ButtonPlayersCardsClicked()
        {
            List<VEmaPlayer> emaPlayers = _data.GetEmaPlayers();
            List<VTeam> teams = _data.GetTournamentTeams(_tournament.TournamentId);
            List<DGVPlayerCard> dgvPlayerCards = new List<DGVPlayerCard>(_tournament.NumPlayers);
            foreach (var player in _players)
            {
                VEmaPlayer vEmaPlayer = new VEmaPlayer();
                foreach (var emaPlayer in emaPlayers) {
                    if (emaPlayer.EmaPlayerEmaNumber == player.PlayerEmaNumber)
                    {
                        vEmaPlayer = emaPlayer;
                        break;
                    }
                }
                string teamName = "";
                foreach (var team in teams)
                {
                    if (team.TeamId == player.PlayerTeamId)
                    {
                        teamName = team.TeamName;
                        break;
                    }
                }
                List<DGVPlayerTable> playerTables = getPlayerTables(player.PlayerId);
                dgvPlayerCards.Add(
                    new DGVPlayerCard(
                        player.PlayerId,  
                        vEmaPlayer.EmaPlayerName, 
                        vEmaPlayer.EmaPlayerLastName, 
                        teamName, 
                        player.PlayerCountryName,
                        playerTables[0].TableId,
                        playerTables[1].TableId,
                        playerTables[2].TableId,
                        playerTables[3].TableId,
                        playerTables[4].TableId,
                        playerTables[5].TableId,
                        playerTables[6].TableId
                    )
                );
            }
            _form.ShowPlayersCards(dgvPlayerCards);
        }

        public void ButtonPlayerClicked(int playerId)
        {
            List<DGVPlayerTable> dgvPlayerTables = getPlayerTables(playerId);
            string playerName = _players.Find(x => x.PlayerId == playerId).PlayerName;

            _form.ShowPlayerTables(new PlayerTables(playerId, playerName, dgvPlayerTables));
        }

        #endregion

        #region Private

        private List<DGVPlayerTable> getPlayerTables(int playerId)
        {
            List<DGVPlayerTable> dgvPlayerTables = new List<DGVPlayerTable>(_tournament.NumRounds);
            for (int i = 1; i <= _tournament.NumRounds; i++)
            {
                int tableId = _tables.Find(x => x.TableRoundId == i &&
                    (x.Player1Id == playerId || x.Player2Id == playerId ||
                    x.Player3Id == playerId || x.Player4Id == playerId))
                    .TableId;
                dgvPlayerTables.Add(new DGVPlayerTable(i, tableId));
            }
            return dgvPlayerTables;
        }

        #endregion
    }
}
