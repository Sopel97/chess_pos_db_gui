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
        private static readonly List<KeyValuePair<ulong, string>> NumberFormattingSuffixes =
            new List<KeyValuePair<ulong, string>>() {
                new KeyValuePair<ulong, string>( 1_000_000_000_000_000_000, "Qi" ),
                new KeyValuePair<ulong, string>( 1_000_000_000_000_000, "Q" ),
                new KeyValuePair<ulong, string>( 1_000_000_000_000, "T" ),
                new KeyValuePair<ulong, string>( 1_000_000_000, "B" ),
                new KeyValuePair<ulong, string>( 1_000_000, "M" ),
                new KeyValuePair<ulong, string>( 1_000, "k" )
        };

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

            supportManifestDataGridView.Columns["Name"].Frozen = true;
            supportManifestDataGridView.Columns["Name"].HeaderText = "Name";

            supportManifestDataGridView.Columns["MaxGames"].HeaderText = "Max games";
            supportManifestDataGridView.Columns["MaxGames"].ToolTipText = "The maximum number of games that can be safely put into the database.";
            supportManifestDataGridView.Columns["MaxPositions"].HeaderText = "Max positions";
            supportManifestDataGridView.Columns["MaxPositions"].ToolTipText = "The maximum number of unique positions that can be safely put into the database.";
            supportManifestDataGridView.Columns["MaxInstancesOfSinglePosition"].HeaderText = "Max instances of\na single position";
            supportManifestDataGridView.Columns["MaxInstancesOfSinglePosition"].ToolTipText = "The maximum number of times a single position may appear in the database. Note: this is often a bigger limit than 'Max games' because sometimes positions appear more than once in a single game.";

            supportManifestDataGridView.Columns["HasOneWayKey"].HeaderText = "Has one way key";
            supportManifestDataGridView.Columns["HasOneWayKey"].ToolTipText = "If set it means that the database doesn't use a unique key for any possible position. This may be the case for example if a database format uses position hash as a key.";
            supportManifestDataGridView.Columns["EstimatedMaxCollisions"].HeaderText = "Estimated\nmax collisions";
            supportManifestDataGridView.Columns["EstimatedMaxCollisions"].ToolTipText = "When one way key is used collisions may happen. When a collision happens two or more positions may be incorrectly seen as identical. This value indicates how many collions can be expected if one pushes the database to its limits. Even with thousands of collisions it would be very hard to notice as they would most likely happen in obscure lines.";
            supportManifestDataGridView.Columns["EstimatedMaxPositionsWithNoCollisions"].HeaderText = "Estimated max\npositions with\nno collisions";
            supportManifestDataGridView.Columns["EstimatedMaxPositionsWithNoCollisions"].ToolTipText = "This indicates how many positions can be put into the database while having <=50% chance of collision.";
            
            supportManifestDataGridView.Columns["HasCount"].HeaderText = "Has count";
            supportManifestDataGridView.Columns["HasCount"].ToolTipText = "Indicates whether the database format provides the number of times a position appeared.";

            supportManifestDataGridView.Columns["HasEloDiff"].HeaderText = "Has elo diff";
            supportManifestDataGridView.Columns["HasEloDiff"].ToolTipText = "Indicates whether the database format provides the total (average) elo difference between players for each position.";
            supportManifestDataGridView.Columns["MaxAbsEloDiff"].HeaderText = "Max abs elo diff";
            supportManifestDataGridView.Columns["MaxAbsEloDiff"].ToolTipText = "Indicates maximum elo difference in a game that can be stored. Higher differences are saturated.";
            supportManifestDataGridView.Columns["MaxAverageAbsEloDiff"].HeaderText = "Max average\nabs elo diff";
            supportManifestDataGridView.Columns["MaxAverageAbsEloDiff"].ToolTipText = "Some database operate on an assumption that for very large numbers of positions the average elo difference will get smaller to 0. This value indicates the maximum elo difference for a single position that can be handled by the database. This only applies when hitting 'Max games' limit.";

            supportManifestDataGridView.Columns["HasWhiteElo"].HeaderText = "Has white elo";
            supportManifestDataGridView.Columns["HasWhiteElo"].ToolTipText = "Indicates whether the database format provides total (average) elo for the white playing side.";
            supportManifestDataGridView.Columns["HasBlackElo"].HeaderText = "Has black elo";
            supportManifestDataGridView.Columns["HasBlackElo"].ToolTipText = "Indicates whether the database format provides total (average) elo for the black playing side.";
            supportManifestDataGridView.Columns["MinElo"].HeaderText = "Min elo";
            supportManifestDataGridView.Columns["MinElo"].ToolTipText = "Indicates the minimum player elo supported by the database format. Lower values are saturated.";
            supportManifestDataGridView.Columns["MaxElo"].HeaderText = "Max elo";
            supportManifestDataGridView.Columns["MaxElo"].ToolTipText = "Indicates the maximum player elo supported by the database format. Higher values are saturated.";
            supportManifestDataGridView.Columns["HasCountWithElo"].HeaderText = "Has count\nwith elo";
            supportManifestDataGridView.Columns["HasCountWithElo"].ToolTipText = "Indicates whether the database format provides for each position the number of times the game had player elo provided. It is always <= the number of times a position was encountered. It allows computing the average player elo for a given position when not all games have player elo included.";

            supportManifestDataGridView.Columns["HasFirstGame"].HeaderText = "Has first game";
            supportManifestDataGridView.Columns["HasFirstGame"].ToolTipText = "Indicates whether the database format provides information about the first game for each position.";
            supportManifestDataGridView.Columns["HasLastGame"].HeaderText = "Has last game";
            supportManifestDataGridView.Columns["HasLastGame"].ToolTipText = "Indicates whether the database format provides information about the last game for each position.";

            supportManifestDataGridView.Columns["AllowsFilteringTranspositions"].HeaderText = "Allows filtering\ntranspositions";
            supportManifestDataGridView.Columns["AllowsFilteringTranspositions"].ToolTipText = "Indicates whether the database format allows filtering transpositions.";
            supportManifestDataGridView.Columns["HasReverseMove"].HeaderText = "Has reverse move";
            supportManifestDataGridView.Columns["HasReverseMove"].ToolTipText = "Indicates whether the database format provides the reverse move information ('Retro Moves' table in the main view)";
            
            supportManifestDataGridView.Columns["AllowsFilteringByEloRange"].HeaderText = "Allows filtering\nby elo range";
            supportManifestDataGridView.Columns["AllowsFilteringByEloRange"].ToolTipText = "Indicates whether the database format allows filtering the output by the player elo.";
            supportManifestDataGridView.Columns["EloFilterGranularity"].HeaderText = "Elo filter\ngranularity";
            supportManifestDataGridView.Columns["EloFilterGranularity"].ToolTipText = "Indicates the granularily at which the elo can be filtered. For example a value of 100 means that player elo is binned by increments of 100.";

            supportManifestDataGridView.Columns["AllowsFilteringByMonthRange"].HeaderText = "Allows filtering\nby month range";
            supportManifestDataGridView.Columns["AllowsFilteringByMonthRange"].ToolTipText = "Indicates whether the database format allows filtering the output by the date the game was played.";
            supportManifestDataGridView.Columns["MonthFilterGranularity"].HeaderText = "Month filter\ngranularity";
            supportManifestDataGridView.Columns["MonthFilterGranularity"].ToolTipText = "Indicates the granularily at which the month can be filtered. For example a value of 12 means it can only be filtered by year.";

            supportManifestDataGridView.Columns["MaxBytesPerPosition"].HeaderText = "Max bytes\nper position";
            supportManifestDataGridView.Columns["MaxBytesPerPosition"].ToolTipText = "Indicates the maximum number of bytes the database format uses for position data per position.";
            supportManifestDataGridView.Columns["EstimatedAverageBytesPerPosition"].HeaderText = "Estimated average\nbytes per position";
            supportManifestDataGridView.Columns["EstimatedAverageBytesPerPosition"].ToolTipText = "Indicates the empirical average number of bytes the database format uses for position data per position for sizable databases.";

            foreach (DataGridViewColumn column in supportManifestDataGridView.Columns)
            {
                if (column.ValueType == typeof(ulong))
                {
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                else if (column.ValueType == typeof(bool))
                {
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                }
            }

            supportManifestDataGridView.CellPainting += DataGridView1_CellPainting;

            supportManifestDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            supportManifestDataGridView.ColumnHeadersHeight = 4;

            FillData();

            supportManifestDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;

            AdjustSizes();
        }

        private void AdjustSizes()
        {
            const int verticalPadding = 10;
            const int horizontalPadding = 3;

            foreach (DataGridViewColumn column in supportManifestDataGridView.Columns)
            {
                Size titleSize = TextRenderer.MeasureText(column.HeaderText.ToString(), column.HeaderCell.Style.Font);
                if (supportManifestDataGridView.ColumnHeadersHeight < titleSize.Width)
                {
                    supportManifestDataGridView.ColumnHeadersHeight = titleSize.Width + verticalPadding;
                }

                if (column.MinimumWidth < titleSize.Height)
                {
                    column.MinimumWidth = titleSize.Height + horizontalPadding;
                }
            }
        }

        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.PaintBackground(e.ClipBounds, true);
                Rectangle rect = e.CellBounds;
                Size titleSize = TextRenderer.MeasureText(e.FormattedValue.ToString(), e.CellStyle.Font);

                e.Graphics.TranslateTransform(0, titleSize.Width);
                e.Graphics.RotateTransform(-90.0F);

                e.Graphics.DrawString(e.FormattedValue.ToString(), e.CellStyle.Font, Brushes.Black, new PointF(rect.Y - (e.CellBounds.Height - titleSize.Width), rect.X));

                e.Graphics.RotateTransform(90.0F);
                e.Graphics.TranslateTransform(0, -titleSize.Width);
                e.Handled = true;
            }
        }

        private void FillData()
        {
            foreach ((string name, var manifest) in SupportManifests)
            {
                AddDataRow(name, manifest);
            }
        }

        private void AddDataRow(string name, DatabaseSupportManifest manifest)
        {
            var row = TabulatedSupportManifestData.NewRow();

            row["Name"] = name;

            row["MaxGames"] = manifest.MaxGames;
            row["MaxPositions"] = manifest.MaxPositions;
            row["MaxInstancesOfSinglePosition"] = manifest.MaxInstancesOfSinglePosition;

            row["HasOneWayKey"] = manifest.HasOneWayKey;
            if (manifest.HasOneWayKey)
            {
                row["EstimatedMaxCollisions"] = manifest.EstimatedMaxCollisions;
                row["EstimatedMaxPositionsWithNoCollisions"] = manifest.EstimatedMaxPositionsWithNoCollisions;
            }

            row["HasCount"] = manifest.HasCount;

            row["HasEloDiff"] = manifest.HasEloDiff;
            if (manifest.HasEloDiff)
            {
                row["MaxAbsEloDiff"] = manifest.MaxAbsEloDiff;
                row["MaxAverageAbsEloDiff"] = manifest.MaxAverageAbsEloDiff;
            }

            row["HasWhiteElo"] = manifest.HasWhiteElo;
            row["HasBlackElo"] = manifest.HasBlackElo;
            if (manifest.HasWhiteElo || manifest.HasBlackElo)
            {
                row["MinElo"] = manifest.MinElo;
                row["MaxElo"] = manifest.MaxElo;
                row["HasCountWithElo"] = manifest.HasCountWithElo;
            }

            row["HasFirstGame"] = manifest.HasFirstGame;
            row["HasLastGame"] = manifest.HasLastGame;

            row["AllowsFilteringTranspositions"] = manifest.AllowsFilteringTranspositions;
            row["HasReverseMove"] = manifest.HasReverseMove;

            row["AllowsFilteringByEloRange"] = manifest.AllowsFilteringByEloRange;
            if (manifest.AllowsFilteringByEloRange)
            {
                row["EloFilterGranularity"] = manifest.EloFilterGranularity;
            }

            row["AllowsFilteringByMonthRange"] = manifest.AllowsFilteringByMonthRange;
            if (manifest.AllowsFilteringByMonthRange)
            {
                row["MonthFilterGranularity"] = manifest.MonthFilterGranularity;
            }

            row["MaxBytesPerPosition"] = manifest.MaxBytesPerPosition;
            foreach(var v in manifest.EstimatedAverageBytesPerPosition)
            {
                row["EstimatedAverageBytesPerPosition"] = v;
            }

            TabulatedSupportManifestData.Rows.Add(row);
        }

        private void supportManifestDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var cell = supportManifestDataGridView[e.ColumnIndex, e.RowIndex];
            if (e.Value != null && e.Value != System.DBNull.Value && cell.ValueType == typeof(ulong))
            {
                e.Value = FormatNumber((ulong)e.Value);
                e.FormattingApplied = true;
            }
        }

        private string FormatNumber(ulong number)
        {
            foreach((ulong unit, string suffix) in NumberFormattingSuffixes)
            {
                if (number >= unit)
                {
                    return (number / unit).ToString() + suffix;
                }
            }

            return number.ToString();
        }
    }
}
