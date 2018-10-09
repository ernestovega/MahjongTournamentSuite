using MahjongTournamentSuite._Data.DataModel;
using MahjongTournamentSuite.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MahjongTournamentSuite._Data.Interfaces;

namespace MahjongTournamentSuite.NewTournament
{
    class NewTournamentController : INewTournamentController
    {
        #region Constants
        
        private readonly int NUM_TABLE_HANDS = 16;

        #endregion

        #region Fields

        private INewTournamentForm _form;
        private INewTournamentDataManager _data;
        private VTournament _tournament;
        private List<Player> _players = new List<Player>();
        private List<TablePlayer> _tablePlayers = new List<TablePlayer>();
        private List<TableWithAll> _tablesWithAll = new List<TableWithAll>();
        private List<TableWithAll> _tablesByPlayer = new List<TableWithAll>();
        private List<Rivals> _rivalsByPlayer = new List<Rivals>();
        private int _numTriesMax, _numPlayers, _numRounds, _countTries;
        private Random random = new Random();
        private string _tournamentName = string.Empty;
        private bool _isTeamsChecked;

        #endregion

        #region Constructor

        public NewTournamentController(INewTournamentForm newTournamentForm)
        {
            _form = newTournamentForm;
            _data = Injector.provideDataManager();
        }

        #endregion

        #region INewTournamentController implementation

        public void StartClicked(string tournamentName)
        {
            if (_form.IsBackgroundWorkerBusy())
            {
                _form.CancelBackgroundWorker();
                _form.EnableViews();
            }
            else
            {
                if (tournamentName.Length > 0)
                {
                    if(_data.ExistTournamentByName(tournamentName))
                    {
                        _form.ShowMessageExistingTournamentName();
                        return;
                    }
                    _tournamentName = tournamentName;
                    if (InitializeCalculation())
                        _form.RunBackgroundWorker();
                }
                else
                    _form.ShowEnterTournamentNameMessage();
            }
        }

        public void BackgroundWorkerDoWork(BackgroundWorker worker, DoWorkEventArgs e)
        {
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            GeneratePlayers();

            //creationDate = string.Format("{0}{1}{2}_{3}{4}{5}", DateTime.Now.Year, DateTime.Now.Month,
            //    DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            //Cada vez que un cálculo es imposible se reintenta desde cero, tantas veces como se hayan indicado.
            int _result = -1;
            _countTries = 0;
            while (_result < 0 && _countTries < _numTriesMax)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                _countTries++;
                worker.ReportProgress(_countTries, null);
                _result = GenerateTournament(_numRounds);
                _form.ApplicationDoEvents();
            }

            if (_countTries >= _numTriesMax)
            {
                return;
            }

            worker.ReportProgress(_countTries, null);

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            
            GenerateTablesWithAll(_numRounds);

            worker.ReportProgress(0, null);

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            //try
            //{
                SaveTournament();
                if (_isTeamsChecked)
                    SaveTeams();
                //SavePlayersWithFakeData();
                //SaveTablesAndHandsWithFakeData();
                SavePlayers();
                SaveTablesAndHands();
            //}
            //catch(Exception ex)
            //{
            //    string message = ex.Message;
            //}
        }

        public void RunWorkerCompleted(bool isCancelled)
        {
            _form.EnableViews();

            if (isCancelled)
                return;
            else
            {
                /*Si no se ha podido calcular en los intentos indicados, 
                 se notifica,*/
                if (_countTries >= _numTriesMax)
                    _form.ShowReachedTriesMessage(_numTriesMax);
                else if (_tournament.TournamentId > 0)
                    _form.CloseFormReturningValue(_tournament.TournamentId);
                else
                    _form.ShowSomethingWentWrongMessage();
            }            
        }

        #endregion

        #region Private

        public bool InitializeCalculation()
        {
            _numPlayers = _form.GetNumPlayers();
            if ((_numPlayers / 4) * 4 < _numPlayers)
            {
                if (_form.ShowWrongPlayersNumberMessage(_numPlayers, (_numPlayers / 4) * 4))
                {
                    _form.SetNumUpDownPlayers((_numPlayers / 4) * 4);
                    _numPlayers = _form.GetNumPlayers();
                }
                else
                    return false;
            }
            _form.DisableViews();
            _numRounds = _form.GetNumRounds();
            _isTeamsChecked = _form.IsTeamsChecked();
            _numTriesMax = _form.GetNumTries();
            _form.SetTriesCounterLabel(1);
            return true;
        }
        
