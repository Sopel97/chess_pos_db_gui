namespace chess_pos_db_gui
{
    partial class Application
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
            this.entriesGridView = new System.Windows.Forms.DataGridView();
            this.levelSelectionGroupBox = new System.Windows.Forms.GroupBox();
            this.levelServerCheckBox = new System.Windows.Forms.CheckBox();
            this.levelEngineCheckBox = new System.Windows.Forms.CheckBox();
            this.levelHumanCheckBox = new System.Windows.Forms.CheckBox();
            this.typeSelectionGroupBox = new System.Windows.Forms.GroupBox();
            this.typeTranspositionsCheckBox = new System.Windows.Forms.CheckBox();
            this.typeContinuationsCheckBox = new System.Windows.Forms.CheckBox();
            this.splitChessAndData = new System.Windows.Forms.SplitContainer();
            this.queryGroupBox = new System.Windows.Forms.GroupBox();
            this.autoQueryCheckbox = new System.Windows.Forms.CheckBox();
            this.queryButton = new System.Windows.Forms.Button();
            this.chessBoard = new chess_pos_db_gui.ChessBoard();
            ((System.ComponentModel.ISupportInitialize)(this.entriesGridView)).BeginInit();
            this.levelSelectionGroupBox.SuspendLayout();
            this.typeSelectionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitChessAndData)).BeginInit();
            this.splitChessAndData.Panel1.SuspendLayout();
            this.splitChessAndData.Panel2.SuspendLayout();
            this.splitChessAndData.SuspendLayout();
            this.queryGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // entriesGridView
            // 
            this.entriesGridView.AllowUserToAddRows = false;
            this.entriesGridView.AllowUserToDeleteRows = false;
            this.entriesGridView.AllowUserToOrderColumns = true;
            this.entriesGridView.AllowUserToResizeRows = false;
            this.entriesGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.entriesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.entriesGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.entriesGridView.Location = new System.Drawing.Point(3, 53);
            this.entriesGridView.Name = "entriesGridView";
            this.entriesGridView.ReadOnly = true;
            this.entriesGridView.Size = new System.Drawing.Size(559, 509);
            this.entriesGridView.TabIndex = 0;
            // 
            // levelSelectionGroupBox
            // 
            this.levelSelectionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.levelSelectionGroupBox.Controls.Add(this.levelServerCheckBox);
            this.levelSelectionGroupBox.Controls.Add(this.levelEngineCheckBox);
            this.levelSelectionGroupBox.Controls.Add(this.levelHumanCheckBox);
            this.levelSelectionGroupBox.Location = new System.Drawing.Point(135, 3);
            this.levelSelectionGroupBox.Name = "levelSelectionGroupBox";
            this.levelSelectionGroupBox.Size = new System.Drawing.Size(206, 44);
            this.levelSelectionGroupBox.TabIndex = 1;
            this.levelSelectionGroupBox.TabStop = false;
            this.levelSelectionGroupBox.Text = "Level";
            // 
            // levelServerCheckBox
            // 
            this.levelServerCheckBox.AutoSize = true;
            this.levelServerCheckBox.Location = new System.Drawing.Point(139, 19);
            this.levelServerCheckBox.Name = "levelServerCheckBox";
            this.levelServerCheckBox.Size = new System.Drawing.Size(57, 17);
            this.levelServerCheckBox.TabIndex = 2;
            this.levelServerCheckBox.Text = "Server";
            this.levelServerCheckBox.UseVisualStyleBackColor = true;
            this.levelServerCheckBox.CheckedChanged += new System.EventHandler(this.LevelServerCheckBox_CheckedChanged);
            // 
            // levelEngineCheckBox
            // 
            this.levelEngineCheckBox.AutoSize = true;
            this.levelEngineCheckBox.Location = new System.Drawing.Point(73, 19);
            this.levelEngineCheckBox.Name = "levelEngineCheckBox";
            this.levelEngineCheckBox.Size = new System.Drawing.Size(59, 17);
            this.levelEngineCheckBox.TabIndex = 1;
            this.levelEngineCheckBox.Text = "Engine";
            this.levelEngineCheckBox.UseVisualStyleBackColor = true;
            this.levelEngineCheckBox.CheckedChanged += new System.EventHandler(this.LevelEngineCheckBox_CheckedChanged);
            // 
            // levelHumanCheckBox
            // 
            this.levelHumanCheckBox.AutoSize = true;
            this.levelHumanCheckBox.Location = new System.Drawing.Point(6, 19);
            this.levelHumanCheckBox.Name = "levelHumanCheckBox";
            this.levelHumanCheckBox.Size = new System.Drawing.Size(60, 17);
            this.levelHumanCheckBox.TabIndex = 0;
            this.levelHumanCheckBox.Text = "Human";
            this.levelHumanCheckBox.UseVisualStyleBackColor = true;
            this.levelHumanCheckBox.CheckedChanged += new System.EventHandler(this.LevelHumanCheckBox_CheckedChanged);
            // 
            // typeSelectionGroupBox
            // 
            this.typeSelectionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.typeSelectionGroupBox.Controls.Add(this.typeTranspositionsCheckBox);
            this.typeSelectionGroupBox.Controls.Add(this.typeContinuationsCheckBox);
            this.typeSelectionGroupBox.Location = new System.Drawing.Point(347, 3);
            this.typeSelectionGroupBox.Name = "typeSelectionGroupBox";
            this.typeSelectionGroupBox.Size = new System.Drawing.Size(206, 44);
            this.typeSelectionGroupBox.TabIndex = 4;
            this.typeSelectionGroupBox.TabStop = false;
            this.typeSelectionGroupBox.Text = "Select";
            // 
            // typeTranspositionsCheckBox
            // 
            this.typeTranspositionsCheckBox.AutoSize = true;
            this.typeTranspositionsCheckBox.Location = new System.Drawing.Point(102, 19);
            this.typeTranspositionsCheckBox.Name = "typeTranspositionsCheckBox";
            this.typeTranspositionsCheckBox.Size = new System.Drawing.Size(94, 17);
            this.typeTranspositionsCheckBox.TabIndex = 1;
            this.typeTranspositionsCheckBox.Text = "Transpositions";
            this.typeTranspositionsCheckBox.UseVisualStyleBackColor = true;
            this.typeTranspositionsCheckBox.CheckedChanged += new System.EventHandler(this.TypeTranspositionsCheckBox_CheckedChanged);
            // 
            // typeContinuationsCheckBox
            // 
            this.typeContinuationsCheckBox.AutoSize = true;
            this.typeContinuationsCheckBox.Location = new System.Drawing.Point(6, 19);
            this.typeContinuationsCheckBox.Name = "typeContinuationsCheckBox";
            this.typeContinuationsCheckBox.Size = new System.Drawing.Size(90, 17);
            this.typeContinuationsCheckBox.TabIndex = 0;
            this.typeContinuationsCheckBox.Text = "Continuations";
            this.typeContinuationsCheckBox.UseVisualStyleBackColor = true;
            this.typeContinuationsCheckBox.CheckedChanged += new System.EventHandler(this.TypeContinuationsCheckBox_CheckedChanged);
            // 
            // splitChessAndData
            // 
            this.splitChessAndData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitChessAndData.Location = new System.Drawing.Point(0, 0);
            this.splitChessAndData.Name = "splitChessAndData";
            // 
            // splitChessAndData.Panel1
            // 
            this.splitChessAndData.Panel1.Controls.Add(this.chessBoard);
            // 
            // splitChessAndData.Panel2
            // 
            this.splitChessAndData.Panel2.Controls.Add(this.queryGroupBox);
            this.splitChessAndData.Panel2.Controls.Add(this.typeSelectionGroupBox);
            this.splitChessAndData.Panel2.Controls.Add(this.levelSelectionGroupBox);
            this.splitChessAndData.Panel2.Controls.Add(this.entriesGridView);
            this.splitChessAndData.Panel2MinSize = 494;
            this.splitChessAndData.Size = new System.Drawing.Size(924, 562);
            this.splitChessAndData.SplitterDistance = 355;
            this.splitChessAndData.TabIndex = 6;
            // 
            // queryGroupBox
            // 
            this.queryGroupBox.Controls.Add(this.queryButton);
            this.queryGroupBox.Controls.Add(this.autoQueryCheckbox);
            this.queryGroupBox.Location = new System.Drawing.Point(3, 3);
            this.queryGroupBox.Name = "queryGroupBox";
            this.queryGroupBox.Size = new System.Drawing.Size(126, 44);
            this.queryGroupBox.TabIndex = 3;
            this.queryGroupBox.TabStop = false;
            this.queryGroupBox.Text = "Level";
            // 
            // autoQueryCheckbox
            // 
            this.autoQueryCheckbox.AutoSize = true;
            this.autoQueryCheckbox.Location = new System.Drawing.Point(72, 19);
            this.autoQueryCheckbox.Name = "autoQueryCheckbox";
            this.autoQueryCheckbox.Size = new System.Drawing.Size(48, 17);
            this.autoQueryCheckbox.TabIndex = 0;
            this.autoQueryCheckbox.Text = "Auto";
            this.autoQueryCheckbox.UseVisualStyleBackColor = true;
            this.autoQueryCheckbox.CheckedChanged += new System.EventHandler(this.AutoQueryCheckbox_CheckedChanged);
            // 
            // queryButton
            // 
            this.queryButton.Location = new System.Drawing.Point(6, 15);
            this.queryButton.Name = "queryButton";
            this.queryButton.Size = new System.Drawing.Size(60, 23);
            this.queryButton.TabIndex = 1;
            this.queryButton.Text = "Query";
            this.queryButton.UseVisualStyleBackColor = true;
            this.queryButton.Click += new System.EventHandler(this.QueryButton_Click);
            // 
            // chessBoard
            // 
            this.chessBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chessBoard.Location = new System.Drawing.Point(0, 0);
            this.chessBoard.Margin = new System.Windows.Forms.Padding(0);
            this.chessBoard.MinimumSize = new System.Drawing.Size(1, 1);
            this.chessBoard.Name = "chessBoard";
            this.chessBoard.Size = new System.Drawing.Size(355, 562);
            this.chessBoard.TabIndex = 0;
            // 
            // Application
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 562);
            this.Controls.Add(this.splitChessAndData);
            this.MinimumSize = new System.Drawing.Size(940, 256);
            this.Name = "Application";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.entriesGridView)).EndInit();
            this.levelSelectionGroupBox.ResumeLayout(false);
            this.levelSelectionGroupBox.PerformLayout();
            this.typeSelectionGroupBox.ResumeLayout(false);
            this.typeSelectionGroupBox.PerformLayout();
            this.splitChessAndData.Panel1.ResumeLayout(false);
            this.splitChessAndData.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitChessAndData)).EndInit();
            this.splitChessAndData.ResumeLayout(false);
            this.queryGroupBox.ResumeLayout(false);
            this.queryGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView entriesGridView;
        private System.Windows.Forms.GroupBox levelSelectionGroupBox;
        private System.Windows.Forms.CheckBox levelServerCheckBox;
        private System.Windows.Forms.CheckBox levelEngineCheckBox;
        private System.Windows.Forms.CheckBox levelHumanCheckBox;
        private System.Windows.Forms.GroupBox typeSelectionGroupBox;
        private System.Windows.Forms.CheckBox typeTranspositionsCheckBox;
        private System.Windows.Forms.CheckBox typeContinuationsCheckBox;
        private System.Windows.Forms.SplitContainer splitChessAndData;
        private ChessBoard chessBoard;
        private System.Windows.Forms.GroupBox queryGroupBox;
        private System.Windows.Forms.Button queryButton;
        private System.Windows.Forms.CheckBox autoQueryCheckbox;
    }
}

