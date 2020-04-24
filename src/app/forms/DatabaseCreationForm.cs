using System;
using System.Collections.Generic;
using System.Drawing;
using System.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public partial class DatabaseCreationForm : Form
    {
        private bool finishedWithErrors = true;
        private bool openAfterFinished = true;

        public bool OpenAfterFinished { get { return openAfterFinished && !finishedWithErrors; } }

        public string DatabasePath { get { return destinationFolderTextBox.Text; } }

        public ulong NumGames { get; private set; }
        public ulong NumPositions { get; private set; }
        public ulong NumSkippedGames { get; private set; }

        public bool KeepFormAlive { get; private set; }

        private readonly DatabaseProxy database;

        private IList<string> SupportedExtensions { get; set; }

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
            var supported = database.GetSupportedDatabaseTypes();
            foreach (var type in supported)
            {
                databaseFormatComboBox.Items.Add(type);
            }

            databaseFormatComboBox.SelectedItem = supported[supported.Count - 1];
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

        private void AddFiles(DataGridView dgv)
        {
            using (OpenFileDialog browser = new OpenFileDialog())
            {
                browser.Filter = GetCurrentFilesFilter();
                browser.CheckFileExists = true;
                browser.Multiselect = true;
                browser.ValidateNames = true;

                if (browser.ShowDialog() == DialogResult.OK)
                {
                    AddPaths(dgv, browser.FileNames);
                }
            }
        }

        private string GetCurrentFilesFilter()
        {
            var parts = SupportedExtensions.Select(ext => "*" + ext);

            return string.Format(
                "Chess game files ({0})|{0}",
                string.Join(";", parts)
                );
        }

        private void AddHumanPgnsButton_Click(object sender, EventArgs e)
        {
            AddFiles(humanPgnsDataGridView);
        }

        private void AddEnginePgnsButton_Click(object sender, EventArgs e)
        {
            AddFiles(enginePgnsDataGridView);
        }

        private void AddServerPgnsButton_Click(object sender, EventArgs e)
        {
            AddFiles(serverPgnsDataGridView);
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

        private void SetMergeProgress(int progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(SetMergeProgress), progress);
            }
            else
            {
                mergeProgressBar.Value = progress;
                mergeProgressLabel.Text = progress.ToString() + "%";
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

                if (InvokeRequired)
                {
                    Invoke(new Action(EnableInput));
                }
                else
                {
                    EnableInput();
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
            catch (Exception ex)
            {
                finishedWithErrors = true;
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
            setDestinationFolderButton.Enabled = true;
            setTempFolderButton.Enabled = true;
            clearTempFolderButton.Enabled = true;
            buildButton.Enabled = true;
            mergeCheckBox.Enabled = true;
            openCheckBox.Enabled = true;
            databaseFormatComboBox.Enabled = true;
            humanPgnsDataGridView.AllowUserToAddRows = true;
            humanPgnsDataGridView.AllowUserToDeleteRows = true;
            humanPgnsDataGridView.ReadOnly = false;
            enginePgnsDataGridView.AllowUserToAddRows = true;
            enginePgnsDataGridView.AllowUserToDeleteRows = true;
            enginePgnsDataGridView.ReadOnly = false;
            serverPgnsDataGridView.AllowUserToAddRows = true;
            serverPgnsDataGridView.AllowUserToDeleteRows = true;
            serverPgnsDataGridView.ReadOnly = false;
            addHumanPgnsButton.Enabled = true;
            addEnginePgnsButton.Enabled = true;
            addServerPgnsButton.Enabled = true;

            KeepFormAlive = false;
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
            humanPgnsDataGridView.AllowUserToAddRows = false;
            humanPgnsDataGridView.AllowUserToDeleteRows = false;
            humanPgnsDataGridView.ReadOnly = true;
            enginePgnsDataGridView.AllowUserToAddRows = false;
            enginePgnsDataGridView.AllowUserToDeleteRows = false;
            enginePgnsDataGridView.ReadOnly = true;
            serverPgnsDataGridView.AllowUserToAddRows = false;
            serverPgnsDataGridView.AllowUserToDeleteRows = false;
            serverPgnsDataGridView.ReadOnly = true;
            addHumanPgnsButton.Enabled = false;
            addEnginePgnsButton.Enabled = false;
            addServerPgnsButton.Enabled = false;

            KeepFormAlive = true;
        }

        private async void BuildButton_Click(object sender, EventArgs e)
        {
            if (destinationFolderTextBox.Text == "")
            {
                MessageBox.Show("You need to specify the destination.");
                return;
            }

            var result = MessageBox.Show(
                string.Format(
                    "Are you sure you want to create a database of type {0}?\n" +
                    "This may take a long time. Make sure you selected everything correctly",
                    (string)(databaseFormatComboBox.SelectedItem)
                    ), 
                "Confirmation", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question
                );

            if (result != DialogResult.Yes)
            {
                return;
            }

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

        private void HumanPgnsDataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = humanPgnsDataGridView.Rows[e.RowIndex];
            if (row.Cells[1].Value.Equals("100%"))
            {
                row.DefaultCellStyle.BackColor = Color.LimeGreen;
            }
        }

        private void EnginePgnsDataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = enginePgnsDataGridView.Rows[e.RowIndex];
            if (row.Cells[1].Value.Equals("100%"))
            {
                row.DefaultCellStyle.BackColor = Color.LimeGreen;
            }
        }

        private void ServerPgnsDataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = serverPgnsDataGridView.Rows[e.RowIndex];
            if (row.Cells[1].Value.Equals("100%"))
            {
                row.DefaultCellStyle.BackColor = Color.LimeGreen;
            }
        }

        private bool HasCompatibileExtension(string path)
        {
            return SupportedExtensions.Contains(
                System.IO.Path.GetExtension(path)
                );
        }

        private void FilterFileLists()
        {
            FilterFileList(humanPgnsDataGridView);
            FilterFileList(enginePgnsDataGridView);
            FilterFileList(serverPgnsDataGridView);
        }

        private void FilterFileList(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; ++i)
            {
                if (!HasCompatibileExtension(dgv[0, i].Value as string))
                {
                    dgv.Rows.RemoveAt(i);
                    --i;
                }
            }
        }

        private void DatabaseFormatComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SupportedExtensions =
                database.GetSupportedExtensionsForType(
                    (string)databaseFormatComboBox.SelectedItem
                    );

            FilterFileLists();
        }

        private void OpenCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            openAfterFinished = openCheckBox.Checked;
        }
    }
}
