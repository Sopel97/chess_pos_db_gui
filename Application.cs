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
        private static readonly int queryCacheSize = 128;

        private HashSet<GameLevel> levels;
        private HashSet<Select> selects;
        private QueryResponse data;
        private DataTable tabulatedData;
        private DatabaseProxy database;
        private LRUCache<string, QueryResponse> queryCache;
        private bool isEntryDataUpToDate = false;
        private string ip = "127.0.0.1";
        private int port = 1234;

        public Application()
        {
            levels = new HashSet<GameLevel>();
            selects = new HashSet<Select>();
            data = null;
            tabulatedData = new DataTable();
            queryCache = new LRUCache<string, QueryResponse>(queryCacheSize);

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
            tabulatedData.Columns.Add(new DataColumn("Perf", typeof(double)));
            tabulatedData.Columns.Add(new DataColumn("DrawPct", typeof(double)));
            tabulatedData.Columns.Add(new DataColumn("HumanPct", typeof(double)));
            tabulatedData.Columns.Add(new DataColumn("Date", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("White", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("Black", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("Result", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("Eco", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("PlyCount", typeof(ushort)));
            tabulatedData.Columns.Add(new DataColumn("Event", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("GameId", typeof(uint)));

            MakeDoubleBuffered(entriesGridView);
            entriesGridView.DataSource = tabulatedData;

            entriesGridView.Columns["Move"].Frozen = true;
            //entriesGridView.Columns["Move"].Width = 60;
            //entriesGridView.Columns["Count"].Width = 100;
            entriesGridView.Columns["Count"].HeaderText = "N";
            //entriesGridView.Columns["WinCount"].Width = 100;
            entriesGridView.Columns["WinCount"].HeaderText = "W";
            //entriesGridView.Columns["DrawCount"].Width = 100;
            entriesGridView.Columns["DrawCount"].HeaderText = "D";
            //entriesGridView.Columns["LossCount"].Width = 100;
            entriesGridView.Columns["LossCount"].HeaderText = "L";
            //entriesGridView.Columns["Perf"].Width = 45;
            entriesGridView.Columns["Perf"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["Perf"].HeaderText = "W%";
            //entriesGridView.Columns["DrawPct"].Width = 45;
            entriesGridView.Columns["DrawPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["DrawPct"].HeaderText = "D%";
            //entriesGridView.Columns["HumanPct"].Width = 45;
            entriesGridView.Columns["HumanPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["HumanPct"].HeaderText = "H%";
            //entriesGridView.Columns["Date"].Width = 70;
            //entriesGridView.Columns["White"].Width = 100;
            //entriesGridView.Columns["Black"].Width = 100;
            //entriesGridView.Columns["Result"].Width = 25;
            //entriesGridView.Columns["Eco"].Width = 32;
            entriesGridView.Columns["Eco"].HeaderText = "ECO";
            //entriesGridView.Columns["PlyCount"].Width = 32;
            entriesGridView.Columns["PlyCount"].HeaderText = "Plies";
            //entriesGridView.Columns["Event"].Width = 100;
            //entriesGridView.Columns["GameId"].Width = 80;
            entriesGridView.Columns["GameId"].HeaderText = "Game ID";

            entriesGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;

            entriesGridView.Sort(entriesGridView.Columns["Count"], ListSortDirection.Descending);

            foreach (DataGridViewColumn column in entriesGridView.Columns)
            {
                if (column.ValueType == typeof(ulong) || column.ValueType == typeof(uint))
                {
                    column.DefaultCellStyle.Format = "N0";
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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

        private void OnProcessExit(object sender, EventArgs e)
        {
            database?.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chessBoard.LoadImages("assets/graphics");

            try
            {
                database = new DatabaseProxy(ip, port);
            }
            catch
            {
                MessageBox.Show("Cannot establish communication with the database backend.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            chessBoard.PositionChanged += OnPositionChanged;

            UpdateDatabaseInfo();
        }

        private void OnPositionChanged(object sender, EventArgs e)
        {
            if (!database.IsOpen) return;

            if (autoQueryCheckbox.Checked)
            {
                UpdateData();
                isEntryDataUpToDate = true;
            }
            else
            {
                isEntryDataUpToDate = false;
            }
        }

        private void Populate(string move, AggregatedEntry entry, AggregatedEntry nonEngineEntry)
        {
            var row = tabulatedData.NewRow();
            row["Move"] = move;
            row["Count"] = entry.Count;
            row["WinCount"] = entry.WinCount;
            row["DrawCount"] = entry.DrawCount;
            row["LossCount"] = entry.LossCount;
            row["Perf"] = (entry.Perf);
            row["DrawPct"] = (entry.DrawRate);
            row["HumanPct"] = ((double)nonEngineEntry.Count / (double)entry.Count);

            foreach (GameHeader header in entry.FirstGame)
            {
                row["GameId"] = header.GameId;
                row["Date"] = header.Date.ToString();
                row["Event"] = header.Event;
                row["White"] = header.White;
                row["Black"] = header.Black;
                row["Result"] = header.Result.Stringify(new GameResultPgnUnicodeFormat());
                row["Eco"] = header.Eco.ToString();
                row["PlyCount"] = header.PlyCount.FirstOrDefault();
            }

            tabulatedData.Rows.Add(row);
        }

        private bool IsEmpty(AggregatedEntry entry)
        {
            return entry.Count == 0;
        }

        private void Populate(Dictionary<string, AggregatedEntry> entries, Dictionary<string, AggregatedEntry> nonEngineEntries)
        {
            entriesGridView.SuspendLayout();
            Clear();
            foreach (KeyValuePair<string, AggregatedEntry> entry in entries)
            {
                if (IsEmpty(entry.Value)) continue;
                if (!nonEngineEntries.ContainsKey(entry.Key))
                {
                    nonEngineEntries.Add(entry.Key, new AggregatedEntry());
                }

                Populate(entry.Key, entry.Value, nonEngineEntries[entry.Key]);
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

            Dictionary<string, AggregatedEntry> aggregatedNonEngineEntries = new Dictionary<string, AggregatedEntry>();
            if (levels.Contains(GameLevel.Human) || levels.Contains(GameLevel.Server))
            {
                var nonEngineLevels = new List<GameLevel> {};
                if (levels.Contains(GameLevel.Human)) nonEngineLevels.Add(GameLevel.Human);
                if (levels.Contains(GameLevel.Server)) nonEngineLevels.Add(GameLevel.Server);
                foreach (Select select in selects)
                {
                    Gather(res, select, nonEngineLevels, ref aggregatedNonEngineEntries);
                }
            }

            Populate(aggregatedEntries, aggregatedNonEngineEntries);
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

        private void AutoQueryCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (autoQueryCheckbox.Checked)
            {
                queryButton.Enabled = false;
                UpdateData();
            }
            else
            {
                queryButton.Enabled = true;
            }
        }

        private void QueryButton_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            if (!database.IsOpen) return;

            var san = chessBoard.GetLastMoveSan();
            var fen = san == "--"
                ? chessBoard.GetFen()
                : chessBoard.GetPrevFen();

            var sig = fen + san;

            try
            {
                var cached = queryCache.Get(sig);
                if (cached == null)
                {
                    if (san == "--")
                    {
                        data = database.Query(fen);
                        queryCache.Add(sig, data);
                    }
                    else
                    {
                        data = database.Query(fen, san);
                        queryCache.Add(sig, data);
                    }
                }
                else
                {
                    data = cached;
                }
                Repopulate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool open = false;
            string path = "";
            using (var form = new DatabaseCreationForm(database))
            {
                form.ShowDialog();
                open = form.OpenAfterFinished;
                path = form.DatabasePath;
            }

            if (open)
            {
                Open(path);
            }
        }

        private void Open(string path)
        {
            database.Close();
            database.Open(path);
            queryCache.Clear();
            UpdateDatabaseInfo();

            OnPositionChanged(this, new EventArgs());
        }

        private void UpdateDatabaseInfo()
        {
            var info = database.GetInfo();

            if (info.IsOpen)
            {
                databaseInfoRichTextBox.Text =
                    "Path: " + info.Path + Environment.NewLine
                    + "Games: " + info.TotalNumGames().ToString("N0") + Environment.NewLine
                    + "Positions: " + info.TotalNumPositions().ToString("N0");
            }
            else
            {
                databaseInfoRichTextBox.Text = "No database open.";
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog browser = new FolderBrowserDialog())
                {
                    if (browser.ShowDialog() == DialogResult.OK)
                    {
                        var path = browser.SelectedPath;

                        Open(path);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            database.Close();
            queryCache.Clear();
            UpdateDatabaseInfo();
        }

        private void EntriesGridView_DoubleClick(object sender, EventArgs e)
        {
            if (isEntryDataUpToDate && entriesGridView.SelectedCells.Count > 0)
            {
                var cell = entriesGridView.SelectedCells[0];
                if (cell.ColumnIndex == 0)
                {
                    var san = cell.Value.ToString();
                    if (san != "--")
                        chessBoard.DoMove(san);
                }
            }
        }

        private void EntriesGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (entriesGridView.Columns[e.ColumnIndex].HeaderText == "H%")
            {
                e.Value = (Double.Parse(e.Value.ToString()) * 100).ToString("0") + "%";
                e.FormattingApplied = true;
            }
            else if (entriesGridView.Columns[e.ColumnIndex].HeaderText.Contains("%"))
            {
                e.Value = (Double.Parse(e.Value.ToString())*100).ToString("0.0") + "%";
                e.FormattingApplied = true;
            }
        }
    }
}
