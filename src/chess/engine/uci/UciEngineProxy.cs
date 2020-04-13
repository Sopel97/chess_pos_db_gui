using chess_pos_db_gui.src.chess;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Json;
using System.Linq;

namespace chess_pos_db_gui
{
    public class UciEngineProxy
    {
        public string Path { get; private set; }
        public string Name { get; private set; }
        public string Author { get; private set; }
        private UciEngineProfile Profile { get; set; }
        private Process EngineProcess { get; set; }
        private BlockingQueue<string> MessageQueue { get; set; }
        private Action<UciInfoResponse> UciInfoHandler { get; set; }
        private string Fen { get; set; }
        public IList<UciOption> CurrentOptions { get; private set; }
        private IList<UciOption> AppliedOptions { get; set; }
        public bool IsSearching { get; private set; }
        public int PvCount { get; private set; }

        private EventHandler OnAnalysisStarted { get; set; }
        public event EventHandler AnalysisStarted
        {
            add
            {
                OnAnalysisStarted += value;
            }

            remove
            {
                OnAnalysisStarted -= value;
            }
        }

        public UciEngineProxy(UciEngineProfile profile) :
            this(profile.Path)
        {
            Profile = profile;
            Profile.OverrideOptions(this);
        }

        public UciEngineProxy(string path)
        {
            Path = path;
            Name = "";
            Author = "";
            Fen = FenProvider.StartPos;
            PvCount = 1;
            MessageQueue = new BlockingQueue<string>();
            CurrentOptions = new List<UciOption>();
            AppliedOptions = new List<UciOption>();

            EngineProcess = new Process();
            EngineProcess.StartInfo.FileName = path;

            EngineProcess.StartInfo.RedirectStandardInput = true;
            EngineProcess.StartInfo.RedirectStandardOutput = true;
            EngineProcess.StartInfo.RedirectStandardError = true;

            EngineProcess.StartInfo.UseShellExecute = false;
            EngineProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            EngineProcess.StartInfo.CreateNoWindow = true;

            EngineProcess.ErrorDataReceived += ErrorReceived;
            EngineProcess.OutputDataReceived += ReceiveMessage;

            EngineProcess.Start();

            EngineProcess.BeginOutputReadLine();
            EngineProcess.BeginErrorReadLine();

            IsSearching = false;

            PerformUciHandshake();
        }

        public void Quit()
        {
            SendMessage("quit");
            EngineProcess = null;
        }

        private IList<UciOption> GetChangedOptions()
        {
            var changedOptions = new List<UciOption>();
            var zippedOptions = CurrentOptions.Zip(AppliedOptions, (current, applied) => new { Current = current, Applied = applied });
            foreach (var ca in zippedOptions)
            {
                if (!ca.Current.Equals(ca.Applied))
                {
                    changedOptions.Add(ca.Current);
                }
            }
            return changedOptions;
        }

        private void ErrorReceived(Object sender, DataReceivedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Error: " + e.Data);
        }

        private void ReceiveMessage(Object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
            {
                return;
            }

            System.Diagnostics.Debug.WriteLine("Received: " + e.Data);

            if (UciInfoHandler != null && e.Data.StartsWith("info"))
            {
                var infoResponse = ParseInfoResponse(e.Data);
                if (infoResponse.IsLegal())
                {
                    try
                    {
                        UciInfoHandler.Invoke(infoResponse);
                    }
                    catch (Exception)
                    {
                        MessageQueue.Clear();
                    }
                }
            }
            else if (e.Data.StartsWith("option"))
            {
                AddOption(e.Data);
            }
            else if (e.Data.StartsWith("id"))
            {
                SetId(e.Data);
            }
            else
            {
                MessageQueue.Enqueue(e.Data);
            }
        }

        private void SetId(string id)
        {
            var parts = id.Split(new char[] { ' ' });
            if (parts.Length < 3)
            {
                return;
            }

            var value = parts.Skip(2).Aggregate((a, b) => a + " " + b);

            switch (parts[1])
            {
                case "name":
                    {
                        Name = value;
                        break;
                    }

                case "author":
                    {
                        Author = value;
                        break;
                    }
            }
        }

        private void SendSetPositionUnsafe(string fen)
        {
            if (fen == null)
            {
                SendMessage("position startpos");
            }
            else
            {
                SendMessage("position fen " + fen);
            }
        }

