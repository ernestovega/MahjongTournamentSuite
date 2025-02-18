﻿namespace MahjongTournamentSuite.NewTournament
{
    partial class NewTournamentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewTournamentForm));
            this.btnStart = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.panelLoading = new System.Windows.Forms.Panel();
            this.lblLoadingMessage = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.numUpDownTriesMax = new System.Windows.Forms.NumericUpDown();
            this.lblTriesMax = new System.Windows.Forms.Label();
            this.cbTeams = new System.Windows.Forms.CheckBox();
            this.tbTournamentName = new System.Windows.Forms.TextBox();
            this.lblTeams = new System.Windows.Forms.Label();
            this.lblRounds = new System.Windows.Forms.Label();
            this.numUpDownRounds = new System.Windows.Forms.NumericUpDown();
            this.numUpDownPlayers = new System.Windows.Forms.NumericUpDown();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.panelOptions = new System.Windows.Forms.Panel();
            this.lblTournamentName = new System.Windows.Forms.Label();
            this.panelLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownTriesMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRounds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPlayers)).BeginInit();
            this.panelOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(56)))));
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStart.Location = new System.Drawing.Point(235, 293);
            this.btnStart.Margin = new System.Windows.Forms.Padding(5);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(168, 35);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "START";
            this.btnStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // panelLoading
            // 
            this.panelLoading.Controls.Add(this.lblLoadingMessage);
            this.panelLoading.Controls.Add(this.pictureBox1);
            this.panelLoading.Location = new System.Drawing.Point(14, 19);
            this.panelLoading.Margin = new System.Windows.Forms.Padding(5);
            this.panelLoading.Name = "panelLoading";
            this.panelLoading.Size = new System.Drawing.Size(611, 259);
            this.panelLoading.TabIndex = 77;
            this.panelLoading.Visible = false;
            // 
            // lblLoadingMessage
            // 
            this.lblLoadingMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLoadingMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblLoadingMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLoadingMessage.Location = new System.Drawing.Point(13, 229);
            this.lblLoadingMessage.Margin = new System.Windows.Forms.Padding(5);
            this.lblLoadingMessage.Name = "lblLoadingMessage";
            this.lblLoadingMessage.Size = new System.Drawing.Size(586, 30);
            this.lblLoadingMessage.TabIndex = 4;
            this.lblLoadingMessage.Text = "Message";
            this.lblLoadingMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::MahjongTournamentSuite.Properties.Resources.MMLoading;
            this.pictureBox1.Location = new System.Drawing.Point(13, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(586, 259);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // numUpDownTriesMax
            // 
            this.numUpDownTriesMax.AccessibleDescription = "Number of tries to fill randomly the tournament until desist";
            this.numUpDownTriesMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numUpDownTriesMax.AutoSize = true;
            this.numUpDownTriesMax.BackColor = System.Drawing.SystemColors.Control;
            this.numUpDownTriesMax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numUpDownTriesMax.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numUpDownTriesMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUpDownTriesMax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.numUpDownTriesMax.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDownTriesMax.Location = new System.Drawing.Point(336, 225);
            this.numUpDownTriesMax.Margin = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.numUpDownTriesMax.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numUpDownTriesMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownTriesMax.Name = "numUpDownTriesMax";
            this.numUpDownTriesMax.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numUpDownTriesMax.Size = new System.Drawing.Size(104, 18);
            this.numUpDownTriesMax.TabIndex = 5;
            this.numUpDownTriesMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numUpDownTriesMax.ThousandsSeparator = true;
            this.numUpDownTriesMax.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numUpDownTriesMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbTournamentName_KeyPress);
            // 
            // lblTriesMax
            // 
            this.lblTriesMax.AccessibleDescription = "Number of tries to fill randomly the tournament until desist";
            this.lblTriesMax.AutoSize = true;
            this.lblTriesMax.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTriesMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTriesMax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTriesMax.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTriesMax.Location = new System.Drawing.Point(152, 224);
            this.lblTriesMax.Margin = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.lblTriesMax.Name = "lblTriesMax";
            this.lblTriesMax.Size = new System.Drawing.Size(64, 16);
            this.lblTriesMax.TabIndex = 70;
            this.lblTriesMax.Text = "Max. tries";
            // 
            // cbTeams
            // 
            this.cbTeams.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTeams.AutoSize = true;
            this.cbTeams.Checked = true;
            this.cbTeams.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTeams.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbTeams.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTeams.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbTeams.Location = new System.Drawing.Point(425, 178);
            this.cbTeams.Name = "cbTeams";
            this.cbTeams.Size = new System.Drawing.Size(15, 14);
            this.cbTeams.TabIndex = 4;
            this.cbTeams.UseVisualStyleBackColor = true;
            this.cbTeams.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbTournamentName_KeyPress);
            // 
            // tbTournamentName
            // 
            this.tbTournamentName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTournamentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTournamentName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbTournamentName.Location = new System.Drawing.Point(240, 25);
            this.tbTournamentName.Margin = new System.Windows.Forms.Padding(0);
            this.tbTournamentName.MaxLength = 250;
            this.tbTournamentName.Multiline = true;
            this.tbTournamentName.Name = "tbTournamentName";
            this.tbTournamentName.Size = new System.Drawing.Size(370, 23);
            this.tbTournamentName.TabIndex = 1;
            this.tbTournamentName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbTournamentName_KeyPress);
            // 
            // lblTeams
            // 
            this.lblTeams.AutoSize = true;
            this.lblTeams.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTeams.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTeams.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTeams.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTeams.Location = new System.Drawing.Point(157, 175);
            this.lblTeams.Margin = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.lblTeams.Name = "lblTeams";
            this.lblTeams.Size = new System.Drawing.Size(58, 16);
            this.lblTeams.TabIndex = 68;
            this.lblTeams.Text = "Teams?";
            // 
            // lblRounds
            // 
            this.lblRounds.AutoSize = true;
            this.lblRounds.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblRounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRounds.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRounds.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRounds.Location = new System.Drawing.Point(158, 128);
            this.lblRounds.Margin = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.lblRounds.Name = "lblRounds";
            this.lblRounds.Size = new System.Drawing.Size(55, 16);
            this.lblRounds.TabIndex = 67;
            this.lblRounds.Text = "Rounds";
            // 
            // numUpDownRounds
            // 
            this.numUpDownRounds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numUpDownRounds.AutoSize = true;
            this.numUpDownRounds.BackColor = System.Drawing.SystemColors.Control;
            this.numUpDownRounds.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numUpDownRounds.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numUpDownRounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUpDownRounds.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.numUpDownRounds.Location = new System.Drawing.Point(369, 129);
            this.numUpDownRounds.Margin = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.numUpDownRounds.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numUpDownRounds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownRounds.Name = "numUpDownRounds";
            this.numUpDownRounds.Size = new System.Drawing.Size(71, 18);
            this.numUpDownRounds.TabIndex = 3;
            this.numUpDownRounds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numUpDownRounds.ThousandsSeparator = true;
            this.numUpDownRounds.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numUpDownRounds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbTournamentName_KeyPress);
            // 
            // numUpDownPlayers
            // 
            this.numUpDownPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numUpDownPlayers.AutoSize = true;
            this.numUpDownPlayers.BackColor = System.Drawing.SystemColors.Control;
            this.numUpDownPlayers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numUpDownPlayers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numUpDownPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUpDownPlayers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.numUpDownPlayers.Increment = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numUpDownPlayers.Location = new System.Drawing.Point(369, 79);
            this.numUpDownPlayers.Margin = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.numUpDownPlayers.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDownPlayers.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numUpDownPlayers.Name = "numUpDownPlayers";
            this.numUpDownPlayers.Size = new System.Drawing.Size(71, 18);
            this.numUpDownPlayers.TabIndex = 2;
            this.numUpDownPlayers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numUpDownPlayers.ThousandsSeparator = true;
            this.numUpDownPlayers.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numUpDownPlayers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbTournamentName_KeyPress);
            // 
            // labelPlayers
            // 
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelPlayers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelPlayers.Location = new System.Drawing.Point(159, 78);
            this.labelPlayers.Margin = new System.Windows.Forms.Padding(50, 50, 50, 10);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(54, 16);
            this.labelPlayers.TabIndex = 64;
            this.labelPlayers.Text = "Players";
            // 
            // panelOptions
            // 
            this.panelOptions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelOptions.Controls.Add(this.lblTournamentName);
            this.panelOptions.Controls.Add(this.labelPlayers);
            this.panelOptions.Controls.Add(this.numUpDownPlayers);
            this.panelOptions.Controls.Add(this.numUpDownRounds);
            this.panelOptions.Controls.Add(this.lblRounds);
            this.panelOptions.Controls.Add(this.lblTeams);
            this.panelOptions.Controls.Add(this.tbTournamentName);
            this.panelOptions.Controls.Add(this.cbTeams);
            this.panelOptions.Controls.Add(this.lblTriesMax);
            this.panelOptions.Controls.Add(this.numUpDownTriesMax);
            this.panelOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelOptions.Location = new System.Drawing.Point(14, 14);
            this.panelOptions.Margin = new System.Windows.Forms.Padding(5);
            this.panelOptions.Name = "panelOptions";
            this.panelOptions.Size = new System.Drawing.Size(610, 269);
            this.panelOptions.TabIndex = 78;
            // 
            // lblTournamentName
            // 
            this.lblTournamentName.AutoSize = true;
            this.lblTournamentName.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTournamentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTournamentName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTournamentName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTournamentName.Location = new System.Drawing.Point(96, 28);
            this.lblTournamentName.Margin = new System.Windows.Forms.Padding(50, 50, 50, 10);
            this.lblTournamentName.Name = "lblTournamentName";
            this.lblTournamentName.Size = new System.Drawing.Size(117, 16);
            this.lblTournamentName.TabIndex = 77;
            this.lblTournamentName.Text = "Tournament name";
            // 
            // NewTournamentForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(638, 342);
            this.Controls.Add(this.panelLoading);
            this.Controls.Add(this.panelOptions);
            this.Controls.Add(this.btnStart);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewTournamentForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mahjong Tournament Suite - New Tournament";
            this.Load += new System.EventHandler(this.NewTournamentForm_Load);
            this.panelLoading.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownTriesMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRounds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPlayers)).EndInit();
            this.panelOptions.ResumeLayout(false);
            this.panelOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Panel panelLoading;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblLoadingMessage;
        private System.Windows.Forms.Panel panelOptions;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.NumericUpDown numUpDownPlayers;
        private System.Windows.Forms.NumericUpDown numUpDownRounds;
        private System.Windows.Forms.Label lblRounds;
        private System.Windows.Forms.Label lblTeams;
        private System.Windows.Forms.TextBox tbTournamentName;
        private System.Windows.Forms.CheckBox cbTeams;
        private System.Windows.Forms.Label lblTriesMax;
        private System.Windows.Forms.NumericUpDown numUpDownTriesMax;
        private System.Windows.Forms.Label lblTournamentName;
    }
}