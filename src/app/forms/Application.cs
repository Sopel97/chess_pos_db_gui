using chess_pos_db_gui.src.app.chessdbcn;

using ChessDotNet;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public partial class Application : Form
    {
        private static readonly int queryCacheSize = 128;

        private HashSet<GameLevel> Levels { get; set; }
        private HashSet<Select> Selects { get; set; }
        private CacheEntry Data { get; set; }
        private DataTable TabulatedData { get; set; }
        private DataTable TotalTabulatedData { get; set; }
        private double BestGoodness { get; set; }
        private DatabaseProxy Database { get; set; }
        private LRUCache<QueryQueueEntry, CacheEntry> QueryCache { get; set; }
        private bool IsEntryDataUpToDate { get; set; } = false;
        private string Ip { get; set; } = "127.0.0.1";
        private int Port { get; set; } = 1234;

        private QueryQueue QueryQueue { get; set; }

        private object CacheLock { get; set; }

        private Thread QueryThread { get; set; }

        private ConditionVariable AnyOutstandingQuery { get; set; }

        private volatile bool EndQueryThread;

        private Mutex QueueMutex { get; set; }

        private ChessDBCNScoreProvider ScoreProvider { get; set; }

        private EngineAnalysisForm AnalysisForm { get; set; }

        public Application()
        {
            QueueMutex = new Mutex();
            QueryQueue = new QueryQueue();
            CacheLock = new object();
            EndQueryThread = false;
            AnyOutstandingQuery = new ConditionVariable();
            QueryThread = new Thread(new ThreadStart(RunQueryThread));
            QueryThread.Start();

            Levels = new HashSet<GameLevel>();
            Selects = new HashSet<Select>();
            Data = null;
            TabulatedData = new DataTable();
            TotalTabulatedData = new DataTable();
            QueryCache = new LRUCache<QueryQueueEntry, CacheEntry>(queryCacheSize);

            BestGoodness = 0.0;

            InitializeComponent();

            levelHumanCheckBox.Checked = true;
            levelEngineCheckBox.Checked = true;
            levelServerCheckBox.Checked = true;
            typeContinuationsCheckBox.Checked = true;
            typeTranspositionsCheckBox.Checked = true;

            queryButton.Enabled = false;

            DoubleBuffered = true;

            gamesWeightCheckbox.Visible = false;
            gamesWeightNumericUpDown.Visible = false;

            TabulatedData.Columns.Add(new DataColumn("Move", typeof(MoveWithSan)));
            TabulatedData.Columns.Add(new DataColumn("Count", typeof(ulong)));
            TabulatedData.Columns.Add(new DataColumn("WinCount", typeof(ulong)));
            TabulatedData.Columns.Add(new DataColumn("DrawCount", typeof(ulong)));
            TabulatedData.Columns.Add(new DataColumn("LossCount", typeof(ulong)));
            TabulatedData.Columns.Add(new DataColumn("Perf", typeof(double)));
            TabulatedData.Columns.Add(new DataColumn("DrawPct", typeof(double)));
            TabulatedData.Columns.Add(new DataColumn("HumanPct", typeof(double)));
            TabulatedData.Columns.Add(new DataColumn("AvgEloDiff", typeof(double)));
            TabulatedData.Columns.Add(new DataColumn("AdjustedPerf", typeof(double)));
            TabulatedData.Columns.Add(new DataColumn("Eval", typeof(Score)));
            TabulatedData.Columns.Add(new DataColumn("EvalPct", typeof(double)));
            TabulatedData.Columns.Add(new DataColumn("Goodness", typeof(double)));
            TabulatedData.Columns.Add(new DataColumn("White", typeof(string)));
            TabulatedData.Columns.Add(new DataColumn("Black", typeof(string)));
            TabulatedData.Columns.Add(new DataColumn("Result", typeof(string)));
            TabulatedData.Columns.Add(new DataColumn("Date", typeof(string)));
            TabulatedData.Columns.Add(new DataColumn("Eco", typeof(string)));
            TabulatedData.Columns.Add(new DataColumn("PlyCount", typeof(ushort)));
            TabulatedData.Columns.Add(new DataColumn("Event", typeof(string)));
            TabulatedData.Columns.Add(new DataColumn("GameId", typeof(uint)));
            TabulatedData.Columns.Add(new DataColumn("IsOnlyTransposition", typeof(bool)));

            TotalTabulatedData.Columns.Add(new DataColumn("Move", typeof(string)));
            TotalTabulatedData.Columns.Add(new DataColumn("Count", typeof(ulong)));
            TotalTabulatedData.Columns.Add(new DataColumn("WinCount", typeof(ulong)));
            TotalTabulatedData.Columns.Add(new DataColumn("DrawCount", typeof(ulong)));
            TotalTabulatedData.Columns.Add(new DataColumn("LossCount", typeof(ulong)));
            TotalTabulatedData.Columns.Add(new DataColumn("Perf", typeof(double)));
            TotalTabulatedData.Columns.Add(new DataColumn("DrawPct", typeof(double)));
            TotalTabulatedData.Columns.Add(new DataColumn("HumanPct", typeof(double)));
            TotalTabulatedData.Columns.Add(new DataColumn("AvgEloDiff", typeof(double)));
            TotalTabulatedData.Columns.Add(new DataColumn("AdjustedPerf", typeof(double)));
            TotalTabulatedData.Columns.Add(new DataColumn("Eval", typeof(Score)));
            TotalTabulatedData.Columns.Add(new DataColumn("EvalPct", typeof(double)));

            MakeDoubleBuffered(entriesGridView);
            entriesGridView.DataSource = TabulatedData;

            MakeDoubleBuffered(totalEntriesGridView);
            totalEntriesGridView.DataSource = TotalTabulatedData;

            totalEntriesGridView.Columns["Move"].Frozen = true;
            totalEntriesGridView.Columns["Move"].MinimumWidth = 50;
            totalEntriesGridView.Columns["Move"].HeaderText = "";
            totalEntriesGridView.Columns["Move"].ToolTipText = "The total stats for all the moves from this position (i.e. total excluding root).";
            totalEntriesGridView.Columns["Count"].HeaderText = "N";
            totalEntriesGridView.Columns["Count"].ToolTipText = "The total number of instances for this position.";
            totalEntriesGridView.Columns["WinCount"].HeaderText = "+";
            totalEntriesGridView.Columns["WinCount"].ToolTipText = "The number of times white has won a game from this position.";
            totalEntriesGridView.Columns["DrawCount"].HeaderText = "=";
            totalEntriesGridView.Columns["DrawCount"].ToolTipText = "The number of times the game has been drawn from this position.";
            totalEntriesGridView.Columns["LossCount"].HeaderText = "-";
            totalEntriesGridView.Columns["LossCount"].ToolTipText = "The number of times white has lost a game from this position.";
            totalEntriesGridView.Columns["Perf"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            totalEntriesGridView.Columns["Perf"].HeaderText = "Wh%";
            totalEntriesGridView.Columns["Perf"].ToolTipText = "The performance (success rate) for the side to move. Equal to (W+D/2)/(W+D+L).";
            totalEntriesGridView.Columns["Perf"].MinimumWidth = 42;
            totalEntriesGridView.Columns["AdjustedPerf"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            totalEntriesGridView.Columns["AdjustedPerf"].HeaderText = "AWh%";
            totalEntriesGridView.Columns["AdjustedPerf"].ToolTipText = "The performance (success rate) adjusted for average elo difference of the players. It is adjusted such that it is 50% if performance is equal to the performance expected from the average elo difference.";
            totalEntriesGridView.Columns["AdjustedPerf"].MinimumWidth = 48;
            totalEntriesGridView.Columns["DrawPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            totalEntriesGridView.Columns["DrawPct"].HeaderText = "D%";
            totalEntriesGridView.Columns["DrawPct"].ToolTipText = "The % of games that ended in a draw.";
            totalEntriesGridView.Columns["HumanPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            totalEntriesGridView.Columns["HumanPct"].HeaderText = "H%";
            totalEntriesGridView.Columns["HumanPct"].ToolTipText = "The % of games being played by humans (Human + Server).";
            totalEntriesGridView.Columns["AvgEloDiff"].HeaderText = "ΔE";
            totalEntriesGridView.Columns["AvgEloDiff"].ToolTipText = "The average elo difference between two players. WhiteElo - BlackElo.";
            totalEntriesGridView.Columns["AvgEloDiff"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            totalEntriesGridView.Columns["Eval"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            totalEntriesGridView.Columns["Eval"].HeaderText = "Ev";
            totalEntriesGridView.Columns["Eval"].ToolTipText = "Engine evaluation. Based on noobpwnftw's https://chessdb.cn/queryc_en/";
            totalEntriesGridView.Columns["EvalPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            totalEntriesGridView.Columns["EvalPct"].HeaderText = "Ev%";
            totalEntriesGridView.Columns["EvalPct"].ToolTipText = "The expected performance based on evaluation. Also sometimes reported by chessdbcn.";

            totalEntriesGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;

            entriesGridView.Columns["Move"].Frozen = true;
            entriesGridView.Columns["Move"].MinimumWidth = 50;
            entriesGridView.Columns["Move"].ToolTipText = "The move leading to the position for which stats are displayed. \"--\" means the root position.";
            entriesGridView.Columns["Count"].HeaderText = "N";
            entriesGridView.Columns["Count"].ToolTipText = "The total number of instances for this position.";
            entriesGridView.Columns["WinCount"].HeaderText = "+";
            entriesGridView.Columns["WinCount"].ToolTipText = "The number of times white has won a game from this position.";
            entriesGridView.Columns["DrawCount"].HeaderText = "=";
            entriesGridView.Columns["DrawCount"].ToolTipText = "The number of times the game has been drawn from this position.";
            entriesGridView.Columns["LossCount"].HeaderText = "-";
            entriesGridView.Columns["LossCount"].ToolTipText = "The number of times white has lost a game from this position.";
            entriesGridView.Columns["Perf"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["Perf"].HeaderText = "Wh%";
            entriesGridView.Columns["Perf"].ToolTipText = "The performance (success rate) for the side to move. Equal to (W+D/2)/(W+D+L).";
            entriesGridView.Columns["Perf"].MinimumWidth = 42;
            entriesGridView.Columns["AdjustedPerf"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["AdjustedPerf"].HeaderText = "AWh%";
            entriesGridView.Columns["AdjustedPerf"].ToolTipText = "The performance (success rate) adjusted for average elo difference of the players. It is adjusted such that it is 50% if performance is equal to the performance expected from the average elo difference.";
            entriesGridView.Columns["AdjustedPerf"].MinimumWidth = 48;
            entriesGridView.Columns["DrawPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["DrawPct"].HeaderText = "D%";
            entriesGridView.Columns["DrawPct"].ToolTipText = "The % of games that ended in a draw.";
            entriesGridView.Columns["HumanPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["HumanPct"].HeaderText = "H%";
            entriesGridView.Columns["HumanPct"].ToolTipText = "The % of games being played by humans (Human + Server).";
            entriesGridView.Columns["AvgEloDiff"].HeaderText = "ΔE";
            entriesGridView.Columns["AvgEloDiff"].ToolTipText = "The average elo difference between two players. WhiteElo - BlackElo.";
            entriesGridView.Columns["AvgEloDiff"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["Result"].HeaderText = "";
            entriesGridView.Columns["Result"].ToolTipText = "The result of the first game with this position.";
            entriesGridView.Columns["Eco"].HeaderText = "ECO";
            entriesGridView.Columns["Eco"].ToolTipText = "The ECO code reported in the first game with this position.";
            entriesGridView.Columns["Eco"].MinimumWidth = 35;
            entriesGridView.Columns["Eco"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["PlyCount"].HeaderText = "Plies";
            entriesGridView.Columns["PlyCount"].ToolTipText = "The length in plies of the first game with this position.";
            entriesGridView.Columns["PlyCount"].MinimumWidth = 40;
            entriesGridView.Columns["PlyCount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["GameId"].HeaderText = "Game\u00A0ID";
            entriesGridView.Columns["GameId"].ToolTipText = "The internal game id of the first game with this position.";
            entriesGridView.Columns["Eval"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["Eval"].HeaderText = "Ev";
            entriesGridView.Columns["Eval"].ToolTipText = "Engine evaluation. Based on noobpwnftw's https://chessdb.cn/queryc_en/";
            entriesGridView.Columns["EvalPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["EvalPct"].HeaderText = "Ev%";
            entriesGridView.Columns["EvalPct"].ToolTipText = "The expected performance based on evaluation. Also sometimes reported by chessdbcn.";
            entriesGridView.Columns["Goodness"].HeaderText = "QI";
            entriesGridView.Columns["Goodness"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["Goodness"].MinimumWidth = 40;
            entriesGridView.Columns["Goodness"].ToolTipText = "The quality of the move calculated from various empirical factors extracted from the data";
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

        private static double GetAdjustedPerformance(double actualPerf, double expectedPerf)
        {
            // We have a cap on elo and elo error. Due to this we don't get infinities
            // from 0% and 100% perf. This means adjusting them results in other values
            // even if expected perf is 50%. So if the perf is borderling we just copy it.
            double eps = 0.001;
            if (actualPerf < eps || actualPerf > 1.0 - eps)
            {
                return actualPerf;
            }

            var actualElo = EloCalculator.GetEloFromPerformance(actualPerf);
            var expectedElo = EloCalculator.GetEloFromPerformance(expectedPerf);
            return EloCalculator.GetExpectedPerformance(actualElo - expectedElo);
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
            Database?.Dispose();
        }

        private Dictionary<Move, Score> GetChessdbcnScores(string fen)
        {
            return ScoreProvider.GetScores(fen);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chessBoard.LoadImages("assets/graphics");

            try
            {
                Database = new DatabaseProxy(Ip, Port);
            }
            catch
            {
                MessageBox.Show("Cannot establish communication with the database backend.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            chessBoard.PositionChanged += OnPositionChanged;

            ScoreProvider = new ChessDBCNScoreProvider();

            UpdateDatabaseInfo();
        }

        private void OnPositionChanged(object sender, EventArgs e)
        {
            if (AnalysisForm != null)
            {
                AnalysisForm.OnPositionChanged(chessBoard.GetFen());
            }

            if (!Database.IsOpen)
            {
                return;
            }

            if (autoQueryCheckbox.Checked)
            {
                UpdateData();
                IsEntryDataUpToDate = true;
            }
            else
            {
                IsEntryDataUpToDate = false;
            }
        }

        /*
         * W - number of wins
         * D - number of draws
         * L - number of losses
         *
         * Ev_Perf - performance estimate based on eval (between 0 and 1)
         * H_Perf - human performance (between 0 and 1)
         * E_Perf - engine performance (between 0 and 1)
         * 
         * expected_perf - performance expected from elo diff
         *
         * H_weight - weight for human games
         * E_weight - weight for engine games
         * Eval_weight - weight for eval
         * Count_confidence - whether elo_error is to be used (boolean)
         *
         * elo error formula: http://talkchess.com/forum3/viewtopic.php?p=645304#p645304
         *
         * N = W+D+L
         * draw_ratio = D/N
         * s(p) = sqrt([p*(1 - p) - draw_ratio/4]/(N - 1))
         * z = 2,58 (for 99% confidence) (would be 2 for 95% confidence)
         *
         * H_elo_error = 1600 * z * s(H_Perf) / ln(10)
         * E_elo_error = 1600 * z * s(E_Perf) / ln(10)
         *
         * if eval not provided:
         *     Eval_weight = 0
         *
         * if Count_confidence:
         *     H_Perf = Perf(Elo(H_Perf) - H_elo_error)
         *     E_Perf = Perf(Elo(E_Perf) - E_elo_error)
         *     
         * H_APerf = AdjustPerf(H_Perf, expected_perf)
         * E_APerf = AdjustPerf(E_Perf, expected_perf)
         *
         * weight_sum = H_weight + E_weight + Eval_weight
         * a = pow(H_APerf, H_weight)
         * b = pow(E_APerf, A_weight)
         * c = pow(Ev_Perf, Eval_weight)
         * goodness = pow(a*b*c, 1.0/weight_sum)
         */
        private double CalculateGoodness(AggregatedEntry entry, AggregatedEntry nonEngineEntry, Score score)
        {
            long maxAllowedEloDiff = 400;

            // if there's less than this amount of games then the goodness contribution will be penalized.
            ulong penaltyFromCountThreshold = 1;

            bool useHumanWeight = humanWeightCheckbox.Checked && !combineHECheckbox.Checked;
            bool useEngineWeight = engineWeightCheckbox.Checked && !combineHECheckbox.Checked;
            bool useTotalWeight = gamesWeightCheckbox.Checked && combineHECheckbox.Checked;
            bool useAnyGames = useHumanWeight || useEngineWeight || useTotalWeight;
            bool useEvalWeight = evaluationWeightCheckbox.Checked;
            bool useCount = goodnessUseCountCheckbox.Checked;

            // return 0 if no games being considered
            ulong usedGameCount = 0;
            ulong usedWins = 0;
            ulong usedDraws = 0;
            ulong usedLosses = 0;
            if (useTotalWeight)
            {
                usedGameCount += entry.Count;
                usedWins += entry.WinCount;
                usedDraws += entry.DrawCount;
                usedLosses += entry.LossCount;
            }
            if (useHumanWeight)
            {
                usedGameCount += nonEngineEntry.Count;
                usedWins += nonEngineEntry.WinCount;
                usedDraws += nonEngineEntry.DrawCount;
                usedLosses += nonEngineEntry.LossCount;
            }
            if (useEngineWeight)
            {
                usedGameCount += entry.Count - nonEngineEntry.Count;
                usedWins += entry.WinCount - nonEngineEntry.WinCount;
                usedDraws += entry.DrawCount - nonEngineEntry.DrawCount;
                usedLosses += entry.LossCount - nonEngineEntry.LossCount;
            }
            if (useAnyGames && usedGameCount == 0)
            {
                return 0.0;
            }

            if (useEvalWeight && score == null && Math.Abs(entry.EloDiff / (long)entry.Count) > maxAllowedEloDiff)
            {
                return 0.0;
            }

            double humanWeight = useHumanWeight ? (double)humanWeightNumericUpDown.Value : 0.0;
            double engineWeight = useEngineWeight ? (double)engineWeightNumericUpDown.Value : 0.0;
            double totalWeight = useTotalWeight ? (double)gamesWeightNumericUpDown.Value : 0.0;
            double evalWeight = useEvalWeight ? (double)evalWeightNumericUpDown.Value : 0.0;

            ulong totalCount = entry.Count;
            double adjustedTotalPerf = 1.0f;
            if (totalCount > 0)
            {
                ulong totalWins = entry.WinCount;
                ulong totalDraws = entry.DrawCount;
                ulong totalLosses = totalCount - totalWins - totalDraws;
                double totalPerf = (totalWins + totalDraws * 0.5) / totalCount;
                double totalEloError = EloCalculator.EloError99pct(totalWins, totalDraws, totalLosses);
                double expectedTotalPerf = EloCalculator.GetExpectedPerformance((entry.EloDiff) / (double)totalCount);
                if (chessBoard.CurrentPlayer() == Player.Black)
                {
                    totalPerf = 1.0 - totalPerf;
                    expectedTotalPerf = 1.0 - expectedTotalPerf;
                }
                if (useCount)
                {
                    totalPerf = EloCalculator.GetExpectedPerformance(EloCalculator.GetEloFromPerformance(totalPerf) - totalEloError);
                }
                adjustedTotalPerf = GetAdjustedPerformance(totalPerf, expectedTotalPerf);
            }

            double adjustedEnginePerf = 1.0f;
            ulong engineCount = entry.Count - nonEngineEntry.Count;
            if (engineCount > 0)
            {
                ulong engineWins = entry.WinCount - nonEngineEntry.WinCount;
                ulong engineDraws = entry.DrawCount - nonEngineEntry.DrawCount;
                ulong engineLosses = engineCount - engineWins - engineDraws;
                double enginePerf = (engineWins + engineDraws * 0.5) / engineCount;
                double engineEloError = EloCalculator.EloError99pct(engineWins, engineDraws, engineLosses);
                double expectedEnginePerf = EloCalculator.GetExpectedPerformance((entry.EloDiff - nonEngineEntry.EloDiff) / (double)engineCount);
                if (chessBoard.CurrentPlayer() == Player.Black)
                {
                    enginePerf = 1.0 - enginePerf;
                    expectedEnginePerf = 1.0 - expectedEnginePerf;
                }
                if (useCount)
                {
                    enginePerf = EloCalculator.GetExpectedPerformance(EloCalculator.GetEloFromPerformance(enginePerf) - engineEloError);
                }
                adjustedEnginePerf = GetAdjustedPerformance(enginePerf, expectedEnginePerf);
            }

            double adjustedHumanPerf = 1.0f;
            ulong humanCount = nonEngineEntry.Count;
            if (humanCount > 0)
            {
                ulong humanWins = nonEngineEntry.WinCount;
                ulong humanDraws = nonEngineEntry.DrawCount;
                ulong humanLosses = humanCount - humanWins - humanDraws;
                double humanPerf = (humanWins + humanDraws * 0.5) / humanCount;
                double humanEloError = EloCalculator.EloError99pct(humanWins, humanDraws, humanLosses);
                double expectedHumanPerf = EloCalculator.GetExpectedPerformance(nonEngineEntry.EloDiff / (double)humanCount);
                if (chessBoard.CurrentPlayer() == Player.Black)
                {
                    humanPerf = 1.0 - humanPerf;
                    expectedHumanPerf = 1.0 - expectedHumanPerf;
                }
                if (useCount)
                {
                    humanPerf = EloCalculator.GetExpectedPerformance(EloCalculator.GetEloFromPerformance(humanPerf) - humanEloError);
                }
                adjustedHumanPerf = GetAdjustedPerformance(humanPerf, expectedHumanPerf);
            }

            double minPerf = 0.01;

            double penalizePerf(double perf, double numGames)
            {
                if (numGames >= penaltyFromCountThreshold)
                {
                    return perf;
                }

                if (numGames == 0)
                {
                    return minPerf;
                }

                double r = ((numGames + 1.0) / (penaltyFromCountThreshold + 1));
                double penalty = 1 - Math.Log(r);
                return Math.Max(minPerf, perf / penalty);
            }

            if (useCount)
            {
                adjustedEnginePerf = penalizePerf(adjustedEnginePerf, engineCount);
                adjustedHumanPerf = penalizePerf(adjustedHumanPerf, humanCount);
                adjustedTotalPerf = penalizePerf(adjustedTotalPerf, totalCount);
            }

            double engineGoodness = Math.Pow(adjustedEnginePerf, engineWeight);
            double humanGoodness = Math.Pow(adjustedHumanPerf, humanWeight);
            double totalGoodness = Math.Pow(adjustedTotalPerf, totalWeight);
            // if eval is not present then assume 0.5 but penalize moves with low game count
            double evalGoodness =
                Math.Pow(
                    score != null
                    ? score.Perf
                    : EloCalculator.GetExpectedPerformance(-EloCalculator.EloError99pct(usedWins, usedDraws, usedLosses))
                    , evalWeight);

            double weightSum = engineWeight + humanWeight + totalWeight + evalWeight;

            double goodness = Math.Pow(engineGoodness * humanGoodness * totalGoodness * evalGoodness, 1.0 / weightSum);

            return goodness;
        }

        private void NormalizeGoodnessValues()
        {
            double highest = 0.0;
            foreach (DataRow row in TabulatedData.Rows)
            {
                if (row["Goodness"] != null)
                {
                    if (((MoveWithSan)row[0]).San != "--")
                    {
                        highest = Math.Max(highest, (double)row["Goodness"]);
                    }
                }
            }

            if (highest != 0.0)
            {
                foreach (DataRow row in TabulatedData.Rows)
                {
                    if (row["Goodness"] != null)
                    {
                        if (((MoveWithSan)row[0]).San != "--")
                        {
                            row["Goodness"] = (double)row["Goodness"] / highest;
                        }
                    }
                }
            }
        }

        private void PopulateFirstGameInfo(AggregatedEntry entry)
        {
            foreach (GameHeader header in entry.FirstGame)
            {
                firstGameInfoRichTextBox.Text = string.Format(
                    "{0} - {1} {2}\n[{3}] ({4}) {5} {6}",
                    new object[]
                    {
                        header.White,
                        header.Black,
                        header.Result.ToStringPgnFormat(),
                        header.Eco,
                        header.PlyCount.Or(0) / 2,
                        header.Event,
                        header.Date.ToStringOmitUnknown()
                    }
                );
            }
        }

        private void Populate(string move, AggregatedEntry entry, AggregatedEntry nonEngineEntry, bool isOnlyTransposition, Score score)
        {
            long maxDisplayedEloDiff = 400;

            var row = TabulatedData.NewRow();
            if (move == "--")
            {
                row["Move"] = new MoveWithSan(null, move);
                row["Goodness"] = double.PositiveInfinity;

                PopulateFirstGameInfo(entry);
            }
            else
            {
                row["Move"] = new MoveWithSan(chessBoard.SanToMove(move), move);
                row["Goodness"] = CalculateGoodness(entry, nonEngineEntry, score);
            }
            row["Count"] = entry.Count;
            row["WinCount"] = entry.WinCount;
            row["DrawCount"] = entry.DrawCount;
            row["LossCount"] = entry.LossCount;

            var averageEloDiff = entry.Count > 0 ? entry.EloDiff / (double)entry.Count : 0.0;
            var expectedPerf = EloCalculator.GetExpectedPerformance(averageEloDiff);
            var adjustedPerf = GetAdjustedPerformance(entry.Perf, expectedPerf);
            if (chessBoard.CurrentPlayer() == Player.White)
            {
                row["Perf"] = entry.Perf;
                row["AdjustedPerf"] = adjustedPerf;
            }
            else
            {
                row["Perf"] = 1.0 - entry.Perf;
                row["AdjustedPerf"] = 1.0 - adjustedPerf;
            }
            row["DrawPct"] = (entry.DrawRate);
            row["HumanPct"] = (nonEngineEntry.Count / (double)entry.Count);

            row["AvgEloDiff"] =
                entry.Count == 0
                ? double.NaN
                : Math.Min(maxDisplayedEloDiff, Math.Max(-maxDisplayedEloDiff, (long)Math.Round(averageEloDiff)));

            // score is always for side to move
            if (score != null)
            {
                row["Eval"] = score;
                row["EvalPct"] = score.Perf;
            }

            foreach (GameHeader header in entry.FirstGame)
            {
                row["GameId"] = header.GameId;
                row["Date"] = header.Date.ToStringOmitUnknown();
                row["Event"] = header.Event;
                row["White"] = header.White;
                row["Black"] = header.Black;
                row["Result"] = header.Result.ToStringPgnUnicodeFormat();
                row["Eco"] = header.Eco.ToString();
                row["PlyCount"] = header.PlyCount.FirstOrDefault();
            }

            row["IsOnlyTransposition"] = isOnlyTransposition;

            TabulatedData.Rows.Add(row);
        }

        private void UpdateGoodness(string move, AggregatedEntry entry, AggregatedEntry nonEngineEntry, Score score)
        {
            System.Data.DataRow row = null;
            foreach (System.Data.DataRow r in TabulatedData.Rows)
            {
                if (((MoveWithSan)r["Move"]).San == move)
                {
                    row = r;
                    break;
                }
            }
            if (row == null)
            {
                return;
            }

            row["Goodness"] = CalculateGoodness(entry, nonEngineEntry, score);
        }

        private bool IsEmpty(AggregatedEntry entry)
        {
            return entry.Count == 0;
        }

        private void PopulateTotal(AggregatedEntry total, AggregatedEntry totalNonEngine, Score totalScore)
        {
            long maxDisplayedEloDiff = 400;

            TotalTabulatedData.Clear();

            var row = TotalTabulatedData.NewRow();
            row["Move"] = "Total";
            row["Count"] = total.Count;
            row["WinCount"] = total.WinCount;
            row["DrawCount"] = total.DrawCount;
            row["LossCount"] = total.LossCount;

            var averageEloDiff = total.Count > 0 ? total.EloDiff / (double)total.Count : 0.0;
            var expectedPerf = EloCalculator.GetExpectedPerformance(averageEloDiff);
            var adjustedPerf = GetAdjustedPerformance(total.Perf, expectedPerf);
            if (chessBoard.CurrentPlayer() == Player.White)
            {
                row["Perf"] = total.Perf;
                row["AdjustedPerf"] = adjustedPerf;
            }
            else
            {
                row["Perf"] = 1.0 - total.Perf;
                row["AdjustedPerf"] = 1.0 - adjustedPerf;
            }
            row["DrawPct"] = (total.DrawRate);
            row["HumanPct"] = (totalNonEngine.Count / (double)total.Count);

            row["AvgEloDiff"] =
                total.Count == 0
                ? double.NaN
                : Math.Min(maxDisplayedEloDiff, Math.Max(-maxDisplayedEloDiff, (long)Math.Round(averageEloDiff)));

            // score is always for side to move
            if (totalScore != null)
            {
                row["Eval"] = totalScore;
                row["EvalPct"] = totalScore.Perf;
            }

            TotalTabulatedData.Rows.InsertAt(row, 0);
        }

        private Score GetBestScore(Dictionary<Move, Score> scores)
        {
            Score best = null;
            foreach (KeyValuePair<Move, Score> entry in scores)
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
            Clear();

            bool hideEmpty = hideNeverPlayedCheckBox.Checked;

            AggregatedEntry total = new AggregatedEntry();
            AggregatedEntry totalNonEngine = new AggregatedEntry();
            Score bestScore = GetBestScore(scores);
            foreach (KeyValuePair<string, AggregatedEntry> entry in entries)
            {
                if (hideEmpty && IsEmpty(entry.Value))
                {
                    continue;
                }

                if (!nonEngineEntries.ContainsKey(entry.Key))
                {
                    nonEngineEntries.Add(entry.Key, new AggregatedEntry());
                }

                if (entry.Key == "--")
                {
                    Populate(entry.Key, entry.Value, nonEngineEntries[entry.Key], !continuationMoves.Contains(entry.Key), bestScore);
                }
                else
                {
                    scores.TryGetValue(chessBoard.SanToMove(entry.Key), out Score score);
                    Populate(entry.Key, entry.Value, nonEngineEntries[entry.Key], !continuationMoves.Contains(entry.Key), score);
                }

                if (entry.Key != "--")
                {
                    total.Combine(entry.Value);
                    totalNonEngine.Combine(nonEngineEntries[entry.Key]);
                }
            }

            PopulateTotal(total, totalNonEngine, bestScore);

            if (goodnessNormalizeCheckbox.Checked)
            {
                NormalizeGoodnessValues();
            }

            UpdateBestGoodness();
        }

        private void UpdateGoodness(
            Dictionary<string, AggregatedEntry> entries,
            Dictionary<string, AggregatedEntry> nonEngineEntries,
            Dictionary<Move, Score> scores
            )
        {
            entriesGridView.SuspendLayout();

            bool hideEmpty = hideNeverPlayedCheckBox.Checked;

            foreach (KeyValuePair<string, AggregatedEntry> entry in entries)
            {
                if (hideEmpty && IsEmpty(entry.Value))
                {
                    continue;
                }

                if (!nonEngineEntries.ContainsKey(entry.Key))
                {
                    nonEngineEntries.Add(entry.Key, new AggregatedEntry());
                }

                if (entry.Key != "--")
                {
                    scores.TryGetValue(chessBoard.SanToMove(entry.Key), out Score score);
                    UpdateGoodness(entry.Key, entry.Value, nonEngineEntries[entry.Key], score);
                }
            }

            if (goodnessNormalizeCheckbox.Checked)
            {
                NormalizeGoodnessValues();
            }

            UpdateBestGoodness();

            entriesGridView.ResumeLayout(false);

            entriesGridView.Refresh();
        }

        private void UpdateBestGoodness()
        {
            BestGoodness = 0.0;

            foreach (DataRow row in TabulatedData.Rows)
            {
                if (row["Goodness"] != null)
                {
                    if (((MoveWithSan)row[0]).San != "--")
                    {
                        BestGoodness = Math.Max(BestGoodness, (double)row["Goodness"]);
                    }
                }
            }
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
                var nonEngineLevels = new List<GameLevel> { };
                if (levels.Contains(GameLevel.Human))
                {
                    nonEngineLevels.Add(GameLevel.Human);
                }

                if (levels.Contains(GameLevel.Server))
                {
                    nonEngineLevels.Add(GameLevel.Server);
                }

                foreach (Select select in selects)
                {
                    Gather(res, select, nonEngineLevels, ref aggregatedNonEngineEntries);
                }
            }

            Populate(aggregatedEntries, aggregatedContinuationEntries.Where(p => p.Value.Count != 0).Select(p => p.Key), aggregatedNonEngineEntries, Data.Scores);
        }

        private void UpdateGoodness(CacheEntry res, List<Select> selects, List<GameLevel> levels)
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
                var nonEngineLevels = new List<GameLevel> { };
                if (levels.Contains(GameLevel.Human))
                {
                    nonEngineLevels.Add(GameLevel.Human);
                }

                if (levels.Contains(GameLevel.Server))
                {
                    nonEngineLevels.Add(GameLevel.Server);
                }

                foreach (Select select in selects)
                {
                    Gather(res, select, nonEngineLevels, ref aggregatedNonEngineEntries);
                }
            }

            UpdateGoodness(aggregatedEntries, aggregatedNonEngineEntries, Data.Scores);
        }

        private void Clear()
        {
            TabulatedData.Clear();
            entriesGridView.Refresh();

            firstGameInfoRichTextBox.Clear();
        }

        private void Repopulate()
        {
            if (Selects.Count == 0 || Levels.Count == 0 || Data == null)
            {
                Clear();
            }
            else
            {
                if (chessBoard.CurrentPlayer() == Player.White)
                {
                    entriesGridView.Columns["Perf"].HeaderText = "Wh%";
                    totalEntriesGridView.Columns["Perf"].HeaderText = "Wh%";
                    entriesGridView.Columns["AdjustedPerf"].HeaderText = "AWh%";
                    totalEntriesGridView.Columns["AdjustedPerf"].HeaderText = "AWh%";
                }
                else
                {
                    entriesGridView.Columns["Perf"].HeaderText = "Bl%";
                    totalEntriesGridView.Columns["Perf"].HeaderText = "Bl%";
                    entriesGridView.Columns["AdjustedPerf"].HeaderText = "ABl%";
                    totalEntriesGridView.Columns["AdjustedPerf"].HeaderText = "ABl%";
                }
                Populate(Data, Selects.ToList(), Levels.ToList());
            }
        }

        private void UpdateGoodness()
        {
            if (Selects.Count == 0 || Levels.Count == 0 || Data == null)
            {
                return;
            }
            else
            {
                UpdateGoodness(Data, Selects.ToList(), Levels.ToList());
            }
        }

        private void LevelHumanCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (levelHumanCheckBox.Checked)
            {
                Levels.Add(GameLevel.Human);
            }
            else
            {
                Levels.Remove(GameLevel.Human);
            }

            Repopulate();
        }

        private void LevelEngineCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (levelEngineCheckBox.Checked)
            {
                Levels.Add(GameLevel.Engine);
            }
            else
            {
                Levels.Remove(GameLevel.Engine);
            }

            Repopulate();
        }

        private void LevelServerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (levelServerCheckBox.Checked)
            {
                Levels.Add(GameLevel.Server);
            }
            else
            {
                Levels.Remove(GameLevel.Server);
            }

            Repopulate();
        }

        private void TypeContinuationsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (typeContinuationsCheckBox.Checked)
            {
                Selects.Add(chess_pos_db_gui.Select.Continuations);
            }
            else
            {
                Selects.Remove(chess_pos_db_gui.Select.Continuations);
            }

            Repopulate();
        }

        private void TypeTranspositionsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (typeTranspositionsCheckBox.Checked)
            {
                Selects.Add(chess_pos_db_gui.Select.Transpositions);
            }
            else
            {
                Selects.Remove(chess_pos_db_gui.Select.Transpositions);
            }

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
            if (!Database.IsOpen)
            {
                return false;
            }

            var e = new QueryQueueEntry(chessBoard);

            try
            {
                CacheEntry cached = null;
                lock (CacheLock)
                {
                    cached = QueryCache.Get(e);
                }

                if (cached == null)
                {
                    TabulatedData.Clear();
                    TotalTabulatedData.Clear();
                    return false;
                }
                else
                {
                    Data = cached;
                }
                Repopulate();

                return true;
            }
            catch
            {
            }

            TabulatedData.Clear();
            TotalTabulatedData.Clear();
            return false;
        }

        private void QueryAsyncToCache(QueryQueueEntry sig)
        {
            if (!Database.IsOpen)
            {
                return;
            }

            try
            {
                var data = new CacheEntry(null, null);
                ;
                var scores = queryEvalCheckBox.Checked
                    ? Task.Run(() => GetChessdbcnScores(sig.CurrentFen))
                    : Task.FromResult(new Dictionary<Move, Score>());

                if (sig.San == "--")
                {
                    data.Stats = Database.Query(sig.Fen);
                    data.Scores = scores.Result;
                    lock (CacheLock)
                    {
                        QueryCache.Add(sig, data);
                    }
                }
                else
                {
                    data.Stats = Database.Query(sig.Fen, sig.San);
                    data.Scores = scores.Result;
                    lock (CacheLock)
                    {
                        QueryCache.Add(sig, data);
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
            QueueMutex.WaitOne();
            QueryQueue.Enqueue(sig);
            QueueMutex.ReleaseMutex();
            AnyOutstandingQuery.Signal();
        }

        private void RunQueryThread()
        {
            for (; ; )
            {
                QueueMutex.WaitOne();
                while (QueryQueue.IsEmpty() && !EndQueryThread)
                {
                    AnyOutstandingQuery.Wait(QueueMutex);
                }

                if (EndQueryThread)
                {
                    QueueMutex.ReleaseMutex();
                    break;
                }

                var sig = QueryQueue.Pop();
                QueueMutex.ReleaseMutex();

                QueryAsyncToCacheAndUpdate(sig);
            }
        }

        private void UpdateData()
        {
            if (!Database.IsOpen)
            {
                return;
            }

            ScheduleUpdateDataAsync();
        }

        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool open = false;
            string path = "";
            using (var form = new DatabaseCreationForm(Database))
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

        private void EpdDumpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new EpdDumpForm(Database))
            {
                form.ShowDialog();
            }
        }

        private void Open(string path)
        {
            Database.Close();
            Database.Open(path);
            QueryCache.Clear();
            UpdateDatabaseInfo();

            OnPositionChanged(this, new EventArgs());
        }

        private void UpdateDatabaseInfo()
        {
            var info = Database.GetInfo();

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
            Database.Close();
            QueryCache.Clear();
            UpdateDatabaseInfo();
        }

        private void EntriesGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= entriesGridView.Rows.Count || e.ColumnIndex != 0)
            {
                return;
            }

            if (IsEntryDataUpToDate)
            {
                var cell = entriesGridView[e.ColumnIndex, e.RowIndex];
                if (cell.ColumnIndex == 0)
                {
                    var san = cell.Value.ToString();
                    if (san != "--" && san != "Total")
                    {
                        chessBoard.DoMove(san);
                    }
                }
            }
        }

        private void EntriesGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = chessBoard.NextMoveNumber() + " " + e.Value.ToString();
                e.FormattingApplied = true;
            }
            else if (entriesGridView.Columns[e.ColumnIndex].Name == "Goodness")
            {
                if (e.Value == null || e.Value.GetType() != typeof(double) || Math.Abs((double)e.Value) < 0.01 || Double.IsNaN((double)e.Value) || Double.IsInfinity((double)e.Value))
                {
                    e.Value = "";
                }
                else
                {
                    e.Value = ((double)e.Value * 100.0).ToString("0.0");
                }
                e.FormattingApplied = true;
            }
            else if (entriesGridView.Columns[e.ColumnIndex].Name == "AvgEloDiff")
            {
                if (e.Value == null || e.Value.GetType() != typeof(double) || Double.IsNaN((double)e.Value) || Double.IsInfinity((double)e.Value))
                {
                    e.Value = "";
                }
                else
                {
                    e.Value = e.Value.ToString();
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
            if (totalEntriesGridView.Columns[e.ColumnIndex].HeaderText.Contains("Goodness"))
            {
                if (e.Value == null || e.Value.GetType() != typeof(double) || Math.Abs((double)e.Value) < 0.01 || Double.IsNaN((double)e.Value) || Double.IsInfinity((double)e.Value))
                {
                    e.Value = "";
                }
                else
                {
                    e.Value = ((double)e.Value * 100).ToString("0");
                }
                e.FormattingApplied = true;
            }
            else if (totalEntriesGridView.Columns[e.ColumnIndex].Name == "AvgEloDiff")
            {
                if (e.Value == null || e.Value.GetType() != typeof(double) || Double.IsNaN((double)e.Value) || Double.IsInfinity((double)e.Value))
                {
                    e.Value = "";
                }
                else
                {
                    e.Value = e.Value.ToString();
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
            var isTransposition = Convert.ToBoolean(row.Cells["IsOnlyTransposition"].Value);
            var goodness = row.Cells["Goodness"].Value;
            var isGoodGoodness = goodness != null && (double)goodness > 0.0 && (double)goodness <= 1.1 && (double)goodness >= 0.9 * BestGoodness;
            if ((ulong)row.Cells["Count"].Value == 0)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(0xAA, 0xAA, 0xAA);
            }
            else
            {
                if (isTransposition && isGoodGoodness)
                {
                    row.DefaultCellStyle.BackColor = Color.DarkGreen;
                }
                else if (isTransposition && !isGoodGoodness)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                }
                else if (!isTransposition && isGoodGoodness)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }

            var nextSan = chessBoard.GetNextMoveSan();
            // we replace # as a workaround for now.
            if (nextSan != null && ((MoveWithSan)row.Cells["Move"].Value).San == nextSan.Replace('#', '+'))
            {
                row.DefaultCellStyle.Font = new Font(entriesGridView.Font, FontStyle.Bold);
            }
        }

        private void TotalEntriesGridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            totalEntriesGridView.Columns[e.Column.Index].Width = e.Column.Width;
        }

        private void Application_FormClosing(object sender, FormClosingEventArgs e)
        {
            EndQueryThread = true;
            AnyOutstandingQuery.Signal();
            QueryThread.Join();
        }

        private void HideNeverPlayedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Repopulate();
        }

        private void HumanWeightNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (humanWeightCheckbox.Checked)
            {
                UpdateGoodness();
            }
        }

        private void EngineWeightNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (engineWeightCheckbox.Checked)
            {
                UpdateGoodness();
            }
        }

        private void EvalWeightNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (evaluationWeightCheckbox.Checked)
            {
                UpdateGoodness();
            }
        }

        private void GoodnessUseCountCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGoodness();
        }

        private void GoodnessNormalizeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGoodness();
        }

        private void CombineHECheckbox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGoodness();

            gamesWeightCheckbox.Visible = combineHECheckbox.Checked;
            gamesWeightNumericUpDown.Visible = combineHECheckbox.Checked;
            engineWeightCheckbox.Visible = !combineHECheckbox.Checked;
            engineWeightNumericUpDown.Visible = !combineHECheckbox.Checked;
        }

        private void GamesWeightCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGoodness();
        }

        private void EngineWeightCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGoodness();
        }

        private void EvaluationWeightCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGoodness();
        }

        private void HumanWeightCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGoodness();
        }

        private void SetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AnalysisForm == null)
            {
                AnalysisForm = new EngineAnalysisForm();
                AnalysisForm.FormClosed += OnOptionsFormClosed;
                AnalysisForm.Show();
            }
        }

        private void OnOptionsFormClosed(object sender, FormClosedEventArgs e)
        {
            AnalysisForm = null;
        }

        private void GamesWeightNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (gamesWeightCheckbox.Checked)
            {
                UpdateGoodness();
            }
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

        private readonly object Lock;

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