        public void SetPosition(string fen)
        {
            if (IsSearching)
            {
                Pause();
                Fen = fen;
                GoInfinite(UciInfoHandler, Fen);
            }
            else
            {
                EnsureReady();
                Fen = fen;
                SendSetPositionUnsafe(fen);
            }
        }

        private void AddOption(string line)
        {
            var opt = new UciOptionFactory().FromString(line);
            if (opt != null)
            {
                CurrentOptions.Add(opt);
                AppliedOptions.Add(new UciOptionFactory().FromString(line)); //we want a copy
            }
        }

        private void SendMessage(string message)
        {
            System.Diagnostics.Debug.WriteLine("Sent: " + message);

            EngineProcess.StandardInput.Write(message + "\n");
            EngineProcess.StandardInput.Flush();
        }

        private string WaitForMessage(string startsWith)
        {
            for (; ; )
            {
                string message = MessageQueue.Dequeue();
                if (message.StartsWith(startsWith))
                {
                    return message;
                }
            }
        }

        private string WaitForMessageTimed(string startsWith, int timeout)
        {
            for (; ; )
            {
                string message = MessageQueue.DequeueTimed(timeout);
                if (message == null)
                {
                    return null;
                }
                else if (message.StartsWith(startsWith))
                {
                    return message;
                }
            }
        }

        private void PerformUciHandshake()
        {
            SendMessage("uci");
            if (WaitForMessageTimed("uciok", 1000) == null)
            {
                throw new TimeoutException("Engine didn't respond with \"uciok\" within the set timeout");
            }

            EnsureReady();
            SendMessage("setoption name UCI_AnalyseMode value true");
        }

        private void EnsureReady()
        {
            SendMessage("isready");
            WaitForMessage("readyok");
        }

        public void DiscardUciOptionChanges()
        {
            foreach (var opt in AppliedOptions)
            {
                CurrentOptions.First((UciOption o) => o.GetName() == opt.GetName()).CopyValueFrom(opt);
            }
        }

        private void UpdateUciOptionsWhileNotSearching()
        {
            foreach (var opt in GetChangedOptions())
            {
                SendMessage(opt.GetSetOptionString());
                AppliedOptions.First((UciOption o) => o.GetName() == opt.GetName()).CopyValueFrom(opt);
            }

            if (Profile != null)
            {
                Profile.SetOverridedOptions(GetOverridedOptions());
            }

            PvCount = 1;
            foreach (var opt in AppliedOptions)
            {
                if (opt.GetName() == "MultiPV")
                {
                    PvCount = (int)(long)opt.GetValue();
                    break;
                }
            }
        }

        public void UpdateUciOptions()
        {
            var changedOptions = GetChangedOptions();
            if (changedOptions.Count == 0)
            {
                return;
            }

            if (IsSearching)
            {
                Pause();
                UpdateUciOptionsWhileNotSearching();
                GoInfinite(UciInfoHandler, Fen);
            }
            else
            {
                EnsureReady();
                UpdateUciOptionsWhileNotSearching();
            }
        }

        public IList<JsonValue> GetOverridedOptions()
        {
            var overrided = new List<JsonValue>();
            foreach (var opt in AppliedOptions)
            {
                if (!opt.IsDefault())
                {
                    overrided.Add(opt.NameValueToJson());
                }
            }
            return overrided;
        }

        public void OverrideOptions(IList<JsonValue> optionsOverrides)
        {
            foreach (var json in optionsOverrides)
            {
                string name = json["name"];
                string value = json["value"];
                foreach (var opt in CurrentOptions)
                {
                    if (opt.GetName() == name)
                    {
                        opt.SetValue(value);
                        break;
                    }
                }
            }

            UpdateUciOptions();
        }

        public void GoInfinite(Action<UciInfoResponse> handler, string fen)
        {
            if (IsSearching)
            {
                Stop();
            }

            UciInfoHandler = handler;
            UpdateUciOptionsWhileNotSearching();
            Fen = fen;
            SendSetPositionUnsafe(fen);
            OnAnalysisStarted.Invoke(this, new EventArgs());
            SendMessage("go infinite");

            IsSearching = true;
        }

        public void Stop()
        {
            if (IsSearching)
            {
                SendMessage("stop");
                // TODO: For some reason waiting for bestmove sometimes prevents it from being received.
                // For example when clicking the stop button after starting analysis.
                //WaitForMessage("bestmove");
                UciInfoHandler = null;
                IsSearching = false;
            }
        }

