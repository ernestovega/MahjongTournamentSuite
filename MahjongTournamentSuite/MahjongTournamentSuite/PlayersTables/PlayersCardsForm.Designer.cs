namespace MahjongTournamentSuite.PlayersTables
{
    partial class PlayersCardsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvPlayersCards = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayersCards)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPlayersCards
            // 
            this.dgvPlayersCards.AllowUserToAddRows = false;
            this.dgvPlayersCards.AllowUserToDeleteRows = false;
            this.dgvPlayersCards.AllowUserToResizeColumns = false;
            this.dgvPlayersCards.AllowUserToResizeRows = false;
            this.dgvPlayersCards.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPlayersCards.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPlayersCards.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvPlayersCards.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPlayersCards.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPlayersCards.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPlayersCards.ColumnHeadersHeight = 40;
            this.dgvPlayersCards.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPlayersCards.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPlayersCards.GridColor = System.Drawing.SystemColors.Control;
            this.dgvPlayersCards.Location = new System.Drawing.Point(14, 14);
            this.dgvPlayersCards.Margin = new System.Windows.Forms.Padding(5);
            this.dgvPlayersCards.Name = "dgvPlayersCards";
            this.dgvPlayersCards.ReadOnly = true;
            this.dgvPlayersCards.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvPlayersCards.RowHeadersVisible = false;
            this.dgvPlayersCards.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvPlayersCards.RowTemplate.Height = 32;
            this.dgvPlayersCards.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvPlayersCards.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvPlayersCards.Size = new System.Drawing.Size(1193, 817);
            // 
            // PlayersCardsForm
            // 
            this.ClientSize = new System.Drawing.Size(1221, 845);
            this.Controls.Add(this.dgvPlayersCards);
            this.Name = "PlayersCardsForm";
            this.Load += new System.EventHandler(this.PlayersCardsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayersCards)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPlayersCards;
    }
}
