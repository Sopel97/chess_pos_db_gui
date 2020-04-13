using System;
using System.Collections;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui
{
    public class SegregatedEntries
    {
        private Dictionary<Origin, Entry> Entries { get; set; }

        public static SegregatedEntries FromJson(JsonValue json)
        {
            var e = new SegregatedEntries();

            foreach(KeyValuePair<string, JsonValue> byLevel in json)
            {
                GameLevel level = GameLevelHelper.FromString(byLevel.Key).First();
                foreach (KeyValuePair<string, JsonValue> byResult in byLevel.Value)
                {
                    GameResult result = GameResultHelper.FromString(new GameResultWordFormat(), byResult.Key).First();

                    e.Add(level, result, Entry.FromJson(byResult.Value));
                }
            }

            return e;
        }

        public SegregatedEntries()
        {
            Entries = new Dictionary<Origin, Entry>();
        }

        public void Add(GameLevel level, GameResult result, Entry entry)
        {
            Entries.Add(new Origin(level, result), entry);
        }

        public Entry Get(GameLevel level, GameResult result)
        {
            if (Entries.TryGetValue(new Origin(level, result), out Entry e))
                return e;

            return null;
        }

        public IEnumerator GetEnumerator()
        {
            return Entries.GetEnumerator();
        }
    }

    internal struct Origin
    {
        public GameLevel Level { get; set; }
        public GameResult Result { get; set; }

        public Origin(GameLevel level, GameResult result)
        {
            Level = level;
            this.Result = result;
        }
    }
}