        private void Pause()
        {
            if (IsSearching)
            {
                SendMessage("stop");
                //WaitForMessage("bestmove");
                IsSearching = false;
            }
        }

        private bool IsValidUciMove(string s)
        {
            string files = "abcdefgh";
            string ranks = "12345678";
            string promotions = "qrbk";

            if (s.Length > 5)
            {
                return false;
            }

            if (s == "0000")
            {
                return true;
            }

            if (files.IndexOf(s[0]) == -1)
            {
                return false;
            }

            if (ranks.IndexOf(s[1]) == -1)
            {
                return false;
            }

            if (files.IndexOf(s[2]) == -1)
            {
                return false;
            }

            if (ranks.IndexOf(s[3]) == -1)
            {
                return false;
            }

            if (s.Length == 5)
            {
                return s[3] == '8' && promotions.IndexOf(s[4]) != -1;
            }

            return true;
        }

        private Optional<IList<string>> NextMovelist(Queue<string> parts)
        {
            var list = new List<string>();

            while (parts.Count > 0)
            {
                var part = parts.Peek();
                if (IsValidUciMove(part))
                {
                    parts.Dequeue();
                    list.Add(part);
                }
            }

            return Optional<IList<string>>.Create(list);
        }

        private Optional<UciScore> NextScore(Queue<string> parts)
        {
            UciScoreBoundType boundType = UciScoreBoundType.Exact;
            UciScoreType type = UciScoreType.Cp;
            string typestr = parts.Dequeue();
            if (typestr == "mate")
            {
                type = UciScoreType.Mate;
            }

            string valuestr = parts.Dequeue();

            string boundstr = parts.Peek();
            if (boundstr == "lowerbound")
            {
                boundType = UciScoreBoundType.LowerBound;
                parts.Dequeue();
            }
            else if (boundstr == "upperbound")
            {
                boundType = UciScoreBoundType.UpperBound;
                parts.Dequeue();
            }

            return Optional<UciScore>.Create(new UciScore(
                int.Parse(valuestr),
                type,
                boundType
                ));
        }

        private Optional<int> NextInt(Queue<string> parts)
        {
            return Optional<int>.Create(int.Parse(parts.Dequeue()));
        }

        private Optional<long> NextLong(Queue<string> parts)
        {
            return Optional<long>.Create(long.Parse(parts.Dequeue()));
        }

        private Optional<string> NextString(Queue<string> parts)
        {
            return Optional<string>.Create(parts.Dequeue());
        }

        private UciInfoResponse ParseInfoResponse(string msg)
        {
            var r = new UciInfoResponse(Fen);
            try
            {
                Queue<string> parts = new Queue<string>(msg.Split(new char[] { ' ' }));
                while (parts.Count > 0)
                {
                    string cmd = parts.Dequeue();
                    switch (cmd)
                    {
                        case "depth":
                            {
                                r.Depth = NextInt(parts);
                                break;
                            }

                        case "seldepth":
                            {
                                r.SelDepth = NextInt(parts);
                                break;
                            }

                        case "time":
                            {
                                r.Time = NextLong(parts);
                                break;
                            }

                        case "nodes":
                            {
                                r.Nodes = NextLong(parts);
                                break;
                            }

                        case "pv":
                            {
                                r.PV = NextMovelist(parts);
                                break;
                            }

                        case "multipv":
                            {
                                r.MultiPV = NextInt(parts);
                                break;
                            }

                        case "score":
                            {
                                r.Score = NextScore(parts);
                                break;
                            }

                        case "currmove":
                            {
                                r.CurrMove = NextString(parts);
                                break;
                            }

                        case "currmovenumber":
                            {
                                r.CurrMoveNumber = NextInt(parts);
                                break;
                            }

                        case "hashfull":
                            {
                                r.HashFull = NextInt(parts);
                                break;
                            }

                        case "nps":
                            {
                                r.Nps = NextLong(parts);
                                break;
                            }

                        case "tbhits":
                            {
                                r.TBHits = NextLong(parts);
                                break;
                            }

                        case "cpuload":
                            {
                                NextInt(parts);
                                break;
                            }

                        case "string":
                            {
                                NextString(parts);
                                break;
                            }

                        case "refutation":
                            {
                                NextMovelist(parts);
                                break;
                            }

                        case "currline":
                            {
                                NextInt(parts);
                                break;
                            }

                        default:
                            {
                                break;
                            }
                    }
                }
            }
            catch (Exception)
            {
            }

            return r;
        }
    }
}
