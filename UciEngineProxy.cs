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

        private UciInfoResponse ParseInfoResponse(string msg)
        {
            var r = new UciInfoResponse();

            return r;
        }
    }

    public class UciInfoResponse : EventArgs
    {

    }
}
