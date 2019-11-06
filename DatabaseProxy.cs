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

        private object Lock = new object();

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
            lock (Lock)
            {
                DatabaseInfo info = new DatabaseInfo(Path, IsOpen);

                if (IsOpen)
                {
                    var stream = client.GetStream();
                    SendMessage(stream, "{\"command\":\"stats\"}");

                    var response = ReceiveMessage(stream);
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
        }

        public void Open(string path)
        {
            lock (Lock)
            {
                if (IsOpen) Close();

                Path = path;
                path = path.Replace("\\", "\\\\"); // we stringify it naively to json so we need to escape manually

                var stream = client.GetStream();
                SendMessage(stream, "{\"command\":\"open\",\"database_path\":\"" + path + "\"}");

                while (true)
                {
                    var response = ReceiveMessage(stream);
                    var json = JsonValue.Parse(response);
                    if (json.ContainsKey("error"))
                    {
                        throw new InvalidDataException("Cannot open database. " + json["error"].ToString());
                    }
                    else if (json.ContainsKey("finished"))
                    {
                        if (json["finished"] == true) break;
                    }
                }

                IsOpen = true;
            }
        }

        private QueryResponse ExecuteQuery(string query)
        {
            lock (Lock)
            {
                var stream = client.GetStream();

                SendMessage(stream, query);

                var responseJson = JsonValue.Parse(ReceiveMessage(stream));
                if (responseJson.ContainsKey("error"))
                {
                    throw new InvalidDataException(responseJson["error"].ToString());
                }

                return QueryResponse.FromJson(responseJson);
            }
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
            lock (Lock)
            {
                if (!IsOpen) return;
                Path = "";
                IsOpen = false;

                try
                {
                    var stream = client.GetStream();
                    SendMessage(stream, "{\"command\":\"close\"}");

                    var responseJson = JsonValue.Parse(ReceiveMessage(stream));
                    if (responseJson.ContainsKey("error"))
                    {
                        throw new InvalidDataException(responseJson["error"].ToString());
                    }
                }
                catch
                {
                }
            }
        }

        public void Dispose()
        {
            lock (Lock)
            {
                try
                {
                    var stream = client.GetStream();
                    SendMessage(stream, "{\"command\":\"exit\"}");
                    process.WaitForExit();
                }
                catch
                {
                }
                client.Close();
            }
        }

        private void SendMessage(NetworkStream stream, string message)
        {
            uint xorValue = 3173045653u;

            var bytes = System.Text.Encoding.UTF8.GetBytes(message);
            uint size = (uint)bytes.Length;
            uint xoredSize = size ^ xorValue;

            byte[] sizeStr = new byte[8];
            sizeStr[0] = (byte)(size % 256); size /= 256;
            sizeStr[1] = (byte)(size % 256); size /= 256;
            sizeStr[2] = (byte)(size % 256); size /= 256;
            sizeStr[3] = (byte)(size);

            sizeStr[4] = (byte)(xoredSize % 256); xoredSize /= 256;
            sizeStr[5] = (byte)(xoredSize % 256); xoredSize /= 256;
            sizeStr[6] = (byte)(xoredSize % 256); xoredSize /= 256;
            sizeStr[7] = (byte)(xoredSize);

            stream.Write(sizeStr, 0, 8);
            stream.Write(bytes, 0, bytes.Length);
        }

        private string ReceiveMessage(NetworkStream stream)
        {
            uint maxMessageLength = 4 * 1024 * 1024;
            uint xorValue = 3173045653u;

            byte[] readLengthBuffer = new byte[8];
            if(stream.Read(readLengthBuffer, 0, 8) != 8)
            {
                throw new InvalidDataException("Message length not received in one packet.");
            }
            uint length = 0;
            length += (uint)(readLengthBuffer[3]); length *= 256;
            length += (uint)(readLengthBuffer[2]); length *= 256;
            length += (uint)(readLengthBuffer[1]); length *= 256;
            length += (uint)(readLengthBuffer[0]);
            uint lengthXor = 0;
            lengthXor += (uint)(readLengthBuffer[7]); lengthXor *= 256;
            lengthXor += (uint)(readLengthBuffer[6]); lengthXor *= 256;
            lengthXor += (uint)(readLengthBuffer[5]); lengthXor *= 256;
            lengthXor += (uint)(readLengthBuffer[4]);
            lengthXor ^= xorValue;

            if (length != lengthXor)
            {
                throw new InvalidDataException("Length doesn't match length xor.");
            }
            else if (length == 0)
            {
                return "";
            }

            if (length > maxMessageLength)
            {
                throw new InvalidDataException("Message too long.");
            }

            byte[] readBuffer = new byte[length];
            int totalRead = 0;
            while(totalRead < length)
            {
                int leftToRead = (int)length - totalRead;
                totalRead += stream.Read(readBuffer, totalRead, leftToRead);
            }

            var response = Encoding.UTF8.GetString(readBuffer);
            return response;
        }

        public void Dump(List<string> pgns, string outPath, string tempPath, int minCount, Action<JsonValue> callback)
        {
            lock (Lock)
            {
                var stream = client.GetStream();

                var json = new JsonObject();
                json.Add("command", "dump");
                json.Add("output_path", outPath);
                if (tempPath != null)
                {
                    json.Add("temporary_path", tempPath);
                }
                json.Add("min_count", minCount);
                json.Add("report_progress", true);

                var pgnsJson = new JsonArray();
                foreach (string pgn in pgns)
                {
                    pgnsJson.Add(pgn);
                }
                json.Add("pgns", pgnsJson);

                SendMessage(stream, json.ToString());

                while (true)
                {
                    var response = ReceiveMessage(stream);
                    var responseJson = JsonValue.Parse(response);
                    if (responseJson.ContainsKey("error"))
                    {
                        throw new InvalidDataException(responseJson["error"].ToString());
                    }
                    else if (responseJson.ContainsKey("operation"))
                    {
                        if (responseJson["operation"] == "import"
                            || responseJson["operation"] == "dump")
                        {
                            callback.Invoke(responseJson);
                        }
                        if (responseJson["operation"] == "dump")
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

        public void Create(JsonValue json, Action<JsonValue> callback)
        {
            lock (Lock)
            {
                var stream = client.GetStream();

                SendMessage(stream, json.ToString());

                while (true)
                {
                    var response = ReceiveMessage(stream);
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

        internal ulong TotalNumGames()
        {
            return NumHumanGames + NumServerGames + NumEngineGames;
        }

        internal ulong TotalNumPositions()
        {
            return NumHumanPositions + NumServerPositions + NumEnginePositions;
        }
    }
}
