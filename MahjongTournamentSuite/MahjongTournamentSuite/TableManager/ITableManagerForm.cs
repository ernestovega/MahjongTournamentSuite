using MahjongTournamentSuite.Model;
using System.Collections.Generic;

namespace MahjongTournamentSuite.TableManager
{
    interface ITableManagerForm
    {
        void SetTournamentName(string tournamentName);

        void SetRoundId(int roundId);

        void SetTableId(int tableId);

        void FillCombosPlayers(List<ComboItem> comboPlayers);

        void SelectPlayersInCombos(int playerEastId, int playerSouthId, int playerWestId, int playerNorthId);

        void FillDataGridHands(List<DGVHand> dataGridHands);

        void FillPlayersHandScores(int id, int eastPlayerScore, 
            int southPlayerScore, int westPlayerScore, int northPlayerScore);

        void SetEastPlayerHeader(string selectedValue);

        void SetSouthPlayerHeader(string selectedValue);

        void SetWestPlayerHeader(string selectedValue);

        void SetNorthPlayerHeader(string selectedValue);

        void OpenEastComboBox();

        void OpenSouthComboBox();

        void OpenWestComboBox();

        void OpenNorthComboBox();

        void ShowWaitCursor();

        void ShowDefaultCursor();

        void ShowDataGridHands();

        void HideDataGridHands();
    }
}
