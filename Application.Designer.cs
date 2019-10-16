namespace chess_pos_db_gui
{
    partial class Application
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
            this.entriesGridView = new System.Windows.Forms.DataGridView();
            this.Move = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WinCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrawCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LossCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Perf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrawPct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GameId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Event = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.White = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Black = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Eco = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlyCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.levelSelectionGroupBox = new System.Windows.Forms.GroupBox();
            this.levelServerCheckBox = new System.Windows.Forms.CheckBox();
            this.levelEngineCheckBox = new System.Windows.Forms.CheckBox();
            this.levelHumanCheckBox = new System.Windows.Forms.CheckBox();
            this.typeSelectionGroupBox = new System.Windows.Forms.GroupBox();
            this.typeTranspositionsCheckBox = new System.Windows.Forms.CheckBox();
            this.typeContinuationsCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.entriesGridView)).BeginInit();
            this.levelSelectionGroupBox.SuspendLayout();
            this.typeSelectionGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // entriesGridView
            // 
            this.entriesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.entriesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Move,
            this.Count,
            this.WinCount,
            this.DrawCount,
            this.LossCount,
            this.Perf,
            this.DrawPct,
            this.GameId,
            this.Date,
            this.Event,
            this.White,
            this.Black,
            this.Result,
            this.Eco,
            this.PlyCount});
            this.entriesGridView.Location = new System.Drawing.Point(12, 201);
            this.entriesGridView.Name = "entriesGridView";
            this.entriesGridView.Size = new System.Drawing.Size(776, 237);
            this.entriesGridView.TabIndex = 0;
            // 
            // Move
            // 
            this.Move.HeaderText = "Move";
            this.Move.Name = "Move";
            this.Move.Width = 60;
            // 
            // Count
            // 
            this.Count.HeaderText = "N";
            this.Count.Name = "Count";
            // 
            // WinCount
            // 
            this.WinCount.HeaderText = "W";
            this.WinCount.Name = "WinCount";
            // 
            // DrawCount
            // 
            this.DrawCount.HeaderText = "D";
            this.DrawCount.Name = "DrawCount";
            // 
            // LossCount
            // 
            this.LossCount.HeaderText = "L";
            this.LossCount.Name = "LossCount";
            // 
            // Perf
            // 
            this.Perf.HeaderText = "%";
            this.Perf.Name = "Perf";
            this.Perf.Width = 40;
            // 
            // DrawPct
            // 
            this.DrawPct.HeaderText = "D%";
            this.DrawPct.Name = "DrawPct";
            this.DrawPct.Width = 40;
            // 
            // GameId
            // 
            this.GameId.HeaderText = "GameId";
            this.GameId.Name = "GameId";
            this.GameId.Width = 60;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.Width = 65;
            // 
            // Event
            // 
            this.Event.HeaderText = "Event";
            this.Event.Name = "Event";
            // 
            // White
            // 
            this.White.HeaderText = "White";
            this.White.Name = "White";
            // 
            // Black
            // 
            this.Black.HeaderText = "Black";
            this.Black.Name = "Black";
            // 
            // Result
            // 
            this.Result.HeaderText = "Result";
            this.Result.Name = "Result";
            this.Result.Width = 30;
            // 
            // Eco
            // 
            this.Eco.HeaderText = "ECO";
            this.Eco.Name = "Eco";
            this.Eco.Width = 40;
            // 
            // PlyCount
            // 
            this.PlyCount.HeaderText = "Plies";
            this.PlyCount.Name = "PlyCount";
            this.PlyCount.Width = 40;
            // 
            // levelSelectionGroupBox
            // 
            this.levelSelectionGroupBox.Controls.Add(this.levelServerCheckBox);
            this.levelSelectionGroupBox.Controls.Add(this.levelEngineCheckBox);
            this.levelSelectionGroupBox.Controls.Add(this.levelHumanCheckBox);
            this.levelSelectionGroupBox.Location = new System.Drawing.Point(12, 151);
            this.levelSelectionGroupBox.Name = "levelSelectionGroupBox";
            this.levelSelectionGroupBox.Size = new System.Drawing.Size(206, 44);
            this.levelSelectionGroupBox.TabIndex = 1;
            this.levelSelectionGroupBox.TabStop = false;
            this.levelSelectionGroupBox.Text = "Level";
            // 
            // levelServerCheckBox
            // 
            this.levelServerCheckBox.AutoSize = true;
            this.levelServerCheckBox.Location = new System.Drawing.Point(139, 19);
            this.levelServerCheckBox.Name = "levelServerCheckBox";
            this.levelServerCheckBox.Size = new System.Drawing.Size(57, 17);
            this.levelServerCheckBox.TabIndex = 2;
            this.levelServerCheckBox.Text = "Server";
            this.levelServerCheckBox.UseVisualStyleBackColor = true;
            this.levelServerCheckBox.CheckedChanged += new System.EventHandler(this.LevelServerCheckBox_CheckedChanged);
            // 
            // levelEngineCheckBox
            // 
            this.levelEngineCheckBox.AutoSize = true;
            this.levelEngineCheckBox.Location = new System.Drawing.Point(73, 19);
            this.levelEngineCheckBox.Name = "levelEngineCheckBox";
            this.levelEngineCheckBox.Size = new System.Drawing.Size(59, 17);
            this.levelEngineCheckBox.TabIndex = 1;
            this.levelEngineCheckBox.Text = "Engine";
            this.levelEngineCheckBox.UseVisualStyleBackColor = true;
            this.levelEngineCheckBox.CheckedChanged += new System.EventHandler(this.LevelEngineCheckBox_CheckedChanged);
            // 
            // levelHumanCheckBox
            // 
            this.levelHumanCheckBox.AutoSize = true;
            this.levelHumanCheckBox.Location = new System.Drawing.Point(6, 19);
            this.levelHumanCheckBox.Name = "levelHumanCheckBox";
            this.levelHumanCheckBox.Size = new System.Drawing.Size(60, 17);
            this.levelHumanCheckBox.TabIndex = 0;
            this.levelHumanCheckBox.Text = "Human";
            this.levelHumanCheckBox.UseVisualStyleBackColor = true;
            this.levelHumanCheckBox.CheckedChanged += new System.EventHandler(this.LevelHumanCheckBox_CheckedChanged);
            // 
            // typeSelectionGroupBox
            // 
            this.typeSelectionGroupBox.Controls.Add(this.typeTranspositionsCheckBox);
            this.typeSelectionGroupBox.Controls.Add(this.typeContinuationsCheckBox);
            this.typeSelectionGroupBox.Location = new System.Drawing.Point(224, 151);
            this.typeSelectionGroupBox.Name = "typeSelectionGroupBox";
            this.typeSelectionGroupBox.Size = new System.Drawing.Size(206, 44);
            this.typeSelectionGroupBox.TabIndex = 4;
            this.typeSelectionGroupBox.TabStop = false;
            this.typeSelectionGroupBox.Text = "Type";
            // 
            // typeTranspositionsCheckBox
            // 
            this.typeTranspositionsCheckBox.AutoSize = true;
            this.typeTranspositionsCheckBox.Location = new System.Drawing.Point(102, 19);
            this.typeTranspositionsCheckBox.Name = "typeTranspositionsCheckBox";
            this.typeTranspositionsCheckBox.Size = new System.Drawing.Size(94, 17);
            this.typeTranspositionsCheckBox.TabIndex = 1;
            this.typeTranspositionsCheckBox.Text = "Transpositions";
            this.typeTranspositionsCheckBox.UseVisualStyleBackColor = true;
            this.typeTranspositionsCheckBox.CheckedChanged += new System.EventHandler(this.TypeTranspositionsCheckBox_CheckedChanged);
            // 
            // typeContinuationsCheckBox
            // 
            this.typeContinuationsCheckBox.AutoSize = true;
            this.typeContinuationsCheckBox.Location = new System.Drawing.Point(6, 19);
            this.typeContinuationsCheckBox.Name = "typeContinuationsCheckBox";
            this.typeContinuationsCheckBox.Size = new System.Drawing.Size(90, 17);
            this.typeContinuationsCheckBox.TabIndex = 0;
            this.typeContinuationsCheckBox.Text = "Continuations";
            this.typeContinuationsCheckBox.UseVisualStyleBackColor = true;
            this.typeContinuationsCheckBox.CheckedChanged += new System.EventHandler(this.TypeContinuationsCheckBox_CheckedChanged);
            // 
            // Application
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.typeSelectionGroupBox);
            this.Controls.Add(this.levelSelectionGroupBox);
            this.Controls.Add(this.entriesGridView);
            this.Name = "Application";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.entriesGridView)).EndInit();
            this.levelSelectionGroupBox.ResumeLayout(false);
            this.levelSelectionGroupBox.PerformLayout();
            this.typeSelectionGroupBox.ResumeLayout(false);
            this.typeSelectionGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView entriesGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Move;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.DataGridViewTextBoxColumn WinCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrawCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn LossCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Perf;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrawPct;
        private System.Windows.Forms.DataGridViewTextBoxColumn GameId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Event;
        private System.Windows.Forms.DataGridViewTextBoxColumn White;
        private System.Windows.Forms.DataGridViewTextBoxColumn Black;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
        private System.Windows.Forms.DataGridViewTextBoxColumn Eco;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlyCount;
        private System.Windows.Forms.GroupBox levelSelectionGroupBox;
        private System.Windows.Forms.CheckBox levelServerCheckBox;
        private System.Windows.Forms.CheckBox levelEngineCheckBox;
        private System.Windows.Forms.CheckBox levelHumanCheckBox;
        private System.Windows.Forms.GroupBox typeSelectionGroupBox;
        private System.Windows.Forms.CheckBox typeTranspositionsCheckBox;
        private System.Windows.Forms.CheckBox typeContinuationsCheckBox;
    }
}

