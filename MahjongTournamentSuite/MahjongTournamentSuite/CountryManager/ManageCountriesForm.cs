using MahjongTournamentSuite.CountryManager;
using System.Windows.Forms;

namespace MahjongTournamentSuite.ManageCountries
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

        }

        #endregion 

        #region ICountryManagerForm implementation

        #endregion 

        #region Private

        #endregion 
    }
}
