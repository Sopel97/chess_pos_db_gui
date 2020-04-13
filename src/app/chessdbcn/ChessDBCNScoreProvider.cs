using chess_pos_db_gui.src.chess;
using ChessDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui.src.app.chessdbcn
{
    class ChessDBCNScoreProvider
    {
        private static readonly string URL = "http://www.chessdb.cn/cdb.php";
        private HttpClient Client { get; set; }

        public ChessDBCNScoreProvider()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );
        }

        public Dictionary<Move, Score> GetScores(string fen)
        {
            const string urlParameters = "?action=queryall&board={0}";

            Dictionary<Move, Score> scores = new Dictionary<Move, Score>();

            try
            {
                HttpResponseMessage response = Client.GetAsync(String.Format(urlParameters, fen)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseStr = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("{0}", responseStr);
                    string[] byMoveStrs = responseStr.Split('|');
                    foreach (var byMoveStr in byMoveStrs)
                    {
                        string[] parts = byMoveStr.Split(',');

                        Dictionary<string, string> values = new Dictionary<string, string>();
                        foreach (var part in parts)
                        {
                            string[] kv = part.Split(':');
                            values.Add(kv[0], kv[1]);
                        }

                        values.TryGetValue("move", out string moveStr);
                        values.TryGetValue("score", out string scoreStr);

                        if (moveStr != null)
                        {
                            try
                            {
                                scores.Add(Lan.LanToMove(fen, moveStr), new Score(scoreStr));
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return scores;
        }
    }
}
