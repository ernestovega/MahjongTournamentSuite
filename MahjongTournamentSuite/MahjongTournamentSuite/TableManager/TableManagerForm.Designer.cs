namespace MahjongTournamentSuite.TableManager
{
    partial class TableManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableManagerForm));
            this.lblTournamentName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRoundId = new System.Windows.Forms.Label();
            this.lblTableId = new System.Windows.Forms.Label();
            this.comboWestPlayer = new System.Windows.Forms.ComboBox();
            this.comboNorthPlayer = new System.Windows.Forms.ComboBox();
            this.comboSouthPlayer = new System.Windows.Forms.ComboBox();
            this.comboEastPlayer = new System.Windows.Forms.ComboBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.imgLogoMM = new System.Windows.Forms.PictureBox();
            this.imgLogoEMA = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoMM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoEMA)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTournamentName
            // 
            this.lblTournamentName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTournamentName.Font = new System.Drawing.Font("Arial Black", 24F);
            this.lblTournamentName.Location = new System.Drawing.Point(348, 16);
            this.lblTournamentName.Name = "lblTournamentName";
            this.lblTournamentName.Size = new System.Drawing.Size(336, 43);
            this.lblTournamentName.TabIndex = 27;
            this.lblTournamentName.Text = "Tournament name";
            this.lblTournamentName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Black", 18F);
            this.label1.Location = new System.Drawing.Point(302, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 33);
            this.label1.TabIndex = 28;
            this.label1.Text = "Round";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Black", 18F);
            this.label2.Location = new System.Drawing.Point(566, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 33);
            this.label2.TabIndex = 29;
            this.label2.Text = "Table";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblRoundId
            // 
            this.lblRoundId.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblRoundId.AutoSize = true;
            this.lblRoundId.Font = new System.Drawing.Font("Arial Black", 18F);
            this.lblRoundId.Location = new System.Drawing.Point(398, 71);
            this.lblRoundId.Name = "lblRoundId";
            this.lblRoundId.Size = new System.Drawing.Size(31, 33);
            this.lblRoundId.TabIndex = 30;
            this.lblRoundId.Text = "1";
            this.lblRoundId.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTableId
            // 
            this.lblTableId.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTableId.AutoSize = true;
            this.lblTableId.Font = new System.Drawing.Font("Arial Black", 18F);
            this.lblTableId.Location = new System.Drawing.Point(653, 71);
            this.lblTableId.Name = "lblTableId";
            this.lblTableId.Size = new System.Drawing.Size(31, 33);
            this.lblTableId.TabIndex = 31;
            this.lblTableId.Text = "1";
            this.lblTableId.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // comboWestPlayer
            // 
            this.comboWestPlayer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboWestPlayer.BackColor = System.Drawing.Color.White;
            this.comboWestPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboWestPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboWestPlayer.Font = new System.Drawing.Font("Arial Black", 12F);
            this.comboWestPlayer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboWestPlayer.FormattingEnabled = true;
            this.comboWestPlayer.Location = new System.Drawing.Point(509, 157);
            this.comboWestPlayer.Margin = new System.Windows.Forms.Padding(5);
            this.comboWestPlayer.MaxDropDownItems = 4;
            this.comboWestPlayer.Name = "comboWestPlayer";
            this.comboWestPlayer.Size = new System.Drawing.Size(240, 31);
            this.comboWestPlayer.TabIndex = 36;
            this.comboWestPlayer.SelectionChangeCommitted += new System.EventHandler(this.comboWestPlayer_SelectionChangeCommitted);
            // 
            // comboNorthPlayer
            // 
            this.comboNorthPlayer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboNorthPlayer.BackColor = System.Drawing.Color.White;
            this.comboNorthPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboNorthPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboNorthPlayer.Font = new System.Drawing.Font("Arial Black", 12F);
            this.comboNorthPlayer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboNorthPlayer.FormattingEnabled = true;
            this.comboNorthPlayer.Location = new System.Drawing.Point(759, 157);
            this.comboNorthPlayer.Margin = new System.Windows.Forms.Padding(5);
            this.comboNorthPlayer.MaxDropDownItems = 4;
            this.comboNorthPlayer.Name = "comboNorthPlayer";
            this.comboNorthPlayer.Size = new System.Drawing.Size(240, 31);
            this.comboNorthPlayer.TabIndex = 37;
            this.comboNorthPlayer.SelectionChangeCommitted += new System.EventHandler(this.comboNorthPlayer_SelectionChangeCommitted);
            // 
            // comboSouthPlayer
            // 
            this.comboSouthPlayer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboSouthPlayer.BackColor = System.Drawing.Color.White;
            this.comboSouthPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboSouthPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSouthPlayer.Font = new System.Drawing.Font("Arial Black", 12F);
            this.comboSouthPlayer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboSouthPlayer.FormattingEnabled = true;
            this.comboSouthPlayer.Location = new System.Drawing.Point(259, 157);
            this.comboSouthPlayer.Margin = new System.Windows.Forms.Padding(5);
            this.comboSouthPlayer.MaxDropDownItems = 4;
            this.comboSouthPlayer.Name = "comboSouthPlayer";
            this.comboSouthPlayer.Size = new System.Drawing.Size(240, 31);
            this.comboSouthPlayer.TabIndex = 38;
            this.comboSouthPlayer.SelectionChangeCommitted += new System.EventHandler(this.comboSouthPlayer_SelectionChangeCommitted);
            // 
            // comboEastPlayer
            // 
            this.comboEastPlayer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboEastPlayer.BackColor = System.Drawing.Color.White;
            this.comboEastPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboEastPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEastPlayer.Font = new System.Drawing.Font("Arial Black", 12F);
            this.comboEastPlayer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboEastPlayer.FormattingEnabled = true;
            this.comboEastPlayer.Location = new System.Drawing.Point(9, 157);
            this.comboEastPlayer.Margin = new System.Windows.Forms.Padding(5);
            this.comboEastPlayer.MaxDropDownItems = 4;
            this.comboEastPlayer.Name = "comboEastPlayer";
            this.comboEastPlayer.Size = new System.Drawing.Size(240, 31);
            this.comboEastPlayer.TabIndex = 39;
            this.comboEastPlayer.SelectionChangeCommitted += new System.EventHandler(this.comboEastPlayer_SelectionChangeCommitted);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Black", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeight = 32;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Black", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView.Location = new System.Drawing.Point(12, 230);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView.RowTemplate.Height = 48;
            this.dataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(984, 505);
            this.dataGridView.TabIndex = 26;
            this.dataGridView.Visible = false;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridHands_CellClick);
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridHands_CellContentClick);
            this.dataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridHands_CellContentDoubleClick);
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridHands_CellDoubleClick);
            this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridHands_CellEndEdit);
            this.dataGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridHands_CellEnter);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Black", 12F);
            this.label6.Location = new System.Drawing.Point(755, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 23);
            this.label6.TabIndex = 35;
            this.label6.Text = "North player";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Black", 12F);
            this.label5.Location = new System.Drawing.Point(505, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 23);
            this.label5.TabIndex = 34;
            this.label5.Text = "West player";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Black", 12F);
            this.label3.Location = new System.Drawing.Point(255, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 23);
            this.label3.TabIndex = 33;
            this.label3.Text = "South player";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Black", 12F);
            this.label4.Location = new System.Drawing.Point(5, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 23);
            this.label4.TabIndex = 32;
            this.label4.Text = "East player";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // imgLogoMM
            // 
            this.imgLogoMM.BackColor = System.Drawing.Color.Transparent;
            this.imgLogoMM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgLogoMM.ErrorImage = ((System.Drawing.Image)(resources.GetObject("imgLogoMM.ErrorImage")));
            this.imgLogoMM.Image = ((System.Drawing.Image)(resources.GetObject("imgLogoMM.Image")));
            this.imgLogoMM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.imgLogoMM.InitialImage = ((System.Drawing.Image)(resources.GetObject("imgLogoMM.InitialImage")));
            this.imgLogoMM.Location = new System.Drawing.Point(14, 14);
            this.imgLogoMM.Margin = new System.Windows.Forms.Padding(5);
            this.imgLogoMM.Name = "imgLogoMM";
            this.imgLogoMM.Size = new System.Drawing.Size(64, 64);
            this.imgLogoMM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgLogoMM.TabIndex = 52;
            this.imgLogoMM.TabStop = false;
            this.imgLogoMM.Click += new System.EventHandler(this.imgLogoMM_Click);
            // 
            // imgLogoEMA
            // 
            this.imgLogoEMA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgLogoEMA.BackColor = System.Drawing.Color.Transparent;
            this.imgLogoEMA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgLogoEMA.ErrorImage = global::MahjongTournamentSuite.Properties.Resources.EMALogo;
            this.imgLogoEMA.Image = global::MahjongTournamentSuite.Properties.Resources.EMALogo;
            this.imgLogoEMA.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.imgLogoEMA.InitialImage = global::MahjongTournamentSuite.Properties.Resources.EMALogo;
            this.imgLogoEMA.Location = new System.Drawing.Point(930, 14);
            this.imgLogoEMA.Margin = new System.Windows.Forms.Padding(5);
            this.imgLogoEMA.Name = "imgLogoEMA";
            this.imgLogoEMA.Size = new System.Drawing.Size(64, 64);
            this.imgLogoEMA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgLogoEMA.TabIndex = 66;
            this.imgLogoEMA.TabStop = false;
            this.imgLogoEMA.Click += new System.EventHandler(this.imgLogoEMA_Click);
            // 
            // TableManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1008, 748);
            this.Controls.Add(this.imgLogoEMA);
            this.Controls.Add(this.imgLogoMM);
            this.Controls.Add(this.comboEastPlayer);
            this.Controls.Add(this.comboSouthPlayer);
            this.Controls.Add(this.comboNorthPlayer);
            this.Controls.Add(this.comboWestPlayer);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblTableId);
            this.Controls.Add(this.lblRoundId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTournamentName);
            this.Controls.Add(this.dataGridView);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1024, 786);
            this.Name = "TableManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mahjong Tournament Suite - Table Manager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoMM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoEMA)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTournamentName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRoundId;
        private System.Windows.Forms.Label lblTableId;
        private System.Windows.Forms.ComboBox comboWestPlayer;
        private System.Windows.Forms.ComboBox comboNorthPlayer;
        private System.Windows.Forms.ComboBox comboSouthPlayer;
        private System.Windows.Forms.ComboBox comboEastPlayer;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox imgLogoMM;
        private System.Windows.Forms.PictureBox imgLogoEMA;
    }
}