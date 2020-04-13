using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public partial class PgnInputForm : Form
    {
        public bool WasCancelled { get; private set; }
        public string Pgn { get; private set; }
        public string MoveText { get; private set; }

        public PgnInputForm()
        {
            InitializeComponent();

            WasCancelled = false;
            Pgn = "";
            if (Clipboard.ContainsText())
            {
                pgnTextBox.Text = Clipboard.GetText();
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            WasCancelled = false;
            Pgn = pgnTextBox.Text;
            var lines = Pgn.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
                );
            MoveText = string.Concat(lines.Where(s => !s.StartsWith("[")));
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            WasCancelled = true;
            Close();
        }
    }
}
