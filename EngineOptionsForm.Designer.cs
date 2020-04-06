namespace chess_pos_db_gui
{
    partial class EngineOptionsForm
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
            this.enginePathLabel = new System.Windows.Forms.Label();
            this.engineIdLabel = new System.Windows.Forms.Label();
            this.optionsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // enginePathLabel
            // 
            this.enginePathLabel.AutoSize = true;
            this.enginePathLabel.Location = new System.Drawing.Point(12, 9);
            this.enginePathLabel.Name = "enginePathLabel";
            this.enginePathLabel.Size = new System.Drawing.Size(80, 13);
            this.enginePathLabel.TabIndex = 4;
            this.enginePathLabel.Text = "ENGINE PATH";
            // 
            // engineIdLabel
            // 
            this.engineIdLabel.AutoSize = true;
            this.engineIdLabel.Location = new System.Drawing.Point(12, 22);
            this.engineIdLabel.Name = "engineIdLabel";
            this.engineIdLabel.Size = new System.Drawing.Size(62, 13);
            this.engineIdLabel.TabIndex = 3;
            this.engineIdLabel.Text = "ENGINE ID";
            // 
            // optionsFlowLayoutPanel
            // 
            this.optionsFlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.optionsFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.optionsFlowLayoutPanel.Location = new System.Drawing.Point(15, 38);
            this.optionsFlowLayoutPanel.Name = "optionsFlowLayoutPanel";
            this.optionsFlowLayoutPanel.Size = new System.Drawing.Size(375, 279);
            this.optionsFlowLayoutPanel.TabIndex = 5;
            // 
            // EngineOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 329);
            this.Controls.Add(this.optionsFlowLayoutPanel);
            this.Controls.Add(this.enginePathLabel);
            this.Controls.Add(this.engineIdLabel);
            this.Name = "EngineOptionsForm";
            this.Text = "EngineOptionsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label enginePathLabel;
        private System.Windows.Forms.Label engineIdLabel;
        private System.Windows.Forms.FlowLayoutPanel optionsFlowLayoutPanel;
    }
}