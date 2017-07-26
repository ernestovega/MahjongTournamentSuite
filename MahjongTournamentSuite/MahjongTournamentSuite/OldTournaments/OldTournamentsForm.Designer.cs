namespace MahjongTournamentSuite.OldTournaments
{
    partial class OldTournamentsForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OldTournamentsForm));
            this.btnReturn = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dataGridTournaments = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.creationDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumPlayers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumRounds = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ButtonDelete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tournamentsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet = new MahjongTournamentSuite._MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet();
            this.imgLogoEMA = new System.Windows.Forms.PictureBox();
            this.imgLogoMM = new System.Windows.Forms.PictureBox();
            this.tournamentsTableAdapter = new MahjongTournamentSuite._MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSetTableAdapters.TournamentsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTournaments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tournamentsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoEMA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoMM)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReturn
            // 
            this.btnReturn.BackgroundImage = global::MahjongTournamentSuite.Properties.Resources.icon_return;
            this.btnReturn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReturn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnReturn.FlatAppearance.BorderSize = 0;
            this.btnReturn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnReturn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Location = new System.Drawing.Point(9, 10);
            this.btnReturn.Margin = new System.Windows.Forms.Padding(0);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(50, 54);
            this.btnReturn.TabIndex = 21;
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTitle.Font = new System.Drawing.Font("Gang of Three", 32F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTitle.Location = new System.Drawing.Point(320, 10);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(50, 54, 50, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(392, 48);
            this.lblTitle.TabIndex = 75;
            this.lblTitle.Text = "OLD TOURNAMENTS";
            // 
            // dataGridTournaments
            // 
            this.dataGridTournaments.AllowUserToAddRows = false;
            this.dataGridTournaments.AllowUserToOrderColumns = true;
            this.dataGridTournaments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridTournaments.AutoGenerateColumns = false;
            this.dataGridTournaments.BackgroundColor = System.Drawing.Color.White;
            this.dataGridTournaments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridTournaments.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridTournaments.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Black", 14F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridTournaments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridTournaments.ColumnHeadersHeight = 64;
            this.dataGridTournaments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.creationDateDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.NumPlayers,
            this.NumRounds,
            this.ButtonDelete});
            this.dataGridTournaments.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataGridTournaments.DataSource = this.tournamentsBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Black", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridTournaments.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridTournaments.EnableHeadersVisualStyles = false;
            this.dataGridTournaments.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridTournaments.Location = new System.Drawing.Point(12, 76);
            this.dataGridTournaments.Name = "dataGridTournaments";
            this.dataGridTournaments.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial Black", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridTournaments.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridTournaments.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial Black", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridTournaments.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridTournaments.Size = new System.Drawing.Size(984, 701);
            this.dataGridTournaments.TabIndex = 76;
            this.dataGridTournaments.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridTournaments_CellClick);
            this.dataGridTournaments.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridTournaments_CellEndEdit);
            this.dataGridTournaments.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridTournaments_CellPainting);
            this.dataGridTournaments.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridTournaments_RowsRemoved);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            this.Id.Width = 90;
            // 
            // creationDateDataGridViewTextBoxColumn
            // 
            this.creationDateDataGridViewTextBoxColumn.DataPropertyName = "CreationDate";
            this.creationDateDataGridViewTextBoxColumn.HeaderText = "Date";
            this.creationDateDataGridViewTextBoxColumn.MinimumWidth = 180;
            this.creationDateDataGridViewTextBoxColumn.Name = "creationDateDataGridViewTextBoxColumn";
            this.creationDateDataGridViewTextBoxColumn.ReadOnly = true;
            this.creationDateDataGridViewTextBoxColumn.Width = 180;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // NumPlayers
            // 
            this.NumPlayers.DataPropertyName = "NumPlayers";
            this.NumPlayers.HeaderText = "Players";
            this.NumPlayers.MinimumWidth = 100;
            this.NumPlayers.Name = "NumPlayers";
            this.NumPlayers.ReadOnly = true;
            // 
            // NumRounds
            // 
            this.NumRounds.DataPropertyName = "NumRounds";
            this.NumRounds.HeaderText = "Rounds";
            this.NumRounds.MinimumWidth = 100;
            this.NumRounds.Name = "NumRounds";
            this.NumRounds.ReadOnly = true;
            // 
            // ButtonDelete
            // 
            this.ButtonDelete.HeaderText = "";
            this.ButtonDelete.MinimumWidth = 90;
            this.ButtonDelete.Name = "ButtonDelete";
            this.ButtonDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ButtonDelete.Width = 90;
            // 
            // tournamentsBindingSource
            // 
            this.tournamentsBindingSource.DataMember = "Tournaments";
            this.tournamentsBindingSource.DataSource = this._MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet;
            // 
            // _MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet
            // 
            this._MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet.DataSetName = "_MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet";
            this._MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // imgLogoEMA
            // 
            this.imgLogoEMA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgLogoEMA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgLogoEMA.ErrorImage = ((System.Drawing.Image)(resources.GetObject("imgLogoEMA.ErrorImage")));
            this.imgLogoEMA.Image = ((System.Drawing.Image)(resources.GetObject("imgLogoEMA.Image")));
            this.imgLogoEMA.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.imgLogoEMA.Location = new System.Drawing.Point(951, 13);
            this.imgLogoEMA.Margin = new System.Windows.Forms.Padding(3, 54, 3, 3);
            this.imgLogoEMA.Name = "imgLogoEMA";
            this.imgLogoEMA.Size = new System.Drawing.Size(45, 49);
            this.imgLogoEMA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgLogoEMA.TabIndex = 77;
            this.imgLogoEMA.TabStop = false;
            this.imgLogoEMA.Click += new System.EventHandler(this.imgLogoEMA_Click);
            // 
            // imgLogoMM
            // 
            this.imgLogoMM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgLogoMM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgLogoMM.ErrorImage = ((System.Drawing.Image)(resources.GetObject("imgLogoMM.ErrorImage")));
            this.imgLogoMM.Image = ((System.Drawing.Image)(resources.GetObject("imgLogoMM.Image")));
            this.imgLogoMM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.imgLogoMM.Location = new System.Drawing.Point(900, 13);
            this.imgLogoMM.Margin = new System.Windows.Forms.Padding(3, 54, 3, 3);
            this.imgLogoMM.Name = "imgLogoMM";
            this.imgLogoMM.Size = new System.Drawing.Size(45, 49);
            this.imgLogoMM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgLogoMM.TabIndex = 78;
            this.imgLogoMM.TabStop = false;
            this.imgLogoMM.Click += new System.EventHandler(this.imgLogoMM_Click);
            // 
            // tournamentsTableAdapter
            // 
            this.tournamentsTableAdapter.ClearBeforeFill = true;
            // 
            // OldTournamentsForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1008, 790);
            this.Controls.Add(this.imgLogoMM);
            this.Controls.Add(this.imgLogoEMA);
            this.Controls.Add(this.dataGridTournaments);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnReturn);
            this.MinimumSize = new System.Drawing.Size(1024, 829);
            this.Name = "OldTournamentsForm";
            this.ShowInTaskbar = false;
            this.Text = "Old tournaments";
            this.Load += new System.EventHandler(this.OldTournamentsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTournaments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tournamentsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoEMA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoMM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dataGridTournaments;
        private System.Windows.Forms.PictureBox imgLogoEMA;
        private System.Windows.Forms.PictureBox imgLogoMM;
        private _MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet _MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet;
        private System.Windows.Forms.BindingSource tournamentsBindingSource;
        private _MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSetTableAdapters.TournamentsTableAdapter tournamentsTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn numPlayersDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numRoundsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn creationDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumPlayers;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumRounds;
        private System.Windows.Forms.DataGridViewTextBoxColumn ButtonDelete;
    }
}