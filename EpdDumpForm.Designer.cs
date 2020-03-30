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
            this.dumpProgressLabel = new System.Windows.Forms.Label();
            this.dumpProgressBar = new System.Windows.Forms.ProgressBar();
            this.dumpButton = new System.Windows.Forms.Button();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pgnsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minCountInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            this.pgnsDataGridView.Size = new System.Drawing.Size(618, 368);
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
            this.splitContainer1.Panel1.Controls.Add(this.minCountLabel);
            this.splitContainer1.Panel1.Controls.Add(this.minCountInput);
            this.splitContainer1.Panel1.Controls.Add(this.clearTempFolderButton);
            this.splitContainer1.Panel1.Controls.Add(this.tempFolderLabel);
            this.splitContainer1.Panel1.Controls.Add(this.outputPathLabel);
            this.splitContainer1.Panel1.Controls.Add(this.setOutputPathButton);
            this.splitContainer1.Panel1.Controls.Add(this.setTempFolderButton);
            this.splitContainer1.Panel1.Controls.Add(this.tempFolderTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.outputPathTextBox);
            this.splitContainer1.Panel1MinSize = 90;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(624, 602);
            this.splitContainer1.SplitterDistance = 90;
            this.splitContainer1.TabIndex = 1;
            // 
            // minCountLabel
            // 
            this.minCountLabel.AutoSize = true;
            this.minCountLabel.Location = new System.Drawing.Point(35, 66);
            this.minCountLabel.Name = "minCountLabel";
            this.minCountLabel.Size = new System.Drawing.Size(54, 13);
            this.minCountLabel.TabIndex = 10;
            this.minCountLabel.Text = "Min count";
            this.tooltip.SetToolTip(this.minCountLabel, "The minimal number of times the position needs to be seen to include it in the ou" +
        "tput epd. If 1 then all positions are included.");
            // 
            // minCountInput
            // 
            this.minCountInput.Location = new System.Drawing.Point(95, 64);
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
            this.minCountInput.Size = new System.Drawing.Size(120, 20);
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
            this.tempFolderLabel.Location = new System.Drawing.Point(26, 41);
            this.tempFolderLabel.Name = "tempFolderLabel";
            this.tempFolderLabel.Size = new System.Drawing.Size(63, 13);
            this.tempFolderLabel.TabIndex = 7;
            this.tempFolderLabel.Text = "Temp folder";
            this.tooltip.SetToolTip(this.tempFolderLabel, "The temporary folder used for intermediate data.");
            // 
            // outputPathLabel
            // 
            this.outputPathLabel.AutoSize = true;
            this.outputPathLabel.Location = new System.Drawing.Point(26, 15);
            this.outputPathLabel.Name = "outputPathLabel";
            this.outputPathLabel.Size = new System.Drawing.Size(63, 13);
            this.outputPathLabel.TabIndex = 6;
            this.outputPathLabel.Text = "Output path";
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
            this.tempFolderTextBox.Location = new System.Drawing.Point(95, 38);
            this.tempFolderTextBox.Name = "tempFolderTextBox";
            this.tempFolderTextBox.ReadOnly = true;
            this.tempFolderTextBox.Size = new System.Drawing.Size(440, 20);
            this.tempFolderTextBox.TabIndex = 2;
            // 
            // outputPathTextBox
            // 
            this.outputPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputPathTextBox.Location = new System.Drawing.Point(95, 12);
            this.outputPathTextBox.Name = "outputPathTextBox";
            this.outputPathTextBox.ReadOnly = true;
            this.outputPathTextBox.Size = new System.Drawing.Size(440, 20);
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
            this.splitContainer2.Panel2.Controls.Add(this.dumpProgressLabel);
            this.splitContainer2.Panel2.Controls.Add(this.dumpProgressBar);
            this.splitContainer2.Panel2.Controls.Add(this.dumpButton);
            this.splitContainer2.Size = new System.Drawing.Size(624, 508);
            this.splitContainer2.SplitterDistance = 406;
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
            this.dumpProgressBar.Location = new System.Drawing.Point(12, 9);
            this.dumpProgressBar.Name = "dumpProgressBar";
            this.dumpProgressBar.Size = new System.Drawing.Size(437, 17);
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
            ((System.ComponentModel.ISupportInitialize)(this.minCountInput)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
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
    }
}