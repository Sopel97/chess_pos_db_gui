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
    public partial class EngineOptionsForm : Form
    {
        private IList<UciOption> Options { get; set; }

        public EngineOptionsForm(IList<UciOption> options)
        {
            InitializeComponent();

            SuspendLayout();
            Options = options;
            AddControlsForOptions(Options);
            ResumeLayout();
        }

        private void AddControlsForOptions(IList<UciOption> options)
        {
            foreach(var opt in options)
            {
                optionsFlowLayoutPanel.Controls.Add(opt.CreateControl());
            }
        }

        private void EngineOptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
