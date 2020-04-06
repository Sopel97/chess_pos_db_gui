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
            Engine.GoInfinite(delegate (UciInfoResponse e) { });
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OptionsForm == null)
            {
                OptionsForm = new EngineOptionsForm(Engine.CurrentOptions);
                OptionsForm.Show();
            }
        }

        private void EngineAnalysisForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Engine.Stop();
            Engine.Quit();
        }
    }
}
