using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public partial class EngineAnalysisForm : Form
    {
        private EngineOptionsForm OptionsForm { get; set; }
        private UciEngineProxy Engine { get; set; }
        private string Fen { get; set; }

        public EngineAnalysisForm()
        {
            InitializeComponent();

            SetToggleButtonName();
            closeToolStripMenuItem.Enabled = false;
            optionsToolStripMenuItem.Enabled = false;
            toggleAnalyzeButton.Enabled = false;

            Fen = null;

            ClearnIdInfo();
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
    }
}
