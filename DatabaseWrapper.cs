using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Json;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace chess_pos_db_gui
{
    class DatabaseWrapper
    {
        private TcpClient client { get; set; }
        private Process process { get; set; }

        public DatabaseWrapper(string path, string address, int port, int numTries = 3, int msBetweenTries = 1000)
        {
            process = new Process();
            process.StartInfo.FileName = "chess_pos_db.exe";
            process.StartInfo.Arguments = "tcp " + port;

            // TODO: Setting this to true makes the program hang after a few queries
            //       Find a fix as it will be needed for reporting progress to the user.
            // process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true; 
            process.Start();

            while(numTries-- > 0)
            {
                try
                {
                    client = new TcpClient(address, port);

                    var bytes = System.Text.Encoding.UTF8.GetBytes("{\"command\":\"open\", \"database_path\":\"" + path + "\"}");
                    var stream = client.GetStream();
                    stream.Write(bytes, 0, bytes.Length);

                    var response = Read(stream);
                    if (JsonValue.Parse(response).ContainsKey("error"))
                    {
                        throw new InvalidDataException("Cannot open database.");
                    }

                    break;
                }
                catch
                {
                    Thread.Sleep(msBetweenTries);
                }
            }
        }

        private QueryResponse ExecuteQuery(string query)
        {
            var stream = client.GetStream();

            var bytes = System.Text.Encoding.UTF8.GetBytes(query);
            stream.Write(bytes, 0, bytes.Length);

            var responseJson = JsonValue.Parse(Read(stream));
            if (responseJson.ContainsKey("error"))
            {
                throw new InvalidDataException(responseJson["error"].ToString());
            }

            return QueryResponse.FromJson(responseJson);
        }

        public QueryResponse Query(string fen)
        {
            string query = "{\"command\":\"query\", \"query\":{\"continuations\":{\"fetch_children\":true,\"fetch_first_game\":true,\"fetch_first_game_for_each_child\":true,\"fetch_last_game\":false,\"fetch_last_game_for_each_child\":false},\"levels\":[\"human\",\"engine\",\"server\"],\"positions\":[{\"fen\":\"" + fen + "\"}],\"results\":[\"win\",\"loss\",\"draw\"],\"token\":\"toktok\",\"transpositions\":{\"fetch_children\":true,\"fetch_first_game\":true,\"fetch_first_game_for_each_child\":true,\"fetch_last_game\":false,\"fetch_last_game_for_each_child\":false}}}";
            return ExecuteQuery(query);   
        }
        public QueryResponse Query(string fen, string san)
        {
            System.Diagnostics.Debug.WriteLine(san);
            string query = "{\"command\":\"query\", \"query\":{\"continuations\":{\"fetch_children\":true,\"fetch_first_game\":true,\"fetch_first_game_for_each_child\":true,\"fetch_last_game\":false,\"fetch_last_game_for_each_child\":false},\"levels\":[\"human\",\"engine\",\"server\"],\"positions\":[{\"fen\":\"" + fen + "\", \"move\":\"" + san + "\"}],\"results\":[\"win\",\"loss\",\"draw\"],\"token\":\"toktok\",\"transpositions\":{\"fetch_children\":true,\"fetch_first_game\":true,\"fetch_first_game_for_each_child\":true,\"fetch_last_game\":false,\"fetch_last_game_for_each_child\":false}}}";
            return ExecuteQuery(query);
        }

        public void Close()
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes("{\"command\":\"exit\"}");
            var stream = client.GetStream();
            stream.Write(bytes, 0, bytes.Length);
            process.WaitForExit();
            client.Close();
        }

        private string Read(NetworkStream stream)
        {
            using (var writer = new MemoryStream())
            {
                byte[] readBuffer = new byte[client.ReceiveBufferSize];
                do
                {
                    int numberOfBytesRead = stream.Read(readBuffer, 0, readBuffer.Length);
                    if (numberOfBytesRead <= 0)
                    {
                        break;
                    }
                    writer.Write(readBuffer, 0, numberOfBytesRead);
                }
                while (stream.DataAvailable);

                var response = Encoding.UTF8.GetString(writer.ToArray());
                return response;
            }
        }
    }
}
