using System.Windows.Forms;

namespace MahjongTournamentRanking.Main
{
    public partial class MainForm : Form, IMainForm
    {
        private IMainPresenter _presenter;

        public MainForm()
        {
            InitializeComponent();
            _presenter = new MainPresenter();
        }
    }
}
