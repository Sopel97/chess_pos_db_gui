using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace chess_pos_db_gui
{
    class UciEngineProxy
    {

        public string Path { get; private set; }
        private Process EngineProcess { get; set; }
        private BlockingQueue<string> MessageQueue { get; set; }
        private Action<UciInfoResponse> UciInfoHandler { get; set; }

        public UciEngineProxy(string path)
        {
            Path = path;
            MessageQueue = new BlockingQueue<string>();

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

            PerformUciHandshake();
        }

        private void ErrorReceived(Object sender, DataReceivedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Error: " + e.Data);
        }

        private void ReceiveMessage(Object sender, DataReceivedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Message: " + e.Data);

            if (UciInfoHandler != null && e.Data.StartsWith("info"))
            {
                var infoResponse = ParseInfoResponse(e.Data);
                UciInfoHandler.Invoke(infoResponse);
            }
            else
            {
                MessageQueue.Enqueue(e.Data);
            }
        }

        private void SendMessage(string message)
        {
            EngineProcess.StandardInput.WriteLine(message);
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
        }

        private void GoInfinite(Action<UciInfoResponse> handler)
        {
            UciInfoHandler = handler;
            SendMessage("go infinite");
        }

        private void Stop()
        {
            SendMessage("stop");
            WaitForMessage("bestmove");
            UciInfoHandler = null;
        }

        private bool IsValidUciMove(string s)
        {
            string files = "abcdefgh";
            string ranks = "12345678";

            if (s.Length != 4) return false;
            if (s == "0000") return true;
            if (files.IndexOf(s[0]) == -1) return false;
            if (ranks.IndexOf(s[1]) == -1) return false;
            if (files.IndexOf(s[2]) == -1) return false;
            if (ranks.IndexOf(s[3]) == -1) return false;

            return true;
        }

        private Optional<IList<string>> NextMovelist(Queue<string> parts)
        {
            var list = new List<string>();

            while(parts.Count > 0)
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
            var r = new UciInfoResponse();
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
                                r.Score = NextInt(parts);
                                break;
                            }

                        case "mate":
                            {
                                r.Mate = NextInt(parts);
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

    public class UciInfoResponse : EventArgs
    {
        public Optional<int> Depth { get; set; }
        public Optional<int> SelDepth { get; set; }
        public Optional<long> Time { get; set; }
        public Optional<long> Nodes { get; set; }
        public Optional<IList<string>> PV { get; set; }
        public Optional<int> MultiPV { get; set; }
        public Optional<int> Score { get; set; }
        public Optional<int> Mate { get; set; }
        public Optional<string> CurrMove { get; set; }
        public Optional<int> CurrMoveNumber { get; set; }
        public Optional<int> HashFull { get; set; }
        public Optional<long> Nps { get; set; }
        public Optional<long> TBHits { get; set; }

        public UciInfoResponse()
        {
            Depth = Optional<int>.CreateEmpty();
            SelDepth = Optional<int>.CreateEmpty();
            Time = Optional<long>.CreateEmpty();
            Nodes = Optional<long>.CreateEmpty();
            PV = Optional<IList<string>>.CreateEmpty();
            MultiPV = Optional<int>.CreateEmpty();
            Score = Optional<int>.CreateEmpty();
            Mate = Optional<int>.CreateEmpty();
            CurrMove = Optional<string>.CreateEmpty();
            CurrMoveNumber = Optional<int>.CreateEmpty();
            HashFull = Optional<int>.CreateEmpty();
            Nps = Optional<long>.CreateEmpty();
            TBHits = Optional<long>.CreateEmpty();
        }
    }
}
