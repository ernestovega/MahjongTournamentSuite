using System.Windows.Forms;
using System.Collections.Generic;
using MahjongTournamentSuite.Resources;
using MahjongTournamentSuite._Data.DataModel;
using MahjongTournamentSuite.CountrySelector;
using MahjongTournamentSuite.ViewModel;
using MahjongTournamentSuite.Resources.flags;
using System.Media;
using MahjongTournamentSuite.EmaReport;

namespace MahjongTournamentSuite.EmaPlayersManager
{
    public partial class EmaPlayersManagerForm : Form, IEmaPlayersManagerForm
    {
        #region Fields

        private IEmaPlayersManagerController _controller;

        #endregion

        #region Constructor

        public EmaPlayersManagerForm()
        {
            InitializeComponent();
            _controller = Injector.provideEmaPlayersManagerController(this);
        }

        #endregion

        #region Events

        private void PlayersManagerForm_Load(object sender, System.EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            _controller.LoadForm();
            Cursor = Cursors.Default;
        }
        
        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgv.CurrentCell != null &&
                dgv.CurrentCell.RowIndex == e.RowIndex &&
                dgv.CurrentCell.ColumnIndex == e.ColumnIndex)
            {
                e.CellStyle.SelectionBackColor =
                    dgv.CurrentCell.ReadOnly ?
                    ColorConstants.GREEN_MM_DARKEST :
                    ColorConstants.GREEN_MM_DARKER;
            }
        }

        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgv.CurrentCell != null && dgv.CurrentRow.Index >= 0)
            {
                if (dgv.CurrentCell.OwningColumn.Name.Equals(DGVEmaPlayer.COLUMN_EMA_PLAYER_COUNTRY_FLAG))
                {
                    ShowCountriesSelector(dgv.CurrentRow.Index);
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgv.Columns[e.ColumnIndex].Name.Equals(VEmaPlayer.COLUMN_EMA_PLAYER_EMA_NUMBER))
                    dgv.BeginEdit(true);
                if (dgv.Columns[e.ColumnIndex].Name.Equals(VEmaPlayer.COLUMN_EMA_PLAYER_LAST_NAME))
                    dgv.BeginEdit(true);
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(VEmaPlayer.COLUMN_EMA_PLAYER_NAME))
                    dgv.BeginEdit(true);
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(DGVEmaPlayer.COLUMN_EMA_PLAYER_COUNTRY_FLAG))
                    ShowCountriesSelector(e.RowIndex);
            }
        }

        private void dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgv.Columns[e.ColumnIndex].Name.Equals(VEmaPlayer.COLUMN_EMA_PLAYER_EMA_NUMBER))
                {
                    Cursor = Cursors.WaitCursor;
                    string previousValue = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    string newValue = ((string)e.FormattedValue).Trim();
                    if (newValue.Length > 0 && !newValue.Equals(previousValue))
                    {
                        string emaPlayerEmaNumber = (string)dgv.Rows[e.RowIndex].Cells[VEmaPlayer.COLUMN_EMA_PLAYER_EMA_NUMBER].Value;
                        _controller.EmaPlayerEmaNumberChanged(emaPlayerEmaNumber, newValue);
                    }
                    else
                        DGVCancelEdit();
                    Cursor = Cursors.Default;
                }
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(VEmaPlayer.COLUMN_EMA_PLAYER_LAST_NAME))
                {
                    Cursor = Cursors.WaitCursor;
                    string previousValue = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    string newValue = ((string)e.FormattedValue).Trim();
                    if (newValue.Length > 0 && !newValue.Equals(previousValue))
                    {
                        string emaPlayerEmaNumber = (string)dgv.Rows[e.RowIndex].Cells[VEmaPlayer.COLUMN_EMA_PLAYER_EMA_NUMBER].Value;
                        _controller.EmaPlayerLastNameChanged(emaPlayerEmaNumber, newValue);
                    }
                    else
                        DGVCancelEdit();
                    Cursor = Cursors.Default;
                }
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(VEmaPlayer.COLUMN_EMA_PLAYER_NAME))
                {
                    Cursor = Cursors.WaitCursor;
                    string previousValue = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    string newValue = ((string)e.FormattedValue).Trim();
                    if (newValue.Length > 0 && !newValue.Equals(previousValue))
                    {
                        string emaPlayerEmaNumber = (string)dgv.Rows[e.RowIndex].Cells[VEmaPlayer.COLUMN_EMA_PLAYER_EMA_NUMBER].Value;
                        _controller.EmaPlayerNameChanged(emaPlayerEmaNumber, newValue);
                    }
                    else
                        DGVCancelEdit();
                    Cursor = Cursors.Default;
                }
            }
        }

        #endregion

        #region IPlayersManagerForm implementation

        public void FillDGV(List<DGVEmaPlayer> players)
        {
            SortableBindingList<DGVEmaPlayer> sortablePlayers = new SortableBindingList<DGVEmaPlayer>(players);
            if (dgv.DataSource != null)
                dgv.DataSource = null;
            dgv.DataSource = sortablePlayers;

            //Visible
            dgv.Columns[VEmaPlayer.COLUMN_EMA_PLAYER_COUNTRY_NAME].Visible = false;
            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_TABLE_POINTS].Visible = false;
            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_SCORE].Visible = false;
            //HeaderText
            dgv.Columns[VEmaPlayer.COLUMN_EMA_PLAYER_EMA_NUMBER].HeaderText = "EMA Number";
            dgv.Columns[VEmaPlayer.COLUMN_EMA_PLAYER_LAST_NAME].HeaderText = "Last Name";
            dgv.Columns[VEmaPlayer.COLUMN_EMA_PLAYER_NAME].HeaderText = "Name";
            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_COUNTRY_FLAG].HeaderText = "Country";
            //AutoSizeMode
            //dgv.Columns[VEmaPlayer.COLUMN_EMA_PLAYER_EMA_NUMBER].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgv.Columns[VEmaPlayer.COLUMN_EMA_PLAYER_LAST_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgv.Columns[VEmaPlayer.COLUMN_EMA_PLAYER_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_COUNTRY_FLAG].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //Column Flags Image Layout
            ((DataGridViewImageColumn)dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_COUNTRY_FLAG]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            //DisplayIndex
            dgv.Columns[VEmaPlayer.COLUMN_EMA_PLAYER_EMA_NUMBER].DisplayIndex = 0;
            dgv.Columns[VEmaPlayer.COLUMN_EMA_PLAYER_LAST_NAME].DisplayIndex = 1;
            dgv.Columns[VEmaPlayer.COLUMN_EMA_PLAYER_NAME].DisplayIndex = 2;
            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_COUNTRY_FLAG].DisplayIndex = 3;
        }

        public void DGVCancelEdit()
        {
            dgv.CancelEdit();
        }

        public void ShowMessagePlayerEmaNumberInUse(string usedEmaNumber)
        {
            MessageBox.Show(string.Format("Ema Number: {0} is in use by other player", usedEmaNumber), "Ema Number in use");
        }

        public void PlayKoSound()
        {
            SystemSounds.Exclamation.Play();
        }

        #endregion

        #region Private

        private void ShowCountriesSelector(int rowIndex)
        {
            using (var countrySelectorForm = new CountrySelectorForm(false))
            {
                if (countrySelectorForm.ShowDialog() == DialogResult.OK)
                {
                    string emaPlayerEmaNumber = (string)dgv.Rows[rowIndex].Cells[VEmaPlayer.COLUMN_EMA_PLAYER_EMA_NUMBER].Value;
                    if (countrySelectorForm.ReturnValue != null && !countrySelectorForm.ReturnValue.Equals(string.Empty))
                    {
                        _controller.EmaPlayerCountryChanged(emaPlayerEmaNumber, countrySelectorForm.ReturnValue);
                    }
                }
            }
        }

        #endregion
    }
}
