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
            this.engineIdLabel = new System.Windows.Forms.Label();
            this.enginePathLabel = new System.Windows.Forms.Label();
            this.analysisDataGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.engineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleAnalyzeButton = new System.Windows.Forms.Button();
            this.BestMove = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Depth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelDepth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nodes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MultiPV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TBHits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.analysisDataGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // engineIdLabel
            // 
            this.engineIdLabel.AutoSize = true;
            this.engineIdLabel.Location = new System.Drawing.Point(12, 37);
            this.engineIdLabel.Name = "engineIdLabel";
            this.engineIdLabel.Size = new System.Drawing.Size(62, 13);
            this.engineIdLabel.TabIndex = 1;
            this.engineIdLabel.Text = "ENGINE ID";
            // 
            // enginePathLabel
            // 
            this.enginePathLabel.AutoSize = true;
            this.enginePathLabel.Location = new System.Drawing.Point(12, 24);
            this.enginePathLabel.Name = "enginePathLabel";
            this.enginePathLabel.Size = new System.Drawing.Size(80, 13);
            this.enginePathLabel.TabIndex = 2;
            this.enginePathLabel.Text = "ENGINE PATH";
            // 
            // analysisDataGridView
            // 
            this.analysisDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.analysisDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BestMove,
            this.Depth,
            this.SelDepth,
            this.Score,
            this.Time,
            this.Nodes,
            this.Nps,
            this.MultiPV,
            this.TBHits,
            this.PV});
            this.analysisDataGridView.Location = new System.Drawing.Point(12, 82);
            this.analysisDataGridView.Name = "analysisDataGridView";
            this.analysisDataGridView.Size = new System.Drawing.Size(776, 356);
            this.analysisDataGridView.TabIndex = 3;
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
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // toggleAnalyzeButton
            // 
            this.toggleAnalyzeButton.Location = new System.Drawing.Point(12, 53);
            this.toggleAnalyzeButton.Name = "toggleAnalyzeButton";
            this.toggleAnalyzeButton.Size = new System.Drawing.Size(75, 23);
            this.toggleAnalyzeButton.TabIndex = 5;
            this.toggleAnalyzeButton.Text = "Analyze";
            this.toggleAnalyzeButton.UseVisualStyleBackColor = true;
            // 
            // BestMove
            // 
            this.BestMove.HeaderText = "Best move";
            this.BestMove.Name = "BestMove";
            // 
            // Depth
            // 
            this.Depth.HeaderText = "Depth";
            this.Depth.Name = "Depth";
            // 
            // SelDepth
            // 
            this.SelDepth.HeaderText = "SDepth";
            this.SelDepth.Name = "SelDepth";
            // 
            // Score
            // 
            this.Score.HeaderText = "Score";
            this.Score.Name = "Score";
            // 
            // Time
            // 
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            // 
            // Nodes
            // 
            this.Nodes.HeaderText = "Nodes";
            this.Nodes.Name = "Nodes";
            // 
            // Nps
            // 
            this.Nps.HeaderText = "NPS";
            this.Nps.Name = "Nps";
            // 
            // MultiPV
            // 
            this.MultiPV.HeaderText = "MultiPV";
            this.MultiPV.Name = "MultiPV";
            // 
            // TBHits
            // 
            this.TBHits.HeaderText = "TBHits";
            this.TBHits.Name = "TBHits";
            // 
            // PV
            // 
            this.PV.HeaderText = "PV";
            this.PV.Name = "PV";
            // 
            // EngineAnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toggleAnalyzeButton);
            this.Controls.Add(this.analysisDataGridView);
            this.Controls.Add(this.enginePathLabel);
            this.Controls.Add(this.engineIdLabel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "EngineAnalysisForm";
            this.Text = "Engine Analysis";
            ((System.ComponentModel.ISupportInitialize)(this.analysisDataGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label engineIdLabel;
        private System.Windows.Forms.Label enginePathLabel;
        private System.Windows.Forms.DataGridView analysisDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn BestMove;
        private System.Windows.Forms.DataGridViewTextBoxColumn Depth;
        private System.Windows.Forms.DataGridViewTextBoxColumn SelDepth;
        private System.Windows.Forms.DataGridViewTextBoxColumn Score;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nodes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nps;
        private System.Windows.Forms.DataGridViewTextBoxColumn MultiPV;
        private System.Windows.Forms.DataGridViewTextBoxColumn TBHits;
        private System.Windows.Forms.DataGridViewTextBoxColumn PV;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem engineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Button toggleAnalyzeButton;
    }
}