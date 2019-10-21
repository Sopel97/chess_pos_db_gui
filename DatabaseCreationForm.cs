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
    public partial class DatabaseCreationForm : Form
    {
        public DatabaseCreationForm()
        {
            InitializeComponent();
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
            foreach(string path in paths)
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
    }
}
