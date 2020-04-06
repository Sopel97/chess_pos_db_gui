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
            this.optionsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // optionsFlowLayoutPanel
            // 
            this.optionsFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.optionsFlowLayoutPanel.Location = new System.Drawing.Point(15, 12);
            this.optionsFlowLayoutPanel.Name = "optionsFlowLayoutPanel";
            this.optionsFlowLayoutPanel.Size = new System.Drawing.Size(757, 538);
            this.optionsFlowLayoutPanel.TabIndex = 5;
            // 
            // EngineOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.optionsFlowLayoutPanel);
            this.Name = "EngineOptionsForm";
            this.Text = "Engine Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EngineOptionsForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel optionsFlowLayoutPanel;
    }
}