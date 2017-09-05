namespace MahjongTournamentSuite.HTMLViewer
{
    partial class HTMLViewerForm
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
            this.tbPlayers = new System.Windows.Forms.TextBox();
            this.tbTeams = new System.Windows.Forms.TextBox();
            this.tbChickenHands = new System.Windows.Forms.TextBox();
            this.btnCopyPlayers = new System.Windows.Forms.Button();
            this.btnCopyTeams = new System.Windows.Forms.Button();
            this.btnCopyChickenHands = new System.Windows.Forms.Button();
            this.btnCopyAll = new System.Windows.Forms.Button();
            this.lblPlayers = new System.Windows.Forms.Label();
            this.lblTeams = new System.Windows.Forms.Label();
            this.lblChickenHands = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbPlayers
            // 
            this.tbPlayers.BackColor = System.Drawing.Color.White;
            this.tbPlayers.Location = new System.Drawing.Point(12, 25);
            this.tbPlayers.MaxLength = 500000;
            this.tbPlayers.MinimumSize = new System.Drawing.Size(4, 40);
            this.tbPlayers.Multiline = true;
            this.tbPlayers.Name = "tbPlayers";
            this.tbPlayers.ReadOnly = true;
            this.tbPlayers.Size = new System.Drawing.Size(430, 142);
            this.tbPlayers.TabIndex = 0;
            // 
            // tbTeams
            // 
            this.tbTeams.BackColor = System.Drawing.Color.White;
            this.tbTeams.Location = new System.Drawing.Point(12, 186);
            this.tbTeams.MaxLength = 500000;
            this.tbTeams.Multiline = true;
            this.tbTeams.Name = "tbTeams";
            this.tbTeams.ReadOnly = true;
            this.tbTeams.Size = new System.Drawing.Size(430, 142);
            this.tbTeams.TabIndex = 1;
            // 
            // tbChickenHands
            // 
            this.tbChickenHands.BackColor = System.Drawing.Color.White;
            this.tbChickenHands.Location = new System.Drawing.Point(12, 347);
            this.tbChickenHands.MaxLength = 500000;
            this.tbChickenHands.Multiline = true;
            this.tbChickenHands.Name = "tbChickenHands";
            this.tbChickenHands.ReadOnly = true;
            this.tbChickenHands.Size = new System.Drawing.Size(430, 142);
            this.tbChickenHands.TabIndex = 2;
            // 
            // btnCopyPlayers
            // 
            this.btnCopyPlayers.Location = new System.Drawing.Point(448, 23);
            this.btnCopyPlayers.Name = "btnCopyPlayers";
            this.btnCopyPlayers.Size = new System.Drawing.Size(54, 144);
            this.btnCopyPlayers.TabIndex = 3;
            this.btnCopyPlayers.Text = "Copy";
            this.btnCopyPlayers.UseVisualStyleBackColor = true;
            this.btnCopyPlayers.Click += new System.EventHandler(this.btnCopyPlayers_Click);
            // 
            // btnCopyTeams
            // 
            this.btnCopyTeams.Location = new System.Drawing.Point(448, 184);
            this.btnCopyTeams.Name = "btnCopyTeams";
            this.btnCopyTeams.Size = new System.Drawing.Size(54, 144);
            this.btnCopyTeams.TabIndex = 4;
            this.btnCopyTeams.Text = "Copy";
            this.btnCopyTeams.UseVisualStyleBackColor = true;
            this.btnCopyTeams.Click += new System.EventHandler(this.btnCopyTeams_Click);
            // 
            // btnCopyChickenHands
            // 
            this.btnCopyChickenHands.Location = new System.Drawing.Point(448, 345);
            this.btnCopyChickenHands.Name = "btnCopyChickenHands";
            this.btnCopyChickenHands.Size = new System.Drawing.Size(54, 144);
            this.btnCopyChickenHands.TabIndex = 5;
            this.btnCopyChickenHands.Text = "Copy";
            this.btnCopyChickenHands.UseVisualStyleBackColor = true;
            this.btnCopyChickenHands.Click += new System.EventHandler(this.btnCopyChickenHands_Click);
            // 
            // btnCopyAll
            // 
            this.btnCopyAll.Location = new System.Drawing.Point(508, 23);
            this.btnCopyAll.Name = "btnCopyAll";
            this.btnCopyAll.Size = new System.Drawing.Size(64, 466);
            this.btnCopyAll.TabIndex = 6;
            this.btnCopyAll.Text = "Copy all";
            this.btnCopyAll.UseVisualStyleBackColor = true;
            this.btnCopyAll.Click += new System.EventHandler(this.btnCopyAll_Click);
            // 
            // lblPlayers
            // 
            this.lblPlayers.AutoSize = true;
            this.lblPlayers.Location = new System.Drawing.Point(12, 9);
            this.lblPlayers.Name = "lblPlayers";
            this.lblPlayers.Size = new System.Drawing.Size(44, 13);
            this.lblPlayers.TabIndex = 7;
            this.lblPlayers.Text = "Players:";
            // 
            // lblTeams
            // 
            this.lblTeams.AutoSize = true;
            this.lblTeams.Location = new System.Drawing.Point(12, 170);
            this.lblTeams.Name = "lblTeams";
            this.lblTeams.Size = new System.Drawing.Size(42, 13);
            this.lblTeams.TabIndex = 8;
            this.lblTeams.Text = "Teams:";
            // 
            // lblChickenHands
            // 
            this.lblChickenHands.AutoSize = true;
            this.lblChickenHands.Location = new System.Drawing.Point(12, 331);
            this.lblChickenHands.Name = "lblChickenHands";
            this.lblChickenHands.Size = new System.Drawing.Size(81, 13);
            this.lblChickenHands.TabIndex = 9;
            this.lblChickenHands.Text = "Chicken hands:";
            // 
            // HTMLViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 501);
            this.Controls.Add(this.lblChickenHands);
            this.Controls.Add(this.lblTeams);
            this.Controls.Add(this.lblPlayers);
            this.Controls.Add(this.btnCopyAll);
            this.Controls.Add(this.btnCopyChickenHands);
            this.Controls.Add(this.btnCopyTeams);
            this.Controls.Add(this.btnCopyPlayers);
            this.Controls.Add(this.tbChickenHands);
            this.Controls.Add(this.tbTeams);
            this.Controls.Add(this.tbPlayers);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HTMLViewerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HTMLViewerForm";
            this.Load += new System.EventHandler(this.HTMLViewerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPlayers;
        private System.Windows.Forms.TextBox tbTeams;
        private System.Windows.Forms.TextBox tbChickenHands;
        private System.Windows.Forms.Button btnCopyPlayers;
        private System.Windows.Forms.Button btnCopyTeams;
        private System.Windows.Forms.Button btnCopyChickenHands;
        private System.Windows.Forms.Button btnCopyAll;
        private System.Windows.Forms.Label lblPlayers;
        private System.Windows.Forms.Label lblTeams;
        private System.Windows.Forms.Label lblChickenHands;
    }
}