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

        private static readonly string StartPosFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        private ChessBoardHistory History { get; set; }

        private Image boardImage { get; set; }
        private Image boardImageWhite { get; set; }
        private Image boardImageBlack { get; set; }
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

        private bool IsBoardFlipped { get; set; }

        private MoveHistoryTable MoveHistory { get; set; }
        private int Plies { get; set; }
        private int FirstPly { get; set; }
        private string LastFen { get; set; }

        private EventHandler onPositionChanged { get; set; }
        public event EventHandler PositionChanged
        {
            add
            {
                onPositionChanged += value;
            }

            remove
            {
                onPositionChanged -= value;
            }
        }

        public ChessBoard()
        {
            InitializeComponent();

            History = new ChessBoardHistory();

            pieceImages = new Dictionary<Piece, Image>();

            MouseFrom = null;
            MouseTo = null;

            MoveHistory = new MoveHistoryTable();

            moveHistoryGridView.DataSource = MoveHistory;
            moveHistoryGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;

            MakeDoubleBuffered(chessBoardPanel);

            LastFen = "";

            IsBoardFlipped = false;
        }

        public string GetFen()
        {
            return History.Current().GetFen();
        }

        public string GetPrevFen()
        {
            return History.Prev().GetFen();
        }

        public string GetLastMoveSan()
        {
            return History.Current().GetSan();
        }

        private void Reset(string fen)
        {
            History.Reset(fen);
            MoveHistory.Clear();
            Plies = 0;
            MoveHistory.Rows.Add();
            MoveHistory.Last().No = 1;
            if (History.Current().GCD.WhoseTurn == Player.Black)
            {
                History.DuplicateLast();
                MoveHistory.Last().WhiteDetailedMove = null;
                Plies = 1;
                SetSelection(1);
            }
            FirstPly = Plies;

            UpdateFenTextBox(fen);
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
            boardImageWhite = Image.FromFile(path + "/board_w.png");
            boardImageBlack = Image.FromFile(path + "/board_b.png");
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

            chessBoardPanel.Refresh();
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
            var img = IsBoardFlipped ? boardImageBlack : boardImageWhite;
            g.DrawImage(img, 0, 0, chessBoardPanel.Width, chessBoardPanel.Height);
        }

        private Rectangle GetSquareRectangle(int file, int rank)
        {
            if (IsBoardFlipped)
            {
                file = 7 - file;
                rank = 7 - rank;
            }

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

            if (IsBoardFlipped)
            {
                x = 7 - x;
                y = 7 - y;
            }

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
            var game = History.Current();
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
        }

        private static Bitmap CreateDefaultBitmap()
        {
            var bmp = new Bitmap(8, 8);
            Graphics.FromImage(bmp).Clear(Color.Red);
            return bmp;
        }

        private void ChessBoard_Load(object sender, EventArgs e)
        {
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

            Reset(StartPosFen);
        }

        public string NextMoveNumber()
        {
            int c = History.Plies;
            int move = (c - 1) / 2 + 1;
            bool isWhite = c % 2 == 0;

            return 
                isWhite 
                ? (move.ToString() + ".")
                : (move.ToString() + "...");
        }

        private void ChessBoard_SizeChanged(object sender, EventArgs e)
        {
            splitFenAndControls.Height = Height;
        }

        private void SplitContainer1_Panel1_SizeChanged(object sender, EventArgs e)
        {
            Size s = new Size(splitBoardAndMoves.Panel1.Width, splitBoardAndMoves.Panel1.Height);
            chessBoardPanel.Size = FitWithAspectRatio(s, 1.0f);
            chessBoardPanel.Refresh();
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

        private void SynchronizeMoveListWithHistory()
        {
            if (History.Plies < FirstPly)
            {
                SetSelection(FirstPly);
            }

            if (Plies > History.Plies)
            {
                RemoveLastMovesFromMoveHistory(Plies - History.Plies);
            }
        }

        public bool DoMove(string san)
        {
            Move move = San.ParseSan(new ChessGame(History.Current().GCD), san);
            return DoMove(move);
        }

        public Move LanToMove(string lan)
        {
            Move move = San.ParseLan(new ChessGame(History.Current().GCD), lan);
            return move;
        }

        public Move LanToMove(string fen, string lan)
        {
            Move move = San.ParseLan(new ChessGame(fen), lan);
            return move;
        }

        public Move SanToMove(string san)
        {
            Move move = San.ParseSan(new ChessGame(History.Current().GCD), san);
            return move;
        }

        private bool DoMove(Move move)
        {
            if (!History.IsMoveValid(move)) return false;

            // We only synch when te move was valid
            SynchronizeMoveListWithHistory();

            History.DoMove(move);
            AddMoveToMoveHistory(History.Current().Move);
            moveHistoryGridView.Refresh();
            chessBoardPanel.Refresh();
            return true;
        }

        private bool UndoMove()
        {
            SynchronizeMoveListWithHistory();

            if (History.UndoMove())
            {
                RemoveLastMoveFromMoveHistory();
                return true;
            }

            return false;
        }

        private void RemoveLastMoveFromMoveHistory()
        {
            RemoveLastMovesFromMoveHistory(1);
        }

        private void RemoveLastMovesFromMoveHistory(int count)
        {
            for(int i = 0; i < count; ++i)
            {
                Player player = Plies % 2 == 0 ? Player.Black : Player.White;
                --Plies;

                if (player == Player.White)
                {
                    MoveHistory.Rows.RemoveAt(MoveHistory.Rows.Count - 1);
                }
                else
                {
                    MoveHistory.Last().BlackDetailedMove = null;
                }
            }
        }

        private bool TryPerformMoveBasedOnMouseDrag(Point? from, Point? to)
        {
            if (from == null || to == null) return false;

            Position fromSquare = PointToSquare(from.Value);
            Position toSquare = PointToSquare(to.Value);

            Player player = History.Current().GCD.WhoseTurn;
            Move move = new Move(fromSquare, toSquare, player);
            return DoMove(move);
        }

        private void SetSelection(int beforePly)
        {
            if (beforePly < FirstPly || beforePly > Plies) return;

            if (beforePly == 0)
            {
                moveHistoryGridView["No", 0].Selected = true;
            }
            else
            {
                --beforePly;
                Player player = beforePly % 2 == 0 ? Player.White : Player.Black;
                if (player == Player.White)
                {
                    moveHistoryGridView["WhiteMove", beforePly / 2].Selected = true;
                }
                else
                {
                    moveHistoryGridView["BlackMove", beforePly / 2].Selected = true;
                }
            }
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
                int No = Plies / 2 + 1;
                while (MoveHistory.Rows.Count < No)
                {
                    MoveHistory.Rows.Add();
                }
                MoveHistory.Last().No = No;
                MoveHistory.Last().WhiteDetailedMove = move;
            }
            else
            {
                MoveHistory.Last().BlackDetailedMove = move;
            }

            SetSelection(Plies);
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

        private void MoveHistoryGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (moveHistoryGridView.SelectedCells.Count == 0) return;

            var cell = moveHistoryGridView.SelectedCells[0];

            int row = cell.RowIndex;
            int col = cell.ColumnIndex;
            int ply = row * 2 + col;
            if (ply > Plies) return;

            History.SetCurrent(ply);
            string fen = History.Current().GetFen();
            UpdateFenTextBox(fen);

            chessBoardPanel.Refresh();
        }

        internal Player CurrentPlayer()
        {
            return History.Current().GCD.WhoseTurn;
        }

        private void UpdateFenTextBox(string fen)
        {
            if (LastFen != fen)
            {
                LastFen = fen;
                fenTextBox.Text = fen;
                onPositionChanged?.Invoke(this, new EventArgs());
            }
        }

        private void GoToStartButton_Click(object sender, EventArgs e)
        {
            SetSelection(FirstPly);
        }

        private void GoToPrevButton_Click(object sender, EventArgs e)
        {
            SetSelection(History.Plies - 1);
        }

        private void GoToNextButton_Click(object sender, EventArgs e)
        {
            SetSelection(History.Plies + 1);
        }

        private void GoToEndButton_Click(object sender, EventArgs e)
        {
            SetSelection(History.Count - 1);
        }

        private void FenTextBox_TextChanged(object sender, EventArgs e)
        {
            if (LastFen != fenTextBox.Text)
            {
                try
                {
                    new ChessGame(fenTextBox.Text);
                }
                catch(Exception)
                {
                    MessageBox.Show("Invalid FEN.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Reset(fenTextBox.Text);
            }
        }

        private void CopyFenButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(fenTextBox.Text);
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            Reset(StartPosFen);
        }

        private void FlipBoardButton_Click(object sender, EventArgs e)
        {
            IsBoardFlipped = !IsBoardFlipped;
            Refresh();
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
            set {
                _WhiteDetailedMove = value;
                if (value != null) base["WhiteMove"] = value.SAN;
                else base["WhiteMove"] = "";
            }
        }
        public DetailedMove BlackDetailedMove {
            get { return _BlackDetailedMove; }
            set {
                _BlackDetailedMove = value;
                if (value != null) base["BlackMove"] = value.SAN;
                else base["BlackMove"] = "";
            } 
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
