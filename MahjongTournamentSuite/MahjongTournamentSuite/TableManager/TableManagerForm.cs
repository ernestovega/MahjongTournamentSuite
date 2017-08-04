using MahjongTournamentSuite.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace MahjongTournamentSuite.TableManager
{
    partial class TableManagerForm : Form, ITableManagerForm
    {
        #region Constants



        #endregion

        #region Fields

        private ITableManagerPresenter _presenter;
        private int _tournamentId, _roundId, _tableId;

        #endregion

        #region Constructor

        public TableManagerForm(int tournamentId, int roundId, int tableId)
        {
            InitializeComponent();
            _tournamentId = tournamentId;
            _roundId = roundId;
            _tableId = tableId;
            _presenter = Injector.provideTableManagerPresenter(this);
            _presenter.LoadTable(_tournamentId, _roundId, _tableId);
        }

        #endregion

        #region Lifecycle

        #endregion

        #region Events

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region ITableManagerForm implementation

        public void SetTournamentName(string tournamentName)
        {
            lblTournamentName.Text = tournamentName;
        }

        public void SetRoundId(int roundId)
        {
            lblTournamentName.Text = roundId.ToString();
        }

        public void SetTableId(int tableId)
        {
            lblTournamentName.Text = tableId.ToString();
        }

        public void FillCombosPlayers(List<ComboItem> comboPlayers)
        {
            comboEastPlayer.DataSource = comboPlayers;
            comboEastPlayer.DisplayMember = "Text";
            comboEastPlayer.ValueMember = "Value";
            comboSouthPlayer.DataSource = comboPlayers;
            comboSouthPlayer.DisplayMember = "Text";
            comboSouthPlayer.ValueMember = "Value";
            comboWestPlayer.DataSource = comboPlayers;
            comboWestPlayer.DisplayMember = "Text";
            comboWestPlayer.ValueMember = "Value";
            comboNorthPlayer.DataSource = comboPlayers;
            comboNorthPlayer.DisplayMember = "Text";
            comboNorthPlayer.ValueMember = "Value";
        }

        public void FillDataGridHands(List<DBHand> hands)
        {
            dataGridHands.DataSource = hands;
            dataGridHands.AllowUserToDeleteRows = false;
            dataGridHands.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridHands.Columns["TournamentId"].ReadOnly = true;
        }

        #endregion

        #region Private

        #endregion

    }
}
