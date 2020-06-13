using System;
using System.Collections.Generic;
using System.Drawing;
using System.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public partial class EpdDumpForm : Form
    {
        private ulong NumGames;
        private ulong NumPosIn;
        private ulong NumPosOut;

        public bool KeepFormAlive { get; private set; }

        private readonly DatabaseProxy database;

        public EpdDumpForm(DatabaseProxy db)
        {
            InitializeComponent();

            Icon = Properties.Resources.application_icon;

            database = db;
            dumpProgressBar.Maximum = 100;
        }

        private void SetTempFolderButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog browser = new FolderBrowserDialog())
            {
                browser.ShowNewFolderButton = true;

                if (browser.ShowDialog() == DialogResult.OK)
                {
                    tempFolderTextBox.Text = browser.SelectedPath;
                }
            }
        }

        private void setSecondaryTempFolderButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog browser = new FolderBrowserDialog())
            {
                browser.ShowNewFolderButton = true;

                if (browser.ShowDialog() == DialogResult.OK)
                {
                    secondaryTempFolderTextBox.Text = browser.SelectedPath;
                }
            }
        }

        private void SetOutputPathButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog browser = new SaveFileDialog())
            {
                if (browser.ShowDialog() == DialogResult.OK)
                {
                    outputPathTextBox.Text = browser.FileName;
                    if (!outputPathTextBox.Text.EndsWith(".epd"))
                    {
                        outputPathTextBox.Text += ".epd";
                    }
                }
            }
        }

        private void ClearTempFolderButton_Click(object sender, EventArgs e)
        {
            tempFolderTextBox.Clear();
        }

        private void clearSecondaryTempFolderButton_Click(object sender, EventArgs e)
        {
            secondaryTempFolderTextBox.Clear();
        }

        private void DatabaseCreationForm_Load(object sender, EventArgs e)
        {
        }

        private void AddPath(DataGridView dgv, string path)
        {
            int row = dgv.Rows.Add();
            dgv[0, row].Value = path;
            dgv[1, row].Value = "0%";
        }

        private void AddPaths(DataGridView dgv, string[] paths)
        {
            foreach (string path in paths)
            {
                AddPath(dgv, path);
            }
        }

        private void AddPgns(DataGridView dgv)
        {
            using (OpenFileDialog browser = new OpenFileDialog())
            {
                browser.Filter = "PGN Files (*.pgn)|*.pgn|All files (*.*)|*.*";
                browser.CheckFileExists = true;
                browser.Multiselect = true;
                browser.ValidateNames = true;

                if (browser.ShowDialog() == DialogResult.OK)
                {
                    AddPaths(dgv, browser.FileNames);
                }
            }
        }

        private void AddPgnsButton_Click(object sender, EventArgs e)
        {
            AddPgns(pgnsDataGridView);
        }

        private List<string> GetPgns(DataGridView dgv)
        {
            var list = new List<string>();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                list.Add(row.Cells[0].Value.ToString());
            }

            return list;
        }

        private List<string> GetPgns()
        {
            return GetPgns(pgnsDataGridView);
        }

        private DataGridViewRow FindRowWithPgnFile(string path)
        {
            DataGridView[] dgvs = new DataGridView[] {
                pgnsDataGridView
            };

            var fullPath = System.IO.Path.GetFullPath(path);
            foreach (var dgv in dgvs)
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (System.IO.Path.GetFullPath((string)row.Cells[0].Value).Equals(fullPath))
                    {
                        return row;
                    }
                }
            }

            return null;
        }

        private void SetFileProgress(string path, float progress)
        {
            var row = FindRowWithPgnFile(path);
            if (row != null)
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<string, float>(SetFileProgress), path, progress);
                }
                else
                {
                    row.Cells[1].Value = ((int)(progress * 100)).ToString() + "%";
                    row.Selected = true;
                }
            }
        }

        private void SetDumpProgress(int progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(SetDumpProgress), progress);
            }
            else
            {
                dumpProgressBar.Value = progress;
                dumpProgressLabel.Text = progress.ToString() + "%";
            }
        }

        private void ProgressCallback(JsonValue progressReport)
        {
            if (progressReport["operation"] == "import")
            {
                if (progressReport.ContainsKey("imported_file_path"))
                {
                    SetFileProgress(progressReport["imported_file_path"], 1.0f);
                }

                if (progressReport["finished"] == true)
                {
                }
            }
            else if (progressReport["operation"] == "dump")
            {
                SetDumpProgress((int)(progressReport["overall_progress"] * 100.0));

                if (progressReport["finished"] == true)
                {
                    NumGames = progressReport["num_games"];
                    NumPosIn = progressReport["num_in_positions"];
                    NumPosOut = progressReport["num_out_positions"];
                }
            }
            else
            {
                return;
            }

            if (InvokeRequired)
            {
                Invoke(new Action(Refresh));
            }
            else
            {
                Refresh();
            }
        }

        private void Dump(List<string> pgns, string outPath, List<string> tempPaths, int minCount, int maxPly)
        {
            try
            {
                database.Dump(pgns, outPath, tempPaths, minCount, maxPly, ProgressCallback);
                MessageBox.Show(
                    String.Format(
                        "Finished.\nGames processed: {0}\nPositions processed: {1}\nPositions dumped: {2}",
                        NumGames,
                        NumPosIn,
                        NumPosOut
                    )
                );

                if (InvokeRequired)
                {
                    Invoke(new Action(Close));
                }
                else
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Finished with errors. " + ex.Message);

                if (InvokeRequired)
                {
                    Invoke(new Action(EnableInput));
                }
                else
                {
                    EnableInput();
                }
            }

            KeepFormAlive = false;
        }
        private void EnableInput()
        {
            setOutputPathButton.Enabled = true;
            setTempFolderButton.Enabled = true;
            clearTempFolderButton.Enabled = true;
            dumpButton.Enabled = true;
            pgnsDataGridView.AllowUserToAddRows = true;
            pgnsDataGridView.AllowUserToDeleteRows = true;
            pgnsDataGridView.ReadOnly = false;
            addPgnsButton.Enabled = true;

            KeepFormAlive = false;
        }

        private void DisableInput()
        {
            setOutputPathButton.Enabled = false;
            setTempFolderButton.Enabled = false;
            clearTempFolderButton.Enabled = false;
            dumpButton.Enabled = false;
            pgnsDataGridView.AllowUserToAddRows = false;
            pgnsDataGridView.AllowUserToDeleteRows = false;
            pgnsDataGridView.ReadOnly = true;
            addPgnsButton.Enabled = false;

            KeepFormAlive = true;
        }

        private async void DumpButton_Click(object sender, EventArgs e)
        {
            DisableInput();

            await Task.Run(() => Dump(
                GetPgns(),
                outputPathTextBox.Text,
                GetTempPaths(),
                (int)minCountInput.Value,
                (int)maxPlyNumericUpDown.Value
                ));
        }

        private List<string> GetTempPaths()
        {
            var paths = new List<string>();

            if (tempFolderTextBox.Text != "")
            {
                paths.Add(tempFolderTextBox.Text);
            }

            if (secondaryTempFolderTextBox.Text != "")
            {
                paths.Add(secondaryTempFolderTextBox.Text);
            }

            return paths;
        }

        private void DatabaseCreationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (KeepFormAlive)
            {
                e.Cancel = true;
            }
        }

        private void PgnsDataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = pgnsDataGridView.Rows[e.RowIndex];
            if (row.Cells[1].Value.Equals("100%"))
            {
                row.DefaultCellStyle.BackColor = Color.LimeGreen;
            }
        }
    }
}
