using ChessDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public partial class EngineAnalysisForm : Form
    {
        private EngineOptionsForm OptionsForm { get; set; }
        private UciEngineProxy Engine { get; set; }
        private DataTable AnalysisData { get; set; }
        private string Fen { get; set; }

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

            MakeDoubleBuffered(analysisDataGridView);
            analysisDataGridView.DataSource = AnalysisData;

            analysisDataGridView.Columns["Move"].MinimumWidth = 50;
            analysisDataGridView.Columns["Move"].HeaderText = "Move";
            analysisDataGridView.Columns["Depth"].MinimumWidth = 40;
            analysisDataGridView.Columns["Depth"].HeaderText = "D";
            analysisDataGridView.Columns["SelDepth"].MinimumWidth = 40;
            analysisDataGridView.Columns["SelDepth"].HeaderText = "SD";
            analysisDataGridView.Columns["Score"].MinimumWidth = 60;
            analysisDataGridView.Columns["Score"].HeaderText = "Score";
            analysisDataGridView.Columns["Time"].MinimumWidth = 110;
            analysisDataGridView.Columns["Time"].HeaderText = "Time [hh:mm:ss]";
            analysisDataGridView.Columns["Nodes"].HeaderText = "Nodes";
            analysisDataGridView.Columns["Nodes"].MinimumWidth = 60;
            analysisDataGridView.Columns["NPS"].HeaderText = "NPS";
            analysisDataGridView.Columns["NPS"].MinimumWidth = 60;
            analysisDataGridView.Columns["MultiPV"].HeaderText = "MultiPV";
            analysisDataGridView.Columns["MultiPV"].MinimumWidth = 40;
            analysisDataGridView.Columns["TBHits"].HeaderText = "TBHits";
            analysisDataGridView.Columns["TBHits"].MinimumWidth = 60;
            analysisDataGridView.Columns["PV"].HeaderText = "PV";

            analysisDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;

            foreach (DataGridViewColumn column in analysisDataGridView.Columns)
            {
                if (column.ValueType == typeof(long) || column.ValueType == typeof(int))
                {
                    column.DefaultCellStyle.Format = "N0";
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            SetToggleButtonName();
            closeToolStripMenuItem.Enabled = false;
            optionsToolStripMenuItem.Enabled = false;
            toggleAnalyzeButton.Enabled = false;

            Fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

            ClearIdInfo();
        }

        private static MoveWithSan LanToMoveWithSan(string fen, string lan)
        {
            if (lan == null || lan == "0000") return new MoveWithSan(null, "null");

            ChessGame game = new ChessGame(fen);
            var from = lan.Substring(0, 2);
            var to = lan.Substring(2, 2);
            Player player = game.WhoseTurn;
            var move = lan.Length == 5 ? new ChessDotNet.Move(from, to, player, lan[4]) : new ChessDotNet.Move(from, to, player);
            game.MakeMove(move, true);
            var detailedMove = game.Moves.Last();
            return new MoveWithSan(move, detailedMove.SAN);
        }

        private string StringifyPV(string fen, IList<string> lans)
        {
            ChessGame game = new ChessGame(fen);
            foreach(var lan in lans)
            {
                var from = lan.Substring(0, 2);
                var to = lan.Substring(2, 2);
                Player player = game.WhoseTurn;
                var move = lan.Length == 5 ? new ChessDotNet.Move(from, to, player, lan[4]) : new ChessDotNet.Move(from, to, player);
                game.MakeMove(move, true);
            }

            return game.Moves.Select(d => d.SAN).Aggregate((a, b) => a + " " + b);
        }

        private static void MakeDoubleBuffered(DataGridView dgv)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);

            pi.SetValue(dgv, true, null);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OptionsForm != null && !OptionsForm.Visible)
            {
                OptionsForm.Show();
            }
        }

        private void OnOptionsFormClosing(object sender, FormClosingEventArgs e)
        {
            if (Engine != null)
            {
                Engine.UpdateUciOptions();
            }
        }

        private void FillIdInfo()
        {
            enginePathLabel.Text = "Path: " + Engine.Path;
            engineIdNameLabel.Text = "Name: " + Engine.Name;
            engineIdAuthorLabel.Text = "Author: " + Engine.Author;
        }

        private void ClearIdInfo()
        {
            enginePathLabel.Text = "Path: ";
            engineIdNameLabel.Text = "Name: ";
            engineIdAuthorLabel.Text = "Author: ";
        }

        private void LoadEngine(string path)
        {
            if (Engine != null)
            {
                UnloadEngine();
            }

            Engine = new UciEngineProxy(path);

            Engine.AnalysisStarted += OnAnalysisStarted;

            toggleAnalyzeButton.Enabled = true;
            optionsToolStripMenuItem.Enabled = true;
            closeToolStripMenuItem.Enabled = true;

            FillIdInfo();

            OptionsForm = new EngineOptionsForm(Engine.CurrentOptions);
            OptionsForm.FormClosing += OnOptionsFormClosing;

            AnalysisData.Clear();
        }

        private void OnAnalysisStarted(object sender, EventArgs e)
        {
            AnalysisData.Clear();
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

            ClearIdInfo();

            OptionsForm = null;
        }

        private void EngineAnalysisForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnloadEngine();
            if (OptionsForm != null)
            {
                OptionsForm.Close();
            }
        }

        private void toggleAnalyzeButton_Click(object sender, EventArgs e)
        {
            if (Engine.IsSearching)
            {
                Engine.Stop();
            }
            else
            {
                Engine.GoInfinite(OnInfoResponse, Fen);
            }

            SetToggleButtonName();
        }

        private void OnInfoResponse(UciInfoResponse info)
        {
            try
            {
                foreach (var _ in info.Score)
                {
                    var multipv = info.MultiPV.Or(0);
                    System.Data.DataRow row = (System.Data.DataRow)Invoke(new Func<System.Data.DataRow>(delegate () { return FindOrCreateRowByMultiPV(multipv); }));
                    Invoke(new Func<bool>(delegate () { FillRowFromInfo(row, info); return true; }));
                }
            }
            catch(ObjectDisposedException e)
            {
                // Since we call in another thread we may happen after actually closing the window
            }
        }

        private void FillRowFromInfo(DataRow row, UciInfoResponse info)
        {
            row["Move"] = LanToMoveWithSan(Fen, info.PV.Or(new List<string>()).FirstOrDefault());
            row["Depth"] = info.Depth.Or(0);
            row["SelDepth"] = info.Depth.Or(0);
            row["Score"] = info.Score.Or(new UciScore(0, UciScoreType.Cp, UciScoreBoundType.Exact));
            row["Time"] = TimeSpan.FromMilliseconds(info.Time.Or(0));
            row["Nodes"] = info.Nodes.Or(0);
            row["NPS"] = info.Nps.Or(0);
            row["MultiPV"] = info.MultiPV.Or(0);
            row["TBHits"] = info.TBHits.Or(0);
            row["PV"] = StringifyPV(Fen, info.PV.FirstOrDefault());
        }

        private System.Data.DataRow FindOrCreateRowByMultiPV(int multipv)
        {
            System.Data.DataRow row = null;
            foreach(var rr in AnalysisData.Rows)
            {
                System.Data.DataRow r = (System.Data.DataRow)rr;
                if (r["MultiPV"] == null) continue;
                if ((int)r["MultiPV"] == multipv)
                {
                    row = r;
                    break;
                }
            }

            if (row == null)
            {
                row = AnalysisData.NewRow();
                AnalysisData.Rows.Add(row);
            }

            return row;
        }

        private void SetToggleButtonName()
        {
            if (Engine == null)
            {
                toggleAnalyzeButton.Text = "Start";
            }
            else
            {
                toggleAnalyzeButton.Text = Engine.IsSearching ? "Stop" : "Start";
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path = "stockfish.exe";
            LoadEngine(path);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnloadEngine();
        }

        public void OnPositionChanged(string fen)
        {
            Fen = fen;
            Engine.SetPosition(Fen);
        }
    }
}
