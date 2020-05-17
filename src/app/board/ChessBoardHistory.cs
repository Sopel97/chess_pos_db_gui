using ChessDotNet;
using System;
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
                return San.NullMove;
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
            var game = new ChessGame();
            Entries = new List<ChessBoardHistoryEntry>
            {
                new ChessBoardHistoryEntry(game)
            };
            Plies = 0;
        }

        public void SetInitialPosition(string fen)
        {
            Entries.Clear();
            Plies = 0;
            var game = new ChessGame(fen);
            Entries = new List<ChessBoardHistoryEntry>
            {
                new ChessBoardHistoryEntry(game)
            };
            Entries[0].Move = null;
        }

        public void SetCurrentPly(int i)
        {
            Plies = i;
        }

        private void TruncateToCurrentPly()
        {
            if (Plies != Entries.Count - 1)
            {
                Entries.RemoveRange(Plies + 1, Entries.Count - Plies - 1);
            }
        }

        public bool DoMove(Move move)
        {
            TruncateToCurrentPly();

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
            ChessGame pos = new ChessGame(Current().GCD);
            Move move = San.ParseSan(pos, san);
            return DoMove(move);
        }

        public bool UndoMove()
        {
            TruncateToCurrentPly();

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
            if (Plies == 0)
            {
                return null;
            }

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

        public bool IsMoveValid(Move move)
        {
            ChessGame pos = new ChessGame(Current().GCD);
            return pos.IsValidMove(move);
        }

        public bool NeedsToBePromotion(Move move)
        {
            Move moveAsPromotion = new Move(move.OriginalPosition, move.NewPosition, move.Player, 'Q');
            return
                IsMoveValid(moveAsPromotion)
                && !IsMoveValid(move);
        }
    }
}