        private void GeneratePlayers()
        {
            _players.Clear();
            for (int i = 1; i <= _numPlayers / 4; i++)//Recorremos cada equipo
            {
                int teamId = (4 * i) / 4;//Generamos el id del equipo
                for (int j = 1; j <= 4; j++)//Recorremos cada jugador del equipo
                {
                    //Generamos el id del jugador
                    int playerId = (4 * i) - (4 - j);
                    //Creamos el jugador
                    Player player = new Player(playerId.ToString(), playerId.ToString(), teamId.ToString());
                    //Añadimos el jugador
                    _players.Add(player);
                }
            }
        }

        private int GenerateTournament(int numRounds)
        {
            //Limpiamos las tablas
            _tablePlayers.Clear();
            _tablesWithAll.Clear();
            _tablesByPlayer.Clear();
            _rivalsByPlayer.Clear();

            int currentRound, currentTable, currentTablePlayer;

            for (currentRound = 1; currentRound <= numRounds; currentRound++)
            {//Iteramos por rondas

                //Copiamos la lista de jugadores para ir borrando los que vayamos usando cada ronda
                List<int> playersNotUsedThisRound = _players.Select(x => x.Clone()).ToList().Select(x => x.Id).ToList();

                for (currentTable = 1; currentTable <= _players.Count / 4; currentTable++)
                {//Iteramos por mesas en cada ronda

                    for (currentTablePlayer = 1; currentTablePlayer <= 4; currentTablePlayer++)
                    {//Iteramos por jugador en cada mesa

                        //Copiamos la lista de jugadores para ir borrando los que vayamos descartando
                        int[] arrayPlayersIdsNotDiscarded = new int[playersNotUsedThisRound.Count];
                        playersNotUsedThisRound.CopyTo(arrayPlayersIdsNotDiscarded);
                        List<int> playersIdsNotDiscarded = new List<int>(arrayPlayersIdsNotDiscarded);

                        bool playerFounded = false;
                        //Si no hay jugador elegido y no hemos recorrido todos los jugadores lo reintentamos.
                        while (!playerFounded && playersIdsNotDiscarded.Count > 0)
                        {
                            //Obtenemos la lista de jugadores de la actual mesa
                            List<TablePlayer> currentTableTablePlayers = _tablePlayers.FindAll
                                (x => x.round == currentRound && x.table == currentTable).ToList();
                            List<Player> currentTablePlayers = new List<Player>();
                            foreach (TablePlayer tp in currentTableTablePlayers)
                                currentTablePlayers.Add(GetPlayerById(tp.playerId));

                            //Elegimos un jugador al azar y lo quitamos de la lista de no descartados
                            int r = random.Next(0, arrayPlayersIdsNotDiscarded.Count());
                            Player choosenOne = GetPlayerById(arrayPlayersIdsNotDiscarded[r]);
                            playersIdsNotDiscarded.Remove(choosenOne.Id);

                            //Obtenemos la lista de jugadores que han jugado en anteriores rondas contra el elegido
                            List<int> playersWHPATCO = GetPlayersWhoHavePlayedAgainstTheChoosenOne(choosenOne, currentRound);
                            bool anyoneHavePlayed = false;
                            foreach (int ctp in currentTablePlayers.Select(x => x.Id))
                            {
                                if (playersWHPATCO.Contains(ctp))
                                    anyoneHavePlayed = true;
                            }

                            /*Si el elegido ya ha jugado contra alguno de los de la mesa actual
                              o es del mismo equipo que alguno de los de la mesa actual(si el cálculo de equipos está seleccionado),
                              hay que buscar un nuevo candidato para esta mesa*/
                            if (anyoneHavePlayed || (_isTeamsChecked && currentTablePlayers.Select(x => x.Team).Contains(choosenOne.Team)))
                                playerFounded = false;
                            else
                            {/*Si no ha jugado contra ninguno ni son de su mismo equipo, lo añadimos a la mesa
                               y lo quitamos de la lista de jugadores sin usar esta ronda*/

                                playerFounded = true;
                                _tablePlayers.Add(new TablePlayer(currentRound, currentTable, currentTablePlayer,
                                    choosenOne.Id));
                                playersNotUsedThisRound.Remove(choosenOne.Id);
                            }
                        }

                        //Si no se ha encontrado un posible jugador delvolvemos error para volver a empezar todo.
                        if (!playerFounded && playersIdsNotDiscarded.Count == 0)
                            return -1;
                    }
                }
            }
            //Si llegamos aqui es que todo ha ido bien y se ha terminado el cálculo
            return 1;
        }

