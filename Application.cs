using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public partial class Application : Form
    {
        private List<GameLevel> levels;
        private List<Select> selects;

        public Application()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string jsonStr = File.ReadAllText("data/test.json");
            var json = JsonValue.Parse(jsonStr);
            QueryResponse resp = QueryResponse.FromJson(json);
            Populate(resp, 
                new List<Select>{chess_pos_db_gui.Select.Continuations, chess_pos_db_gui.Select.Transpositions}, 
                new List<GameLevel>{GameLevel.Human, GameLevel.Engine, GameLevel.Server });
        }

        private void Populate(string move, AggregatedEntry entry)
        {
            int row = entriesGridView.Rows.Add();
            entriesGridView["Move", row].Value = move;
            entriesGridView["Count", row].Value = entry.Count;
            entriesGridView["WinCount", row].Value = entry.WinCount;
            entriesGridView["DrawCount", row].Value = entry.DrawCount;
            entriesGridView["LossCount", row].Value = entry.LossCount;
            entriesGridView["Perf", row].Value = (entry.Perf * 100).ToString("F1") + "%";
            entriesGridView["DrawPct", row].Value = (entry.DrawRate * 100).ToString("F1") + "%";

            foreach (GameHeader header in entry.FirstGame)
            {
                entriesGridView["GameId", row].Value = header.GameId;
                entriesGridView["Date", row].Value = header.Date.ToString();
                entriesGridView["Event", row].Value = header.Event;
                entriesGridView["White", row].Value = header.White;
                entriesGridView["Black", row].Value = header.Black;
                entriesGridView["Result", row].Value = header.Result.Stringify(new GameResultLetterFormat());
                entriesGridView["Eco", row].Value = header.Eco.ToString();
                entriesGridView["PlyCount", row].Value = header.PlyCount.FirstOrDefault();
            }
        }

        private void Populate(Dictionary<string, AggregatedEntry> entries)
        {
            Clear();
            foreach (KeyValuePair<string, AggregatedEntry> entry in entries)
            {
                Populate(entry.Key, entry.Value);
            }
            entriesGridView.Refresh();
        }

        private void Gather(QueryResponse res, Select select, List<GameLevel> levels, ref Dictionary<string, AggregatedEntry> aggregatedEntries)
        {
            var rootEntries = res.Results[0].ResultsBySelect[select].Root;
            var childrenEntries = res.Results[0].ResultsBySelect[select].Children;

            if (aggregatedEntries.ContainsKey("--"))
            {
                aggregatedEntries["--"].Combine(new AggregatedEntry(rootEntries, levels));
            }
            else
            {
                aggregatedEntries.Add("--", new AggregatedEntry(rootEntries, levels));
            }
            foreach (KeyValuePair<string, SegregatedEntries> entry in childrenEntries)
            {
                if (aggregatedEntries.ContainsKey(entry.Key))
                {
                    aggregatedEntries[entry.Key].Combine(new AggregatedEntry(entry.Value, levels));
                }
                else
                {
                    aggregatedEntries.Add(entry.Key, new AggregatedEntry(entry.Value, levels));
                }
            }
        }

        private void Populate(QueryResponse res, List<Select> selects, List<GameLevel> levels)
        {
            Dictionary<string, AggregatedEntry> aggregatedEntries = new Dictionary<string, AggregatedEntry>();

            foreach (Select select in selects)
            {
                Gather(res, select, levels, ref aggregatedEntries);
            }

            Populate(aggregatedEntries);
        }

        private void Clear()
        {
            entriesGridView.Rows.Clear();
            entriesGridView.Refresh();
        }

        private void LevelHumanCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void LevelEngineCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void LevelServerCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void TypeContinuationsCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void TypeTranspositionsCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
