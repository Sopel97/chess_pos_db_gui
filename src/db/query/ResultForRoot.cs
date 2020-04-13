using System.Collections.Generic;
using System.Json;

namespace chess_pos_db_gui
{
    public class ResultForRoot
    {
        public RootPosition Position { get; set; }
        public Dictionary<Select, SelectResult> ResultsBySelect { get; set; }

        public static ResultForRoot FromJson(JsonValue json)
        {
            var result = new ResultForRoot(
                RootPosition.FromJson(json["position"])
            );

            foreach (Select select in SelectHelper.Values)
            {
                var selectStr = select.Stringify();
                if (json.ContainsKey(selectStr))
                {
                    result.ResultsBySelect.Add(select, SelectResult.FromJson(json[selectStr]));
                }
            }

            return result;
        }

        public ResultForRoot()
        {
            Position = null;
            ResultsBySelect = new Dictionary<Select, SelectResult>();
        }

        public ResultForRoot(RootPosition position)
        {
            Position = position;
            ResultsBySelect = new Dictionary<Select, SelectResult>();
        }
    }

    public class SelectResult
    {
        public SegregatedEntries Root { get; set; }
        public Dictionary<string, SegregatedEntries> Children { get; set; }

        public static SelectResult FromJson(JsonValue json)
        {
            var result = new SelectResult();

            foreach (KeyValuePair<string, JsonValue> entry in json)
            {
                var entries = SegregatedEntries.FromJson(entry.Value);
                if (entry.Key == "--")
                {
                    result.Root = entries;
                }
                else
                {
                    result.Children.Add(entry.Key, entries);
                }
            }

            return result;
        }

        public SelectResult()
        {
            Children = new Dictionary<string, SegregatedEntries>();
        }
    }
}
