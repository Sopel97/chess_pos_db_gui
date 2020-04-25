using ChessDotNet;

using System;
using System.Collections.Generic;
using System.Linq;

namespace chess_pos_db_gui.src.chess
{
    static class Lan
    {
        public static readonly string NullMove = "0000";

        public static bool IsLegal(string fen, string lan)
        {
            if (lan == null || lan == NullMove)
            {
                return false;
            }

            ChessGame game = new ChessGame(fen);
            var from = lan.Substring(0, 2);
            var to = lan.Substring(2, 2);
            Player player = game.WhoseTurn;
            var move = lan.Length == 5 ? new ChessDotNet.Move(from, to, player, lan[4]) : new ChessDotNet.Move(from, to, player);
         
            return game.MakeMove(move, false) != MoveType.Invalid;
        }

        public static Move ParseLan(ChessGame game, string lan)
        {
            // a1a1

            Player player = game.WhoseTurn;
            string move = lan.TrimEnd('#', '?', '!', '+').Trim();

            Position origin = null;
            Position destination = null;
            char? promotion = null;

            if (move.Length > 4)
            {
                promotion = move.ToUpper()[4];
                move = move.Remove(move.Length - 1, 1);
            }

            if (move.ToUpperInvariant() == "O-O")
            {
                int r = player == Player.White ? 1 : 8;
                origin = new Position(File.E, r);
                destination = new Position(File.G, r);
            }
            else if (move.ToUpperInvariant() == "O-O-O")
            {
                int r = player == Player.White ? 1 : 8;
                origin = new Position(File.E, r);
                destination = new Position(File.C, r);
            }

            if (destination == null)
            {
                origin = new Position(move.Substring(0, 2));
                destination = new Position(move.Substring(2, 2));
            }

            var m = new Move(origin, destination, player, promotion);

            if (game.IsValidMove(m))
            {
                return m;
            }
            else
            {
                throw new ArgumentException("Invalid move.");
            }
        }

        public static Move LanToMove(string fen, string lan)
        {
            return ParseLan(new ChessGame(fen), lan);
        }

        public static MoveWithSan LanToMoveWithSan(string fen, string lan)
        {
            if (lan == null || lan == NullMove)
            {
                return new MoveWithSan(null, San.NullMove);
            }

            ChessGame game = new ChessGame(fen);
            var from = lan.Substring(0, 2);
            var to = lan.Substring(2, 2);
            Player player = game.WhoseTurn;
            var move = 
                lan.Length == 5 
                ? new ChessDotNet.Move(from, to, player, lan[4]) 
                : new ChessDotNet.Move(from, to, player);
            game.MakeMove(move, true);
            
            var detailedMove = game.Moves.Last();
            return new MoveWithSan(move, detailedMove.SAN);
        }

        public static string PVToString(string fen, IList<string> lans)
        {
            ChessGame game = new ChessGame(fen);
            foreach (var lan in lans)
            {
                var from = lan.Substring(0, 2);
                var to = lan.Substring(2, 2);
                Player player = game.WhoseTurn;
                var move = 
                    lan.Length == 5 
                    ? new ChessDotNet.Move(from, to, player, lan[4]) 
                    : new ChessDotNet.Move(from, to, player);
                game.MakeMove(move, true);
            }

            return game.Moves.Select(d => d.SAN).Aggregate((a, b) => a + " " + b);
        }
    }
}
