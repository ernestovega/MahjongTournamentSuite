namespace MahjongTournamentSuite.TeamsManager
{
    partial class TeamsManagerForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeamsManagerForm));
            this.dgvTeams = new System.Windows.Forms.DataGridView();
            this.lblStub = new System.Windows.Forms.Label();
            this.dgvTeamPlayers = new System.Windows.Forms.DataGridView();
            this.lblTitleTeamPlayers = new System.Windows.Forms.Label();
            this.lblTeamsTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTeams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTeamPlayers)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTeams
            // 
            this.dgvTeams.AllowUserToAddRows = false;
            this.dgvTeams.AllowUserToDeleteRows = false;
            this.dgvTeams.AllowUserToResizeColumns = false;
            this.dgvTeams.AllowUserToResizeRows = false;
            this.dgvTeams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvTeams.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTeams.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvTeams.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTeams.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTeams.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTeams.ColumnHeadersHeight = 40;
            this.dgvTeams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTeams.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTeams.GridColor = System.Drawing.SystemColors.Control;
            this.dgvTeams.Location = new System.Drawing.Point(14, 50);
            this.dgvTeams.Margin = new System.Windows.Forms.Padding(5);
            this.dgvTeams.MultiSelect = false;
            this.dgvTeams.Name = "dgvTeams";
            this.dgvTeams.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvTeams.RowHeadersVisible = false;
            this.dgvTeams.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvTeams.RowTemplate.Height = 32;
            this.dgvTeams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTeams.Size = new System.Drawing.Size(539, 684);
            this.dgvTeams.TabIndex = 76;
            this.dgvTeams.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvTeams_CellFormatting);
            this.dgvTeams.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTeams_CellMouseClick);
            this.dgvTeams.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvTeams_CellValidating);
            // 
            // lblStub
            // 
            this.lblStub.AutoSize = true;
            this.lblStub.Location = new System.Drawing.Point(0, 0);
            this.lblStub.Name = "lblStub";
            this.lblStub.Size = new System.Drawing.Size(0, 13);
            this.lblStub.TabIndex = 77;
            // 
            // dgvTeamPlayers
            // 
            this.dgvTeamPlayers.AllowUserToAddRows = false;
            this.dgvTeamPlayers.AllowUserToDeleteRows = false;
            this.dgvTeamPlayers.AllowUserToResizeColumns = false;
            this.dgvTeamPlayers.AllowUserToResizeRows = false;
            this.dgvTeamPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTeamPlayers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTeamPlayers.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvTeamPlayers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTeamPlayers.CausesValidation = false;
            this.dgvTeamPlayers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTeamPlayers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTeamPlayers.ColumnHeadersHeight = 40;
            this.dgvTeamPlayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTeamPlayers.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTeamPlayers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvTeamPlayers.EnableHeadersVisualStyles = false;
            this.dgvTeamPlayers.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvTeamPlayers.Location = new System.Drawing.Point(574, 50);
            this.dgvTeamPlayers.Margin = new System.Windows.Forms.Padding(5);
            this.dgvTeamPlayers.MultiSelect = false;
            this.dgvTeamPlayers.Name = "dgvTeamPlayers";
            this.dgvTeamPlayers.ReadOnly = true;
            this.dgvTeamPlayers.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTeamPlayers.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTeamPlayers.RowHeadersVisible = false;
            this.dgvTeamPlayers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvTeamPlayers.RowTemplate.Height = 32;
            this.dgvTeamPlayers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTeamPlayers.ShowCellErrors = false;
            this.dgvTeamPlayers.ShowCellToolTips = false;
            this.dgvTeamPlayers.ShowEditingIcon = false;
            this.dgvTeamPlayers.ShowRowErrors = false;
            this.dgvTeamPlayers.Size = new System.Drawing.Size(420, 298);
            this.dgvTeamPlayers.TabIndex = 78;
            // 
            // lblTitleTeamPlayers
            // 
            this.lblTitleTeamPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitleTeamPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleTeamPlayers.Location = new System.Drawing.Point(574, 24);
            this.lblTitleTeamPlayers.Margin = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.lblTitleTeamPlayers.Name = "lblTitleTeamPlayers";
            this.lblTitleTeamPlayers.Size = new System.Drawing.Size(421, 21);
            this.lblTitleTeamPlayers.TabIndex = 79;
            this.lblTitleTeamPlayers.Text = "Team 1 Players";
            this.lblTitleTeamPlayers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTeamsTitle
            // 
            this.lblTeamsTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTeamsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTeamsTitle.Location = new System.Drawing.Point(10, 24);
            this.lblTeamsTitle.Margin = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.lblTeamsTitle.Name = "lblTeamsTitle";
            this.lblTeamsTitle.Size = new System.Drawing.Size(543, 21);
            this.lblTeamsTitle.TabIndex = 80;
            this.lblTeamsTitle.Text = "Teams";
            this.lblTeamsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TeamsManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1008, 748);
            this.Controls.Add(this.lblTeamsTitle);
            this.Controls.Add(this.lblTitleTeamPlayers);
            this.Controls.Add(this.dgvTeamPlayers);
            this.Controls.Add(this.lblStub);
            this.Controls.Add(this.dgvTeams);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1024, 786);
            this.Name = "TeamsManagerForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mahjong Tournament Suite - Players Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TeamsManagerForm_FormClosing);
            this.Load += new System.EventHandler(this.TeamsManagerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTeams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTeamPlayers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTeams;
        private System.Windows.Forms.Label lblStub;
        private System.Windows.Forms.DataGridView dgvTeamPlayers;
        private System.Windows.Forms.Label lblTitleTeamPlayers;
        private System.Windows.Forms.Label lblTeamsTitle;
    }
}