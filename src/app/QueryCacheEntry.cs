
using ChessDotNet;
using System.Collections.Generic;

namespace chess_pos_db_gui
{
    public class QueryCacheEntry
    {
        public QueryResponse Stats { get; set; }
        public Dictionary<Move, ChessDBCNScore> Scores { get; set; }

        public QueryCacheEntry(QueryResponse stats, Dictionary<Move, ChessDBCNScore> scores)
        {
            Stats = stats;
            Scores = scores;
        }
    }

}
