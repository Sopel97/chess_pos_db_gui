
using chess_pos_db_gui.src.chess;
using chess_pos_db_gui.src.chess.engine.uci;
using ChessDotNet;

using System;
using System.Collections.Generic;
using System.Linq;

namespace chess_pos_db_gui
{
    public class UciInfoResponse : EventArgs
    {
        public string Fen { get; private set; }
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

        public UciInfoResponse(string fen)
        {
            Fen = fen;
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

        private Optional<IList<string>> NextMovelist(Queue<string> parts)
        {
            var list = new List<string>();

            while (parts.Count > 0)
            {
                var part = parts.Peek();
                if (UciUtil.IsValidUciMove(part))
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

        public UciInfoResponse(string fen, string msg) :
            this(fen)
        {
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
                                Depth = NextInt(parts);
                                break;
                            }

                        case "seldepth":
                            {
                                SelDepth = NextInt(parts);
                                break;
                            }

                        case "time":
                            {
                                Time = NextLong(parts);
                                break;
                            }

                        case "nodes":
                            {
                                Nodes = NextLong(parts);
                                break;
                            }

                        case "pv":
                            {
                                PV = NextMovelist(parts);
                                break;
                            }

                        case "multipv":
                            {
                                MultiPV = NextInt(parts);
                                break;
                            }

                        case "score":
                            {
                                Score = NextScore(parts);
                                break;
                            }

                        case "currmove":
                            {
                                CurrMove = NextString(parts);
                                break;
                            }

                        case "currmovenumber":
                            {
                                CurrMoveNumber = NextInt(parts);
                                break;
                            }

                        case "hashfull":
                            {
                                HashFull = NextInt(parts);
                                break;
                            }

                        case "nps":
                            {
                                Nps = NextLong(parts);
                                break;
                            }

                        case "tbhits":
                            {
                                TBHits = NextLong(parts);
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
        }

        public string GetMoveLan()
        {
            return PV.Or(new List<string>()).FirstOrDefault() ?? Lan.NullMove;
        }

        public bool IsLegal()
        {
            return Lan.IsLegal(Fen, GetMoveLan());
        }
    }
}
