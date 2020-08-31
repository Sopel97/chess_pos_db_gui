namespace chess_pos_db_gui
{
    partial class EpdDumpForm
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
            this.pgnsDataGridView = new System.Windows.Forms.DataGridView();
            this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Progress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.clearSecondaryTempFolderButton = new System.Windows.Forms.Button();
            this.setSecondaryTempFolderButton = new System.Windows.Forms.Button();
            this.secondaryTempFolderTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.maxPlyNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.maxPlyLabel = new System.Windows.Forms.Label();
            this.minCountLabel = new System.Windows.Forms.Label();
            this.minCountInput = new System.Windows.Forms.NumericUpDown();
            this.clearTempFolderButton = new System.Windows.Forms.Button();
            this.tempFolderLabel = new System.Windows.Forms.Label();
            this.outputPathLabel = new System.Windows.Forms.Label();
            this.setOutputPathButton = new System.Windows.Forms.Button();
            this.setTempFolderButton = new System.Windows.Forms.Button();
            this.tempFolderTextBox = new System.Windows.Forms.TextBox();
            this.outputPathTextBox = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.addPgnsButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dumpProgressLabel = new System.Windows.Forms.Label();
            this.dumpProgressBar = new System.Windows.Forms.ProgressBar();
            this.dumpButton = new System.Windows.Forms.Button();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.minPiecesLabel = new System.Windows.Forms.Label();
            this.minPiecesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.includeFenCheckBox = new System.Windows.Forms.CheckBox();
            this.includeWinCountCheckBox = new System.Windows.Forms.CheckBox();
            this.includeDrawCountCheckBox = new System.Windows.Forms.CheckBox();
            this.includeLossCountCheckBox = new System.Windows.Forms.CheckBox();
            this.includePerfCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pgnsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxPlyNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minCountInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minPiecesNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // pgnsDataGridView
            // 
            this.pgnsDataGridView.AllowUserToAddRows = false;
            this.pgnsDataGridView.AllowUserToResizeRows = false;
            this.pgnsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgnsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.pgnsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.pgnsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Path,
            this.Progress});
            this.pgnsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.pgnsDataGridView.Location = new System.Drawing.Point(3, 35);
            this.pgnsDataGridView.Name = "pgnsDataGridView";
            this.pgnsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.pgnsDataGridView.Size = new System.Drawing.Size(618, 323);
            this.pgnsDataGridView.TabIndex = 2;
            this.tooltip.SetToolTip(this.pgnsDataGridView, "The PGN files to be scanned.");
            this.pgnsDataGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.PgnsDataGridView_RowPrePaint);
            // 
            // Path
            // 
            this.Path.FillWeight = 85F;
            this.Path.HeaderText = "Path";
            this.Path.Name = "Path";
            this.Path.ReadOnly = true;
            // 
            // Progress
            // 
            this.Progress.FillWeight = 15F;
            this.Progress.HeaderText = "Progress";
            this.Progress.Name = "Progress";
            this.Progress.ReadOnly = true;
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
            this.splitContainer1.Panel1.Controls.Add(this.includePerfCheckBox);
            this.splitContainer1.Panel1.Controls.Add(this.includeLossCountCheckBox);
            this.splitContainer1.Panel1.Controls.Add(this.includeDrawCountCheckBox);
            this.splitContainer1.Panel1.Controls.Add(this.includeWinCountCheckBox);
            this.splitContainer1.Panel1.Controls.Add(this.includeFenCheckBox);
            this.splitContainer1.Panel1.Controls.Add(this.minPiecesNumericUpDown);
            this.splitContainer1.Panel1.Controls.Add(this.minPiecesLabel);
            this.splitContainer1.Panel1.Controls.Add(this.clearSecondaryTempFolderButton);
            this.splitContainer1.Panel1.Controls.Add(this.setSecondaryTempFolderButton);
            this.splitContainer1.Panel1.Controls.Add(this.secondaryTempFolderTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.maxPlyNumericUpDown);
            this.splitContainer1.Panel1.Controls.Add(this.maxPlyLabel);
            this.splitContainer1.Panel1.Controls.Add(this.minCountLabel);
            this.splitContainer1.Panel1.Controls.Add(this.minCountInput);
            this.splitContainer1.Panel1.Controls.Add(this.clearTempFolderButton);
            this.splitContainer1.Panel1.Controls.Add(this.tempFolderLabel);
            this.splitContainer1.Panel1.Controls.Add(this.outputPathLabel);
            this.splitContainer1.Panel1.Controls.Add(this.setOutputPathButton);
            this.splitContainer1.Panel1.Controls.Add(this.setTempFolderButton);
            this.splitContainer1.Panel1.Controls.Add(this.tempFolderTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.outputPathTextBox);
            this.splitContainer1.Panel1MinSize = 135;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(624, 602);
            this.splitContainer1.SplitterDistance = 135;
            this.splitContainer1.TabIndex = 1;
            // 
            // clearSecondaryTempFolderButton
            // 
            this.clearSecondaryTempFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearSecondaryTempFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.clearSecondaryTempFolderButton.Location = new System.Drawing.Point(586, 65);
            this.clearSecondaryTempFolderButton.Name = "clearSecondaryTempFolderButton";
            this.clearSecondaryTempFolderButton.Size = new System.Drawing.Size(26, 19);
            this.clearSecondaryTempFolderButton.TabIndex = 16;
            this.clearSecondaryTempFolderButton.Text = "X";
            this.clearSecondaryTempFolderButton.UseVisualStyleBackColor = true;
            this.clearSecondaryTempFolderButton.Click += new System.EventHandler(this.clearSecondaryTempFolderButton_Click);
            // 
            // setSecondaryTempFolderButton
            // 
            this.setSecondaryTempFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.setSecondaryTempFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.setSecondaryTempFolderButton.Location = new System.Drawing.Point(541, 65);
            this.setSecondaryTempFolderButton.Name = "setSecondaryTempFolderButton";
            this.setSecondaryTempFolderButton.Size = new System.Drawing.Size(39, 19);
            this.setSecondaryTempFolderButton.TabIndex = 15;
            this.setSecondaryTempFolderButton.Text = "...";
            this.setSecondaryTempFolderButton.UseVisualStyleBackColor = true;
            this.setSecondaryTempFolderButton.Click += new System.EventHandler(this.setSecondaryTempFolderButton_Click);
            // 
            // secondaryTempFolderTextBox
            // 
            this.secondaryTempFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.secondaryTempFolderTextBox.Location = new System.Drawing.Point(130, 64);
            this.secondaryTempFolderTextBox.Name = "secondaryTempFolderTextBox";
            this.secondaryTempFolderTextBox.ReadOnly = true;
            this.secondaryTempFolderTextBox.Size = new System.Drawing.Size(405, 20);
            this.secondaryTempFolderTextBox.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Secondary temp folder: ";
            this.tooltip.SetToolTip(this.label1, "The temporary folder used for intermediate data.");
            // 
            // maxPlyNumericUpDown
            // 
            this.maxPlyNumericUpDown.Location = new System.Drawing.Point(261, 90);
            this.maxPlyNumericUpDown.Maximum = new decimal(new int[] {
            276447231,
            23283,
            0,
            0});
            this.maxPlyNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxPlyNumericUpDown.Name = "maxPlyNumericUpDown";
            this.maxPlyNumericUpDown.Size = new System.Drawing.Size(70, 20);
            this.maxPlyNumericUpDown.TabIndex = 12;
            this.maxPlyNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // maxPlyLabel
            // 
            this.maxPlyLabel.AutoSize = true;
            this.maxPlyLabel.Location = new System.Drawing.Point(206, 92);
            this.maxPlyLabel.Name = "maxPlyLabel";
            this.maxPlyLabel.Size = new System.Drawing.Size(49, 13);
            this.maxPlyLabel.TabIndex = 11;
            this.maxPlyLabel.Text = "Max ply: ";
            this.tooltip.SetToolTip(this.maxPlyLabel, "The minimal number of times the position needs to be seen to include it in the ou" +
        "tput epd. If 1 then all positions are included.");
            // 
            // minCountLabel
            // 
            this.minCountLabel.AutoSize = true;
            this.minCountLabel.Location = new System.Drawing.Point(64, 92);
            this.minCountLabel.Name = "minCountLabel";
            this.minCountLabel.Size = new System.Drawing.Size(60, 13);
            this.minCountLabel.TabIndex = 10;
            this.minCountLabel.Text = "Min count: ";
            this.tooltip.SetToolTip(this.minCountLabel, "The minimal number of times the position needs to be seen to include it in the ou" +
        "tput epd. If 1 then all positions are included.");
            // 
            // minCountInput
            // 
            this.minCountInput.Location = new System.Drawing.Point(130, 90);
            this.minCountInput.Maximum = new decimal(new int[] {
            276447231,
            23283,
            0,
            0});
            this.minCountInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.minCountInput.Name = "minCountInput";
            this.minCountInput.Size = new System.Drawing.Size(70, 20);
            this.minCountInput.TabIndex = 9;
            this.minCountInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // clearTempFolderButton
            // 
            this.clearTempFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearTempFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.clearTempFolderButton.Location = new System.Drawing.Point(586, 38);
            this.clearTempFolderButton.Name = "clearTempFolderButton";
            this.clearTempFolderButton.Size = new System.Drawing.Size(26, 19);
            this.clearTempFolderButton.TabIndex = 8;
            this.clearTempFolderButton.Text = "X";
            this.clearTempFolderButton.UseVisualStyleBackColor = true;
            this.clearTempFolderButton.Click += new System.EventHandler(this.ClearTempFolderButton_Click);
            // 
            // tempFolderLabel
            // 
            this.tempFolderLabel.AutoSize = true;
            this.tempFolderLabel.Location = new System.Drawing.Point(22, 41);
            this.tempFolderLabel.Name = "tempFolderLabel";
            this.tempFolderLabel.Size = new System.Drawing.Size(102, 13);
            this.tempFolderLabel.TabIndex = 7;
            this.tempFolderLabel.Text = "Primary temp folder: ";
            this.tooltip.SetToolTip(this.tempFolderLabel, "The temporary folder used for intermediate data.");
            // 
            // outputPathLabel
            // 
            this.outputPathLabel.AutoSize = true;
            this.outputPathLabel.Location = new System.Drawing.Point(55, 15);
            this.outputPathLabel.Name = "outputPathLabel";
            this.outputPathLabel.Size = new System.Drawing.Size(69, 13);
            this.outputPathLabel.TabIndex = 6;
            this.outputPathLabel.Text = "Output path: ";
            this.tooltip.SetToolTip(this.outputPathLabel, "The path to the resulting epd file.");
            // 
            // setOutputPathButton
            // 
            this.setOutputPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.setOutputPathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.setOutputPathButton.Location = new System.Drawing.Point(541, 12);
            this.setOutputPathButton.Name = "setOutputPathButton";
            this.setOutputPathButton.Size = new System.Drawing.Size(39, 19);
            this.setOutputPathButton.TabIndex = 5;
            this.setOutputPathButton.Text = "...";
            this.setOutputPathButton.UseVisualStyleBackColor = true;
            this.setOutputPathButton.Click += new System.EventHandler(this.SetOutputPathButton_Click);
            // 
            // setTempFolderButton
            // 
            this.setTempFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.setTempFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.setTempFolderButton.Location = new System.Drawing.Point(541, 38);
            this.setTempFolderButton.Name = "setTempFolderButton";
            this.setTempFolderButton.Size = new System.Drawing.Size(39, 19);
            this.setTempFolderButton.TabIndex = 4;
            this.setTempFolderButton.Text = "...";
            this.setTempFolderButton.UseVisualStyleBackColor = true;
            this.setTempFolderButton.Click += new System.EventHandler(this.SetTempFolderButton_Click);
            // 
            // tempFolderTextBox
            // 
            this.tempFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tempFolderTextBox.Location = new System.Drawing.Point(130, 38);
            this.tempFolderTextBox.Name = "tempFolderTextBox";
            this.tempFolderTextBox.ReadOnly = true;
            this.tempFolderTextBox.Size = new System.Drawing.Size(405, 20);
            this.tempFolderTextBox.TabIndex = 2;
            // 
            // outputPathTextBox
            // 
            this.outputPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputPathTextBox.Location = new System.Drawing.Point(130, 12);
            this.outputPathTextBox.Name = "outputPathTextBox";
            this.outputPathTextBox.ReadOnly = true;
            this.outputPathTextBox.Size = new System.Drawing.Size(405, 20);
            this.outputPathTextBox.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.addPgnsButton);
            this.splitContainer2.Panel1.Controls.Add(this.pgnsDataGridView);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2.Controls.Add(this.dumpProgressLabel);
            this.splitContainer2.Panel2.Controls.Add(this.dumpProgressBar);
            this.splitContainer2.Panel2.Controls.Add(this.dumpButton);
            this.splitContainer2.Size = new System.Drawing.Size(624, 463);
            this.splitContainer2.SplitterDistance = 361;
            this.splitContainer2.TabIndex = 0;
            // 
            // addPgnsButton
            // 
            this.addPgnsButton.Location = new System.Drawing.Point(3, 3);
            this.addPgnsButton.Name = "addPgnsButton";
            this.addPgnsButton.Size = new System.Drawing.Size(110, 26);
            this.addPgnsButton.TabIndex = 3;
            this.addPgnsButton.Text = "Add PGN Files";
            this.addPgnsButton.UseVisualStyleBackColor = true;
            this.addPgnsButton.Click += new System.EventHandler(this.AddPgnsButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Dump progress: ";
            this.tooltip.SetToolTip(this.label2, "The minimal number of times the position needs to be seen to include it in the ou" +
        "tput epd. If 1 then all positions are included.");
            // 
            // dumpProgressLabel
            // 
            this.dumpProgressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dumpProgressLabel.AutoSize = true;
            this.dumpProgressLabel.Location = new System.Drawing.Point(455, 13);
            this.dumpProgressLabel.Name = "dumpProgressLabel";
            this.dumpProgressLabel.Size = new System.Drawing.Size(21, 13);
            this.dumpProgressLabel.TabIndex = 4;
            this.dumpProgressLabel.Text = "0%";
            // 
            // dumpProgressBar
            // 
            this.dumpProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dumpProgressBar.Location = new System.Drawing.Point(130, 9);
            this.dumpProgressBar.Name = "dumpProgressBar";
            this.dumpProgressBar.Size = new System.Drawing.Size(319, 20);
            this.dumpProgressBar.TabIndex = 3;
            this.tooltip.SetToolTip(this.dumpProgressBar, "Dumping progress.");
            // 
            // dumpButton
            // 
            this.dumpButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.dumpButton.Location = new System.Drawing.Point(496, 0);
            this.dumpButton.Name = "dumpButton";
            this.dumpButton.Size = new System.Drawing.Size(128, 98);
            this.dumpButton.TabIndex = 1;
            this.dumpButton.Text = "Dump";
            this.tooltip.SetToolTip(this.dumpButton, "Perform dump.");
            this.dumpButton.UseVisualStyleBackColor = true;
            this.dumpButton.Click += new System.EventHandler(this.DumpButton_Click);
            // 
            // minPiecesLabel
            // 
            this.minPiecesLabel.AutoSize = true;
            this.minPiecesLabel.Location = new System.Drawing.Point(337, 92);
            this.minPiecesLabel.Name = "minPiecesLabel";
            this.minPiecesLabel.Size = new System.Drawing.Size(64, 13);
            this.minPiecesLabel.TabIndex = 17;
            this.minPiecesLabel.Text = "Min pieces: ";
            this.tooltip.SetToolTip(this.minPiecesLabel, "The minimal number of pieces for positions to be included");
            // 
            // minPiecesNumericUpDown
            // 
            this.minPiecesNumericUpDown.Location = new System.Drawing.Point(406, 90);
            this.minPiecesNumericUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.minPiecesNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.minPiecesNumericUpDown.Name = "minPiecesNumericUpDown";
            this.minPiecesNumericUpDown.Size = new System.Drawing.Size(70, 20);
            this.minPiecesNumericUpDown.TabIndex = 18;
            this.minPiecesNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // includeFenCheckBox
            // 
            this.includeFenCheckBox.AutoSize = true;
            this.includeFenCheckBox.Checked = true;
            this.includeFenCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.includeFenCheckBox.Location = new System.Drawing.Point(130, 115);
            this.includeFenCheckBox.Name = "includeFenCheckBox";
            this.includeFenCheckBox.Size = new System.Drawing.Size(47, 17);
            this.includeFenCheckBox.TabIndex = 19;
            this.includeFenCheckBox.Text = "FEN";
            this.includeFenCheckBox.UseVisualStyleBackColor = true;
            // 
            // includeWinCountCheckBox
            // 
            this.includeWinCountCheckBox.AutoSize = true;
            this.includeWinCountCheckBox.Location = new System.Drawing.Point(183, 115);
            this.includeWinCountCheckBox.Name = "includeWinCountCheckBox";
            this.includeWinCountCheckBox.Size = new System.Drawing.Size(75, 17);
            this.includeWinCountCheckBox.TabIndex = 20;
            this.includeWinCountCheckBox.Text = "Win count";
            this.includeWinCountCheckBox.UseVisualStyleBackColor = true;
            // 
            // includeDrawCountCheckBox
            // 
            this.includeDrawCountCheckBox.AutoSize = true;
            this.includeDrawCountCheckBox.Location = new System.Drawing.Point(264, 115);
            this.includeDrawCountCheckBox.Name = "includeDrawCountCheckBox";
            this.includeDrawCountCheckBox.Size = new System.Drawing.Size(81, 17);
            this.includeDrawCountCheckBox.TabIndex = 21;
            this.includeDrawCountCheckBox.Text = "Draw count";
            this.includeDrawCountCheckBox.UseVisualStyleBackColor = true;
            // 
            // includeLossCountCheckBox
            // 
            this.includeLossCountCheckBox.AutoSize = true;
            this.includeLossCountCheckBox.Location = new System.Drawing.Point(351, 115);
            this.includeLossCountCheckBox.Name = "includeLossCountCheckBox";
            this.includeLossCountCheckBox.Size = new System.Drawing.Size(78, 17);
            this.includeLossCountCheckBox.TabIndex = 22;
            this.includeLossCountCheckBox.Text = "Loss count";
            this.includeLossCountCheckBox.UseVisualStyleBackColor = true;
            // 
            // includePerfCheckBox
            // 
            this.includePerfCheckBox.AutoSize = true;
            this.includePerfCheckBox.Location = new System.Drawing.Point(435, 115);
            this.includePerfCheckBox.Name = "includePerfCheckBox";
            this.includePerfCheckBox.Size = new System.Drawing.Size(45, 17);
            this.includePerfCheckBox.TabIndex = 23;
            this.includePerfCheckBox.Text = "Perf";
            this.includePerfCheckBox.UseVisualStyleBackColor = true;
            // 
            // EpdDumpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 602);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "EpdDumpForm";
            this.Text = "Position dump";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DatabaseCreationForm_FormClosing);
            this.Load += new System.EventHandler(this.DatabaseCreationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pgnsDataGridView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.maxPlyNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minCountInput)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.minPiecesNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView pgnsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Path;
        private System.Windows.Forms.DataGridViewTextBoxColumn Progress;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button clearTempFolderButton;
        private System.Windows.Forms.Label tempFolderLabel;
        private System.Windows.Forms.Label outputPathLabel;
        private System.Windows.Forms.Button setOutputPathButton;
        private System.Windows.Forms.Button setTempFolderButton;
        private System.Windows.Forms.TextBox tempFolderTextBox;
        private System.Windows.Forms.TextBox outputPathTextBox;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button addPgnsButton;
        private System.Windows.Forms.Label dumpProgressLabel;
        private System.Windows.Forms.ProgressBar dumpProgressBar;
        private System.Windows.Forms.Button dumpButton;
        private System.Windows.Forms.Label minCountLabel;
        private System.Windows.Forms.NumericUpDown minCountInput;
        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.NumericUpDown maxPlyNumericUpDown;
        private System.Windows.Forms.Label maxPlyLabel;
        private System.Windows.Forms.Button clearSecondaryTempFolderButton;
        private System.Windows.Forms.Button setSecondaryTempFolderButton;
        private System.Windows.Forms.TextBox secondaryTempFolderTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown minPiecesNumericUpDown;
        private System.Windows.Forms.Label minPiecesLabel;
        private System.Windows.Forms.CheckBox includePerfCheckBox;
        private System.Windows.Forms.CheckBox includeLossCountCheckBox;
        private System.Windows.Forms.CheckBox includeDrawCountCheckBox;
        private System.Windows.Forms.CheckBox includeWinCountCheckBox;
        private System.Windows.Forms.CheckBox includeFenCheckBox;
    }
}