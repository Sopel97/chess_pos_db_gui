using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    class UciEngineProxy
    {
        public string Path { get; private set; }
        public string Name { get; private set; }
        public string Author { get; private set; }
        private Process EngineProcess { get; set; }
        private BlockingQueue<string> MessageQueue { get; set; }
        private Action<UciInfoResponse> UciInfoHandler { get; set; }
        public IList<UciOption> CurrentOptions { get; private set; }
        private IList<UciOption> AppliedOptions { get; set; }
        public bool IsSearching { get; private set; }

        public UciEngineProxy(string path)
        {
            Path = path;
            Name = "";
            Author = "";
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
            if (e.Data == null) return;

            System.Diagnostics.Debug.WriteLine("Message: " + e.Data);

            if (UciInfoHandler != null && e.Data.StartsWith("info"))
            {
                var infoResponse = ParseInfoResponse(e.Data);
                UciInfoHandler.Invoke(infoResponse);
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
            if (parts.Length < 3) return;
            var value = parts.Skip(2).Aggregate((a, b) => a + " " + b);

            switch(parts[1])
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

        private void EnsureReady()
        {
            SendMessage("isready");
            WaitForMessage("readyok");
        }

        private void UpdateUciOptionsWhileNotSearching()
        {
            foreach(var opt in GetChangedOptions())
            {
                SendMessage(opt.GetSetOptionString());
                AppliedOptions.First((UciOption o) => o.GetName() == opt.GetName()).CopyValueFrom(opt);
            }
        }

        public void UpdateUciOptions()
        {
            var changedOptions = GetChangedOptions();
            if (changedOptions.Count == 0) return;

            if (IsSearching)
            {
                SendMessage("stop");
                WaitForMessage("bestmove");
                UpdateUciOptionsWhileNotSearching();
                SendMessage("go infinite");
            }
            else
            {
                EnsureReady();
                UpdateUciOptionsWhileNotSearching();
            }
        }

        public void GoInfinite(Action<UciInfoResponse> handler, string fen = null)
        {
            if (IsSearching)
            {
                Stop();
            }

            UciInfoHandler = handler;
            EnsureReady();
            SendMessage("setoption name UCI_AnalyseMode value true");
            UpdateUciOptions();
            if (fen == null)
            {
                SendMessage("position startpos");
            }
            else
            {
                SendMessage("position fen " + fen);
            }
            SendMessage("go infinite");

            IsSearching = true;
        }

        public void Stop()
        {
            if (IsSearching)
            {

                SendMessage("stop");
                WaitForMessage("bestmove");
                UciInfoHandler = null;
                IsSearching = false;
            }
        }

        private bool IsValidUciMove(string s)
        {
            string files = "abcdefgh";
            string ranks = "12345678";
            string promotions = "qrbk";

            if (s.Length > 5) return false;
            if (s == "0000") return true;
            if (files.IndexOf(s[0]) == -1) return false;
            if (ranks.IndexOf(s[1]) == -1) return false;
            if (files.IndexOf(s[2]) == -1) return false;
            if (ranks.IndexOf(s[3]) == -1) return false;

            if (s.Length == 5)
            {
                return s[3] == '8' && promotions.IndexOf(s[4]) != -1;
            }

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

    public enum UciScoreType
    {
        Cp,
        Mate
    }

    public enum UciScoreBoundType
    {
        Exact,
        LowerBound,
        UpperBound
    }

    public class UciScore
    {
        private int Value { get; set; }
        private UciScoreType Type { get; set; }
        private UciScoreBoundType BoundType { get; set; }

        public UciScore(int value, UciScoreType type, UciScoreBoundType boundType)
        {
            Value = value;
            Type = type;
            BoundType = boundType;
        }

        public override string ToString()
        {
            if (Type == UciScoreType.Cp)
            {
                return (Value / 100.0).ToString("0.00");
            }
            else
            {
                return string.Format("{0}M{1}", Value < 0 ? "-" : "", Math.Abs(Value));
            }
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
        public Optional<UciScore> Score { get; set; }
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
            Score = Optional<UciScore>.CreateEmpty();
            CurrMove = Optional<string>.CreateEmpty();
            CurrMoveNumber = Optional<int>.CreateEmpty();
            HashFull = Optional<int>.CreateEmpty();
            Nps = Optional<long>.CreateEmpty();
            TBHits = Optional<long>.CreateEmpty();
        }
    }

    public class UciOptionFactory
    {
        public UciOptionFactory()
        {

        }

        private string NextString(Queue<string> parts)
        {
            return parts.Dequeue();
        }

        private string NextName(Queue<string> parts)
        {
            var breakers = new string[] { "type", "default", "min", "max", "var" };

            string name = parts.Dequeue();

            while (!breakers.Contains(parts.Peek()))
            {
                name += " " + parts.Dequeue();
            }

            return name;
        }

        public UciOption FromString(string str)
        {
            string name = null;
            string type = null;
            string defaultValue = null;
            IList<string> vars = new List<string>();
            string min = null;
            string max = null;

            try
            {
                Queue<string> parts = new Queue<string>(str.Split(new char[] { ' ' }));
                while (parts.Count > 0)
                {
                    string cmd = parts.Dequeue();
                    switch (cmd)
                    {
                        case "name":
                            {
                                name = NextName(parts);
                                break;
                            }

                        case "type":
                            {
                                type = NextString(parts);
                                break;
                            }

                        case "min":
                            {
                                min = NextString(parts);
                                break;
                            }

                        case "max":
                            {
                                max = NextString(parts);
                                break;
                            }

                        case "var":
                            {
                                vars.Add(NextString(parts));
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
                return null;
            }

            if (type == null)
            {
                return null;
            }

            switch (type)
            {
                case "check": return new CheckUciOption(name, defaultValue, min, max, vars);
                case "spin": return new SpinUciOption(name, defaultValue, min, max, vars);
                case "combo": return new ComboUciOption(name, defaultValue, min, max, vars);
                case "string": return new StringUciOption(name, defaultValue, min, max, vars);
                default: return null;
            }
        }
    }

    public enum UciOptionType
    {
        Check,
        Spin,
        Combo,
        String
    }

    public abstract class UciOption
    {
        abstract public UciOptionType GetOptionType();
        // TODO: pass some parent as parameter
        abstract public System.Windows.Forms.Control CreateControl();
        abstract public string GetName();
        public string GetSetOptionString()
        {
            return string.Format("setoption name {0} value {1}", GetName(), this);
        }

        public abstract void CopyValueFrom(UciOption other);
    }

    public class CheckUciOption : UciOption
    {
        public string Name { get; private set; }
        public System.Windows.Forms.TableLayoutPanel Panel { get; private set; }
        public System.Windows.Forms.CheckBox Control { get; private set; }
        public bool DefaultValue { get; private set; }
        public bool Value { get; private set; }

        public CheckUciOption(string name, string defaultValue, string min, string max, IList<string> var)
        {
            Name = name;
            Control = null;
            DefaultValue = Value = (defaultValue == "true");
        }

        public override UciOptionType GetOptionType()
        {
            return UciOptionType.Check;
        }

        public override void CopyValueFrom(UciOption other)
        {
            if (this.GetType() != other.GetType()) throw new ArgumentException("Option type different");
            Value = ((CheckUciOption)other).Value;
            if (Control != null)
            {
                Control.Checked = Value;
            }
        }

        public override System.Windows.Forms.Control CreateControl()
        {
            Panel = new System.Windows.Forms.TableLayoutPanel();
            Panel.ColumnCount = 2;
            Panel.RowCount = 1;
            Panel.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            Panel.AutoSize = true;

            Control = new System.Windows.Forms.CheckBox();
            Control.Checked = Value;
            Control.Enabled = true;
            Control.Visible = true;
            Control.CheckedChanged += OnCheckedChanged;

            var label = new System.Windows.Forms.Label();
            label.Text = Name;
            label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            label.Width = 128;

            Panel.Controls.Add(label);
            Panel.Controls.Add(Control);

            return Panel;
        }

        private void OnCheckedChanged(object sender, EventArgs e)
        {
            Value = Control.Checked;
        }

        public override string ToString()
        {
            return Value ? "true" : "false";
        }

        public override string GetName()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is CheckUciOption option &&
                   Name == option.Name &&
                   Value == option.Value;
        }
    }

    public class SpinUciOption : UciOption
    {
        public string Name { get; private set; }
        public System.Windows.Forms.TableLayoutPanel Panel { get; private set; }
        public System.Windows.Forms.NumericUpDown Control { get; private set; }
        public long DefaultValue { get; private set; }
        public long Value { get; private set; }
        public Optional<long> Min { get; private set; }
        public Optional<long> Max { get; private set; }

        public SpinUciOption(string name, string defaultValue, string min, string max, IList<string> var)
        {
            Name = name;
            Control = null;
            Min = min == null ? Optional<long>.CreateEmpty() : Optional<long>.Create(long.Parse(min));
            Max = max == null ? Optional<long>.CreateEmpty() : Optional<long>.Create(long.Parse(max));
            if (defaultValue == null)
            {
                DefaultValue = Value = Min.Or(0);
            }
            else
            {
                DefaultValue = Value = long.Parse(defaultValue);
            }
        }

        public override UciOptionType GetOptionType()
        {
            return UciOptionType.Spin;
        }

        public override void CopyValueFrom(UciOption other)
        {
            if (this.GetType() != other.GetType()) throw new ArgumentException("Option type different");
            Value = ((SpinUciOption)other).Value;
            if (Control != null)
            {
                Control.Value = Value;
            }
        }

        public override System.Windows.Forms.Control CreateControl()
        {
            Panel = new System.Windows.Forms.TableLayoutPanel();
            Panel.ColumnCount = 2;
            Panel.RowCount = 1;
            Panel.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            Panel.AutoSize = true;

            Control = new System.Windows.Forms.NumericUpDown();
            Control.Minimum = Min.Or(DefaultValue);
            Control.Maximum = Max.Or(DefaultValue);
            Control.Value = Value;
            Control.DecimalPlaces = 0;
            Control.Increment = 1;
            Control.AutoSize = true;
            Control.ValueChanged += OnValueChanged;

            var label = new System.Windows.Forms.Label();
            label.Text = Name;
            label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            label.Width = 128;

            Panel.Controls.Add(label);
            Panel.Controls.Add(Control);

            return Panel;
        }

        private void OnValueChanged(object sender, EventArgs e)
        {
            Value = (long)Control.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override string GetName()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is SpinUciOption option &&
                   Name == option.Name &&
                   Value == option.Value;
        }
    }

    public class ComboUciOption : UciOption
    {
        public string Name { get; private set; }
        public System.Windows.Forms.TableLayoutPanel Panel { get; private set; }
        public System.Windows.Forms.ComboBox Control { get; private set; }
        public string DefaultValue { get; private set; }
        public string Value { get; private set; }
        public IList<string> Vars { get; private set; }

        public ComboUciOption(string name, string defaultValue, string min, string max, IList<string> var)
        {
            Name = name;
            Control = null;
            DefaultValue = Value = defaultValue;
            Vars = var;
        }

        public override UciOptionType GetOptionType()
        {
            return UciOptionType.Combo;
        }

        public override void CopyValueFrom(UciOption other)
        {
            if (this.GetType() != other.GetType()) throw new ArgumentException("Option type different");
            Value = ((ComboUciOption)other).Value;
            if (Control != null)
            {
                Control.SelectedItem = Value;
            }
        }

        public override System.Windows.Forms.Control CreateControl()
        {
            Panel = new System.Windows.Forms.TableLayoutPanel();
            Panel.ColumnCount = 2;
            Panel.RowCount = 1;
            Panel.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            Panel.AutoSize = true;

            Control = new System.Windows.Forms.ComboBox();
            foreach (var var in Vars)
            {
                Control.Items.Add(var);
            }
            Control.SelectedItem = Value;
            Control.SelectedValueChanged += OnSelectedValueChanged;

            var label = new System.Windows.Forms.Label();
            label.Text = Name;
            label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            label.Width = 128;

            Panel.Controls.Add(label);
            Panel.Controls.Add(Control);

            return Panel;
        }

        private void OnSelectedValueChanged(object sender, EventArgs e)
        {
            Value = Control.SelectedText;
        }

        public override string ToString()
        {
            return Value;
        }

        public override string GetName()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is ComboUciOption option &&
                   Name == option.Name &&
                   Value == option.Value;
        }
    }

    public class StringUciOption : UciOption
    {
        public string Name { get; private set; }
        public System.Windows.Forms.TableLayoutPanel Panel { get; private set; }
        public System.Windows.Forms.TextBox Control { get; private set; }
        public string DefaultValue { get; private set; }
        public string Value { get; private set; }

        public StringUciOption(string name, string defaultValue, string min, string max, IList<string> var)
        {
            if (defaultValue == null) defaultValue = "";

            Name = name;
            Control = null;
            DefaultValue = Value = defaultValue;
        }

        public override UciOptionType GetOptionType()
        {
            return UciOptionType.String;
        }

        public override void CopyValueFrom(UciOption other)
        {
            if (this.GetType() != other.GetType()) throw new ArgumentException("Option type different");
            Value = ((StringUciOption)other).Value;
            if (Control != null)
            {
                Control.Text = Value;
            }
        }

        public override System.Windows.Forms.Control CreateControl()
        {
            Panel = new System.Windows.Forms.TableLayoutPanel();
            Panel.ColumnCount = 2;
            Panel.RowCount = 1;
            Panel.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            Panel.AutoSize = true;

            Control = new System.Windows.Forms.TextBox();
            Control.Text = DefaultValue;
            Control.Width = 100;
            Control.TextChanged += OnTextChanged;

            var label = new System.Windows.Forms.Label();
            label.Text = Name;
            label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            label.Width = 128;

            Panel.Controls.Add(label);
            Panel.Controls.Add(Control);

            return Panel;
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            Value = Control.Text;
        }

        public override string ToString()
        {
            return Value;
        }

        public override string GetName()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is StringUciOption option &&
                   Name == option.Name &&
                   Value == option.Value;
        }
    }
}