        private List<int> GetPlayersWhoHavePlayedAgainstTheChoosenOne(Player choosenOne, int currentRound)
        {
            //Obtenemos una lista con las mesas de las anteriores rondas
            List<TablePlayer> anterioresRondas = _tablePlayers.FindAll(x => x.round < currentRound).ToList();

            //Si hay anteriores rondas
            if (anterioresRondas.Count > 0)
            {
                //Obtenemos una lista con los ids de las anteriores rondas
                List<int> roundIdsWhichHavePlayed = anterioresRondas.Select(x => x.round).Distinct().ToList();

                //Obtenemos una lista de las mesas en las que ha jugado el elegido en cada ronda
                List<TablePlayer> tablePlayersWhichHavePlayedChoosenOne = new List<TablePlayer>();
                foreach (int roundPlayed in roundIdsWhichHavePlayed)
                {
                    tablePlayersWhichHavePlayedChoosenOne.AddRange(anterioresRondas.FindAll(
                        x => x.round == roundPlayed && x.playerId == choosenOne.Id).ToList());
                }
                List<TablePlayer> completeTablePlayersWhichHavePlayedAll = new List<TablePlayer>();
                foreach (TablePlayer tp in tablePlayersWhichHavePlayedChoosenOne)
                {
                    completeTablePlayersWhichHavePlayedAll.AddRange(anterioresRondas.FindAll(
                        x => x.round == tp.round && x.table == tp.table).ToList());
                }

                //Obtenemos una lista con los jugadores que ya han jugado contra el elegido en cada mesa donde él jugó
                List<int> rivalsWhoHavePlayedAgainst = new List<int>();
                foreach (TablePlayer tp in completeTablePlayersWhichHavePlayedAll)
                {
                    if (tp.playerId != choosenOne.Id)
                        rivalsWhoHavePlayedAgainst.Add(tp.playerId);
                }
                return rivalsWhoHavePlayedAgainst;
            }
            else
                return new List<int>();
        }

        private Player GetPlayerById(int id)
        {
            foreach (Player p in _players)
            {
                if (p.Id == id)
                {
                    return p;
                }
            }
            return null;
        }

        private void GenerateTablesWithAll(int numRounds)
        {
            int currentRound, currentTable, currentTablePlayer;

            for (currentRound = 1; currentRound <= numRounds; currentRound++)
            {
                for (currentTable = 1; currentTable <= _players.Count / 4; currentTable++)
                {
                    TableWithAll tableWithAll = new TableWithAll();
                    tableWithAll.roundId = currentRound;
                    tableWithAll.tableId = currentTable;
                    for (currentTablePlayer = 1; currentTablePlayer <= 4; currentTablePlayer++)
                    {
                        switch (currentTablePlayer)
                        {
                            case 1:
                                int player1Id = _tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                Player player = _players.Find(x => x.Id == player1Id);
                                tableWithAll.player1Name = player.Name;
                                tableWithAll.player1Team = player.Team;
                                tableWithAll.player1Id = player.Id;
                                break;
                            case 2:
                                int player2Id = _tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                Player player2 = _players.Find(x => x.Id == player2Id);
                                tableWithAll.player2Name = player2.Name;
                                tableWithAll.player2Team = player2.Team;
                                tableWithAll.player2Id = player2.Id;
                                break;
                            case 3:
                                int player3Id = _tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                Player player3 = _players.Find(x => x.Id == player3Id);
                                tableWithAll.player3Name = player3.Name;
                                tableWithAll.player3Team = player3.Team;
                                tableWithAll.player3Id = player3.Id;
                                break;
                            case 4:
                                int player4Id = _tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                Player player4 = _players.Find(x => x.Id == player4Id);
                                tableWithAll.player4Name = player4.Name;
                                tableWithAll.player4Team = player4.Team;
                                tableWithAll.player4Id = player4.Id;
                                break;
                        }
                    }
                    _tablesWithAll.Add(tableWithAll);
                }
            }
        }

        private void SaveTournament()
        {
            _tournament = new VTournament(_data.GetIdForNewTournament(), DateTime.Now, _tournamentName, _players.Count, _numRounds, _isTeamsChecked);
            _data.AddTournament(_tournament);
        }

        private void SavePlayers()
        {
            List<VPlayer> dbPlayers = new List<VPlayer>();
            foreach (Player player in _players)
            {
                dbPlayers.Add(new VPlayer(_tournament.TournamentId, player.Id, string.Format("Player {0}", player.Id), int.Parse(player.Team), "No Country", string.Empty));
            }
            _data.AddPlayers(dbPlayers);
        }

        private void SavePlayersWithFakeData()
        {
            List<VPlayer> dbPlayers = new List<VPlayer>();
            foreach (Player player in _players)
            {
                dbPlayers.Add(new VPlayer(_tournament.TournamentId, player.Id, string.Format("Player {0}", player.Id), int.Parse(player.Team), "Spain", string.Empty));
            }
            _data.AddPlayers(dbPlayers);
        }

