﻿using chess_pos_db_gui.src.app.forms;
using chess_pos_db_gui.src.util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Json;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace chess_pos_db_gui
{
    public class DatabaseProxy
    {
        private TcpClient Client { get; set; }
        private Process Process { get; set; }

        public bool IsOpen { get; private set; }

        public string Path { get; private set; }

        private Dictionary<string, DatabaseSupportManifest> SupportManifests { get; set; } = null;
        private DatabaseManifest Manifest { get; set; } = null;

        private readonly object Lock = new object();

        public DatabaseProxy(string address, int port, int numTries = 3, int msBetweenTries = 1000)
        {
            IsOpen = false;
            Path = "";

            var processName = "chess_pos_db";

            {
                var collidingProcessses = System.Diagnostics.Process.GetProcessesByName(processName);
                System.Diagnostics.Debug.WriteLine("Killing " + collidingProcessses.Length + " colliding processes...");
                foreach (var process in collidingProcessses)
                {
                    process.Kill();
                }
            }

            Process = new Process();
            Process.StartInfo.FileName = processName + ".exe";
            Process.StartInfo.Arguments = string.Format("tcp --port {0}", port);
            System.Diagnostics.Debug.WriteLine(Process.StartInfo.Arguments);

            // TODO: Setting this to true makes the program hang after a few queries
            //       Find a fix as it will be needed for reporting progress to the user.
            // process.StartInfo.RedirectStandardOutput = true;
            Process.StartInfo.RedirectStandardInput = true;
            Process.StartInfo.UseShellExecute = false;
            Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            Process.StartInfo.CreateNoWindow = true;
            Process.Start();

            while (numTries-- > 0)
            {
                try
                {
                    Client = new TcpClient(address, port);

                    break;
                }
                catch (SocketException)
                {
                    if (numTries == 0)
                    {
                        Process.Kill();
                        throw new InvalidDataException("Cannot open communication channel with the database.");
                    }
                    else
                    {
                        Thread.Sleep(msBetweenTries);
                    }
                }
                catch
                {
                    Process.Kill();
                    throw;
                }
            }
        }

        public void FetchSupportManifest()
        {
            lock (Lock)
            {
                var stream = Client.GetStream();
                SendMessage(stream, "{\"command\":\"support\"}");

                var response = ReceiveMessage(stream);
                var json = JsonValue.Parse(response);
                if (json.ContainsKey("error"))
                {
                    throw new InvalidDataException("Cannot fetch database info.");
                }

                SupportManifests = new Dictionary<string, DatabaseSupportManifest>();
                JsonObject supportManifestsJson = json["support_manifests"] as JsonObject;
                foreach (string key in supportManifestsJson.Keys)
                {
                    SupportManifests.Add(key, new DatabaseSupportManifest(supportManifestsJson[key]));
                }
            }
        }

        public string GetDefaultDatabaseSchema()
        {
            return GetRichestDatabaseSchema();
        }

        private int GetDatabaseFormatRichness(DatabaseSupportManifest manifest)
        {
            int r = 0;

            if (manifest.HasWhiteElo || manifest.HasBlackElo)
                r += 1;
            if (manifest.HasCount)
                r += 1;
            if (manifest.HasEloDiff)
                r += 1;
            if (manifest.HasReverseMove)
                r += 1;
            if (manifest.HasFirstGame)
                r += 1;
            if (manifest.HasLastGame)
                r += 1;
            if (manifest.AllowsFilteringTranspositions)
                r += 1;
            if (manifest.AllowsFilteringByEloRange)
                r += 1;
            if (manifest.AllowsFilteringByMonthRange)
                r += 1;

            return r;
        }

        public string GetRichestDatabaseSchema()
        {
            int bestR = int.MaxValue;
            string bestName = "";

            foreach ((string name, var manifest) in GetSupportManifests())
            {
                int r = GetDatabaseFormatRichness(manifest);
                if (bestName == "" || r > bestR)
                {
                    bestName = name;
                    bestR = r;
                }
            }

            return bestName;
        }

        public Dictionary<string, DatabaseSupportManifest> GetSupportManifests()
        {
            if (SupportManifests == null)
            {
                FetchSupportManifest();
            }
            return SupportManifests;
        }

        public DatabaseSupportManifest GetSupportManifest()
        {
            if (!IsOpen)
            {
                return null;
            }

            if (SupportManifests == null)
            {
                FetchSupportManifest();
            }

            return SupportManifests[GetDatabaseSchema()];
        }

        public void FetchManifest()
        {
            lock (Lock)
            {
                if (!IsOpen)
                {
                    return;
                }

                var stream = Client.GetStream();
                SendMessage(stream, "{\"command\":\"manifest\"}");

                var response = ReceiveMessage(stream);
                var json = JsonValue.Parse(response);
                if (json.ContainsKey("error"))
                {
                    throw new InvalidDataException("Cannot fetch database manifest.");
                }

                Manifest = new DatabaseManifest(json["manifest"]);
            }
        }

        public IList<string> GetSupportedDatabaseSchemas()
        {
            if (SupportManifests == null)
            {
                FetchSupportManifest();
            }

            return new List<string>(SupportManifests.Keys);
        }

        public MergeMode GetMergeMode()
        {
            if (!IsOpen)
            {
                throw new InvalidOperationException("Database is not open");
            }

            if (SupportManifests == null)
            {
                FetchSupportManifest();
            }

            return SupportManifests[GetDatabaseSchema()].MergeMode;
        }

        public string GetDatabaseSchema()
        {
            if (Manifest == null)
            {
                FetchManifest();
            }

            return Manifest.Schema;
        }

        public Dictionary<string, List<DatabaseMergableFile>> GetMergableFiles()
        {
            lock (Lock)
            {
                if (!IsOpen)
                {
                    throw new InvalidOperationException("Database is not open");
                }

                var stream = Client.GetStream();
                SendMessage(stream, "{\"command\":\"mergable_files\"}");

                var response = ReceiveMessage(stream);
                var json = JsonValue.Parse(response);
                if (json.ContainsKey("error"))
                {
                    throw new InvalidDataException("Cannot fetch mergable files.");
                }

                var mergableFiles = new Dictionary<string, List<DatabaseMergableFile>>();
                JsonObject mergableFilesJson = json["mergable_files"] as JsonObject;
                foreach (string partition in mergableFilesJson.Keys)
                {
                    var list = new List<DatabaseMergableFile>();
                    foreach (JsonValue val in mergableFilesJson[partition])
                    {
                        list.Add(new DatabaseMergableFile(val));
                    }
                    mergableFiles.Add(partition, list);
                }

                return mergableFiles;
            }
        }

        public DatabaseInfo GetInfo()
        {
            lock (Lock)
            {
                DatabaseInfo info = new DatabaseInfo(Path, IsOpen);

                if (IsOpen)
                {
                    var stream = Client.GetStream();
                    SendMessage(stream, "{\"command\":\"stats\"}");

                    var response = ReceiveMessage(stream);
                    var json = JsonValue.Parse(response);
                    if (json.ContainsKey("error"))
                    {
                        throw new InvalidDataException("Cannot fetch database info.");
                    }
                    else
                    {
                        info.SetStatsFromJson(json);
                    }
                }
                return info;
            }
        }

        public void Open(string path)
        {
            lock (Lock)
            {
                if (IsOpen)
                {
                    Close();
                }

                Path = path;
                path = path.Replace("\\", "\\\\"); // we stringify it naively to json so we need to escape manually

                var stream = Client.GetStream();
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
                        if (json["finished"] == true)
                        {
                            break;
                        }
                    }
                }

                IsOpen = true;
            }
        }

        private QueryResponse ExecuteQuery(string query)
        {
            lock (Lock)
            {
                var stream = Client.GetStream();

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
            string query = "{\"command\":\"query\", \"query\":{\"continuations\":{\"fetch_children\":true,\"fetch_first_game\":true,\"fetch_first_game_for_each_child\":true,\"fetch_last_game\":false,\"fetch_last_game_for_each_child\":false},\"levels\":[\"human\",\"engine\",\"server\"],\"positions\":[{\"fen\":\"" + fen + "\"}],\"results\":[\"win\",\"loss\",\"draw\"],\"token\":\"toktok\",\"transpositions\":{\"fetch_children\":true,\"fetch_first_game\":true,\"fetch_first_game_for_each_child\":true,\"fetch_last_game\":false,\"fetch_last_game_for_each_child\":false},\"retractions\":{\"fetch_first_game_for_each\":true,\"fetch_last_game_for_each\":false}}}";
            return ExecuteQuery(query);
        }
        public QueryResponse Query(string fen, string san)
        {
            string query = "{\"command\":\"query\", \"query\":{\"continuations\":{\"fetch_children\":true,\"fetch_first_game\":true,\"fetch_first_game_for_each_child\":true,\"fetch_last_game\":false,\"fetch_last_game_for_each_child\":false},\"levels\":[\"human\",\"engine\",\"server\"],\"positions\":[{\"fen\":\"" + fen + "\", \"move\":\"" + san + "\"}],\"results\":[\"win\",\"loss\",\"draw\"],\"token\":\"toktok\",\"transpositions\":{\"fetch_children\":true,\"fetch_first_game\":true,\"fetch_first_game_for_each_child\":true,\"fetch_last_game\":false,\"fetch_last_game_for_each_child\":false},\"retractions\":{\"fetch_first_game_for_each\":true,\"fetch_last_game_for_each\":false}}}";
            return ExecuteQuery(query);
        }

        public void Close()
        {
            lock (Lock)
            {
                if (!IsOpen)
                {
                    return;
                }

                Manifest = null;

                Path = "";
                IsOpen = false;

                try
                {
                    var stream = Client.GetStream();
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
                    var stream = Client.GetStream();
                    SendMessage(stream, "{\"command\":\"exit\"}");
                    Process.WaitForExit();
                }
                catch
                {
                }
                Client.Close();
            }
        }

        private void SendMessage(NetworkStream stream, string message)
        {
            uint xorValue = 3173045653u;

            var bytes = System.Text.Encoding.UTF8.GetBytes(message);
            uint size = (uint)bytes.Length;
            uint xoredSize = size ^ xorValue;

            byte[] sizeStr = new byte[8];
            sizeStr[0] = (byte)(size % 256);
            size /= 256;
            sizeStr[1] = (byte)(size % 256);
            size /= 256;
            sizeStr[2] = (byte)(size % 256);
            size /= 256;
            sizeStr[3] = (byte)(size);

            sizeStr[4] = (byte)(xoredSize % 256);
            xoredSize /= 256;
            sizeStr[5] = (byte)(xoredSize % 256);
            xoredSize /= 256;
            sizeStr[6] = (byte)(xoredSize % 256);
            xoredSize /= 256;
            sizeStr[7] = (byte)(xoredSize);

            stream.Write(sizeStr, 0, 8);
            stream.Write(bytes, 0, bytes.Length);
        }

        private string ReceiveMessage(NetworkStream stream)
        {
            uint maxMessageLength = 4 * 1024 * 1024;
            uint xorValue = 3173045653u;

            byte[] readLengthBuffer = new byte[8];
            if (stream.Read(readLengthBuffer, 0, 8) != 8)
            {
                throw new InvalidDataException("Message length not received in one packet.");
            }
            uint length = 0;
            length += readLengthBuffer[3];
            length *= 256;
            length += readLengthBuffer[2];
            length *= 256;
            length += readLengthBuffer[1];
            length *= 256;
            length += readLengthBuffer[0];
            uint lengthXor = 0;
            lengthXor += readLengthBuffer[7];
            lengthXor *= 256;
            lengthXor += readLengthBuffer[6];
            lengthXor *= 256;
            lengthXor += readLengthBuffer[5];
            lengthXor *= 256;
            lengthXor += readLengthBuffer[4];
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
            while (totalRead < length)
            {
                int leftToRead = (int)length - totalRead;
                totalRead += stream.Read(readBuffer, totalRead, leftToRead);
            }

            var response = Encoding.UTF8.GetString(readBuffer);
            return response;
        }

        public void Dump(
            List<string> pgns, 
            string outPath, 
            List<string> tempPaths, 
            EpdDumpFilters filters, 
            List<EpdDumpOutputElement> outputElements, 
            Action<JsonValue> callback
            )
        {
            if (outputElements.Count == 0)
            {
                throw new ArgumentException("No output elements specified.");
            }

            lock (Lock)
            {
                var stream = Client.GetStream();

                var json = new JsonObject
                {
                    { "command", "dump" },
                    { "output_path", outPath }
                };
                json.Add("temporary_paths", new JsonArray(tempPaths.Select(t =>
                {
                    JsonValue s = t;
                    return s;
                })));
                json.Add("min_count", filters.MinCount);
                json.Add("max_ply", filters.MaxPly);
                json.Add("min_pieces", filters.MinPieces);
                json.Add("report_progress", true);
                json.Add("output", new JsonArray(outputElements.Select(t =>
                {
                    JsonValue s = t.GetName();
                    return s;
                })));

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

        internal IList<string> GetSupportedExtensionsForType(string name)
        {
            if (SupportManifests == null)
            {
                FetchSupportManifest();
            }

            return SupportManifests[name].SupportedExtensions;
        }

        public void Create(JsonValue json, Action<JsonValue> callback)
        {
            lock (Lock)
            {
                var stream = Client.GetStream();

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

        public void Append(JsonValue json, Action<JsonValue> callback)
        {
            lock (Lock)
            {
                var stream = Client.GetStream();

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
                        else if (responseJson["operation"] == "append")
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

        public void Merge(string partition, List<string> names, List<string> temps, ulong? maxSpace, Action<JsonValue> callback)
        {
            lock (Lock)
            {
                JsonObject request = new JsonObject
                {
                    { "command", "merge" },
                    { "report_progress", true },
                    { "partition", partition },
                    { "files", new JsonArray(names.Select(t => (JsonValue)t)) },
                    { "temporary_paths", new JsonArray(temps.Select(t =>
                        {
                            JsonValue s = t;
                            return s;
                        })) }
                };
                if (maxSpace.HasValue)
                {
                    request.Add("temporary_space", maxSpace.Value);
                }

                var stream = Client.GetStream();

                SendMessage(stream, request.ToString());

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
                        if (responseJson["operation"] == "merge")
                        {
                            callback.Invoke(responseJson);

                            if (responseJson.ContainsKey("finished") && responseJson["finished"] == true)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    public class EpdDumpFilters
    {
        public int MinCount { get; set; }
        public int MaxPly { get; set; }
        public int MinPieces { get; set; }
    }

    public enum EpdDumpOutputElement
    {
        Fen,
        WinCount,
        DrawCount,
        LossCount,
        Perf
    }

    static class EpdDumpOutputElementHelper
    {
        static public string GetName(this EpdDumpOutputElement elem)
        {
            switch (elem)
            {
                case EpdDumpOutputElement.Fen:
                    return "fen";
                case EpdDumpOutputElement.WinCount:
                    return "win_count";
                case EpdDumpOutputElement.DrawCount:
                    return "draw_count";
                case EpdDumpOutputElement.LossCount:
                    return "loss_count";
                case EpdDumpOutputElement.Perf:
                    return "perf";
            }

            throw new ArgumentException("Invalid elem");
        }
    }

    public enum MergeMode
    {
        None,
        Consecutive,
        Any
    }

    public class DatabaseMergableFile
    {
        public string Name { get; private set; }
        public ulong Size { get; private set; }

        public DatabaseMergableFile(JsonValue json)
        {
            Name = json["name"];
            Size = json["size"];
        }
    }

    public class DatabaseSingleLevelStats
    {
        public ulong NumGames { get; private set; }
        public ulong NumPositions { get; private set; }
        public ulong TotalWhiteElo { get; private set; }
        public ulong TotalBlackElo { get; private set; }
        public ulong NumGamesWithElo { get; private set; }
        public ulong NumGamesWithDate { get; private set; }
        public ulong MinElo { get; private set; }
        public ulong MaxElo { get; private set; }
        public Date MinDate { get; private set; }
        public Date MaxDate { get; private set; }

        public DatabaseSingleLevelStats(JsonValue json)
        {
            NumGames = json["num_games"];
            NumPositions = json["num_positions"];
            TotalWhiteElo = json["total_white_elo"];
            TotalBlackElo = json["total_black_elo"];
            NumGamesWithElo = json["num_games_with_elo"];
            NumGamesWithDate = json["num_games_with_date"];
            if (NumGamesWithElo > 0)
            {
                MinElo = json["min_elo"];
                MaxElo = json["max_elo"];
            }
            if (NumGamesWithDate > 0)
            {
                MinDate = Date.FromString(json["min_date"], '-');
                MaxDate = Date.FromString(json["max_date"], '-');
            }
        }

        public DatabaseSingleLevelStats(DatabaseSingleLevelStats other)
        {
            NumGames = other.NumGames;
            NumPositions = other.NumPositions;
            TotalWhiteElo = other.TotalWhiteElo;
            TotalBlackElo = other.TotalBlackElo;
            NumGamesWithElo = other.NumGamesWithElo;
            NumGamesWithDate = other.NumGamesWithDate;
            MinElo = other.MinElo;
            MaxElo = other.MaxElo;
            MinDate = other.MinDate;
            MaxDate = other.MaxDate;
        }

        public void Add(DatabaseSingleLevelStats other)
        {
            NumGames += other.NumGames;
            NumPositions += other.NumPositions;
            TotalWhiteElo += other.TotalWhiteElo;
            TotalBlackElo += other.TotalBlackElo;

            if (NumGamesWithElo == 0)
            {
                MinElo = other.MinElo;
                MaxElo = other.MaxElo;
            }
            else if (other.NumGamesWithElo != 0)
            {
                MinElo = Math.Min(MinElo, other.MinElo);
                MaxElo = Math.Min(MaxElo, other.MaxElo);
            }

            if (NumGamesWithDate == 0)
            {
                MinDate = other.MinDate;
                MaxDate = other.MaxDate;
            }
            else if (other.NumGamesWithDate != 0)
            {
                MinDate = Date.Min(MinDate, other.MinDate);
                MaxDate = Date.Max(MaxDate, other.MaxDate);
            }

            NumGamesWithElo += other.NumGamesWithElo;
            NumGamesWithDate += other.NumGamesWithDate;
        }
    }

    public class DatabaseSingleLevelImportStats : DatabaseSingleLevelStats
    {
        public ulong NumSkippedGames { get; private set; }

        public DatabaseSingleLevelImportStats(JsonValue json) :
            base(json)
        {
            NumSkippedGames = json["num_skipped_games"];
        }
        public DatabaseSingleLevelImportStats(DatabaseSingleLevelImportStats other) :
            base(other)
        {
            NumSkippedGames = other.NumSkippedGames;
        }

        public void Add(DatabaseSingleLevelImportStats other)
        {
            base.Add(other);
            NumSkippedGames += other.NumSkippedGames;
        }
    }

    public class DatabaseStats
    {
        public Dictionary<GameLevel, DatabaseSingleLevelStats> StatsByLevel { get; private set; }

        public DatabaseStats(JsonValue json)
        {
            StatsByLevel = new Dictionary<GameLevel, DatabaseSingleLevelStats>();
            StatsByLevel.Add(GameLevel.Engine, new DatabaseSingleLevelStats(json["engine"]));
            StatsByLevel.Add(GameLevel.Human, new DatabaseSingleLevelStats(json["human"]));
            StatsByLevel.Add(GameLevel.Server, new DatabaseSingleLevelStats(json["server"]));
        }

        public DatabaseSingleLevelStats GetTotal()
        {
            DatabaseSingleLevelStats total = new DatabaseSingleLevelStats(StatsByLevel[GameLevel.Engine]);
            total.Add(StatsByLevel[GameLevel.Human]);
            total.Add(StatsByLevel[GameLevel.Server]);
            return total;
        }
    }

    public class DatabaseImportStats
    {
        public Dictionary<GameLevel, DatabaseSingleLevelImportStats> StatsByLevel { get; private set; }

        public DatabaseImportStats(JsonValue json)
        {
            StatsByLevel = new Dictionary<GameLevel, DatabaseSingleLevelImportStats>();
            StatsByLevel.Add(GameLevel.Engine, new DatabaseSingleLevelImportStats(json["engine"]));
            StatsByLevel.Add(GameLevel.Human, new DatabaseSingleLevelImportStats(json["human"]));
            StatsByLevel.Add(GameLevel.Server, new DatabaseSingleLevelImportStats(json["server"]));
        }

        public DatabaseSingleLevelImportStats GetTotal()
        {
            DatabaseSingleLevelImportStats total = new DatabaseSingleLevelImportStats(StatsByLevel[GameLevel.Engine]);
            total.Add(StatsByLevel[GameLevel.Human]);
            total.Add(StatsByLevel[GameLevel.Server]);
            return total;
        }
    }

    public class DatabaseManifest
    {
        public string Schema { get; private set; }
        public string Version { get; private set; }

        public DatabaseManifest(JsonValue json)
        {
            Schema = json["schema"];
            Version = json["version"];
        }
    }

    public class DatabaseSupportManifest
    {
        public IList<string> SupportedExtensions { get; private set; }
        public MergeMode MergeMode { get; private set; }

        public ulong MaxGames { get; private set; }
        public ulong MaxPositions { get; private set; }
        public ulong MaxInstancesOfSinglePosition { get; private set; }

        public bool HasOneWayKey { get; private set; }
        public ulong EstimatedMaxCollisions { get; private set; }
        public ulong EstimatedMaxPositionsWithNoCollisions { get; private set; }

        public bool HasCount { get; private set; }

        public bool HasEloDiff { get; private set; }
        public ulong MaxAbsEloDiff { get; private set; }
        public ulong MaxAverageAbsEloDiff { get; private set; }

        public bool HasWhiteElo { get; private set; }
        public bool HasBlackElo { get; private set; }
        public ulong MinElo { get; private set; }
        public ulong MaxElo { get; private set; }
        public bool HasCountWithElo { get; private set; }

        public bool HasFirstGame { get; private set; }
        public bool HasLastGame { get; private set; }

        public bool AllowsFilteringTranspositions { get; private set; }
        public bool HasReverseMove { get; private set; }

        public bool AllowsFilteringByEloRange { get; private set; }
        public ulong EloFilterGranularity { get; private set; }

        public bool AllowsFilteringByMonthRange { get; private set; }
        public ulong MonthFilterGranularity { get; private set; }

        public ulong MaxBytesPerPosition { get; private set; }
        public Optional<ulong> EstimatedAverageBytesPerPosition{ get; private set; }

        public DatabaseSupportManifest(JsonValue json)
        {
            SupportedExtensions = new List<string>();

            foreach(JsonValue ext in json["supported_file_types"])
            {
                SupportedExtensions.Add(ext);
            }

            switch ((string)json["merge_mode"])
            {
                case "none":
                    MergeMode = MergeMode.None;
                    break;

                case "consecutive":
                    MergeMode = MergeMode.Consecutive;
                    break;

                case "any":
                    MergeMode = MergeMode.Any;
                    break;
            }

            MaxGames = json["max_games"];
            MaxPositions = json["max_positions"];
            MaxInstancesOfSinglePosition = json["max_instances_of_single_position"];

            HasOneWayKey = json["has_one_way_key"];
            if (HasOneWayKey)
            {
                EstimatedMaxCollisions = json["estimated_max_collisions"];
                EstimatedMaxPositionsWithNoCollisions = json["estimated_max_positions_with_no_collisions"];
            }

            HasCount = json["has_count"];

            HasEloDiff = json["has_elo_diff"];
            if (HasEloDiff)
            {
                MaxAbsEloDiff = json["max_abs_elo_diff"];
                MaxAverageAbsEloDiff = json["max_average_abs_elo_diff"];
            }

            HasWhiteElo = json["has_white_elo"];
            HasBlackElo = json["has_black_elo"];
            if (HasWhiteElo || HasBlackElo)
            {
                MinElo = json["min_elo"];
                MaxElo = json["max_elo"];
                HasCountWithElo = json["has_count_with_elo"];
            }

            HasFirstGame = json["has_first_game"];
            HasLastGame = json["has_last_game"];

            AllowsFilteringTranspositions = json["allows_filtering_transpositions"];
            HasReverseMove = json["has_reverse_move"];

            AllowsFilteringByEloRange = json["allows_filtering_by_elo_range"];
            EloFilterGranularity = json["elo_filter_granularity"];

            AllowsFilteringByMonthRange = json["allows_filtering_by_month_range"];
            MonthFilterGranularity = json["month_filter_granularity"];

            MaxBytesPerPosition = json["max_bytes_per_position"];

            if (json.ContainsKey("estimated_average_bytes_per_position"))
            {
                EstimatedAverageBytesPerPosition = Optional<ulong>.Create(json["estimated_average_bytes_per_position"]);
            }
            else
            {
                EstimatedAverageBytesPerPosition = Optional<ulong>.CreateEmpty();
            }
        }
    }

    public class DatabaseInfo
    {
        public bool IsOpen { get; private set; }
        public string Path { get; private set; }

        public DatabaseStats Stats { get; private set; }

        public DatabaseInfo(string path, bool isOpen)
        {
            Path = path;
            IsOpen = isOpen;
            Stats = null;
        }

        public void SetStatsFromJson(JsonValue json)
        {
            Stats = new DatabaseStats(json);
        }
    }
}
