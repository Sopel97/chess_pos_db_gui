using ChessDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
