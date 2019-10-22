using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public partial class DatabaseCreationForm : Form
    {
        private bool finishedWithErrors = false;

        public bool OpenAfterFinished { get { return openCheckBox.Checked && !finishedWithErrors; } }

        public string DatabasePath { get { return destinationFolderTextBox.Text; } }

        public ulong NumGames { get; private set; }
        public ulong NumPositions { get; private set; }
        public ulong NumSkippedGames { get; private set; }
        public bool KeepFormAlive { get; private set; }

        private readonly DatabaseProxy database;

        public DatabaseCreationForm(DatabaseProxy db)
        {
            InitializeComponent();

            database = db;
            mergeProgressBar.Maximum = 100;
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

        private void SetDestinationFolderButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog browser = new FolderBrowserDialog())
            {
                browser.ShowNewFolderButton = true;

                if (browser.ShowDialog() == DialogResult.OK)
                {
                    destinationFolderTextBox.Text = browser.SelectedPath;
                }
            }
        }

        private void ClearTempFolderButton_Click(object sender, EventArgs e)
        {
            tempFolderTextBox.Clear();
        }

        private void DatabaseCreationForm_Load(object sender, EventArgs e)
        {
            databaseFormatComboBox.Items.Add("db_alpha");
            databaseFormatComboBox.Items.Add("db_beta");
            databaseFormatComboBox.SelectedItem = "db_beta";
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

        private void AddHumanPgnsButton_Click(object sender, EventArgs e)
        {
            AddPgns(humanPgnsDataGridView);
        }

        private void AddEnginePgnsButton_Click(object sender, EventArgs e)
        {
            AddPgns(enginePgnsDataGridView);
        }

        private void AddServerPgnsButton_Click(object sender, EventArgs e)
        {
            AddPgns(serverPgnsDataGridView);
        }

        private List<JsonValue> GetPgns(DataGridView dgv)
        {
            var list = new List<JsonValue>();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                list.Add(row.Cells[0].Value.ToString());
            }

            return list;
        }

        private List<JsonValue> GetHumanPgns()
        {
            return GetPgns(humanPgnsDataGridView);
        }

        private List<JsonValue> GetEnginePgns()
        {
            return GetPgns(enginePgnsDataGridView);
        }

        private List<JsonValue> GetServerPgns()
        {
            return GetPgns(serverPgnsDataGridView);
        }

        private DataGridViewRow FindRowWithPgnFile(string path)
        {
            DataGridView[] dgvs = new DataGridView[] {
                humanPgnsDataGridView,
                enginePgnsDataGridView,
                serverPgnsDataGridView
            };

            foreach (var dgv in dgvs)
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if ((string)row.Cells[0].Value == path)
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

        private void SetMergeProgress(int progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(SetMergeProgress), progress);
            }
            else
            {
                mergeProgressBar.Value = progress;
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
                    NumGames = progressReport["num_games"];
                    NumPositions = progressReport["num_positions"];
                    NumSkippedGames = progressReport["num_skipped_games"];
                }
            }
            else if (progressReport["operation"] == "merge")
            {
                SetMergeProgress((int)(progressReport["overall_progress"] * 100.0));
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

        private void BuildDatabase(JsonObject request)
        {
            try
            {
                database.Create(request, ProgressCallback);
                finishedWithErrors = false;
                MessageBox.Show(
                    string.Format("Finished.\nGames imported: {0}\nGames skipped: {1}\nPosition imported: {2}",
                        NumGames,
                        NumSkippedGames,
                        NumPositions
                    )
                );
            }
            catch
            {
                finishedWithErrors = true;
                MessageBox.Show("Finished with errors");
            }

            if (OpenAfterFinished)
            {
                database.Open(DatabasePath);
            }

            KeepFormAlive = false;

            if (InvokeRequired)
            {
                Invoke(new Action(Close));
            }
            else
            {
                Close();
            }
        }

        private void DisableInput()
        {
            setDestinationFolderButton.Enabled = false;
            setTempFolderButton.Enabled = false;
            clearTempFolderButton.Enabled = false;
            buildButton.Enabled = false;
            mergeCheckBox.Enabled = false;
            openCheckBox.Enabled = false;
            databaseFormatComboBox.Enabled = false;
            humanPgnsDataGridView.Enabled = false;
            enginePgnsDataGridView.Enabled = false;
            serverPgnsDataGridView.Enabled = false;
            addHumanPgnsButton.Enabled = false;
            addEnginePgnsButton.Enabled = false;
            addServerPgnsButton.Enabled = false;

            KeepFormAlive = true;
        }

        private async void BuildButton_Click(object sender, EventArgs e)
        {
            DisableInput();

            JsonObject request = new JsonObject
            {
                { "command", "create" },
                { "destination_path", destinationFolderTextBox.Text },
                { "merge", mergeCheckBox.Checked },
                { "report_progress", true },
                { "database_format", databaseFormatComboBox.Text },
                { "human_pgns", new JsonArray(GetHumanPgns()) },
                { "engine_pgns", new JsonArray(GetEnginePgns()) },
                { "server_pgns", new JsonArray(GetServerPgns()) }
            };
            if (tempFolderTextBox.Text != "")
            {
                request.Add("temporary_path", tempFolderTextBox.Text);
            }

            await Task.Run(() => BuildDatabase(request));
        }

        private void DatabaseCreationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (KeepFormAlive)
            {
                e.Cancel = true;
            }
        }
    }
}
