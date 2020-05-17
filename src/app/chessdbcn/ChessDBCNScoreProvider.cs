using chess_pos_db_gui.src.chess;

using ChessDotNet;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace chess_pos_db_gui.src.app.chessdbcn
{
    class ChessDBCNScoreProvider
    {
        private static readonly string URL = "http://www.chessdb.cn/cdb.php";

        private HttpClient Client { get; set; }

        public ChessDBCNScoreProvider()
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri(URL)
            };

            // Add an Accept header for JSON format.
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );
        }

        private Dictionary<Move, ChessDBCNScore> ParseScoresFromResponse(string fen, string responseStr)
        {
            Dictionary<Move, ChessDBCNScore> scores = new Dictionary<Move, ChessDBCNScore>();

            string[] byMoveStrs = responseStr.Split('|');
            foreach (var byMoveStr in byMoveStrs)
            {
                string[] parts = byMoveStr.Split(',');

                Dictionary<string, string> values = new Dictionary<string, string>();
                foreach (var part in parts)
                {
                    string[] kv = part.Split(':');
                    if (kv.Length < 2)
                    {
                        continue;
                    }
                    values.Add(kv[0], kv[1]);
                }

                values.TryGetValue("move", out string moveStr);
                values.TryGetValue("score", out string scoreStr);

                if (moveStr != null && scoreStr != null)
                {
                    try
                    {
                        scores.Add(Lan.LanToMove(fen, moveStr), new ChessDBCNScore(scoreStr));
                    }
                    catch
                    {
                    }
                }
            }

            return scores;
        }

        public Dictionary<Move, ChessDBCNScore> GetScores(string fen)
        {
            const string urlParameters = "?action=queryall&board={0}";

            try
            {
                HttpResponseMessage response = Client.GetAsync(String.Format(urlParameters, fen)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseStr = response.Content.ReadAsStringAsync().Result;
                    System.Diagnostics.Debug.WriteLine(responseStr);
                    return ParseScoresFromResponse(fen, responseStr);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(
                        string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase)
                        );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new Dictionary<Move, ChessDBCNScore>();
        }
    }
}
