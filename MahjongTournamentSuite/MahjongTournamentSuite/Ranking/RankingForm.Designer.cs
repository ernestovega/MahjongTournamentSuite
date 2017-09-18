namespace MahjongTournamentSuite.Ranking
{
    partial class RankingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RankingForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMaximize = new System.Windows.Forms.Button();
            this.lblRankingTitle = new System.Windows.Forms.Label();
            this.pbIconTitle = new System.Windows.Forms.PictureBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnSecondsDown = new System.Windows.Forms.Button();
            this.btnSecondsUp = new System.Windows.Forms.Button();
            this.lblSeconds = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.flowLayoutPanelTitle = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pbIconTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.flowLayoutPanelTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(147)))), ((int)(((byte)(76)))));
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClose.Image = global::MahjongTournamentSuite.Properties.Resources.no;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(999, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(25, 25);
            this.btnClose.TabIndex = 68;
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMaximize
            // 
            this.btnMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximize.BackColor = System.Drawing.Color.Transparent;
            this.btnMaximize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMaximize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaximize.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnMaximize.FlatAppearance.BorderSize = 0;
            this.btnMaximize.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.btnMaximize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(147)))), ((int)(((byte)(76)))));
            this.btnMaximize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            this.btnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnMaximize.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMaximize.Image = ((System.Drawing.Image)(resources.GetObject("btnMaximize.Image")));
            this.btnMaximize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMaximize.Location = new System.Drawing.Point(974, 0);
            this.btnMaximize.Margin = new System.Windows.Forms.Padding(0);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new System.Drawing.Size(25, 25);
            this.btnMaximize.TabIndex = 69;
            this.btnMaximize.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMaximize.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnMaximize.UseVisualStyleBackColor = false;
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // lblRankingTitle
            // 
            this.lblRankingTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRankingTitle.AutoSize = true;
            this.lblRankingTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblRankingTitle.Font = new System.Drawing.Font("Gang of Three", 28F, System.Drawing.FontStyle.Bold);
            this.lblRankingTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRankingTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblRankingTitle.Location = new System.Drawing.Point(79, 16);
            this.lblRankingTitle.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblRankingTitle.Name = "lblRankingTitle";
            this.lblRankingTitle.Size = new System.Drawing.Size(174, 42);
            this.lblRankingTitle.TabIndex = 70;
            this.lblRankingTitle.Text = "RANKING";
            this.lblRankingTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbIconTitle
            // 
            this.pbIconTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbIconTitle.BackColor = System.Drawing.Color.Transparent;
            this.pbIconTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbIconTitle.ErrorImage = global::MahjongTournamentSuite.Properties.Resources.ranking_big;
            this.pbIconTitle.Image = global::MahjongTournamentSuite.Properties.Resources.ranking_big;
            this.pbIconTitle.InitialImage = global::MahjongTournamentSuite.Properties.Resources.ranking_big;
            this.pbIconTitle.Location = new System.Drawing.Point(5, 5);
            this.pbIconTitle.Margin = new System.Windows.Forms.Padding(5);
            this.pbIconTitle.Name = "pbIconTitle";
            this.pbIconTitle.Size = new System.Drawing.Size(64, 64);
            this.pbIconTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbIconTitle.TabIndex = 75;
            this.pbIconTitle.TabStop = false;
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
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.CausesValidation = false;
            this.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeight = 52;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(32, 0, 32, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.Enabled = false;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.GridColor = System.Drawing.Color.White;
            this.dgv.Location = new System.Drawing.Point(34, 92);
            this.dgv.Margin = new System.Windows.Forms.Padding(25);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidth = 24;
            this.dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv.RowTemplate.Height = 40;
            this.dgv.RowTemplate.ReadOnly = true;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.ShowCellErrors = false;
            this.dgv.ShowCellToolTips = false;
            this.dgv.ShowEditingIcon = false;
            this.dgv.ShowRowErrors = false;
            this.dgv.Size = new System.Drawing.Size(956, 642);
            this.dgv.TabIndex = 67;
            // 
            // btnSecondsDown
            // 
            this.btnSecondsDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSecondsDown.BackgroundImage")));
            this.btnSecondsDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSecondsDown.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSecondsDown.FlatAppearance.BorderSize = 0;
            this.btnSecondsDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnSecondsDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnSecondsDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSecondsDown.Location = new System.Drawing.Point(19, 40);
            this.btnSecondsDown.Margin = new System.Windows.Forms.Padding(0);
            this.btnSecondsDown.Name = "btnSecondsDown";
            this.btnSecondsDown.Size = new System.Drawing.Size(24, 24);
            this.btnSecondsDown.TabIndex = 78;
            this.btnSecondsDown.UseVisualStyleBackColor = true;
            this.btnSecondsDown.Click += new System.EventHandler(this.btnSecondsDown_Click);
            // 
            // btnSecondsUp
            // 
            this.btnSecondsUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSecondsUp.BackgroundImage")));
            this.btnSecondsUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSecondsUp.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSecondsUp.FlatAppearance.BorderSize = 0;
            this.btnSecondsUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnSecondsUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnSecondsUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSecondsUp.Location = new System.Drawing.Point(19, 20);
            this.btnSecondsUp.Margin = new System.Windows.Forms.Padding(0);
            this.btnSecondsUp.Name = "btnSecondsUp";
            this.btnSecondsUp.Size = new System.Drawing.Size(24, 24);
            this.btnSecondsUp.TabIndex = 77;
            this.btnSecondsUp.UseVisualStyleBackColor = true;
            this.btnSecondsUp.Click += new System.EventHandler(this.btnSecondsUp_Click);
            // 
            // lblSeconds
            // 
            this.lblSeconds.AutoSize = true;
            this.lblSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F);
            this.lblSeconds.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblSeconds.Location = new System.Drawing.Point(44, 18);
            this.lblSeconds.Margin = new System.Windows.Forms.Padding(0);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(41, 44);
            this.lblSeconds.TabIndex = 76;
            this.lblSeconds.Text = "7";
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPause.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPause.BackgroundImage")));
            this.btnPause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPause.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPause.FlatAppearance.BorderSize = 0;
            this.btnPause.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnPause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Location = new System.Drawing.Point(911, 16);
            this.btnPause.Margin = new System.Windows.Forms.Padding(0);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(50, 50);
            this.btnPause.TabIndex = 80;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Visible = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPlay.BackgroundImage")));
            this.btnPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlay.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPlay.FlatAppearance.BorderSize = 0;
            this.btnPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Location = new System.Drawing.Point(911, 16);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(0);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(50, 50);
            this.btnPlay.TabIndex = 79;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // flowLayoutPanelTitle
            // 
            this.flowLayoutPanelTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.flowLayoutPanelTitle.AutoSize = true;
            this.flowLayoutPanelTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.flowLayoutPanelTitle.Controls.Add(this.pbIconTitle);
            this.flowLayoutPanelTitle.Controls.Add(this.lblRankingTitle);
            this.flowLayoutPanelTitle.Location = new System.Drawing.Point(306, 9);
            this.flowLayoutPanelTitle.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelTitle.Name = "flowLayoutPanelTitle";
            this.flowLayoutPanelTitle.Size = new System.Drawing.Size(424, 74);
            this.flowLayoutPanelTitle.TabIndex = 81;
            // 
            // RankingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.flowLayoutPanelTitle);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnSecondsDown);
            this.Controls.Add(this.btnSecondsUp);
            this.Controls.Add(this.lblSeconds);
            this.Controls.Add(this.btnMaximize);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "RankingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.RankingForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RankingForm_MouseDown);
            this.Resize += new System.EventHandler(this.RankingForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbIconTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.flowLayoutPanelTitle.ResumeLayout(false);
            this.flowLayoutPanelTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMaximize;
        private System.Windows.Forms.Label lblRankingTitle;
        private System.Windows.Forms.PictureBox pbIconTitle;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnSecondsDown;
        private System.Windows.Forms.Button btnSecondsUp;
        private System.Windows.Forms.Label lblSeconds;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTitle;
    }
}

