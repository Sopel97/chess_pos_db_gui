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
            this.clearTempFolderButton = new System.Windows.Forms.Button();
            this.tempFolderLabel = new System.Windows.Forms.Label();
            this.destinationFolderLabel = new System.Windows.Forms.Label();
            this.setDestinationFolderButton = new System.Windows.Forms.Button();
            this.setTempFolderButton = new System.Windows.Forms.Button();
            this.tempFolderTextBox = new System.Windows.Forms.TextBox();
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
            this.mergeProgressLabel = new System.Windows.Forms.Label();
            this.mergeProgressBar = new System.Windows.Forms.ProgressBar();
            this.openCheckBox = new System.Windows.Forms.CheckBox();
            this.buildButton = new System.Windows.Forms.Button();
            this.mergeCheckBox = new System.Windows.Forms.CheckBox();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
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
            this.splitContainer1.Panel1.Controls.Add(this.whatsDatabaseFormatButton);
            this.splitContainer1.Panel1.Controls.Add(this.databaseFormatLabel);
            this.splitContainer1.Panel1.Controls.Add(this.databaseFormatComboBox);
            this.splitContainer1.Panel1.Controls.Add(this.clearTempFolderButton);
            this.splitContainer1.Panel1.Controls.Add(this.tempFolderLabel);
            this.splitContainer1.Panel1.Controls.Add(this.destinationFolderLabel);
            this.splitContainer1.Panel1.Controls.Add(this.setDestinationFolderButton);
            this.splitContainer1.Panel1.Controls.Add(this.setTempFolderButton);
            this.splitContainer1.Panel1.Controls.Add(this.tempFolderTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.destinationFolderTextBox);
            this.splitContainer1.Panel1MinSize = 90;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(624, 602);
            this.splitContainer1.SplitterDistance = 90;
            this.splitContainer1.TabIndex = 0;
            // 
            // whatsDatabaseFormatButton
            // 
            this.whatsDatabaseFormatButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.whatsDatabaseFormatButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.whatsDatabaseFormatButton.Location = new System.Drawing.Point(541, 63);
            this.whatsDatabaseFormatButton.Name = "whatsDatabaseFormatButton";
            this.whatsDatabaseFormatButton.Size = new System.Drawing.Size(79, 23);
            this.whatsDatabaseFormatButton.TabIndex = 11;
            this.whatsDatabaseFormatButton.Text = "What\'s this?";
            this.whatsDatabaseFormatButton.UseVisualStyleBackColor = true;
            this.whatsDatabaseFormatButton.Click += new System.EventHandler(this.WhatsDatabaseFormatButton_Click);
            // 
            // databaseFormatLabel
            // 
            this.databaseFormatLabel.AutoSize = true;
            this.databaseFormatLabel.Location = new System.Drawing.Point(4, 68);
            this.databaseFormatLabel.Name = "databaseFormatLabel";
            this.databaseFormatLabel.Size = new System.Drawing.Size(85, 13);
            this.databaseFormatLabel.TabIndex = 10;
            this.databaseFormatLabel.Text = "Database format";
            this.tooltip.SetToolTip(this.databaseFormatLabel, "The database format to use. Descriptions can be found on project\'s github page in" +
        " the documentation. Default is best for uninformed users.");
            // 
            // databaseFormatComboBox
            // 
            this.databaseFormatComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.databaseFormatComboBox.FormattingEnabled = true;
            this.databaseFormatComboBox.Location = new System.Drawing.Point(95, 65);
            this.databaseFormatComboBox.Name = "databaseFormatComboBox";
            this.databaseFormatComboBox.Size = new System.Drawing.Size(440, 21);
            this.databaseFormatComboBox.TabIndex = 9;
            this.databaseFormatComboBox.SelectedValueChanged += new System.EventHandler(this.DatabaseFormatComboBox_SelectedValueChanged);
            // 
            // clearTempFolderButton
            // 
            this.clearTempFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearTempFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.clearTempFolderButton.Location = new System.Drawing.Point(594, 38);
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
            this.tempFolderLabel.Location = new System.Drawing.Point(26, 41);
            this.tempFolderLabel.Name = "tempFolderLabel";
            this.tempFolderLabel.Size = new System.Drawing.Size(63, 13);
            this.tempFolderLabel.TabIndex = 7;
            this.tempFolderLabel.Text = "Temp folder";
            this.tooltip.SetToolTip(this.tempFolderLabel, "The temporary folder used for intermediate when creating the database. Best to be" +
        " a separate phsical drive. If left empty then no temporary folder will be used b" +
        "ut it may slow down the process.");
            // 
            // destinationFolderLabel
            // 
            this.destinationFolderLabel.AutoSize = true;
            this.destinationFolderLabel.Location = new System.Drawing.Point(0, 15);
            this.destinationFolderLabel.Name = "destinationFolderLabel";
            this.destinationFolderLabel.Size = new System.Drawing.Size(89, 13);
            this.destinationFolderLabel.TabIndex = 6;
            this.destinationFolderLabel.Text = "Destination folder";
            this.tooltip.SetToolTip(this.destinationFolderLabel, "The folder in which the database will be created. The folder must exist.");
            // 
            // setDestinationFolderButton
            // 
            this.setDestinationFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.setDestinationFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.setDestinationFolderButton.Location = new System.Drawing.Point(541, 12);
            this.setDestinationFolderButton.Name = "setDestinationFolderButton";
            this.setDestinationFolderButton.Size = new System.Drawing.Size(47, 19);
            this.setDestinationFolderButton.TabIndex = 5;
            this.setDestinationFolderButton.Text = "...";
            this.setDestinationFolderButton.UseVisualStyleBackColor = true;
            this.setDestinationFolderButton.Click += new System.EventHandler(this.SetDestinationFolderButton_Click);
            // 
            // setTempFolderButton
            // 
            this.setTempFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.setTempFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.setTempFolderButton.Location = new System.Drawing.Point(541, 38);
            this.setTempFolderButton.Name = "setTempFolderButton";
            this.setTempFolderButton.Size = new System.Drawing.Size(47, 19);
            this.setTempFolderButton.TabIndex = 4;
            this.setTempFolderButton.Text = "...";
            this.setTempFolderButton.UseVisualStyleBackColor = true;
            this.setTempFolderButton.Click += new System.EventHandler(this.SetTempFolderButton_Click);
            // 
            // tempFolderTextBox
            // 
            this.tempFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tempFolderTextBox.Location = new System.Drawing.Point(95, 38);
            this.tempFolderTextBox.Name = "tempFolderTextBox";
            this.tempFolderTextBox.ReadOnly = true;
            this.tempFolderTextBox.Size = new System.Drawing.Size(440, 20);
            this.tempFolderTextBox.TabIndex = 2;
            // 
            // destinationFolderTextBox
            // 
            this.destinationFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.destinationFolderTextBox.Location = new System.Drawing.Point(95, 12);
            this.destinationFolderTextBox.Name = "destinationFolderTextBox";
            this.destinationFolderTextBox.ReadOnly = true;
            this.destinationFolderTextBox.Size = new System.Drawing.Size(440, 20);
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
            this.splitContainer2.Panel2.Controls.Add(this.mergeProgressLabel);
            this.splitContainer2.Panel2.Controls.Add(this.mergeProgressBar);
            this.splitContainer2.Panel2.Controls.Add(this.openCheckBox);
            this.splitContainer2.Panel2.Controls.Add(this.buildButton);
            this.splitContainer2.Panel2.Controls.Add(this.mergeCheckBox);
            this.splitContainer2.Size = new System.Drawing.Size(624, 508);
            this.splitContainer2.SplitterDistance = 406;
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
            this.pgnPathsTabControl.Size = new System.Drawing.Size(624, 406);
            this.pgnPathsTabControl.TabIndex = 0;
            // 
            // humanTabPage
            // 
            this.humanTabPage.Controls.Add(this.addHumanPgnsButton);
            this.humanTabPage.Controls.Add(this.humanPgnsDataGridView);
            this.humanTabPage.Location = new System.Drawing.Point(4, 22);
            this.humanTabPage.Name = "humanTabPage";
            this.humanTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.humanTabPage.Size = new System.Drawing.Size(616, 380);
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
            this.humanPgnsDataGridView.Size = new System.Drawing.Size(604, 335);
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
            this.engineTabPage.Size = new System.Drawing.Size(616, 380);
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
            this.enginePgnsDataGridView.Size = new System.Drawing.Size(604, 335);
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
            this.serverTabPage.Size = new System.Drawing.Size(616, 380);
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
            this.serverPgnsDataGridView.Size = new System.Drawing.Size(604, 335);
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
            // mergeProgressLabel
            // 
            this.mergeProgressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mergeProgressLabel.AutoSize = true;
            this.mergeProgressLabel.Location = new System.Drawing.Point(455, 13);
            this.mergeProgressLabel.Name = "mergeProgressLabel";
            this.mergeProgressLabel.Size = new System.Drawing.Size(21, 13);
            this.mergeProgressLabel.TabIndex = 4;
            this.mergeProgressLabel.Text = "0%";
            // 
            // mergeProgressBar
            // 
            this.mergeProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mergeProgressBar.Location = new System.Drawing.Point(129, 12);
            this.mergeProgressBar.Name = "mergeProgressBar";
            this.mergeProgressBar.Size = new System.Drawing.Size(320, 17);
            this.mergeProgressBar.TabIndex = 3;
            this.tooltip.SetToolTip(this.mergeProgressBar, "Merge (optimization) progress");
            // 
            // openCheckBox
            // 
            this.openCheckBox.AutoSize = true;
            this.openCheckBox.Checked = true;
            this.openCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.openCheckBox.Location = new System.Drawing.Point(12, 35);
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
            this.buildButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.buildButton.Location = new System.Drawing.Point(496, 0);
            this.buildButton.Name = "buildButton";
            this.buildButton.Size = new System.Drawing.Size(128, 98);
            this.buildButton.TabIndex = 1;
            this.buildButton.Text = "Build";
            this.tooltip.SetToolTip(this.buildButton, "Create the database.");
            this.buildButton.UseVisualStyleBackColor = true;
            this.buildButton.Click += new System.EventHandler(this.BuildButton_Click);
            // 
            // mergeCheckBox
            // 
            this.mergeCheckBox.AutoSize = true;
            this.mergeCheckBox.Checked = true;
            this.mergeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mergeCheckBox.Location = new System.Drawing.Point(12, 12);
            this.mergeCheckBox.Name = "mergeCheckBox";
            this.mergeCheckBox.Size = new System.Drawing.Size(111, 17);
            this.mergeCheckBox.TabIndex = 0;
            this.mergeCheckBox.Text = "Merge after import";
            this.tooltip.SetToolTip(this.mergeCheckBox, "When enabled the database will optimize itself before completion.");
            this.mergeCheckBox.UseVisualStyleBackColor = true;
            // 
            // tooltip
            // 
            this.tooltip.AutomaticDelay = 200;
            this.tooltip.AutoPopDelay = 10000;
            this.tooltip.InitialDelay = 200;
            this.tooltip.ReshowDelay = 40;
            // 
            // DatabaseCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 602);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "DatabaseCreationForm";
            this.Text = "Create database";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DatabaseCreationForm_FormClosing);
            this.Load += new System.EventHandler(this.DatabaseCreationForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.pgnPathsTabControl.ResumeLayout(false);
            this.humanTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.humanPgnsDataGridView)).EndInit();
            this.engineTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.enginePgnsDataGridView)).EndInit();
            this.serverTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.serverPgnsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button clearTempFolderButton;
        private System.Windows.Forms.Label tempFolderLabel;
        private System.Windows.Forms.Label destinationFolderLabel;
        private System.Windows.Forms.Button setDestinationFolderButton;
        private System.Windows.Forms.Button setTempFolderButton;
        private System.Windows.Forms.TextBox tempFolderTextBox;
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
        private System.Windows.Forms.CheckBox mergeCheckBox;
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
        private System.Windows.Forms.ProgressBar mergeProgressBar;
        private System.Windows.Forms.Label mergeProgressLabel;
        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.Button whatsDatabaseFormatButton;
    }
}