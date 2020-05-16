namespace chess_pos_db_gui
{
    partial class DatabaseCreationForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.whatsDatabaseFormatButton = new System.Windows.Forms.Button();
            this.databaseFormatLabel = new System.Windows.Forms.Label();
            this.databaseFormatComboBox = new System.Windows.Forms.ComboBox();
            this.destinationFolderLabel = new System.Windows.Forms.Label();
            this.setDestinationFolderButton = new System.Windows.Forms.Button();
            this.destinationFolderTextBox = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pgnPathsTabControl = new System.Windows.Forms.TabControl();
            this.humanTabPage = new System.Windows.Forms.TabPage();
            this.addHumanPgnsButton = new System.Windows.Forms.Button();
            this.humanPgnsDataGridView = new System.Windows.Forms.DataGridView();
            this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Progress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.engineTabPage = new System.Windows.Forms.TabPage();
            this.enginePgnsDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addEnginePgnsButton = new System.Windows.Forms.Button();
            this.serverTabPage = new System.Windows.Forms.TabPage();
            this.serverPgnsDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addServerPgnsButton = new System.Windows.Forms.Button();
            this.openCheckBox = new System.Windows.Forms.CheckBox();
            this.buildButton = new System.Windows.Forms.Button();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.tempDirsGroupBox = new System.Windows.Forms.GroupBox();
            this.maxTempStorageUsageCheckBox = new System.Windows.Forms.CheckBox();
            this.tempStorageUsageUnitComboBox = new System.Windows.Forms.ComboBox();
            this.tempStorageUsageSizeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.clearSecondaryTempFolderButton = new System.Windows.Forms.Button();
            this.secondaryTempFolderLabel = new System.Windows.Forms.Label();
            this.setSecondaryTempFolderButton = new System.Windows.Forms.Button();
            this.secondaryTempFolderTextBox = new System.Windows.Forms.TextBox();
            this.clearPrimaryTempFolderButton = new System.Windows.Forms.Button();
            this.primaryTempFolderLabel = new System.Windows.Forms.Label();
            this.setPrimaryTempFolderButton = new System.Windows.Forms.Button();
            this.primaryTempFolderTextBox = new System.Windows.Forms.TextBox();
            this.progressGroupBox = new System.Windows.Forms.GroupBox();
            this.mergeProgressLabelInfo = new System.Windows.Forms.Label();
            this.importProgressLabelInfo = new System.Windows.Forms.Label();
            this.mergeProgressLabel = new System.Windows.Forms.Label();
            this.mergeProgressBar = new System.Windows.Forms.ProgressBar();
            this.importProgressLabel = new System.Windows.Forms.Label();
            this.importProgressBar = new System.Windows.Forms.ProgressBar();
            this.mergeAllAfterImportRadioButton = new System.Windows.Forms.RadioButton();
            this.dontMergeRadioButton = new System.Windows.Forms.RadioButton();
            this.mergeManuallyAfterImportRadioButton = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.importSettingsGroupBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.pgnPathsTabControl.SuspendLayout();
            this.humanTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.humanPgnsDataGridView)).BeginInit();
            this.engineTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.enginePgnsDataGridView)).BeginInit();
            this.serverTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverPgnsDataGridView)).BeginInit();
            this.tempDirsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tempStorageUsageSizeNumericUpDown)).BeginInit();
            this.progressGroupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.importSettingsGroupBox.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.importSettingsGroupBox);
            this.splitContainer1.Panel1MinSize = 90;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(658, 662);
            this.splitContainer1.SplitterDistance = 90;
            this.splitContainer1.TabIndex = 0;
            // 
            // whatsDatabaseFormatButton
            // 
            this.whatsDatabaseFormatButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.whatsDatabaseFormatButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.whatsDatabaseFormatButton.Location = new System.Drawing.Point(567, 38);
            this.whatsDatabaseFormatButton.Name = "whatsDatabaseFormatButton";
            this.whatsDatabaseFormatButton.Size = new System.Drawing.Size(79, 19);
            this.whatsDatabaseFormatButton.TabIndex = 11;
            this.whatsDatabaseFormatButton.Text = "What\'s this?";
            this.whatsDatabaseFormatButton.UseVisualStyleBackColor = true;
            this.whatsDatabaseFormatButton.Click += new System.EventHandler(this.WhatsDatabaseFormatButton_Click);
            // 
            // databaseFormatLabel
            // 
            this.databaseFormatLabel.AutoSize = true;
            this.databaseFormatLabel.Location = new System.Drawing.Point(82, 41);
            this.databaseFormatLabel.Name = "databaseFormatLabel";
            this.databaseFormatLabel.Size = new System.Drawing.Size(91, 13);
            this.databaseFormatLabel.TabIndex = 10;
            this.databaseFormatLabel.Text = "Database format: ";
            this.tooltip.SetToolTip(this.databaseFormatLabel, "The database format to use. Descriptions can be found on project\'s github page in" +
        " the documentation. Default is best for uninformed users.");
            // 
            // databaseFormatComboBox
            // 
            this.databaseFormatComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.databaseFormatComboBox.FormattingEnabled = true;
            this.databaseFormatComboBox.Location = new System.Drawing.Point(179, 38);
            this.databaseFormatComboBox.Name = "databaseFormatComboBox";
            this.databaseFormatComboBox.Size = new System.Drawing.Size(380, 21);
            this.databaseFormatComboBox.TabIndex = 9;
            this.databaseFormatComboBox.SelectedValueChanged += new System.EventHandler(this.DatabaseFormatComboBox_SelectedValueChanged);
            // 
            // destinationFolderLabel
            // 
            this.destinationFolderLabel.AutoSize = true;
            this.destinationFolderLabel.Location = new System.Drawing.Point(78, 15);
            this.destinationFolderLabel.Name = "destinationFolderLabel";
            this.destinationFolderLabel.Size = new System.Drawing.Size(95, 13);
            this.destinationFolderLabel.TabIndex = 6;
            this.destinationFolderLabel.Text = "Destination folder: ";
            this.tooltip.SetToolTip(this.destinationFolderLabel, "The folder in which the database will be created. The folder must exist.");
            // 
            // setDestinationFolderButton
            // 
            this.setDestinationFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.setDestinationFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.setDestinationFolderButton.Location = new System.Drawing.Point(567, 12);
            this.setDestinationFolderButton.Name = "setDestinationFolderButton";
            this.setDestinationFolderButton.Size = new System.Drawing.Size(47, 19);
            this.setDestinationFolderButton.TabIndex = 5;
            this.setDestinationFolderButton.Text = "...";
            this.setDestinationFolderButton.UseVisualStyleBackColor = true;
            this.setDestinationFolderButton.Click += new System.EventHandler(this.SetDestinationFolderButton_Click);
            // 
            // destinationFolderTextBox
            // 
            this.destinationFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.destinationFolderTextBox.Location = new System.Drawing.Point(179, 12);
            this.destinationFolderTextBox.Name = "destinationFolderTextBox";
            this.destinationFolderTextBox.ReadOnly = true;
            this.destinationFolderTextBox.Size = new System.Drawing.Size(380, 20);
            this.destinationFolderTextBox.TabIndex = 0;
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
            this.splitContainer2.Panel1.Controls.Add(this.pgnPathsTabControl);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.progressGroupBox);
            this.splitContainer2.Panel2.Controls.Add(this.buildButton);
            this.splitContainer2.Panel2.Controls.Add(this.tempDirsGroupBox);
            this.splitContainer2.Size = new System.Drawing.Size(658, 568);
            this.splitContainer2.SplitterDistance = 352;
            this.splitContainer2.TabIndex = 0;
            // 
            // pgnPathsTabControl
            // 
            this.pgnPathsTabControl.Controls.Add(this.humanTabPage);
            this.pgnPathsTabControl.Controls.Add(this.engineTabPage);
            this.pgnPathsTabControl.Controls.Add(this.serverTabPage);
            this.pgnPathsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgnPathsTabControl.Location = new System.Drawing.Point(0, 0);
            this.pgnPathsTabControl.Name = "pgnPathsTabControl";
            this.pgnPathsTabControl.SelectedIndex = 0;
            this.pgnPathsTabControl.Size = new System.Drawing.Size(658, 352);
            this.pgnPathsTabControl.TabIndex = 0;
            // 
            // humanTabPage
            // 
            this.humanTabPage.Controls.Add(this.addHumanPgnsButton);
            this.humanTabPage.Controls.Add(this.humanPgnsDataGridView);
            this.humanTabPage.Location = new System.Drawing.Point(4, 22);
            this.humanTabPage.Name = "humanTabPage";
            this.humanTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.humanTabPage.Size = new System.Drawing.Size(650, 326);
            this.humanTabPage.TabIndex = 0;
            this.humanTabPage.Text = "Human";
            this.humanTabPage.UseVisualStyleBackColor = true;
            // 
            // addHumanPgnsButton
            // 
            this.addHumanPgnsButton.Location = new System.Drawing.Point(6, 6);
            this.addHumanPgnsButton.Name = "addHumanPgnsButton";
            this.addHumanPgnsButton.Size = new System.Drawing.Size(110, 26);
            this.addHumanPgnsButton.TabIndex = 1;
            this.addHumanPgnsButton.Text = "Add Files";
            this.addHumanPgnsButton.UseVisualStyleBackColor = true;
            this.addHumanPgnsButton.Click += new System.EventHandler(this.AddHumanPgnsButton_Click);
            // 
            // humanPgnsDataGridView
            // 
            this.humanPgnsDataGridView.AllowUserToAddRows = false;
            this.humanPgnsDataGridView.AllowUserToResizeRows = false;
            this.humanPgnsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.humanPgnsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.humanPgnsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.humanPgnsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Path,
            this.Progress});
            this.humanPgnsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.humanPgnsDataGridView.Location = new System.Drawing.Point(6, 38);
            this.humanPgnsDataGridView.Name = "humanPgnsDataGridView";
            this.humanPgnsDataGridView.ReadOnly = true;
            this.humanPgnsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.humanPgnsDataGridView.Size = new System.Drawing.Size(638, 281);
            this.humanPgnsDataGridView.TabIndex = 0;
            this.tooltip.SetToolTip(this.humanPgnsDataGridView, "The list of PGN files to collect games from into Human category.");
            this.humanPgnsDataGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.HumanPgnsDataGridView_RowPrePaint);
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
            // engineTabPage
            // 
            this.engineTabPage.Controls.Add(this.enginePgnsDataGridView);
            this.engineTabPage.Controls.Add(this.addEnginePgnsButton);
            this.engineTabPage.Location = new System.Drawing.Point(4, 22);
            this.engineTabPage.Name = "engineTabPage";
            this.engineTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.engineTabPage.Size = new System.Drawing.Size(650, 326);
            this.engineTabPage.TabIndex = 1;
            this.engineTabPage.Text = "Engine";
            this.engineTabPage.UseVisualStyleBackColor = true;
            // 
            // enginePgnsDataGridView
            // 
            this.enginePgnsDataGridView.AllowUserToAddRows = false;
            this.enginePgnsDataGridView.AllowUserToResizeRows = false;
            this.enginePgnsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.enginePgnsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.enginePgnsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.enginePgnsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.enginePgnsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.enginePgnsDataGridView.Location = new System.Drawing.Point(6, 38);
            this.enginePgnsDataGridView.Name = "enginePgnsDataGridView";
            this.enginePgnsDataGridView.ReadOnly = true;
            this.enginePgnsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.enginePgnsDataGridView.Size = new System.Drawing.Size(638, 281);
            this.enginePgnsDataGridView.TabIndex = 4;
            this.tooltip.SetToolTip(this.enginePgnsDataGridView, "The list of PGN files to collect games from into Engine category.");
            this.enginePgnsDataGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.EnginePgnsDataGridView_RowPrePaint);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 85F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Path";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 15F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Progress";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // addEnginePgnsButton
            // 
            this.addEnginePgnsButton.Location = new System.Drawing.Point(6, 6);
            this.addEnginePgnsButton.Name = "addEnginePgnsButton";
            this.addEnginePgnsButton.Size = new System.Drawing.Size(110, 26);
            this.addEnginePgnsButton.TabIndex = 3;
            this.addEnginePgnsButton.Text = "Add Files";
            this.addEnginePgnsButton.UseVisualStyleBackColor = true;
            this.addEnginePgnsButton.Click += new System.EventHandler(this.AddEnginePgnsButton_Click);
            // 
            // serverTabPage
            // 
            this.serverTabPage.Controls.Add(this.serverPgnsDataGridView);
            this.serverTabPage.Controls.Add(this.addServerPgnsButton);
            this.serverTabPage.Location = new System.Drawing.Point(4, 22);
            this.serverTabPage.Name = "serverTabPage";
            this.serverTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.serverTabPage.Size = new System.Drawing.Size(650, 326);
            this.serverTabPage.TabIndex = 2;
            this.serverTabPage.Text = "Server";
            this.serverTabPage.UseVisualStyleBackColor = true;
            // 
            // serverPgnsDataGridView
            // 
            this.serverPgnsDataGridView.AllowUserToAddRows = false;
            this.serverPgnsDataGridView.AllowUserToResizeRows = false;
            this.serverPgnsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverPgnsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.serverPgnsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.serverPgnsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.serverPgnsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.serverPgnsDataGridView.Location = new System.Drawing.Point(6, 38);
            this.serverPgnsDataGridView.Name = "serverPgnsDataGridView";
            this.serverPgnsDataGridView.ReadOnly = true;
            this.serverPgnsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.serverPgnsDataGridView.Size = new System.Drawing.Size(638, 281);
            this.serverPgnsDataGridView.TabIndex = 4;
            this.tooltip.SetToolTip(this.serverPgnsDataGridView, "The list of PGN files to collect games from into Server category.");
            this.serverPgnsDataGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.ServerPgnsDataGridView_RowPrePaint);
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 85F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Path";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.FillWeight = 15F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Progress";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // addServerPgnsButton
            // 
            this.addServerPgnsButton.Location = new System.Drawing.Point(6, 6);
            this.addServerPgnsButton.Name = "addServerPgnsButton";
            this.addServerPgnsButton.Size = new System.Drawing.Size(110, 26);
            this.addServerPgnsButton.TabIndex = 3;
            this.addServerPgnsButton.Text = "Add Files";
            this.addServerPgnsButton.UseVisualStyleBackColor = true;
            this.addServerPgnsButton.Click += new System.EventHandler(this.AddServerPgnsButton_Click);
            // 
            // openCheckBox
            // 
            this.openCheckBox.AutoSize = true;
            this.openCheckBox.Checked = true;
            this.openCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.openCheckBox.Location = new System.Drawing.Point(179, 65);
            this.openCheckBox.Name = "openCheckBox";
            this.openCheckBox.Size = new System.Drawing.Size(120, 17);
            this.openCheckBox.TabIndex = 2;
            this.openCheckBox.Text = "Open when finished";
            this.tooltip.SetToolTip(this.openCheckBox, "When enabled the database will automatically open after the process is finished. " +
        "This will allow queries to be made instantly after closing this window.");
            this.openCheckBox.UseVisualStyleBackColor = true;
            this.openCheckBox.CheckedChanged += new System.EventHandler(this.OpenCheckBox_CheckedChanged);
            // 
            // buildButton
            // 
            this.buildButton.Location = new System.Drawing.Point(565, 135);
            this.buildButton.MaximumSize = new System.Drawing.Size(87, 74);
            this.buildButton.MinimumSize = new System.Drawing.Size(87, 74);
            this.buildButton.Name = "buildButton";
            this.buildButton.Size = new System.Drawing.Size(87, 74);
            this.buildButton.TabIndex = 1;
            this.buildButton.Text = "Build";
            this.tooltip.SetToolTip(this.buildButton, "Create the database.");
            this.buildButton.UseVisualStyleBackColor = true;
            this.buildButton.Click += new System.EventHandler(this.BuildButton_Click);
            // 
            // tooltip
            // 
            this.tooltip.AutomaticDelay = 200;
            this.tooltip.AutoPopDelay = 10000;
            this.tooltip.InitialDelay = 200;
            this.tooltip.ReshowDelay = 40;
            // 
            // tempDirsGroupBox
            // 
            this.tempDirsGroupBox.Controls.Add(this.panel1);
            this.tempDirsGroupBox.Controls.Add(this.maxTempStorageUsageCheckBox);
            this.tempDirsGroupBox.Controls.Add(this.tempStorageUsageUnitComboBox);
            this.tempDirsGroupBox.Controls.Add(this.tempStorageUsageSizeNumericUpDown);
            this.tempDirsGroupBox.Controls.Add(this.clearSecondaryTempFolderButton);
            this.tempDirsGroupBox.Controls.Add(this.secondaryTempFolderLabel);
            this.tempDirsGroupBox.Controls.Add(this.setSecondaryTempFolderButton);
            this.tempDirsGroupBox.Controls.Add(this.secondaryTempFolderTextBox);
            this.tempDirsGroupBox.Controls.Add(this.clearPrimaryTempFolderButton);
            this.tempDirsGroupBox.Controls.Add(this.primaryTempFolderLabel);
            this.tempDirsGroupBox.Controls.Add(this.setPrimaryTempFolderButton);
            this.tempDirsGroupBox.Controls.Add(this.primaryTempFolderTextBox);
            this.tempDirsGroupBox.Location = new System.Drawing.Point(3, 3);
            this.tempDirsGroupBox.MaximumSize = new System.Drawing.Size(649, 126);
            this.tempDirsGroupBox.MinimumSize = new System.Drawing.Size(649, 126);
            this.tempDirsGroupBox.Name = "tempDirsGroupBox";
            this.tempDirsGroupBox.Size = new System.Drawing.Size(649, 126);
            this.tempDirsGroupBox.TabIndex = 10;
            this.tempDirsGroupBox.TabStop = false;
            this.tempDirsGroupBox.Text = "Merge Settings";
            // 
            // maxTempStorageUsageCheckBox
            // 
            this.maxTempStorageUsageCheckBox.AutoSize = true;
            this.maxTempStorageUsageCheckBox.Location = new System.Drawing.Point(20, 101);
            this.maxTempStorageUsageCheckBox.Name = "maxTempStorageUsageCheckBox";
            this.maxTempStorageUsageCheckBox.Size = new System.Drawing.Size(154, 17);
            this.maxTempStorageUsageCheckBox.TabIndex = 20;
            this.maxTempStorageUsageCheckBox.Text = "Max. temp. storage usage: ";
            this.maxTempStorageUsageCheckBox.UseVisualStyleBackColor = true;
            // 
            // tempStorageUsageUnitComboBox
            // 
            this.tempStorageUsageUnitComboBox.FormattingEnabled = true;
            this.tempStorageUsageUnitComboBox.Items.AddRange(new object[] {
            "MB",
            "GB",
            "TB"});
            this.tempStorageUsageUnitComboBox.Location = new System.Drawing.Point(249, 99);
            this.tempStorageUsageUnitComboBox.Name = "tempStorageUsageUnitComboBox";
            this.tempStorageUsageUnitComboBox.Size = new System.Drawing.Size(57, 21);
            this.tempStorageUsageUnitComboBox.TabIndex = 18;
            // 
            // tempStorageUsageSizeNumericUpDown
            // 
            this.tempStorageUsageSizeNumericUpDown.DecimalPlaces = 1;
            this.tempStorageUsageSizeNumericUpDown.Location = new System.Drawing.Point(176, 100);
            this.tempStorageUsageSizeNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.tempStorageUsageSizeNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tempStorageUsageSizeNumericUpDown.Name = "tempStorageUsageSizeNumericUpDown";
            this.tempStorageUsageSizeNumericUpDown.Size = new System.Drawing.Size(67, 20);
            this.tempStorageUsageSizeNumericUpDown.TabIndex = 17;
            this.tempStorageUsageSizeNumericUpDown.ThousandsSeparator = true;
            this.tempStorageUsageSizeNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // clearSecondaryTempFolderButton
            // 
            this.clearSecondaryTempFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearSecondaryTempFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.clearSecondaryTempFolderButton.Location = new System.Drawing.Point(617, 74);
            this.clearSecondaryTempFolderButton.Name = "clearSecondaryTempFolderButton";
            this.clearSecondaryTempFolderButton.Size = new System.Drawing.Size(26, 19);
            this.clearSecondaryTempFolderButton.TabIndex = 16;
            this.clearSecondaryTempFolderButton.Text = "X";
            this.clearSecondaryTempFolderButton.UseVisualStyleBackColor = true;
            this.clearSecondaryTempFolderButton.Click += new System.EventHandler(this.clearSecondaryTempFolderButton_Click);
            // 
            // secondaryTempFolderLabel
            // 
            this.secondaryTempFolderLabel.AutoSize = true;
            this.secondaryTempFolderLabel.Location = new System.Drawing.Point(15, 77);
            this.secondaryTempFolderLabel.Name = "secondaryTempFolderLabel";
            this.secondaryTempFolderLabel.Size = new System.Drawing.Size(156, 13);
            this.secondaryTempFolderLabel.TabIndex = 15;
            this.secondaryTempFolderLabel.Text = "Secondary temporary directory: ";
            // 
            // setSecondaryTempFolderButton
            // 
            this.setSecondaryTempFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.setSecondaryTempFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.setSecondaryTempFolderButton.Location = new System.Drawing.Point(564, 74);
            this.setSecondaryTempFolderButton.Name = "setSecondaryTempFolderButton";
            this.setSecondaryTempFolderButton.Size = new System.Drawing.Size(47, 19);
            this.setSecondaryTempFolderButton.TabIndex = 14;
            this.setSecondaryTempFolderButton.Text = "...";
            this.setSecondaryTempFolderButton.UseVisualStyleBackColor = true;
            this.setSecondaryTempFolderButton.Click += new System.EventHandler(this.setSecondaryTempFolderButton_Click);
            // 
            // secondaryTempFolderTextBox
            // 
            this.secondaryTempFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.secondaryTempFolderTextBox.Location = new System.Drawing.Point(176, 74);
            this.secondaryTempFolderTextBox.Name = "secondaryTempFolderTextBox";
            this.secondaryTempFolderTextBox.ReadOnly = true;
            this.secondaryTempFolderTextBox.Size = new System.Drawing.Size(382, 20);
            this.secondaryTempFolderTextBox.TabIndex = 13;
            // 
            // clearPrimaryTempFolderButton
            // 
            this.clearPrimaryTempFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearPrimaryTempFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.clearPrimaryTempFolderButton.Location = new System.Drawing.Point(617, 48);
            this.clearPrimaryTempFolderButton.Name = "clearPrimaryTempFolderButton";
            this.clearPrimaryTempFolderButton.Size = new System.Drawing.Size(26, 19);
            this.clearPrimaryTempFolderButton.TabIndex = 12;
            this.clearPrimaryTempFolderButton.Text = "X";
            this.clearPrimaryTempFolderButton.UseVisualStyleBackColor = true;
            this.clearPrimaryTempFolderButton.Click += new System.EventHandler(this.clearPrimaryTempFolderButton_Click);
            // 
            // primaryTempFolderLabel
            // 
            this.primaryTempFolderLabel.AutoSize = true;
            this.primaryTempFolderLabel.Location = new System.Drawing.Point(31, 51);
            this.primaryTempFolderLabel.Name = "primaryTempFolderLabel";
            this.primaryTempFolderLabel.Size = new System.Drawing.Size(139, 13);
            this.primaryTempFolderLabel.TabIndex = 11;
            this.primaryTempFolderLabel.Text = "Primary temporary directory: ";
            // 
            // setPrimaryTempFolderButton
            // 
            this.setPrimaryTempFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.setPrimaryTempFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.setPrimaryTempFolderButton.Location = new System.Drawing.Point(564, 48);
            this.setPrimaryTempFolderButton.Name = "setPrimaryTempFolderButton";
            this.setPrimaryTempFolderButton.Size = new System.Drawing.Size(47, 19);
            this.setPrimaryTempFolderButton.TabIndex = 10;
            this.setPrimaryTempFolderButton.Text = "...";
            this.setPrimaryTempFolderButton.UseVisualStyleBackColor = true;
            this.setPrimaryTempFolderButton.Click += new System.EventHandler(this.setPrimaryTempFolderButton_Click);
            // 
            // primaryTempFolderTextBox
            // 
            this.primaryTempFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.primaryTempFolderTextBox.Location = new System.Drawing.Point(176, 48);
            this.primaryTempFolderTextBox.Name = "primaryTempFolderTextBox";
            this.primaryTempFolderTextBox.ReadOnly = true;
            this.primaryTempFolderTextBox.Size = new System.Drawing.Size(382, 20);
            this.primaryTempFolderTextBox.TabIndex = 9;
            // 
            // progressGroupBox
            // 
            this.progressGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.progressGroupBox.Controls.Add(this.mergeProgressLabelInfo);
            this.progressGroupBox.Controls.Add(this.importProgressLabelInfo);
            this.progressGroupBox.Controls.Add(this.mergeProgressLabel);
            this.progressGroupBox.Controls.Add(this.mergeProgressBar);
            this.progressGroupBox.Controls.Add(this.importProgressLabel);
            this.progressGroupBox.Controls.Add(this.importProgressBar);
            this.progressGroupBox.Location = new System.Drawing.Point(4, 135);
            this.progressGroupBox.MaximumSize = new System.Drawing.Size(555, 74);
            this.progressGroupBox.MinimumSize = new System.Drawing.Size(555, 74);
            this.progressGroupBox.Name = "progressGroupBox";
            this.progressGroupBox.Size = new System.Drawing.Size(555, 74);
            this.progressGroupBox.TabIndex = 12;
            this.progressGroupBox.TabStop = false;
            this.progressGroupBox.Text = "Progress";
            // 
            // mergeProgressLabelInfo
            // 
            this.mergeProgressLabelInfo.AutoSize = true;
            this.mergeProgressLabelInfo.Location = new System.Drawing.Point(85, 42);
            this.mergeProgressLabelInfo.Name = "mergeProgressLabelInfo";
            this.mergeProgressLabelInfo.Size = new System.Drawing.Size(86, 13);
            this.mergeProgressLabelInfo.TabIndex = 10;
            this.mergeProgressLabelInfo.Text = "Merge progress: ";
            // 
            // importProgressLabelInfo
            // 
            this.importProgressLabelInfo.AutoSize = true;
            this.importProgressLabelInfo.Location = new System.Drawing.Point(86, 20);
            this.importProgressLabelInfo.Name = "importProgressLabelInfo";
            this.importProgressLabelInfo.Size = new System.Drawing.Size(85, 13);
            this.importProgressLabelInfo.TabIndex = 9;
            this.importProgressLabelInfo.Text = "Import progress: ";
            // 
            // mergeProgressLabel
            // 
            this.mergeProgressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mergeProgressLabel.AutoSize = true;
            this.mergeProgressLabel.Location = new System.Drawing.Point(522, 42);
            this.mergeProgressLabel.Name = "mergeProgressLabel";
            this.mergeProgressLabel.Size = new System.Drawing.Size(21, 13);
            this.mergeProgressLabel.TabIndex = 8;
            this.mergeProgressLabel.Text = "0%";
            // 
            // mergeProgressBar
            // 
            this.mergeProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mergeProgressBar.Location = new System.Drawing.Point(175, 42);
            this.mergeProgressBar.Name = "mergeProgressBar";
            this.mergeProgressBar.Size = new System.Drawing.Size(341, 16);
            this.mergeProgressBar.TabIndex = 7;
            // 
            // importProgressLabel
            // 
            this.importProgressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.importProgressLabel.AutoSize = true;
            this.importProgressLabel.Location = new System.Drawing.Point(522, 20);
            this.importProgressLabel.Name = "importProgressLabel";
            this.importProgressLabel.Size = new System.Drawing.Size(21, 13);
            this.importProgressLabel.TabIndex = 6;
            this.importProgressLabel.Text = "0%";
            // 
            // importProgressBar
            // 
            this.importProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.importProgressBar.Location = new System.Drawing.Point(175, 19);
            this.importProgressBar.Name = "importProgressBar";
            this.importProgressBar.Size = new System.Drawing.Size(341, 17);
            this.importProgressBar.TabIndex = 5;
            // 
            // mergeAllAfterImportRadioButton
            // 
            this.mergeAllAfterImportRadioButton.AutoSize = true;
            this.mergeAllAfterImportRadioButton.Location = new System.Drawing.Point(91, 3);
            this.mergeAllAfterImportRadioButton.Name = "mergeAllAfterImportRadioButton";
            this.mergeAllAfterImportRadioButton.Size = new System.Drawing.Size(123, 17);
            this.mergeAllAfterImportRadioButton.TabIndex = 21;
            this.mergeAllAfterImportRadioButton.TabStop = true;
            this.mergeAllAfterImportRadioButton.Text = "Merge all after import";
            this.mergeAllAfterImportRadioButton.UseVisualStyleBackColor = true;
            this.mergeAllAfterImportRadioButton.CheckedChanged += new System.EventHandler(this.mergeAllAfterImportRadioButton_CheckedChanged);
            // 
            // dontMergeRadioButton
            // 
            this.dontMergeRadioButton.AutoSize = true;
            this.dontMergeRadioButton.Location = new System.Drawing.Point(3, 3);
            this.dontMergeRadioButton.Name = "dontMergeRadioButton";
            this.dontMergeRadioButton.Size = new System.Drawing.Size(82, 17);
            this.dontMergeRadioButton.TabIndex = 22;
            this.dontMergeRadioButton.TabStop = true;
            this.dontMergeRadioButton.Text = "Don\'t merge";
            this.dontMergeRadioButton.UseVisualStyleBackColor = true;
            this.dontMergeRadioButton.CheckedChanged += new System.EventHandler(this.dontMergeRadioButton_CheckedChanged);
            // 
            // mergeManuallyAfterImportRadioButton
            // 
            this.mergeManuallyAfterImportRadioButton.AutoSize = true;
            this.mergeManuallyAfterImportRadioButton.Location = new System.Drawing.Point(220, 3);
            this.mergeManuallyAfterImportRadioButton.Name = "mergeManuallyAfterImportRadioButton";
            this.mergeManuallyAfterImportRadioButton.Size = new System.Drawing.Size(154, 17);
            this.mergeManuallyAfterImportRadioButton.TabIndex = 23;
            this.mergeManuallyAfterImportRadioButton.TabStop = true;
            this.mergeManuallyAfterImportRadioButton.Text = "Merge manually after import";
            this.mergeManuallyAfterImportRadioButton.UseVisualStyleBackColor = true;
            this.mergeManuallyAfterImportRadioButton.CheckedChanged += new System.EventHandler(this.mergeManuallyAfterImportRadioButton_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mergeAllAfterImportRadioButton);
            this.panel1.Controls.Add(this.mergeManuallyAfterImportRadioButton);
            this.panel1.Controls.Add(this.dontMergeRadioButton);
            this.panel1.Location = new System.Drawing.Point(6, 19);
            this.panel1.MaximumSize = new System.Drawing.Size(637, 23);
            this.panel1.MinimumSize = new System.Drawing.Size(637, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(637, 23);
            this.panel1.TabIndex = 24;
            // 
            // importSettingsGroupBox
            // 
            this.importSettingsGroupBox.Controls.Add(this.openCheckBox);
            this.importSettingsGroupBox.Controls.Add(this.whatsDatabaseFormatButton);
            this.importSettingsGroupBox.Controls.Add(this.destinationFolderTextBox);
            this.importSettingsGroupBox.Controls.Add(this.databaseFormatLabel);
            this.importSettingsGroupBox.Controls.Add(this.setDestinationFolderButton);
            this.importSettingsGroupBox.Controls.Add(this.databaseFormatComboBox);
            this.importSettingsGroupBox.Controls.Add(this.destinationFolderLabel);
            this.importSettingsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importSettingsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.importSettingsGroupBox.Name = "importSettingsGroupBox";
            this.importSettingsGroupBox.Size = new System.Drawing.Size(658, 90);
            this.importSettingsGroupBox.TabIndex = 12;
            this.importSettingsGroupBox.TabStop = false;
            this.importSettingsGroupBox.Text = "Import Settings";
            // 
            // DatabaseCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 662);
            this.Controls.Add(this.splitContainer1);
            this.MaximumSize = new System.Drawing.Size(674, 700);
            this.MinimumSize = new System.Drawing.Size(674, 700);
            this.Name = "DatabaseCreationForm";
            this.Text = "Create database";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DatabaseCreationForm_FormClosing);
            this.Load += new System.EventHandler(this.DatabaseCreationForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.pgnPathsTabControl.ResumeLayout(false);
            this.humanTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.humanPgnsDataGridView)).EndInit();
            this.engineTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.enginePgnsDataGridView)).EndInit();
            this.serverTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.serverPgnsDataGridView)).EndInit();
            this.tempDirsGroupBox.ResumeLayout(false);
            this.tempDirsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tempStorageUsageSizeNumericUpDown)).EndInit();
            this.progressGroupBox.ResumeLayout(false);
            this.progressGroupBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.importSettingsGroupBox.ResumeLayout(false);
            this.importSettingsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label destinationFolderLabel;
        private System.Windows.Forms.Button setDestinationFolderButton;
        private System.Windows.Forms.TextBox destinationFolderTextBox;
        private System.Windows.Forms.Label databaseFormatLabel;
        private System.Windows.Forms.ComboBox databaseFormatComboBox;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl pgnPathsTabControl;
        private System.Windows.Forms.TabPage humanTabPage;
        private System.Windows.Forms.Button addHumanPgnsButton;
        private System.Windows.Forms.DataGridView humanPgnsDataGridView;
        private System.Windows.Forms.TabPage engineTabPage;
        private System.Windows.Forms.TabPage serverTabPage;
        private System.Windows.Forms.Button addEnginePgnsButton;
        private System.Windows.Forms.Button addServerPgnsButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Path;
        private System.Windows.Forms.DataGridViewTextBoxColumn Progress;
        private System.Windows.Forms.DataGridView enginePgnsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridView serverPgnsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Button buildButton;
        private System.Windows.Forms.CheckBox openCheckBox;
        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.Button whatsDatabaseFormatButton;
        private System.Windows.Forms.GroupBox tempDirsGroupBox;
        private System.Windows.Forms.CheckBox maxTempStorageUsageCheckBox;
        private System.Windows.Forms.ComboBox tempStorageUsageUnitComboBox;
        private System.Windows.Forms.NumericUpDown tempStorageUsageSizeNumericUpDown;
        private System.Windows.Forms.Button clearSecondaryTempFolderButton;
        private System.Windows.Forms.Label secondaryTempFolderLabel;
        private System.Windows.Forms.Button setSecondaryTempFolderButton;
        private System.Windows.Forms.TextBox secondaryTempFolderTextBox;
        private System.Windows.Forms.Button clearPrimaryTempFolderButton;
        private System.Windows.Forms.Label primaryTempFolderLabel;
        private System.Windows.Forms.Button setPrimaryTempFolderButton;
        private System.Windows.Forms.TextBox primaryTempFolderTextBox;
        private System.Windows.Forms.GroupBox importSettingsGroupBox;
        private System.Windows.Forms.GroupBox progressGroupBox;
        private System.Windows.Forms.Label mergeProgressLabelInfo;
        private System.Windows.Forms.Label importProgressLabelInfo;
        private System.Windows.Forms.Label mergeProgressLabel;
        private System.Windows.Forms.ProgressBar mergeProgressBar;
        private System.Windows.Forms.Label importProgressLabel;
        private System.Windows.Forms.ProgressBar importProgressBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton mergeAllAfterImportRadioButton;
        private System.Windows.Forms.RadioButton mergeManuallyAfterImportRadioButton;
        private System.Windows.Forms.RadioButton dontMergeRadioButton;
    }
}