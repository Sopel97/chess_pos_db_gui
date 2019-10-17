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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.moveHistoryGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            this.chessBoardPanel.SizeChanged += new System.EventHandler(this.ChessBoardPanel_SizeChanged);
            this.chessBoardPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ChessBoardPanel_Paint);
            this.chessBoardPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChessBoardPanel_MouseDown);
            this.chessBoardPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChessBoardPanel_MouseUp);
            // 
            // fenTextBox
            // 
            this.fenTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fenTextBox.Location = new System.Drawing.Point(3, 3);
            this.fenTextBox.Name = "fenTextBox";
            this.fenTextBox.Size = new System.Drawing.Size(421, 20);
            this.fenTextBox.TabIndex = 5;
            this.fenTextBox.TextChanged += new System.EventHandler(this.FenTextBox_TextChanged);
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
            this.moveHistoryGridView.Location = new System.Drawing.Point(4, 41);
            this.moveHistoryGridView.MinimumSize = new System.Drawing.Size(146, 0);
            this.moveHistoryGridView.MultiSelect = false;
            this.moveHistoryGridView.Name = "moveHistoryGridView";
            this.moveHistoryGridView.ReadOnly = true;
            this.moveHistoryGridView.RowHeadersVisible = false;
            this.moveHistoryGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.moveHistoryGridView.Size = new System.Drawing.Size(146, 266);
            this.moveHistoryGridView.TabIndex = 0;
            this.moveHistoryGridView.SelectionChanged += new System.EventHandler(this.MoveHistoryGridView_SelectionChanged);
            // 
            // goToStartButton
            // 
            this.goToStartButton.Location = new System.Drawing.Point(3, 3);
            this.goToStartButton.Name = "goToStartButton";
            this.goToStartButton.Size = new System.Drawing.Size(32, 32);
            this.goToStartButton.TabIndex = 1;
            this.goToStartButton.Text = "<<";
            this.goToStartButton.UseVisualStyleBackColor = true;
            this.goToStartButton.Click += new System.EventHandler(this.GoToStartButton_Click);
            // 
            // goToPrevButton
            // 
            this.goToPrevButton.Location = new System.Drawing.Point(41, 3);
            this.goToPrevButton.Name = "goToPrevButton";
            this.goToPrevButton.Size = new System.Drawing.Size(32, 32);
            this.goToPrevButton.TabIndex = 2;
            this.goToPrevButton.Text = "<";
            this.goToPrevButton.UseVisualStyleBackColor = true;
            this.goToPrevButton.Click += new System.EventHandler(this.GoToPrevButton_Click);
            // 
            // goToNextButton
            // 
            this.goToNextButton.Location = new System.Drawing.Point(79, 3);
            this.goToNextButton.Name = "goToNextButton";
            this.goToNextButton.Size = new System.Drawing.Size(32, 32);
            this.goToNextButton.TabIndex = 3;
            this.goToNextButton.Text = ">";
            this.goToNextButton.UseVisualStyleBackColor = true;
            this.goToNextButton.Click += new System.EventHandler(this.GoToNextButton_Click);
            // 
            // goToEndButton
            // 
            this.goToEndButton.Location = new System.Drawing.Point(117, 3);
            this.goToEndButton.Name = "goToEndButton";
            this.goToEndButton.Size = new System.Drawing.Size(32, 32);
            this.goToEndButton.TabIndex = 4;
            this.goToEndButton.Text = ">>";
            this.goToEndButton.UseVisualStyleBackColor = true;
            this.goToEndButton.Click += new System.EventHandler(this.GoToEndButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chessBoardPanel);
            this.splitContainer1.Panel1.SizeChanged += new System.EventHandler(this.SplitContainer1_Panel1_SizeChanged);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.goToEndButton);
            this.splitContainer1.Panel2.Controls.Add(this.goToPrevButton);
            this.splitContainer1.Panel2.Controls.Add(this.goToNextButton);
            this.splitContainer1.Panel2.Controls.Add(this.moveHistoryGridView);
            this.splitContainer1.Panel2.Controls.Add(this.goToStartButton);
            this.splitContainer1.Size = new System.Drawing.Size(429, 310);
            this.splitContainer1.SplitterDistance = 271;
            this.splitContainer1.TabIndex = 6;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.fenTextBox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Size = new System.Drawing.Size(429, 339);
            this.splitContainer2.SplitterDistance = 25;
            this.splitContainer2.TabIndex = 7;
            // 
            // ChessBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(1, 1);
            this.Name = "ChessBoard";
            this.Size = new System.Drawing.Size(428, 339);
            this.Load += new System.EventHandler(this.ChessBoard_Load);
            this.SizeChanged += new System.EventHandler(this.ChessBoard_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.moveHistoryGridView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
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
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
    }
}
