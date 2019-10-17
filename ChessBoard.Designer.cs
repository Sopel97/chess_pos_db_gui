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
            this.goToEndButton = new System.Windows.Forms.Button();
            this.goToNextButton = new System.Windows.Forms.Button();
            this.goToPrevButton = new System.Windows.Forms.Button();
            this.goToStartButton = new System.Windows.Forms.Button();
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
            this.historyPanel.Controls.Add(this.goToEndButton);
            this.historyPanel.Controls.Add(this.goToNextButton);
            this.historyPanel.Controls.Add(this.goToPrevButton);
            this.historyPanel.Controls.Add(this.goToStartButton);
            this.historyPanel.Controls.Add(this.moveHistoryGridView);
            this.historyPanel.Location = new System.Drawing.Point(259, 3);
            this.historyPanel.Name = "historyPanel";
            this.historyPanel.Size = new System.Drawing.Size(632, 250);
            this.historyPanel.TabIndex = 1;
            // 
            // goToEndButton
            // 
            this.goToEndButton.Location = new System.Drawing.Point(118, 4);
            this.goToEndButton.Name = "goToEndButton";
            this.goToEndButton.Size = new System.Drawing.Size(32, 32);
            this.goToEndButton.TabIndex = 4;
            this.goToEndButton.Text = ">>";
            this.goToEndButton.UseVisualStyleBackColor = true;
            this.goToEndButton.Click += new System.EventHandler(this.GoToEndButton_Click);
            // 
            // goToNextButton
            // 
            this.goToNextButton.Location = new System.Drawing.Point(80, 4);
            this.goToNextButton.Name = "goToNextButton";
            this.goToNextButton.Size = new System.Drawing.Size(32, 32);
            this.goToNextButton.TabIndex = 3;
            this.goToNextButton.Text = ">";
            this.goToNextButton.UseVisualStyleBackColor = true;
            this.goToNextButton.Click += new System.EventHandler(this.GoToNextButton_Click);
            // 
            // goToPrevButton
            // 
            this.goToPrevButton.Location = new System.Drawing.Point(42, 4);
            this.goToPrevButton.Name = "goToPrevButton";
            this.goToPrevButton.Size = new System.Drawing.Size(32, 32);
            this.goToPrevButton.TabIndex = 2;
            this.goToPrevButton.Text = "<";
            this.goToPrevButton.UseVisualStyleBackColor = true;
            this.goToPrevButton.Click += new System.EventHandler(this.GoToPrevButton_Click);
            // 
            // goToStartButton
            // 
            this.goToStartButton.Location = new System.Drawing.Point(4, 4);
            this.goToStartButton.Name = "goToStartButton";
            this.goToStartButton.Size = new System.Drawing.Size(32, 32);
            this.goToStartButton.TabIndex = 1;
            this.goToStartButton.Text = "<<";
            this.goToStartButton.UseVisualStyleBackColor = true;
            this.goToStartButton.Click += new System.EventHandler(this.GoToStartButton_Click);
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
            this.moveHistoryGridView.Location = new System.Drawing.Point(4, 42);
            this.moveHistoryGridView.MinimumSize = new System.Drawing.Size(146, 0);
            this.moveHistoryGridView.MultiSelect = false;
            this.moveHistoryGridView.Name = "moveHistoryGridView";
            this.moveHistoryGridView.ReadOnly = true;
            this.moveHistoryGridView.RowHeadersVisible = false;
            this.moveHistoryGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.moveHistoryGridView.Size = new System.Drawing.Size(146, 205);
            this.moveHistoryGridView.TabIndex = 0;
            this.moveHistoryGridView.SelectionChanged += new System.EventHandler(this.MoveHistoryGridView_SelectionChanged);
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
            this.Size = new System.Drawing.Size(894, 256);
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
        private System.Windows.Forms.Button goToEndButton;
        private System.Windows.Forms.Button goToNextButton;
        private System.Windows.Forms.Button goToPrevButton;
        private System.Windows.Forms.Button goToStartButton;
    }
}
