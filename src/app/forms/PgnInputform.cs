using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public partial class PgnInputForm : Form
    {
        public bool WasCancelled { get; private set; }
        public string MoveText { get; private set; }

        public PgnInputForm()
        {
            InitializeComponent();

            okButton.DialogResult = DialogResult.OK;
            cancelButton.DialogResult = DialogResult.Cancel;

            WasCancelled = false;
            MoveText = "";

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
            MoveText = StripPgnHeader(pgnTextBox.Text);
            MoveText = MoveText.Replace(Environment.NewLine, " ");
            MoveText = MoveText.Replace('\n', ' ');
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            WasCancelled = true;
            Close();
        }

        private void PgnInputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.Cancel)
            {
                WasCancelled = true;
            }
        }
    }
}
