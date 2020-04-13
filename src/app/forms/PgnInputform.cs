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

        private string StripPgnHeader(string pgn)
        {
            var lines = pgn.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
                );
            return string.Concat(lines.Where(s => !s.StartsWith("[")));
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            WasCancelled = false;
            Pgn = StripPgnHeader(pgnTextBox.Text);
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            WasCancelled = true;
            Close();
        }
    }
}
