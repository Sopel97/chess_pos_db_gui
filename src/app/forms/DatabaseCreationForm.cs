using chess_pos_db_gui.src.app.forms;
using chess_pos_db_gui.src.util;
using ChessDotNet;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        private bool IsAppend { get; set; } = false;

        public string DatabasePath { get { return destinationFolderTextBox.Text; } }

        public ulong NumGames { get; private set; }
        public ulong NumPositions { get; private set; }
        public ulong NumSkippedGames { get; private set; }

        private ulong TotalFileSizeBeingImported { get; set; }
        private ulong TotalFileSizeAlreadyImported { get; set; }

        public bool KeepFormAlive { get; private set; }

        private readonly DatabaseProxy database;

        private IList<string> SupportedExtensions { get; set; }

        public DatabaseCreationForm(DatabaseProxy db)
        {
            InitializeComponent();

            Icon = Properties.Resources.application_icon;

            database = db;

            mergeAllAfterImportRadioButton.Checked = true;

            tempStorageUsageUnitComboBox.SelectedItem = "GB";

            WinFormsControlUtil.MakeDoubleBuffered(humanPgnsDataGridView);
            WinFormsControlUtil.MakeDoubleBuffered(enginePgnsDataGridView);
            WinFormsControlUtil.MakeDoubleBuffered(serverPgnsDataGridView);
            WinFormsControlUtil.MakeDoubleBuffered(importProgressBar);
            WinFormsControlUtil.MakeDoubleBuffered(mergeProgressBar);

            if (database.IsOpen)
            {
                IsAppend = true;
                destinationFolderTextBox.Text = database.Path;
                destinationFolderTextBox.Enabled = false;
                databaseFormatComboBox.Enabled = false;
                mergeNewAfterImportRadioButton.Visible = true;
                Text = "Append to database.";
                buildButton.Text = "Append";
                openCheckBox.Visible = false;
                setDestinationFolderButton.Enabled = false;
            }
            else
            {
                IsAppend = false;
                mergeNewAfterImportRadioButton.Visible = false;
                Text = "Create database.";
                buildButton.Text = "Create";
            }
        }

        private void SetFolder(TextBox textBox)
        {
            using (FolderBrowserDialog browser = new FolderBrowserDialog())
            {
                browser.ShowNewFolderButton = true;

                if (browser.ShowDialog() == DialogResult.OK)
                {
                    textBox.Text = browser.SelectedPath;
                }
            }
        }

        private void setPrimaryTempFolderButton_Click(object sender, EventArgs e)
        {
            SetFolder(primaryTempFolderTextBox);
        }

        private void setSecondaryTempFolderButton_Click(object sender, EventArgs e)
        {
            SetFolder(secondaryTempFolderTextBox);
        }

        private void SetDestinationFolderButton_Click(object sender, EventArgs e)
        {
            SetFolder(destinationFolderTextBox);
        }

        private void clearPrimaryTempFolderButton_Click(object sender, EventArgs e)
        {
            primaryTempFolderTextBox.Clear();
        }

        private void clearSecondaryTempFolderButton_Click(object sender, EventArgs e)
        {
            secondaryTempFolderTextBox.Clear();
        }

        private void DatabaseCreationForm_Load(object sender, EventArgs e)
        {
            if (IsAppend)
            {
                string format = database.GetDatabaseFormat();
                databaseFormatComboBox.Items.Add(format);
                databaseFormatComboBox.SelectedItem = format;
            }
            else
            {
                var supported = database.GetSupportedDatabaseTypes();
                foreach (var type in supported)
                {
                    databaseFormatComboBox.Items.Add(type);
                }

                databaseFormatComboBox.SelectedItem = database.GetDefaultDatabaseFormat();
            }
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
            if (databaseFormatComboBox.SelectedItem == null || (string)databaseFormatComboBox.SelectedItem == "")
            {
                MessageBox.Show("Please select the database format first.");
                return;
            }

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

        private void SetFileImported(string path)
        {
            var row = FindRowWithPgnFile(path);
            if (row != null)
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<string>(SetFileImported), path);
                }
                else
                {
                    row.Cells[1].Value = "100%";

                    if (row.DataGridView == humanPgnsDataGridView)
                    {
                        pgnPathsTabControl.SelectedTab = humanTabPage;
                    }
                    else if (row.DataGridView == enginePgnsDataGridView)
                    {
                        pgnPathsTabControl.SelectedTab = engineTabPage;
                    }
                    else if (row.DataGridView == serverPgnsDataGridView)
                    {
                        pgnPathsTabControl.SelectedTab = serverTabPage;
                    }

                    row.DataGridView.FirstDisplayedCell = row.Cells[0];
                }
            }
        }

        private void SetImportProgress(int progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(SetImportProgress), progress);
            }
            else
            {
                importProgressBar.Value = progress;
                importProgressLabel.Text = progress.ToString() + "%";
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

        private void RefreshProgress()
        {
            humanPgnsDataGridView.Refresh();
            enginePgnsDataGridView.Refresh();
            serverPgnsDataGridView.Refresh();

            progressGroupBox.Refresh();
        }

        private void ProgressCallback(JsonValue progressReport)
        {
            if (progressReport["operation"] == "import")
            {
                if (progressReport.ContainsKey("imported_file_path"))
                {
                    string path = progressReport["imported_file_path"];
                    SetFileImported(path);

                    ulong filesize = (ulong)new System.IO.FileInfo(path).Length;
                    TotalFileSizeAlreadyImported += filesize;
                    SetImportProgress((int)(TotalFileSizeAlreadyImported * 100 / TotalFileSizeBeingImported));
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
                Invoke(new Action(RefreshProgress));
            }
            else
            {
                RefreshProgress();
            }
        }

        private void FinalizeBuild()
        {
            if (IsManualMergeAfterImport())
            {
                database.Open(DatabasePath);
                using (var form = new DatabaseMergeForm(database))
                {
                    form.ShowDialog();
                }
            }

            EnableInput();

            KeepFormAlive = false;

            Close();
        }

        private void FinalizeAppend()
        {
            if (IsManualMergeAfterImport())
            {
                // database already opened
                using (var form = new DatabaseMergeForm(database))
                {
                    form.ShowDialog();
                }
            }

            EnableInput();

            KeepFormAlive = false;

            Close();
        }

        private void BuildDatabase(JsonObject request)
        {
            try
            {
                database.Create(request, ProgressCallback);
                finishedWithErrors = false;
                MessageBox.Show(
                    string.Format("Finished.\nGames imported: {0}\nGames skipped: {1}\nPositions imported: {2}",
                        NumGames,
                        NumSkippedGames,
                        NumPositions
                    )
                );

                if (InvokeRequired)
                {
                    Invoke(new Action(FinalizeBuild));
                }
                else
                {
                    FinalizeBuild();
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

        private void AppendToDatabase(JsonObject request)
        {
            try
            {
                database.Append(request, ProgressCallback);
                finishedWithErrors = false;
                MessageBox.Show(
                    string.Format("Finished.\nGames imported: {0}\nGames skipped: {1}\nPositions imported: {2}",
                        NumGames,
                        NumSkippedGames,
                        NumPositions
                    )
                );

                if (InvokeRequired)
                {
                    Invoke(new Action(FinalizeAppend));
                }
                else
                {
                    FinalizeAppend();
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
            importSettingsGroupBox.Enabled = true;
            buildButton.Enabled = true;
            humanPgnsDataGridView.AllowUserToDeleteRows = true;
            humanPgnsDataGridView.ReadOnly = false;
            enginePgnsDataGridView.AllowUserToDeleteRows = true;
            enginePgnsDataGridView.ReadOnly = false;
            serverPgnsDataGridView.AllowUserToDeleteRows = true;
            serverPgnsDataGridView.ReadOnly = false;
            addHumanPgnsButton.Enabled = true;
            addEnginePgnsButton.Enabled = true;
            addServerPgnsButton.Enabled = true;

            tempDirsGroupBox.Enabled = true;

            mergeProgressBar.Enabled = true;

            KeepFormAlive = false;
        }

        private void DisableInput()
        {
            importSettingsGroupBox.Enabled = false;
            buildButton.Enabled = false;
            humanPgnsDataGridView.AllowUserToDeleteRows = false;
            humanPgnsDataGridView.ReadOnly = true;
            enginePgnsDataGridView.AllowUserToDeleteRows = false;
            enginePgnsDataGridView.ReadOnly = true;
            serverPgnsDataGridView.AllowUserToDeleteRows = false;
            serverPgnsDataGridView.ReadOnly = true;
            addHumanPgnsButton.Enabled = false;
            addEnginePgnsButton.Enabled = false;
            addServerPgnsButton.Enabled = false;

            tempDirsGroupBox.Enabled = false;

            if (!IsAutomaticMergeAll())
            {
                mergeProgressBar.Enabled = false;
            }

            KeepFormAlive = true;
        }

        private bool IsAutomaticMergeAll()
        {
            return mergeAllAfterImportRadioButton.Checked;
        }

        private bool IsManualMergeAfterImport()
        {
            return mergeManuallyAfterImportRadioButton.Checked;
        }

        private bool IsAutomaticMergeNew()
        {
            return mergeNewAfterImportRadioButton.Checked;
        }

        private void UpdateRadioButtonsState()
        {
            bool enable = IsAutomaticMergeAll() || IsAutomaticMergeNew();
            setPrimaryTempFolderButton.Enabled = enable;
            setSecondaryTempFolderButton.Enabled = enable;
            clearPrimaryTempFolderButton.Enabled = enable;
            clearSecondaryTempFolderButton.Enabled = enable;
            maxTempStorageUsageCheckBox.Enabled = enable;
            tempStorageUsageSizeNumericUpDown.Enabled = enable;
            tempStorageUsageUnitComboBox.Enabled = enable;
        }

        private List<string> GetTemporaryDirectories()
        {
            List<string> dirs = new List<string>();

            if (primaryTempFolderTextBox.Text != null && primaryTempFolderTextBox.Text != "")
            {
                dirs.Add(primaryTempFolderTextBox.Text);
            }

            if (secondaryTempFolderTextBox.Text != null && secondaryTempFolderTextBox.Text != "")
            {
                dirs.Add(secondaryTempFolderTextBox.Text);
            }

            return dirs;
        }

        private decimal GetUnitFromAbbreviation(string abbr)
        {
            switch (abbr)
            {
                case "MB":
                    return 1000m * 1000m;
                case "GB":
                    return 1000m * 1000m * 1000m;
                case "TB":
                    return 1000m * 1000m * 1000m * 1000m;
            }

            throw new ArgumentException("Invalid size abbreviation.");
        }

        private ulong? GetMaxStorageUsage()
        {
            if (maxTempStorageUsageCheckBox.Checked)
            {
                decimal amount = tempStorageUsageSizeNumericUpDown.Value;
                decimal unit = GetUnitFromAbbreviation((string)tempStorageUsageUnitComboBox.SelectedItem);
                decimal bytes = amount * unit;
                return (ulong)bytes;
            }
            else
            {
                return null;
            }
        }

        private async void HandleCreateDatabaseRequest()
        {
            var dir = destinationFolderTextBox.Text;

            if (databaseFormatComboBox.SelectedItem == null || (string)databaseFormatComboBox.SelectedItem == "")
            {
                MessageBox.Show("You need to specify the database format.");
                return;
            }

            if (dir == "")
            {
                MessageBox.Show("You need to specify the destination.");
                return;
            }

            if (!System.IO.Directory.Exists(dir))
            {
                MessageBox.Show("Destination directory doesn't exist.");
                return;
            }

            if (System.IO.Directory.EnumerateFileSystemEntries(dir).Any())
            {
                MessageBox.Show("Destination directory is not empty. You need to empty it before proceeding.");
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

            var humanPgns = GetHumanPgns();
            var enginePgns = GetEnginePgns();
            var serverPgns = GetServerPgns();

            TotalFileSizeBeingImported =
                GetTotalImportableFileSize(humanPgns)
                + GetTotalImportableFileSize(enginePgns)
                + GetTotalImportableFileSize(serverPgns);
            TotalFileSizeAlreadyImported = 0;

            var maxSpace = GetMaxStorageUsage();
            var temps = GetTemporaryDirectories();
            bool doMerge = IsAutomaticMergeAll();

            JsonObject request = new JsonObject
            {
                { "command", "create" },
                { "destination_path", dir },
                { "merge", doMerge },
                { "report_progress", true },
                { "database_format", databaseFormatComboBox.Text },
                { "human_pgns", new JsonArray(humanPgns) },
                { "engine_pgns", new JsonArray(enginePgns) },
                { "server_pgns", new JsonArray(serverPgns) }
            };
            if (doMerge)
            {
                request.Add("temporary_paths", new JsonArray(temps.Select(
                    t =>
                    {
                        JsonValue s = t;
                        return s;
                    })));
                if (maxSpace.HasValue)
                {
                    request.Add("temporary_space", maxSpace.Value);
                }
            }

            await Task.Run(() => BuildDatabase(request));
        }
        private async void HandleAppendRequest()
        {
            var result = MessageBox.Show(
                string.Format(
                    "Are you sure you want to append to the database?\n" +
                    "This may take a long time. Make sure you selected everything correctly"
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

            var humanPgns = GetHumanPgns();
            var enginePgns = GetEnginePgns();
            var serverPgns = GetServerPgns();

            TotalFileSizeBeingImported =
                GetTotalImportableFileSize(humanPgns)
                + GetTotalImportableFileSize(enginePgns)
                + GetTotalImportableFileSize(serverPgns);
            TotalFileSizeAlreadyImported = 0;

            var maxSpace = GetMaxStorageUsage();
            var temps = GetTemporaryDirectories();
            string mergeType = "none";
            if (IsAutomaticMergeAll())
            {
                mergeType = "all";
            }
            else if (IsAutomaticMergeNew())
            {
                mergeType = "new";
            }

            JsonObject request = new JsonObject
            {
                { "command", "append" },
                { "merge", mergeType },
                { "report_progress", true },
                { "human_pgns", new JsonArray(humanPgns) },
                { "engine_pgns", new JsonArray(enginePgns) },
                { "server_pgns", new JsonArray(serverPgns) }
            };
            if (mergeType != "none")
            {
                request.Add("temporary_paths", new JsonArray(temps.Select(
                    t =>
                    {
                        JsonValue s = t;
                        return s;
                    })));
                if (maxSpace.HasValue)
                {
                    request.Add("temporary_space", maxSpace.Value);
                }
            }

            await Task.Run(() => AppendToDatabase(request));
        }

        private void BuildButton_Click(object sender, EventArgs e)
        {
            if (IsAppend)
            {
                HandleAppendRequest();
            }
            else
            {
                HandleCreateDatabaseRequest();
            }
        }

        private ulong GetTotalImportableFileSize(List<JsonValue> serverPgns)
        {
            ulong total = 0;
            foreach (string path in serverPgns)
            {
                total += (ulong)new System.IO.FileInfo(path).Length;
            }
            return total;
        }

        private void DatabaseCreationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (KeepFormAlive)
            {
                e.Cancel = true;
            }
        }

        private void PaintIfCompleted(DataGridViewRow row)
        {
            if (row.Cells[1].Value != null && row.Cells[1].Value.Equals("100%"))
            {
                row.DefaultCellStyle.BackColor = Color.LimeGreen;
            }
        }

        private void HumanPgnsDataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = humanPgnsDataGridView.Rows[e.RowIndex];
            PaintIfCompleted(row);
        }

        private void EnginePgnsDataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = enginePgnsDataGridView.Rows[e.RowIndex];
            PaintIfCompleted(row);
        }

        private void ServerPgnsDataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = serverPgnsDataGridView.Rows[e.RowIndex];
            PaintIfCompleted(row);
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

        private void WhatsDatabaseFormatButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "This determines the binary format used for storing the database you're about to create.\n" +
                "Different formats provide different tradeoffs between storage space required and the information they keep.\n" +
                "Currently the following database formats exist:\n" +
                "\n" +
                "- db_alpha - An old deprecated format. Not many advantages to using it now. Definitely don't use it if you have an HDD. May use less space than other formats for small databases. If you really want to know more about this see chess_pos_db documentation on github. If you're not certain don't use it.\n" +
                "\n" +
                "- db_beta - Each unique position takes 24 bytes of storage space (usually ~70% of total number of positions). It provides WDL data and information about first game for each position. Supports filtering transpositions. Should handle every practical database size, is expected to work correctly with trillion of positions.\n" +
                "\n" +
                "- db_delta - Each unique position takes 32 bytes of storage space. Has all the features of db_beta but also stores average elo delta for each position (BlackElo - WhiteElo). This allows calculation of expected performance for white and effecively allows the GUI to compute more reliable move quality values. Also stores information about last game for each postion but it's unavailable in the GUI. This should be the default if you don't have any reason to go for a different one.\n" +
                "\n" +
                "- db_epsilon - Each unique position takes 16 bytes of storage space. This is a bare bones format that only keeps WDL and transposition information. You should only use it for huge databases and when you're low on disk space. Allows a maximum of 4 billion games to be aggregated, but there may be very very rare glitches beyond 1 billion games (you shouldn't be able to spot them).\n",
                "Help",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
        }

        private void dontMergeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateRadioButtonsState();
        }

        private void mergeAllAfterImportRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateRadioButtonsState();
        }

        private void mergeManuallyAfterImportRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateRadioButtonsState();
        }
    }
}
