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
            this.partitionComboBox = new System.Windows.Forms.ComboBox();
            this.partitionNameLabel = new System.Windows.Forms.Label();
            this.tempDirsGroupBox = new System.Windows.Forms.GroupBox();
            this.maxTempStorageUsageLabel = new System.Windows.Forms.Label();
            this.tempStorageUsageUnitComboBox = new System.Windows.Forms.ComboBox();
            this.tempStorageUsageSizeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.secondaryTempFolderTextBox = new System.Windows.Forms.TextBox();
            this.clearTempFolderButton = new System.Windows.Forms.Button();
            this.tempFolderLabel = new System.Windows.Forms.Label();
            this.setTempFolderButton = new System.Windows.Forms.Button();
            this.primaryTempFolderTextBox = new System.Windows.Forms.TextBox();
            this.progressGroupBox = new System.Windows.Forms.GroupBox();
            this.startButton = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.filesGroupBox.SuspendLayout();
            this.tempDirsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tempStorageUsageSizeNumericUpDown)).BeginInit();
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
            this.unassignedEntriesView.Size = new System.Drawing.Size(300, 325);
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
            this.entryGroupsView.Size = new System.Drawing.Size(300, 325);
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
            // partitionComboBox
            // 
            this.partitionComboBox.FormattingEnabled = true;
            this.partitionComboBox.Location = new System.Drawing.Point(87, 6);
            this.partitionComboBox.Name = "partitionComboBox";
            this.partitionComboBox.Size = new System.Drawing.Size(223, 21);
            this.partitionComboBox.TabIndex = 7;
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
            this.tempDirsGroupBox.Controls.Add(this.maxTempStorageUsageLabel);
            this.tempDirsGroupBox.Controls.Add(this.tempStorageUsageUnitComboBox);
            this.tempDirsGroupBox.Controls.Add(this.tempStorageUsageSizeNumericUpDown);
            this.tempDirsGroupBox.Controls.Add(this.button1);
            this.tempDirsGroupBox.Controls.Add(this.label1);
            this.tempDirsGroupBox.Controls.Add(this.button2);
            this.tempDirsGroupBox.Controls.Add(this.secondaryTempFolderTextBox);
            this.tempDirsGroupBox.Controls.Add(this.clearTempFolderButton);
            this.tempDirsGroupBox.Controls.Add(this.tempFolderLabel);
            this.tempDirsGroupBox.Controls.Add(this.setTempFolderButton);
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
            // maxTempStorageUsageLabel
            // 
            this.maxTempStorageUsageLabel.AutoSize = true;
            this.maxTempStorageUsageLabel.Location = new System.Drawing.Point(15, 73);
            this.maxTempStorageUsageLabel.Name = "maxTempStorageUsageLabel";
            this.maxTempStorageUsageLabel.Size = new System.Drawing.Size(155, 13);
            this.maxTempStorageUsageLabel.TabIndex = 19;
            this.maxTempStorageUsageLabel.Text = "Max. temporary storage usage: ";
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
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(617, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 19);
            this.button1.TabIndex = 16;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Secondary temporary directory: ";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button2.Location = new System.Drawing.Point(564, 45);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(47, 19);
            this.button2.TabIndex = 14;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
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
            // clearTempFolderButton
            // 
            this.clearTempFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearTempFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.clearTempFolderButton.Location = new System.Drawing.Point(617, 19);
            this.clearTempFolderButton.Name = "clearTempFolderButton";
            this.clearTempFolderButton.Size = new System.Drawing.Size(26, 19);
            this.clearTempFolderButton.TabIndex = 12;
            this.clearTempFolderButton.Text = "X";
            this.clearTempFolderButton.UseVisualStyleBackColor = true;
            // 
            // tempFolderLabel
            // 
            this.tempFolderLabel.AutoSize = true;
            this.tempFolderLabel.Location = new System.Drawing.Point(31, 22);
            this.tempFolderLabel.Name = "tempFolderLabel";
            this.tempFolderLabel.Size = new System.Drawing.Size(139, 13);
            this.tempFolderLabel.TabIndex = 11;
            this.tempFolderLabel.Text = "Primary temporary directory: ";
            // 
            // setTempFolderButton
            // 
            this.setTempFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.setTempFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.setTempFolderButton.Location = new System.Drawing.Point(564, 19);
            this.setTempFolderButton.Name = "setTempFolderButton";
            this.setTempFolderButton.Size = new System.Drawing.Size(47, 19);
            this.setTempFolderButton.TabIndex = 10;
            this.setTempFolderButton.Text = "...";
            this.setTempFolderButton.UseVisualStyleBackColor = true;
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
            this.progressGroupBox.Location = new System.Drawing.Point(6, 108);
            this.progressGroupBox.MaximumSize = new System.Drawing.Size(555, 92);
            this.progressGroupBox.MinimumSize = new System.Drawing.Size(555, 92);
            this.progressGroupBox.Name = "progressGroupBox";
            this.progressGroupBox.Size = new System.Drawing.Size(555, 92);
            this.progressGroupBox.TabIndex = 10;
            this.progressGroupBox.TabStop = false;
            this.progressGroupBox.Text = "Progress";
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
            this.tempDirsGroupBox.ResumeLayout(false);
            this.tempDirsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tempStorageUsageSizeNumericUpDown)).EndInit();
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
        private System.Windows.Forms.Button clearTempFolderButton;
        private System.Windows.Forms.Label tempFolderLabel;
        private System.Windows.Forms.Button setTempFolderButton;
        private System.Windows.Forms.TextBox primaryTempFolderTextBox;
        private System.Windows.Forms.Label maxTempStorageUsageLabel;
        private System.Windows.Forms.ComboBox tempStorageUsageUnitComboBox;
        private System.Windows.Forms.NumericUpDown tempStorageUsageSizeNumericUpDown;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox secondaryTempFolderTextBox;
        private System.Windows.Forms.GroupBox progressGroupBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}