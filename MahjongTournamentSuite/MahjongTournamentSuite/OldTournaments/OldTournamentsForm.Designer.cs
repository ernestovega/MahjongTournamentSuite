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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OldTournamentsForm));
            this.btnReturn = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dataGridTournaments = new System.Windows.Forms.DataGridView();
            this.imgLogoEMA = new System.Windows.Forms.PictureBox();
            this.imgLogoMM = new System.Windows.Forms.PictureBox();
            this._MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet = new MahjongTournamentSuite._MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet();
            this.tournamentsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tournamentsTableAdapter = new MahjongTournamentSuite._MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSetTableAdapters.TournamentsTableAdapter();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.creationDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numPlayersDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numRoundsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTournaments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoEMA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoMM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tournamentsBindingSource)).BeginInit();
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
            this.btnReturn.Location = new System.Drawing.Point(9, 9);
            this.btnReturn.Margin = new System.Windows.Forms.Padding(0);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(50, 50);
            this.btnReturn.TabIndex = 21;
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTitle.Font = new System.Drawing.Font("Gang of Three", 32F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTitle.Location = new System.Drawing.Point(208, 9);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(50, 50, 50, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(392, 48);
            this.lblTitle.TabIndex = 75;
            this.lblTitle.Text = "OLD TOURNAMENTS";
            // 
            // dataGridTournaments
            // 
            this.dataGridTournaments.AllowUserToAddRows = false;
            this.dataGridTournaments.AllowUserToOrderColumns = true;
            this.dataGridTournaments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridTournaments.AutoGenerateColumns = false;
            this.dataGridTournaments.BackgroundColor = System.Drawing.Color.White;
            this.dataGridTournaments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridTournaments.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Gang of Three", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridTournaments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridTournaments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridTournaments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.creationDateDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.numPlayersDataGridViewTextBoxColumn,
            this.numRoundsDataGridViewTextBoxColumn});
            this.dataGridTournaments.DataSource = this.tournamentsBindingSource;
            this.dataGridTournaments.GridColor = System.Drawing.Color.White;
            this.dataGridTournaments.Location = new System.Drawing.Point(12, 70);
            this.dataGridTournaments.Name = "dataGridTournaments";
            this.dataGridTournaments.Size = new System.Drawing.Size(760, 479);
            this.dataGridTournaments.TabIndex = 76;
            this.dataGridTournaments.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridTournaments_CellEndEdit);
            this.dataGridTournaments.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridTournaments_RowsRemoved);
            // 
            // imgLogoEMA
            // 
            this.imgLogoEMA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgLogoEMA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgLogoEMA.ErrorImage = ((System.Drawing.Image)(resources.GetObject("imgLogoEMA.ErrorImage")));
            this.imgLogoEMA.Image = ((System.Drawing.Image)(resources.GetObject("imgLogoEMA.Image")));
            this.imgLogoEMA.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.imgLogoEMA.Location = new System.Drawing.Point(727, 12);
            this.imgLogoEMA.Margin = new System.Windows.Forms.Padding(3, 50, 3, 3);
            this.imgLogoEMA.Name = "imgLogoEMA";
            this.imgLogoEMA.Size = new System.Drawing.Size(45, 45);
            this.imgLogoEMA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgLogoEMA.TabIndex = 77;
            this.imgLogoEMA.TabStop = false;
            this.imgLogoEMA.Click += new System.EventHandler(this.imgLogoEMA_Click);
            // 
            // imgLogoMM
            // 
            this.imgLogoMM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgLogoMM.ErrorImage = ((System.Drawing.Image)(resources.GetObject("imgLogoMM.ErrorImage")));
            this.imgLogoMM.Image = ((System.Drawing.Image)(resources.GetObject("imgLogoMM.Image")));
            this.imgLogoMM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.imgLogoMM.Location = new System.Drawing.Point(676, 12);
            this.imgLogoMM.Margin = new System.Windows.Forms.Padding(3, 50, 3, 3);
            this.imgLogoMM.Name = "imgLogoMM";
            this.imgLogoMM.Size = new System.Drawing.Size(45, 45);
            this.imgLogoMM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgLogoMM.TabIndex = 78;
            this.imgLogoMM.TabStop = false;
            this.imgLogoMM.Click += new System.EventHandler(this.imgLogoMM_Click);
            // 
            // _MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet
            // 
            this._MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet.DataSetName = "_MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet";
            this._MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tournamentsBindingSource
            // 
            this.tournamentsBindingSource.DataMember = "Tournaments";
            this.tournamentsBindingSource.DataSource = this._MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet;
            // 
            // tournamentsTableAdapter
            // 
            this.tournamentsTableAdapter.ClearBeforeFill = true;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // creationDateDataGridViewTextBoxColumn
            // 
            this.creationDateDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.creationDateDataGridViewTextBoxColumn.DataPropertyName = "CreationDate";
            this.creationDateDataGridViewTextBoxColumn.HeaderText = "Creation date";
            this.creationDateDataGridViewTextBoxColumn.Name = "creationDateDataGridViewTextBoxColumn";
            this.creationDateDataGridViewTextBoxColumn.ReadOnly = true;
            this.creationDateDataGridViewTextBoxColumn.Width = 96;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // numPlayersDataGridViewTextBoxColumn
            // 
            this.numPlayersDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.numPlayersDataGridViewTextBoxColumn.DataPropertyName = "Players";
            this.numPlayersDataGridViewTextBoxColumn.HeaderText = "NumPlayers";
            this.numPlayersDataGridViewTextBoxColumn.Name = "numPlayersDataGridViewTextBoxColumn";
            this.numPlayersDataGridViewTextBoxColumn.ReadOnly = true;
            this.numPlayersDataGridViewTextBoxColumn.Width = 94;
            // 
            // numRoundsDataGridViewTextBoxColumn
            // 
            this.numRoundsDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.numRoundsDataGridViewTextBoxColumn.DataPropertyName = "Rounds";
            this.numRoundsDataGridViewTextBoxColumn.HeaderText = "NumRounds";
            this.numRoundsDataGridViewTextBoxColumn.Name = "numRoundsDataGridViewTextBoxColumn";
            this.numRoundsDataGridViewTextBoxColumn.ReadOnly = true;
            this.numRoundsDataGridViewTextBoxColumn.Width = 90;
            // 
            // OldTournamentsForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.imgLogoMM);
            this.Controls.Add(this.imgLogoEMA);
            this.Controls.Add(this.dataGridTournaments);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnReturn);
            this.Font = new System.Drawing.Font("Gang of Three", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "OldTournamentsForm";
            this.ShowInTaskbar = false;
            this.Text = "Old tournaments";
            this.Load += new System.EventHandler(this.OldTournamentsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTournaments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoEMA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoMM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tournamentsBindingSource)).EndInit();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn creationDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numPlayersDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numRoundsDataGridViewTextBoxColumn;
    }
}