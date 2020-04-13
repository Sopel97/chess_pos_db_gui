
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

        public string GetMoveLan()
        {
            return PV.Or(new List<string>()).FirstOrDefault();
        }

        public bool IsLegal()
        {
            var lan = PV.Or(new List<string>()).FirstOrDefault();
            if (lan == null || lan == "0000")
            {
                return false;
            }

            ChessGame game = new ChessGame(Fen);
            var from = lan.Substring(0, 2);
            var to = lan.Substring(2, 2);
            Player player = game.WhoseTurn;
            var move = lan.Length == 5 ? new ChessDotNet.Move(from, to, player, lan[4]) : new ChessDotNet.Move(from, to, player);
            if (game.MakeMove(move, false) == MoveType.Invalid)
            {
                return false;
            }

            return true;
        }
    }
}
