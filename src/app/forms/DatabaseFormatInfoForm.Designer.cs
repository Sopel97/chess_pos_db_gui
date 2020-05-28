namespace chess_pos_db_gui.src.app.forms
{
    partial class DatabaseFormatInfoForm
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
            this.supportManifestDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.supportManifestDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // supportManifestDataGridView
            // 
            this.supportManifestDataGridView.AllowUserToAddRows = false;
            this.supportManifestDataGridView.AllowUserToDeleteRows = false;
            this.supportManifestDataGridView.AllowUserToResizeColumns = false;
            this.supportManifestDataGridView.AllowUserToResizeRows = false;
            this.supportManifestDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.supportManifestDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.supportManifestDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.supportManifestDataGridView.Location = new System.Drawing.Point(0, 0);
            this.supportManifestDataGridView.Name = "supportManifestDataGridView";
            this.supportManifestDataGridView.ReadOnly = true;
            this.supportManifestDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.supportManifestDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.supportManifestDataGridView.Size = new System.Drawing.Size(805, 454);
            this.supportManifestDataGridView.TabIndex = 0;
            // 
            // DatabaseFormatInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 454);
            this.Controls.Add(this.supportManifestDataGridView);
            this.Name = "DatabaseFormatInfoForm";
            this.Text = "Database format info";
            ((System.ComponentModel.ISupportInitialize)(this.supportManifestDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView supportManifestDataGridView;
    }
}