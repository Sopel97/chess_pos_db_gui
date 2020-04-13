using System.Collections.Generic;
using System.Json;

namespace chess_pos_db_gui
{
    public class QueryResponse
    {
        public List<ResultForRoot> Results { get; set; }

        public static QueryResponse FromJson(JsonValue json)
        {
            var result = new QueryResponse();

            foreach (JsonValue entry in json["results"])
            {
                result.Results.Add(ResultForRoot.FromJson(entry));
            }

            return result;
        }

        public QueryResponse()
        {
            Results = new List<ResultForRoot>();
        }
    }
}
