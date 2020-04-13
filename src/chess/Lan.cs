using ChessDotNet;

using System;

namespace chess_pos_db_gui.src.chess
{
    static class Lan
    {
        public static Move ParseLan(ChessGame game, string lan)
        {
            // a1a1

            Player player = game.WhoseTurn;
            string move = lan.TrimEnd('#', '?', '!', '+').Trim();

            Position origin = null;
            Position destination = null;
            char? promotion = null;

            if (move.Length > 2)
            {
                string possiblePromotionPiece = move.Substring(move.Length - 2).ToUpperInvariant();
                if (possiblePromotionPiece[0] == '=')
                {
                    promotion = possiblePromotionPiece[1];
                    move = move.Remove(move.Length - 2, 2);
                }
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
    }
}
