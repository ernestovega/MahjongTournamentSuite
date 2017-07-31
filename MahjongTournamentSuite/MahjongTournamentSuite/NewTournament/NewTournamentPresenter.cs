﻿using MahjongTournamentSuite.Data;
using MahjongTournamentSuite.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MahjongTournamentSuite.NewTournament
{
    class NewTournamentPresenter : INewTournamentPresenter
    {
        #region Fields

        private INewTournamentForm _form;
        private IDBManager _db;
        private DBTournament dbTournament;
        private List<Player> players = new List<Player>();
        private List<TablePlayer> tablePlayers = new List<TablePlayer>();
        private List<TableWithAll> tablesWithAll = new List<TableWithAll>();
        private List<string[]> sTablesNames = new List<string[]>();
        private List<string[]> sTablesTeams = new List<string[]>();
        private List<string[]> sTablesCountries = new List<string[]>();
        private List<string[]> sTablesIds = new List<string[]>();
        private List<TableWithAll> tablesByPlayer = new List<TableWithAll>();
        private List<Rivals> rivalsByPlayer = new List<Rivals>();
        private int currentRound, currentTable, currentTablePlayer;
        private int numTriesMax, result, numPlayers, numRounds, countTries;
        private Random random = new Random();
        private string tournamentName = string.Empty;
        private Boolean isTeamsChecked;

        #endregion

        #region Constructor

        public NewTournamentPresenter(INewTournamentForm newTournamentForm)
        {
            _form = newTournamentForm;
            _db = Injector.provideDBManager();
        }

        #endregion

        #region INewTournamentPresenter implementation

        public void StartClicked()
        {
            if (_form.isBackgroundWorkerBusy())
            {
                _form.CancelBackgroundWorker();
                _form.EnableViews();
            }
            else
            {
                InitializeCalculation();
                _form.RunBackgroundWorker();
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
            result = -1;
            countTries = 0;
            while (result < 0 && countTries < numTriesMax)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                countTries++;
                worker.ReportProgress(countTries, null);
                result = GenerateTournament(numRounds);
                _form.ApplicationDoEvents();
            }

            if (countTries >= numTriesMax)
            {
                return;
            }

            worker.ReportProgress(countTries, null);

            //Generamos todas las vistas y mostramos las mesas
            GenerateTablesWhitAll(numRounds);
            //GenerateSTablesWithNames();
            //GenerateSTablesWithIds();
            //GenerateTablesByPlayer();
            //GenerateRivalsByPlayer();

            SaveTournament();
            SavePlayers();
            SaveTables();
        }

        public void RunWorkerCompleted()
        {
            /*Si no se ha podido calcular en los intentos indicados, se notifica,
              se muestra la lista de jugadores y se termina*/
            if (countTries == numTriesMax)
            {
                _form.ShowReachedTriesMessage(numTriesMax);
            }
            _form.EnableViews();
        }

        #endregion

        #region Private

        public void InitializeCalculation()
        {
            numPlayers = _form.getNumPlayers();
            numRounds = _form.getNumRounds();
            isTeamsChecked = _form.IsTeamsChecked();
            tournamentName = _form.getTournamentName();
            numTriesMax = _form.getNumTries();
            _form.ResetProgressBar(numTriesMax);
            _form.DisableViews();
        }
        
        private void GeneratePlayers()
        {
            players.Clear();
            for (int i = 1; i <= numPlayers / 4; i++)//Recorremos cada equipo
            {
                int teamId = (4 * i) / 4;//Generamos el id del equipo
                for (int j = 1; j <= 4; j++)//Recorremos cada jugador del equipo
                {
                    //Generamos el id del jugador
                    int playerId = (4 * i) - (4 - j);
                    //Creamos el jugador
                    Player player = new Player(playerId.ToString(), playerId.ToString(), teamId.ToString());
                    //Añadimos el jugador
                    players.Add(player);
                }
            }
        }

        private int GenerateTournament(int numRounds)
        {
            //Limpiamos las tablas
            tablePlayers.Clear();
            tablesWithAll.Clear();
            sTablesNames.Clear();
            sTablesTeams.Clear();
            sTablesCountries.Clear();
            sTablesIds.Clear();
            tablesByPlayer.Clear();
            rivalsByPlayer.Clear();

            for (currentRound = 1; currentRound <= numRounds; currentRound++)
            {//Iteramos por rondas

                //Copiamos la lista de jugadores para ir borrando los que vayamos usando cada ronda
                List<int> playersNotUsedThisRound = players.Select(x => x.Clone()).ToList().Select(x => x.Id).ToList();

                for (currentTable = 1; currentTable <= players.Count / 4; currentTable++)
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
                            List<TablePlayer> currentTableTablePlayers = tablePlayers.FindAll
                                (x => x.round == currentRound && x.table == currentTable).ToList();
                            List<Player> currentTablePlayers = new List<Player>();
                            foreach (TablePlayer tp in currentTableTablePlayers)
                                currentTablePlayers.Add(GetPlayerById(tp.playerId));

                            //Elegimos un jugador al azar y lo quitamos de la lista de no descartados
                            int r = random.Next(0, arrayPlayersIdsNotDiscarded.Count());
                            Player choosenOne = GetPlayerById(arrayPlayersIdsNotDiscarded[r]);
                            playersIdsNotDiscarded.Remove(choosenOne.Id);

                            //Obtenemos la lista de jugadores que han jugado en anteriores rondas contra el elegido
                            List<int> playersWHPATCO = GetPlayersWhoHavePlayedAgainstTheChoosenOne(choosenOne);
                            bool anyoneHavePlayed = false;
                            foreach (int ctp in currentTablePlayers.Select(x => x.Id))
                            {
                                if (playersWHPATCO.Contains(ctp))
                                    anyoneHavePlayed = true;
                            }

                            /*Si el elegido ya ha jugado contra alguno de los de la mesa actual
                              o es del mismo equipo que alguno de los de la mesa actual(si el cálculo de equipos está seleccionado),
                              hay que buscar un nuevo candidato para esta mesa*/
                            if (anyoneHavePlayed || (isTeamsChecked && currentTablePlayers.Select(x => x.Team).Contains(choosenOne.Team)))
                                playerFounded = false;
                            else
                            {/*Si no ha jugado contra ninguno ni son de su mismo equipo, lo añadimos a la mesa
                               y lo quitamos de la lista de jugadores sin usar esta ronda*/

                                playerFounded = true;
                                tablePlayers.Add(new TablePlayer(currentRound, currentTable, currentTablePlayer,
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

        private List<int> GetPlayersWhoHavePlayedAgainstTheChoosenOne(Player choosenOne)
        {
            //Obtenemos una lista con las mesas de las anteriores rondas
            List<TablePlayer> anterioresRondas = tablePlayers.FindAll(x => x.round < currentRound).ToList();

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
            foreach (Player p in players)
            {
                if (p.Id == id)
                {
                    return p;
                }
            }
            return null;
        }

        private void GenerateTablesWhitAll(int numRounds)
        {
            for (currentRound = 1; currentRound <= numRounds; currentRound++)
            {
                for (currentTable = 1; currentTable <= players.Count / 4; currentTable++)
                {
                    TableWithAll tableWithAll = new TableWithAll();
                    tableWithAll.roundId = currentRound;
                    tableWithAll.tableId = currentTable;
                    for (currentTablePlayer = 1; currentTablePlayer <= 4; currentTablePlayer++)
                    {
                        switch (currentTablePlayer)
                        {
                            case 1:
                                int player1Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                Player player = players.Find(x => x.Id == player1Id);
                                tableWithAll.player1Name = player.Name;
                                tableWithAll.player1Team = player.Team;
                                tableWithAll.player1Id = player.Id;
                                break;
                            case 2:
                                int player2Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                Player player2 = players.Find(x => x.Id == player2Id);
                                tableWithAll.player2Name = player2.Name;
                                tableWithAll.player2Team = player2.Team;
                                tableWithAll.player2Id = player2.Id;
                                break;
                            case 3:
                                int player3Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                Player player3 = players.Find(x => x.Id == player3Id);
                                tableWithAll.player3Name = player3.Name;
                                tableWithAll.player3Team = player3.Team;
                                tableWithAll.player3Id = player3.Id;
                                break;
                            case 4:
                                int player4Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                Player player4 = players.Find(x => x.Id == player4Id);
                                tableWithAll.player4Name = player4.Name;
                                tableWithAll.player4Team = player4.Team;
                                tableWithAll.player4Id = player4.Id;
                                break;
                        }
                    }
                    tablesWithAll.Add(tableWithAll);
                }
            }
        }

        private void GenerateTablesByPlayer()
        {
            foreach (Player p in players)
            {
                tablesByPlayer.AddRange(
                    tablesWithAll.FindAll(x =>
                        x.player1Name.Equals(p.Name) ||
                        x.player2Name.Equals(p.Name) ||
                        x.player3Name.Equals(p.Name) ||
                        x.player4Name.Equals(p.Name)));
            }
        }

        private void GenerateRivalsByPlayer()
        {
            foreach (Player p in players)
            {
                List<TableWithAll> thisPlayerTables = tablesWithAll.FindAll(x =>
                        x.player1Name.Equals(p.Name) ||
                        x.player2Name.Equals(p.Name) ||
                        x.player3Name.Equals(p.Name) ||
                        x.player4Name.Equals(p.Name));
                List<string> thisPlayerRivals = new List<string>();
                foreach (TableWithAll twa in thisPlayerTables)
                {
                    if (!twa.player1Name.Equals(p.Name))
                        thisPlayerRivals.Add(twa.player1Name);
                    if (!twa.player2Name.Equals(p.Name))
                        thisPlayerRivals.Add(twa.player2Name);
                    if (!twa.player3Name.Equals(p.Name))
                        thisPlayerRivals.Add(twa.player3Name);
                    if (!twa.player4Name.Equals(p.Name))
                        thisPlayerRivals.Add(twa.player4Name);
                }
                rivalsByPlayer.Add(new Rivals(p.Name, thisPlayerRivals.ToArray()));
            }
        }

        private void GenerateSTablesWithNames()
        {
            foreach (TableWithAll t in tablesWithAll)
            {
                sTablesNames.Add(new string[] {
                    t.roundId.ToString(),
                    t.tableId.ToString(),
                    t.player1Name.ToString(),
                    t.player2Name.ToString(),
                    t.player3Name.ToString(),
                    t.player4Name.ToString(), });
            }
        }

        private void GenerateSTablesWithIds()
        {
            foreach (TableWithAll t in tablesWithAll)
            {
                sTablesIds.Add(new string[] {
                    t.roundId.ToString(),
                    t.tableId.ToString(),
                    t.player1Id.ToString(),
                    t.player2Id.ToString(),
                    t.player3Id.ToString(),
                    t.player4Id.ToString(), });
            }
        }

        private void SaveTournament()
        {
            int id = _db.GetExistingMaxTournamentId() + 1;
            dbTournament = new DBTournament(id, players.Count, numRounds, tournamentName, DateTime.Now);
            _db.AddTournament(dbTournament);
        }

        private void SavePlayers()
        {
            List<DBPlayer> dbPlayers = new List<DBPlayer>();
            foreach (Player player in players)
            {
                dbPlayers.Add(new DBPlayer(player.Id, dbTournament.Id, player.Name, player.Team, ""));
            }
            _db.AddPlayers(dbPlayers);
        }

        private void SaveTables()
        {
            List<DBTable> dbTables = new List<DBTable>();
            foreach (TableWithAll table in tablesWithAll)
            {
                dbTables.Add(new DBTable(table.tableId, dbTournament.Id, table.roundId,
                    table.player1Id, table.player2Id, table.player3Id, table.player4Id));
            }
            _db.AddTables(dbTables);
        }

        #endregion
    }
}
