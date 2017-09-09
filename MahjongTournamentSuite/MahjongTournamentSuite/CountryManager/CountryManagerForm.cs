using System.Windows.Forms;
using MahjongTournamentSuiteDataLayer.Model;
using System.Collections.Generic;
using MahjongTournamentSuite.Model;
using System.Drawing;
using MahjongTournamentSuite.Resources;

namespace MahjongTournamentSuite.CountryManager
{
    public partial class CountryManagerForm : Form, ICountryManagerForm
    {
        #region Fields

        private ICountryManagerPresenter _presenter;

        #endregion

        #region Constructor

        public CountryManagerForm()
        {
            InitializeComponent();
            _presenter = Injector.provideCountryManagerPresenter(this);
        }

        #endregion

        #region Events

        private void CountryManagerForm_Load(object sender, System.EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            _presenter.LoadForm();
            Cursor = Cursors.Default;
        }

        #endregion 

        #region ICountryManagerForm implementation

        public void FillDGV(List<DBCountry> countries)
        {
            SortableBindingList<DBCountry> sortableCountries = new SortableBindingList<DBCountry>(countries);
            if (dgv.DataSource != null)
                dgv.DataSource = null;
            dgv.DataSource = sortableCountries;
            
            //ReadOnly
            dgv.Columns[DBCountry.COLUMN_COUNTRY_NAME].ReadOnly = true;
            //Readonly columns BackColor
            dgv.Columns[DBCountry.COLUMN_COUNTRY_NAME].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            //Readonly columns ForeColor
            dgv.Columns[DBCountry.COLUMN_COUNTRY_NAME].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            //HeaderText
            dgv.Columns[DBCountry.COLUMN_COUNTRY_NAME].HeaderText = "Country Name";
            dgv.Columns[DBCountry.COLUMN_COUNTRY_IMAGE_URL].HeaderText = "Country Image URL for HTML Exporting";
            //AutoSizeMode
            dgv.Columns[DBCountry.COLUMN_COUNTRY_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        #endregion 

        #region Events

        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgv.CurrentCell != null &&
                dgv.CurrentCell.RowIndex == e.RowIndex &&
                dgv.CurrentCell.ColumnIndex == e.ColumnIndex)
            {
                e.CellStyle.SelectionBackColor =
                    dgv.CurrentCell.ReadOnly ?
                    MyConstants.GREEN_MM_DARKEST :
                    MyConstants.GREEN_MM_DARKER;
            }
        }

        private void dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1 && dgv.Columns[e.ColumnIndex].Name.Equals(DBCountry.COLUMN_COUNTRY_IMAGE_URL))
                dgv.BeginEdit(true);
        }

        private void dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex > -1 && dgv.Columns[e.ColumnIndex].Name.Equals(DBCountry.COLUMN_COUNTRY_IMAGE_URL))
            {
                Cursor = Cursors.WaitCursor;
                string previousValue = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                string newValue = ((string)e.FormattedValue).Trim();
                if (newValue.Length > 0 && !newValue.Equals(previousValue))
                {
                    string countryName = (string)dgv.Rows[e.RowIndex].Cells[DBCountry.COLUMN_COUNTRY_NAME].Value;
                    _presenter.CountryImageURLChanged(countryName, newValue);
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
        
        #region Private
        
        #endregion
    }
}
