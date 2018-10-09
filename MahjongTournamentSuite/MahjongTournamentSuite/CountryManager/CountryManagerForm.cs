using System.Windows.Forms;
using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;
using MahjongTournamentSuite.ViewModel;
using System.Drawing;
using MahjongTournamentSuite.Resources;

namespace MahjongTournamentSuite.CountryManager
{
    public partial class CountryManagerForm : Form, ICountryManagerForm
    {
        #region Fields

        private ICountryManagerController _controller;

        #endregion

        #region Constructor

        public CountryManagerForm()
        {
            InitializeComponent();
            _controller = Injector.provideCountryManagerController(this);
        }

        #endregion

        #region Events

        private void CountryManagerForm_Load(object sender, System.EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            _controller.LoadForm();
            Cursor = Cursors.Default;
        }

        private void CountryManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Para guardar los cambios que nos e hayan guardado.
            lblStub.Focus();
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

        private void dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1 && dgv.Columns[e.ColumnIndex].Name.Equals(VCountry.COLUMN_COUNTRY_IMAGE_URL))
                dgv.BeginEdit(true);
        }

        private void dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex > -1 && dgv.Columns[e.ColumnIndex].Name.Equals(VCountry.COLUMN_COUNTRY_IMAGE_URL))
            {
                Cursor = Cursors.WaitCursor;
                string previousValue = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                string newValue = ((string)e.FormattedValue).Trim();
                if (!newValue.Equals(previousValue))
                {
                    string countryName = (string)dgv.Rows[e.RowIndex].Cells[VCountry.COLUMN_COUNTRY_NAME].Value;
                    _controller.CountryImageURLChanged(countryName, newValue);
                }
                else
                    DGVCancelEdit();
                Cursor = Cursors.Default;
            }
        }

        public void DGVCancelEdit()
        {
            dgv.CancelEdit();
        }

        #endregion

        #region ICountryManagerForm implementation

        public void FillDGV(List<DGVCountry> dgvCountries)
        {
            SortableBindingList<DGVCountry> sortableDgvCountries = new SortableBindingList<DGVCountry>(dgvCountries);
            if (dgv.DataSource != null)
                dgv.DataSource = null;
            dgv.DataSource = sortableDgvCountries;
            
            //ReadOnly
            dgv.Columns[VCountry.COLUMN_COUNTRY_NAME].ReadOnly = true;
            dgv.Columns[DGVCountry.COLUMN_COUNTRY_COUNTRY_FLAG].ReadOnly = true;
            //Readonly columns BackColor
            dgv.Columns[VCountry.COLUMN_COUNTRY_NAME].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgv.Columns[DGVCountry.COLUMN_COUNTRY_COUNTRY_FLAG].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            //Readonly columns ForeColor
            dgv.Columns[VCountry.COLUMN_COUNTRY_NAME].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            dgv.Columns[DGVCountry.COLUMN_COUNTRY_COUNTRY_FLAG].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            //HeaderText
            dgv.Columns[VCountry.COLUMN_COUNTRY_NAME].HeaderText = "Country Name";
            dgv.Columns[VCountry.COLUMN_COUNTRY_IMAGE_URL].HeaderText = "Image URL for HTML Exporting";
            dgv.Columns[DGVCountry.COLUMN_COUNTRY_COUNTRY_FLAG].HeaderText = "Flag";
            //AutoSizeMode
            dgv.Columns[VCountry.COLUMN_COUNTRY_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[DGVCountry.COLUMN_COUNTRY_COUNTRY_FLAG].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //Column Flags Image Layout
            ((DataGridViewImageColumn)dgv.Columns[DGVCountry.COLUMN_COUNTRY_COUNTRY_FLAG]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            //DisplayIndex
            dgv.Columns[VCountry.COLUMN_COUNTRY_NAME].DisplayIndex = 0;
            dgv.Columns[DGVCountry.COLUMN_COUNTRY_COUNTRY_FLAG].DisplayIndex = 1;
            dgv.Columns[VCountry.COLUMN_COUNTRY_IMAGE_URL].DisplayIndex = 2;
        }

        #endregion 

        #region Private

        #endregion
    }
}
