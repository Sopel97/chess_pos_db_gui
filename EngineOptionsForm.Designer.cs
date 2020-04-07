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
            this.saveButton = new System.Windows.Forms.Button();
            this.discardButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // optionsFlowLayoutPanel
            // 
            this.optionsFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.optionsFlowLayoutPanel.Location = new System.Drawing.Point(15, 35);
            this.optionsFlowLayoutPanel.Name = "optionsFlowLayoutPanel";
            this.optionsFlowLayoutPanel.Size = new System.Drawing.Size(757, 515);
            this.optionsFlowLayoutPanel.TabIndex = 5;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(15, 6);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // discardButton
            // 
            this.discardButton.Location = new System.Drawing.Point(96, 6);
            this.discardButton.Name = "discardButton";
            this.discardButton.Size = new System.Drawing.Size(75, 23);
            this.discardButton.TabIndex = 7;
            this.discardButton.Text = "Discard";
            this.discardButton.UseVisualStyleBackColor = true;
            this.discardButton.Click += new System.EventHandler(this.discardButton_Click);
            // 
            // EngineOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.discardButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.optionsFlowLayoutPanel);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "EngineOptionsForm";
            this.Text = "Engine Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EngineOptionsForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel optionsFlowLayoutPanel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button discardButton;
    }
}