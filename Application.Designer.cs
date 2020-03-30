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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.entriesGridView = new System.Windows.Forms.DataGridView();
            this.levelSelectionGroupBox = new System.Windows.Forms.GroupBox();
            this.levelServerCheckBox = new System.Windows.Forms.CheckBox();
            this.levelEngineCheckBox = new System.Windows.Forms.CheckBox();
            this.levelHumanCheckBox = new System.Windows.Forms.CheckBox();
            this.typeSelectionGroupBox = new System.Windows.Forms.GroupBox();
            this.typeTranspositionsCheckBox = new System.Windows.Forms.CheckBox();
            this.typeContinuationsCheckBox = new System.Windows.Forms.CheckBox();
            this.splitChessAndData = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.databaseInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.databaseInfoRichTextBox = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.epdDumpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayGroupBox = new System.Windows.Forms.GroupBox();
            this.hideNeverPlayedCheckBox = new System.Windows.Forms.CheckBox();
            this.totalEntriesGridView = new System.Windows.Forms.DataGridView();
            this.queryGroupBox = new System.Windows.Forms.GroupBox();
            this.queryEvalCheckBox = new System.Windows.Forms.CheckBox();
            this.queryButton = new System.Windows.Forms.Button();
            this.autoQueryCheckbox = new System.Windows.Forms.CheckBox();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.chessBoard = new chess_pos_db_gui.ChessBoard();
            ((System.ComponentModel.ISupportInitialize)(this.entriesGridView)).BeginInit();
            this.levelSelectionGroupBox.SuspendLayout();
            this.typeSelectionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitChessAndData)).BeginInit();
            this.splitChessAndData.Panel1.SuspendLayout();
            this.splitChessAndData.Panel2.SuspendLayout();
            this.splitChessAndData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.databaseInfoGroupBox.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.displayGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.totalEntriesGridView)).BeginInit();
            this.queryGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // entriesGridView
            // 
            this.entriesGridView.AllowUserToAddRows = false;
            this.entriesGridView.AllowUserToDeleteRows = false;
            this.entriesGridView.AllowUserToResizeRows = false;
            this.entriesGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.entriesGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.entriesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.entriesGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.entriesGridView.Location = new System.Drawing.Point(3, 154);
            this.entriesGridView.Name = "entriesGridView";
            this.entriesGridView.ReadOnly = true;
            this.entriesGridView.RowHeadersWidth = 20;
            this.entriesGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.entriesGridView.Size = new System.Drawing.Size(504, 404);
            this.entriesGridView.TabIndex = 0;
            this.entriesGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.EntriesGridView_CellContentDoubleClick);
            this.entriesGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.EntriesGridView_CellFormatting);
            this.entriesGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.EntriesGridView_RowPrePaint);
            // 
            // levelSelectionGroupBox
            // 
            this.levelSelectionGroupBox.Controls.Add(this.levelServerCheckBox);
            this.levelSelectionGroupBox.Controls.Add(this.levelEngineCheckBox);
            this.levelSelectionGroupBox.Controls.Add(this.levelHumanCheckBox);
            this.levelSelectionGroupBox.Location = new System.Drawing.Point(3, 53);
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
            this.tooltip.SetToolTip(this.levelServerCheckBox, "When enabled the statistics will include data from games from Server category.");
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
            this.tooltip.SetToolTip(this.levelEngineCheckBox, "When enabled the statistics will include data from games from Engine category.");
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
            this.tooltip.SetToolTip(this.levelHumanCheckBox, "When enabled the statistics will include data from games from Human category.");
            this.levelHumanCheckBox.UseVisualStyleBackColor = true;
            this.levelHumanCheckBox.CheckedChanged += new System.EventHandler(this.LevelHumanCheckBox_CheckedChanged);
            // 
            // typeSelectionGroupBox
            // 
            this.typeSelectionGroupBox.Controls.Add(this.typeTranspositionsCheckBox);
            this.typeSelectionGroupBox.Controls.Add(this.typeContinuationsCheckBox);
            this.typeSelectionGroupBox.Location = new System.Drawing.Point(215, 53);
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
            this.tooltip.SetToolTip(this.typeTranspositionsCheckBox, "When enabled the statistics will include data from positions that arise by transp" +
        "osition (ie. the exact move was not played, but the position after it has happen" +
        "ed before).");
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
            this.tooltip.SetToolTip(this.typeContinuationsCheckBox, "When enabled the statistics will include data from positions that arise by perfor" +
        "ming the exact move (ie. not transpositions).");
            this.typeContinuationsCheckBox.UseVisualStyleBackColor = true;
            this.typeContinuationsCheckBox.CheckedChanged += new System.EventHandler(this.TypeContinuationsCheckBox_CheckedChanged);
            // 
            // splitChessAndData
            // 
            this.splitChessAndData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitChessAndData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitChessAndData.Location = new System.Drawing.Point(0, 0);
            this.splitChessAndData.Name = "splitChessAndData";
            // 
            // splitChessAndData.Panel1
            // 
            this.splitChessAndData.Panel1.Controls.Add(this.splitContainer1);
            this.splitChessAndData.Panel1MinSize = 400;
            // 
            // splitChessAndData.Panel2
            // 
            this.splitChessAndData.Panel2.Controls.Add(this.displayGroupBox);
            this.splitChessAndData.Panel2.Controls.Add(this.totalEntriesGridView);
            this.splitChessAndData.Panel2.Controls.Add(this.queryGroupBox);
            this.splitChessAndData.Panel2.Controls.Add(this.typeSelectionGroupBox);
            this.splitChessAndData.Panel2.Controls.Add(this.levelSelectionGroupBox);
            this.splitChessAndData.Panel2.Controls.Add(this.entriesGridView);
            this.splitChessAndData.Panel2MinSize = 420;
            this.splitChessAndData.Size = new System.Drawing.Size(924, 562);
            this.splitChessAndData.SplitterDistance = 400;
            this.splitChessAndData.SplitterWidth = 5;
            this.splitChessAndData.TabIndex = 6;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.databaseInfoGroupBox);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chessBoard);
            this.splitContainer1.Size = new System.Drawing.Size(396, 558);
            this.splitContainer1.SplitterDistance = 140;
            this.splitContainer1.TabIndex = 1;
            // 
            // databaseInfoGroupBox
            // 
            this.databaseInfoGroupBox.Controls.Add(this.databaseInfoRichTextBox);
            this.databaseInfoGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.databaseInfoGroupBox.Location = new System.Drawing.Point(0, 22);
            this.databaseInfoGroupBox.Name = "databaseInfoGroupBox";
            this.databaseInfoGroupBox.Size = new System.Drawing.Size(396, 118);
            this.databaseInfoGroupBox.TabIndex = 0;
            this.databaseInfoGroupBox.TabStop = false;
            // 
            // databaseInfoRichTextBox
            // 
            this.databaseInfoRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.databaseInfoRichTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.databaseInfoRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.databaseInfoRichTextBox.Location = new System.Drawing.Point(3, 16);
            this.databaseInfoRichTextBox.Name = "databaseInfoRichTextBox";
            this.databaseInfoRichTextBox.ReadOnly = true;
            this.databaseInfoRichTextBox.Size = new System.Drawing.Size(390, 99);
            this.databaseInfoRichTextBox.TabIndex = 0;
            this.databaseInfoRichTextBox.Text = "";
            this.databaseInfoRichTextBox.WordWrap = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.databaseToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(396, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "Menu";
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripMenuItem,
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.databaseToolStripMenuItem.Text = "Database";
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.createToolStripMenuItem.Text = "Create";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.CreateToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.epdDumpToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // epdDumpToolStripMenuItem
            // 
            this.epdDumpToolStripMenuItem.Name = "epdDumpToolStripMenuItem";
            this.epdDumpToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.epdDumpToolStripMenuItem.Text = "Epd dump";
            this.epdDumpToolStripMenuItem.Click += new System.EventHandler(this.EpdDumpToolStripMenuItem_Click);
            // 
            // displayGroupBox
            // 
            this.displayGroupBox.Controls.Add(this.hideNeverPlayedCheckBox);
            this.displayGroupBox.Location = new System.Drawing.Point(215, 3);
            this.displayGroupBox.Name = "displayGroupBox";
            this.displayGroupBox.Size = new System.Drawing.Size(206, 44);
            this.displayGroupBox.TabIndex = 5;
            this.displayGroupBox.TabStop = false;
            this.displayGroupBox.Text = "Display";
            // 
            // hideNeverPlayedCheckBox
            // 
            this.hideNeverPlayedCheckBox.AutoSize = true;
            this.hideNeverPlayedCheckBox.Location = new System.Drawing.Point(6, 19);
            this.hideNeverPlayedCheckBox.Name = "hideNeverPlayedCheckBox";
            this.hideNeverPlayedCheckBox.Size = new System.Drawing.Size(71, 17);
            this.hideNeverPlayedCheckBox.TabIndex = 0;
            this.hideNeverPlayedCheckBox.Text = "Hide N=0";
            this.tooltip.SetToolTip(this.hideNeverPlayedCheckBox, "When enabled moves with no position instances will not be shown in the table.");
            this.hideNeverPlayedCheckBox.UseVisualStyleBackColor = true;
            this.hideNeverPlayedCheckBox.CheckedChanged += new System.EventHandler(this.HideNeverPlayedCheckBox_CheckedChanged);
            // 
            // totalEntriesGridView
            // 
            this.totalEntriesGridView.AllowUserToAddRows = false;
            this.totalEntriesGridView.AllowUserToDeleteRows = false;
            this.totalEntriesGridView.AllowUserToResizeColumns = false;
            this.totalEntriesGridView.AllowUserToResizeRows = false;
            this.totalEntriesGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.totalEntriesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.totalEntriesGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.totalEntriesGridView.Location = new System.Drawing.Point(3, 103);
            this.totalEntriesGridView.Name = "totalEntriesGridView";
            this.totalEntriesGridView.ReadOnly = true;
            this.totalEntriesGridView.RowHeadersWidth = 20;
            this.totalEntriesGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.totalEntriesGridView.Size = new System.Drawing.Size(504, 45);
            this.totalEntriesGridView.TabIndex = 5;
            this.totalEntriesGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.TotalEntriesGridView_CellFormatting);
            this.totalEntriesGridView.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.TotalEntriesGridView_ColumnWidthChanged);
            // 
            // queryGroupBox
            // 
            this.queryGroupBox.Controls.Add(this.queryEvalCheckBox);
            this.queryGroupBox.Controls.Add(this.queryButton);
            this.queryGroupBox.Controls.Add(this.autoQueryCheckbox);
            this.queryGroupBox.Location = new System.Drawing.Point(3, 3);
            this.queryGroupBox.Name = "queryGroupBox";
            this.queryGroupBox.Size = new System.Drawing.Size(206, 44);
            this.queryGroupBox.TabIndex = 3;
            this.queryGroupBox.TabStop = false;
            this.queryGroupBox.Text = "Query";
            // 
            // queryEvalCheckBox
            // 
            this.queryEvalCheckBox.AutoSize = true;
            this.queryEvalCheckBox.Checked = true;
            this.queryEvalCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.queryEvalCheckBox.Location = new System.Drawing.Point(139, 19);
            this.queryEvalCheckBox.Name = "queryEvalCheckBox";
            this.queryEvalCheckBox.Size = new System.Drawing.Size(47, 17);
            this.queryEvalCheckBox.TabIndex = 2;
            this.queryEvalCheckBox.Text = "Eval";
            this.tooltip.SetToolTip(this.queryEvalCheckBox, "When enabled it allows querying evaluation data from chessdb.cn. This can reduce " +
        "responsivness due to network latency.");
            this.queryEvalCheckBox.UseVisualStyleBackColor = true;
            // 
            // queryButton
            // 
            this.queryButton.Location = new System.Drawing.Point(6, 15);
            this.queryButton.Name = "queryButton";
            this.queryButton.Size = new System.Drawing.Size(60, 23);
            this.queryButton.TabIndex = 1;
            this.queryButton.Text = "Query";
            this.tooltip.SetToolTip(this.queryButton, "Manually queries the statistics for the current position.");
            this.queryButton.UseVisualStyleBackColor = true;
            this.queryButton.Click += new System.EventHandler(this.QueryButton_Click);
            // 
            // autoQueryCheckbox
            // 
            this.autoQueryCheckbox.AutoSize = true;
            this.autoQueryCheckbox.Checked = true;
            this.autoQueryCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoQueryCheckbox.Location = new System.Drawing.Point(73, 19);
            this.autoQueryCheckbox.Name = "autoQueryCheckbox";
            this.autoQueryCheckbox.Size = new System.Drawing.Size(48, 17);
            this.autoQueryCheckbox.TabIndex = 0;
            this.autoQueryCheckbox.Text = "Auto";
            this.tooltip.SetToolTip(this.autoQueryCheckbox, "If enabled it will automatically perform an evaluation query the statistics for e" +
        "ach position.");
            this.autoQueryCheckbox.UseVisualStyleBackColor = true;
            this.autoQueryCheckbox.CheckedChanged += new System.EventHandler(this.AutoQueryCheckbox_CheckedChanged);
            // 
            // chessBoard
            // 
            this.chessBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chessBoard.Location = new System.Drawing.Point(0, 0);
            this.chessBoard.Margin = new System.Windows.Forms.Padding(0);
            this.chessBoard.MinimumSize = new System.Drawing.Size(1, 1);
            this.chessBoard.Name = "chessBoard";
            this.chessBoard.Size = new System.Drawing.Size(396, 414);
            this.chessBoard.TabIndex = 0;
            // 
            // Application
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 562);
            this.Controls.Add(this.splitChessAndData);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(850, 480);
            this.Name = "Application";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Application_FormClosing);
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
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.databaseInfoGroupBox.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.displayGroupBox.ResumeLayout(false);
            this.displayGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.totalEntriesGridView)).EndInit();
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
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox databaseInfoGroupBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.RichTextBox databaseInfoRichTextBox;
        private System.Windows.Forms.DataGridView totalEntriesGridView;
        private System.Windows.Forms.CheckBox queryEvalCheckBox;
        private System.Windows.Forms.GroupBox displayGroupBox;
        private System.Windows.Forms.CheckBox hideNeverPlayedCheckBox;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem epdDumpToolStripMenuItem;
        private System.Windows.Forms.ToolTip tooltip;
    }
}

