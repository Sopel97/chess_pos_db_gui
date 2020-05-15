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
            this.unassignedEntriesView = new System.Windows.Forms.ListView();
            this.makeGroupButton = new System.Windows.Forms.Button();
            this.entryGroupsView = new System.Windows.Forms.ListView();
            this.removeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(318, 12);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(24, 91);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // unassignedEntriesView
            // 
            this.unassignedEntriesView.AllowDrop = true;
            this.unassignedEntriesView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.unassignedEntriesView.FullRowSelect = true;
            this.unassignedEntriesView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.unassignedEntriesView.HideSelection = false;
            this.unassignedEntriesView.Location = new System.Drawing.Point(12, 12);
            this.unassignedEntriesView.Name = "unassignedEntriesView";
            this.unassignedEntriesView.Size = new System.Drawing.Size(300, 438);
            this.unassignedEntriesView.TabIndex = 2;
            this.unassignedEntriesView.UseCompatibleStateImageBehavior = false;
            this.unassignedEntriesView.View = System.Windows.Forms.View.Details;
            this.unassignedEntriesView.VirtualMode = true;
            this.unassignedEntriesView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.unassignedEntriesView_ItemDrag);
            this.unassignedEntriesView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.unassignedEntriesView_RetrieveVirtualItem);
            this.unassignedEntriesView.DragDrop += new System.Windows.Forms.DragEventHandler(this.unassignedEntriesView_DragDrop);
            this.unassignedEntriesView.DragEnter += new System.Windows.Forms.DragEventHandler(this.unassignedEntriesView_DragEnter);
            // 
            // makeGroupButton
            // 
            this.makeGroupButton.Location = new System.Drawing.Point(318, 147);
            this.makeGroupButton.Name = "makeGroupButton";
            this.makeGroupButton.Size = new System.Drawing.Size(24, 43);
            this.makeGroupButton.TabIndex = 3;
            this.makeGroupButton.Text = ">";
            this.makeGroupButton.UseVisualStyleBackColor = true;
            this.makeGroupButton.Click += new System.EventHandler(this.makeGroupButton_Click);
            // 
            // entryGroupsView
            // 
            this.entryGroupsView.AllowDrop = true;
            this.entryGroupsView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.entryGroupsView.FullRowSelect = true;
            this.entryGroupsView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.entryGroupsView.HideSelection = false;
            this.entryGroupsView.Location = new System.Drawing.Point(348, 12);
            this.entryGroupsView.Name = "entryGroupsView";
            this.entryGroupsView.Size = new System.Drawing.Size(300, 438);
            this.entryGroupsView.TabIndex = 4;
            this.entryGroupsView.UseCompatibleStateImageBehavior = false;
            this.entryGroupsView.View = System.Windows.Forms.View.Details;
            this.entryGroupsView.VirtualMode = true;
            this.entryGroupsView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.entryGroupsView_ItemDrag);
            this.entryGroupsView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.entryGroupsView_RetrieveVirtualItem);
            this.entryGroupsView.SelectedIndexChanged += new System.EventHandler(this.entryGroupsView_SelectedIndexChanged);
            this.entryGroupsView.VirtualItemsSelectionRangeChanged += new System.Windows.Forms.ListViewVirtualItemsSelectionRangeChangedEventHandler(this.entryGroupsView_VirtualItemsSelectionRangeChanged);
            this.entryGroupsView.DragDrop += new System.Windows.Forms.DragEventHandler(this.entryGroupsView_DragDrop);
            this.entryGroupsView.DragEnter += new System.Windows.Forms.DragEventHandler(this.entryGroupsView_DragEnter);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(318, 196);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(24, 43);
            this.removeButton.TabIndex = 5;
            this.removeButton.Text = "<";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // DatabaseMergeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 462);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.entryGroupsView);
            this.Controls.Add(this.makeGroupButton);
            this.Controls.Add(this.unassignedEntriesView);
            this.Controls.Add(this.addButton);
            this.MaximumSize = new System.Drawing.Size(676, 9999);
            this.MinimumSize = new System.Drawing.Size(676, 400);
            this.Name = "DatabaseMergeForm";
            this.Text = "DatabaseMergeForm";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.ListView unassignedEntriesView;
        private System.Windows.Forms.Button makeGroupButton;
        private System.Windows.Forms.ListView entryGroupsView;
        private System.Windows.Forms.Button removeButton;
    }
}