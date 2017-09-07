using System.Windows.Forms;

namespace MahjongTournamentSuite.TemplateTableManager
{
    public partial class TemplateTableManagerForm : Form, ITemplateTableManagerForm
    {
        #region Fields

        private ITemplateTableManagerPresenter _presenter;

        #endregion

        #region Constructor

        public TemplateTableManagerForm()
        {
            InitializeComponent();
            _presenter = Injector.provideTemplateTableManagerPresenter(this);
        }

        #endregion 

        #region Events

        private void TemplateTableManagerForm_Load(object sender, System.EventArgs e)
        {

        }

        #endregion 

        #region ITemplateTableManagerForm implementation

        #endregion 

        #region Private

        #endregion 
    }
}
