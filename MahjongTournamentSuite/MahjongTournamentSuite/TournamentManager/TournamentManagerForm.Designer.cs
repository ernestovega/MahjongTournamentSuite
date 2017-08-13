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
            this.btnTimer = new System.Windows.Forms.Button();
            this.btnRankingShower = new System.Windows.Forms.Button();
            this.comboRounds = new System.Windows.Forms.ComboBox();
            this.panelTables = new System.Windows.Forms.Panel();
            this.btnReturn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTimer
            // 
            this.btnTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTimer.AutoSize = true;
            this.btnTimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnTimer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimer.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnTimer.FlatAppearance.BorderSize = 0;
            this.btnTimer.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnTimer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.btnTimer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(56)))));
            this.btnTimer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnTimer.ForeColor = System.Drawing.Color.White;
            this.btnTimer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnTimer.Location = new System.Drawing.Point(834, 14);
            this.btnTimer.Margin = new System.Windows.Forms.Padding(5);
            this.btnTimer.Name = "btnTimer";
            this.btnTimer.Size = new System.Drawing.Size(75, 75);
            this.btnTimer.TabIndex = 14;
            this.btnTimer.Text = "TIMER";
            this.btnTimer.UseVisualStyleBackColor = false;
            this.btnTimer.Click += new System.EventHandler(this.btnTimer_Click);
            // 
            // btnRankingShower
            // 
            this.btnRankingShower.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRankingShower.AutoSize = true;
            this.btnRankingShower.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnRankingShower.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRankingShower.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnRankingShower.FlatAppearance.BorderSize = 0;
            this.btnRankingShower.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnRankingShower.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.btnRankingShower.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(56)))));
            this.btnRankingShower.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRankingShower.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnRankingShower.ForeColor = System.Drawing.Color.White;
            this.btnRankingShower.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRankingShower.Location = new System.Drawing.Point(919, 14);
            this.btnRankingShower.Margin = new System.Windows.Forms.Padding(5);
            this.btnRankingShower.Name = "btnRankingShower";
            this.btnRankingShower.Size = new System.Drawing.Size(75, 75);
            this.btnRankingShower.TabIndex = 16;
            this.btnRankingShower.Text = "RANKING";
            this.btnRankingShower.UseVisualStyleBackColor = false;
            this.btnRankingShower.Click += new System.EventHandler(this.btnRankingShower_Click);
            // 
            // comboRounds
            // 
            this.comboRounds.BackColor = System.Drawing.Color.White;
            this.comboRounds.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboRounds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRounds.Font = new System.Drawing.Font("Arial Black", 12F);
            this.comboRounds.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboRounds.FormattingEnabled = true;
            this.comboRounds.Location = new System.Drawing.Point(108, 35);
            this.comboRounds.Margin = new System.Windows.Forms.Padding(5);
            this.comboRounds.MaxDropDownItems = 100;
            this.comboRounds.Name = "comboRounds";
            this.comboRounds.Size = new System.Drawing.Size(127, 31);
            this.comboRounds.TabIndex = 22;
            // 
            // panelTables
            // 
            this.panelTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTables.BackColor = System.Drawing.Color.White;
            this.panelTables.Location = new System.Drawing.Point(14, 99);
            this.panelTables.Margin = new System.Windows.Forms.Padding(5);
            this.panelTables.Name = "panelTables";
            this.panelTables.Size = new System.Drawing.Size(980, 629);
            this.panelTables.TabIndex = 23;
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
            this.btnReturn.TabIndex = 24;
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // TournamentManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1008, 742);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnTimer);
            this.Controls.Add(this.panelTables);
            this.Controls.Add(this.btnRankingShower);
            this.Controls.Add(this.comboRounds);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1024, 726);
            this.Name = "TournamentManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mahjong Tournament Suite - Tournament Manager";
            this.Resize += new System.EventHandler(this.TournamentManagerForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnTimer;
        private System.Windows.Forms.Button btnRankingShower;
        private System.Windows.Forms.ComboBox comboRounds;
        private System.Windows.Forms.Panel panelTables;
        private System.Windows.Forms.Button btnReturn;
    }
}