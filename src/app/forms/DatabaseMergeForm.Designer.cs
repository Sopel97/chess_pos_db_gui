namespace chess_pos_db_gui.src.app.forms
{
    partial class DatabaseMergeForm
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
            this.addButton = new System.Windows.Forms.Button();
            this.unassignedEntriesView = new System.Windows.Forms.ListView();
            this.makeGroupButton = new System.Windows.Forms.Button();
            this.entryGroupsView = new System.Windows.Forms.ListView();
            this.removeButton = new System.Windows.Forms.Button();
            this.filesGroupBox = new System.Windows.Forms.GroupBox();
            this.estimatedNumberOfFilesAfterMergingLabel = new System.Windows.Forms.Label();
            this.initialNumberOfFilesLabel = new System.Windows.Forms.Label();
            this.partitionComboBox = new System.Windows.Forms.ComboBox();
            this.partitionNameLabel = new System.Windows.Forms.Label();
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
            this.currentOperationInfoLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.totalMergeProgressLabelInfo = new System.Windows.Forms.Label();
            this.subtotalMargeProgressLabelInfo = new System.Windows.Forms.Label();
            this.totalMergeProgressLabel = new System.Windows.Forms.Label();
            this.totalMergeProgressBar = new System.Windows.Forms.ProgressBar();
            this.subtotalMergeProgressLabel = new System.Windows.Forms.Label();
            this.subtotalMergeProgressBar = new System.Windows.Forms.ProgressBar();
            this.startButton = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.filesGroupBox.SuspendLayout();
            this.tempDirsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tempStorageUsageSizeNumericUpDown)).BeginInit();
            this.progressGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(312, 19);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(25, 71);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // unassignedEntriesView
            // 
            this.unassignedEntriesView.AllowDrop = true;
            this.unassignedEntriesView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.unassignedEntriesView.FullRowSelect = true;
            this.unassignedEntriesView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.unassignedEntriesView.HideSelection = false;
            this.unassignedEntriesView.Location = new System.Drawing.Point(6, 19);
            this.unassignedEntriesView.Name = "unassignedEntriesView";
            this.unassignedEntriesView.Size = new System.Drawing.Size(300, 309);
            this.unassignedEntriesView.TabIndex = 2;
            this.unassignedEntriesView.UseCompatibleStateImageBehavior = false;
            this.unassignedEntriesView.View = System.Windows.Forms.View.Details;
            this.unassignedEntriesView.VirtualMode = true;
            this.unassignedEntriesView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.unassignedEntriesView_ItemDrag);
            this.unassignedEntriesView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.unassignedEntriesView_RetrieveVirtualItem);
            this.unassignedEntriesView.DragDrop += new System.Windows.Forms.DragEventHandler(this.unassignedEntriesView_DragDrop);
            this.unassignedEntriesView.DragEnter += new System.Windows.Forms.DragEventHandler(this.unassignedEntriesView_DragEnter);
            // 
            // makeGroupButton
            // 
            this.makeGroupButton.Location = new System.Drawing.Point(312, 127);
            this.makeGroupButton.Name = "makeGroupButton";
            this.makeGroupButton.Size = new System.Drawing.Size(25, 43);
            this.makeGroupButton.TabIndex = 3;
            this.makeGroupButton.Text = ">";
            this.makeGroupButton.UseVisualStyleBackColor = true;
            this.makeGroupButton.Click += new System.EventHandler(this.makeGroupButton_Click);
            // 
            // entryGroupsView
            // 
            this.entryGroupsView.AllowDrop = true;
            this.entryGroupsView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.entryGroupsView.FullRowSelect = true;
            this.entryGroupsView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.entryGroupsView.HideSelection = false;
            this.entryGroupsView.Location = new System.Drawing.Point(343, 19);
            this.entryGroupsView.Name = "entryGroupsView";
            this.entryGroupsView.Size = new System.Drawing.Size(300, 309);
            this.entryGroupsView.TabIndex = 4;
            this.entryGroupsView.UseCompatibleStateImageBehavior = false;
            this.entryGroupsView.View = System.Windows.Forms.View.Details;
            this.entryGroupsView.VirtualMode = true;
            this.entryGroupsView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.entryGroupsView_ItemDrag);
            this.entryGroupsView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.entryGroupsView_RetrieveVirtualItem);
            this.entryGroupsView.SelectedIndexChanged += new System.EventHandler(this.entryGroupsView_SelectedIndexChanged);
            this.entryGroupsView.VirtualItemsSelectionRangeChanged += new System.Windows.Forms.ListViewVirtualItemsSelectionRangeChangedEventHandler(this.entryGroupsView_VirtualItemsSelectionRangeChanged);
            this.entryGroupsView.DragDrop += new System.Windows.Forms.DragEventHandler(this.entryGroupsView_DragDrop);
            this.entryGroupsView.DragEnter += new System.Windows.Forms.DragEventHandler(this.entryGroupsView_DragEnter);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(312, 176);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(25, 43);
            this.removeButton.TabIndex = 5;
            this.removeButton.Text = "<";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // filesGroupBox
            // 
            this.filesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filesGroupBox.Controls.Add(this.estimatedNumberOfFilesAfterMergingLabel);
            this.filesGroupBox.Controls.Add(this.initialNumberOfFilesLabel);
            this.filesGroupBox.Controls.Add(this.unassignedEntriesView);
            this.filesGroupBox.Controls.Add(this.entryGroupsView);
            this.filesGroupBox.Controls.Add(this.removeButton);
            this.filesGroupBox.Controls.Add(this.addButton);
            this.filesGroupBox.Controls.Add(this.makeGroupButton);
            this.filesGroupBox.Location = new System.Drawing.Point(4, 33);
            this.filesGroupBox.Name = "filesGroupBox";
            this.filesGroupBox.Size = new System.Drawing.Size(648, 350);
            this.filesGroupBox.TabIndex = 6;
            this.filesGroupBox.TabStop = false;
            this.filesGroupBox.Text = "Files";
            // 
            // estimatedNumberOfFilesAfterMergingLabel
            // 
            this.estimatedNumberOfFilesAfterMergingLabel.AutoSize = true;
            this.estimatedNumberOfFilesAfterMergingLabel.Location = new System.Drawing.Point(340, 334);
            this.estimatedNumberOfFilesAfterMergingLabel.Name = "estimatedNumberOfFilesAfterMergingLabel";
            this.estimatedNumberOfFilesAfterMergingLabel.Size = new System.Drawing.Size(194, 13);
            this.estimatedNumberOfFilesAfterMergingLabel.TabIndex = 7;
            this.estimatedNumberOfFilesAfterMergingLabel.Text = "Estimated number of files after merging: ";
            // 
            // initialNumberOfFilesLabel
            // 
            this.initialNumberOfFilesLabel.AutoSize = true;
            this.initialNumberOfFilesLabel.Location = new System.Drawing.Point(6, 334);
            this.initialNumberOfFilesLabel.Name = "initialNumberOfFilesLabel";
            this.initialNumberOfFilesLabel.Size = new System.Drawing.Size(108, 13);
            this.initialNumberOfFilesLabel.TabIndex = 6;
            this.initialNumberOfFilesLabel.Text = "Initial number of files: ";
            // 
            // partitionComboBox
            // 
            this.partitionComboBox.FormattingEnabled = true;
            this.partitionComboBox.Location = new System.Drawing.Point(87, 6);
            this.partitionComboBox.Name = "partitionComboBox";
            this.partitionComboBox.Size = new System.Drawing.Size(223, 21);
            this.partitionComboBox.TabIndex = 7;
            this.partitionComboBox.SelectedIndexChanged += new System.EventHandler(this.partitionComboBox_SelectedIndexChanged);
            // 
            // partitionNameLabel
            // 
            this.partitionNameLabel.AutoSize = true;
            this.partitionNameLabel.Location = new System.Drawing.Point(4, 9);
            this.partitionNameLabel.Name = "partitionNameLabel";
            this.partitionNameLabel.Size = new System.Drawing.Size(77, 13);
            this.partitionNameLabel.TabIndex = 8;
            this.partitionNameLabel.Text = "Partition name:";
            // 
            // tempDirsGroupBox
            // 
            this.tempDirsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.tempDirsGroupBox.MaximumSize = new System.Drawing.Size(649, 99);
            this.tempDirsGroupBox.MinimumSize = new System.Drawing.Size(649, 99);
            this.tempDirsGroupBox.Name = "tempDirsGroupBox";
            this.tempDirsGroupBox.Size = new System.Drawing.Size(649, 99);
            this.tempDirsGroupBox.TabIndex = 9;
            this.tempDirsGroupBox.TabStop = false;
            this.tempDirsGroupBox.Text = "Temporary Directories";
            // 
            // maxTempStorageUsageCheckBox
            // 
            this.maxTempStorageUsageCheckBox.AutoSize = true;
            this.maxTempStorageUsageCheckBox.Location = new System.Drawing.Point(20, 72);
            this.maxTempStorageUsageCheckBox.Name = "maxTempStorageUsageCheckBox";
            this.maxTempStorageUsageCheckBox.Size = new System.Drawing.Size(154, 17);
            this.maxTempStorageUsageCheckBox.TabIndex = 20;
            this.maxTempStorageUsageCheckBox.Text = "Max. temp. storage usage: ";
            this.maxTempStorageUsageCheckBox.UseVisualStyleBackColor = true;
            this.maxTempStorageUsageCheckBox.CheckedChanged += new System.EventHandler(this.maxTempStorageUsageCheckBox_CheckedChanged);
            // 
            // tempStorageUsageUnitComboBox
            // 
            this.tempStorageUsageUnitComboBox.FormattingEnabled = true;
            this.tempStorageUsageUnitComboBox.Items.AddRange(new object[] {
            "MB",
            "GB",
            "TB"});
            this.tempStorageUsageUnitComboBox.Location = new System.Drawing.Point(249, 70);
            this.tempStorageUsageUnitComboBox.Name = "tempStorageUsageUnitComboBox";
            this.tempStorageUsageUnitComboBox.Size = new System.Drawing.Size(57, 21);
            this.tempStorageUsageUnitComboBox.TabIndex = 18;
            this.tempStorageUsageUnitComboBox.SelectedIndexChanged += new System.EventHandler(this.tempStorageUsageUnitComboBox_SelectedIndexChanged);
            // 
            // tempStorageUsageSizeNumericUpDown
            // 
            this.tempStorageUsageSizeNumericUpDown.DecimalPlaces = 1;
            this.tempStorageUsageSizeNumericUpDown.Location = new System.Drawing.Point(176, 71);
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
            this.tempStorageUsageSizeNumericUpDown.ValueChanged += new System.EventHandler(this.tempStorageUsageSizeNumericUpDown_ValueChanged);
            // 
            // clearSecondaryTempFolderButton
            // 
            this.clearSecondaryTempFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearSecondaryTempFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.clearSecondaryTempFolderButton.Location = new System.Drawing.Point(617, 45);
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
            this.secondaryTempFolderLabel.Location = new System.Drawing.Point(15, 48);
            this.secondaryTempFolderLabel.Name = "secondaryTempFolderLabel";
            this.secondaryTempFolderLabel.Size = new System.Drawing.Size(156, 13);
            this.secondaryTempFolderLabel.TabIndex = 15;
            this.secondaryTempFolderLabel.Text = "Secondary temporary directory: ";
            // 
            // setSecondaryTempFolderButton
            // 
            this.setSecondaryTempFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.setSecondaryTempFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.setSecondaryTempFolderButton.Location = new System.Drawing.Point(564, 45);
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
            this.secondaryTempFolderTextBox.Location = new System.Drawing.Point(176, 45);
            this.secondaryTempFolderTextBox.Name = "secondaryTempFolderTextBox";
            this.secondaryTempFolderTextBox.ReadOnly = true;
            this.secondaryTempFolderTextBox.Size = new System.Drawing.Size(382, 20);
            this.secondaryTempFolderTextBox.TabIndex = 13;
            // 
            // clearPrimaryTempFolderButton
            // 
            this.clearPrimaryTempFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearPrimaryTempFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.clearPrimaryTempFolderButton.Location = new System.Drawing.Point(617, 19);
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
            this.primaryTempFolderLabel.Location = new System.Drawing.Point(31, 22);
            this.primaryTempFolderLabel.Name = "primaryTempFolderLabel";
            this.primaryTempFolderLabel.Size = new System.Drawing.Size(139, 13);
            this.primaryTempFolderLabel.TabIndex = 11;
            this.primaryTempFolderLabel.Text = "Primary temporary directory: ";
            // 
            // setPrimaryTempFolderButton
            // 
            this.setPrimaryTempFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.setPrimaryTempFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.setPrimaryTempFolderButton.Location = new System.Drawing.Point(564, 19);
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
            this.primaryTempFolderTextBox.Location = new System.Drawing.Point(176, 19);
            this.primaryTempFolderTextBox.Name = "primaryTempFolderTextBox";
            this.primaryTempFolderTextBox.ReadOnly = true;
            this.primaryTempFolderTextBox.Size = new System.Drawing.Size(382, 20);
            this.primaryTempFolderTextBox.TabIndex = 9;
            // 
            // progressGroupBox
            // 
            this.progressGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.progressGroupBox.Controls.Add(this.currentOperationInfoLabel);
            this.progressGroupBox.Controls.Add(this.label2);
            this.progressGroupBox.Controls.Add(this.totalMergeProgressLabelInfo);
            this.progressGroupBox.Controls.Add(this.subtotalMargeProgressLabelInfo);
            this.progressGroupBox.Controls.Add(this.totalMergeProgressLabel);
            this.progressGroupBox.Controls.Add(this.totalMergeProgressBar);
            this.progressGroupBox.Controls.Add(this.subtotalMergeProgressLabel);
            this.progressGroupBox.Controls.Add(this.subtotalMergeProgressBar);
            this.progressGroupBox.Location = new System.Drawing.Point(6, 108);
            this.progressGroupBox.MaximumSize = new System.Drawing.Size(555, 92);
            this.progressGroupBox.MinimumSize = new System.Drawing.Size(555, 92);
            this.progressGroupBox.Name = "progressGroupBox";
            this.progressGroupBox.Size = new System.Drawing.Size(555, 92);
            this.progressGroupBox.TabIndex = 10;
            this.progressGroupBox.TabStop = false;
            this.progressGroupBox.Text = "Progress";
            // 
            // currentOperationInfoLabel
            // 
            this.currentOperationInfoLabel.AutoSize = true;
            this.currentOperationInfoLabel.Location = new System.Drawing.Point(170, 16);
            this.currentOperationInfoLabel.Name = "currentOperationInfoLabel";
            this.currentOperationInfoLabel.Size = new System.Drawing.Size(244, 13);
            this.currentOperationInfoLabel.TabIndex = 12;
            this.currentOperationInfoLabel.Text = "Merge A out of B: Merging N files with total size M.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Current operation: ";
            // 
            // totalMergeProgressLabelInfo
            // 
            this.totalMergeProgressLabelInfo.AutoSize = true;
            this.totalMergeProgressLabelInfo.Location = new System.Drawing.Point(42, 65);
            this.totalMergeProgressLabelInfo.Name = "totalMergeProgressLabelInfo";
            this.totalMergeProgressLabelInfo.Size = new System.Drawing.Size(125, 13);
            this.totalMergeProgressLabelInfo.TabIndex = 10;
            this.totalMergeProgressLabelInfo.Text = "Estimated total progress: ";
            // 
            // subtotalMargeProgressLabelInfo
            // 
            this.subtotalMargeProgressLabelInfo.AutoSize = true;
            this.subtotalMargeProgressLabelInfo.Location = new System.Drawing.Point(30, 33);
            this.subtotalMargeProgressLabelInfo.Name = "subtotalMargeProgressLabelInfo";
            this.subtotalMargeProgressLabelInfo.Size = new System.Drawing.Size(137, 13);
            this.subtotalMargeProgressLabelInfo.TabIndex = 9;
            this.subtotalMargeProgressLabelInfo.Text = "Current operation progress: ";
            // 
            // totalMergeProgressLabel
            // 
            this.totalMergeProgressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.totalMergeProgressLabel.AutoSize = true;
            this.totalMergeProgressLabel.Location = new System.Drawing.Point(520, 65);
            this.totalMergeProgressLabel.Name = "totalMergeProgressLabel";
            this.totalMergeProgressLabel.Size = new System.Drawing.Size(21, 13);
            this.totalMergeProgressLabel.TabIndex = 8;
            this.totalMergeProgressLabel.Text = "0%";
            // 
            // totalMergeProgressBar
            // 
            this.totalMergeProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.totalMergeProgressBar.Location = new System.Drawing.Point(173, 65);
            this.totalMergeProgressBar.Name = "totalMergeProgressBar";
            this.totalMergeProgressBar.Size = new System.Drawing.Size(341, 16);
            this.totalMergeProgressBar.TabIndex = 7;
            // 
            // subtotalMergeProgressLabel
            // 
            this.subtotalMergeProgressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.subtotalMergeProgressLabel.AutoSize = true;
            this.subtotalMergeProgressLabel.Location = new System.Drawing.Point(520, 33);
            this.subtotalMergeProgressLabel.Name = "subtotalMergeProgressLabel";
            this.subtotalMergeProgressLabel.Size = new System.Drawing.Size(21, 13);
            this.subtotalMergeProgressLabel.TabIndex = 6;
            this.subtotalMergeProgressLabel.Text = "0%";
            // 
            // subtotalMergeProgressBar
            // 
            this.subtotalMergeProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subtotalMergeProgressBar.Location = new System.Drawing.Point(173, 32);
            this.subtotalMergeProgressBar.Name = "subtotalMergeProgressBar";
            this.subtotalMergeProgressBar.Size = new System.Drawing.Size(341, 17);
            this.subtotalMergeProgressBar.TabIndex = 5;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(567, 108);
            this.startButton.MaximumSize = new System.Drawing.Size(85, 92);
            this.startButton.MinimumSize = new System.Drawing.Size(85, 92);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(85, 92);
            this.startButton.TabIndex = 11;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.filesGroupBox);
            this.splitContainer.Panel1.Controls.Add(this.partitionComboBox);
            this.splitContainer.Panel1.Controls.Add(this.partitionNameLabel);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tempDirsGroupBox);
            this.splitContainer.Panel2.Controls.Add(this.startButton);
            this.splitContainer.Panel2.Controls.Add(this.progressGroupBox);
            this.splitContainer.Size = new System.Drawing.Size(658, 596);
            this.splitContainer.SplitterDistance = 386;
            this.splitContainer.TabIndex = 12;
            // 
            // DatabaseMergeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 596);
            this.Controls.Add(this.splitContainer);
            this.MaximumSize = new System.Drawing.Size(674, 9999);
            this.MinimumSize = new System.Drawing.Size(674, 600);
            this.Name = "DatabaseMergeForm";
            this.Text = "Manual Database File Merging";
            this.filesGroupBox.ResumeLayout(false);
            this.filesGroupBox.PerformLayout();
            this.tempDirsGroupBox.ResumeLayout(false);
            this.tempDirsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tempStorageUsageSizeNumericUpDown)).EndInit();
            this.progressGroupBox.ResumeLayout(false);
            this.progressGroupBox.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.ListView unassignedEntriesView;
        private System.Windows.Forms.Button makeGroupButton;
        private System.Windows.Forms.ListView entryGroupsView;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.GroupBox filesGroupBox;
        private System.Windows.Forms.ComboBox partitionComboBox;
        private System.Windows.Forms.Label partitionNameLabel;
        private System.Windows.Forms.GroupBox tempDirsGroupBox;
        private System.Windows.Forms.Button clearPrimaryTempFolderButton;
        private System.Windows.Forms.Label primaryTempFolderLabel;
        private System.Windows.Forms.Button setPrimaryTempFolderButton;
        private System.Windows.Forms.TextBox primaryTempFolderTextBox;
        private System.Windows.Forms.ComboBox tempStorageUsageUnitComboBox;
        private System.Windows.Forms.NumericUpDown tempStorageUsageSizeNumericUpDown;
        private System.Windows.Forms.Button clearSecondaryTempFolderButton;
        private System.Windows.Forms.Label secondaryTempFolderLabel;
        private System.Windows.Forms.Button setSecondaryTempFolderButton;
        private System.Windows.Forms.TextBox secondaryTempFolderTextBox;
        private System.Windows.Forms.GroupBox progressGroupBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label totalMergeProgressLabel;
        private System.Windows.Forms.ProgressBar totalMergeProgressBar;
        private System.Windows.Forms.Label subtotalMergeProgressLabel;
        private System.Windows.Forms.ProgressBar subtotalMergeProgressBar;
        private System.Windows.Forms.Label estimatedNumberOfFilesAfterMergingLabel;
        private System.Windows.Forms.Label initialNumberOfFilesLabel;
        private System.Windows.Forms.Label currentOperationInfoLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label totalMergeProgressLabelInfo;
        private System.Windows.Forms.Label subtotalMargeProgressLabelInfo;
        private System.Windows.Forms.CheckBox maxTempStorageUsageCheckBox;
    }
}