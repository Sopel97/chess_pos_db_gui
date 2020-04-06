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
            analysisDataGridView.Columns["Time"].MinimumWidth = 80;
            analysisDataGridView.Columns["Time"].HeaderText = "Time";
            analysisDataGridView.Columns["Nodes"].HeaderText = "Nodes";
            analysisDataGridView.Columns["NPS"].HeaderText = "NPS";
            analysisDataGridView.Columns["MultiPV"].HeaderText = "MultiPV";
            analysisDataGridView.Columns["TBHits"].HeaderText = "TBHits";
            analysisDataGridView.Columns["PV"].HeaderText = "PV";

            analysisDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;

            SetToggleButtonName();
            closeToolStripMenuItem.Enabled = false;
            optionsToolStripMenuItem.Enabled = false;
            toggleAnalyzeButton.Enabled = false;

            Fen = null;

            ClearnIdInfo();
        }

        private MoveWithSan LanToMoveWithSan(string lan)
        {
            ChessGame game = new ChessGame(Fen);
            var from = lan.Substring(0, 2);
            var to = lan.Substring(2, 2);
            Player player = game.WhoseTurn;
            var move = lan.Length == 5 ? new ChessDotNet.Move(from, to, player, lan[4]) : new ChessDotNet.Move(from, to, player);
            game.MakeMove(move, true);

            var detailedMove = game.Moves.Last();
            return new MoveWithSan(move, detailedMove.SAN);
        }

        private string StringifyPV(IList<string> lans)
        {
            return lans.Aggregate((a, b) => a + " " + LanToMoveWithSan(b).San);
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

        private void ClearnIdInfo()
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

            toggleAnalyzeButton.Enabled = true;
            optionsToolStripMenuItem.Enabled = true;
            closeToolStripMenuItem.Enabled = true;

            FillIdInfo();

            OptionsForm = new EngineOptionsForm(Engine.CurrentOptions);
            OptionsForm.FormClosing += OnOptionsFormClosing;
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

            ClearnIdInfo();

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
                Engine.GoInfinite(delegate (UciInfoResponse ee) { }, Fen);
            }

            SetToggleButtonName();
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

        private void analysisDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }
    }
}
