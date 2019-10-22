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
        public ulong NumPosition { get; private set; }
        public ulong NumSkippedGames { get; private set; }

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
                row.Cells[1].Value = ((int)(progress * 100)).ToString() + "%";
                row.Selected = true;
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
            }
            else if (progressReport["operation"] == "merge")
            {
                mergeProgressBar.Value = (int)(progressReport["overall_progress"] * 100.0);
            }
            else
            {
                return;
            }

            Refresh();
        }

        private void BuildDatabase()
        {
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

            try
            {
                database.Create(request, ProgressCallback);
                finishedWithErrors = false;
                MessageBox.Show("Finished");
            }
            catch
            {
                finishedWithErrors = true;
                MessageBox.Show("Finished with errors");
            }

            Close();
        }

        private void BuildButton_Click(object sender, EventArgs e)
        {
            BuildDatabase();
            if (OpenAfterFinished)
            {
                database.Open(DatabasePath);
            }
        }
    }
}
