using ChessDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui
{
    internal class ChessBoardHistoryEntry
    {
        public GameCreationData GCD { get; set; }
        public DetailedMove Move { get; set; }

        public ChessBoardHistoryEntry(ChessGame game)
        {
            GCD = game.GetGameCreationData();
            
            if (GCD.Moves.Length > 0)
            {
                Move = GCD.Moves.First();
                GCD.Moves = new DetailedMove[0];
            }
        }

        public Piece[][] GetBoard()
        {
            return GCD.Board;
        }

        public string GetFen()
        {
            return new ChessGame(GCD).GetFen();
        }

        public string GetSan()
        {
            if (Move == null) return "--";

            return Move.SAN;
        }
    }

    class ChessBoardHistory
    {
        private List<ChessBoardHistoryEntry> Entries { get; set; }

        public ChessBoardHistory()
        {
            Entries = new List<ChessBoardHistoryEntry>
            {
                new ChessBoardHistoryEntry(
                    new ChessGame()
                )
            };
        }

        public void DoMove(Move move)
        {
            ChessGame pos = new ChessGame(Entries.Last().GCD);
            pos.MakeMove(move, false);
            Entries.Add(new ChessBoardHistoryEntry(pos));
        }

        public void DoMove(string san)
        {
            ChessGame pos = new ChessGame(Entries.Last().GCD);
            Move move = San.ParseSan(pos, san);
            pos.MakeMove(move, false);
            Entries.Add(new ChessBoardHistoryEntry(pos));
        }

        public void UndoMove()
        {
            if (Entries.Count() > 1)
            {
                Entries.RemoveAt(Entries.Count() - 1);
            }
        }

        public ChessBoardHistoryEntry Last()
        {
            return Entries[Entries.Count() - 1];
        }
    }
}
