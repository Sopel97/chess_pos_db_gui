﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessDotNet;

namespace chess_pos_db_gui
{
    public partial class ChessBoard : UserControl
    {
        private static readonly Bitmap DefaultBitmap = CreateDefaultBitmap();

        private ChessGame Game { get; set; }

        private Image boardImage { get; set; }
        private Image boardLightSquare { get; set; }
        private Image boardDarkSquare { get; set; }
        private Image whitePawn { get; set; }
        private Image whiteKnight { get; set; }
        private Image whiteBishop { get; set; }
        private Image whiteRook { get; set; }
        private Image whiteQueen { get; set; }
        private Image whiteKing { get; set; }
        private Image blackPawn { get; set; }
        private Image blackKnight { get; set; }
        private Image blackBishop { get; set; }
        private Image blackRook { get; set; }
        private Image blackQueen { get; set; }
        private Image blackKing { get; set; }

        private Dictionary<char, Image> pieceImages;

        public ChessBoard()
        {
            InitializeComponent();

            Game = new ChessGame();
            pieceImages = new Dictionary<char, Image>();
        }

        public void LoadImages(string path)
        {
            boardImage = Image.FromFile(path + "/board.png");
            boardLightSquare = Image.FromFile(path + "/board_light.png");
            boardDarkSquare = Image.FromFile(path + "/board_dark.png");

            whitePawn = Image.FromFile(path + "/white_pawn.png");
            whiteKnight = Image.FromFile(path + "/white_knight.png");
            whiteBishop = Image.FromFile(path + "/white_bishop.png");
            whiteRook = Image.FromFile(path + "/white_rook.png");
            whiteQueen = Image.FromFile(path + "/white_queen.png");
            whiteKing = Image.FromFile(path + "/white_king.png");

            blackPawn = Image.FromFile(path + "/black_pawn.png");
            blackKnight = Image.FromFile(path + "/black_knight.png");
            blackBishop = Image.FromFile(path + "/black_bishop.png");
            blackRook = Image.FromFile(path + "/black_rook.png");
            blackQueen = Image.FromFile(path + "/black_queen.png");
            blackKing = Image.FromFile(path + "/black_king.png");

            UpdatePieceImagesDictionary();
        }

        private void UpdatePieceImagesDictionary()
        {
            pieceImages.Clear();

            pieceImages.Add('P', whitePawn);
            pieceImages.Add('N', whiteKnight);
            pieceImages.Add('B', whiteBishop);
            pieceImages.Add('R', whiteRook);
            pieceImages.Add('Q', whiteQueen);
            pieceImages.Add('K', whiteKing);

            pieceImages.Add('p', blackPawn);
            pieceImages.Add('n', blackKnight);
            pieceImages.Add('b', blackBishop);
            pieceImages.Add('r', blackRook);
            pieceImages.Add('q', blackQueen);
            pieceImages.Add('k', blackKing);
        }

        private void DrawBoard(Graphics g)
        {
            // TODO: maybe from single squares?
            g.DrawImage(boardImage, 0, 0, chessBoardPanel.Width, chessBoardPanel.Height);
        }

        private Rectangle GetSquareRectangle(int file, int rank)
        {
            float w = chessBoardPanel.Width;
            float h = chessBoardPanel.Height;

            float sw = w / 8;
            float sh = h / 8;

            float x = sw * file;
            float y = sh * rank;

            return new Rectangle((int)x, (int)y, (int)sw, (int)sh);
        }

        private void DrawOnSquare(Graphics g, Image img, int file, int rank)
        {
            g.DrawImage(img, GetSquareRectangle(file, rank));
        }

        private void DrawPiece(Graphics g, Piece piece, int file, int rank)
        {
            char c = piece.GetFenCharacter();
            Image img = pieceImages[c];
            DrawOnSquare(g, img, file, rank);
        }

        private void DrawPieces(Graphics g)
        {
            var board = Game.GetBoard();

            for(int x = 0; x < Game.BoardWidth; ++x)
            {
                for(int y = 0; y < Game.BoardHeight; ++y)
                {
                    Piece piece = board[y][x];
                    if (piece != null)
                    {
                        DrawPiece(g, piece, x, y);
                    }
                }
            }
        }

        private void ChessBoardPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            DrawBoard(g);
            DrawPieces(g);

            System.Diagnostics.Debug.WriteLine("DRAW");
        }

        private static Bitmap CreateDefaultBitmap()
        {
            var bmp = new Bitmap(8, 8);
            Graphics.FromImage(bmp).Clear(Color.Red);
            return bmp;
        }

        private void ChessBoard_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(Game.GetFen());
            System.Diagnostics.Debug.WriteLine(Size);

            boardImage = DefaultBitmap;
            boardLightSquare = DefaultBitmap;
            boardDarkSquare = DefaultBitmap;

            whitePawn = DefaultBitmap;
            whiteKnight = DefaultBitmap;
            whiteBishop = DefaultBitmap;
            whiteRook = DefaultBitmap;
            whiteQueen = DefaultBitmap;
            whiteKing = DefaultBitmap;

            blackPawn = DefaultBitmap;
            blackKnight = DefaultBitmap;
            blackBishop = DefaultBitmap;
            blackRook = DefaultBitmap;
            blackQueen = DefaultBitmap;
            blackKing = DefaultBitmap;

            UpdatePieceImagesDictionary();
        }

        private void ChessBoardPanel_SizeChanged(object sender, EventArgs e)
        {
            chessBoardPanel.Size = FitWithAspectRatio(chessBoardPanel.Size, 1.0f);
        }

        private Size FitWithAspectRatio(Size size, float ratio)
        {
            float w = size.Width;
            float h = size.Height;
            if (w > h * ratio)
            {
                w = h * ratio;
            }
            else if (h > w / ratio)
            {
                h = w / ratio;
            }

            return new Size((int)w, (int)h);
        }
    }
}
