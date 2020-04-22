namespace chess_pos_db_gui.src.chess.engine.analysis.controls
{
    partial class EmbeddedEngineAnalysisControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toggleAnalysisButton = new System.Windows.Forms.Button();
            this.analysisDataGridView = new System.Windows.Forms.DataGridView();
            this.engineIdNameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.analysisDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // toggleAnalysisButton
            // 
            this.toggleAnalysisButton.Location = new System.Drawing.Point(3, 3);
            this.toggleAnalysisButton.Name = "toggleAnalysisButton";
            this.toggleAnalysisButton.Size = new System.Drawing.Size(75, 23);
            this.toggleAnalysisButton.TabIndex = 0;
            this.toggleAnalysisButton.Text = "Start";
            this.toggleAnalysisButton.UseVisualStyleBackColor = true;
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
            this.analysisDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.analysisDataGridView.Location = new System.Drawing.Point(3, 32);
            this.analysisDataGridView.Name = "analysisDataGridView";
            this.analysisDataGridView.ReadOnly = true;
            this.analysisDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.analysisDataGridView.Size = new System.Drawing.Size(372, 224);
            this.analysisDataGridView.TabIndex = 1;
            this.analysisDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.AnalysisDataGridView_ColumnHeaderMouseClick);
            // 
            // engineIdNameLabel
            // 
            this.engineIdNameLabel.AutoSize = true;
            this.engineIdNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.engineIdNameLabel.Location = new System.Drawing.Point(84, 8);
            this.engineIdNameLabel.Name = "engineIdNameLabel";
            this.engineIdNameLabel.Size = new System.Drawing.Size(0, 13);
            this.engineIdNameLabel.TabIndex = 2;
            // 
            // EmbeddedEngineAnalysisControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.engineIdNameLabel);
            this.Controls.Add(this.analysisDataGridView);
            this.Controls.Add(this.toggleAnalysisButton);
            this.Name = "EmbeddedEngineAnalysisControl";
            this.Size = new System.Drawing.Size(378, 259);
            ((System.ComponentModel.ISupportInitialize)(this.analysisDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button toggleAnalysisButton;
        private System.Windows.Forms.DataGridView analysisDataGridView;
        private System.Windows.Forms.Label engineIdNameLabel;
    }
}
