using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MahjongTournamentSuite.OldTournaments
{
    public partial class OldTournamentsForm : Form, IOldTournamentsForm
    {
        #region Fields

        private IOldTournamentsPresenter _presenter;

        #endregion

        #region Constructor

        public OldTournamentsForm()
        {
            InitializeComponent();
            _presenter = new OldTournamentsPresenter(this);
        }

        #endregion

        #region Events

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void imgLogoMM_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://www.mahjongmadrid.com/");
            Process.Start(sInfo);
        }

        private void imgLogoEMA_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://mahjong-europe.org/portal/");
            Process.Start(sInfo);
        }

        private void OldTournamentsForm_Load(object sender, EventArgs e)
        {
            tournamentsTableAdapter.Fill(this._MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet.Tournaments);
        }

        private void dataGridTournaments_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == dataGridTournaments.Columns["ButtonDelete"].Index)
            {
                //TODO: icono de borrar
                var image = Properties.Resources.icon_exit; //An image
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var x = e.CellBounds.Left + (e.CellBounds.Width - image.Width) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - image.Height) / 2;
                e.Graphics.DrawImage(image, new Point(x, y));
                e.Handled = true;
            }
        }

        private void dataGridTournaments_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Sólo es editable al columna de nombre
            DataGridViewRow row = dataGridTournaments.Rows[e.RowIndex];
            int tournamentId = (int)row.Cells["Id"].Value;
            string tournamentName = (string)row.Cells["nameDataGridViewTextBoxColumn"].Value;
            _presenter.UpdateName(tournamentId, tournamentName);
        }

        private void dataGridTournaments_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            DeleteTournament(e.RowIndex);
        }

        private void dataGridTournaments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridTournaments.ColumnCount - 1)
            {
                DeleteTournament(e.RowIndex);
            }
        }

        #endregion

        #region IOldTournamentsForm implementation



        #endregion

        #region Private

        private void DeleteTournament(int rowIndex)
        {
            //TODO: pedir confirmación
            DataGridViewRow row = dataGridTournaments.Rows[rowIndex];
            int tournamentId = (int)row.Cells[0].Value;
            //dataGridTournaments.Rows.Remove(row);
            _presenter.DeleteTournament(tournamentId);
        }

        #endregion
    }
}
