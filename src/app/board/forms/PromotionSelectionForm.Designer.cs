namespace chess_pos_db_gui.src.app.board.forms
{
    partial class PromotionSelectionForm
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
            this.knightPromotionButton = new System.Windows.Forms.Button();
            this.bishopPromotionButton = new System.Windows.Forms.Button();
            this.rookPromotionButton = new System.Windows.Forms.Button();
            this.queenPromotionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // knightPromotionButton
            // 
            this.knightPromotionButton.Location = new System.Drawing.Point(12, 12);
            this.knightPromotionButton.Name = "knightPromotionButton";
            this.knightPromotionButton.Size = new System.Drawing.Size(80, 80);
            this.knightPromotionButton.TabIndex = 0;
            this.knightPromotionButton.UseVisualStyleBackColor = true;
            this.knightPromotionButton.Click += new System.EventHandler(this.KnightPromotionButton_Click);
            // 
            // bishopPromotionButton
            // 
            this.bishopPromotionButton.Location = new System.Drawing.Point(98, 12);
            this.bishopPromotionButton.Name = "bishopPromotionButton";
            this.bishopPromotionButton.Size = new System.Drawing.Size(80, 80);
            this.bishopPromotionButton.TabIndex = 1;
            this.bishopPromotionButton.UseVisualStyleBackColor = true;
            this.bishopPromotionButton.Click += new System.EventHandler(this.BishopPromotionButton_Click);
            // 
            // rookPromotionButton
            // 
            this.rookPromotionButton.Location = new System.Drawing.Point(184, 12);
            this.rookPromotionButton.Name = "rookPromotionButton";
            this.rookPromotionButton.Size = new System.Drawing.Size(80, 80);
            this.rookPromotionButton.TabIndex = 2;
            this.rookPromotionButton.UseVisualStyleBackColor = true;
            this.rookPromotionButton.Click += new System.EventHandler(this.RookPromotionButton_Click);
            // 
            // queenPromotionButton
            // 
            this.queenPromotionButton.Location = new System.Drawing.Point(270, 12);
            this.queenPromotionButton.Name = "queenPromotionButton";
            this.queenPromotionButton.Size = new System.Drawing.Size(80, 80);
            this.queenPromotionButton.TabIndex = 3;
            this.queenPromotionButton.UseVisualStyleBackColor = true;
            this.queenPromotionButton.Click += new System.EventHandler(this.QueenPromotionButton_Click);
            // 
            // PromotionSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(362, 104);
            this.Controls.Add(this.queenPromotionButton);
            this.Controls.Add(this.rookPromotionButton);
            this.Controls.Add(this.bishopPromotionButton);
            this.Controls.Add(this.knightPromotionButton);
            this.Name = "PromotionSelectionForm";
            this.Text = "Select piece to promote to";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button knightPromotionButton;
        private System.Windows.Forms.Button bishopPromotionButton;
        private System.Windows.Forms.Button rookPromotionButton;
        private System.Windows.Forms.Button queenPromotionButton;
    }
}