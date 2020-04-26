using ChessDotNet;
using ChessDotNet.Pieces;
using System;
using System.Linq;

namespace chess_pos_db_gui.src.chess
{
    static class Eran
    {
        private static Piece TryMakePieceFromChar(char c, Player player)
        {
            switch (c)
            {
                case 'N':
                    return new Knight(player);
                case 'B':
                    return new Bishop(player);
                case 'R':
                    return new Rook(player);
                case 'Q':
                    return new Queen(player);
                case 'K':
                    return new King(player);
                default:
                    return null;
            }
        }
        public static ReverseMove ParseEran(string fen, string eran)
        {
            return ParseEran(new ChessGame(fen), eran);
        }

        public static ReverseMove ParseEran(ChessGame game, string eran)
        {
            ReverseMove rmove = new ReverseMove();

            var parts = eran.Split(' ');
            var ran = parts[0];
            var oldCastlingRights = parts[1];
            var oldEpSquare = parts[2];

            Player prevPlayer =
                game.WhoseTurn == Player.Black
                ? Player.White
                : Player.Black;

            if (ran == "O-O" || ran == "O-O-O")
            {
                if (ran == "O-O")
                {
                    int r = prevPlayer == Player.White ? 1 : 8;
                    var origin = new Position(File.E, r);
                    var destination = new Position(File.G, r);
                    rmove.Move = new Move(origin, destination, prevPlayer, null);
                }
                else
                {
                    int r = prevPlayer == Player.White ? 1 : 8;
                    var origin = new Position(File.E, r);
                    var destination = new Position(File.C, r);
                    rmove.Move = new Move(origin, destination, prevPlayer, null);
                }
            }
            else
            {
                rmove.Move = new Move((Position)null, null, prevPlayer);

                Piece movedPiece = TryMakePieceFromChar(ran[0], prevPlayer);
                if (movedPiece != null)
                {
                    ran = ran.Substring(1);
                }

                {
                    int fromSquareFile = ran[0] - 'a';
                    int fromSquareRank = ran[1] - '1';
                    rmove.Move.OriginalPosition = new Position((File)fromSquareFile, fromSquareRank + 1);
                    ran = ran.Substring(2);
                }

                if (ran[0] == '-')
                {
                    ran = ran.Substring(1);
                }
                else if (ran[0] == 'x')
                {
                    rmove.CapturedPiece = TryMakePieceFromChar(ran[1], game.WhoseTurn);
                    if (rmove.CapturedPiece == null)
                    {
                        rmove.CapturedPiece = new Pawn(game.WhoseTurn);
                        ran = ran.Substring(1);
                    }
                    else
                    {
                        ran = ran.Substring(2);
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid ERAN");
                }

                {
                    int toSquareFile = ran[0] - 'a';
                    int toSquareRank = ran[1] - '1';
                    rmove.Move.NewPosition = new Position((File)toSquareFile, toSquareRank + 1);
                    ran = ran.Substring(2);
                }

                if (ran.Length > 0 && ran[0] == '=')
                {
                    rmove.Move.Promotion = ran[1];
                }
            }

            // castling rights
            if (oldCastlingRights.Contains('K'))
                rmove.CouldWhiteCastleKingSide = true;

            if (oldCastlingRights.Contains('Q'))
                rmove.CouldWhiteCastleQueenSide = true;

            if (oldCastlingRights.Contains('k'))
                rmove.CouldBlackCastleKingSide = true;

            if (oldCastlingRights.Contains('q'))
                rmove.CouldBlackCastleQueenSide = true;

            // epsquare
            if (oldEpSquare.Length > 1)
            {
                int epSquareFile = oldEpSquare[0] - 'a';
                int epSquareRank = oldEpSquare[1] - '1';
                rmove.OldEpSquare = new Position((File)epSquareFile, epSquareRank + 1);
            }

            return rmove;
        }
    }
}
