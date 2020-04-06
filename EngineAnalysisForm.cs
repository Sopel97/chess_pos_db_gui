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

        public EngineAnalysisForm()
        {
            InitializeComponent();

            Engine = new UciEngineProxy("stockfish.exe");
            SetToggleButtonName();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OptionsForm == null)
            {
                OptionsForm = new EngineOptionsForm(Engine.CurrentOptions);
                OptionsForm.FormClosing += OnOptionsFormClosing;
            }

            if (!OptionsForm.Visible)
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

        private void EngineAnalysisForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Engine.Stop();
            Engine.Quit();
            Engine = null;
            OptionsForm.Close();
        }

        private void toggleAnalyzeButton_Click(object sender, EventArgs e)
        {
            if (Engine.IsSearching)
            {
                Engine.Stop();
            }
            else
            {
                Engine.GoInfinite(delegate (UciInfoResponse ee) { });
            }

            SetToggleButtonName();
        }

        private void SetToggleButtonName()
        {
            toggleAnalyzeButton.Text = Engine.IsSearching ? "Stop" : "Start";
        }
    }
}
