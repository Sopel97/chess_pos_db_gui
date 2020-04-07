namespace chess_pos_db_gui
{
    partial class EngineProfilesForm
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
            this.profilesListBox = new System.Windows.Forms.ListBox();
            this.confirmButton = new System.Windows.Forms.Button();
            this.addProfileButton = new System.Windows.Forms.Button();
            this.removeProfileButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // profilesListBox
            // 
            this.profilesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.profilesListBox.FormattingEnabled = true;
            this.profilesListBox.Location = new System.Drawing.Point(12, 12);
            this.profilesListBox.Name = "profilesListBox";
            this.profilesListBox.Size = new System.Drawing.Size(367, 212);
            this.profilesListBox.TabIndex = 0;
            // 
            // confirmButton
            // 
            this.confirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.confirmButton.Location = new System.Drawing.Point(277, 232);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(102, 23);
            this.confirmButton.TabIndex = 1;
            this.confirmButton.Text = "Select";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // addProfileButton
            // 
            this.addProfileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addProfileButton.Location = new System.Drawing.Point(12, 232);
            this.addProfileButton.Name = "addProfileButton";
            this.addProfileButton.Size = new System.Drawing.Size(74, 23);
            this.addProfileButton.TabIndex = 2;
            this.addProfileButton.Text = "Add";
            this.addProfileButton.UseVisualStyleBackColor = true;
            this.addProfileButton.Click += new System.EventHandler(this.addProfileButton_Click);
            // 
            // removeProfileButton
            // 
            this.removeProfileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeProfileButton.Location = new System.Drawing.Point(92, 232);
            this.removeProfileButton.Name = "removeProfileButton";
            this.removeProfileButton.Size = new System.Drawing.Size(74, 23);
            this.removeProfileButton.TabIndex = 3;
            this.removeProfileButton.Text = "Remove";
            this.removeProfileButton.UseVisualStyleBackColor = true;
            this.removeProfileButton.Click += new System.EventHandler(this.removeProfileButton_Click);
            // 
            // EngineProfilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 262);
            this.Controls.Add(this.removeProfileButton);
            this.Controls.Add(this.addProfileButton);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.profilesListBox);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "EngineProfilesForm";
            this.Text = "Engine profiles";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox profilesListBox;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Button addProfileButton;
        private System.Windows.Forms.Button removeProfileButton;
    }
}