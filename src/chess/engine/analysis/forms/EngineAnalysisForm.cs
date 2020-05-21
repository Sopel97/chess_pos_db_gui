using chess_pos_db_gui.src.chess;
using chess_pos_db_gui.src.chess.engine.analysis;
using chess_pos_db_gui.src.chess.engine.analysis.controls;
using chess_pos_db_gui.src.util;
using ChessDotNet;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public partial class EngineAnalysisForm : Form
    {
        private class SerializableSettings : JsonSerializable<SerializableSettings>
        {
            public int FormWidth { get; set; } = 816;
            public int FormHeight { get; set; } = 488;
        }

        private static readonly int maxEmbeddedAnalysisRows = 3;
        private static readonly string settingsPath = "data/engine_analysis_form/settings.json";

        private UciEngineProfileStorage EngineProfiles { get; set; }

        private EngineOptionsForm OptionsForm { get; set; }

        private UciEngineProxy Engine { get; set; }

        private BlockingQueue<UciInfoResponse> PendingInfoResponses { get; set; }

        private System.Timers.Timer InfoUpdateTimer { get; set; }

        private DataTable AnalysisData { get; set; }

        private DataTable EmbeddedAnalysisData { get; set; }

        private EmbeddedEngineAnalysisControl EmbeddedControl { get; set; }

        private string Fen { get; set; }
        private Player SideToMove { get; set; }

        private bool IsScoreSortDescending { get; set; }

        private DataGridViewColumn SortedColumn { get; set; }

        private EmbeddedAnalysisHandler EmbeddedHandler { get; set; }

        private bool IsEmbedded { get; set; }

        public EngineAnalysisForm(UciEngineProfileStorage profiles, EmbeddedAnalysisHandler embeddedHandler = null)
        {
            InitializeComponent();

            EngineProfiles = profiles;

            EmbeddedControl = new EmbeddedEngineAnalysisControl();

            AnalysisData = new DataTable();
            AnalysisData.Columns.Add(new DataColumn("Move", typeof(MoveWithSan)));
            AnalysisData.Columns.Add(new DataColumn("Depth", typeof(int)));
            AnalysisData.Columns.Add(new DataColumn("SelDepth", typeof(int)));
            AnalysisData.Columns.Add(new DataColumn("Score", typeof(UciScore)));
            AnalysisData.Columns.Add(new DataColumn("Time", typeof(TimeSpan)));
            AnalysisData.Columns.Add(new DataColumn("Nodes", typeof(long)));
            AnalysisData.Columns.Add(new DataColumn("NPS", typeof(long)));
            AnalysisData.Columns.Add(new DataColumn("MultiPV", typeof(int)));
            AnalysisData.Columns.Add(new DataColumn("TBHits", typeof(long)));
            AnalysisData.Columns.Add(new DataColumn("PV", typeof(string)));
            AnalysisData.Columns.Add(new DataColumn("ScoreInt", typeof(int)));

            WinFormsControlUtil.MakeDoubleBuffered(analysisDataGridView);
            analysisDataGridView.DataSource = AnalysisData;

            SetupColumns();

            analysisDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;

            EmbeddedAnalysisData = new DataTable();
            EmbeddedAnalysisData.Columns.Add(new DataColumn("Move", typeof(MoveWithSan)));
            EmbeddedAnalysisData.Columns.Add(new DataColumn("D/SD", typeof(KeyValuePair<int, int>)));
            EmbeddedAnalysisData.Columns.Add(new DataColumn("Score", typeof(UciScore)));
            EmbeddedAnalysisData.Columns.Add(new DataColumn("Time", typeof(TimeSpan)));
            EmbeddedAnalysisData.Columns.Add(new DataColumn("Nodes", typeof(long)));
            EmbeddedAnalysisData.Columns.Add(new DataColumn("NPS", typeof(long)));
            EmbeddedAnalysisData.Columns.Add(new DataColumn("TBHits", typeof(long)));
            EmbeddedAnalysisData.Columns.Add(new DataColumn("ScoreInt", typeof(int)));
            EmbeddedAnalysisData.Columns.Add(new DataColumn("MultiPV", typeof(int)));

            WinFormsControlUtil.MakeDoubleBuffered(EmbeddedControl.AnalysisDataGridView);
            EmbeddedControl.AnalysisDataGridView.ColumnHeadersDefaultCellStyle = 
                analysisDataGridView.ColumnHeadersDefaultCellStyle;

            foreach (DataGridViewColumn column in analysisDataGridView.Columns)
            {
                if (column.ValueType == typeof(long) || column.ValueType == typeof(int))
                {
                    column.DefaultCellStyle.Format = "N0";
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            UpdateAnalysisButtonName();
            EmbeddedControl.ToggleAnalysisButton.Enabled = false;
            closeToolStripMenuItem.Enabled = false;
            optionsToolStripMenuItem.Enabled = false;
            toggleAnalyzeButton.Enabled = false;
            embedButton.Enabled = false;

            IsEmbedded = false;
            EmbeddedHandler = embeddedHandler;
            if (EmbeddedHandler == null)
            {
                embedButton.Enabled = false;
            }

            PendingInfoResponses = new BlockingQueue<UciInfoResponse>();

            Fen = FenProvider.StartPos;
            SideToMove = Player.White;

            InfoUpdateTimer = new System.Timers.Timer
            {
                Interval = 2000,
                AutoReset = true
            };
            InfoUpdateTimer.Elapsed += ProcessPendingInfoReponses;
            InfoUpdateTimer.Enabled = true;

            ClearEngineIdInfo();
        }

        public void ForceStopEmbeddedAnalysis(EmbeddedAnalysisHandler handler)
        {
            EmbeddedHandler.OnEmbeddedAnalysisEnded();
            ClearEmbeddedAnalysisPanel();
            IsEmbedded = false;
        }

        private void SetupColumns()
        {
            analysisDataGridView.Columns["Move"].MinimumWidth = 50;
            analysisDataGridView.Columns["Move"].HeaderText = "Move";
            analysisDataGridView.Columns["Move"].ToolTipText = "Move suggested by the engine";
            analysisDataGridView.Columns["Depth"].MinimumWidth = 40;
            analysisDataGridView.Columns["Depth"].HeaderText = "D";
            analysisDataGridView.Columns["Depth"].ToolTipText = "The depth in plies reached by the engine";
            analysisDataGridView.Columns["SelDepth"].MinimumWidth = 40;
            analysisDataGridView.Columns["SelDepth"].HeaderText = "SD";
            analysisDataGridView.Columns["SelDepth"].ToolTipText = "The depth in plies reached by the engine in some extended lines";
            analysisDataGridView.Columns["Score"].MinimumWidth = 60;
            analysisDataGridView.Columns["Score"].HeaderText = "Score";
            analysisDataGridView.Columns["Score"].ToolTipText = "Score of the move in pawns for the side to move.";
            analysisDataGridView.Columns["Time"].MinimumWidth = 60;
            analysisDataGridView.Columns["Time"].HeaderText = "Time";
            analysisDataGridView.Columns["Time"].ToolTipText = "Time spend to produce the move";
            analysisDataGridView.Columns["Nodes"].HeaderText = "Nodes";
            analysisDataGridView.Columns["Nodes"].MinimumWidth = 60;
            analysisDataGridView.Columns["Nodes"].ToolTipText = "Number of nodes examined";
            analysisDataGridView.Columns["NPS"].HeaderText = "NPS";
            analysisDataGridView.Columns["NPS"].MinimumWidth = 60;
            analysisDataGridView.Columns["NPS"].ToolTipText = "Average number of nodes being examined per second";
            analysisDataGridView.Columns["MultiPV"].HeaderText = "MultiPV";
            analysisDataGridView.Columns["MultiPV"].MinimumWidth = 40;
            analysisDataGridView.Columns["MultiPV"].ToolTipText = "Engines internal id of the line";
            analysisDataGridView.Columns["TBHits"].HeaderText = "TB\u00A0Hits";
            analysisDataGridView.Columns["TBHits"].MinimumWidth = 55;
            analysisDataGridView.Columns["TBHits"].ToolTipText = "Number of successful tablebase lookups";
            analysisDataGridView.Columns["PV"].HeaderText = "PV";
            analysisDataGridView.Columns["PV"].ToolTipText = "The principal variation - engine's predicted line";
            analysisDataGridView.Columns["ScoreInt"].Visible = false;
            analysisDataGridView.Columns["MultiPV"].Visible = false;
        }

        private void SetupEmbeddedColumns()
        {
            EmbeddedControl.AnalysisDataGridView.Columns["Move"].MinimumWidth = 50;
            EmbeddedControl.AnalysisDataGridView.Columns["Move"].HeaderText = "Move";
            EmbeddedControl.AnalysisDataGridView.Columns["Move"].ToolTipText = "Move suggested by the engine";
            EmbeddedControl.AnalysisDataGridView.Columns["D/SD"].MinimumWidth = 40;
            EmbeddedControl.AnalysisDataGridView.Columns["D/SD"].HeaderText = "D/SD";
            EmbeddedControl.AnalysisDataGridView.Columns["D/SD"].ToolTipText = "The depth/selective depth in plies reached by the engine";
            EmbeddedControl.AnalysisDataGridView.Columns["Score"].MinimumWidth = 45;
            EmbeddedControl.AnalysisDataGridView.Columns["Score"].HeaderText = "Score";
            EmbeddedControl.AnalysisDataGridView.Columns["Score"].ToolTipText = "Score of the move in pawns for the side to move.";
            EmbeddedControl.AnalysisDataGridView.Columns["Time"].MinimumWidth = 60;
            EmbeddedControl.AnalysisDataGridView.Columns["Time"].HeaderText = "Time";
            EmbeddedControl.AnalysisDataGridView.Columns["Time"].ToolTipText = "Time spend to produce the move";
            EmbeddedControl.AnalysisDataGridView.Columns["Nodes"].HeaderText = "Nodes";
            EmbeddedControl.AnalysisDataGridView.Columns["Nodes"].MinimumWidth = 45;
            EmbeddedControl.AnalysisDataGridView.Columns["Nodes"].ToolTipText = "Number of nodes examined";
            EmbeddedControl.AnalysisDataGridView.Columns["NPS"].HeaderText = "NPS";
            EmbeddedControl.AnalysisDataGridView.Columns["NPS"].MinimumWidth = 45;
            EmbeddedControl.AnalysisDataGridView.Columns["NPS"].ToolTipText = "Average number of nodes being examined per second";
            EmbeddedControl.AnalysisDataGridView.Columns["TBHits"].HeaderText = "TB\u00A0Hits";
            EmbeddedControl.AnalysisDataGridView.Columns["TBHits"].MinimumWidth = 55;
            EmbeddedControl.AnalysisDataGridView.Columns["TBHits"].ToolTipText = "Number of successful tablebase lookups";
            EmbeddedControl.AnalysisDataGridView.Columns["ScoreInt"].Visible = false;
            EmbeddedControl.AnalysisDataGridView.Columns["MultiPV"].Visible = false;
        }

        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OptionsForm != null && !OptionsForm.Visible)
            {
                OptionsForm.Show();
            }
        }

        private void OnOptionsFormClosing(object sender, FormClosingEventArgs e)
        {
            if (OptionsForm.Discard)
            {
                Engine.DiscardUciOptionChanges();
            }
            else if (Engine != null)
            {
                Engine.UpdateUciOptions();
            }
        }

        private void OnOptionsFormVisibilityChanged(object sender, EventArgs e)
        {
            if (!OptionsForm.Visible)
            {
                if (OptionsForm.Discard)
                {
                    Engine.DiscardUciOptionChanges();
                }
                else if (Engine != null)
                {
                    Engine.UpdateUciOptions();
                }
            }
        }

        private void FillEngineIdInfo()
        {
            enginePathLabel.Text = "Path: " + Engine.Path;
            engineIdNameLabel.Text = "Name: " + Engine.IdName;
            engineIdAuthorLabel.Text = "Author: " + Engine.IdAuthor;

            EmbeddedControl.EngineIdNameLabel.Text = Engine.IdName;
        }

        private void ClearEngineIdInfo()
        {
            enginePathLabel.Text = "Path: ";
            engineIdNameLabel.Text = "Name: ";
            engineIdAuthorLabel.Text = "Author: ";

            EmbeddedControl.EngineIdNameLabel.Text = "";
        }

        private void LoadEngine(UciEngineProfile profile)
        {
            if (Engine != null)
            {
                UnloadEngine();
            }

            Engine = profile.LoadEngine();

            AfterEngineLoaded();
        }

        private void AfterEngineLoaded()
        {
            Engine.AnalysisStarted += OnAnalysisStarted;

            FillEngineIdInfo();

            EmbeddedControl.ToggleAnalysisButton.Enabled = true;
            toggleAnalyzeButton.Enabled = true;
            optionsToolStripMenuItem.Enabled = true;
            closeToolStripMenuItem.Enabled = true;
            if (EmbeddedHandler != null)
            {
                embedButton.Enabled = true;
            }
            UpdateAnalysisButtonName();

            OptionsForm = new EngineOptionsForm(Engine.ScratchOptions, false);
            OptionsForm.FormClosing += OnOptionsFormClosing;
            OptionsForm.VisibleChanged += OnOptionsFormVisibilityChanged;

            AnalysisData.Clear();
        }

        private void OnAnalysisStarted(object sender, EventArgs e)
        {
            PendingInfoResponses.Clear();

            var sortedColumn = analysisDataGridView.SortedColumn;
            var sortedOrder = analysisDataGridView.SortOrder;
            AnalysisData.Clear();
            if (sortedColumn != null)
            {
                analysisDataGridView.Sort(
                    sortedColumn, 
                    sortedOrder == SortOrder.Ascending 
                        ? ListSortDirection.Ascending 
                        : ListSortDirection.Descending
                    );
            }
        }

        private void UnloadEngine()
        {
            if (Engine != null)
            {
                Engine.StopAnalysis();
                Engine.Quit();
                Engine = null;
            }

            EmbeddedControl.ToggleAnalysisButton.Enabled = false;
            toggleAnalyzeButton.Enabled = false;
            optionsToolStripMenuItem.Enabled = false;
            closeToolStripMenuItem.Enabled = false;
            embedButton.Enabled = false;

            ClearEngineIdInfo();
            UpdateAnalysisButtonName();

            if (OptionsForm != null)
            {
                OptionsForm.Dispose();
            }
            OptionsForm = null;
        }

        private void EngineAnalysisForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnloadEngine();
            if (OptionsForm != null)
            {
                OptionsForm.Close();
            }

            InfoUpdateTimer.Dispose();

            SaveSettings();
        }

        private void ToggleAnalyzeButton_Click(object sender, EventArgs e)
        {
            if (Engine.IsSearching)
            {
                Engine.StopAnalysis();
            }
            else
            {
                Engine.StartAnalysis(OnInfoResponse, Fen);
            }

            UpdateAnalysisButtonName();
        }

        private void ApplyNewEmbeddedAnalysisData(DataTable newAnalysisData)
        {
            try
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    EmbeddedAnalysisData = newAnalysisData;

                    EmbeddedControl.AnalysisDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    var oldSortOrder = EmbeddedControl.AnalysisDataGridView.SortOrder;
                    var oldSortedColumn = oldSortOrder == SortOrder.None ? 0 : EmbeddedControl.AnalysisDataGridView.SortedColumn.Index;
                    RemoveSuperfluousInfoRows(EmbeddedAnalysisData, maxEmbeddedAnalysisRows);
                    EmbeddedControl.AnalysisDataGridView.DataSource = EmbeddedAnalysisData;
                    SetupEmbeddedColumns();
                    if (oldSortOrder != SortOrder.None)
                    {
                        EmbeddedControl.AnalysisDataGridView.Sort(
                            EmbeddedControl.AnalysisDataGridView.Columns[oldSortedColumn],
                            oldSortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending
                            );
                    }
                    EmbeddedControl.AnalysisDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
                }));
            }
            catch (Exception)
            {

            }
        }

        private void ApplyNewAnalysisData(DataTable newAnalysisData)
        {
            try
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    AnalysisData = newAnalysisData;

                    analysisDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    var oldSortOrder = analysisDataGridView.SortOrder;
                    var oldSortedColumn = oldSortOrder == SortOrder.None ? 0 : analysisDataGridView.SortedColumn.Index;
                    RemoveSuperfluousInfoRows(AnalysisData);
                    analysisDataGridView.DataSource = AnalysisData;
                    SetupColumns();
                    if (oldSortOrder != SortOrder.None)
                    {
                        analysisDataGridView.Sort(
                            analysisDataGridView.Columns[oldSortedColumn],
                            oldSortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending
                            );
                    }
                    analysisDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
                }));
            }
            catch (Exception)
            {

            }
        }

        private void UpdateAnalysisData(DataTable newAnalysisData, Dictionary<string, UciInfoResponse> responseByMove)
        {
            // update only one info per move
            foreach ((string lan, UciInfoResponse info) in responseByMove)
            {
                var move = Lan.LanToMoveWithSan(info.Fen, lan);
                var multipv = info.MultiPV.Or(0);
                System.Data.DataRow row = FindOrCreateRowByMoveOrMultiPV(newAnalysisData, move, multipv);

                FillRowFromInfo(row, info);
            }
        }

        private void UpdateEmbeddedAnalysisData(DataTable newAnalysisData, Dictionary<string, UciInfoResponse> responseByMove)
        {
            // update only one info per move
            foreach ((string lan, UciInfoResponse info) in responseByMove)
            {
                var move = Lan.LanToMoveWithSan(info.Fen, lan);
                var multipv = info.MultiPV.Or(0);
                System.Data.DataRow row = FindOrCreateRowByMoveOrMultiPV(newAnalysisData, move, multipv);

                FillEmbeddedRowFromInfo(row, info);
            }
        }

        private void ProcessPendingInfoReponses(object sender, System.Timers.ElapsedEventArgs e)
        {
            Dictionary<string, UciInfoResponse> responseByMove = new Dictionary<string, UciInfoResponse>();

            // collect per move so we don't do updates for duplicates
            for (; ; )
            {
                var info = PendingInfoResponses.TryDequeue();
                if (info == null)
                {
                    break;
                }

                if (info.Fen != Fen)
                {
                    continue;
                }

                foreach (var _ in info.Score)
                {
                    // replace so we have the latest
                    responseByMove[info.GetMoveLan()] = info;
                }
            }

            if (responseByMove.Count == 0)
            {
                return;
            }

            if (IsEmbedded)
            {
                var newAnalysisData = EmbeddedAnalysisData.Copy();
                UpdateEmbeddedAnalysisData(newAnalysisData, responseByMove);
                ApplyNewEmbeddedAnalysisData(newAnalysisData);
            }
            else
            {
                var newAnalysisData = AnalysisData.Copy();
                UpdateAnalysisData(newAnalysisData, responseByMove);
                ApplyNewAnalysisData(newAnalysisData);
            }
        }

        private bool IsLhsMoreSuperfluousThanRhs(System.Data.DataRow lhs, System.Data.DataRow rhs)
        {
            return 
                ((TimeSpan)lhs["Time"] < (TimeSpan)rhs["Time"])
                || (
                   (TimeSpan)lhs["Time"] == (TimeSpan)rhs["Time"]
                   && (int)lhs["MultiPV"] >= (int)rhs["MultiPV"]
                   );
        }

        private void RemoveSuperfluousInfoRow(DataTable dt, int maxRows)
        {
            if (dt.Rows.Count > maxRows)
            {
                System.Data.DataRow rowToRemove = dt.Rows[0];
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    if (IsLhsMoreSuperfluousThanRhs(row, rowToRemove))
                    {
                        rowToRemove = row;
                    }
                }
                dt.Rows.Remove(rowToRemove);
            }
        }

        private void RemoveSuperfluousInfoRows(DataTable dt, int maxRows = -1)
        {
            if (maxRows == -1)
            {
                maxRows = Engine.PvCount;
            }
            else if (Engine.PvCount < maxRows)
            {
                maxRows = Engine.PvCount;
            }

            while (dt.Rows.Count > maxRows)
            {
                RemoveSuperfluousInfoRow(dt, maxRows);
            }
        }

        private void OnInfoResponse(UciInfoResponse info)
        {
            PendingInfoResponses.Enqueue(info);
        }

        private int GetSideToMoveScoreMultiplier()
        {
            return SideToMove == Player.White ? 1 : -1;
        }

        private void FillRowFromInfo(DataRow row, UciInfoResponse info)
        {
            row["Move"] = Lan.LanToMoveWithSan(info.Fen, info.PV.Or(new List<string>()).FirstOrDefault());
            row["Depth"] = info.Depth.Or(0);
            row["SelDepth"] = info.SelDepth.Or(0);
            var score = info.Score.Or(new UciScore(0, UciScoreType.Cp, UciScoreBoundType.Exact));
            row["Score"] = score;
            row["ScoreInt"] = score.ToInteger() * GetSideToMoveScoreMultiplier();
            row["Time"] = TimeSpan.FromSeconds(info.Time.Or(0) / 1000);
            row["Nodes"] = info.Nodes.Or(0);
            row["NPS"] = info.Nps.Or(0);
            row["MultiPV"] = info.MultiPV.Or(0);
            row["TBHits"] = info.TBHits.Or(0);
            row["PV"] = Lan.PVToString(info.Fen, info.PV.FirstOrDefault());
        }

        private void FillEmbeddedRowFromInfo(DataRow row, UciInfoResponse info)
        {
            row["Move"] = Lan.LanToMoveWithSan(info.Fen, info.PV.Or(new List<string>()).FirstOrDefault());
            row["D/SD"] = new KeyValuePair<int, int>(info.Depth.Or(0), info.SelDepth.Or(0));
            var score = info.Score.Or(new UciScore(0, UciScoreType.Cp, UciScoreBoundType.Exact));
            row["Score"] = score;
            row["ScoreInt"] = score.ToInteger() * GetSideToMoveScoreMultiplier();
            row["Time"] = TimeSpan.FromSeconds(info.Time.Or(0) / 1000);
            row["Nodes"] = info.Nodes.Or(0);
            row["NPS"] = info.Nps.Or(0);
            row["MultiPV"] = info.MultiPV.Or(0);
            row["TBHits"] = info.TBHits.Or(0);
        }

        private void FillEmbeddedRowFromNormal(DataRow row, DataRow from)
        {
            row["Move"] = from["Move"];
            row["D/SD"] = new KeyValuePair<int, int>((int)from["Depth"], (int)from["SelDepth"]);
            row["Score"] = from["Score"];
            row["ScoreInt"] = from["ScoreInt"];
            row["Time"] = from["Time"];
            row["Nodes"] = from["Nodes"];
            row["NPS"] = from["NPS"];
            row["MultiPV"] = from["MultiPV"];
            row["TBHits"] = from["TBHits"];
        }

        private void TransferDataToEmbedded()
        {
            EmbeddedAnalysisData.Clear();

            foreach (DataRow from in AnalysisData.Rows)
            {
                System.Data.DataRow row = EmbeddedAnalysisData.NewRow();
                FillEmbeddedRowFromNormal(row, from);
                EmbeddedAnalysisData.Rows.Add(row);
            }

            RemoveSuperfluousInfoRows(EmbeddedAnalysisData, maxEmbeddedAnalysisRows);
        }

        private System.Data.DataRow FindOrCreateRowByMoveOrMultiPV(DataTable dt, MoveWithSan move, int multipv)
        {
            System.Data.DataRow row = null;

            // First look for a matching move.
            // If no matching move found then it means we have a new entry and we replace with matching multipv.
            foreach (var rr in dt.Rows)
            {
                System.Data.DataRow r = (System.Data.DataRow)rr;
                if (r["Move"] != null && ((MoveWithSan)r["Move"]).San == move.San)
                {
                    row = r;
                    break;
                }
            }

            if (row == null)
            {
                foreach (var rr in dt.Rows)
                {
                    System.Data.DataRow r = (System.Data.DataRow)rr;
                    if (r["MultiPV"] != null && (int)r["MultiPV"] == multipv)
                    {
                        row = r;
                        break;
                    }
                }
            }

            if (row == null)
            {
                row = dt.NewRow();
                dt.Rows.Add(row);
            }

            return row;
        }

        private void UpdateAnalysisButtonName()
        {
            toggleAnalyzeButton.Text = 
                (Engine != null && Engine.IsSearching)
                ? "Stop" 
                : "Start";

            EmbeddedControl.ToggleAnalysisButton.Text =
                (Engine != null && Engine.IsSearching)
                ? "Stop"
                : "Start";
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new EngineProfilesForm(EngineProfiles, EngineProfilesFormMode.Select);
            form.ShowDialog();
            if (form.SelectedProfile != null)
            {
                LoadEngine(form.SelectedProfile);
            }
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnloadEngine();
        }

        public void OnPositionChanged(string fen)
        {
            if (Fen != fen)
            {
                Fen = fen;
                SideToMove = new ChessGame(Fen).WhoseTurn;
                Engine.SetPosition(fen);
            }
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

        private void ClearEmbeddedAnalysisPanel()
        {
            EmbeddedControl.AnalysisDataGridView.CellFormatting -= OnEmbeddedAnalysisDataGridViewCellFormatting;
            EmbeddedControl.ToggleAnalysisButton.Click -= ToggleAnalyzeButton_Click;

            EmbeddedControl.Detach();
        }

        private void SetupEmbeddedAnalysisPanel(Panel panel)
        {
            EmbeddedControl.Attach(panel, EmbeddedAnalysisData);

            EmbeddedControl.AnalysisDataGridView.CellFormatting += OnEmbeddedAnalysisDataGridViewCellFormatting;
            EmbeddedControl.ToggleAnalysisButton.Click += ToggleAnalyzeButton_Click;

            SetupEmbeddedColumns();
            TransferDataToEmbedded();
        }

        private string NumberToShortString(long number)
        {
            if (number >= 1_000_000_000_000)
                return ((double)number / 1_000_000_000_000).ToString("0.##") + "T";

            if (number >= 1_000_000_000)
                return ((double)number / 1_000_000_000).ToString("0.##") + "B";

            if (number >= 1_000_000)
                return ((double)number / 1_000_000).ToString("0.##") + "M";

            if (number >= 1_000)
                return ((double)number / 1_000).ToString("0.##") + "k";

            return number.ToString();
        }

        private void OnEmbeddedAnalysisDataGridViewCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var column = EmbeddedControl.AnalysisDataGridView.Columns[e.ColumnIndex];
            var columnName = column.Name;

            if (columnName == "D/SD")
            {
                if (e.Value == null || e.Value.GetType() != typeof(KeyValuePair<int, int>))
                {
                    e.Value = "";
                }
                else
                {
                    var kv = (KeyValuePair<int, int>)e.Value;
                    e.Value = kv.Key.ToString() + "/" + kv.Value.ToString();
                }
                e.FormattingApplied = true;
            }
            else if (columnName == "Nodes" || columnName == "NPS" || columnName == "TBHits")
            {
                if (e.Value == null || e.Value.GetType() != typeof(long))
                {
                    e.Value = "";
                }
                else
                {
                    e.Value = NumberToShortString((long)e.Value);
                }
                e.FormattingApplied = true;
            }
        }

        private void EmbedButton_Click(object sender, EventArgs e)
        {
            if (EmbeddedHandler != null)
            {
                var panel = EmbeddedHandler.PrepareAndGetEmbeddedAnalysisPanel(this);
                SetupEmbeddedAnalysisPanel(panel);
                IsEmbedded = true;
                Hide();
            }
        }

        private void EngineAnalysisForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                if (IsEmbedded)
                {
                    IsEmbedded = false;
                    EmbeddedHandler.OnEmbeddedAnalysisEnded();
                    ClearEmbeddedAnalysisPanel();
                }
            }
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
        }

        private SerializableSettings GatherSerializableSettings()
        {
            return new SerializableSettings
            {
                FormWidth = Width,
                FormHeight = Height
            };
        }

        private void EngineAnalysisForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }
    }
}
