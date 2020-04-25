using ChessDotNet;
using ChessDotNet.Pieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui.src.app.board
{
    public class PieceTheme
    {
        private Dictionary<Piece, Image> PieceImages { get; set; }

        public PieceTheme(string path)
        {
            PieceImages = new Dictionary<Piece, Image>();

            PieceImages.Add(new Pawn(Player.White), Image.FromFile(path + "/white_pawn.png"));
            PieceImages.Add(new Knight(Player.White), Image.FromFile(path + "/white_knight.png"));
            PieceImages.Add(new Bishop(Player.White), Image.FromFile(path + "/white_bishop.png"));
            PieceImages.Add(new Rook(Player.White), Image.FromFile(path + "/white_rook.png"));
            PieceImages.Add(new Queen(Player.White), Image.FromFile(path + "/white_queen.png"));
            PieceImages.Add(new King(Player.White), Image.FromFile(path + "/white_king.png"));

            PieceImages.Add(new Pawn(Player.Black), Image.FromFile(path + "/black_pawn.png"));
            PieceImages.Add(new Knight(Player.Black), Image.FromFile(path + "/black_knight.png"));
            PieceImages.Add(new Bishop(Player.Black), Image.FromFile(path + "/black_bishop.png"));
            PieceImages.Add(new Rook(Player.Black), Image.FromFile(path + "/black_rook.png"));
            PieceImages.Add(new Queen(Player.Black), Image.FromFile(path + "/black_queen.png"));
            PieceImages.Add(new King(Player.Black), Image.FromFile(path + "/black_king.png"));
        }

        public Image GetImageForPiece(Piece piece)
        {
            return PieceImages[piece];
        }
    }
}
