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
            this.engineOptionsDataGridView = new System.Windows.Forms.DataGridView();
            this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.OptionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.engineOptionsDataGridView)).BeginInit();
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
            // engineOptionsDataGridView
            // 
            this.engineOptionsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.engineOptionsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Edit,
            this.OptionName,
            this.Type,
            this.Value});
            this.engineOptionsDataGridView.Location = new System.Drawing.Point(15, 38);
            this.engineOptionsDataGridView.Name = "engineOptionsDataGridView";
            this.engineOptionsDataGridView.Size = new System.Drawing.Size(394, 249);
            this.engineOptionsDataGridView.TabIndex = 5;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "";
            this.Edit.Name = "Edit";
            this.Edit.Text = "Edit";
            this.Edit.Width = 50;
            // 
            // OptionName
            // 
            this.OptionName.HeaderText = "Name";
            this.OptionName.Name = "OptionName";
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // EngineOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.engineOptionsDataGridView);
            this.Controls.Add(this.enginePathLabel);
            this.Controls.Add(this.engineIdLabel);
            this.Name = "EngineOptionsForm";
            this.Text = "EngineOptionsForm";
            ((System.ComponentModel.ISupportInitialize)(this.engineOptionsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label enginePathLabel;
        private System.Windows.Forms.Label engineIdLabel;
        private System.Windows.Forms.DataGridView engineOptionsDataGridView;
        private System.Windows.Forms.DataGridViewButtonColumn Edit;
        private System.Windows.Forms.DataGridViewTextBoxColumn OptionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
    }
}