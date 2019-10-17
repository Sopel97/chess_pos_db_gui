using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessDotNet;
using ChessDotNet.Pieces;
using System.Reflection;

namespace chess_pos_db_gui
{

    public partial class ChessBoard : UserControl
    {
        private static readonly Bitmap DefaultBitmap = CreateDefaultBitmap();

        private ChessBoardHistory History { get; set; }

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

        private Dictionary<Piece, Image> pieceImages;
        private Point? MouseFrom { get; set; }
        private Point? MouseTo { get; set; }

        private MoveHistoryTable MoveHistory { get; set; }
        private int Plies { get; set; }

        public ChessBoard()
        {
            InitializeComponent();

            History = new ChessBoardHistory();
            History.DoMove("e4");
            History.DoMove("e5");
            History.DoMove("Ke2");
            History.UndoMove();
            pieceImages = new Dictionary<Piece, Image>();

            MouseFrom = null;
            MouseTo = null;

            MoveHistory = new MoveHistoryTable();

            moveHistoryGridView.DataSource = MoveHistory;
            moveHistoryGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;

            Plies = 0;

            MakeDoubleBuffered(chessBoardPanel);
        }
        private static void MakeDoubleBuffered(Panel chessBoardPanel)
        {
            Type dgvType = chessBoardPanel.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(chessBoardPanel, true, null);
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

            pieceImages.Add(new Pawn(Player.White), whitePawn);
            pieceImages.Add(new Knight(Player.White), whiteKnight);
            pieceImages.Add(new Bishop(Player.White), whiteBishop);
            pieceImages.Add(new Rook(Player.White), whiteRook);
            pieceImages.Add(new Queen(Player.White), whiteQueen);
            pieceImages.Add(new King(Player.White), whiteKing);

            pieceImages.Add(new Pawn(Player.Black), blackPawn);
            pieceImages.Add(new Knight(Player.Black), blackKnight);
            pieceImages.Add(new Bishop(Player.Black), blackBishop);
            pieceImages.Add(new Rook(Player.Black), blackRook);
            pieceImages.Add(new Queen(Player.Black), blackQueen);
            pieceImages.Add(new King(Player.Black), blackKing);
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

        private Position PointToSquare(Point point)
        {
            float w = chessBoardPanel.Width;
            float h = chessBoardPanel.Height;

            float sw = w / 8;
            float sh = h / 8;

            int x = (int)(point.X / sw);
            int y = (int)(point.Y / sh);

            x = Math.Min(x, 7);
            x = Math.Max(x, 0);

            y = Math.Min(y, 7);
            y = Math.Max(y, 0);

            return new Position((File)x, 8 - y); //y is in range 1-8
        }

        private void DrawOnSquare(Graphics g, Image img, int file, int rank)
        {
            g.DrawImage(img, GetSquareRectangle(file, rank));
        }

        private void DrawPiece(Graphics g, Piece piece, int file, int rank)
        {
            Image img = pieceImages[piece];
            DrawOnSquare(g, img, file, rank);
        }

        private void DrawPieces(Graphics g)
        {
            var game = History.Last();
            var board = game.GetBoard();

            for(int x = 0; x < 8; ++x)
            {
                for(int y = 0; y < 8; ++y)
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
            System.Diagnostics.Debug.WriteLine(History.Last().GetFen());

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

        private void ChessBoard_SizeChanged(object sender, EventArgs e)
        {
            chessBoardPanel.Size = FitWithAspectRatio(Size, 1.0f);
            historyPanel.Height = Height - historyPanel.Margin.Top - historyPanel.Margin.Bottom;
            chessBoardPanel.Refresh();
        }

        private void ChessBoardPanel_SizeChanged(object sender, EventArgs e)
        {
            historyPanel.Location = new Point(chessBoardPanel.Location.X + chessBoardPanel.Width, historyPanel.Location.Y);
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

        private bool TryPerformMoveBasedOnMouseDrag(Point? from, Point? to)
        {
            if (from == null || to == null) return false;

            Position fromSquare = PointToSquare(from.Value);
            Position toSquare = PointToSquare(to.Value);

            Player player = History.Last().GCD.WhoseTurn;
            Move move = new Move(fromSquare, toSquare, player);
            if (History.DoMove(move))
            {
                AddMoveToMoveHistory(History.Last().Move);
                moveHistoryGridView.Refresh();
                chessBoardPanel.Refresh();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Invalid move.");
                System.Diagnostics.Debug.WriteLine(fromSquare);
                System.Diagnostics.Debug.WriteLine(toSquare);
            }

            return true;
        }

        private void AddMoveToMoveHistory(DetailedMove move)
        {
            Player shouldBePlayer = Plies % 2 == 0 ? Player.White : Player.Black;
            if (move.Player != shouldBePlayer)
            {
                throw new ArgumentException("");
            }
            ++Plies;

            if (move.Player == Player.White)
            {
                MoveHistory.Rows.Add();
                MoveHistory.Last().No = Plies / 2 + 1;
                MoveHistory.Last().WhiteDetailedMove = move;
            }
            else
            {
                MoveHistory.Last().BlackDetailedMove = move;
            }
        }

        private void ChessBoardPanel_MouseDown(object sender, MouseEventArgs e)
        {
            MouseFrom = new Point(e.X, e.Y);
        }

        private void ChessBoardPanel_MouseUp(object sender, MouseEventArgs e)
        {
            MouseTo = new Point(e.X, e.Y);
            TryPerformMoveBasedOnMouseDrag(MouseFrom, MouseTo);
        }
    }
    internal class MoveHistoryDataRow : DataRow
    {
        private int _No { get; set; }
        private DetailedMove _WhiteDetailedMove { get; set; }
        private DetailedMove _BlackDetailedMove { get; set; }
        public int No {
            get { return _No; }
            set { _No = value; base["No"] = value.ToString() + "."; }
        }
        public DetailedMove WhiteDetailedMove {
            get { return _WhiteDetailedMove; }
            set { _WhiteDetailedMove = value; base["WhiteMove"] = value.SAN; }
        }
        public DetailedMove BlackDetailedMove {
            get { return _BlackDetailedMove; }
            set { _BlackDetailedMove = value; base["BlackMove"] = value.SAN; } 
        }

        public MoveHistoryDataRow(DataRowBuilder builder) :
            base(builder)
        {
        }
    }

    internal class MoveHistoryTable : DataTable
    {
        public MoveHistoryTable()
        {
            Columns.Add(new DataColumn("No", typeof(string)));
            Columns.Add(new DataColumn("WhiteMove", typeof(string)));
            Columns.Add(new DataColumn("BlackMove", typeof(string)));
        }
        public MoveHistoryDataRow this[int idx]
        {
            get { return (MoveHistoryDataRow)Rows[idx]; }
        }

        public MoveHistoryDataRow Last()
        {
            return this[Rows.Count - 1];
        }

        protected override Type GetRowType()
        {
            return typeof(MoveHistoryDataRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new MoveHistoryDataRow(builder);
        }
        public void Add(MoveHistoryDataRow row)
        {
            Rows.Add(row);
        }
    }
}
