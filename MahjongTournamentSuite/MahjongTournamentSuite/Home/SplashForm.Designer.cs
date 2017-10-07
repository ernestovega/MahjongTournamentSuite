namespace MahjongTournamentSuite.Home
{
    partial class SplashForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashForm));
            this.imgLogoMM = new System.Windows.Forms.PictureBox();
            this.imgLogoEMA = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoMM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoEMA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.imgLogoMM.TabIndex = 52;
            this.imgLogoMM.TabStop = false;
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
            this.imgLogoEMA.Location = new System.Drawing.Point(306, 14);
            this.imgLogoEMA.Margin = new System.Windows.Forms.Padding(5);
            this.imgLogoEMA.Name = "imgLogoEMA";
            this.imgLogoEMA.Size = new System.Drawing.Size(64, 64);
            this.imgLogoEMA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgLogoEMA.TabIndex = 66;
            this.imgLogoEMA.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(292, 339);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 67;
            this.label1.Text = "By Ernesto Vega";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.ErrorImage = global::MahjongTournamentSuite.Properties.Resources.MMLoading;
            this.pictureBox1.Image = global::MahjongTournamentSuite.Properties.Resources.MMLoading;
            this.pictureBox1.InitialImage = global::MahjongTournamentSuite.Properties.Resources.MMLoading;
            this.pictureBox1.Location = new System.Drawing.Point(86, 44);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(212, 292);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 68;
            this.pictureBox1.TabStop = false;
            // 
            // SplashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imgLogoEMA);
            this.Controls.Add(this.imgLogoMM);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SplashForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mahjong Tournament Suite - Splash";
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoMM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogoEMA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgLogoMM;
        private System.Windows.Forms.PictureBox imgLogoEMA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}