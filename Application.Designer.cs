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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Move = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WinCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrawCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LossCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Perf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrawPct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GameId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Event = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.White = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Black = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Eco = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlyCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.Move,
            this.Count,
            this.WinCount,
            this.DrawCount,
            this.LossCount,
            this.Perf,
            this.DrawPct});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(776, 237);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick);
            // 
            // No
            // 
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.Width = 40;
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
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Result,
            this.GameId,
            this.Date,
            this.Event,
            this.White,
            this.Black,
            this.Eco,
            this.PlyCount});
            this.dataGridView2.Location = new System.Drawing.Point(12, 255);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(776, 183);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView2_CellContentClick);
            // 
            // Result
            // 
            this.Result.HeaderText = "";
            this.Result.Name = "Result";
            this.Result.Width = 25;
            // 
            // GameId
            // 
            this.GameId.HeaderText = "Game ID";
            this.GameId.Name = "GameId";
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
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
            // Eco
            // 
            this.Eco.HeaderText = "ECO";
            this.Eco.Name = "Eco";
            this.Eco.Width = 40;
            // 
            // PlyCount
            // 
            this.PlyCount.HeaderText = "Ply Count";
            this.PlyCount.Name = "PlyCount";
            this.PlyCount.Width = 40;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Move;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.DataGridViewTextBoxColumn WinCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrawCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn LossCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Perf;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrawPct;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
        private System.Windows.Forms.DataGridViewTextBoxColumn GameId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Event;
        private System.Windows.Forms.DataGridViewTextBoxColumn White;
        private System.Windows.Forms.DataGridViewTextBoxColumn Black;
        private System.Windows.Forms.DataGridViewTextBoxColumn Eco;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlyCount;
    }
}

