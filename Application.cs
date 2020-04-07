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

        private EngineAnalysisForm AnalysisForm { get; set; }

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

            queryButton.Enabled = false;

            DoubleBuffered = true;

            gamesWeightCheckbox.Visible = false;
            gamesWeightNumericUpDown.Visible = false;

            tabulatedData.Columns.Add(new DataColumn("Move", typeof(MoveWithSan)));
            tabulatedData.Columns.Add(new DataColumn("Count", typeof(ulong)));
            tabulatedData.Columns.Add(new DataColumn("WinCount", typeof(ulong)));
            tabulatedData.Columns.Add(new DataColumn("DrawCount", typeof(ulong)));
            tabulatedData.Columns.Add(new DataColumn("LossCount", typeof(ulong)));
            tabulatedData.Columns.Add(new DataColumn("Perf", typeof(double)));
            tabulatedData.Columns.Add(new DataColumn("DrawPct", typeof(double)));
            tabulatedData.Columns.Add(new DataColumn("HumanPct", typeof(double)));
            tabulatedData.Columns.Add(new DataColumn("AvgEloDiff", typeof(double)));
            tabulatedData.Columns.Add(new DataColumn("AdjustedPerf", typeof(double)));
            tabulatedData.Columns.Add(new DataColumn("Eval", typeof(Score)));
            tabulatedData.Columns.Add(new DataColumn("EvalPct", typeof(double)));
            tabulatedData.Columns.Add(new DataColumn("Goodness", typeof(double)));
            tabulatedData.Columns.Add(new DataColumn("Date", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("White", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("Black", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("Result", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("Eco", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("PlyCount", typeof(ushort)));
            tabulatedData.Columns.Add(new DataColumn("Event", typeof(string)));
            tabulatedData.Columns.Add(new DataColumn("GameId", typeof(uint)));
            tabulatedData.Columns.Add(new DataColumn("IsOnlyTransposition", typeof(bool)));

            totalTabulatedData.Columns.Add(new DataColumn("Move", typeof(string)));
            totalTabulatedData.Columns.Add(new DataColumn("Count", typeof(ulong)));
            totalTabulatedData.Columns.Add(new DataColumn("WinCount", typeof(ulong)));
            totalTabulatedData.Columns.Add(new DataColumn("DrawCount", typeof(ulong)));
            totalTabulatedData.Columns.Add(new DataColumn("LossCount", typeof(ulong)));
            totalTabulatedData.Columns.Add(new DataColumn("Perf", typeof(double)));
            totalTabulatedData.Columns.Add(new DataColumn("DrawPct", typeof(double)));
            totalTabulatedData.Columns.Add(new DataColumn("HumanPct", typeof(double)));
            totalTabulatedData.Columns.Add(new DataColumn("AvgEloDiff", typeof(double)));
            totalTabulatedData.Columns.Add(new DataColumn("AdjustedPerf", typeof(double)));
            totalTabulatedData.Columns.Add(new DataColumn("Eval", typeof(Score)));
            totalTabulatedData.Columns.Add(new DataColumn("EvalPct", typeof(double)));

            MakeDoubleBuffered(entriesGridView);
            entriesGridView.DataSource = tabulatedData;

            MakeDoubleBuffered(totalEntriesGridView);
            totalEntriesGridView.DataSource = totalTabulatedData;

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
            if (actualPerf < eps || actualPerf > 1.0 - eps) return actualPerf;

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

                        if (moveStr != null)
                        {
                            try
                            {
                                scores.Add(chessBoard.LanToMove(fen, moveStr), new Score(scoreStr));
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
            if (AnalysisForm != null)
            {
                AnalysisForm.OnPositionChanged(chessBoard.GetFen());
            }

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
            if (usedGameCount == 0) return 0.0;

            if (useEvalWeight && score == null && Math.Abs(entry.EloDiff / (long)entry.Count) > maxAllowedEloDiff) return 0.0;

            double engineWeight = useHumanWeight ? (double)engineWeightNumericUpDown.Value : 0.0;
            double humanWeight = useEngineWeight ? (double)humanWeightNumericUpDown.Value : 0.0;
            double totalWeight = useTotalWeight ? (double)gamesWeightNumericUpDown.Value : 0.0;
            double evalWeight = useEvalWeight ? (double)evalWeightNumericUpDown.Value : 0.0;

            ulong totalCount = entry.Count;
            double adjustedTotalPerf = 1.0f;
            if (totalCount > 0)
            {
                ulong totalWins = entry.WinCount;
                ulong totalDraws = entry.DrawCount;
                ulong totalLosses = totalCount - totalWins - totalDraws;
                double totalPerf = ((double)totalWins + (double)totalDraws * 0.5) / (double)totalCount;
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
                double enginePerf = ((double)engineWins + (double)engineDraws * 0.5) / (double)engineCount;
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
                double humanPerf = ((double)humanWins + (double)humanDraws * 0.5) / (double)humanCount;
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

            Func<double, double, double> penalizePerf = (perf, numGames) =>
            {
                if (numGames >= penaltyFromCountThreshold) return perf;
                if (numGames == 0) return minPerf;

                double r = ((numGames+1.0) / (penaltyFromCountThreshold+1));
                double penalty = 1 - Math.Log(r);
                return Math.Max(minPerf, perf / penalty);
            };

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
            foreach (DataRow row in tabulatedData.Rows)
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
                foreach (DataRow row in tabulatedData.Rows)
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
                        header.Result.Stringify(new GameResultPgnFormat()),
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

            var row = tabulatedData.NewRow();
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

            var averageEloDiff = entry.Count > 0 ? (double)entry.EloDiff / (double)entry.Count : 0.0;
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
            row["HumanPct"] = ((double)nonEngineEntry.Count / (double)entry.Count);

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

        private void PopulateTotal(AggregatedEntry total, AggregatedEntry totalNonEngine, Score totalScore)
        {
            long maxDisplayedEloDiff = 400;

            totalTabulatedData.Clear();

            var row = totalTabulatedData.NewRow();
            row["Move"] = "Total";
            row["Count"] = total.Count;
            row["WinCount"] = total.WinCount;
            row["DrawCount"] = total.DrawCount;
            row["LossCount"] = total.LossCount;

            var averageEloDiff = total.Count > 0 ? (double)total.EloDiff / (double)total.Count : 0.0;
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
            row["HumanPct"] = ((double)totalNonEngine.Count / (double)total.Count);

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
            Score bestScore = GetBestScore(scores);
            foreach (KeyValuePair<string, AggregatedEntry> entry in entries)
            {
                if (hideEmpty && IsEmpty(entry.Value)) continue;

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

            PopulateTotal(total, totalNonEngine, bestScore);

            if (goodnessNormalizeCheckbox.Checked)
            {
                NormalizeGoodnessValues();
            }

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

            firstGameInfoRichTextBox.Clear();
        }

        private void Repopulate()
        {
            if (selects.Count == 0 || levels.Count == 0 || data == null)
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

        private void EpdDumpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new EpdDumpForm(database))
            {
                form.ShowDialog();
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

        private void EntriesGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= entriesGridView.Rows.Count || e.ColumnIndex != 0) return;

            if (isEntryDataUpToDate)
            {
                var cell = entriesGridView[e.ColumnIndex, e.RowIndex];
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
            if ((ulong)row.Cells["Count"].Value == 0)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(0xAA, 0xAA, 0xAA);
            }
            else if (Convert.ToBoolean(row.Cells["IsOnlyTransposition"].Value))
            {
                row.DefaultCellStyle.BackColor = Color.LightGray;
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
            endQueryThread = true;
            anyOutstandingQuery.Signal();
            queryThread.Join();
        }

        private void HideNeverPlayedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Repopulate();
        }

        private void HumanWeightNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Repopulate();
        }

        private void EngineWeightNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Repopulate();
        }

        private void EvalWeightNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Repopulate();
        }

        private void GoodnessUseCountCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Repopulate();
        }

        private void GoodnessNormalizeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Repopulate();
        }

        private void CombineHECheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Repopulate();

            gamesWeightCheckbox.Visible = combineHECheckbox.Checked;
            gamesWeightNumericUpDown.Visible = combineHECheckbox.Checked;
            engineWeightCheckbox.Visible = !combineHECheckbox.Checked;
            engineWeightNumericUpDown.Visible = !combineHECheckbox.Checked;
        }

        private void GamesWeightCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Repopulate();
        }

        private void EngineWeightCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Repopulate();
        }

        private void EvaluationWeightCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Repopulate();
        }

        private void humanWeightCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Repopulate();
        }

        private void meanTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Repopulate();
        }

        private void setupToolStripMenuItem_Click(object sender, EventArgs e)
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
