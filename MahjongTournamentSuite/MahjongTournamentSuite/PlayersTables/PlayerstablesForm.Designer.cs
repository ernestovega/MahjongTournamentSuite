namespace MahjongTournamentSuite.PlayersTables
{
    partial class PlayersTablesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayersTablesForm));
            this.panel = new System.Windows.Forms.Panel();
            this.btnPlayersCards = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel.Location = new System.Drawing.Point(14, 78);
            this.panel.Margin = new System.Windows.Forms.Padding(5);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(980, 655);
            this.panel.TabIndex = 0;
            // 
            // btnPlayersCards
            // 
            this.btnPlayersCards.AutoSize = true;
            this.btnPlayersCards.BackColor = System.Drawing.Color.Transparent;
            this.btnPlayersCards.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPlayersCards.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlayersCards.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnPlayersCards.FlatAppearance.BorderSize = 0;
            this.btnPlayersCards.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.btnPlayersCards.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(147)))), ((int)(((byte)(76)))));
            this.btnPlayersCards.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.btnPlayersCards.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlayersCards.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnPlayersCards.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPlayersCards.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayersCards.Image")));
            this.btnPlayersCards.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPlayersCards.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPlayersCards.Location = new System.Drawing.Point(14, 9);
            this.btnPlayersCards.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnPlayersCards.MinimumSize = new System.Drawing.Size(64, 64);
            this.btnPlayersCards.Name = "btnPlayersCards";
            this.btnPlayersCards.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.btnPlayersCards.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnPlayersCards.Size = new System.Drawing.Size(105, 64);
            this.btnPlayersCards.TabIndex = 84;
            this.btnPlayersCards.Text = "Players Cards";
            this.btnPlayersCards.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPlayersCards.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPlayersCards.UseVisualStyleBackColor = false;
            this.btnPlayersCards.Click += new System.EventHandler(this.btnPlayersCards_Click);
            // 
            // PlayersTablesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1008, 748);
            this.Controls.Add(this.btnPlayersCards);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1024, 786);
            this.Name = "PlayersTablesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mahjong Tournament Suite - Players Tables";
            this.Load += new System.EventHandler(this.PlayerstablesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button btnPlayersCards;
    }
}