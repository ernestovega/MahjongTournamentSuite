namespace MahjongTournamentSuite.NewTournament
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
            this.labelPlayers = new System.Windows.Forms.Label();
            this.numUpDownPlayers = new System.Windows.Forms.NumericUpDown();
            this.numUpDownRounds = new System.Windows.Forms.NumericUpDown();
            this.lblRounds = new System.Windows.Forms.Label();
            this.lblTeams = new System.Windows.Forms.Label();
            this.cbTeams = new System.Windows.Forms.CheckBox();
            this.lblTriesMax = new System.Windows.Forms.Label();
            this.numUpDownTriesMax = new System.Windows.Forms.NumericUpDown();
            this.btnStart = new System.Windows.Forms.Button();
            this.tbTournamentName = new System.Windows.Forms.TextBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.panelOptions = new System.Windows.Forms.Panel();
            this.panelLoading = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblLoadingMessage = new System.Windows.Forms.Label();
            this.imgLogoEMA = new System.Windows.Forms.PictureBox();
            this.imgLogoMM = new System.Windows.Forms.PictureBox();
            this.btnReturn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPlayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRounds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownTriesMax)).BeginInit();
            this.panelOptions.SuspendLayout();
            this.panelLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoEMA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoMM)).BeginInit();
            this.SuspendLayout();
            // 
            // labelPlayers
            // 
            this.labelPlayers.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelPlayers.Font = new System.Drawing.Font("Arial Black", 14F);
            this.labelPlayers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelPlayers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelPlayers.Location = new System.Drawing.Point(82, 48);
            this.labelPlayers.Margin = new System.Windows.Forms.Padding(50, 50, 50, 10);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(90, 27);
            this.labelPlayers.TabIndex = 64;
            this.labelPlayers.Text = "Players";
            // 
            // numUpDownPlayers
            // 
            this.numUpDownPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numUpDownPlayers.BackColor = System.Drawing.Color.White;
            this.numUpDownPlayers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numUpDownPlayers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numUpDownPlayers.Font = new System.Drawing.Font("Arial Black", 14F);
            this.numUpDownPlayers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.numUpDownPlayers.Increment = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numUpDownPlayers.Location = new System.Drawing.Point(221, 47);
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
            this.numUpDownPlayers.Size = new System.Drawing.Size(71, 30);
            this.numUpDownPlayers.TabIndex = 65;
            this.numUpDownPlayers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDownPlayers.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // numUpDownRounds
            // 
            this.numUpDownRounds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numUpDownRounds.BackColor = System.Drawing.Color.White;
            this.numUpDownRounds.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numUpDownRounds.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numUpDownRounds.Font = new System.Drawing.Font("Arial Black", 14F);
            this.numUpDownRounds.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.numUpDownRounds.Location = new System.Drawing.Point(221, 97);
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
            this.numUpDownRounds.Size = new System.Drawing.Size(71, 30);
            this.numUpDownRounds.TabIndex = 66;
            this.numUpDownRounds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDownRounds.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // lblRounds
            // 
            this.lblRounds.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblRounds.AutoSize = true;
            this.lblRounds.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblRounds.Font = new System.Drawing.Font("Arial Black", 14F);
            this.lblRounds.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRounds.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRounds.Location = new System.Drawing.Point(81, 98);
            this.lblRounds.Margin = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.lblRounds.Name = "lblRounds";
            this.lblRounds.Size = new System.Drawing.Size(91, 27);
            this.lblRounds.TabIndex = 67;
            this.lblRounds.Text = "Rounds";
            // 
            // lblTeams
            // 
            this.lblTeams.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblTeams.AutoSize = true;
            this.lblTeams.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTeams.Font = new System.Drawing.Font("Arial Black", 14F);
            this.lblTeams.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTeams.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTeams.Location = new System.Drawing.Point(80, 145);
            this.lblTeams.Margin = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.lblTeams.Name = "lblTeams";
            this.lblTeams.Size = new System.Drawing.Size(94, 27);
            this.lblTeams.TabIndex = 68;
            this.lblTeams.Text = "Teams?";
            // 
            // cbTeams
            // 
            this.cbTeams.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTeams.AutoSize = true;
            this.cbTeams.Checked = true;
            this.cbTeams.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTeams.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbTeams.Font = new System.Drawing.Font("Arial Black", 14F);
            this.cbTeams.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbTeams.Location = new System.Drawing.Point(275, 152);
            this.cbTeams.Name = "cbTeams";
            this.cbTeams.Size = new System.Drawing.Size(15, 14);
            this.cbTeams.TabIndex = 69;
            this.cbTeams.UseVisualStyleBackColor = true;
            // 
            // lblTriesMax
            // 
            this.lblTriesMax.AccessibleDescription = "Number of tries to fill randomly the tournament until desist";
            this.lblTriesMax.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblTriesMax.AutoSize = true;
            this.lblTriesMax.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTriesMax.Font = new System.Drawing.Font("Arial Black", 12F);
            this.lblTriesMax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTriesMax.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTriesMax.Location = new System.Drawing.Point(75, 200);
            this.lblTriesMax.Margin = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.lblTriesMax.Name = "lblTriesMax";
            this.lblTriesMax.Size = new System.Drawing.Size(97, 23);
            this.lblTriesMax.TabIndex = 70;
            this.lblTriesMax.Text = "Max. tries";
            // 
            // numUpDownTriesMax
            // 
            this.numUpDownTriesMax.AccessibleDescription = "Number of tries to fill randomly the tournament until desist";
            this.numUpDownTriesMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numUpDownTriesMax.BackColor = System.Drawing.Color.White;
            this.numUpDownTriesMax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numUpDownTriesMax.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numUpDownTriesMax.Font = new System.Drawing.Font("Arial Black", 12F);
            this.numUpDownTriesMax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.numUpDownTriesMax.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDownTriesMax.Location = new System.Drawing.Point(190, 201);
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
            this.numUpDownTriesMax.Size = new System.Drawing.Size(100, 26);
            this.numUpDownTriesMax.TabIndex = 71;
            this.numUpDownTriesMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDownTriesMax.ThousandsSeparator = true;
            this.numUpDownTriesMax.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // btnStart
            // 
            this.btnStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(56)))));
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold);
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStart.Location = new System.Drawing.Point(217, 442);
            this.btnStart.Margin = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(363, 84);
            this.btnStart.TabIndex = 72;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tbTournamentName
            // 
            this.tbTournamentName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTournamentName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbTournamentName.Font = new System.Drawing.Font("Arial Black", 24F);
            this.tbTournamentName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbTournamentName.Location = new System.Drawing.Point(219, 12);
            this.tbTournamentName.MaxLength = 250;
            this.tbTournamentName.Multiline = true;
            this.tbTournamentName.Name = "tbTournamentName";
            this.tbTournamentName.Size = new System.Drawing.Size(358, 152);
            this.tbTournamentName.TabIndex = 76;
            this.tbTournamentName.Text = "Enter here Tournament name";
            this.tbTournamentName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // panelOptions
            // 
            this.panelOptions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelOptions.Controls.Add(this.labelPlayers);
            this.panelOptions.Controls.Add(this.numUpDownPlayers);
            this.panelOptions.Controls.Add(this.numUpDownRounds);
            this.panelOptions.Controls.Add(this.lblRounds);
            this.panelOptions.Controls.Add(this.lblTeams);
            this.panelOptions.Controls.Add(this.cbTeams);
            this.panelOptions.Controls.Add(this.lblTriesMax);
            this.panelOptions.Controls.Add(this.numUpDownTriesMax);
            this.panelOptions.Location = new System.Drawing.Point(219, 170);
            this.panelOptions.Name = "panelOptions";
            this.panelOptions.Size = new System.Drawing.Size(358, 250);
            this.panelOptions.TabIndex = 78;
            // 
            // panelLoading
            // 
            this.panelLoading.Controls.Add(this.pictureBox1);
            this.panelLoading.Controls.Add(this.lblLoadingMessage);
            this.panelLoading.Location = new System.Drawing.Point(219, 147);
            this.panelLoading.Name = "panelLoading";
            this.panelLoading.Size = new System.Drawing.Size(358, 294);
            this.panelLoading.TabIndex = 77;
            this.panelLoading.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::MahjongTournamentSuite.Properties.Resources.MMLoading160;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(358, 240);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // lblLoadingMessage
            // 
            this.lblLoadingMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLoadingMessage.Font = new System.Drawing.Font("Arial Black", 12F);
            this.lblLoadingMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLoadingMessage.Location = new System.Drawing.Point(3, 257);
            this.lblLoadingMessage.Name = "lblLoadingMessage";
            this.lblLoadingMessage.Size = new System.Drawing.Size(352, 23);
            this.lblLoadingMessage.TabIndex = 4;
            this.lblLoadingMessage.Text = "Message";
            this.lblLoadingMessage.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // imgLogoEMA
            // 
            this.imgLogoEMA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgLogoEMA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgLogoEMA.ErrorImage = ((System.Drawing.Image)(resources.GetObject("imgLogoEMA.ErrorImage")));
            this.imgLogoEMA.Image = ((System.Drawing.Image)(resources.GetObject("imgLogoEMA.Image")));
            this.imgLogoEMA.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.imgLogoEMA.Location = new System.Drawing.Point(583, 74);
            this.imgLogoEMA.Margin = new System.Windows.Forms.Padding(3, 50, 3, 3);
            this.imgLogoEMA.Name = "imgLogoEMA";
            this.imgLogoEMA.Size = new System.Drawing.Size(150, 150);
            this.imgLogoEMA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgLogoEMA.TabIndex = 63;
            this.imgLogoEMA.TabStop = false;
            this.imgLogoEMA.Click += new System.EventHandler(this.imgLogoEMA_Click);
            // 
            // imgLogoMM
            // 
            this.imgLogoMM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgLogoMM.ErrorImage = ((System.Drawing.Image)(resources.GetObject("imgLogoMM.ErrorImage")));
            this.imgLogoMM.Image = ((System.Drawing.Image)(resources.GetObject("imgLogoMM.Image")));
            this.imgLogoMM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.imgLogoMM.Location = new System.Drawing.Point(63, 74);
            this.imgLogoMM.Margin = new System.Windows.Forms.Padding(3, 50, 3, 3);
            this.imgLogoMM.Name = "imgLogoMM";
            this.imgLogoMM.Size = new System.Drawing.Size(150, 150);
            this.imgLogoMM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgLogoMM.TabIndex = 49;
            this.imgLogoMM.TabStop = false;
            this.imgLogoMM.Click += new System.EventHandler(this.imgLogoMM_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReturn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnReturn.FlatAppearance.BorderSize = 0;
            this.btnReturn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnReturn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Image = global::MahjongTournamentSuite.Properties.Resources.icon_return;
            this.btnReturn.Location = new System.Drawing.Point(9, 9);
            this.btnReturn.Margin = new System.Windows.Forms.Padding(0);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(50, 50);
            this.btnReturn.TabIndex = 20;
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // NewTournamentForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panelLoading);
            this.Controls.Add(this.imgLogoEMA);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.imgLogoMM);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.panelOptions);
            this.Controls.Add(this.tbTournamentName);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Verdana", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "NewTournamentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mahjong Tournament Suite - New Tournament";
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPlayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRounds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownTriesMax)).EndInit();
            this.panelOptions.ResumeLayout(false);
            this.panelOptions.PerformLayout();
            this.panelLoading.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoEMA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoMM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.PictureBox imgLogoMM;
        private System.Windows.Forms.PictureBox imgLogoEMA;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.NumericUpDown numUpDownPlayers;
        private System.Windows.Forms.NumericUpDown numUpDownRounds;
        private System.Windows.Forms.Label lblRounds;
        private System.Windows.Forms.Label lblTeams;
        private System.Windows.Forms.CheckBox cbTeams;
        private System.Windows.Forms.Label lblTriesMax;
        private System.Windows.Forms.NumericUpDown numUpDownTriesMax;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox tbTournamentName;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Panel panelOptions;
        private System.Windows.Forms.Panel panelLoading;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblLoadingMessage;
    }
}