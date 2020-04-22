using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using chess_pos_db_gui.src.util;

namespace chess_pos_db_gui.src.chess.engine.analysis.controls
{
    public partial class EmbeddedEngineAnalysisControl : UserControl
    {
        public System.Windows.Forms.Panel ParentPanel { get; private set; }

        public System.Windows.Forms.DataGridView AnalysisDataGridView => analysisDataGridView;

        public System.Windows.Forms.Button ToggleAnalysisButton => toggleAnalysisButton;

        public System.Windows.Forms.Label EngineIdNameLabel => engineIdNameLabel;
        private bool IsScoreSortDescending { get; set; }
        private DataGridViewColumn SortedColumn { get; set; }

        public EmbeddedEngineAnalysisControl()
        {
            InitializeComponent();
        }

        public void Attach(Panel panel)
        {
            ParentPanel = panel;
            Parent = ParentPanel;
            Dock = DockStyle.Fill;
        }

        public void Detach()
        {
            ParentPanel = null;
            Parent = null;
        }

        private void AnalysisDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn column = analysisDataGridView.Columns[e.ColumnIndex];
            if (column.Name == "Score")
            {
                IsScoreSortDescending = (SortedColumn == null || !IsScoreSortDescending);
                var dir = IsScoreSortDescending ? ListSortDirection.Descending : ListSortDirection.Ascending;
                analysisDataGridView.Sort(analysisDataGridView.Columns["ScoreInt"], dir);
                SortedColumn = column;
            }
            else
            {
                IsScoreSortDescending = false;
                SortedColumn = null;
            }

            analysisDataGridView.Columns["Score"].HeaderCell.SortGlyphDirection = SortOrder.None;
            if (SortedColumn != null && SortedColumn.Name == "Score")
            {
                column.HeaderCell.SortGlyphDirection = IsScoreSortDescending ? SortOrder.Descending : SortOrder.Ascending;
            }
        }
    }
}
