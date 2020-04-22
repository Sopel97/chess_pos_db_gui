namespace chess_pos_db_gui
{
    partial class EngineAnalysisForm
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
            this.engineIdNameLabel = new System.Windows.Forms.Label();
            this.enginePathLabel = new System.Windows.Forms.Label();
            this.analysisDataGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.engineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleAnalyzeButton = new System.Windows.Forms.Button();
            this.engineIdAuthorLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.embedButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.analysisDataGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // engineIdNameLabel
            // 
            this.engineIdNameLabel.AutoSize = true;
            this.engineIdNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.engineIdNameLabel.Location = new System.Drawing.Point(12, 37);
            this.engineIdNameLabel.Name = "engineIdNameLabel";
            this.engineIdNameLabel.Size = new System.Drawing.Size(71, 13);
            this.engineIdNameLabel.TabIndex = 1;
            this.engineIdNameLabel.Text = "ENGINE ID";
            // 
            // enginePathLabel
            // 
            this.enginePathLabel.AutoSize = true;
            this.enginePathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.enginePathLabel.Location = new System.Drawing.Point(12, 24);
            this.enginePathLabel.Name = "enginePathLabel";
            this.enginePathLabel.Size = new System.Drawing.Size(91, 13);
            this.enginePathLabel.TabIndex = 2;
            this.enginePathLabel.Text = "ENGINE PATH";
            // 
            // analysisDataGridView
            // 
            this.analysisDataGridView.AllowUserToAddRows = false;
            this.analysisDataGridView.AllowUserToDeleteRows = false;
            this.analysisDataGridView.AllowUserToResizeColumns = false;
            this.analysisDataGridView.AllowUserToResizeRows = false;
            this.analysisDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.analysisDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.analysisDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.analysisDataGridView.Location = new System.Drawing.Point(12, 95);
            this.analysisDataGridView.Name = "analysisDataGridView";
            this.analysisDataGridView.ReadOnly = true;
            this.analysisDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.analysisDataGridView.Size = new System.Drawing.Size(776, 343);
            this.analysisDataGridView.TabIndex = 3;
            this.analysisDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.AnalysisDataGridView_ColumnHeaderMouseClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.engineToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // engineToolStripMenuItem
            // 
            this.engineToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.engineToolStripMenuItem.Name = "engineToolStripMenuItem";
            this.engineToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.engineToolStripMenuItem.Text = "Engine";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OptionsToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.closeToolStripMenuItem.Text = "Unload";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // toggleAnalyzeButton
            // 
            this.toggleAnalyzeButton.Location = new System.Drawing.Point(12, 66);
            this.toggleAnalyzeButton.Name = "toggleAnalyzeButton";
            this.toggleAnalyzeButton.Size = new System.Drawing.Size(75, 23);
            this.toggleAnalyzeButton.TabIndex = 5;
            this.toggleAnalyzeButton.Text = "Analyze";
            this.toolTip1.SetToolTip(this.toggleAnalyzeButton, "Start/stop analysis.");
            this.toggleAnalyzeButton.UseVisualStyleBackColor = true;
            this.toggleAnalyzeButton.Click += new System.EventHandler(this.ToggleAnalyzeButton_Click);
            // 
            // engineIdAuthorLabel
            // 
            this.engineIdAuthorLabel.AutoSize = true;
            this.engineIdAuthorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.engineIdAuthorLabel.Location = new System.Drawing.Point(12, 50);
            this.engineIdAuthorLabel.Name = "engineIdAuthorLabel";
            this.engineIdAuthorLabel.Size = new System.Drawing.Size(71, 13);
            this.engineIdAuthorLabel.TabIndex = 6;
            this.engineIdAuthorLabel.Text = "ENGINE ID";
            // 
            // embedButton
            // 
            this.embedButton.Location = new System.Drawing.Point(93, 66);
            this.embedButton.Name = "embedButton";
            this.embedButton.Size = new System.Drawing.Size(75, 23);
            this.embedButton.TabIndex = 7;
            this.embedButton.Text = "Embed";
            this.toolTip1.SetToolTip(this.embedButton, "Start/stop analysis.");
            this.embedButton.UseVisualStyleBackColor = true;
            this.embedButton.Click += new System.EventHandler(this.EmbedButton_Click);
            // 
            // EngineAnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.embedButton);
            this.Controls.Add(this.engineIdAuthorLabel);
            this.Controls.Add(this.toggleAnalyzeButton);
            this.Controls.Add(this.analysisDataGridView);
            this.Controls.Add(this.enginePathLabel);
            this.Controls.Add(this.engineIdNameLabel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "EngineAnalysisForm";
            this.Text = "Engine Analysis";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EngineAnalysisForm_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.EngineAnalysisForm_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.analysisDataGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label engineIdNameLabel;
        private System.Windows.Forms.Label enginePathLabel;
        private System.Windows.Forms.DataGridView analysisDataGridView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem engineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Button toggleAnalyzeButton;
        private System.Windows.Forms.Label engineIdAuthorLabel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button embedButton;
    }
}