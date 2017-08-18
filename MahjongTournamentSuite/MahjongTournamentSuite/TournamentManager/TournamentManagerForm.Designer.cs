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
            this.comboRounds = new System.Windows.Forms.ComboBox();
            this.panelTables = new System.Windows.Forms.Panel();
            this.btnRanking = new System.Windows.Forms.Button();
            this.btnTimer = new System.Windows.Forms.Button();
            this.imgLogoMM = new System.Windows.Forms.PictureBox();
            this.imgLogoEMA = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoMM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoEMA)).BeginInit();
            this.SuspendLayout();
            // 
            // comboRounds
            // 
            this.comboRounds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboRounds.BackColor = System.Drawing.Color.White;
            this.comboRounds.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboRounds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRounds.Font = new System.Drawing.Font("Arial Black", 12F);
            this.comboRounds.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboRounds.FormattingEnabled = true;
            this.comboRounds.Location = new System.Drawing.Point(372, 34);
            this.comboRounds.Margin = new System.Windows.Forms.Padding(5);
            this.comboRounds.MaxDropDownItems = 100;
            this.comboRounds.Name = "comboRounds";
            this.comboRounds.Size = new System.Drawing.Size(240, 31);
            this.comboRounds.TabIndex = 22;
            // 
            // panelTables
            // 
            this.panelTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTables.BackColor = System.Drawing.SystemColors.Control;
            this.panelTables.Location = new System.Drawing.Point(14, 88);
            this.panelTables.Margin = new System.Windows.Forms.Padding(5);
            this.panelTables.Name = "panelTables";
            this.panelTables.Size = new System.Drawing.Size(980, 640);
            this.panelTables.TabIndex = 23;
            // 
            // btnRanking
            // 
            this.btnRanking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRanking.BackColor = System.Drawing.Color.Transparent;
            this.btnRanking.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRanking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRanking.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnRanking.FlatAppearance.BorderSize = 0;
            this.btnRanking.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.btnRanking.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnRanking.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnRanking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRanking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnRanking.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRanking.Image = ((System.Drawing.Image)(resources.GetObject("btnRanking.Image")));
            this.btnRanking.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRanking.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRanking.Location = new System.Drawing.Point(772, 14);
            this.btnRanking.Margin = new System.Windows.Forms.Padding(5);
            this.btnRanking.Name = "btnRanking";
            this.btnRanking.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.btnRanking.Size = new System.Drawing.Size(64, 64);
            this.btnRanking.TabIndex = 14;
            this.btnRanking.Text = "Ranking";
            this.btnRanking.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRanking.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRanking.UseVisualStyleBackColor = false;
            this.btnRanking.Click += new System.EventHandler(this.btnRanking_Click);
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
            this.btnTimer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnTimer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnTimer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnTimer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTimer.Image = ((System.Drawing.Image)(resources.GetObject("btnTimer.Image")));
            this.btnTimer.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnTimer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnTimer.Location = new System.Drawing.Point(846, 14);
            this.btnTimer.Margin = new System.Windows.Forms.Padding(5);
            this.btnTimer.Name = "btnTimer";
            this.btnTimer.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.btnTimer.Size = new System.Drawing.Size(64, 64);
            this.btnTimer.TabIndex = 25;
            this.btnTimer.Text = "Timer";
            this.btnTimer.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTimer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTimer.UseVisualStyleBackColor = false;
            this.btnTimer.Click += new System.EventHandler(this.btnTimer_Click);
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
            // TournamentManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1008, 742);
            this.Controls.Add(this.imgLogoEMA);
            this.Controls.Add(this.imgLogoMM);
            this.Controls.Add(this.btnTimer);
            this.Controls.Add(this.btnRanking);
            this.Controls.Add(this.panelTables);
            this.Controls.Add(this.comboRounds);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1024, 726);
            this.Name = "TournamentManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mahjong Tournament Suite - Tournament Manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TournamentManagerForm_FormClosed);
            this.Resize += new System.EventHandler(this.TournamentManagerForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoMM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoEMA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox comboRounds;
        private System.Windows.Forms.Panel panelTables;
        private System.Windows.Forms.Button btnRanking;
        private System.Windows.Forms.Button btnTimer;
        private System.Windows.Forms.PictureBox imgLogoMM;
        private System.Windows.Forms.PictureBox imgLogoEMA;
    }
}