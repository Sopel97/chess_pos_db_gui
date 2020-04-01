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
            this.fenTextBox = new System.Windows.Forms.TextBox();
            this.moveHistoryGridView = new System.Windows.Forms.DataGridView();
            this.goToStartButton = new System.Windows.Forms.Button();
            this.goToPrevButton = new System.Windows.Forms.Button();
            this.goToNextButton = new System.Windows.Forms.Button();
            this.goToEndButton = new System.Windows.Forms.Button();
            this.splitBoardAndMoves = new System.Windows.Forms.SplitContainer();
            this.splitFenAndControls = new System.Windows.Forms.SplitContainer();
            this.setPgnButton = new System.Windows.Forms.Button();
            this.setFenButton = new System.Windows.Forms.Button();
            this.flipBoardButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.copyFenButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.moveHistoryGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitBoardAndMoves)).BeginInit();
            this.splitBoardAndMoves.Panel1.SuspendLayout();
            this.splitBoardAndMoves.Panel2.SuspendLayout();
            this.splitBoardAndMoves.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitFenAndControls)).BeginInit();
            this.splitFenAndControls.Panel1.SuspendLayout();
            this.splitFenAndControls.Panel2.SuspendLayout();
            this.splitFenAndControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // chessBoardPanel
            // 
            this.chessBoardPanel.Location = new System.Drawing.Point(0, 0);
            this.chessBoardPanel.Margin = new System.Windows.Forms.Padding(0);
            this.chessBoardPanel.MinimumSize = new System.Drawing.Size(16, 16);
            this.chessBoardPanel.Name = "chessBoardPanel";
            this.chessBoardPanel.Size = new System.Drawing.Size(97, 133);
            this.chessBoardPanel.TabIndex = 0;
            this.chessBoardPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ChessBoardPanel_Paint);
            this.chessBoardPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChessBoardPanel_MouseDown);
            this.chessBoardPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChessBoardPanel_MouseUp);
            // 
            // fenTextBox
            // 
            this.fenTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fenTextBox.Enabled = false;
            this.fenTextBox.Location = new System.Drawing.Point(3, 3);
            this.fenTextBox.Name = "fenTextBox";
            this.fenTextBox.Size = new System.Drawing.Size(421, 20);
            this.fenTextBox.TabIndex = 5;
            // 
            // moveHistoryGridView
            // 
            this.moveHistoryGridView.AllowUserToAddRows = false;
            this.moveHistoryGridView.AllowUserToDeleteRows = false;
            this.moveHistoryGridView.AllowUserToResizeColumns = false;
            this.moveHistoryGridView.AllowUserToResizeRows = false;
            this.moveHistoryGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.moveHistoryGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.moveHistoryGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.moveHistoryGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.moveHistoryGridView.ColumnHeadersVisible = false;
            this.moveHistoryGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moveHistoryGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.moveHistoryGridView.GridColor = System.Drawing.SystemColors.Window;
            this.moveHistoryGridView.Location = new System.Drawing.Point(0, 0);
            this.moveHistoryGridView.MinimumSize = new System.Drawing.Size(146, 0);
            this.moveHistoryGridView.MultiSelect = false;
            this.moveHistoryGridView.Name = "moveHistoryGridView";
            this.moveHistoryGridView.ReadOnly = true;
            this.moveHistoryGridView.RowHeadersVisible = false;
            this.moveHistoryGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.moveHistoryGridView.Size = new System.Drawing.Size(154, 259);
            this.moveHistoryGridView.TabIndex = 0;
            this.moveHistoryGridView.SelectionChanged += new System.EventHandler(this.MoveHistoryGridView_SelectionChanged);
            // 
            // goToStartButton
            // 
            this.goToStartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.goToStartButton.Location = new System.Drawing.Point(278, 53);
            this.goToStartButton.Name = "goToStartButton";
            this.goToStartButton.Size = new System.Drawing.Size(32, 20);
            this.goToStartButton.TabIndex = 1;
            this.goToStartButton.Text = "<<";
            this.goToStartButton.UseVisualStyleBackColor = true;
            this.goToStartButton.Click += new System.EventHandler(this.GoToStartButton_Click);
            // 
            // goToPrevButton
            // 
            this.goToPrevButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.goToPrevButton.Location = new System.Drawing.Point(316, 53);
            this.goToPrevButton.Name = "goToPrevButton";
            this.goToPrevButton.Size = new System.Drawing.Size(32, 20);
            this.goToPrevButton.TabIndex = 2;
            this.goToPrevButton.Text = "<";
            this.goToPrevButton.UseVisualStyleBackColor = true;
            this.goToPrevButton.Click += new System.EventHandler(this.GoToPrevButton_Click);
            // 
            // goToNextButton
            // 
            this.goToNextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.goToNextButton.Location = new System.Drawing.Point(354, 53);
            this.goToNextButton.Name = "goToNextButton";
            this.goToNextButton.Size = new System.Drawing.Size(32, 20);
            this.goToNextButton.TabIndex = 3;
            this.goToNextButton.Text = ">";
            this.goToNextButton.UseVisualStyleBackColor = true;
            this.goToNextButton.Click += new System.EventHandler(this.GoToNextButton_Click);
            // 
            // goToEndButton
            // 
            this.goToEndButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.goToEndButton.Location = new System.Drawing.Point(392, 53);
            this.goToEndButton.Name = "goToEndButton";
            this.goToEndButton.Size = new System.Drawing.Size(32, 20);
            this.goToEndButton.TabIndex = 4;
            this.goToEndButton.Text = ">>";
            this.goToEndButton.UseVisualStyleBackColor = true;
            this.goToEndButton.Click += new System.EventHandler(this.GoToEndButton_Click);
            // 
            // splitBoardAndMoves
            // 
            this.splitBoardAndMoves.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitBoardAndMoves.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitBoardAndMoves.IsSplitterFixed = true;
            this.splitBoardAndMoves.Location = new System.Drawing.Point(0, 0);
            this.splitBoardAndMoves.Name = "splitBoardAndMoves";
            // 
            // splitBoardAndMoves.Panel1
            // 
            this.splitBoardAndMoves.Panel1.Controls.Add(this.chessBoardPanel);
            this.splitBoardAndMoves.Panel1.SizeChanged += new System.EventHandler(this.SplitContainer1_Panel1_SizeChanged);
            // 
            // splitBoardAndMoves.Panel2
            // 
            this.splitBoardAndMoves.Panel2.Controls.Add(this.moveHistoryGridView);
            this.splitBoardAndMoves.Size = new System.Drawing.Size(429, 259);
            this.splitBoardAndMoves.SplitterDistance = 271;
            this.splitBoardAndMoves.TabIndex = 6;
            // 
            // splitFenAndControls
            // 
            this.splitFenAndControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitFenAndControls.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitFenAndControls.IsSplitterFixed = true;
            this.splitFenAndControls.Location = new System.Drawing.Point(0, 0);
            this.splitFenAndControls.Name = "splitFenAndControls";
            this.splitFenAndControls.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitFenAndControls.Panel1
            // 
            this.splitFenAndControls.Panel1.Controls.Add(this.button1);
            this.splitFenAndControls.Panel1.Controls.Add(this.setPgnButton);
            this.splitFenAndControls.Panel1.Controls.Add(this.setFenButton);
            this.splitFenAndControls.Panel1.Controls.Add(this.flipBoardButton);
            this.splitFenAndControls.Panel1.Controls.Add(this.goToEndButton);
            this.splitFenAndControls.Panel1.Controls.Add(this.resetButton);
            this.splitFenAndControls.Panel1.Controls.Add(this.goToPrevButton);
            this.splitFenAndControls.Panel1.Controls.Add(this.goToNextButton);
            this.splitFenAndControls.Panel1.Controls.Add(this.copyFenButton);
            this.splitFenAndControls.Panel1.Controls.Add(this.fenTextBox);
            this.splitFenAndControls.Panel1.Controls.Add(this.goToStartButton);
            this.splitFenAndControls.Panel1MinSize = 52;
            // 
            // splitFenAndControls.Panel2
            // 
            this.splitFenAndControls.Panel2.Controls.Add(this.splitBoardAndMoves);
            this.splitFenAndControls.Size = new System.Drawing.Size(429, 339);
            this.splitFenAndControls.SplitterDistance = 76;
            this.splitFenAndControls.TabIndex = 7;
            // 
            // setPgnButton
            // 
            this.setPgnButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.setPgnButton.Location = new System.Drawing.Point(219, 29);
            this.setPgnButton.Name = "setPgnButton";
            this.setPgnButton.Size = new System.Drawing.Size(66, 20);
            this.setPgnButton.TabIndex = 10;
            this.setPgnButton.Text = "Set PGN";
            this.setPgnButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.setPgnButton.UseVisualStyleBackColor = true;
            this.setPgnButton.Click += new System.EventHandler(this.SetPgnButton_Click);
            // 
            // setFenButton
            // 
            this.setFenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.setFenButton.Location = new System.Drawing.Point(147, 29);
            this.setFenButton.Name = "setFenButton";
            this.setFenButton.Size = new System.Drawing.Size(66, 20);
            this.setFenButton.TabIndex = 9;
            this.setFenButton.Text = "Set FEN";
            this.setFenButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.setFenButton.UseVisualStyleBackColor = true;
            this.setFenButton.Click += new System.EventHandler(this.SetFenButton_Click);
            // 
            // flipBoardButton
            // 
            this.flipBoardButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.flipBoardButton.Location = new System.Drawing.Point(75, 53);
            this.flipBoardButton.Name = "flipBoardButton";
            this.flipBoardButton.Size = new System.Drawing.Size(66, 20);
            this.flipBoardButton.TabIndex = 8;
            this.flipBoardButton.Text = "Flip";
            this.flipBoardButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.flipBoardButton.UseVisualStyleBackColor = true;
            this.flipBoardButton.Click += new System.EventHandler(this.FlipBoardButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.resetButton.Location = new System.Drawing.Point(3, 53);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(66, 20);
            this.resetButton.TabIndex = 7;
            this.resetButton.Text = "Reset";
            this.resetButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // copyFenButton
            // 
            this.copyFenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.copyFenButton.Location = new System.Drawing.Point(3, 29);
            this.copyFenButton.Name = "copyFenButton";
            this.copyFenButton.Size = new System.Drawing.Size(66, 20);
            this.copyFenButton.TabIndex = 6;
            this.copyFenButton.Text = "Copy FEN";
            this.copyFenButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.copyFenButton.UseVisualStyleBackColor = true;
            this.copyFenButton.Click += new System.EventHandler(this.CopyFenButton_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(75, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 20);
            this.button1.TabIndex = 11;
            this.button1.Text = "Paste FEN";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // ChessBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitFenAndControls);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(1, 1);
            this.Name = "ChessBoard";
            this.Size = new System.Drawing.Size(428, 339);
            this.Load += new System.EventHandler(this.ChessBoard_Load);
            this.SizeChanged += new System.EventHandler(this.ChessBoard_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.moveHistoryGridView)).EndInit();
            this.splitBoardAndMoves.Panel1.ResumeLayout(false);
            this.splitBoardAndMoves.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitBoardAndMoves)).EndInit();
            this.splitBoardAndMoves.ResumeLayout(false);
            this.splitFenAndControls.Panel1.ResumeLayout(false);
            this.splitFenAndControls.Panel1.PerformLayout();
            this.splitFenAndControls.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitFenAndControls)).EndInit();
            this.splitFenAndControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel chessBoardPanel;
        private System.Windows.Forms.TextBox fenTextBox;
        private System.Windows.Forms.DataGridView moveHistoryGridView;
        private System.Windows.Forms.Button goToStartButton;
        private System.Windows.Forms.Button goToPrevButton;
        private System.Windows.Forms.Button goToNextButton;
        private System.Windows.Forms.Button goToEndButton;
        private System.Windows.Forms.SplitContainer splitBoardAndMoves;
        private System.Windows.Forms.SplitContainer splitFenAndControls;
        private System.Windows.Forms.Button copyFenButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button flipBoardButton;
        private System.Windows.Forms.Button setPgnButton;
        private System.Windows.Forms.Button setFenButton;
        private System.Windows.Forms.Button button1;
    }
}
