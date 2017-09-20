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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HTMLViewerForm));
            this.tbHtml = new System.Windows.Forms.TextBox();
            this.btnCopyPlayers = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbHtml
            // 
            this.tbHtml.BackColor = System.Drawing.Color.White;
            this.tbHtml.Location = new System.Drawing.Point(12, 14);
            this.tbHtml.Margin = new System.Windows.Forms.Padding(5);
            this.tbHtml.MaxLength = 500000;
            this.tbHtml.MinimumSize = new System.Drawing.Size(4, 40);
            this.tbHtml.Multiline = true;
            this.tbHtml.Name = "tbHtml";
            this.tbHtml.ReadOnly = true;
            this.tbHtml.Size = new System.Drawing.Size(478, 473);
            this.tbHtml.TabIndex = 0;
            // 
            // btnCopyPlayers
            // 
            this.btnCopyPlayers.Location = new System.Drawing.Point(500, 14);
            this.btnCopyPlayers.Margin = new System.Windows.Forms.Padding(5);
            this.btnCopyPlayers.Name = "btnCopyPlayers";
            this.btnCopyPlayers.Size = new System.Drawing.Size(72, 473);
            this.btnCopyPlayers.TabIndex = 3;
            this.btnCopyPlayers.Text = "Copy";
            this.btnCopyPlayers.UseVisualStyleBackColor = true;
            this.btnCopyPlayers.Click += new System.EventHandler(this.btnCopyHtml_Click);
            // 
            // HTMLViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 501);
            this.Controls.Add(this.btnCopyPlayers);
            this.Controls.Add(this.tbHtml);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HTMLViewerForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HTMLViewerForm";
            this.Load += new System.EventHandler(this.HTMLViewerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbHtml;
        private System.Windows.Forms.Button btnCopyPlayers;
    }
}