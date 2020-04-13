using ChessDotNet;

using System.Collections.Generic;
using System.Linq;

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
            if (Move == null)
            {
                return "--";
            }

            return Move.SAN;
        }
    }

    class ChessBoardHistory
    {
        private List<ChessBoardHistoryEntry> Entries { get; set; }
        public int Plies { get; private set; }
        public int Count { get { return Entries.Count; } }

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

        public void Reset(string fen)
        {
            Entries.Clear();
            Plies = 0;
            var game = new ChessGame(fen);
            Entries = new List<ChessBoardHistoryEntry>
            {
                new ChessBoardHistoryEntry(
                    game
                )
            };
            Entries[0].Move = null;
        }

        public void SetCurrent(int i)
        {
            Plies = i;
        }

        private void TruncateToCurrent()
        {
            if (Plies != Entries.Count - 1)
            {
                Entries.RemoveRange(Plies + 1, Entries.Count - Plies - 1);
            }
        }

        public bool DoMove(Move move)
        {
            TruncateToCurrent();

            ChessGame pos = new ChessGame(Current().GCD);
            if (!pos.IsValidMove(move))
            {
                return false;
            }

            pos.MakeMove(move, true);
            Entries.Add(new ChessBoardHistoryEntry(pos));
            ++Plies;
            return true;
        }

        internal void DuplicateLast()
        {
            Entries.Add(Entries.Last());
            ++Plies;
        }

        public bool DoMove(string san)
        {
            TruncateToCurrent();

            ChessGame pos = new ChessGame(Current().GCD);
            Move move = San.ParseSan(pos, san);
            if (move == null)
            {
                return false;
            }

            pos.MakeMove(move, false);
            Entries.Add(new ChessBoardHistoryEntry(pos));
            ++Plies;
            return true;
        }

        public bool UndoMove()
        {
            TruncateToCurrent();

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

        public ChessBoardHistoryEntry Prev()
        {
            return Entries[Plies - 1];
        }

        public ChessBoardHistoryEntry Next()
        {
            if (Plies == Entries.Count() - 1)
            {
                return null;
            }

            return Entries[Plies + 1];
        }

        internal bool IsMoveValid(Move move)
        {
            ChessGame pos = new ChessGame(Current().GCD);
            return pos.IsValidMove(move);
        }
    }
}