        private void SaveTeams()
        {
            List<VTeam> dbTeams = new List<VTeam>();
            for (int i = 1; i <= _players.Count / 4; i++)
            {
                dbTeams.Add(new VTeam(_tournament.TournamentId, i, string.Format("Team {0}", i)));
            }
            _data.AddTeams(dbTeams);
        }

        private void SaveTablesAndHands()
        {
            List<VTable> dbTables = new List<VTable>();
            List<VHand> dbHands = new List<VHand>();
            foreach (TableWithAll table in _tablesWithAll)
            {
                dbTables.Add(new VTable(_tournament.TournamentId, table.roundId, table.tableId,
                    table.player1Id, table.player2Id, table.player3Id, table.player4Id));

                for (int i = 1; i <= NUM_TABLE_HANDS; i++)
                    dbHands.Add(new VHand(_tournament.TournamentId, table.roundId, table.tableId, i));
            }
            _data.AddTables(dbTables, dbHands);
        }

        private void SaveTablesAndHandsWithFakeData()
        {
            List<VTable> dbTables = new List<VTable>();
            List<VHand> dbHands = new List<VHand>();
            foreach (TableWithAll table in _tablesWithAll)
            {
                VTable dbTable = new VTable(_tournament.TournamentId, table.roundId, table.tableId,
                    table.player1Id, table.player2Id, table.player3Id, table.player4Id,
                    table.player1Id.ToString(), table.player2Id.ToString(), table.player3Id.ToString(), table.player4Id.ToString(),
                    "107", "13", "-115", "-5", "4", "2", "0", "1", true, false);

                dbHands.Add(new VHand(_tournament.TournamentId, table.roundId, table.tableId, 1,  table.player1Id.ToString(), table.player4Id.ToString(), "9",  true,   "0", "0", "0", "0"));
                dbHands.Add(new VHand(_tournament.TournamentId, table.roundId, table.tableId, 2,  table.player1Id.ToString(), table.player2Id.ToString(), "25", false,  "0", "0", "0", "0"));
                dbHands.Add(new VHand(_tournament.TournamentId, table.roundId, table.tableId, 3,  table.player4Id.ToString(), table.player1Id.ToString(), "20", false,  "0", "0", "0", "0"));
                dbHands.Add(new VHand(_tournament.TournamentId, table.roundId, table.tableId, 4,  string.Empty,               string.Empty,               "0",  false , "0", "0", "0", "0"));
                dbHands.Add(new VHand(_tournament.TournamentId, table.roundId, table.tableId, 5,  table.player2Id.ToString(), table.player4Id.ToString(), "18", false,  "0", "0", "0", "0"));
                dbHands.Add(new VHand(_tournament.TournamentId, table.roundId, table.tableId, 6,  table.player1Id.ToString(), table.player4Id.ToString(), "12", false,  "0", "0", "0", "0"));
                dbHands.Add(new VHand(_tournament.TournamentId, table.roundId, table.tableId, 7,  table.player2Id.ToString(), string.Empty,               "23", false,  "0", "0", "0", "0"));
                dbHands.Add(new VHand(_tournament.TournamentId, table.roundId, table.tableId, 8,  table.player4Id.ToString(), table.player1Id.ToString(), "13", false,  "0", "0", "0", "0"));
                dbHands.Add(new VHand(_tournament.TournamentId, table.roundId, table.tableId, 9,  table.player1Id.ToString(), table.player2Id.ToString(), "10", true,   "0", "0", "0", "0"));
                dbHands.Add(new VHand(_tournament.TournamentId, table.roundId, table.tableId, 10, table.player1Id.ToString(), table.player2Id.ToString(), "10", true,   "0", "0", "0", "0"));
                dbHands.Add(new VHand(_tournament.TournamentId, table.roundId, table.tableId, 11, table.player3Id.ToString(), table.player2Id.ToString(), "20", false,  "0", "0", "0", "0"));
                dbHands.Add(new VHand(_tournament.TournamentId, table.roundId, table.tableId, 12, table.player2Id.ToString(), table.player3Id.ToString(), "11", false,  "0", "0", "0", "0"));
                dbHands.Add(new VHand(_tournament.TournamentId, table.roundId, table.tableId, 13, table.player4Id.ToString(), string.Empty,               "11", false,  "0", "0", "0", "0"));
                dbHands.Add(new VHand(_tournament.TournamentId, table.roundId, table.tableId, 14, table.player1Id.ToString(), table.player3Id.ToString(), "20", false,  "0", "0", "0", "0"));
                dbHands.Add(new VHand(_tournament.TournamentId, table.roundId, table.tableId, 15));
                dbHands.Add(new VHand(_tournament.TournamentId, table.roundId, table.tableId, 16));
                dbTables.Add(dbTable);
            }
            _data.AddTables(dbTables, dbHands);
        }

        #endregion
    }
}
