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
        public string IdName { get; private set; }
        public string IdAuthor { get; private set; }

        private UciEngineProfile Profile { get; set; }

        private Process EngineProcess { get; set; }

        private BlockingQueue<string> MessageQueue { get; set; }

        private Action<UciInfoResponse> UciInfoHandler { get; set; }

        private string Fen { get; set; }

        public IList<UciOption> ScratchOptions { get; private set; }
        private IList<UciOption> CurrentlyAppliedOptions { get; set; }

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
            Profile.ApplyOptionsToEngine(this);
        }

        public UciEngineProxy(string path)
        {
            Path = path;
            IdName = "";
            IdAuthor = "";
            Fen = FenProvider.StartPos;
            PvCount = 1;
            MessageQueue = new BlockingQueue<string>();
            ScratchOptions = new List<UciOption>();
            CurrentlyAppliedOptions = new List<UciOption>();
            IsSearching = false;

            EngineProcess = MakeEngineProcess(path);

            PerformUciHandshake();
        }

        private Process MakeEngineProcess(string path)
        {
            var engineProcess = new Process();
            engineProcess.StartInfo.FileName = path;

            engineProcess.StartInfo.RedirectStandardInput = true;
            engineProcess.StartInfo.RedirectStandardOutput = true;
            engineProcess.StartInfo.RedirectStandardError = true;

            engineProcess.StartInfo.UseShellExecute = false;
            engineProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            engineProcess.StartInfo.CreateNoWindow = true;

            engineProcess.Start();

            engineProcess.ErrorDataReceived += ErrorReceived;
            engineProcess.OutputDataReceived += ReceiveMessage;

            engineProcess.BeginOutputReadLine();
            engineProcess.BeginErrorReadLine();

            return engineProcess;
        }

        public void Quit()
        {
            SendMessage("quit");
            EngineProcess = null;
        }

        private IList<UciOption> GetChangedOptions()
        {
            var changedOptions = new List<UciOption>();
            var zippedOptions = 
                ScratchOptions.Zip(
                    CurrentlyAppliedOptions, 
                    (scratch, applied) => new { Scratch = scratch, Applied = applied }
                );

            foreach (var optionPair in zippedOptions)
            {
                if (!optionPair.Scratch.Equals(optionPair.Applied))
                {
                    changedOptions.Add(optionPair.Scratch);
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
                var infoResponse = new UciInfoResponse(Fen, e.Data);
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
                        IdName = value;
                        break;
                    }

                case "author":
                    {
                        IdAuthor = value;
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
                PauseAnalysis();
                Fen = fen;
                StartAnalysis(UciInfoHandler, Fen);
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
                ScratchOptions.Add(opt);
                CurrentlyAppliedOptions.Add(new UciOptionFactory().FromString(line)); //we want a copy
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
            foreach (var opt in CurrentlyAppliedOptions)
            {
                ScratchOptions.First((UciOption o) => o.GetName() == opt.GetName()).SetValue(opt);
            }
        }

        private void UpdateUciOptionsWhileNotSearching()
        {
            foreach (var opt in GetChangedOptions())
            {
                SendMessage(opt.GetSetOptionString());
                CurrentlyAppliedOptions.First((UciOption o) => o.GetName() == opt.GetName()).SetValue(opt);
            }

            if (Profile != null)
            {
                Profile.SetOverridedOptions(GetOverridedOptions());
            }

            PvCount = 1;
            foreach (var opt in CurrentlyAppliedOptions)
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
                PauseAnalysis();
                UpdateUciOptionsWhileNotSearching();
                StartAnalysis(UciInfoHandler, Fen);
            }
            else
            {
                EnsureReady();
                UpdateUciOptionsWhileNotSearching();
            }
        }

        public IList<KeyValuePair<string, string>> GetOverridedOptions()
        {
            var overrided = new List<KeyValuePair<string, string>>();
            foreach (var opt in CurrentlyAppliedOptions)
            {
                if (!opt.IsDefault())
                {
                    overrided.Add(opt.GetKeyValuePair());
                }
            }
            return overrided;
        }

        public void ApplyOptions(IList<KeyValuePair<string, string>> options)
        {
            foreach (var newOpt in options)
            {
                foreach (var opt in ScratchOptions)
                {
                    var name = newOpt.Key;
                    var value = newOpt.Value;
                    if (opt.GetName() == name)
                    {
                        opt.SetValue(value);
                        break;
                    }
                }
            }

            UpdateUciOptions();
        }

        public void StartAnalysis(Action<UciInfoResponse> handler, string fen)
        {
            if (IsSearching)
            {
                StopAnalysis();
            }

            UciInfoHandler = handler;
            UpdateUciOptionsWhileNotSearching();
            Fen = fen;
            SendSetPositionUnsafe(fen);
            OnAnalysisStarted.Invoke(this, new EventArgs());
            SendMessage("go infinite");

            IsSearching = true;
        }

        public void StopAnalysis()
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

        // Doesn't remove the uci info handler.
        private void PauseAnalysis()
        {
            if (IsSearching)
            {
                SendMessage("stop");
                //WaitForMessage("bestmove");
                IsSearching = false;
            }
        }
    }
}
