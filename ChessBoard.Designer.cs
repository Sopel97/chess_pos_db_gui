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
            this.historyPanel = new System.Windows.Forms.Panel();
            this.moveHistoryGridView = new System.Windows.Forms.DataGridView();
            this.historyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moveHistoryGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // chessBoardPanel
            // 
            this.chessBoardPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chessBoardPanel.Location = new System.Drawing.Point(0, 0);
            this.chessBoardPanel.Margin = new System.Windows.Forms.Padding(0);
            this.chessBoardPanel.Name = "chessBoardPanel";
            this.chessBoardPanel.Size = new System.Drawing.Size(256, 256);
            this.chessBoardPanel.TabIndex = 0;
            this.chessBoardPanel.SizeChanged += new System.EventHandler(this.ChessBoardPanel_SizeChanged);
            this.chessBoardPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ChessBoardPanel_Paint);
            this.chessBoardPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChessBoardPanel_MouseDown);
            this.chessBoardPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChessBoardPanel_MouseUp);
            // 
            // historyPanel
            // 
            this.historyPanel.Controls.Add(this.moveHistoryGridView);
            this.historyPanel.Location = new System.Drawing.Point(259, 3);
            this.historyPanel.Name = "historyPanel";
            this.historyPanel.Size = new System.Drawing.Size(250, 250);
            this.historyPanel.TabIndex = 1;
            // 
            // moveHistoryGridView
            // 
            this.moveHistoryGridView.AllowUserToAddRows = false;
            this.moveHistoryGridView.AllowUserToDeleteRows = false;
            this.moveHistoryGridView.AllowUserToResizeColumns = false;
            this.moveHistoryGridView.AllowUserToResizeRows = false;
            this.moveHistoryGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.moveHistoryGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.moveHistoryGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.moveHistoryGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.moveHistoryGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.moveHistoryGridView.ColumnHeadersVisible = false;
            this.moveHistoryGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.moveHistoryGridView.GridColor = System.Drawing.SystemColors.Window;
            this.moveHistoryGridView.Location = new System.Drawing.Point(4, 4);
            this.moveHistoryGridView.MinimumSize = new System.Drawing.Size(203, 0);
            this.moveHistoryGridView.MultiSelect = false;
            this.moveHistoryGridView.Name = "moveHistoryGridView";
            this.moveHistoryGridView.ReadOnly = true;
            this.moveHistoryGridView.RowHeadersVisible = false;
            this.moveHistoryGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.moveHistoryGridView.Size = new System.Drawing.Size(203, 243);
            this.moveHistoryGridView.TabIndex = 0;
            // 
            // ChessBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.historyPanel);
            this.Controls.Add(this.chessBoardPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(1, 1);
            this.Name = "ChessBoard";
            this.Size = new System.Drawing.Size(512, 256);
            this.Load += new System.EventHandler(this.ChessBoard_Load);
            this.SizeChanged += new System.EventHandler(this.ChessBoard_SizeChanged);
            this.historyPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.moveHistoryGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel chessBoardPanel;
        private System.Windows.Forms.Panel historyPanel;
        private System.Windows.Forms.DataGridView moveHistoryGridView;
    }
}
