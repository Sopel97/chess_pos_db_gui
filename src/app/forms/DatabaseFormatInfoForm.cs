using chess_pos_db_gui.src.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_pos_db_gui.src.app.forms
{
    public partial class DatabaseFormatInfoForm : Form
    {
        private DataTable TabulatedSupportManifestData { get; set; }

        private DatabaseProxy Database { get; set; }
        private Dictionary<string, DatabaseSupportManifest> SupportManifests { get; set; }

        public DatabaseFormatInfoForm(DatabaseProxy database)
        {
            Database = database;
            SupportManifests = database.GetSupportManifests();

            InitializeComponent();

            TabulatedSupportManifestData = new DataTable();
            TabulatedSupportManifestData.Columns.Add(new DataColumn("Name", typeof(string)));

            TabulatedSupportManifestData.Columns.Add(new DataColumn("MaxGames", typeof(ulong)));
            TabulatedSupportManifestData.Columns.Add(new DataColumn("MaxPositions", typeof(ulong)));
            TabulatedSupportManifestData.Columns.Add(new DataColumn("MaxInstancesOfSinglePosition", typeof(ulong)));

            TabulatedSupportManifestData.Columns.Add(new DataColumn("HasOneWayKey", typeof(bool)));
            TabulatedSupportManifestData.Columns.Add(new DataColumn("EstimatedMaxCollisions", typeof(ulong)));
            TabulatedSupportManifestData.Columns.Add(new DataColumn("EstimatedMaxPositionsWithNoCollisions", typeof(ulong)));

            TabulatedSupportManifestData.Columns.Add(new DataColumn("HasCount", typeof(bool)));

            TabulatedSupportManifestData.Columns.Add(new DataColumn("HasEloDiff", typeof(bool)));
            TabulatedSupportManifestData.Columns.Add(new DataColumn("MaxAbsEloDiff", typeof(ulong)));
            TabulatedSupportManifestData.Columns.Add(new DataColumn("MaxAverageAbsEloDiff", typeof(ulong)));

            TabulatedSupportManifestData.Columns.Add(new DataColumn("HasWhiteElo", typeof(bool)));
            TabulatedSupportManifestData.Columns.Add(new DataColumn("HasBlackElo", typeof(bool)));
            TabulatedSupportManifestData.Columns.Add(new DataColumn("MinElo", typeof(ulong)));
            TabulatedSupportManifestData.Columns.Add(new DataColumn("MaxElo", typeof(ulong)));
            TabulatedSupportManifestData.Columns.Add(new DataColumn("HasCountWithElo", typeof(bool)));

            TabulatedSupportManifestData.Columns.Add(new DataColumn("HasFirstGame", typeof(bool)));
            TabulatedSupportManifestData.Columns.Add(new DataColumn("HasLastGame", typeof(bool)));

            TabulatedSupportManifestData.Columns.Add(new DataColumn("AllowsFilteringTranspositions", typeof(bool)));
            TabulatedSupportManifestData.Columns.Add(new DataColumn("HasReverseMove", typeof(bool)));

            TabulatedSupportManifestData.Columns.Add(new DataColumn("AllowsFilteringByEloRange", typeof(bool)));
            TabulatedSupportManifestData.Columns.Add(new DataColumn("EloFilterGranularity", typeof(ulong)));

            TabulatedSupportManifestData.Columns.Add(new DataColumn("AllowsFilteringByMonthRange", typeof(bool)));
            TabulatedSupportManifestData.Columns.Add(new DataColumn("MonthFilterGranularity", typeof(ulong)));

            TabulatedSupportManifestData.Columns.Add(new DataColumn("MaxBytesPerPosition", typeof(ulong)));
            TabulatedSupportManifestData.Columns.Add(new DataColumn("EstimatedAverageBytesPerPosition", typeof(ulong)));

            WinFormsControlUtil.MakeDoubleBuffered(supportManifestDataGridView);
            supportManifestDataGridView.DataSource = TabulatedSupportManifestData;
        }
    }
}
