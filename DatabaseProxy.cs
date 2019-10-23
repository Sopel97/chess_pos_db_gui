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
    public class DatabaseProxy
    {
        private TcpClient client { get; set; }
        private Process process { get; set; }

        public bool IsOpen { get; private set; }

        public string Path { get; private set; }

        public DatabaseProxy(string address, int port, int numTries = 3, int msBetweenTries = 1000)
        {
            IsOpen = false;
            Path = "";

            var processName = "chess_pos_db";

            {
                var collidingProcessses = System.Diagnostics.Process.GetProcessesByName(processName);
                System.Diagnostics.Debug.WriteLine("Killing " + collidingProcessses.Length + " colliding processes...");
                foreach(var process in collidingProcessses)
                {
                    process.Kill();
                }
            }

            process = new Process();
            process.StartInfo.FileName = processName + ".exe";
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

                    break;
                }
                catch (SocketException)
                {
                    if (numTries == 0)
                    {
                        process.Kill();
                        throw new InvalidDataException("Cannot open communication channel with the database.");
                    }
                    else
                    {
                        Thread.Sleep(msBetweenTries);
                    }
                }
                catch
                {
                    process.Kill();
                    throw;
                }
            }
        }

        public DatabaseInfo GetInfo()
        {
            DatabaseInfo info = new DatabaseInfo(Path, IsOpen);
            if (IsOpen)
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes("{\"command\":\"stats\"}");
                var stream = client.GetStream();
                stream.Write(bytes, 0, bytes.Length);

                var response = Read(stream);
                var json = JsonValue.Parse(response);
                if (json.ContainsKey("error"))
                {
                    throw new InvalidDataException("Cannot fetch database info.");
                }
                else
                {
                    info.SetCounts(json);
                }
            }
            return info;
        }

        public void Open(string path)
        {
            if (IsOpen) Close();

            Path = path;
            path = path.Replace("\\", "\\\\"); // we stringify it naively to json so we need to escape manually

            var bytes = System.Text.Encoding.UTF8.GetBytes("{\"command\":\"open\",\"database_path\":\"" + path + "\"}");
            var stream = client.GetStream();
            stream.Write(bytes, 0, bytes.Length);

            while (true)
            {
                var response = Read(stream);
                var json = JsonValue.Parse(response);
                if (json.ContainsKey("error"))
                {
                    throw new InvalidDataException("Cannot open database.");
                }
                else if (json.ContainsKey("finished"))
                {
                    if (json["finished"] == true) break;
                }
            }

            IsOpen = true;
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
            string query = "{\"command\":\"query\", \"query\":{\"continuations\":{\"fetch_children\":true,\"fetch_first_game\":true,\"fetch_first_game_for_each_child\":true,\"fetch_last_game\":false,\"fetch_last_game_for_each_child\":false},\"levels\":[\"human\",\"engine\",\"server\"],\"positions\":[{\"fen\":\"" + fen + "\", \"move\":\"" + san + "\"}],\"results\":[\"win\",\"loss\",\"draw\"],\"token\":\"toktok\",\"transpositions\":{\"fetch_children\":true,\"fetch_first_game\":true,\"fetch_first_game_for_each_child\":true,\"fetch_last_game\":false,\"fetch_last_game_for_each_child\":false}}}";
            return ExecuteQuery(query);
        }

        public void Close()
        {
            if (!IsOpen) return;
            Path = "";
            IsOpen = false;

            var bytes = System.Text.Encoding.UTF8.GetBytes("{\"command\":\"close\"}");
            try
            {
                var stream = client.GetStream();
                stream.Write(bytes, 0, bytes.Length);

                var responseJson = JsonValue.Parse(Read(stream));
                if (responseJson.ContainsKey("error"))
                {
                    throw new InvalidDataException(responseJson["error"].ToString());
                }
            }
            catch
            {
            }
        }

        public void Dispose()
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes("{\"command\":\"exit\"}");
            try
            {
                var stream = client.GetStream();
                stream.Write(bytes, 0, bytes.Length);
                process.WaitForExit();
            }
            catch
            {
            }
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

        public void Create(JsonValue json, Action<JsonValue> callback)
        {
            var stream = client.GetStream();

            var bytes = System.Text.Encoding.UTF8.GetBytes(json.ToString());
            stream.Write(bytes, 0, bytes.Length);

            while (true)
            {
                var response = Read(stream);
                var responseJson = JsonValue.Parse(response);
                if (responseJson.ContainsKey("error"))
                {
                    throw new InvalidDataException(responseJson["error"].ToString());
                }
                else if (responseJson.ContainsKey("operation"))
                {
                    if (responseJson["operation"] == "import"
                        || responseJson["operation"] == "merge")
                    {
                        callback.Invoke(responseJson);
                    }
                    else if (responseJson["operation"] == "create")
                    {
                        if (responseJson["finished"] == true)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }

    public class DatabaseInfo
    {
        public bool IsOpen { get; private set; }
        public string Path { get; private set; }

        public ulong NumHumanGames { get; private set; }
        public ulong NumEngineGames { get; private set; }
        public ulong NumServerGames { get; private set; }
        public ulong NumHumanPositions { get; private set; }
        public ulong NumEnginePositions { get; private set; }
        public ulong NumServerPositions { get; private set; }

        public DatabaseInfo(string path, bool isOpen)
        {
            Path = path;
            IsOpen = isOpen;
        }

        public void SetCounts(JsonValue json)
        {
            NumHumanGames = json["human"]["num_games"];
            NumEngineGames = json["engine"]["num_games"];
            NumServerGames = json["server"]["num_games"];

            NumHumanPositions = json["human"]["num_positions"];
            NumEnginePositions = json["engine"]["num_positions"];
            NumServerPositions = json["server"]["num_positions"];
        }
    }
}
