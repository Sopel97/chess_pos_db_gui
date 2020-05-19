using chess_pos_db_gui.src.app;
using chess_pos_db_gui.src.app.board;
using chess_pos_db_gui.src.app.board.forms;
using chess_pos_db_gui.src.app.chessdbcn;
using chess_pos_db_gui.src.app.forms;
using chess_pos_db_gui.src.chess;
using chess_pos_db_gui.src.chess.engine.analysis;
using chess_pos_db_gui.src.util;
using ChessDotNet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public partial class Application : Form
    {
        private class ApplicationEmbeddedAnalysisHandler : EmbeddedAnalysisHandler
        {
            private readonly int embeddedAnalysisPanelHeight = 130;

            private readonly Application application;

            private EngineAnalysisForm ResponsibleEntity { get; set; }

            public ApplicationEmbeddedAnalysisHandler(Application application)
            {
                this.application = application;
            }

            public override System.Windows.Forms.Panel PrepareAndGetEmbeddedAnalysisPanel(EngineAnalysisForm form)
            {
                ResponsibleEntity = form;

                application.analysisAndBoardSplitContainer.SplitterDistance = embeddedAnalysisPanelHeight;
                application.analysisAndBoardSplitContainer.IsSplitterFixed = true;
                application.analysisAndBoardSplitContainer.FixedPanel = FixedPanel.Panel1;
                return application.analysisAndBoardSplitContainer.Panel1;
            }

            public override void OnEmbeddedAnalysisEnded()
            {
                application.analysisAndBoardSplitContainer.SplitterDistance = 0;
                application.analysisAndBoardSplitContainer.IsSplitterFixed = true;
                application.analysisAndBoardSplitContainer.FixedPanel = FixedPanel.Panel1;

                ResponsibleEntity = null;
            }

            public override void Dispose()
            {
                if (ResponsibleEntity != null)
                {
                    ResponsibleEntity.ForceStopEmbeddedAnalysis(this);
                }
            }
        }

        private class SerializableSettings : JsonSerializable<SerializableSettings>
        {
            public bool AutoQueryCheckBoxChecked { get; set; } = true;
            public bool QueryEvalCheckBoxChecked { get; set; } = true;
            public bool LevelHumanCheckBoxChecked { get; set; } = true;
            public bool LevelEngineCheckBoxChecked { get; set; } = true;
            public bool LevelServerCheckBoxChecked { get; set; } = true;
            public bool HideNeverPlayedCheckBoxChecked { get; set; } = false;
            public bool TypeContinuationsCheckBoxChecked { get; set; } = true;
            public bool TypeTranspositionsCheckBoxChecked { get; set; } = true;
            public bool HumanWeightCheckBoxChecked { get; set; } = true;
            public bool GamesWeightCheckBoxChecked { get; set; } = true;
            public bool EngineWeightCheckBoxChecked { get; set; } = true;
            public bool EvaluationWeightCheckBoxChecked { get; set; } = true;
            public bool CombineHECheckBoxChecked { get; set; } = false;
            public bool GoodnessUseCountCheckBoxChecked { get; set; } = true;
            public bool GoodnessNormalizeCheckBoxChecked { get; set; } = true;
            public decimal GamesWeightNumericUpDownValue { get; set; } = 1;
            public decimal HumanWeightNumericUpDownValue { get; set; } = 1;
            public decimal EngineWeightNumericUpDownValue { get; set; } = 3;
            public decimal EvalWeightNumericUpDownValue { get; set; } = 2;
            public int SplitChessAndDataSplitterDistance { get; set; } = 428;
            public int EntriesRetractionsSplitPanelSplitterDistance { get; set; } = 270;
            public int FormWidth { get; set; } = 1075;
            public int FormHeight { get; set; } = 600;
            public string BoardThemeName { get; set; } = null;
            public string PieceThemeName { get; set; } = null;
        }

        private static readonly string engineProfilesPath = "data/engine_profiles.json";
        private static readonly string settingsPath = "data/application/settings.json";
        private static readonly double insignificantGoodnessTheshold = 0.01;
        private static readonly int minFilesToDisableAutoQueryOnDatabaseLoad = 3;

        private UciEngineProfileStorage EngineProfiles { get; set; }

        private string DatabaseTcpClientIp { get; set; } = "127.0.0.1";
        private int DatabaseTcpClientPort { get; set; } = 1234;
        private DatabaseProxy Database { get; set; }

        private HashSet<GameLevel> Levels { get; set; }
        private HashSet<Select> Selects { get; set; }

        private QueryCacheEntry CacheEntry { get; set; }
        private DataTable TabulatedData { get; set; }
        private DataTable RetractionsData { get; set; }
        private DataTable TotalTabulatedData { get; set; }

        private double BestGoodness { get; set; }
        private bool IsEntryDataUpToDate { get; set; } = false;

        private EngineAnalysisForm AnalysisForm { get; set; }

        private EmbeddedAnalysisHandler EmbeddedHandler { get; set; }

        private QueryExecutor QueryExecutor { get; set; }

        private ThemeDatabase Themes { get; set; }

        private int PrevFirstVisibleRow { get; set; } = -1;
        private int PrevFirstVisibleColumn { get; set; } = -1;

        public Application()
        {
            Levels = new HashSet<GameLevel>();
            Selects = new HashSet<Select>();
            CacheEntry = null;
            TabulatedData = new DataTable();
            RetractionsData = new DataTable();
            TotalTabulatedData = new DataTable();

            EngineProfiles = new UciEngineProfileStorage(engineProfilesPath);

            BestGoodness = 0.0;

            InitializeComponent();

            levelHumanCheckBox.Checked = true;
            levelEngineCheckBox.Checked = true;
            levelServerCheckBox.Checked = true;
            typeContinuationsCheckBox.Checked = true;
            typeTranspositionsCheckBox.Checked = true;

            queryButton.Enabled = false;

            DoubleBuffered = true;

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
            TabulatedData.Columns.Add(new DataColumn("Eval", typeof(ChessDBCNScore)));
            TabulatedData.Columns.Add(new DataColumn("EvalPct", typeof(double)));
            TabulatedData.Columns.Add(new DataColumn("Goodness", typeof(double)));
            TabulatedData.Columns.Add(new DataColumn("White", typeof(string)));
            TabulatedData.Columns.Add(new DataColumn("Black", typeof(string)));
            TabulatedData.Columns.Add(new DataColumn("Result", typeof(string)));
            TabulatedData.Columns.Add(new DataColumn("Date", typeof(string)));
            TabulatedData.Columns.Add(new DataColumn("Eco", typeof(string)));
            TabulatedData.Columns.Add(new DataColumn("PlyCount", typeof(ushort)));
            TabulatedData.Columns.Add(new DataColumn("Event", typeof(string)));
            TabulatedData.Columns.Add(new DataColumn("IsOnlyTransposition", typeof(bool)));

            RetractionsData.Columns.Add(new DataColumn("Move", typeof(ReverseMoveWithEran)));
            RetractionsData.Columns.Add(new DataColumn("Count", typeof(ulong)));
            RetractionsData.Columns.Add(new DataColumn("WinCount", typeof(ulong)));
            RetractionsData.Columns.Add(new DataColumn("DrawCount", typeof(ulong)));
            RetractionsData.Columns.Add(new DataColumn("LossCount", typeof(ulong)));
            RetractionsData.Columns.Add(new DataColumn("Perf", typeof(double)));
            RetractionsData.Columns.Add(new DataColumn("DrawPct", typeof(double)));
            RetractionsData.Columns.Add(new DataColumn("HumanPct", typeof(double)));
            RetractionsData.Columns.Add(new DataColumn("AvgEloDiff", typeof(double)));
            RetractionsData.Columns.Add(new DataColumn("AdjustedPerf", typeof(double)));
            RetractionsData.Columns.Add(new DataColumn("Eval", typeof(ChessDBCNScore)));
            RetractionsData.Columns.Add(new DataColumn("EvalPct", typeof(double)));
            RetractionsData.Columns.Add(new DataColumn("Goodness", typeof(double)));
            RetractionsData.Columns.Add(new DataColumn("White", typeof(string)));
            RetractionsData.Columns.Add(new DataColumn("Black", typeof(string)));
            RetractionsData.Columns.Add(new DataColumn("Result", typeof(string)));
            RetractionsData.Columns.Add(new DataColumn("Date", typeof(string)));
            RetractionsData.Columns.Add(new DataColumn("Eco", typeof(string)));
            RetractionsData.Columns.Add(new DataColumn("PlyCount", typeof(ushort)));
            RetractionsData.Columns.Add(new DataColumn("Event", typeof(string)));
            RetractionsData.Columns.Add(new DataColumn("IsOnlyTransposition", typeof(bool)));

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
            TotalTabulatedData.Columns.Add(new DataColumn("Eval", typeof(ChessDBCNScore)));
            TotalTabulatedData.Columns.Add(new DataColumn("EvalPct", typeof(double)));

            WinFormsControlUtil.MakeDoubleBuffered(entriesGridView);
            entriesGridView.DataSource = TabulatedData;

            WinFormsControlUtil.MakeDoubleBuffered(totalEntriesGridView);
            totalEntriesGridView.DataSource = TotalTabulatedData;

            WinFormsControlUtil.MakeDoubleBuffered(retractionsGridView);
            retractionsGridView.DataSource = RetractionsData;

            totalEntriesGridView.Columns["Move"].Frozen = true;
            totalEntriesGridView.Columns["Move"].MinimumWidth = 50;
            totalEntriesGridView.Columns["Move"].HeaderText = "";
            totalEntriesGridView.Columns["Move"].ToolTipText = "The type of statistic displayed. See '?'";
            totalEntriesGridView.Columns["Count"].HeaderText = "N";
            totalEntriesGridView.Columns["Count"].MinimumWidth = 30;
            totalEntriesGridView.Columns["Count"].ToolTipText = "The total number of instances for this position.";
            totalEntriesGridView.Columns["WinCount"].HeaderText = "+";
            totalEntriesGridView.Columns["WinCount"].MinimumWidth = 30;
            totalEntriesGridView.Columns["WinCount"].ToolTipText = "The number of times white has won a game from this position.";
            totalEntriesGridView.Columns["DrawCount"].HeaderText = "=";
            totalEntriesGridView.Columns["DrawCount"].MinimumWidth = 30;
            totalEntriesGridView.Columns["DrawCount"].ToolTipText = "The number of times the game has been drawn from this position.";
            totalEntriesGridView.Columns["LossCount"].HeaderText = "-";
            totalEntriesGridView.Columns["LossCount"].MinimumWidth = 30;
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
            totalEntriesGridView.Columns["DrawPct"].MinimumWidth = 30;
            totalEntriesGridView.Columns["DrawPct"].HeaderText = "Dr%";
            totalEntriesGridView.Columns["DrawPct"].ToolTipText = "The % of games that ended in a draw.";
            totalEntriesGridView.Columns["HumanPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            totalEntriesGridView.Columns["HumanPct"].MinimumWidth = 42;
            totalEntriesGridView.Columns["HumanPct"].HeaderText = "Hu%";
            totalEntriesGridView.Columns["HumanPct"].ToolTipText = "The % of games being played by humans (Human + Server).";
            totalEntriesGridView.Columns["AvgEloDiff"].MinimumWidth = 30;
            totalEntriesGridView.Columns["AvgEloDiff"].HeaderText = "ΔE";
            totalEntriesGridView.Columns["AvgEloDiff"].ToolTipText = "The average elo difference between two players. WhiteElo - BlackElo.";
            totalEntriesGridView.Columns["AvgEloDiff"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            totalEntriesGridView.Columns["Eval"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            totalEntriesGridView.Columns["Eval"].MinimumWidth = 40;
            totalEntriesGridView.Columns["Eval"].HeaderText = "Ev";
            totalEntriesGridView.Columns["Eval"].ToolTipText = "Engine evaluation based on Stockfish analysis provided by https://chessdb.cn/queryc_en/.";
            totalEntriesGridView.Columns["EvalPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            totalEntriesGridView.Columns["EvalPct"].MinimumWidth = 35;
            totalEntriesGridView.Columns["EvalPct"].HeaderText = "Ev%";
            totalEntriesGridView.Columns["EvalPct"].ToolTipText = "The expected performance based on evaluation.";

            totalEntriesGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;

            entriesGridView.Columns["Move"].Frozen = true;
            entriesGridView.Columns["Move"].MinimumWidth = 50;
            entriesGridView.Columns["Move"].ToolTipText = "The move leading to the position for which stats are displayed.";
            entriesGridView.Columns["Count"].HeaderText = "N";
            entriesGridView.Columns["Count"].MinimumWidth = 30;
            entriesGridView.Columns["Count"].ToolTipText = totalEntriesGridView.Columns["Count"].ToolTipText;
            entriesGridView.Columns["WinCount"].HeaderText = "+";
            entriesGridView.Columns["WinCount"].MinimumWidth = 30;
            entriesGridView.Columns["WinCount"].ToolTipText = totalEntriesGridView.Columns["WinCount"].ToolTipText;
            entriesGridView.Columns["DrawCount"].HeaderText = "=";
            entriesGridView.Columns["DrawCount"].MinimumWidth = 30;
            entriesGridView.Columns["DrawCount"].ToolTipText = totalEntriesGridView.Columns["DrawCount"].ToolTipText;
            entriesGridView.Columns["LossCount"].HeaderText = "-";
            entriesGridView.Columns["LossCount"].MinimumWidth = 30;
            entriesGridView.Columns["LossCount"].ToolTipText = totalEntriesGridView.Columns["LossCount"].ToolTipText;
            entriesGridView.Columns["Perf"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["Perf"].HeaderText = "Wh%";
            entriesGridView.Columns["Perf"].ToolTipText = totalEntriesGridView.Columns["Perf"].ToolTipText;
            entriesGridView.Columns["Perf"].MinimumWidth = 42;
            entriesGridView.Columns["AdjustedPerf"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["AdjustedPerf"].HeaderText = "AWh%";
            entriesGridView.Columns["AdjustedPerf"].ToolTipText = totalEntriesGridView.Columns["AdjustedPerf"].ToolTipText;
            entriesGridView.Columns["AdjustedPerf"].MinimumWidth = 48;
            entriesGridView.Columns["DrawPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["DrawPct"].MinimumWidth = 30;
            entriesGridView.Columns["DrawPct"].HeaderText = "Dr%";
            entriesGridView.Columns["DrawPct"].ToolTipText = totalEntriesGridView.Columns["DrawPct"].ToolTipText;
            entriesGridView.Columns["HumanPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["HumanPct"].MinimumWidth = 42;
            entriesGridView.Columns["HumanPct"].HeaderText = "Hu%";
            entriesGridView.Columns["HumanPct"].ToolTipText = totalEntriesGridView.Columns["HumanPct"].ToolTipText;
            entriesGridView.Columns["AvgEloDiff"].MinimumWidth = 30;
            entriesGridView.Columns["AvgEloDiff"].HeaderText = "ΔE";
            entriesGridView.Columns["AvgEloDiff"].ToolTipText = totalEntriesGridView.Columns["AvgEloDiff"].ToolTipText;
            entriesGridView.Columns["AvgEloDiff"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["Result"].MinimumWidth = 45;
            entriesGridView.Columns["Result"].HeaderText = "Result";
            entriesGridView.Columns["Result"].ToolTipText = "The result of the first game with this position.";
            entriesGridView.Columns["Eco"].HeaderText = "ECO";
            entriesGridView.Columns["Eco"].ToolTipText = "The ECO code reported in the first game with this position.";
            entriesGridView.Columns["Eco"].MinimumWidth = 35;
            entriesGridView.Columns["Eco"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["PlyCount"].HeaderText = "Ply";
            entriesGridView.Columns["PlyCount"].ToolTipText = "The length in plies of the first game with this position.";
            entriesGridView.Columns["PlyCount"].MinimumWidth = 40;
            entriesGridView.Columns["PlyCount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["Event"].MinimumWidth = 50;
            entriesGridView.Columns["Event"].ToolTipText = "Even at which this position was first achieved.";
            entriesGridView.Columns["White"].MinimumWidth = 50;
            entriesGridView.Columns["White"].ToolTipText = "White player for the first game achieving this position.";
            entriesGridView.Columns["Black"].MinimumWidth = 50;
            entriesGridView.Columns["Black"].ToolTipText = "Black player for the first game achieving this position.";
            entriesGridView.Columns["Date"].MinimumWidth = 40;
            entriesGridView.Columns["Date"].ToolTipText = "Date of the first game achieving this position.";
            entriesGridView.Columns["Eval"].MinimumWidth = 40;
            entriesGridView.Columns["Eval"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["Eval"].HeaderText = "Ev";
            entriesGridView.Columns["Eval"].ToolTipText = totalEntriesGridView.Columns["Eval"].ToolTipText;
            entriesGridView.Columns["EvalPct"].MinimumWidth = 35;
            entriesGridView.Columns["EvalPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["EvalPct"].HeaderText = "Ev%";
            entriesGridView.Columns["EvalPct"].ToolTipText = totalEntriesGridView.Columns["EvalPct"].ToolTipText;
            entriesGridView.Columns["Goodness"].HeaderText = "QI";
            entriesGridView.Columns["Goodness"].ToolTipText = "Quality Index. This value represents the calcualated quality of the move based on available data and user set weights.";
            entriesGridView.Columns["Goodness"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entriesGridView.Columns["Goodness"].MinimumWidth = 40;
            entriesGridView.Columns["IsOnlyTransposition"].Visible = false;

            entriesGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;

            entriesGridView.Sort(entriesGridView.Columns["Goodness"], ListSortDirection.Descending);

            retractionsGridView.Columns["Move"].Frozen = true;
            retractionsGridView.Columns["Move"].MinimumWidth = 80;
            retractionsGridView.Columns["Move"].ToolTipText = "The move leading to the position for which stats are displayed.";
            retractionsGridView.Columns["Move"].HeaderText = "Retro\u00A0Move";
            retractionsGridView.Columns["Count"].HeaderText = "N";
            retractionsGridView.Columns["Count"].MinimumWidth = 30;
            retractionsGridView.Columns["Count"].ToolTipText = totalEntriesGridView.Columns["Count"].ToolTipText;
            retractionsGridView.Columns["WinCount"].HeaderText = "+";
            retractionsGridView.Columns["WinCount"].MinimumWidth = 30;
            retractionsGridView.Columns["WinCount"].ToolTipText = totalEntriesGridView.Columns["WinCount"].ToolTipText;
            retractionsGridView.Columns["DrawCount"].HeaderText = "=";
            retractionsGridView.Columns["DrawCount"].MinimumWidth = 30;
            retractionsGridView.Columns["DrawCount"].ToolTipText = totalEntriesGridView.Columns["DrawCount"].ToolTipText;
            retractionsGridView.Columns["LossCount"].HeaderText = "-";
            retractionsGridView.Columns["LossCount"].MinimumWidth = 30;
            retractionsGridView.Columns["LossCount"].ToolTipText = totalEntriesGridView.Columns["LossCount"].ToolTipText;
            retractionsGridView.Columns["Perf"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            retractionsGridView.Columns["Perf"].HeaderText = "Wh%";
            retractionsGridView.Columns["Perf"].ToolTipText = totalEntriesGridView.Columns["Perf"].ToolTipText;
            retractionsGridView.Columns["Perf"].MinimumWidth = 42;
            retractionsGridView.Columns["AdjustedPerf"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            retractionsGridView.Columns["AdjustedPerf"].HeaderText = "AWh%";
            retractionsGridView.Columns["AdjustedPerf"].ToolTipText = totalEntriesGridView.Columns["AdjustedPerf"].ToolTipText;
            retractionsGridView.Columns["AdjustedPerf"].MinimumWidth = 48;
            retractionsGridView.Columns["DrawPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            retractionsGridView.Columns["DrawPct"].MinimumWidth = 30;
            retractionsGridView.Columns["DrawPct"].HeaderText = "Dr%";
            retractionsGridView.Columns["DrawPct"].ToolTipText = totalEntriesGridView.Columns["DrawPct"].ToolTipText;
            retractionsGridView.Columns["HumanPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            retractionsGridView.Columns["HumanPct"].MinimumWidth = 42;
            retractionsGridView.Columns["HumanPct"].HeaderText = "Hu%";
            retractionsGridView.Columns["HumanPct"].ToolTipText = totalEntriesGridView.Columns["HumanPct"].ToolTipText;
            retractionsGridView.Columns["AvgEloDiff"].MinimumWidth = 30;
            retractionsGridView.Columns["AvgEloDiff"].HeaderText = "ΔE";
            retractionsGridView.Columns["AvgEloDiff"].ToolTipText = totalEntriesGridView.Columns["AvgEloDiff"].ToolTipText;
            retractionsGridView.Columns["AvgEloDiff"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            retractionsGridView.Columns["Result"].MinimumWidth = 45;
            retractionsGridView.Columns["Result"].HeaderText = "Result";
            retractionsGridView.Columns["Result"].ToolTipText = "The result of the first game with this position.";
            retractionsGridView.Columns["Eco"].HeaderText = "ECO";
            retractionsGridView.Columns["Eco"].ToolTipText = "The ECO code reported in the first game with this position.";
            retractionsGridView.Columns["Eco"].MinimumWidth = 35;
            retractionsGridView.Columns["Eco"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            retractionsGridView.Columns["PlyCount"].HeaderText = "Ply";
            retractionsGridView.Columns["PlyCount"].ToolTipText = "The length in plies of the first game with this position.";
            retractionsGridView.Columns["PlyCount"].MinimumWidth = 40;
            retractionsGridView.Columns["PlyCount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            retractionsGridView.Columns["Event"].MinimumWidth = 50;
            retractionsGridView.Columns["Event"].ToolTipText = "Even at which this position was first achieved.";
            retractionsGridView.Columns["White"].MinimumWidth = 50;
            retractionsGridView.Columns["White"].ToolTipText = "White player for the first game achieving this position.";
            retractionsGridView.Columns["Black"].MinimumWidth = 50;
            retractionsGridView.Columns["Black"].ToolTipText = "Black player for the first game achieving this position.";
            retractionsGridView.Columns["Date"].MinimumWidth = 40;
            retractionsGridView.Columns["Date"].ToolTipText = "Date of the first game achieving this position.";
            retractionsGridView.Columns["Eval"].MinimumWidth = 40;
            retractionsGridView.Columns["Eval"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            retractionsGridView.Columns["Eval"].HeaderText = "Ev";
            retractionsGridView.Columns["Eval"].ToolTipText = totalEntriesGridView.Columns["Eval"].ToolTipText;
            retractionsGridView.Columns["EvalPct"].MinimumWidth = 35;
            retractionsGridView.Columns["EvalPct"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            retractionsGridView.Columns["EvalPct"].HeaderText = "Ev%";
            retractionsGridView.Columns["EvalPct"].ToolTipText = totalEntriesGridView.Columns["EvalPct"].ToolTipText;
            retractionsGridView.Columns["Goodness"].HeaderText = "QI";
            retractionsGridView.Columns["Goodness"].ToolTipText = "Quality Index. This value represents the calcualated quality of the move based on available data and user set weights.";
            retractionsGridView.Columns["Goodness"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            retractionsGridView.Columns["Goodness"].MinimumWidth = 40;
            retractionsGridView.Columns["IsOnlyTransposition"].Visible = false;

            retractionsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;

            retractionsGridView.Sort(retractionsGridView.Columns["Goodness"], ListSortDirection.Descending);

            totalDataHelpButton.Width = totalEntriesGridView.RowHeadersWidth;
            totalDataHelpButton.Height = totalEntriesGridView.ColumnHeadersHeight;

            dataHelpButton.Width = entriesGridView.RowHeadersWidth;
            dataHelpButton.Height = entriesGridView.ColumnHeadersHeight;

            retractionsHelpButton.Width = retractionsGridView.RowHeadersWidth;
            retractionsHelpButton.Height = retractionsGridView.ColumnHeadersHeight;

            WinFormsControlUtil.SetThousandSeparator(entriesGridView);
            WinFormsControlUtil.SetThousandSeparator(totalEntriesGridView);
            WinFormsControlUtil.SetThousandSeparator(retractionsGridView);

            analysisAndBoardSplitContainer.SplitterDistance = 0;

            fenRichTextBox.Text = "FEN: " + FenProvider.StartPos;
            ContextMenuStrip fenRichTextBoxContextMenu = new ContextMenuStrip()
            {
                ShowImageMargin = false
            };

            ToolStripMenuItem fenRichTextBoxCopy = new ToolStripMenuItem("Copy");
            fenRichTextBoxCopy.Click += (sender, e) => Clipboard.SetText(fenRichTextBox.Text.Substring(5));
            fenRichTextBoxContextMenu.Items.Add(fenRichTextBoxCopy);
            fenRichTextBox.ContextMenuStrip = fenRichTextBoxContextMenu;

            mergeToolStripMenuItem.Enabled = false;
            appendToolStripMenuItem.Enabled = false;

            Themes = new ThemeDatabase("assets/graphics");
        }

        private void OnProcessExit(object sender, EventArgs e)
        {
            Database?.Dispose();
        }

        private void Application_Load(object sender, EventArgs e)
        {
            LoadSettings();

            try
            {
                Database = new DatabaseProxy(DatabaseTcpClientIp, DatabaseTcpClientPort);
                Database.FetchSupportManifest();
                QueryExecutor = new QueryExecutor(Database);
                QueryExecutor.DataReceived += OnDataReceived;
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

        private void OnDataReceived(object sender, KeyValuePair<QueryQueueEntry, QueryCacheEntry> p)
        {
            var key = p.Key;
            var data = p.Value;
            if (key.CurrentFen == chessBoard.GetFen())
            {
                CacheEntry = data;
                Invoke(new MethodInvoker(Repopulate));
            }
        }

        private void OnPositionChanged(object sender, EventArgs e)
        {
            var fen = chessBoard.GetFen();

            if (AnalysisForm != null)
            {
                AnalysisForm.OnPositionChanged(fen);
            }

            fenRichTextBox.Text = "FEN: " + fen;

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

        private double CalculateGoodness(
            EnumArray<GameLevel, AggregatedEntry> aggregatedEntries,
            ChessDBCNScore score
            )
        {
            var options = new GoodnessCalculator.Options
            {
                UseGames = levelHumanCheckBox.Checked || levelServerCheckBox.Checked || levelEngineCheckBox.Checked,
                UseEval = evaluationWeightCheckbox.Checked,
                UseCount = goodnessUseCountCheckbox.Checked,

                DrawScore = (double)drawScoreNumericUpDown.Value,

                EvalWeight = (double)evalWeightNumericUpDown.Value,
                GamesWeight = (double)gamesWeightNumericUpDown.Value
            };

            return GoodnessCalculator.CalculateGoodness(
                chessBoard.SideToMove(),
                aggregatedEntries,
                score,
                options
                );
        }

        private void NormalizeGoodnessValues()
        {
            double highest = GetBestGoodness();

            if (highest > 0.0)
            {
                foreach (DataRow row in TabulatedData.Rows)
                {
                    // We want to keep values below insignificantGoodnessTheshold so
                    // that they become blank.
                    if (row["Goodness"] != null && (double)row["Goodness"] >= insignificantGoodnessTheshold)
                    {
                        row["Goodness"] = (double)row["Goodness"] / highest;
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

        private void PopulateCommon(
            DataRow row,
            EnumArray<GameLevel, AggregatedEntry> aggregatedEntries,
            ChessDBCNScore totalScore
            )
        {
            long maxDisplayedEloDiff = 400;

            var total = SumEntries(aggregatedEntries);
            var totalNonEngine = SumNonEngineEntries(aggregatedEntries);

            row["Count"] = total.Count;
            row["WinCount"] = total.WinCount;
            row["DrawCount"] = total.DrawCount;
            row["LossCount"] = total.LossCount;

            var averageEloDiff = total.Count > 0 ? total.TotalEloDiff / (double)total.Count : 0.0;
            var expectedPerf = EloCalculator.GetExpectedPerformance(averageEloDiff);
            var adjustedPerf = EloCalculator.GetAdjustedPerformance(total.Perf, expectedPerf);
            if (chessBoard.SideToMove() == Player.White)
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
        }

        private void Populate(
            string san, 
            EnumArray<GameLevel, AggregatedEntry> aggregatedEntries,
            bool isOnlyTransposition, 
            ChessDBCNScore score
            )
        {
            var fen = chessBoard.GetFen();

            var row = TabulatedData.NewRow();

            PopulateCommon(row, aggregatedEntries, score);

            row["Move"] = new MoveWithSan(San.ParseSan(fen, san), san);
            row["Goodness"] = CalculateGoodness(aggregatedEntries, score);

            var totalEntry = SumEntries(aggregatedEntries);

            foreach (GameHeader header in totalEntry.FirstGame)
            {
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

        private AggregatedEntry SumEntries(EnumArray<GameLevel, AggregatedEntry> aggregatedEntries)
        {
            return aggregatedEntries.Aggregate(
                new AggregatedEntry(),
                (a, e) => { a.Combine(e.Value); return a; }
                );
        }
        private AggregatedEntry SumNonEngineEntries(EnumArray<GameLevel, AggregatedEntry> aggregatedEntries)
        {
            var res = new AggregatedEntry();
            res.Combine(aggregatedEntries[GameLevel.Human]);
            res.Combine(aggregatedEntries[GameLevel.Server]);
            return res;
        }

        private void PopulateTotal(
            string name,
            EnumArray<GameLevel, AggregatedEntry> aggregatedEntries,
            ChessDBCNScore totalScore
            )
        {
            var row = TotalTabulatedData.NewRow();

            PopulateCommon(row, aggregatedEntries, totalScore);

            row["Move"] = name;

            if (name == "Root")
            {
                PopulateFirstGameInfo(SumEntries(aggregatedEntries));
            }

            TotalTabulatedData.Rows.InsertAt(row, 0);
        }

        private void UpdateGoodness(
            string move,
            EnumArray<GameLevel, AggregatedEntry> aggregatedEntries,
            ChessDBCNScore score
            )
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

            row["Goodness"] = CalculateGoodness(aggregatedEntries, score);
        }

        private bool IsEmpty(EnumArray<GameLevel, AggregatedEntry> entry)
        {
            return entry.All(e => e.Value.Count == 0);
        }

        private ChessDBCNScore GetBestScore(Dictionary<Move, ChessDBCNScore> scores)
        {
            ChessDBCNScore best = null;
            foreach (KeyValuePair<Move, ChessDBCNScore> entry in scores)
            {
                if (best == null || (entry.Value.Value > best.Value))
                {
                    best = entry.Value;
                }
            }
            return best;
        }

        private void Populate(
            Dictionary<string, EnumArray<GameLevel, AggregatedEntry>> aggregatedEntries,
            IEnumerable<string> continuationMoves,
            Dictionary<Move, ChessDBCNScore> scores
            )
        {
            Clear();

            bool hideEmpty = hideNeverPlayedCheckBox.Checked;

            var totalEntries = new InitializedEnumArray<GameLevel, AggregatedEntry>();
            var rootEntries = new InitializedEnumArray<GameLevel, AggregatedEntry>();

            ChessDBCNScore bestScore = GetBestScore(scores);

            foreach (KeyValuePair<string, EnumArray<GameLevel, AggregatedEntry>> entriesByLevel in aggregatedEntries)
            {
                var san = entriesByLevel.Key;
                if (san == San.NullMove)
                {
                    foreach (KeyValuePair<GameLevel, AggregatedEntry> entry in entriesByLevel.Value)
                    {
                        GameLevel level = entry.Key;
                        rootEntries[level].Combine(entry.Value);
                    }
                }
                else
                {
                    foreach (KeyValuePair<GameLevel, AggregatedEntry> entry in entriesByLevel.Value)
                    {
                        GameLevel level = entry.Key;
                        totalEntries[level].Combine(entry.Value);
                    }
                }

                if (hideEmpty && IsEmpty(entriesByLevel.Value))
                {
                    continue;
                }

                if (san != San.NullMove)
                {
                    var fen = chessBoard.GetFen();
                    scores.TryGetValue(San.ParseSan(fen, san), out ChessDBCNScore score);
                    Populate(san, entriesByLevel.Value, !continuationMoves.Contains(san), score);
                }
            }

            PopulateTotal("Root", rootEntries, bestScore);
            PopulateTotal("Total", totalEntries, bestScore);

            if (goodnessNormalizeCheckbox.Checked)
            {
                NormalizeGoodnessValues();
            }

            UpdateBestGoodness();
        }

        private void UpdateGoodness(
            Dictionary<string, EnumArray<GameLevel, AggregatedEntry>> aggregatedEntries,
            Dictionary<Move, ChessDBCNScore> scores
            )
        {
            entriesGridView.SuspendLayout();

            bool hideEmpty = hideNeverPlayedCheckBox.Checked;

            foreach (KeyValuePair<string, EnumArray<GameLevel, AggregatedEntry>> entryByLevel in aggregatedEntries)
            {
                var san = entryByLevel.Key;

                if (hideEmpty && IsEmpty(entryByLevel.Value))
                {
                    continue;
                }

                if (san != San.NullMove)
                {
                    var fen = chessBoard.GetFen();
                    scores.TryGetValue(San.ParseSan(fen, san), out ChessDBCNScore score);
                    UpdateGoodness(san, entryByLevel.Value, score);
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
            BestGoodness = GetBestGoodness();
        }

        private double GetBestGoodness()
        {
            double goodness = 0.0;

            foreach (DataRow row in TabulatedData.Rows)
            {
                if (row["Goodness"] != null)
                {
                    goodness = Math.Max(goodness, (double)row["Goodness"]);
                }
            }

            return goodness;
        }

        private Dictionary<string, EnumArray<GameLevel, AggregatedEntry>> GatherEntriesByMove(
            QueryCacheEntry res, 
            List<Select> selects, 
            List<GameLevel> levels
            )
        {
            Dictionary<string, EnumArray<GameLevel, AggregatedEntry>> aggregatedEntries =
                new Dictionary<string, EnumArray<GameLevel, AggregatedEntry>>();

            foreach (Select select in selects)
            {
                var rootEntries = res.Stats.Results[0].ResultsBySelect[select].Root;
                var childrenEntries = res.Stats.Results[0].ResultsBySelect[select].Children;

                if (!aggregatedEntries.ContainsKey(San.NullMove))
                {
                    aggregatedEntries.Add(San.NullMove, new InitializedEnumArray<GameLevel, AggregatedEntry>());
                }

                foreach(GameLevel level in levels)
                {
                    aggregatedEntries[San.NullMove][level].Combine(new AggregatedEntry(rootEntries, level));
                }

                foreach (KeyValuePair<string, SegregatedEntries> entry in childrenEntries)
                {
                    if (!aggregatedEntries.ContainsKey(entry.Key))
                    {
                        aggregatedEntries.Add(entry.Key, new InitializedEnumArray<GameLevel, AggregatedEntry>());
                    }

                    foreach (GameLevel level in levels)
                    {
                        aggregatedEntries[entry.Key][level].Combine(new AggregatedEntry(entry.Value, level));
                    }
                }
            }

            return aggregatedEntries;
        }

        private void Populate(QueryCacheEntry res, List<Select> selects, List<GameLevel> levels)
        {
            var aggregatedContinuationEntries = GatherEntriesByMove(
                res,
                new List<chess_pos_db_gui.Select> { chess_pos_db_gui.Select.Continuations },
                levels
                );

            var aggregatedEntriesByLevel =
                GatherEntriesByMove(
                    res,
                    selects,
                    levels
                    );

            Populate(
                aggregatedEntriesByLevel,
                aggregatedContinuationEntries.Where(p => !IsEmpty(p.Value)).Select(p => p.Key),
                CacheEntry.Scores
                );
        }

        private void UpdateGoodness(QueryCacheEntry res, List<Select> selects, List<GameLevel> levels)
        {
            var aggregatedEntries = GatherEntriesByMove(
                res,
                selects,
                levels
                );

            UpdateGoodness(aggregatedEntries, CacheEntry.Scores);
        }

        private void Clear()
        {
            TabulatedData.Clear();
            TotalTabulatedData.Clear();
            RetractionsData.Clear();

            entriesGridView.Refresh();
            totalEntriesGridView.Refresh();
            retractionsGridView.Refresh();

            firstGameInfoRichTextBox.Clear();
        }

        private void Repopulate()
        {
            WinFormsControlUtil.SuspendDrawing(entriesGridView);
            WinFormsControlUtil.SuspendDrawing(totalEntriesGridView);
            WinFormsControlUtil.SuspendDrawing(retractionsGridView);

            SaveViewScroll();

            if (Selects.Count == 0 || Levels.Count == 0 || CacheEntry == null)
            {
                Clear();
            }
            else
            {
                if (chessBoard.SideToMove() == Player.White)
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

                Populate(CacheEntry, Selects.ToList(), Levels.ToList());
                PopulateRetractions(CacheEntry, Levels.ToList());
            }

            entriesGridView.ClearSelection();
            ReloadViewScroll();

            totalEntriesGridView.ClearSelection();
            retractionsGridView.ClearSelection();

            WinFormsControlUtil.ResumeDrawing(entriesGridView);
            WinFormsControlUtil.ResumeDrawing(totalEntriesGridView);
            WinFormsControlUtil.ResumeDrawing(retractionsGridView);
        }

        private void PopulateRetractions(QueryCacheEntry cache, List<GameLevel> levels)
        {
            var retractions = cache.Stats.Results[0].Retractions;
            var bestScore = GetBestScore(cache.Scores);

            foreach (KeyValuePair<string, SegregatedEntries> entry in retractions)
            {
                EnumArray<GameLevel, AggregatedEntry> aggregatedEntries =
                new InitializedEnumArray<GameLevel, AggregatedEntry>();

                foreach (GameLevel level in levels)
                {
                    aggregatedEntries[level].Combine(new AggregatedEntry(entry.Value, level));
                }

                PopulateRetraction(entry.Key, aggregatedEntries, bestScore);
            }
        }

        private void PopulateRetraction(
            string eran, 
            EnumArray<GameLevel, AggregatedEntry> aggregatedEntries, 
            ChessDBCNScore totalScore
            )
        {
            var fen = chessBoard.GetFen();

            var row = RetractionsData.NewRow();

            long maxDisplayedEloDiff = 400;

            var total = SumEntries(aggregatedEntries);
            var totalNonEngine = SumNonEngineEntries(aggregatedEntries);

            row["Count"] = total.Count;
            row["WinCount"] = total.WinCount;
            row["DrawCount"] = total.DrawCount;
            row["LossCount"] = total.LossCount;

            var averageEloDiff = total.Count > 0 ? total.TotalEloDiff / (double)total.Count : 0.0;
            var expectedPerf = EloCalculator.GetExpectedPerformance(averageEloDiff);
            var adjustedPerf = EloCalculator.GetAdjustedPerformance(total.Perf, expectedPerf);
            if (chessBoard.SideToMove() == Player.White)
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

            row["Move"] = new ReverseMoveWithEran(Eran.ParseEran(fen, eran), eran);
            row["Goodness"] = CalculateGoodness(aggregatedEntries, totalScore);

            foreach (GameHeader header in total.FirstGame)
            {
                row["Date"] = header.Date.ToStringOmitUnknown();
                row["Event"] = header.Event;
                row["White"] = header.White;
                row["Black"] = header.Black;
                row["Result"] = header.Result.ToStringPgnUnicodeFormat();
                row["Eco"] = header.Eco.ToString();
                row["PlyCount"] = header.PlyCount.FirstOrDefault();
            }

            row["IsOnlyTransposition"] = false;

            RetractionsData.Rows.Add(row);
        }

        private void ReloadViewScroll()
        {
            if (PrevFirstVisibleRow >= 0 && PrevFirstVisibleRow < entriesGridView.Rows.Count)
            {
                entriesGridView.FirstDisplayedScrollingRowIndex = Math.Min(PrevFirstVisibleRow, entriesGridView.Rows.Count - 1);
            }
            else if (entriesGridView.Rows.Count > 0)
            {
                entriesGridView.FirstDisplayedScrollingRowIndex = 0;
            }

            if (PrevFirstVisibleColumn >= 1 && PrevFirstVisibleColumn < entriesGridView.Columns.Count)
            {
                entriesGridView.FirstDisplayedScrollingColumnIndex = PrevFirstVisibleColumn;
            }
        }

        private void SaveViewScroll()
        {
            if (entriesGridView.Rows.Count > 0)
            {
                PrevFirstVisibleColumn = entriesGridView.FirstDisplayedScrollingColumnIndex;
                PrevFirstVisibleRow = entriesGridView.FirstDisplayedScrollingRowIndex;
            }
        }

        private void UpdateGoodness()
        {
            WinFormsControlUtil.SuspendDrawing(entriesGridView);

            SaveViewScroll();

            if (Selects.Count == 0 || Levels.Count == 0 || CacheEntry == null)
            {
                WinFormsControlUtil.ResumeDrawing(entriesGridView);
                return;
            }
            else
            {
                UpdateGoodness(CacheEntry, Selects.ToList(), Levels.ToList());
            }

            entriesGridView.ClearSelection();
            ReloadViewScroll();

            WinFormsControlUtil.ResumeDrawing(entriesGridView);
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

        private void ScheduleUpdateDataAsync()
        {
            var sig = new QueryQueueEntry(chessBoard, queryEvalCheckBox.Checked);
            QueryExecutor.ScheduleUpdateDataAsync(sig);
        }

        private void UpdateData()
        {
            if (!Database.IsOpen)
            {
                return;
            }

            SaveViewScroll();

            Clear();
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
            QueryExecutor.ResetQueueAndCache();
            UpdateDatabaseInfo();

            var files = Database.GetMergableFiles();
            var total = files.Values.Sum(l => l.Count);
            if (total >= minFilesToDisableAutoQueryOnDatabaseLoad)
            {
                autoQueryCheckbox.Checked = false;
                MessageBox.Show(string.Format(
                    "The database contains {0} files. " +
                    "You may experience very long query times and increased disk usage. " +
                    "Consider merging some database files before using this database for queries. " +
                    "To do that navigate to 'Database' -> 'Merge' with this database open. " +
                    "\n\n" +
                    "For this reason automatic queries were disabled. " +
                    "You can reenable them on your own risk if you want. "
                    , total));
            }

            OnPositionChanged(this, new EventArgs());

            mergeToolStripMenuItem.Enabled = true;
            createToolStripMenuItem.Enabled = false;
            appendToolStripMenuItem.Enabled = true;
        }

        private void UpdateDatabaseInfo()
        {
            var info = Database.GetInfo();

            if (info.IsOpen)
            {
                int averageMovesPerGame = ((int)Math.Round((double)info.TotalNumPositions() / info.TotalNumGames() / 2.0));

                databaseInfoRichTextBox.Text =
                    "Path: " + info.Path + Environment.NewLine
                    + "Games: " + info.TotalNumGames().ToString("N0")
                    + " | Plies: " + info.TotalNumPositions().ToString("N0") + Environment.NewLine
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
            mergeToolStripMenuItem.Enabled = false;
            createToolStripMenuItem.Enabled = true;
            appendToolStripMenuItem.Enabled = false;

            Database.Close();
            QueryExecutor.ResetQueueAndCache();
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
                    chessBoard.DoMove(san);
                }
            }
        }

        private bool IsInvalidDouble(object value)
        {
            return value == null || value.GetType() != typeof(double) || Double.IsNaN((double)value) || Double.IsInfinity((double)value);
        }

        private void ApplyCommonCellFormatting(DataGridView dgv, DataGridViewCellFormattingEventArgs e)
        {
            var column = dgv.Columns[e.ColumnIndex];
            var columnName = column.Name;
            var headerText = column.HeaderText;

            if (columnName == "Goodness")
            {
                if (IsInvalidDouble(e.Value) || Math.Abs((double)e.Value) < insignificantGoodnessTheshold)
                {
                    e.Value = "";
                }
                else
                {
                    e.Value = ((double)e.Value * 100).ToString("0.0");
                }
                e.FormattingApplied = true;
            }
            else if (columnName == "AvgEloDiff")
            {
                if (IsInvalidDouble(e.Value))
                {
                    e.Value = "";
                }
                else
                {
                    e.Value = e.Value.ToString();
                }
                e.FormattingApplied = true;
            }
            else if (headerText.Contains("%"))
            {
                if (IsInvalidDouble(e.Value))
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

        private void EntriesGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            ApplyCommonCellFormatting(entriesGridView, e);

            if (e.FormattingApplied)
            {
                return;
            }

            if (e.ColumnIndex == 0)
            {
                e.Value = chessBoard.NextMoveNumber() + " " + e.Value.ToString();
                e.FormattingApplied = true;
            }
        }

        private void RetractionsGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            ApplyCommonCellFormatting(retractionsGridView, e);

            if (e.FormattingApplied)
            {
                return;
            }
        }

        private void TotalEntriesGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            ApplyCommonCellFormatting(totalEntriesGridView, e);
        }

        private void EntriesGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            const double goodGoodnessTheshold = 0.95;
            const double badGoodnessTheshold = 0.8;

            var row = entriesGridView.Rows[e.RowIndex];
            var isTransposition = Convert.ToBoolean(row.Cells["IsOnlyTransposition"].Value);
            var goodness = row.Cells["Goodness"].Value;
            var isGoodGoodness = goodness != null && (double)goodness > 0.0 && (double)goodness >= goodGoodnessTheshold * BestGoodness;
            var isBadGoodness = goodness == null || (double)goodness < badGoodnessTheshold * BestGoodness;
            if ((ulong)row.Cells["Count"].Value == 0)
            {
                if (!row.DefaultCellStyle.BackColor.Equals(Color.DarkGray))
                {
                    row.DefaultCellStyle.BackColor = Color.DarkGray;
                }
            }
            else
            {
                if (isTransposition)
                {
                    if (!row.DefaultCellStyle.BackColor.Equals(Color.LightGray))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                    }
                }
                else
                {
                    if (!row.DefaultCellStyle.BackColor.Equals(Color.White))
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }

                if (isGoodGoodness)
                {
                    if (!row.Cells[0].Style.ForeColor.Equals(Color.Green))
                    {
                        row.Cells[0].Style.ForeColor = Color.Green;
                    }
                }
                else if (isBadGoodness)
                {
                    if (!row.Cells[0].Style.ForeColor.Equals(Color.Red))
                    {
                        row.Cells[0].Style.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (!row.Cells[0].Style.ForeColor.Equals(Color.Black))
                    {
                        row.Cells[0].Style.ForeColor = Color.Black;
                    }
                }
            }

            var nextSan = chessBoard.GetNextMoveSan();
            // we replace # as a workaround for now.
            if (nextSan != null && ((MoveWithSan)row.Cells["Move"].Value).San == nextSan.Replace('#', '+'))
            {
                row.DefaultCellStyle.Font = new Font(entriesGridView.Font, FontStyle.Bold);
            }
            else
            {
                if (row.DefaultCellStyle.Font != null && !row.DefaultCellStyle.Font.Equals(entriesGridView.Font))
                {
                    row.DefaultCellStyle.Font = entriesGridView.Font;
                }
            }
        }

        private void retractionsGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = retractionsGridView.Rows[e.RowIndex];
            var prevEran = chessBoard.GetPrevMoveEran();

            if (prevEran != null && ((ReverseMoveWithEran)row.Cells["Move"].Value).Eran == prevEran)
            {
                row.DefaultCellStyle.Font = new Font(retractionsGridView.Font, FontStyle.Bold);
            }
            else
            {
                if (row.DefaultCellStyle.Font != null && !row.DefaultCellStyle.Font.Equals(retractionsGridView.Font))
                {
                    row.DefaultCellStyle.Font = retractionsGridView.Font;
                }
            }
        }

        private void TotalEntriesGridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            totalEntriesGridView.Columns[e.Column.Index].Width = e.Column.Width;
        }

        private void Application_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (EmbeddedHandler != null)
            {
                EmbeddedHandler.Dispose();
            }

            if (QueryExecutor != null)
            {
                QueryExecutor.Dispose();
            }

            SaveSettings();
        }

        private void SaveSettings()
        {
            var settings = GatherSerializableSettings();
            settings.Serialize(settingsPath);
        }

        private void LoadSettings()
        {
            var settings = SerializableSettings.Deserialize(settingsPath);
            ApplySerializableSettings(settings);
        }

        private void ApplySerializableSettings(SerializableSettings settings)
        {
            Width = settings.FormWidth;
            Height = settings.FormHeight;
            autoQueryCheckbox.Checked = settings.AutoQueryCheckBoxChecked;
            queryEvalCheckBox.Checked = settings.QueryEvalCheckBoxChecked;
            levelHumanCheckBox.Checked = settings.LevelHumanCheckBoxChecked;
            levelEngineCheckBox.Checked = settings.LevelEngineCheckBoxChecked;
            levelServerCheckBox.Checked = settings.LevelServerCheckBoxChecked;
            hideNeverPlayedCheckBox.Checked = settings.HideNeverPlayedCheckBoxChecked;
            typeContinuationsCheckBox.Checked = settings.TypeContinuationsCheckBoxChecked;
            typeTranspositionsCheckBox.Checked = settings.TypeTranspositionsCheckBoxChecked;
            gamesWeightCheckbox.Checked = settings.GamesWeightCheckBoxChecked;
            evaluationWeightCheckbox.Checked = settings.EvaluationWeightCheckBoxChecked;
            goodnessUseCountCheckbox.Checked = settings.GoodnessUseCountCheckBoxChecked;
            goodnessNormalizeCheckbox.Checked = settings.GoodnessNormalizeCheckBoxChecked;
            gamesWeightNumericUpDown.Value = settings.GamesWeightNumericUpDownValue;
            evalWeightNumericUpDown.Value = settings.EvalWeightNumericUpDownValue;
            splitChessAndData.SplitterDistance = settings.SplitChessAndDataSplitterDistance;
            entriesRetractionsSplitPanel.SplitterDistance = settings.EntriesRetractionsSplitPanelSplitterDistance;

            BoardTheme boardTheme = null;
            PieceTheme pieceTheme = null;
            if (settings.BoardThemeName != null)
            {
                boardTheme = Themes.BoardThemes[settings.BoardThemeName];
            }

            if (boardTheme == null)
            {
                boardTheme = Themes.GetAnyBoardTheme();
            }

            if (settings.PieceThemeName != null)
            {
                pieceTheme = Themes.PieceThemes[settings.PieceThemeName];
            }

            if (pieceTheme == null)
            {
                pieceTheme = Themes.GetAnyPieceTheme();
            }

            chessBoard.SetThemes(boardTheme, pieceTheme);
        }

        private SerializableSettings GatherSerializableSettings()
        {
            return new SerializableSettings
            {
                AutoQueryCheckBoxChecked = autoQueryCheckbox.Checked,
                QueryEvalCheckBoxChecked = queryEvalCheckBox.Checked,
                LevelHumanCheckBoxChecked = levelHumanCheckBox.Checked,
                LevelEngineCheckBoxChecked = levelEngineCheckBox.Checked,
                LevelServerCheckBoxChecked = levelServerCheckBox.Checked,
                HideNeverPlayedCheckBoxChecked = hideNeverPlayedCheckBox.Checked,
                TypeContinuationsCheckBoxChecked = typeContinuationsCheckBox.Checked,
                TypeTranspositionsCheckBoxChecked = typeTranspositionsCheckBox.Checked,
                GamesWeightCheckBoxChecked = gamesWeightCheckbox.Checked,
                EvaluationWeightCheckBoxChecked = evaluationWeightCheckbox.Checked,
                GoodnessUseCountCheckBoxChecked = goodnessUseCountCheckbox.Checked,
                GoodnessNormalizeCheckBoxChecked = goodnessNormalizeCheckbox.Checked,
                GamesWeightNumericUpDownValue = gamesWeightNumericUpDown.Value,
                EvalWeightNumericUpDownValue = evalWeightNumericUpDown.Value,
                SplitChessAndDataSplitterDistance = splitChessAndData.SplitterDistance,
                EntriesRetractionsSplitPanelSplitterDistance = entriesRetractionsSplitPanel.SplitterDistance,
                FormWidth = Width,
                FormHeight = Height,
                BoardThemeName = chessBoard.BoardImages?.Name,
                PieceThemeName = chessBoard.PieceImages?.Name
            };
        }

        private void HideNeverPlayedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Repopulate();
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

        private void GamesWeightCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGoodness();
        }

        private void EvaluationWeightCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGoodness();
        }

        private void SetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AnalysisForm == null)
            {
                EmbeddedHandler = new ApplicationEmbeddedAnalysisHandler(this);
                AnalysisForm = new EngineAnalysisForm(EngineProfiles, EmbeddedHandler);
                AnalysisForm.FormClosed += OnAnalysisFormClosed;
                AnalysisForm.Show();
            }
            else if (!AnalysisForm.Visible)
            {
                AnalysisForm.Show();
            }
        }

        private void OnAnalysisFormClosed(object sender, FormClosedEventArgs e)
        {
            if (EmbeddedHandler != null)
            {
                EmbeddedHandler.Dispose();
            }

            EmbeddedHandler = null;
            AnalysisForm = null;
        }

        private void GamesWeightNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (gamesWeightCheckbox.Checked)
            {
                UpdateGoodness();
            }
        }

        private void ProfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AnalysisForm == null)
            {
                var form = new EngineProfilesForm(EngineProfiles, EngineProfilesFormMode.Manage);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("You have to stop analysis to manage engine profiles.");
            }
        }

        private void TotalDataHelpButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "When a database is loaded and queried this table shows 2 rows:\n\n" +
                "Root:\n\n" +
                "(if Continuations and Transpositions boxes are checked)\n" +
                "= number of games that played any move that resulted in achieving the current board position\n\n" +
                "(if only Continuations box is checked)\n" +
                "= number of games that played the actual move that achieved the current board position\n\n" +
                "(if only Transposition box is checked)\n" +
                "= number of games that achieved the current board position as a result of moves " +
                "other than the one that was actually made\n\n\n" +
                "Total:\n\n" +
                "(if only Continuations box is checked)\n" +
                "= number of game continuations one move ahead from the current board position\n\n" +
                "(if only Transposition box is checked)\n" +
                "= number of game transpositions one move ahead joining the game from other board positions\n\n" +
                "(if Continuations and Transpositions boxes are checked)\n" +
                "= sum of the above two values\n\n\n" +
                "Notes:\n\n" +
                "Root answers \"how did this position get here?\" while Total " +
                "answers \"where could this position potentially go from here?\" " +
                "Root is backward-looking; Total is forward-looking.\n\n" +
                "Typically Total is greater than or equal to Root in almost all cases, " +
                "but in some instances it may be slightly smaller. " +
                "This is a result of database games reaching their " +
                "last move with no continuation one move ahead.",
                "Help",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
        }

        private void dataHelpButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "When a database is loaded and queried this table shows one row for each " +
                "move that is legal from the current position. The values shown are aggregated " +
                "over the selected 'select's and 'level's. Each row includes the WDL for each move, " +
                "calculated performance and move quality; and depending on the database format used " +
                "it may also include average elo difference between white and black, and information " +
                "about the first game to [reach the position (transpositions)]/[play the move (continuations)].",
                "Help",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
        }

        private void retractionsHelpButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "When a database that supports this feature is loaded and queried " +
                "this table shows one row for each retro move from the current position. " +
                "This shows the results of a 'backward' query, compared to the results of the 'forward' query " +
                "that are shown in the two tables above. It currently doesn't respect " +
                "transposition settings.",
                "Help",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
        }

        private void ThemesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var boardTheme = chessBoard.BoardImages;
            var pieceTheme = chessBoard.PieceImages;
            using (var dialog = new ThemeSelectionForm(Themes, boardTheme, pieceTheme))
            {
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    chessBoard.SetThemes(
                        dialog.SelectedBoardTheme,
                        dialog.SelectedPieceTheme
                        );
                }
            }
        }

        private void retractionsGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var reverseMove = (retractionsGridView[0, e.RowIndex].Value as ReverseMoveWithEran).ReverseMove;
            var fen = chessBoard.GetFen();
            var game = new ChessGame(fen);
            var prevGame = reverseMove.AppliedTo(game);
            var prevFen = prevGame.GetFen();
            prevGame.MakeMove(reverseMove.Move, true);
            chessBoard.SetGame(prevFen, prevGame);
        }

        private void MergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new DatabaseMergeForm(Database))
            {
                form.ShowDialog();
            }
        }

        private void appendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new DatabaseCreationForm(Database))
            {
                form.ShowDialog();
            }

            UpdateDatabaseInfo();
        }

        private void drawScoreNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpdateGoodness();
        }
    }
}
