using chess_pos_db_gui.src.chess;
using chess_pos_db_gui.src.util;
using ChessDotNet;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public partial class EngineAnalysisForm : Form
    {
        private static readonly string profilesPath = "data/engine_profiles.json";

        private UciEngineProfileStorage Profiles { get; set; }

        private EngineOptionsForm OptionsForm { get; set; }

        private UciEngineProxy Engine { get; set; }

        private BlockingQueue<UciInfoResponse> PendingInfoResponses { get; set; }

        private System.Timers.Timer InfoUpdateTimer { get; set; }

        private DataTable AnalysisData { get; set; }

        private string Fen { get; set; }
        private bool IsScoreSortDescending { get; set; }

        private DataGridViewColumn SortedColumn { get; set; }

        public EngineAnalysisForm()
        {
            InitializeComponent();

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

            foreach (DataGridViewColumn column in analysisDataGridView.Columns)
            {
                if (column.ValueType == typeof(long) || column.ValueType == typeof(int))
                {
                    column.DefaultCellStyle.Format = "N0";
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            UpdateAnalysisButtonName();
            closeToolStripMenuItem.Enabled = false;
            optionsToolStripMenuItem.Enabled = false;
            toggleAnalyzeButton.Enabled = false;

            PendingInfoResponses = new BlockingQueue<UciInfoResponse>();

            Fen = FenProvider.StartPos;

            Profiles = new UciEngineProfileStorage(profilesPath);

            InfoUpdateTimer = new System.Timers.Timer
            {
                Interval = 2000,
                AutoReset = true
            };
            InfoUpdateTimer.Elapsed += ProcessPendingInfoReponses;
            InfoUpdateTimer.Enabled = true;

            ClearEngineIdInfo();
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
            analysisDataGridView.Columns["Time"].MinimumWidth = 110;
            analysisDataGridView.Columns["Time"].HeaderText = "Time [hh:mm:ss]";
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
            analysisDataGridView.Columns["TBHits"].HeaderText = "TBHits";
            analysisDataGridView.Columns["TBHits"].MinimumWidth = 60;
            analysisDataGridView.Columns["TBHits"].ToolTipText = "Number of successful tablebase lookups";
            analysisDataGridView.Columns["PV"].HeaderText = "PV";
            analysisDataGridView.Columns["PV"].ToolTipText = "The principal variation - engine's predicted line";
            analysisDataGridView.Columns["ScoreInt"].Visible = false;
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
            engineIdNameLabel.Text = "Name: " + Engine.Name;
            engineIdAuthorLabel.Text = "Author: " + Engine.Author;
        }

        private void ClearEngineIdInfo()
        {
            enginePathLabel.Text = "Path: ";
            engineIdNameLabel.Text = "Name: ";
            engineIdAuthorLabel.Text = "Author: ";
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

            toggleAnalyzeButton.Enabled = true;
            optionsToolStripMenuItem.Enabled = true;
            closeToolStripMenuItem.Enabled = true;

            FillEngineIdInfo();
            UpdateAnalysisButtonName();

            OptionsForm = new EngineOptionsForm(Engine.CurrentOptions);
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
                Engine.Stop();
                Engine.Quit();
                Engine = null;
            }

            toggleAnalyzeButton.Enabled = false;
            optionsToolStripMenuItem.Enabled = false;
            closeToolStripMenuItem.Enabled = false;

            ClearEngineIdInfo();
            UpdateAnalysisButtonName();

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
        }

        private void ToggleAnalyzeButton_Click(object sender, EventArgs e)
        {
            if (Engine.IsSearching)
            {
                Engine.Stop();
            }
            else
            {
                Engine.GoInfinite(OnInfoResponse, Fen);
            }

            UpdateAnalysisButtonName();
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
            foreach (KeyValuePair<string, UciInfoResponse> response in responseByMove)
            {
                var info = response.Value;
                var move = Lan.LanToMoveWithSan(info.Fen, response.Key);
                var multipv = info.MultiPV.Or(0);
                System.Data.DataRow row = FindOrCreateRowByMoveOrMultiPV(newAnalysisData, move, multipv);

                FillRowFromInfo(row, info);
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

            var newAnalysisData = AnalysisData.Copy();

            UpdateAnalysisData(newAnalysisData, responseByMove);

            ApplyNewAnalysisData(newAnalysisData);
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

        private void RemoveSuperfluousInfoRow(DataTable dt)
        {
            var maxNumPvs = Engine.PvCount;
            if (dt.Rows.Count > maxNumPvs)
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

        private void RemoveSuperfluousInfoRows(DataTable dt)
        {
            var maxNumPvs = Engine.PvCount;
            while (dt.Rows.Count > maxNumPvs)
            {
                RemoveSuperfluousInfoRow(dt);
            }
        }

        private void OnInfoResponse(UciInfoResponse info)
        {
            PendingInfoResponses.Enqueue(info);
        }

        private void FillRowFromInfo(DataRow row, UciInfoResponse info)
        {
            row["Move"] = Lan.LanToMoveWithSan(info.Fen, info.PV.Or(new List<string>()).FirstOrDefault());
            row["Depth"] = info.Depth.Or(0);
            row["SelDepth"] = info.SelDepth.Or(0);
            var score = info.Score.Or(new UciScore(0, UciScoreType.Cp, UciScoreBoundType.Exact));
            row["Score"] = score;
            row["ScoreInt"] = score.ToInteger();
            row["Time"] = TimeSpan.FromSeconds(info.Time.Or(0) / 1000);
            row["Nodes"] = info.Nodes.Or(0);
            row["NPS"] = info.Nps.Or(0);
            row["MultiPV"] = info.MultiPV.Or(0);
            row["TBHits"] = info.TBHits.Or(0);
            row["PV"] = Lan.PVToString(info.Fen, info.PV.FirstOrDefault());
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
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new EngineProfilesForm(Profiles);
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
    }
}
