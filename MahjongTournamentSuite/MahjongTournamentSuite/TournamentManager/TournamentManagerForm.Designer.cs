﻿namespace MahjongTournamentSuite.TournamentManager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imgLogoMM = new System.Windows.Forms.PictureBox();
            this.imgLogoEMA = new System.Windows.Forms.PictureBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblTitleRounds = new System.Windows.Forms.Label();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.lblTitleTables = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.button7 = new System.Windows.Forms.Button();
            this.btnPlayers = new System.Windows.Forms.Button();
            this.btnRounds = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.btnTeams = new System.Windows.Forms.Button();
            this.panelMainButtons = new System.Windows.Forms.Panel();
            this.btnExportHTML = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnRankings = new System.Windows.Forms.Button();
            this.btnTimer = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoMM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoEMA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panelMainButtons.SuspendLayout();
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
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeight = 40;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.GridColor = System.Drawing.SystemColors.Control;
            this.dgv.Location = new System.Drawing.Point(14, 120);
            this.dgv.Margin = new System.Windows.Forms.Padding(5);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv.RowTemplate.Height = 32;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(980, 613);
            this.dgv.TabIndex = 66;
            this.dgv.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_CellFormatting);
            this.dgv.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_CellMouseClick);
            this.dgv.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_CellValidating);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(14, 111);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(5);
            this.splitContainer1.MinimumSize = new System.Drawing.Size(980, 282);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.splitContainer1.Panel1.Controls.Add(this.lblTitleRounds);
            this.splitContainer1.Panel1.Controls.Add(this.lblSeparator);
            this.splitContainer1.Panel1.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Panel1MinSize = 99;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.splitContainer1.Panel2.Controls.Add(this.lblTitleTables);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Panel2MinSize = 200;
            this.splitContainer1.Size = new System.Drawing.Size(980, 622);
            this.splitContainer1.SplitterDistance = 120;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 70;
            this.splitContainer1.Visible = false;
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
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Transparent;
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button6.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(147)))), ((int)(((byte)(76)))));
            this.button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.button6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button6.Image = global::MahjongTournamentSuite.Properties.Resources.export_excel;
            this.button6.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button6.Location = new System.Drawing.Point(-284, 0);
            this.button6.Margin = new System.Windows.Forms.Padding(25, 0, 5, 0);
            this.button6.Name = "button6";
            this.button6.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.button6.Size = new System.Drawing.Size(68, 64);
            this.button6.TabIndex = 72;
            this.button6.Text = "Excel";
            this.button6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox3.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.ErrorImage")));
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox3.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.InitialImage")));
            this.pictureBox3.Location = new System.Drawing.Point(-378, 0);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(5);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(64, 64);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 51;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.imgLogoMM_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Transparent;
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button7.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.button7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(147)))), ((int)(((byte)(76)))));
            this.button7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.button7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button7.Image = global::MahjongTournamentSuite.Properties.Resources.export_html;
            this.button7.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button7.Location = new System.Drawing.Point(-206, 0);
            this.button7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.button7.Name = "button7";
            this.button7.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.button7.Size = new System.Drawing.Size(68, 64);
            this.button7.TabIndex = 72;
            this.button7.Text = "HTML";
            this.button7.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.btnExportHTML_Click);
            // 
            // btnPlayers
            // 
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
            this.btnPlayers.Location = new System.Drawing.Point(78, 0);
            this.btnPlayers.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnPlayers.Name = "btnPlayers";
            this.btnPlayers.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.btnPlayers.Size = new System.Drawing.Size(68, 64);
            this.btnPlayers.TabIndex = 70;
            this.btnPlayers.Text = "Players";
            this.btnPlayers.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPlayers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPlayers.UseVisualStyleBackColor = false;
            this.btnPlayers.Click += new System.EventHandler(this.btnPlayers_Click);
            // 
            // btnRounds
            // 
            this.btnRounds.BackColor = System.Drawing.Color.Transparent;
            this.btnRounds.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRounds.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRounds.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnRounds.FlatAppearance.BorderSize = 0;
            this.btnRounds.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.btnRounds.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(147)))), ((int)(((byte)(76)))));
            this.btnRounds.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.btnRounds.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnRounds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRounds.Image = global::MahjongTournamentSuite.Properties.Resources.gong_big;
            this.btnRounds.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRounds.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRounds.Location = new System.Drawing.Point(156, 0);
            this.btnRounds.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnRounds.Name = "btnRounds";
            this.btnRounds.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.btnRounds.Size = new System.Drawing.Size(68, 64);
            this.btnRounds.TabIndex = 69;
            this.btnRounds.Text = "Rounds";
            this.btnRounds.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRounds.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRounds.UseVisualStyleBackColor = false;
            this.btnRounds.Click += new System.EventHandler(this.btnRounds_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox4.ErrorImage = global::MahjongTournamentSuite.Properties.Resources.EMALogo;
            this.pictureBox4.Image = global::MahjongTournamentSuite.Properties.Resources.EMALogo;
            this.pictureBox4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox4.InitialImage = global::MahjongTournamentSuite.Properties.Resources.EMALogo;
            this.pictureBox4.Location = new System.Drawing.Point(538, 0);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(5);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(64, 64);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 65;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.imgLogoEMA_Click);
            // 
            // btnTeams
            // 
            this.btnTeams.Anchor = System.Windows.Forms.AnchorStyles.Left;
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
            this.btnTeams.Location = new System.Drawing.Point(0, 0);
            this.btnTeams.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnTeams.Name = "btnTeams";
            this.btnTeams.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.btnTeams.Size = new System.Drawing.Size(68, 64);
            this.btnTeams.TabIndex = 71;
            this.btnTeams.Text = "Teams";
            this.btnTeams.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTeams.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTeams.UseVisualStyleBackColor = false;
            this.btnTeams.Visible = false;
            this.btnTeams.Click += new System.EventHandler(this.btnTeams_Click);
            // 
            // panelMainButtons
            // 
            this.panelMainButtons.Controls.Add(this.btnTeams);
            this.panelMainButtons.Controls.Add(this.pictureBox4);
            this.panelMainButtons.Controls.Add(this.btnRounds);
            this.panelMainButtons.Controls.Add(this.btnPlayers);
            this.panelMainButtons.Controls.Add(this.button7);
            this.panelMainButtons.Controls.Add(this.pictureBox3);
            this.panelMainButtons.Controls.Add(this.button6);
            this.panelMainButtons.Location = new System.Drawing.Point(392, 14);
            this.panelMainButtons.Name = "panelMainButtons";
            this.panelMainButtons.Size = new System.Drawing.Size(224, 64);
            this.panelMainButtons.TabIndex = 73;
            // 
            // btnExportHTML
            // 
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
            this.btnExportHTML.Location = new System.Drawing.Point(186, 14);
            this.btnExportHTML.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnExportHTML.Name = "btnExportHTML";
            this.btnExportHTML.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.btnExportHTML.Size = new System.Drawing.Size(68, 64);
            this.btnExportHTML.TabIndex = 72;
            this.btnExportHTML.Text = "HTML";
            this.btnExportHTML.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExportHTML.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportHTML.UseVisualStyleBackColor = false;
            this.btnExportHTML.Click += new System.EventHandler(this.btnExportHTML_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.BackColor = System.Drawing.Color.Transparent;
            this.btnExportExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnExportExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportExcel.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnExportExcel.FlatAppearance.BorderSize = 0;
            this.btnExportExcel.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.btnExportExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(147)))), ((int)(((byte)(76)))));
            this.btnExportExcel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnExportExcel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExportExcel.Image = global::MahjongTournamentSuite.Properties.Resources.export_excel;
            this.btnExportExcel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExportExcel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExportExcel.Location = new System.Drawing.Point(108, 14);
            this.btnExportExcel.Margin = new System.Windows.Forms.Padding(25, 0, 5, 0);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.btnExportExcel.Size = new System.Drawing.Size(68, 64);
            this.btnExportExcel.TabIndex = 72;
            this.btnExportExcel.Text = "Excel";
            this.btnExportExcel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportExcel.UseVisualStyleBackColor = false;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnRankings
            // 
            this.btnRankings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnRankings.Location = new System.Drawing.Point(754, 14);
            this.btnRankings.Margin = new System.Windows.Forms.Padding(5);
            this.btnRankings.Name = "btnRankings";
            this.btnRankings.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.btnRankings.Size = new System.Drawing.Size(68, 64);
            this.btnRankings.TabIndex = 14;
            this.btnRankings.Text = "Ranking";
            this.btnRankings.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRankings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRankings.UseVisualStyleBackColor = false;
            this.btnRankings.Click += new System.EventHandler(this.btnRankings_Click);
            // 
            // btnTimer
            // 
            this.btnTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnTimer.Location = new System.Drawing.Point(832, 14);
            this.btnTimer.Margin = new System.Windows.Forms.Padding(5, 5, 25, 5);
            this.btnTimer.Name = "btnTimer";
            this.btnTimer.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.btnTimer.Size = new System.Drawing.Size(68, 64);
            this.btnTimer.TabIndex = 25;
            this.btnTimer.Text = "Timer";
            this.btnTimer.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTimer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTimer.UseVisualStyleBackColor = false;
            this.btnTimer.Click += new System.EventHandler(this.btnTimer_Click);
            // 
            // TournamentManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1008, 747);
            this.Controls.Add(this.panelMainButtons);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imgLogoEMA);
            this.Controls.Add(this.btnExportHTML);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.imgLogoMM);
            this.Controls.Add(this.btnTimer);
            this.Controls.Add(this.btnRankings);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.splitContainer1);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panelMainButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox imgLogoMM;
        private System.Windows.Forms.PictureBox imgLogoEMA;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSeparator;
        private System.Windows.Forms.Label lblTitleRounds;
        private System.Windows.Forms.Label lblTitleTables;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button btnPlayers;
        private System.Windows.Forms.Button btnRounds;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Button btnTeams;
        private System.Windows.Forms.Panel panelMainButtons;
        private System.Windows.Forms.Button btnExportHTML;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnRankings;
        private System.Windows.Forms.Button btnTimer;
    }
}