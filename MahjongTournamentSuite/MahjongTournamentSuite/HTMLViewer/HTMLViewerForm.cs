using MahjongTournamentSuite.Model;
using System.Windows.Forms;
using System;

namespace MahjongTournamentSuite.HTMLViewer
{
    public partial class HTMLViewerForm : Form, IHTMLViewerForm
    {
        #region FIELDS

        private IHTMLViewerPresenter _presenter;
        private HTMLRankings _htmlRankings;

        #endregion

        #region Constructor

        public HTMLViewerForm(HTMLRankings htmlRankings)
        {
            InitializeComponent();
            _presenter = Injector.provideHTMLViewerPresenter(this);
            _htmlRankings = htmlRankings;
        }

        #endregion

        #region Events

        private void HTMLViewerForm_Load(object sender, System.EventArgs e)
        {
            _presenter.LoadForm(_htmlRankings);
        }

        private void btnCopyHtml_Click(object sender, EventArgs e)
        {
            _presenter.CopyHtmlClicked();
        }

        #endregion

        #region IHTMLViewerForm implementation

        public void SetRankingHTMLText(string htmlRanking)
        {
            tbHtml.Text = htmlRanking;
        }

        public void SelectHTMLText()
        {
            tbHtml.SelectAll();
            tbHtml.Focus();
        }

        #endregion
    }
}
