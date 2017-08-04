namespace MahjongTournamentSuite.TableManager
{
    partial class TableManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableManagerForm));
            this.btnReturn = new System.Windows.Forms.Button();
            this.dataGridHands = new System.Windows.Forms.DataGridView();
            this.lblTournamentName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRoundId = new System.Windows.Forms.Label();
            this.lblTableId = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboWestPlayer = new System.Windows.Forms.ComboBox();
            this.comboNorthPlayer = new System.Windows.Forms.ComboBox();
            this.comboSouthPlayer = new System.Windows.Forms.ComboBox();
            this.comboEastPlayer = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridHands)).BeginInit();
            this.SuspendLayout();
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
            this.btnReturn.TabIndex = 25;
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // dataGridHands
            // 
            this.dataGridHands.AllowUserToAddRows = false;
            this.dataGridHands.AllowUserToDeleteRows = false;
            this.dataGridHands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridHands.BackgroundColor = System.Drawing.Color.White;
            this.dataGridHands.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridHands.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Black", 14F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridHands.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridHands.ColumnHeadersHeight = 32;
            this.dataGridHands.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Black", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridHands.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridHands.Location = new System.Drawing.Point(12, 251);
            this.dataGridHands.MultiSelect = false;
            this.dataGridHands.Name = "dataGridHands";
            this.dataGridHands.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridHands.RowHeadersVisible = false;
            this.dataGridHands.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridHands.RowTemplate.Height = 32;
            this.dataGridHands.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridHands.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridHands.Size = new System.Drawing.Size(984, 484);
            this.dataGridHands.TabIndex = 26;
            // 
            // lblTournamentName
            // 
            this.lblTournamentName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTournamentName.Font = new System.Drawing.Font("Arial Black", 16F);
            this.lblTournamentName.Location = new System.Drawing.Point(379, 16);
            this.lblTournamentName.Name = "lblTournamentName";
            this.lblTournamentName.Size = new System.Drawing.Size(236, 31);
            this.lblTournamentName.TabIndex = 27;
            this.lblTournamentName.Text = "Tournament name";
            this.lblTournamentName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Arial Black", 14F);
            this.label1.Location = new System.Drawing.Point(307, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 27);
            this.label1.TabIndex = 28;
            this.label1.Text = "Round";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Arial Black", 14F);
            this.label2.Location = new System.Drawing.Point(571, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 27);
            this.label2.TabIndex = 29;
            this.label2.Text = "Table";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblRoundId
            // 
            this.lblRoundId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRoundId.Font = new System.Drawing.Font("Arial Black", 14F);
            this.lblRoundId.Location = new System.Drawing.Point(403, 71);
            this.lblRoundId.Name = "lblRoundId";
            this.lblRoundId.Size = new System.Drawing.Size(25, 27);
            this.lblRoundId.TabIndex = 30;
            this.lblRoundId.Text = "1";
            this.lblRoundId.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTableId
            // 
            this.lblTableId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTableId.Font = new System.Drawing.Font("Arial Black", 14F);
            this.lblTableId.Location = new System.Drawing.Point(658, 71);
            this.lblTableId.Name = "lblTableId";
            this.lblTableId.Size = new System.Drawing.Size(25, 27);
            this.lblTableId.TabIndex = 31;
            this.lblTableId.Text = "1";
            this.lblTableId.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Black", 12F);
            this.label4.Location = new System.Drawing.Point(8, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 23);
            this.label4.TabIndex = 32;
            this.label4.Text = "East player";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Black", 12F);
            this.label3.Location = new System.Drawing.Point(548, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 23);
            this.label3.TabIndex = 33;
            this.label3.Text = "South player";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Black", 12F);
            this.label5.Location = new System.Drawing.Point(8, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 23);
            this.label5.TabIndex = 34;
            this.label5.Text = "West player";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Black", 12F);
            this.label6.Location = new System.Drawing.Point(548, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 23);
            this.label6.TabIndex = 35;
            this.label6.Text = "North player";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // comboWestPlayer
            // 
            this.comboWestPlayer.BackColor = System.Drawing.Color.White;
            this.comboWestPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboWestPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboWestPlayer.Font = new System.Drawing.Font("Arial Black", 12F);
            this.comboWestPlayer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboWestPlayer.FormattingEnabled = true;
            this.comboWestPlayer.Location = new System.Drawing.Point(130, 180);
            this.comboWestPlayer.Margin = new System.Windows.Forms.Padding(5);
            this.comboWestPlayer.MaxDropDownItems = 100;
            this.comboWestPlayer.Name = "comboWestPlayer";
            this.comboWestPlayer.Size = new System.Drawing.Size(320, 31);
            this.comboWestPlayer.TabIndex = 36;
            // 
            // comboNorthPlayer
            // 
            this.comboNorthPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboNorthPlayer.BackColor = System.Drawing.Color.White;
            this.comboNorthPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboNorthPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboNorthPlayer.Font = new System.Drawing.Font("Arial Black", 12F);
            this.comboNorthPlayer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboNorthPlayer.FormattingEnabled = true;
            this.comboNorthPlayer.Location = new System.Drawing.Point(676, 180);
            this.comboNorthPlayer.Margin = new System.Windows.Forms.Padding(5);
            this.comboNorthPlayer.MaxDropDownItems = 100;
            this.comboNorthPlayer.Name = "comboNorthPlayer";
            this.comboNorthPlayer.Size = new System.Drawing.Size(320, 31);
            this.comboNorthPlayer.TabIndex = 37;
            // 
            // comboSouthPlayer
            // 
            this.comboSouthPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboSouthPlayer.BackColor = System.Drawing.Color.White;
            this.comboSouthPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboSouthPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSouthPlayer.Font = new System.Drawing.Font("Arial Black", 12F);
            this.comboSouthPlayer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboSouthPlayer.FormattingEnabled = true;
            this.comboSouthPlayer.Location = new System.Drawing.Point(676, 135);
            this.comboSouthPlayer.Margin = new System.Windows.Forms.Padding(5);
            this.comboSouthPlayer.MaxDropDownItems = 100;
            this.comboSouthPlayer.Name = "comboSouthPlayer";
            this.comboSouthPlayer.Size = new System.Drawing.Size(320, 31);
            this.comboSouthPlayer.TabIndex = 38;
            // 
            // comboEastPlayer
            // 
            this.comboEastPlayer.BackColor = System.Drawing.Color.White;
            this.comboEastPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboEastPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEastPlayer.Font = new System.Drawing.Font("Arial Black", 12F);
            this.comboEastPlayer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboEastPlayer.FormattingEnabled = true;
            this.comboEastPlayer.Location = new System.Drawing.Point(130, 135);
            this.comboEastPlayer.Margin = new System.Windows.Forms.Padding(5);
            this.comboEastPlayer.MaxDropDownItems = 100;
            this.comboEastPlayer.Name = "comboEastPlayer";
            this.comboEastPlayer.Size = new System.Drawing.Size(320, 31);
            this.comboEastPlayer.TabIndex = 39;
            // 
            // TableManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1008, 747);
            this.Controls.Add(this.comboEastPlayer);
            this.Controls.Add(this.comboSouthPlayer);
            this.Controls.Add(this.comboNorthPlayer);
            this.Controls.Add(this.comboWestPlayer);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblTableId);
            this.Controls.Add(this.lblRoundId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTournamentName);
            this.Controls.Add(this.dataGridHands);
            this.Controls.Add(this.btnReturn);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1024, 786);
            this.Name = "TableManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mahjong Tournament Suite - Table Manager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridHands)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.DataGridView dataGridHands;
        private System.Windows.Forms.Label lblTournamentName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRoundId;
        private System.Windows.Forms.Label lblTableId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboWestPlayer;
        private System.Windows.Forms.ComboBox comboNorthPlayer;
        private System.Windows.Forms.ComboBox comboSouthPlayer;
        private System.Windows.Forms.ComboBox comboEastPlayer;
    }
}