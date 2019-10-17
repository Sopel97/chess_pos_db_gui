using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Json;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public partial class Application : Form
    {
        private HashSet<GameLevel> levels;
        private HashSet<Select> selects;
        private QueryResponse data;
        private DataTable tabulatedData;

        public Application()
        {
            levels = new HashSet<GameLevel>();
            selects = new HashSet<Select>();
            data = null;
            tabulatedData = new DataTable();

            InitializeComponent();

            levelHumanCheckBox.Checked = true;
            levelEngineCheckBox.Checked = true;
            levelServerCheckBox.Checked = true;
            typeContinuationsCheckBox.Checked = true;
            typeTranspositionsCheckBox.Checked = true;

            DoubleBuffered = true;

            tabulatedData.Columns.Add(new DataColumn("Move", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("Count", typeof(ulong)));
            tabulatedData.Columns.Add(new DataColumn("WinCount", typeof(ulong)));
            tabulatedData.Columns.Add(new DataColumn("DrawCount", typeof(ulong)));
            tabulatedData.Columns.Add(new DataColumn("LossCount", typeof(ulong)));
            tabulatedData.Columns.Add(new DataColumn("Perf", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("DrawPct", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("HumanPct", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("Date", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("White", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("Black", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("Result", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("Eco", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("PlyCount", typeof(ushort)));
            tabulatedData.Columns.Add(new DataColumn("Event", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("GameId", typeof(uint)));

            MakeDoubleBuffered(entriesGridView);
            entriesGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            entriesGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            entriesGridView.DataSource = tabulatedData;

            foreach (DataColumn column in tabulatedData.Columns)
            {
                if (column.DataType == typeof(ulong) || column.DataType == typeof(uint))
                {
                    entriesGridView.Columns[column.ColumnName].DefaultCellStyle.Format = "N0";
                }
            }
        }
        private static void MakeDoubleBuffered(DataGridView dgv)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, true, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chessBoard.LoadImages("assets/graphics");

            string jsonStr = File.ReadAllText("data/data.json");
            var json = JsonValue.Parse(jsonStr);
            data = QueryResponse.FromJson(json);
            Repopulate();
        }

        private string PctToString(double pct)
        {
            if (double.IsNaN(pct) || double.IsPositiveInfinity(pct) || double.IsNegativeInfinity(pct)) return "-";
            return (pct * 100).ToString("F1") + "%";
        }

        private void Populate(string move, AggregatedEntry entry, AggregatedEntry humanEntry)
        {
            var row = tabulatedData.NewRow();
            row["Move"] = move;
            row["Count"] = entry.Count;
            row["WinCount"] = entry.WinCount;
            row["DrawCount"] = entry.DrawCount;
            row["LossCount"] = entry.LossCount;
            row["Perf"] = PctToString(entry.Perf);
            row["DrawPct"] = PctToString(entry.DrawRate);
            row["HumanPct"] = PctToString((double)humanEntry.Count / (double)entry.Count);

            foreach (GameHeader header in entry.FirstGame)
            {
                row["GameId"] = header.GameId;
                row["Date"] = header.Date.ToString();
                row["Event"] = header.Event;
                row["White"] = header.White;
                row["Black"] = header.Black;
                row["Result"] = header.Result.Stringify(new GameResultLetterFormat());
                row["Eco"] = header.Eco.ToString();
                row["PlyCount"] = header.PlyCount.FirstOrDefault();
            }

            tabulatedData.Rows.Add(row);
        }

        private void Populate(Dictionary<string, AggregatedEntry> entries, Dictionary<string, AggregatedEntry> humanEntries)
        {
            entriesGridView.SuspendLayout();
            Clear();
            foreach (KeyValuePair<string, AggregatedEntry> entry in entries)
            {
                Populate(entry.Key, entry.Value, humanEntries[entry.Key]);
            }
            entriesGridView.ResumeLayout(false);

            entriesGridView.Refresh();
        }

        private void Gather(QueryResponse res, Select select, List<GameLevel> levels, ref Dictionary<string, AggregatedEntry> aggregatedEntries)
        {
            var rootEntries = res.Results[0].ResultsBySelect[select].Root;
            var childrenEntries = res.Results[0].ResultsBySelect[select].Children;

            if (aggregatedEntries.ContainsKey("--"))
            {
                aggregatedEntries["--"].Combine(new AggregatedEntry(rootEntries, levels));
            }
            else
            {
                aggregatedEntries.Add("--", new AggregatedEntry(rootEntries, levels));
            }
            foreach (KeyValuePair<string, SegregatedEntries> entry in childrenEntries)
            {
                if (aggregatedEntries.ContainsKey(entry.Key))
                {
                    aggregatedEntries[entry.Key].Combine(new AggregatedEntry(entry.Value, levels));
                }
                else
                {
                    aggregatedEntries.Add(entry.Key, new AggregatedEntry(entry.Value, levels));
                }
            }
        }

        private void Populate(QueryResponse res, List<Select> selects, List<GameLevel> levels)
        {
            Dictionary<string, AggregatedEntry> aggregatedEntries = new Dictionary<string, AggregatedEntry>();

            foreach (Select select in selects)
            {
                Gather(res, select, levels, ref aggregatedEntries);
            }

            Dictionary<string, AggregatedEntry> aggregatedHumanEntries = new Dictionary<string, AggregatedEntry>();
            foreach (Select select in selects)
            {
                Gather(res, select, new List<GameLevel> { GameLevel.Human }, ref aggregatedHumanEntries);
            }

            Populate(aggregatedEntries, aggregatedHumanEntries);
        }

        private void Clear()
        {
            tabulatedData.Clear();
            entriesGridView.Refresh();
        }

        private void Repopulate()
        {
            if (selects.Count == 0 || levels.Count == 0 || data == null)
            {
                Clear();
            }
            else
            {
                Populate(data, selects.ToList(), levels.ToList());
            }
        }

        private void LevelHumanCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (levelHumanCheckBox.Checked) levels.Add(GameLevel.Human);
            else levels.Remove(GameLevel.Human);
            Repopulate();
        }

        private void LevelEngineCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (levelEngineCheckBox.Checked) levels.Add(GameLevel.Engine);
            else levels.Remove(GameLevel.Engine);
            Repopulate();
        }

        private void LevelServerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (levelServerCheckBox.Checked) levels.Add(GameLevel.Server);
            else levels.Remove(GameLevel.Server);
            Repopulate();
        }

        private void TypeContinuationsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (typeContinuationsCheckBox.Checked) selects.Add(chess_pos_db_gui.Select.Continuations);
            else selects.Remove(chess_pos_db_gui.Select.Continuations);
            Repopulate();
        }

        private void TypeTranspositionsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (typeTranspositionsCheckBox.Checked) selects.Add(chess_pos_db_gui.Select.Transpositions);
            else selects.Remove(chess_pos_db_gui.Select.Transpositions);
            Repopulate();
        }

        private void SplitContainer1_Panel1_SizeChanged(object sender, EventArgs e)
        {
            //chessBoard.Width = splitContainer1.Panel1.Width - splitContainer1.Margin.Left - splitContainer1.Margin.Right;
        }
    }
}
