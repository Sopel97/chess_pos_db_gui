using ChessDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Json;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public partial class Application : Form
    {
        private static readonly int queryCacheSize = 128;

        private HashSet<GameLevel> levels;
        private HashSet<Select> selects;
        private CacheEntry data;
        private DataTable tabulatedData;
        private DataTable totalTabulatedData;
        private DatabaseProxy database;
        private LRUCache<QueryQueueEntry, CacheEntry> queryCache;
        private bool isEntryDataUpToDate = false;
        private string ip = "127.0.0.1";
        private int port = 1234;

        private QueryQueue queryQueue;

        private object cacheLock;

        private Thread queryThread;

        private ConditionVariable anyOutstandingQuery;

        private volatile bool endQueryThread;

        private Mutex queueMutex;

        private const string URL = "http://www.chessdb.cn/cdb.php";
        private HttpClient chessdbcn;

        public Application()
        {
            queueMutex = new Mutex();
            queryQueue = new QueryQueue();
            cacheLock = new object();
            endQueryThread = false;
            anyOutstandingQuery = new ConditionVariable();
            queryThread = new Thread(new ThreadStart(QueryThread));
            queryThread.Start();

            levels = new HashSet<GameLevel>();
            selects = new HashSet<Select>();
            data = null;
            tabulatedData = new DataTable();
            totalTabulatedData = new DataTable();
            queryCache = new LRUCache<QueryQueueEntry, CacheEntry>(queryCacheSize);

            InitializeComponent();

            levelHumanCheckBox.Checked = true;
            levelEngineCheckBox.Checked = true;
            levelServerCheckBox.Checked = true;
            typeContinuationsCheckBox.Checked = true;
            typeTranspositionsCheckBox.Checked = true;

            DoubleBuffered = true;

            tabulatedData.Columns.Add(new DataColumn("Move", typeof(MoveWithSan)));
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
            tabulatedData.Columns.Add(new DataColumn("Eval", typeof(Score)));
            tabulatedData.Columns.Add(new DataColumn("EvalPct", typeof(double)));
            tabulatedData.Columns.Add(new DataColumn("IsOnlyTransposition", typeof(bool)));

            totalTabulatedData.Columns.Add(new DataColumn("Move", typeof(string)));
            totalTabulatedData.Columns.Add(new DataColumn("Count", typeof(ulong)));
            totalTabulatedData.Columns.Add(new DataColumn("WinCount", typeof(ulong)));
            totalTabulatedData.Columns.Add(new DataColumn("DrawCount", typeof(ulong)));
            totalTabulatedData.Columns.Add(new DataColumn("LossCount", typeof(ulong)));
            totalTabulatedData.Columns.Add(new DataColumn("Perf", typeof(double)));
            totalTabulatedData.Columns.Add(new DataColumn("DrawPct", typeof(double)));
            totalTabulatedData.Columns.Add(new DataColumn("HumanPct", typeof(double)));

            MakeDoubleBuffered(entriesGridView);
            entriesGridView.DataSource = tabulatedData;

            MakeDoubleBuffered(totalEntriesGridView);
            totalEntriesGridView.DataSource = totalTabulatedData;

            totalEntriesGridView.Columns["Move"].Frozen = true;
            totalEntriesGridView.Columns["Move"].HeaderText = "";
            totalEntriesGridView.Columns["Count"].HeaderText = "N";
            totalEntriesGridView.Columns["WinCount"].HeaderText = "W";
            totalEntriesGridView.Columns["DrawCount"].HeaderText = "D";
            totalEntriesGridView.Columns["LossCount"].HeaderText = "L";
            totalEntriesGridView.Columns["Perf"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            totalEntriesGridView.Columns["Perf"].HeaderText = "W%";
            totalEntriesGridView.Columns["DrawPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            totalEntriesGridView.Columns["DrawPct"].HeaderText = "D%";
            totalEntriesGridView.Columns["HumanPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            totalEntriesGridView.Columns["HumanPct"].HeaderText = "H%";

            totalEntriesGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;

            entriesGridView.Columns["Move"].Frozen = true;
            entriesGridView.Columns["Move"].MinimumWidth = 40;
            entriesGridView.Columns["Count"].HeaderText = "N";
            entriesGridView.Columns["WinCount"].HeaderText = "W";
            entriesGridView.Columns["DrawCount"].HeaderText = "D";
            entriesGridView.Columns["LossCount"].HeaderText = "L";
            entriesGridView.Columns["Perf"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["Perf"].HeaderText = "W%";
            entriesGridView.Columns["DrawPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["DrawPct"].HeaderText = "D%";
            entriesGridView.Columns["HumanPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["HumanPct"].HeaderText = "H%";
            entriesGridView.Columns["Result"].HeaderText = "";
            entriesGridView.Columns["Eco"].HeaderText = "ECO";
            entriesGridView.Columns["Eco"].MinimumWidth = 35;
            entriesGridView.Columns["PlyCount"].HeaderText = "Ply";
            entriesGridView.Columns["PlyCount"].MinimumWidth = 35;
            entriesGridView.Columns["PlyCount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["GameId"].HeaderText = "Game\u00A0ID";
            entriesGridView.Columns["Eval"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["Eval"].HeaderText = "Eval";
            entriesGridView.Columns["EvalPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["EvalPct"].HeaderText = "Eval%";
            entriesGridView.Columns["IsOnlyTransposition"].Visible = false;

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

            foreach (DataGridViewColumn column in totalEntriesGridView.Columns)
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

        private Dictionary<Move, Score> GetChessdbcnScores(string fen)
        {
            const string urlParameters = "?action=queryall&board={0}";

            Dictionary<Move, Score> scores = new Dictionary<Move, Score>();

            try
            {
                HttpResponseMessage response = chessdbcn.GetAsync(String.Format(urlParameters, fen)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseStr = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("{0}", responseStr);
                    string[] byMoveStrs = responseStr.Split('|');
                    foreach (var byMoveStr in byMoveStrs)
                    {
                        string[] parts = byMoveStr.Split(',');

                        Dictionary<string, string> values = new Dictionary<string, string>();
                        foreach (var part in parts)
                        {
                            string[] kv = part.Split(':');
                            values.Add(kv[0], kv[1]);
                        }

                        values.TryGetValue("move", out string moveStr);
                        values.TryGetValue("score", out string scoreStr);
                        values.TryGetValue("winrate", out string winrateStr);

                        if (moveStr != null)
                        {
                            try
                            {
                                scores.Add(chessBoard.LanToMove(fen, moveStr), new Score(scoreStr, winrateStr));
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return scores;
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

            chessdbcn = new HttpClient();
            chessdbcn.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            chessdbcn.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

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

        private void Populate(string move, AggregatedEntry entry, AggregatedEntry nonEngineEntry, bool isOnlyTransposition, Score score)
        {
            var row = tabulatedData.NewRow();
            if (move == "--")
            {
                row["Move"] = new MoveWithSan(null, move);
            }
            else
            {
                row["Move"] = new MoveWithSan(chessBoard.SanToMove(move), move);
            }
            row["Count"] = entry.Count;
            row["WinCount"] = entry.WinCount;
            row["DrawCount"] = entry.DrawCount;
            row["LossCount"] = entry.LossCount;
            row["Perf"] = (entry.Perf);
            row["DrawPct"] = (entry.DrawRate);
            row["HumanPct"] = ((double)nonEngineEntry.Count / (double)entry.Count);

            // score is always for side to move
            if (score != null)
            {
                row["Eval"] = score;
                row["EvalPct"] = score.WinPct;
            }

            foreach (GameHeader header in entry.FirstGame)
            {
                row["GameId"] = header.GameId;
                row["Date"] = header.Date.ToStringOmitUnknown();
                row["Event"] = header.Event;
                row["White"] = header.White;
                row["Black"] = header.Black;
                row["Result"] = header.Result.Stringify(new GameResultPgnUnicodeFormat());
                row["Eco"] = header.Eco.ToString();
                row["PlyCount"] = header.PlyCount.FirstOrDefault();
            }

            row["IsOnlyTransposition"] = isOnlyTransposition;

            tabulatedData.Rows.Add(row);
        }

        private bool IsEmpty(AggregatedEntry entry)
        {
            return entry.Count == 0;
        }

        private void PopulateTotal(AggregatedEntry total, AggregatedEntry totalNonEngine)
        {
            totalTabulatedData.Clear();

            var row = totalTabulatedData.NewRow();
            row["Move"] = "Total";
            row["Count"] = total.Count;
            row["WinCount"] = total.WinCount;
            row["DrawCount"] = total.DrawCount;
            row["LossCount"] = total.LossCount;
            row["Perf"] = (total.Perf);
            row["DrawPct"] = (total.DrawRate);
            row["HumanPct"] = ((double)totalNonEngine.Count / (double)total.Count);

            totalTabulatedData.Rows.InsertAt(row, 0);
        }

        private Score GetBestScore(Dictionary<Move, Score> scores)
        {
            Score best = null;
            foreach(KeyValuePair<Move, Score> entry in scores)
            {
                if (best == null || (entry.Value.Value > best.Value))
                {
                    best = entry.Value;
                }
            }
            return best;
        }

        private void Populate(
            Dictionary<string, AggregatedEntry> entries, 
            IEnumerable<string> continuationMoves, 
            Dictionary<string, AggregatedEntry> nonEngineEntries, 
            Dictionary<Move, Score> scores
            )
        {
            entriesGridView.SuspendLayout();
            Clear();

            bool hideEmpty = hideNeverPlayedCheckBox.Checked;

            AggregatedEntry total = new AggregatedEntry();
            AggregatedEntry totalNonEngine = new AggregatedEntry();
            foreach (KeyValuePair<string, AggregatedEntry> entry in entries)
            {
                if (hideEmpty && IsEmpty(entry.Value)) continue;

                if (!nonEngineEntries.ContainsKey(entry.Key))
                {
                    nonEngineEntries.Add(entry.Key, new AggregatedEntry());
                }

                if (entry.Key == "--")
                {
                    Score score = GetBestScore(scores);
                    Populate(entry.Key, entry.Value, nonEngineEntries[entry.Key], !continuationMoves.Contains(entry.Key), score);
                }
                else
                {
                    Score score = null;
                    scores.TryGetValue(chessBoard.SanToMove(entry.Key), out score);
                    Populate(entry.Key, entry.Value, nonEngineEntries[entry.Key], !continuationMoves.Contains(entry.Key), score);
                }

                if (entry.Key != "--")
                {
                    total.Combine(entry.Value);
                    totalNonEngine.Combine(nonEngineEntries[entry.Key]);
                }
            }

            PopulateTotal(total, totalNonEngine);

            entriesGridView.ResumeLayout(false);

            entriesGridView.Refresh();
        }

        private void Gather(CacheEntry res, Select select, List<GameLevel> levels, ref Dictionary<string, AggregatedEntry> aggregatedEntries)
        {
            var rootEntries = res.Stats.Results[0].ResultsBySelect[select].Root;
            var childrenEntries = res.Stats.Results[0].ResultsBySelect[select].Children;

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

        private void Populate(CacheEntry res, List<Select> selects, List<GameLevel> levels)
        {
            Dictionary<string, AggregatedEntry> aggregatedContinuationEntries = new Dictionary<string, AggregatedEntry>();

            Gather(res, chess_pos_db_gui.Select.Continuations, levels, ref aggregatedContinuationEntries);

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

            Populate(aggregatedEntries, aggregatedContinuationEntries.Where(p => p.Value.Count != 0).Select(p => p.Key), aggregatedNonEngineEntries, data.Scores);
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

        private bool TryUpdateDataFromCache()
        {
            if (!database.IsOpen) return false;

            var e = new QueryQueueEntry(chessBoard);

            try
            {
                CacheEntry cached = null;
                lock (cacheLock)
                {
                    cached = queryCache.Get(e);
                }

                if (cached == null)
                {
                    tabulatedData.Clear();
                    totalTabulatedData.Clear();
                    return false;
                }
                else
                {
                    data = cached;
                }
                Repopulate();

                return true;
            }
            catch
            {
            }

            tabulatedData.Clear();
            totalTabulatedData.Clear();
            return false;
        }

        private void QueryAsyncToCache(QueryQueueEntry sig)
        {
            if (!database.IsOpen) return;

            try
            {
                var data = new CacheEntry(null, null);
                ;
                var scores = queryEvalCheckBox.Checked
                    ? Task.Run(() => GetChessdbcnScores(sig.CurrentFen))
                    : Task.FromResult(new Dictionary<Move, Score>());

                if (sig.San == "--")
                {
                    data.Stats = database.Query(sig.Fen);
                    data.Scores = scores.Result;
                    lock (cacheLock)
                    {
                        queryCache.Add(sig, data);
                    }
                }
                else
                {
                    data.Stats = database.Query(sig.Fen, sig.San);
                    data.Scores = scores.Result;
                    lock (cacheLock)
                    {
                        queryCache.Add(sig, data);
                    }
                }
            }
            catch
            {
            }
        }

        private void QueryAsyncToCacheAndUpdate(QueryQueueEntry sig)
        {
            QueryAsyncToCache(sig);

            if (InvokeRequired)
            {
                Invoke(new Func<bool>(TryUpdateDataFromCache));
            }
            else
            {
                TryUpdateDataFromCache();
            }
        }

        private void ScheduleUpdateDataAsync()
        {
            if (TryUpdateDataFromCache())
            {
                return;
            }

            var sig = new QueryQueueEntry(chessBoard);
            queueMutex.WaitOne();
            queryQueue.Enqueue(sig);
            queueMutex.ReleaseMutex();
            anyOutstandingQuery.Signal();
        }
        
        private void QueryThread()
        {
            for(; ; )
            {
                queueMutex.WaitOne();
                while(queryQueue.IsEmpty() && !endQueryThread)
                {
                    anyOutstandingQuery.Wait(queueMutex);
                }

                if (endQueryThread)
                {
                    queueMutex.ReleaseMutex();
                    break;
                }

                var sig = queryQueue.Pop();
                queueMutex.ReleaseMutex();

                QueryAsyncToCacheAndUpdate(sig);
            }
        }

        private void UpdateData()
        {
            if (!database.IsOpen) return;
            ScheduleUpdateDataAsync();
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
                int averageMovesPerGame = ((int)Math.Round((double)info.TotalNumPositions() / info.TotalNumGames() / 2.0));

                databaseInfoRichTextBox.Text =
                    "Path: " + info.Path + Environment.NewLine
                    + "Games: " + info.TotalNumGames().ToString("N0") + Environment.NewLine
                    + "Plies: " + info.TotalNumPositions().ToString("N0") + Environment.NewLine
                    + "Avg game length: " + averageMovesPerGame.ToString("N0") + " moves";
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
                    if (san != "--" && san != "Total")
                        chessBoard.DoMove(san);
                }
            }
        }

        private void EntriesGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (entriesGridView.Columns[e.ColumnIndex].HeaderText == "H%")
            {
                if (e.Value == null || e.Value.GetType() != typeof(double) || Double.IsNaN((double)e.Value) || Double.IsInfinity((double)e.Value))
                {
                    e.Value = "";
                }
                else
                {
                    e.Value = ((double)e.Value * 100).ToString("0") + "%";
                }
                e.FormattingApplied = true;
            }
            else if (entriesGridView.Columns[e.ColumnIndex].HeaderText.Contains("%"))
            {
                if (e.Value == null || e.Value.GetType() != typeof(double) || Double.IsNaN((double)e.Value) || Double.IsInfinity((double)e.Value))
                {
                    e.Value = "";
                }
                else
                {
                    e.Value = ((double)e.Value * 100).ToString("0.0") + "%";
                }
                e.FormattingApplied = true;
            }
        }

        private void TotalEntriesGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (totalEntriesGridView.Columns[e.ColumnIndex].HeaderText == "H%")
            {
                if (e.Value == null || e.Value.GetType() != typeof(double) || Double.IsNaN((double)e.Value) || Double.IsInfinity((double)e.Value))
                {
                    e.Value = "";
                }
                else
                {
                    e.Value = ((double)e.Value * 100).ToString("0") + "%";
                }
                e.FormattingApplied = true;
            }
            else if (totalEntriesGridView.Columns[e.ColumnIndex].HeaderText.Contains("%"))
            {
                if (e.Value == null || e.Value.GetType() != typeof(double) || Double.IsNaN((double)e.Value) || Double.IsInfinity((double)e.Value))
                {
                    e.Value = "";
                }
                else
                {
                    e.Value = ((double)e.Value * 100).ToString("0.0") + "%";
                }
                e.FormattingApplied = true;
            }
        }

        private void EntriesGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = entriesGridView.Rows[e.RowIndex];
            if ((ulong)row.Cells["Count"].Value == 0)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(0xAA, 0xAA, 0xAA);
            }
            else if (Convert.ToBoolean(row.Cells["IsOnlyTransposition"].Value))
            {
                row.DefaultCellStyle.BackColor = Color.LightGray;
            }
        }

        private void TotalEntriesGridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            totalEntriesGridView.Columns[e.Column.Index].Width = e.Column.Width;
        }

        private void Application_FormClosing(object sender, FormClosingEventArgs e)
        {
            endQueryThread = true;
            anyOutstandingQuery.Signal();
            queryThread.Join();
        }

        private void HideNeverPlayedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Repopulate();
        }
    }

    class QueryQueueEntry
    {
        public string Sig { get; private set; }
        public string Fen { get; private set; }
        public string San { get; private set; }
        public string CurrentFen { get; private set; }

        public QueryQueueEntry(ChessBoard chessBoard)
        {
            San = chessBoard.GetLastMoveSan();
            Fen = San == "--"
                ? chessBoard.GetFen()
                : chessBoard.GetPrevFen();

            CurrentFen = chessBoard.GetFen();

            Sig = Fen + San;
        }

        public override int GetHashCode()
        {
            return Sig.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == this.GetType())
            {
                return Sig.Equals(((QueryQueueEntry)obj).Sig);
            }
            return false;
        }
    }

    class QueryQueue
    {
        private QueryQueueEntry Current { get; set; }
        private QueryQueueEntry Next { get; set; }

        private object Lock;

        public QueryQueue()
        {
            Current = null;
            Next = null;

            Lock = new object();
        }

        public void Enqueue(QueryQueueEntry e)
        {
            lock (Lock)
            {
                if (Current == null)
                {
                    Current = e;
                }
                else
                {
                    Next = e;
                }
            }
        }

        public bool IsEmpty()
        {
            return Current == null;
        }

        public QueryQueueEntry Pop()
        {
            lock (Lock)
            {
                if (Current != null)
                {
                    var ret = Current;
                    Current = Next;
                    Next = null;
                    return ret;
                }
                else
                {
                    return null;
                }
            }
        }
    }

    class MoveWithSan
    {
        public Move Move { get; set; }
        public string San { get; set; }

        public MoveWithSan(Move move, string san)
        {
            Move = move;
            San = san;
        }

        public override string ToString()
        {
            return San;
        }
    }

    class CacheEntry
    {
        public QueryResponse Stats { get; set; }
        public Dictionary<Move, Score> Scores { get; set; }

        public CacheEntry(QueryResponse stats, Dictionary<Move, Score> scores)
        {
            Stats = stats;
            Scores = scores;
        }
    }

}
