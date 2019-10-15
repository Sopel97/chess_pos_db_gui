using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui
{
    class QueryResponse
    {
        public List<ResultForRoot> Results { get; set; }

        public static QueryResponse FromJson(JsonValue json)
        {
            var result = new QueryResponse();

            foreach(JsonValue entry in json)
            {
                result.Results.Add(ResultForRoot.FromJson(entry));
            }

            return result;
        }
    }
}
