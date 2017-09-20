namespace MahjongTournamentSuite.TournamentManager
{
    partial class TournamentManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TournamentManagerForm));
            this.imgLogoMM = new System.Windows.Forms.PictureBox();
            this.imgLogoEMA = new System.Windows.Forms.PictureBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.lblTitleRounds = new System.Windows.Forms.Label();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.lblTitleTables = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPlayers = new System.Windows.Forms.Button();
            this.btnTeams = new System.Windows.Forms.Button();
            this.btnExportHTML = new System.Windows.Forms.Button();
            this.btnRankings = new System.Windows.Forms.Button();
            this.btnTimer = new System.Windows.Forms.Button();
            this.btnPlayersTables = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoMM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoEMA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
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
            this.imgLogoMM.TabIndex = 51;
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
            this.imgLogoEMA.TabIndex = 65;
            this.imgLogoEMA.TabStop = false;
            this.imgLogoEMA.Click += new System.EventHandler(this.imgLogoEMA_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(14, 111);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(5);
            this.splitContainer.MinimumSize = new System.Drawing.Size(980, 282);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.AutoScroll = true;
            this.splitContainer.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.splitContainer.Panel1.Controls.Add(this.lblTitleRounds);
            this.splitContainer.Panel1.Controls.Add(this.lblSeparator);
            this.splitContainer.Panel1.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.splitContainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer.Panel1MinSize = 99;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.AutoScroll = true;
            this.splitContainer.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.splitContainer.Panel2.Controls.Add(this.lblTitleTables);
            this.splitContainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer.Panel2MinSize = 200;
            this.splitContainer.Size = new System.Drawing.Size(980, 622);
            this.splitContainer.SplitterDistance = 120;
            this.splitContainer.SplitterWidth = 1;
            this.splitContainer.TabIndex = 70;
            // 
            // lblTitleRounds
            // 
            this.lblTitleRounds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitleRounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleRounds.Location = new System.Drawing.Point(443, 0);
            this.lblTitleRounds.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitleRounds.Name = "lblTitleRounds";
            this.lblTitleRounds.Size = new System.Drawing.Size(100, 23);
            this.lblTitleRounds.TabIndex = 73;
            this.lblTitleRounds.Text = "ROUNDS";
            this.lblTitleRounds.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSeparator
            // 
            this.lblSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSeparator.Location = new System.Drawing.Point(0, 118);
            this.lblSeparator.Margin = new System.Windows.Forms.Padding(0);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(980, 2);
            this.lblSeparator.TabIndex = 72;
            // 
            // lblTitleTables
            // 
            this.lblTitleTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitleTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleTables.Location = new System.Drawing.Point(443, 14);
            this.lblTitleTables.Margin = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.lblTitleTables.Name = "lblTitleTables";
            this.lblTitleTables.Size = new System.Drawing.Size(88, 24);
            this.lblTitleTables.TabIndex = 74;
            this.lblTitleTables.Text = "TABLES";
            this.lblTitleTables.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(14, 98);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 15, 5, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(980, 2);
            this.label1.TabIndex = 71;
            // 
            // btnPlayers
            // 
            this.btnPlayers.AutoSize = true;
            this.btnPlayers.BackColor = System.Drawing.Color.Transparent;
            this.btnPlayers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPlayers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlayers.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnPlayers.FlatAppearance.BorderSize = 0;
            this.btnPlayers.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.btnPlayers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(147)))), ((int)(((byte)(76)))));
            this.btnPlayers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.btnPlayers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnPlayers.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPlayers.Image = global::MahjongTournamentSuite.Properties.Resources.players;
            this.btnPlayers.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPlayers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPlayers.Location = new System.Drawing.Point(234, 14);
            this.btnPlayers.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnPlayers.Name = "btnPlayers";
            this.btnPlayers.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.btnPlayers.Size = new System.Drawing.Size(120, 64);
            this.btnPlayers.TabIndex = 70;
            this.btnPlayers.Text = "Manage Players";
            this.btnPlayers.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPlayers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPlayers.UseVisualStyleBackColor = false;
            this.btnPlayers.Click += new System.EventHandler(this.btnPlayers_Click);
            // 
            // btnTeams
            // 
            this.btnTeams.AutoSize = true;
            this.btnTeams.BackColor = System.Drawing.Color.Transparent;
            this.btnTeams.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnTeams.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTeams.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnTeams.FlatAppearance.BorderSize = 0;
            this.btnTeams.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.btnTeams.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(147)))), ((int)(((byte)(76)))));
            this.btnTeams.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.btnTeams.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTeams.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnTeams.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTeams.Image = global::MahjongTournamentSuite.Properties.Resources.teams;
            this.btnTeams.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnTeams.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnTeams.Location = new System.Drawing.Point(108, 14);
            this.btnTeams.Margin = new System.Windows.Forms.Padding(25, 0, 5, 0);
            this.btnTeams.Name = "btnTeams";
            this.btnTeams.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.btnTeams.Size = new System.Drawing.Size(116, 64);
            this.btnTeams.TabIndex = 71;
            this.btnTeams.Text = "Manage Teams";
            this.btnTeams.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTeams.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTeams.UseVisualStyleBackColor = false;
            this.btnTeams.Visible = false;
            this.btnTeams.Click += new System.EventHandler(this.btnTeams_Click);
            // 
            // btnExportHTML
            // 
            this.btnExportHTML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportHTML.AutoSize = true;
            this.btnExportHTML.BackColor = System.Drawing.Color.Transparent;
            this.btnExportHTML.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnExportHTML.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportHTML.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnExportHTML.FlatAppearance.BorderSize = 0;
            this.btnExportHTML.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.btnExportHTML.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(147)))), ((int)(((byte)(76)))));
            this.btnExportHTML.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.btnExportHTML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportHTML.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnExportHTML.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExportHTML.Image = global::MahjongTournamentSuite.Properties.Resources.export_html;
            this.btnExportHTML.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExportHTML.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExportHTML.Location = new System.Drawing.Point(566, 14);
            this.btnExportHTML.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnExportHTML.Name = "btnExportHTML";
            this.btnExportHTML.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.btnExportHTML.Size = new System.Drawing.Size(112, 64);
            this.btnExportHTML.TabIndex = 72;
            this.btnExportHTML.Text = "Html Ranking";
            this.btnExportHTML.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExportHTML.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportHTML.UseVisualStyleBackColor = false;
            this.btnExportHTML.Click += new System.EventHandler(this.btnExportHTML_Click);
            // 
            // btnRankings
            // 
            this.btnRankings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRankings.AutoSize = true;
            this.btnRankings.BackColor = System.Drawing.Color.Transparent;
            this.btnRankings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRankings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRankings.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnRankings.FlatAppearance.BorderSize = 0;
            this.btnRankings.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.btnRankings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(147)))), ((int)(((byte)(76)))));
            this.btnRankings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.btnRankings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRankings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnRankings.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRankings.Image = ((System.Drawing.Image)(resources.GetObject("btnRankings.Image")));
            this.btnRankings.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRankings.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRankings.Location = new System.Drawing.Point(688, 14);
            this.btnRankings.Margin = new System.Windows.Forms.Padding(5);
            this.btnRankings.Name = "btnRankings";
            this.btnRankings.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.btnRankings.Size = new System.Drawing.Size(109, 64);
            this.btnRankings.TabIndex = 14;
            this.btnRankings.Text = "Show Ranking";
            this.btnRankings.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRankings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRankings.UseVisualStyleBackColor = false;
            this.btnRankings.Click += new System.EventHandler(this.btnRankings_Click);
            // 
            // btnTimer
            // 
            this.btnTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTimer.AutoSize = true;
            this.btnTimer.BackColor = System.Drawing.Color.Transparent;
            this.btnTimer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnTimer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimer.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnTimer.FlatAppearance.BorderSize = 0;
            this.btnTimer.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.btnTimer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(147)))), ((int)(((byte)(76)))));
            this.btnTimer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.btnTimer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnTimer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTimer.Image = ((System.Drawing.Image)(resources.GetObject("btnTimer.Image")));
            this.btnTimer.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnTimer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnTimer.Location = new System.Drawing.Point(807, 14);
            this.btnTimer.Margin = new System.Windows.Forms.Padding(5, 5, 25, 5);
            this.btnTimer.Name = "btnTimer";
            this.btnTimer.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.btnTimer.Size = new System.Drawing.Size(93, 64);
            this.btnTimer.TabIndex = 25;
            this.btnTimer.Text = "Show Timer";
            this.btnTimer.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTimer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTimer.UseVisualStyleBackColor = false;
            this.btnTimer.Click += new System.EventHandler(this.btnTimer_Click);
            // 
            // btnPlayersTables
            // 
            this.btnPlayersTables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlayersTables.AutoSize = true;
            this.btnPlayersTables.BackColor = System.Drawing.Color.Transparent;
            this.btnPlayersTables.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPlayersTables.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlayersTables.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnPlayersTables.FlatAppearance.BorderSize = 0;
            this.btnPlayersTables.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.btnPlayersTables.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(147)))), ((int)(((byte)(76)))));
            this.btnPlayersTables.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.btnPlayersTables.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlayersTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnPlayersTables.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPlayersTables.Image = global::MahjongTournamentSuite.Properties.Resources.players_tables;
            this.btnPlayersTables.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPlayersTables.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPlayersTables.Location = new System.Drawing.Point(445, 14);
            this.btnPlayersTables.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnPlayersTables.Name = "btnPlayersTables";
            this.btnPlayersTables.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.btnPlayersTables.Size = new System.Drawing.Size(111, 64);
            this.btnPlayersTables.TabIndex = 73;
            this.btnPlayersTables.Text = "Players Tables";
            this.btnPlayersTables.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPlayersTables.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPlayersTables.UseVisualStyleBackColor = false;
            this.btnPlayersTables.Click += new System.EventHandler(this.btnPlayersTables_Click);
            // 
            // TournamentManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1008, 747);
            this.Controls.Add(this.btnPlayersTables);
            this.Controls.Add(this.btnTeams);
            this.Controls.Add(this.btnPlayers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imgLogoEMA);
            this.Controls.Add(this.btnExportHTML);
            this.Controls.Add(this.imgLogoMM);
            this.Controls.Add(this.btnTimer);
            this.Controls.Add(this.btnRankings);
            this.Controls.Add(this.splitContainer);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1024, 786);
            this.Name = "TournamentManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mahjong Tournament Suite - Tournament Manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TournamentManagerForm_FormClosed);
            this.Load += new System.EventHandler(this.TournamentManagerForm_Load);
            this.Resize += new System.EventHandler(this.TournamentManagerForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoMM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoEMA)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox imgLogoMM;
        private System.Windows.Forms.PictureBox imgLogoEMA;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSeparator;
        private System.Windows.Forms.Label lblTitleRounds;
        private System.Windows.Forms.Label lblTitleTables;
        private System.Windows.Forms.Button btnPlayers;
        private System.Windows.Forms.Button btnTeams;
        private System.Windows.Forms.Button btnExportHTML;
        private System.Windows.Forms.Button btnRankings;
        private System.Windows.Forms.Button btnTimer;
        private System.Windows.Forms.Button btnPlayersTables;
    }
}