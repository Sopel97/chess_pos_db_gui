namespace chess_pos_db_gui.src.app.board.forms
{
    partial class ThemeSelectionForm
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
            this.boardThemesListBox = new System.Windows.Forms.ListBox();
            this.pieceThemesListBox = new System.Windows.Forms.ListBox();
            this.boardThemesLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.boardPreviewPanel = new System.Windows.Forms.Panel();
            this.confirmButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // boardThemesListBox
            // 
            this.boardThemesListBox.FormattingEnabled = true;
            this.boardThemesListBox.Location = new System.Drawing.Point(12, 29);
            this.boardThemesListBox.Name = "boardThemesListBox";
            this.boardThemesListBox.Size = new System.Drawing.Size(176, 186);
            this.boardThemesListBox.TabIndex = 0;
            this.boardThemesListBox.SelectedIndexChanged += new System.EventHandler(this.BoardThemesListBox_SelectedIndexChanged);
            // 
            // pieceThemesListBox
            // 
            this.pieceThemesListBox.FormattingEnabled = true;
            this.pieceThemesListBox.Location = new System.Drawing.Point(12, 244);
            this.pieceThemesListBox.Name = "pieceThemesListBox";
            this.pieceThemesListBox.Size = new System.Drawing.Size(176, 186);
            this.pieceThemesListBox.TabIndex = 1;
            this.pieceThemesListBox.SelectedIndexChanged += new System.EventHandler(this.PieceThemesListBox_SelectedIndexChanged);
            // 
            // boardThemesLabel
            // 
            this.boardThemesLabel.AutoSize = true;
            this.boardThemesLabel.Location = new System.Drawing.Point(13, 13);
            this.boardThemesLabel.Name = "boardThemesLabel";
            this.boardThemesLabel.Size = new System.Drawing.Size(79, 13);
            this.boardThemesLabel.TabIndex = 2;
            this.boardThemesLabel.Text = "Board Themes:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 228);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Piece Themes:";
            // 
            // boardPreviewPanel
            // 
            this.boardPreviewPanel.Location = new System.Drawing.Point(194, 12);
            this.boardPreviewPanel.Name = "boardPreviewPanel";
            this.boardPreviewPanel.Size = new System.Drawing.Size(418, 418);
            this.boardPreviewPanel.TabIndex = 4;
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(456, 436);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(75, 23);
            this.confirmButton.TabIndex = 5;
            this.confirmButton.Text = "Confirm";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(537, 436);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ThemeSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 467);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.boardPreviewPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.boardThemesLabel);
            this.Controls.Add(this.pieceThemesListBox);
            this.Controls.Add(this.boardThemesListBox);
            this.Name = "ThemeSelectionForm";
            this.Text = "Theme Selection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox boardThemesListBox;
        private System.Windows.Forms.ListBox pieceThemesListBox;
        private System.Windows.Forms.Label boardThemesLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel boardPreviewPanel;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Button cancelButton;
    }
}