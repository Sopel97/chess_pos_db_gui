namespace chess_pos_db_gui.src.app.forms
{
    partial class DatabaseMergeForm
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
            this.addButton = new System.Windows.Forms.Button();
            this.entriesView = new System.Windows.Forms.ListView();
            this.makeGroupButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(338, 12);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // entriesView
            // 
            this.entriesView.AllowDrop = true;
            this.entriesView.FullRowSelect = true;
            this.entriesView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.entriesView.Location = new System.Drawing.Point(12, 12);
            this.entriesView.Name = "entriesView";
            this.entriesView.Size = new System.Drawing.Size(295, 426);
            this.entriesView.TabIndex = 2;
            this.entriesView.UseCompatibleStateImageBehavior = false;
            this.entriesView.View = System.Windows.Forms.View.Details;
            this.entriesView.VirtualMode = true;
            this.entriesView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.entriesView_ItemDrag);
            this.entriesView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.entriesView_RetrieveVirtualItem);
            this.entriesView.SelectedIndexChanged += new System.EventHandler(this.entriesView_SelectedIndexChanged);
            this.entriesView.VirtualItemsSelectionRangeChanged += new System.Windows.Forms.ListViewVirtualItemsSelectionRangeChangedEventHandler(this.entriesView_VirtualItemsSelectionRangeChanged);
            this.entriesView.DragDrop += new System.Windows.Forms.DragEventHandler(this.entriesView_DragDrop);
            // 
            // makeGroupButton
            // 
            this.makeGroupButton.Location = new System.Drawing.Point(338, 41);
            this.makeGroupButton.Name = "makeGroupButton";
            this.makeGroupButton.Size = new System.Drawing.Size(75, 23);
            this.makeGroupButton.TabIndex = 3;
            this.makeGroupButton.Text = "makeGroup";
            this.makeGroupButton.UseVisualStyleBackColor = true;
            this.makeGroupButton.Click += new System.EventHandler(this.makeGroupButton_Click);
            // 
            // DatabaseMergeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.makeGroupButton);
            this.Controls.Add(this.entriesView);
            this.Controls.Add(this.addButton);
            this.Name = "DatabaseMergeForm";
            this.Text = "DatabaseMergeForm";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.ListView entriesView;
        private System.Windows.Forms.Button makeGroupButton;
    }
}