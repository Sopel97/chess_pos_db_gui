namespace chess_pos_db_gui
{
    partial class ChessBoard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chessBoardPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // chessBoardPanel
            // 
            this.chessBoardPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chessBoardPanel.Location = new System.Drawing.Point(17, 19);
            this.chessBoardPanel.Margin = new System.Windows.Forms.Padding(0);
            this.chessBoardPanel.Name = "chessBoardPanel";
            this.chessBoardPanel.Size = new System.Drawing.Size(256, 256);
            this.chessBoardPanel.TabIndex = 0;
            this.chessBoardPanel.SizeChanged += new System.EventHandler(this.ChessBoardPanel_SizeChanged);
            this.chessBoardPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ChessBoardPanel_Paint);
            // 
            // ChessBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chessBoardPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ChessBoard";
            this.Size = new System.Drawing.Size(368, 320);
            this.Load += new System.EventHandler(this.ChessBoard_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel chessBoardPanel;
    }
}
