using MahjongTournamentSuite.ViewModel;
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

        void FillDGV(List<DGVHand> dataGridHands);

        void FillHandPlayersScoresCells(DGVHand dgvHand);

        void FillAllTotalScoreTextBoxes(string playerEastTotalScore, string playerSouthTotalScore, string playerWestTotalScore, string playerNorthTotalScore);

        void FillAllTotalPointsTextBoxes(string playerEastTotalPoints, string playerSouthTotalPoints, string playerWestTotalPoints, string playerNorthTotalPoints);

        void SetEastPlayerHeader(string selectedValue);

        void SetSouthPlayerHeader(string selectedValue);

        void SetWestPlayerHeader(string selectedValue);

        void SetNorthPlayerHeader(string selectedValue);

        void DGVCancelEdit();

        void UncheckChickenHand(int handId);

        void EnableTotalScoresTextBoxes();

        void DisableTotalScoresTextBoxes();

        void ShowPanelTotals();

        void HidePanelTotals();

        void ShowWaitCursor();

        void ShowDefaultCursor();

        void ShowDataGridHands();

        void HideDataGridHands();
        
        void PlayKoSound();

        void ShowMessageChickenHandNeedWinnerLooserAndScore();
        void SetIsCompleted(bool isCompleted);
    }
}
