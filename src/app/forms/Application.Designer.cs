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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.appendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.profilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.epdDumpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseFormatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analysisAndBoardSplitContainer = new System.Windows.Forms.SplitContainer();
            this.firstGameInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.fenRichTextBox = new System.Windows.Forms.RichTextBox();
            this.firstGameInfoRichTextBox = new System.Windows.Forms.RichTextBox();
            this.entriesRetractionsSplitPanel = new System.Windows.Forms.SplitContainer();
            this.dataHelpButton = new System.Windows.Forms.Button();
            this.retractionsHelpButton = new System.Windows.Forms.Button();
            this.retractionsGridView = new System.Windows.Forms.DataGridView();
            this.totalDataHelpButton = new System.Windows.Forms.Button();
            this.goodnessGroupBox = new System.Windows.Forms.GroupBox();
            this.goodnessFormulaComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gamesEvalWeightTrackBar = new System.Windows.Forms.TrackBar();
            this.lowNThresholdNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lowNThesholdCheckBox = new System.Windows.Forms.CheckBox();
            this.drawScoreLabel = new System.Windows.Forms.Label();
            this.drawScoreNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.goodnessNormalizeCheckbox = new System.Windows.Forms.CheckBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.analysisAndBoardSplitContainer)).BeginInit();
            this.analysisAndBoardSplitContainer.Panel2.SuspendLayout();
            this.analysisAndBoardSplitContainer.SuspendLayout();
            this.firstGameInfoGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entriesRetractionsSplitPanel)).BeginInit();
            this.entriesRetractionsSplitPanel.Panel1.SuspendLayout();
            this.entriesRetractionsSplitPanel.Panel2.SuspendLayout();
            this.entriesRetractionsSplitPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.retractionsGridView)).BeginInit();
            this.goodnessGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gamesEvalWeightTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowNThresholdNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawScoreNumericUpDown)).BeginInit();
            this.displayGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.totalEntriesGridView)).BeginInit();
            this.queryGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // entriesGridView
            // 
            this.entriesGridView.AllowUserToAddRows = false;
            this.entriesGridView.AllowUserToDeleteRows = false;
            this.entriesGridView.AllowUserToResizeColumns = false;
            this.entriesGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.entriesGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.entriesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.entriesGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entriesGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.entriesGridView.Location = new System.Drawing.Point(0, 0);
            this.entriesGridView.Name = "entriesGridView";
            this.entriesGridView.ReadOnly = true;
            this.entriesGridView.RowHeadersWidth = 20;
            this.entriesGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.entriesGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.entriesGridView.Size = new System.Drawing.Size(601, 270);
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
            this.levelSelectionGroupBox.Size = new System.Drawing.Size(206, 43);
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
        "osition (i.e. the exact move was not played, but the position after it has happe" +
        "ned before).");
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
        "ming the exact move (i.e. not transpositions).");
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
            this.splitChessAndData.Panel2.Controls.Add(this.entriesRetractionsSplitPanel);
            this.splitChessAndData.Panel2.Controls.Add(this.totalDataHelpButton);
            this.splitChessAndData.Panel2.Controls.Add(this.goodnessGroupBox);
            this.splitChessAndData.Panel2.Controls.Add(this.displayGroupBox);
            this.splitChessAndData.Panel2.Controls.Add(this.totalEntriesGridView);
            this.splitChessAndData.Panel2.Controls.Add(this.queryGroupBox);
            this.splitChessAndData.Panel2.Controls.Add(this.levelSelectionGroupBox);
            this.splitChessAndData.Panel2MinSize = 620;
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
            this.splitContainer1.Panel2.Controls.Add(this.analysisAndBoardSplitContainer);
            this.splitContainer1.Panel2.Controls.Add(this.firstGameInfoGroupBox);
            this.splitContainer1.Size = new System.Drawing.Size(424, 558);
            this.splitContainer1.SplitterDistance = 110;
            this.splitContainer1.TabIndex = 1;
            // 
            // databaseInfoGroupBox
            // 
            this.databaseInfoGroupBox.Controls.Add(this.databaseInfoRichTextBox);
            this.databaseInfoGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.databaseInfoGroupBox.Location = new System.Drawing.Point(0, 24);
            this.databaseInfoGroupBox.Name = "databaseInfoGroupBox";
            this.databaseInfoGroupBox.Size = new System.Drawing.Size(424, 86);
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
            this.databaseInfoRichTextBox.Size = new System.Drawing.Size(418, 67);
            this.databaseInfoRichTextBox.TabIndex = 0;
            this.databaseInfoRichTextBox.Text = "";
            this.databaseInfoRichTextBox.WordWrap = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.databaseToolStripMenuItem,
            this.analysisToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.themesToolStripMenuItem,
            this.helpToolStripMenuItem});
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
            this.toolStripSeparator2,
            this.appendToolStripMenuItem,
            this.mergeToolStripMenuItem,
            this.toolStripSeparator1,
            this.closeToolStripMenuItem});
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.databaseToolStripMenuItem.Text = "Database";
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.createToolStripMenuItem.Text = "Create";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.CreateToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(113, 6);
            // 
            // appendToolStripMenuItem
            // 
            this.appendToolStripMenuItem.Name = "appendToolStripMenuItem";
            this.appendToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.appendToolStripMenuItem.Text = "Append";
            this.appendToolStripMenuItem.Click += new System.EventHandler(this.appendToolStripMenuItem_Click);
            // 
            // mergeToolStripMenuItem
            // 
            this.mergeToolStripMenuItem.Name = "mergeToolStripMenuItem";
            this.mergeToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.mergeToolStripMenuItem.Text = "Merge";
            this.mergeToolStripMenuItem.Click += new System.EventHandler(this.MergeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(113, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // analysisToolStripMenuItem
            // 
            this.analysisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setupToolStripMenuItem,
            this.profilesToolStripMenuItem});
            this.analysisToolStripMenuItem.Name = "analysisToolStripMenuItem";
            this.analysisToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.analysisToolStripMenuItem.Text = "Engine";
            // 
            // setupToolStripMenuItem
            // 
            this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
            this.setupToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.setupToolStripMenuItem.Text = "Analysis";
            this.setupToolStripMenuItem.Click += new System.EventHandler(this.SetupToolStripMenuItem_Click);
            // 
            // profilesToolStripMenuItem
            // 
            this.profilesToolStripMenuItem.Name = "profilesToolStripMenuItem";
            this.profilesToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.profilesToolStripMenuItem.Text = "Profiles";
            this.profilesToolStripMenuItem.Click += new System.EventHandler(this.ProfilesToolStripMenuItem_Click);
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
            // themesToolStripMenuItem
            // 
            this.themesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.themesToolStripMenuItem1});
            this.themesToolStripMenuItem.Name = "themesToolStripMenuItem";
            this.themesToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.themesToolStripMenuItem.Text = "Appearance";
            // 
            // themesToolStripMenuItem1
            // 
            this.themesToolStripMenuItem1.Name = "themesToolStripMenuItem1";
            this.themesToolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
            this.themesToolStripMenuItem1.Text = "Themes";
            this.themesToolStripMenuItem1.Click += new System.EventHandler(this.ThemesToolStripMenuItem1_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.databaseFormatsToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // databaseFormatsToolStripMenuItem
            // 
            this.databaseFormatsToolStripMenuItem.Name = "databaseFormatsToolStripMenuItem";
            this.databaseFormatsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.databaseFormatsToolStripMenuItem.Text = "Database formats";
            this.databaseFormatsToolStripMenuItem.Click += new System.EventHandler(this.databaseFormatsToolStripMenuItem_Click);
            // 
            // analysisAndBoardSplitContainer
            // 
            this.analysisAndBoardSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.analysisAndBoardSplitContainer.IsSplitterFixed = true;
            this.analysisAndBoardSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.analysisAndBoardSplitContainer.Name = "analysisAndBoardSplitContainer";
            this.analysisAndBoardSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.analysisAndBoardSplitContainer.Panel1MinSize = 0;
            // 
            // analysisAndBoardSplitContainer.Panel2
            // 
            this.analysisAndBoardSplitContainer.Panel2.Controls.Add(this.chessBoard);
            this.analysisAndBoardSplitContainer.Size = new System.Drawing.Size(424, 374);
            this.analysisAndBoardSplitContainer.SplitterDistance = 132;
            this.analysisAndBoardSplitContainer.TabIndex = 3;
            // 
            // firstGameInfoGroupBox
            // 
            this.firstGameInfoGroupBox.Controls.Add(this.fenRichTextBox);
            this.firstGameInfoGroupBox.Controls.Add(this.firstGameInfoRichTextBox);
            this.firstGameInfoGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.firstGameInfoGroupBox.Location = new System.Drawing.Point(0, 380);
            this.firstGameInfoGroupBox.Name = "firstGameInfoGroupBox";
            this.firstGameInfoGroupBox.Size = new System.Drawing.Size(424, 64);
            this.firstGameInfoGroupBox.TabIndex = 2;
            this.firstGameInfoGroupBox.TabStop = false;
            // 
            // fenRichTextBox
            // 
            this.fenRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fenRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fenRichTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.fenRichTextBox.Location = new System.Drawing.Point(3, 43);
            this.fenRichTextBox.Name = "fenRichTextBox";
            this.fenRichTextBox.ReadOnly = true;
            this.fenRichTextBox.Size = new System.Drawing.Size(415, 15);
            this.fenRichTextBox.TabIndex = 1;
            this.fenRichTextBox.Text = "";
            this.fenRichTextBox.WordWrap = false;
            // 
            // firstGameInfoRichTextBox
            // 
            this.firstGameInfoRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.firstGameInfoRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.firstGameInfoRichTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.firstGameInfoRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.firstGameInfoRichTextBox.Location = new System.Drawing.Point(3, 10);
            this.firstGameInfoRichTextBox.Name = "firstGameInfoRichTextBox";
            this.firstGameInfoRichTextBox.ReadOnly = true;
            this.firstGameInfoRichTextBox.Size = new System.Drawing.Size(415, 27);
            this.firstGameInfoRichTextBox.TabIndex = 0;
            this.firstGameInfoRichTextBox.Text = "";
            this.firstGameInfoRichTextBox.WordWrap = false;
            // 
            // entriesRetractionsSplitPanel
            // 
            this.entriesRetractionsSplitPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.entriesRetractionsSplitPanel.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.entriesRetractionsSplitPanel.Location = new System.Drawing.Point(3, 174);
            this.entriesRetractionsSplitPanel.Name = "entriesRetractionsSplitPanel";
            this.entriesRetractionsSplitPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // entriesRetractionsSplitPanel.Panel1
            // 
            this.entriesRetractionsSplitPanel.Panel1.Controls.Add(this.dataHelpButton);
            this.entriesRetractionsSplitPanel.Panel1.Controls.Add(this.entriesGridView);
            // 
            // entriesRetractionsSplitPanel.Panel2
            // 
            this.entriesRetractionsSplitPanel.Panel2.Controls.Add(this.retractionsHelpButton);
            this.entriesRetractionsSplitPanel.Panel2.Controls.Add(this.retractionsGridView);
            this.entriesRetractionsSplitPanel.Size = new System.Drawing.Size(601, 381);
            this.entriesRetractionsSplitPanel.SplitterDistance = 270;
            this.entriesRetractionsSplitPanel.TabIndex = 9;
            // 
            // dataHelpButton
            // 
            this.dataHelpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dataHelpButton.Location = new System.Drawing.Point(0, 0);
            this.dataHelpButton.Margin = new System.Windows.Forms.Padding(0);
            this.dataHelpButton.Name = "dataHelpButton";
            this.dataHelpButton.Size = new System.Drawing.Size(50, 21);
            this.dataHelpButton.TabIndex = 10;
            this.dataHelpButton.Text = "?";
            this.tooltip.SetToolTip(this.dataHelpButton, "Click me!");
            this.dataHelpButton.UseVisualStyleBackColor = true;
            this.dataHelpButton.Click += new System.EventHandler(this.dataHelpButton_Click);
            // 
            // retractionsHelpButton
            // 
            this.retractionsHelpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.retractionsHelpButton.Location = new System.Drawing.Point(0, 0);
            this.retractionsHelpButton.Margin = new System.Windows.Forms.Padding(0);
            this.retractionsHelpButton.Name = "retractionsHelpButton";
            this.retractionsHelpButton.Size = new System.Drawing.Size(50, 21);
            this.retractionsHelpButton.TabIndex = 11;
            this.retractionsHelpButton.Text = "?";
            this.tooltip.SetToolTip(this.retractionsHelpButton, "Click me!");
            this.retractionsHelpButton.UseVisualStyleBackColor = true;
            this.retractionsHelpButton.Click += new System.EventHandler(this.retractionsHelpButton_Click);
            // 
            // retractionsGridView
            // 
            this.retractionsGridView.AllowUserToAddRows = false;
            this.retractionsGridView.AllowUserToDeleteRows = false;
            this.retractionsGridView.AllowUserToResizeColumns = false;
            this.retractionsGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.retractionsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.retractionsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.retractionsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.retractionsGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.retractionsGridView.Location = new System.Drawing.Point(0, 0);
            this.retractionsGridView.Name = "retractionsGridView";
            this.retractionsGridView.ReadOnly = true;
            this.retractionsGridView.RowHeadersWidth = 20;
            this.retractionsGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.retractionsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.retractionsGridView.Size = new System.Drawing.Size(601, 107);
            this.retractionsGridView.TabIndex = 8;
            this.retractionsGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.retractionsGridView_CellContentDoubleClick);
            this.retractionsGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.RetractionsGridView_CellFormatting);
            this.retractionsGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.retractionsGridView_RowPrePaint);
            // 
            // totalDataHelpButton
            // 
            this.totalDataHelpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.totalDataHelpButton.Location = new System.Drawing.Point(3, 102);
            this.totalDataHelpButton.Margin = new System.Windows.Forms.Padding(0);
            this.totalDataHelpButton.Name = "totalDataHelpButton";
            this.totalDataHelpButton.Size = new System.Drawing.Size(50, 21);
            this.totalDataHelpButton.TabIndex = 7;
            this.totalDataHelpButton.Text = "?";
            this.tooltip.SetToolTip(this.totalDataHelpButton, "Click me!");
            this.totalDataHelpButton.UseVisualStyleBackColor = true;
            this.totalDataHelpButton.Click += new System.EventHandler(this.TotalDataHelpButton_Click);
            // 
            // goodnessGroupBox
            // 
            this.goodnessGroupBox.Controls.Add(this.goodnessFormulaComboBox);
            this.goodnessGroupBox.Controls.Add(this.label2);
            this.goodnessGroupBox.Controls.Add(this.label1);
            this.goodnessGroupBox.Controls.Add(this.gamesEvalWeightTrackBar);
            this.goodnessGroupBox.Controls.Add(this.lowNThresholdNumericUpDown);
            this.goodnessGroupBox.Controls.Add(this.lowNThesholdCheckBox);
            this.goodnessGroupBox.Controls.Add(this.drawScoreLabel);
            this.goodnessGroupBox.Controls.Add(this.drawScoreNumericUpDown);
            this.goodnessGroupBox.Controls.Add(this.goodnessNormalizeCheckbox);
            this.goodnessGroupBox.Location = new System.Drawing.Point(322, 3);
            this.goodnessGroupBox.Name = "goodnessGroupBox";
            this.goodnessGroupBox.Size = new System.Drawing.Size(293, 93);
            this.goodnessGroupBox.TabIndex = 6;
            this.goodnessGroupBox.TabStop = false;
            this.goodnessGroupBox.Text = "Quality Index";
            // 
            // goodnessFormulaComboBox
            // 
            this.goodnessFormulaComboBox.FormattingEnabled = true;
            this.goodnessFormulaComboBox.Location = new System.Drawing.Point(6, 15);
            this.goodnessFormulaComboBox.Name = "goodnessFormulaComboBox";
            this.goodnessFormulaComboBox.Size = new System.Drawing.Size(159, 21);
            this.goodnessFormulaComboBox.TabIndex = 20;
            this.goodnessFormulaComboBox.SelectedIndexChanged += new System.EventHandler(this.goodnessFormulaComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(254, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Eval";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Games";
            // 
            // gamesEvalWeightTrackBar
            // 
            this.gamesEvalWeightTrackBar.AutoSize = false;
            this.gamesEvalWeightTrackBar.LargeChange = 10;
            this.gamesEvalWeightTrackBar.Location = new System.Drawing.Point(52, 64);
            this.gamesEvalWeightTrackBar.Maximum = 100;
            this.gamesEvalWeightTrackBar.Name = "gamesEvalWeightTrackBar";
            this.gamesEvalWeightTrackBar.Size = new System.Drawing.Size(196, 20);
            this.gamesEvalWeightTrackBar.TabIndex = 17;
            this.gamesEvalWeightTrackBar.TickFrequency = 10;
            this.gamesEvalWeightTrackBar.Value = 50;
            this.gamesEvalWeightTrackBar.Scroll += new System.EventHandler(this.gamesEvalWeightTrackBar_Scroll);
            // 
            // lowNThresholdNumericUpDown
            // 
            this.lowNThresholdNumericUpDown.Location = new System.Drawing.Point(115, 42);
            this.lowNThresholdNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.lowNThresholdNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lowNThresholdNumericUpDown.Name = "lowNThresholdNumericUpDown";
            this.lowNThresholdNumericUpDown.Size = new System.Drawing.Size(50, 20);
            this.lowNThresholdNumericUpDown.TabIndex = 16;
            this.lowNThresholdNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.lowNThresholdNumericUpDown.ValueChanged += new System.EventHandler(this.lowNThresholdNumericUpDown_ValueChanged);
            // 
            // lowNThesholdCheckBox
            // 
            this.lowNThesholdCheckBox.AutoSize = true;
            this.lowNThesholdCheckBox.Checked = true;
            this.lowNThesholdCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lowNThesholdCheckBox.Location = new System.Drawing.Point(6, 43);
            this.lowNThesholdCheckBox.Name = "lowNThesholdCheckBox";
            this.lowNThesholdCheckBox.Size = new System.Drawing.Size(103, 17);
            this.lowNThesholdCheckBox.TabIndex = 15;
            this.lowNThesholdCheckBox.Text = "Low N threshold";
            this.tooltip.SetToolTip(this.lowNThesholdCheckBox, "When enabled the quality index calculation will include engine evaluation");
            this.lowNThesholdCheckBox.UseVisualStyleBackColor = true;
            this.lowNThesholdCheckBox.CheckedChanged += new System.EventHandler(this.lowNThesholdCheckBox_CheckedChanged);
            // 
            // drawScoreLabel
            // 
            this.drawScoreLabel.AutoSize = true;
            this.drawScoreLabel.Location = new System.Drawing.Point(170, 44);
            this.drawScoreLabel.Name = "drawScoreLabel";
            this.drawScoreLabel.Size = new System.Drawing.Size(61, 13);
            this.drawScoreLabel.TabIndex = 14;
            this.drawScoreLabel.Text = "Draw score";
            // 
            // drawScoreNumericUpDown
            // 
            this.drawScoreNumericUpDown.DecimalPlaces = 2;
            this.drawScoreNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.drawScoreNumericUpDown.Location = new System.Drawing.Point(237, 42);
            this.drawScoreNumericUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.drawScoreNumericUpDown.Name = "drawScoreNumericUpDown";
            this.drawScoreNumericUpDown.Size = new System.Drawing.Size(50, 20);
            this.drawScoreNumericUpDown.TabIndex = 13;
            this.drawScoreNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.drawScoreNumericUpDown.ValueChanged += new System.EventHandler(this.drawScoreNumericUpDown_ValueChanged);
            // 
            // goodnessNormalizeCheckbox
            // 
            this.goodnessNormalizeCheckbox.AutoSize = true;
            this.goodnessNormalizeCheckbox.Checked = true;
            this.goodnessNormalizeCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.goodnessNormalizeCheckbox.Location = new System.Drawing.Point(173, 19);
            this.goodnessNormalizeCheckbox.Name = "goodnessNormalizeCheckbox";
            this.goodnessNormalizeCheckbox.Size = new System.Drawing.Size(72, 17);
            this.goodnessNormalizeCheckbox.TabIndex = 6;
            this.goodnessNormalizeCheckbox.Text = "Normalize";
            this.tooltip.SetToolTip(this.goodnessNormalizeCheckbox, "When enabled the quality index will be normalized to be in range 0..100");
            this.goodnessNormalizeCheckbox.UseVisualStyleBackColor = true;
            this.goodnessNormalizeCheckbox.CheckedChanged += new System.EventHandler(this.GoodnessNormalizeCheckbox_CheckedChanged);
            // 
            // displayGroupBox
            // 
            this.displayGroupBox.Controls.Add(this.typeTranspositionsCheckBox);
            this.displayGroupBox.Controls.Add(this.hideNeverPlayedCheckBox);
            this.displayGroupBox.Controls.Add(this.typeContinuationsCheckBox);
            this.displayGroupBox.Location = new System.Drawing.Point(215, 3);
            this.displayGroupBox.Name = "displayGroupBox";
            this.displayGroupBox.Size = new System.Drawing.Size(101, 93);
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.totalEntriesGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.totalEntriesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.totalEntriesGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.totalEntriesGridView.Location = new System.Drawing.Point(3, 102);
            this.totalEntriesGridView.Name = "totalEntriesGridView";
            this.totalEntriesGridView.ReadOnly = true;
            this.totalEntriesGridView.RowHeadersWidth = 20;
            this.totalEntriesGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.totalEntriesGridView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.totalEntriesGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.totalEntriesGridView.Size = new System.Drawing.Size(601, 66);
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
            this.queryEvalCheckBox.CheckedChanged += new System.EventHandler(this.queryEvalCheckBox_CheckedChanged);
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
            // tooltip
            // 
            this.tooltip.AutomaticDelay = 200;
            this.tooltip.AutoPopDelay = 10000;
            this.tooltip.InitialDelay = 200;
            this.tooltip.ReshowDelay = 40;
            // 
            // chessBoard
            // 
            this.chessBoard.BoardImages = null;
            this.chessBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chessBoard.Location = new System.Drawing.Point(0, 0);
            this.chessBoard.Margin = new System.Windows.Forms.Padding(0);
            this.chessBoard.MinimumSize = new System.Drawing.Size(1, 1);
            this.chessBoard.Name = "chessBoard";
            this.chessBoard.PieceImages = null;
            this.chessBoard.Size = new System.Drawing.Size(424, 238);
            this.chessBoard.TabIndex = 0;
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
            this.Load += new System.EventHandler(this.Application_Load);
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
            this.analysisAndBoardSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.analysisAndBoardSplitContainer)).EndInit();
            this.analysisAndBoardSplitContainer.ResumeLayout(false);
            this.firstGameInfoGroupBox.ResumeLayout(false);
            this.entriesRetractionsSplitPanel.Panel1.ResumeLayout(false);
            this.entriesRetractionsSplitPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.entriesRetractionsSplitPanel)).EndInit();
            this.entriesRetractionsSplitPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.retractionsGridView)).EndInit();
            this.goodnessGroupBox.ResumeLayout(false);
            this.goodnessGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gamesEvalWeightTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowNThresholdNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawScoreNumericUpDown)).EndInit();
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
        private System.Windows.Forms.CheckBox goodnessNormalizeCheckbox;
        private System.Windows.Forms.ToolStripMenuItem analysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
        private System.Windows.Forms.RichTextBox fenRichTextBox;
        private System.Windows.Forms.SplitContainer analysisAndBoardSplitContainer;
        private System.Windows.Forms.ToolStripMenuItem profilesToolStripMenuItem;
        private System.Windows.Forms.Button totalDataHelpButton;
        private System.Windows.Forms.ToolStripMenuItem themesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themesToolStripMenuItem1;
        private System.Windows.Forms.DataGridView retractionsGridView;
        private System.Windows.Forms.SplitContainer entriesRetractionsSplitPanel;
        private System.Windows.Forms.ToolStripMenuItem mergeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem appendToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label drawScoreLabel;
        private System.Windows.Forms.NumericUpDown drawScoreNumericUpDown;
        private System.Windows.Forms.Button dataHelpButton;
        private System.Windows.Forms.Button retractionsHelpButton;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseFormatsToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown lowNThresholdNumericUpDown;
        private System.Windows.Forms.CheckBox lowNThesholdCheckBox;
        private System.Windows.Forms.ComboBox goodnessFormulaComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar gamesEvalWeightTrackBar;
    }
}

