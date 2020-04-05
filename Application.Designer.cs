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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.entriesGridView = new System.Windows.Forms.DataGridView();
            this.levelSelectionGroupBox = new System.Windows.Forms.GroupBox();
            this.levelServerCheckBox = new System.Windows.Forms.CheckBox();
            this.levelEngineCheckBox = new System.Windows.Forms.CheckBox();
            this.levelHumanCheckBox = new System.Windows.Forms.CheckBox();
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
            this.firstGameInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.firstGameInfoRichTextBox = new System.Windows.Forms.RichTextBox();
            this.chessBoard = new chess_pos_db_gui.ChessBoard();
            this.goodnessGroupBox = new System.Windows.Forms.GroupBox();
            this.gamesWeightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.gamesWeightCheckbox = new System.Windows.Forms.CheckBox();
            this.humanWeightCheckbox = new System.Windows.Forms.CheckBox();
            this.engineWeightCheckbox = new System.Windows.Forms.CheckBox();
            this.evaluationWeightCheckbox = new System.Windows.Forms.CheckBox();
            this.combineHECheckbox = new System.Windows.Forms.CheckBox();
            this.goodnessNormalizeCheckbox = new System.Windows.Forms.CheckBox();
            this.goodnessUseCountCheckbox = new System.Windows.Forms.CheckBox();
            this.evalWeightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.engineWeightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.humanWeightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.displayGroupBox = new System.Windows.Forms.GroupBox();
            this.hideNeverPlayedCheckBox = new System.Windows.Forms.CheckBox();
            this.totalEntriesGridView = new System.Windows.Forms.DataGridView();
            this.queryGroupBox = new System.Windows.Forms.GroupBox();
            this.queryEvalCheckBox = new System.Windows.Forms.CheckBox();
            this.queryButton = new System.Windows.Forms.Button();
            this.autoQueryCheckbox = new System.Windows.Forms.CheckBox();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.entriesGridView)).BeginInit();
            this.levelSelectionGroupBox.SuspendLayout();
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
            this.firstGameInfoGroupBox.SuspendLayout();
            this.goodnessGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gamesWeightNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.evalWeightNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.engineWeightNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.humanWeightNumericUpDown)).BeginInit();
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.entriesGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.entriesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.entriesGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.entriesGridView.Location = new System.Drawing.Point(3, 154);
            this.entriesGridView.Name = "entriesGridView";
            this.entriesGridView.ReadOnly = true;
            this.entriesGridView.RowHeadersWidth = 20;
            this.entriesGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.entriesGridView.Size = new System.Drawing.Size(610, 404);
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
            // typeTranspositionsCheckBox
            // 
            this.typeTranspositionsCheckBox.AutoSize = true;
            this.typeTranspositionsCheckBox.Location = new System.Drawing.Point(6, 67);
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
            this.typeContinuationsCheckBox.Location = new System.Drawing.Point(6, 43);
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
            this.splitChessAndData.Panel2.Controls.Add(this.goodnessGroupBox);
            this.splitChessAndData.Panel2.Controls.Add(this.displayGroupBox);
            this.splitChessAndData.Panel2.Controls.Add(this.totalEntriesGridView);
            this.splitChessAndData.Panel2.Controls.Add(this.queryGroupBox);
            this.splitChessAndData.Panel2.Controls.Add(this.levelSelectionGroupBox);
            this.splitChessAndData.Panel2.Controls.Add(this.entriesGridView);
            this.splitChessAndData.Panel2MinSize = 580;
            this.splitChessAndData.Size = new System.Drawing.Size(1059, 562);
            this.splitChessAndData.SplitterDistance = 428;
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
            this.splitContainer1.Panel2.Controls.Add(this.firstGameInfoGroupBox);
            this.splitContainer1.Panel2.Controls.Add(this.chessBoard);
            this.splitContainer1.Size = new System.Drawing.Size(424, 558);
            this.splitContainer1.SplitterDistance = 140;
            this.splitContainer1.TabIndex = 1;
            // 
            // databaseInfoGroupBox
            // 
            this.databaseInfoGroupBox.Controls.Add(this.databaseInfoRichTextBox);
            this.databaseInfoGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.databaseInfoGroupBox.Location = new System.Drawing.Point(0, 22);
            this.databaseInfoGroupBox.Name = "databaseInfoGroupBox";
            this.databaseInfoGroupBox.Size = new System.Drawing.Size(424, 118);
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
            this.databaseInfoRichTextBox.Size = new System.Drawing.Size(418, 99);
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
            this.menuStrip1.Size = new System.Drawing.Size(424, 24);
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
            // firstGameInfoGroupBox
            // 
            this.firstGameInfoGroupBox.Controls.Add(this.firstGameInfoRichTextBox);
            this.firstGameInfoGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.firstGameInfoGroupBox.Location = new System.Drawing.Point(0, 364);
            this.firstGameInfoGroupBox.Name = "firstGameInfoGroupBox";
            this.firstGameInfoGroupBox.Size = new System.Drawing.Size(424, 50);
            this.firstGameInfoGroupBox.TabIndex = 2;
            this.firstGameInfoGroupBox.TabStop = false;
            // 
            // firstGameInfoRichTextBox
            // 
            this.firstGameInfoRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.firstGameInfoRichTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.firstGameInfoRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.firstGameInfoRichTextBox.Location = new System.Drawing.Point(3, 16);
            this.firstGameInfoRichTextBox.Name = "firstGameInfoRichTextBox";
            this.firstGameInfoRichTextBox.ReadOnly = true;
            this.firstGameInfoRichTextBox.Size = new System.Drawing.Size(418, 31);
            this.firstGameInfoRichTextBox.TabIndex = 0;
            this.firstGameInfoRichTextBox.Text = "";
            this.firstGameInfoRichTextBox.WordWrap = false;
            // 
            // chessBoard
            // 
            this.chessBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chessBoard.Location = new System.Drawing.Point(0, 0);
            this.chessBoard.Margin = new System.Windows.Forms.Padding(0);
            this.chessBoard.MinimumSize = new System.Drawing.Size(1, 1);
            this.chessBoard.Name = "chessBoard";
            this.chessBoard.Size = new System.Drawing.Size(424, 361);
            this.chessBoard.TabIndex = 0;
            // 
            // goodnessGroupBox
            // 
            this.goodnessGroupBox.Controls.Add(this.gamesWeightNumericUpDown);
            this.goodnessGroupBox.Controls.Add(this.gamesWeightCheckbox);
            this.goodnessGroupBox.Controls.Add(this.humanWeightCheckbox);
            this.goodnessGroupBox.Controls.Add(this.engineWeightCheckbox);
            this.goodnessGroupBox.Controls.Add(this.evaluationWeightCheckbox);
            this.goodnessGroupBox.Controls.Add(this.combineHECheckbox);
            this.goodnessGroupBox.Controls.Add(this.goodnessNormalizeCheckbox);
            this.goodnessGroupBox.Controls.Add(this.goodnessUseCountCheckbox);
            this.goodnessGroupBox.Controls.Add(this.evalWeightNumericUpDown);
            this.goodnessGroupBox.Controls.Add(this.engineWeightNumericUpDown);
            this.goodnessGroupBox.Controls.Add(this.humanWeightNumericUpDown);
            this.goodnessGroupBox.Location = new System.Drawing.Point(322, 3);
            this.goodnessGroupBox.Name = "goodnessGroupBox";
            this.goodnessGroupBox.Size = new System.Drawing.Size(293, 94);
            this.goodnessGroupBox.TabIndex = 6;
            this.goodnessGroupBox.TabStop = false;
            this.goodnessGroupBox.Text = "Quality Index";
            // 
            // gamesWeightNumericUpDown
            // 
            this.gamesWeightNumericUpDown.DecimalPlaces = 1;
            this.gamesWeightNumericUpDown.Location = new System.Drawing.Point(117, 18);
            this.gamesWeightNumericUpDown.Name = "gamesWeightNumericUpDown";
            this.gamesWeightNumericUpDown.Size = new System.Drawing.Size(50, 20);
            this.gamesWeightNumericUpDown.TabIndex = 12;
            this.gamesWeightNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // gamesWeightCheckbox
            // 
            this.gamesWeightCheckbox.AutoSize = true;
            this.gamesWeightCheckbox.Checked = true;
            this.gamesWeightCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.gamesWeightCheckbox.Location = new System.Drawing.Point(6, 20);
            this.gamesWeightCheckbox.Name = "gamesWeightCheckbox";
            this.gamesWeightCheckbox.Size = new System.Drawing.Size(93, 17);
            this.gamesWeightCheckbox.TabIndex = 11;
            this.gamesWeightCheckbox.Text = "Games weight";
            this.tooltip.SetToolTip(this.gamesWeightCheckbox, "When enabled the quality index calculation will include all games");
            this.gamesWeightCheckbox.UseVisualStyleBackColor = true;
            this.gamesWeightCheckbox.CheckedChanged += new System.EventHandler(this.GamesWeightCheckbox_CheckedChanged);
            // 
            // humanWeightCheckbox
            // 
            this.humanWeightCheckbox.AutoSize = true;
            this.humanWeightCheckbox.Checked = true;
            this.humanWeightCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.humanWeightCheckbox.Location = new System.Drawing.Point(6, 19);
            this.humanWeightCheckbox.Name = "humanWeightCheckbox";
            this.humanWeightCheckbox.Size = new System.Drawing.Size(94, 17);
            this.humanWeightCheckbox.TabIndex = 10;
            this.humanWeightCheckbox.Text = "Human weight";
            this.tooltip.SetToolTip(this.humanWeightCheckbox, "When enabled the quality index calculation will include human games");
            this.humanWeightCheckbox.UseVisualStyleBackColor = true;
            this.humanWeightCheckbox.CheckedChanged += new System.EventHandler(this.humanWeightCheckbox_CheckedChanged);
            // 
            // engineWeightCheckbox
            // 
            this.engineWeightCheckbox.AutoSize = true;
            this.engineWeightCheckbox.Checked = true;
            this.engineWeightCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.engineWeightCheckbox.Location = new System.Drawing.Point(6, 43);
            this.engineWeightCheckbox.Name = "engineWeightCheckbox";
            this.engineWeightCheckbox.Size = new System.Drawing.Size(93, 17);
            this.engineWeightCheckbox.TabIndex = 9;
            this.engineWeightCheckbox.Text = "Engine weight";
            this.tooltip.SetToolTip(this.engineWeightCheckbox, "When enabled the quality index calculation will include engine games");
            this.engineWeightCheckbox.UseVisualStyleBackColor = true;
            this.engineWeightCheckbox.CheckedChanged += new System.EventHandler(this.EngineWeightCheckbox_CheckedChanged);
            // 
            // evaluationWeightCheckbox
            // 
            this.evaluationWeightCheckbox.AutoSize = true;
            this.evaluationWeightCheckbox.Checked = true;
            this.evaluationWeightCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.evaluationWeightCheckbox.Location = new System.Drawing.Point(6, 67);
            this.evaluationWeightCheckbox.Name = "evaluationWeightCheckbox";
            this.evaluationWeightCheckbox.Size = new System.Drawing.Size(110, 17);
            this.evaluationWeightCheckbox.TabIndex = 8;
            this.evaluationWeightCheckbox.Text = "Evaluation weight";
            this.tooltip.SetToolTip(this.evaluationWeightCheckbox, "When enabled the quality index calculation will include engine evaluation");
            this.evaluationWeightCheckbox.UseVisualStyleBackColor = true;
            this.evaluationWeightCheckbox.CheckedChanged += new System.EventHandler(this.EvaluationWeightCheckbox_CheckedChanged);
            // 
            // combineHECheckbox
            // 
            this.combineHECheckbox.AutoSize = true;
            this.combineHECheckbox.Location = new System.Drawing.Point(187, 21);
            this.combineHECheckbox.Name = "combineHECheckbox";
            this.combineHECheckbox.Size = new System.Drawing.Size(101, 17);
            this.combineHECheckbox.TabIndex = 7;
            this.combineHECheckbox.Text = "Combine games";
            this.tooltip.SetToolTip(this.combineHECheckbox, "When enabled all games will be included as one factor for the quality index calcu" +
        "lation");
            this.combineHECheckbox.UseVisualStyleBackColor = true;
            this.combineHECheckbox.CheckedChanged += new System.EventHandler(this.CombineHECheckbox_CheckedChanged);
            // 
            // goodnessNormalizeCheckbox
            // 
            this.goodnessNormalizeCheckbox.AutoSize = true;
            this.goodnessNormalizeCheckbox.Checked = true;
            this.goodnessNormalizeCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.goodnessNormalizeCheckbox.Location = new System.Drawing.Point(187, 68);
            this.goodnessNormalizeCheckbox.Name = "goodnessNormalizeCheckbox";
            this.goodnessNormalizeCheckbox.Size = new System.Drawing.Size(72, 17);
            this.goodnessNormalizeCheckbox.TabIndex = 6;
            this.goodnessNormalizeCheckbox.Text = "Normalize";
            this.tooltip.SetToolTip(this.goodnessNormalizeCheckbox, "When enabled the quality index will be normalized to be in range 0..100");
            this.goodnessNormalizeCheckbox.UseVisualStyleBackColor = true;
            this.goodnessNormalizeCheckbox.CheckedChanged += new System.EventHandler(this.GoodnessNormalizeCheckbox_CheckedChanged);
            // 
            // goodnessUseCountCheckbox
            // 
            this.goodnessUseCountCheckbox.AutoSize = true;
            this.goodnessUseCountCheckbox.Checked = true;
            this.goodnessUseCountCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.goodnessUseCountCheckbox.Location = new System.Drawing.Point(187, 45);
            this.goodnessUseCountCheckbox.Name = "goodnessUseCountCheckbox";
            this.goodnessUseCountCheckbox.Size = new System.Drawing.Size(75, 17);
            this.goodnessUseCountCheckbox.TabIndex = 2;
            this.goodnessUseCountCheckbox.Text = "Use count";
            this.tooltip.SetToolTip(this.goodnessUseCountCheckbox, "When enabled the number of postion instances will affect the quality index. The l" +
        "ess more positions the more confidence - more confidence means better quality in" +
        "dex.");
            this.goodnessUseCountCheckbox.UseVisualStyleBackColor = true;
            this.goodnessUseCountCheckbox.CheckedChanged += new System.EventHandler(this.GoodnessUseCountCheckbox_CheckedChanged);
            // 
            // evalWeightNumericUpDown
            // 
            this.evalWeightNumericUpDown.DecimalPlaces = 1;
            this.evalWeightNumericUpDown.Location = new System.Drawing.Point(117, 66);
            this.evalWeightNumericUpDown.Name = "evalWeightNumericUpDown";
            this.evalWeightNumericUpDown.Size = new System.Drawing.Size(50, 20);
            this.evalWeightNumericUpDown.TabIndex = 4;
            this.evalWeightNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.evalWeightNumericUpDown.ValueChanged += new System.EventHandler(this.EvalWeightNumericUpDown_ValueChanged);
            // 
            // engineWeightNumericUpDown
            // 
            this.engineWeightNumericUpDown.DecimalPlaces = 1;
            this.engineWeightNumericUpDown.Location = new System.Drawing.Point(117, 42);
            this.engineWeightNumericUpDown.Name = "engineWeightNumericUpDown";
            this.engineWeightNumericUpDown.Size = new System.Drawing.Size(50, 20);
            this.engineWeightNumericUpDown.TabIndex = 2;
            this.engineWeightNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.engineWeightNumericUpDown.ValueChanged += new System.EventHandler(this.EngineWeightNumericUpDown_ValueChanged);
            // 
            // humanWeightNumericUpDown
            // 
            this.humanWeightNumericUpDown.DecimalPlaces = 1;
            this.humanWeightNumericUpDown.Location = new System.Drawing.Point(117, 18);
            this.humanWeightNumericUpDown.Name = "humanWeightNumericUpDown";
            this.humanWeightNumericUpDown.Size = new System.Drawing.Size(50, 20);
            this.humanWeightNumericUpDown.TabIndex = 0;
            this.humanWeightNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.humanWeightNumericUpDown.ValueChanged += new System.EventHandler(this.HumanWeightNumericUpDown_ValueChanged);
            // 
            // displayGroupBox
            // 
            this.displayGroupBox.Controls.Add(this.typeTranspositionsCheckBox);
            this.displayGroupBox.Controls.Add(this.hideNeverPlayedCheckBox);
            this.displayGroupBox.Controls.Add(this.typeContinuationsCheckBox);
            this.displayGroupBox.Location = new System.Drawing.Point(215, 3);
            this.displayGroupBox.Name = "displayGroupBox";
            this.displayGroupBox.Size = new System.Drawing.Size(101, 94);
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
            this.totalEntriesGridView.Size = new System.Drawing.Size(610, 45);
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
            // Application
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 562);
            this.Controls.Add(this.splitChessAndData);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1075, 480);
            this.Name = "Application";
            this.Text = "chess_pos_db_gui";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Application_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.entriesGridView)).EndInit();
            this.levelSelectionGroupBox.ResumeLayout(false);
            this.levelSelectionGroupBox.PerformLayout();
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
            this.firstGameInfoGroupBox.ResumeLayout(false);
            this.goodnessGroupBox.ResumeLayout(false);
            this.goodnessGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gamesWeightNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.evalWeightNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.engineWeightNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.humanWeightNumericUpDown)).EndInit();
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
        private System.Windows.Forms.GroupBox firstGameInfoGroupBox;
        private System.Windows.Forms.RichTextBox firstGameInfoRichTextBox;
        private System.Windows.Forms.GroupBox goodnessGroupBox;
        private System.Windows.Forms.NumericUpDown humanWeightNumericUpDown;
        private System.Windows.Forms.NumericUpDown evalWeightNumericUpDown;
        private System.Windows.Forms.NumericUpDown engineWeightNumericUpDown;
        private System.Windows.Forms.CheckBox goodnessNormalizeCheckbox;
        private System.Windows.Forms.CheckBox goodnessUseCountCheckbox;
        private System.Windows.Forms.CheckBox humanWeightCheckbox;
        private System.Windows.Forms.CheckBox engineWeightCheckbox;
        private System.Windows.Forms.CheckBox evaluationWeightCheckbox;
        private System.Windows.Forms.CheckBox combineHECheckbox;
        private System.Windows.Forms.NumericUpDown gamesWeightNumericUpDown;
        private System.Windows.Forms.CheckBox gamesWeightCheckbox;
    }
}

