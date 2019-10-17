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
                Move = GCD.Moves.Last();
                GCD.Moves = new DetailedMove[1] { Move };
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
        public int Plies { get; private set; }

        public ChessBoardHistory()
        {
            Entries = new List<ChessBoardHistoryEntry>
            {
                new ChessBoardHistoryEntry(
                    new ChessGame()
                )
            };
            Plies = 0;
        }

        public void SetCurrent(int i)
        {
            Plies = i;
        }

        private void TruncateToCurrent()
        {
            if (Plies != Entries.Count - 1)
                Entries.RemoveRange(Plies + 1, Entries.Count - Plies - 1);
        }

        public bool DoMove(Move move)
        {
            TruncateToCurrent();

            ChessGame pos = new ChessGame(Entries.Last().GCD);
            if (!pos.IsValidMove(move)) return false;
            pos.MakeMove(move, true);
            Entries.Add(new ChessBoardHistoryEntry(pos));
            ++Plies;
            return true;
        }

        public bool DoMove(string san)
        {
            TruncateToCurrent();

            ChessGame pos = new ChessGame(Entries.Last().GCD);
            Move move = San.ParseSan(pos, san);
            if (move == null) return false;
            pos.MakeMove(move, false);
            Entries.Add(new ChessBoardHistoryEntry(pos));
            ++Plies;
            return true;
        }

        public bool UndoMove()
        {
            if (Entries.Count() > 1)
            {
                Entries.RemoveAt(Entries.Count() - 1);
                --Plies;
                return true;
            }

            return false;
        }

        public ChessBoardHistoryEntry Current()
        {
            return Entries[Plies];
        }
    }
}
