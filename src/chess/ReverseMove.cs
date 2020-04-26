using ChessDotNet;
using ChessDotNet.Pieces;
using System;

namespace chess_pos_db_gui.src.chess
{
    public class ReverseMove
    {
        public Move Move { get; set; } = null;
        public Piece CapturedPiece { get; set; } = null;
        public Position OldEpSquare { get; set; } = null;
        public bool CouldWhiteCastleKingSide { get; set; } = false;
        public bool CouldWhiteCastleQueenSide { get; set; } = false;
        public bool CouldBlackCastleKingSide { get; set; } = false;
        public bool CouldBlackCastleQueenSide { get; set; } = false;

        public ChessGame AppliedTo(ChessGame game)
        {
            Player prevPlayer =
                game.WhoseTurn == Player.White
                ? Player.Black
                : Player.White;

            var creationData = game.GetGameCreationData();

            creationData.WhoseTurn = prevPlayer;

            creationData.CanWhiteCastleKingSide = CouldWhiteCastleKingSide;
            creationData.CanWhiteCastleQueenSide = CouldWhiteCastleQueenSide;
            creationData.CanBlackCastleKingSide = CouldBlackCastleKingSide;
            creationData.CanBlackCastleQueenSide = CouldBlackCastleQueenSide;

            creationData.FullMoveNumber = 1;
            creationData.HalfMoveClock = 1;

            creationData.EnPassant = OldEpSquare;

            bool isCastling = IsCastling(game);
            if (isCastling)
            {
                UndoCastling(creationData);
            }
            else
            {
                Piece movedPiece = new Pawn(prevPlayer);
                if (Move.Promotion == null)
                {
                    movedPiece = game.GetPieceAt(Move.NewPosition);
                }

                creationData.Board[8 - Move.NewPosition.Rank][(int)Move.NewPosition.File] = CapturedPiece;
                creationData.Board[8 - Move.OriginalPosition.Rank][(int)Move.OriginalPosition.File] = movedPiece;
            }

            creationData.Moves = new DetailedMove[0];

            return new ChessGame(creationData);
        }

        private void UndoCastling(GameCreationData creationData)
        {
            int r =
                creationData.WhoseTurn == Player.White
                ? 7
                : 0;

            creationData.Board[r][(int)File.E] = new King(creationData.WhoseTurn);
            creationData.Board[r][(int)Move.NewPosition.File] = null; // erase king

            if (Move.NewPosition.File == File.C)
            {
                creationData.Board[r][(int)File.D] = null; // erase rook
                creationData.Board[r][(int)File.A] = new Rook(creationData.WhoseTurn); // place rook
            }
            else // File.G
            {
                creationData.Board[r][(int)File.F] = null; // erase rook
                creationData.Board[r][(int)File.H] = new Rook(creationData.WhoseTurn); // place rook
            }
        }

        private bool IsCastling(ChessGame game)
        {
            if (Move.OriginalPosition.File != File.E)
            {
                return false;
            }

            Piece piece = game.GetPieceAt(Move.NewPosition);
            if (piece.Equals(new King(Player.White)) || piece.Equals(new King(Player.Black)))
            {
                return
                    Move.NewPosition.File == File.G
                    || Move.NewPosition.File == File.C;
            }

            return false;
        }
    }
}
