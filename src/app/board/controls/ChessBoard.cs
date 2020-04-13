using chess_pos_db_gui.src.chess;

using ChessDotNet;
using ChessDotNet.Pieces;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace chess_pos_db_gui
{

    public partial class ChessBoard : UserControl
    {
        private static readonly Bitmap DefaultBitmap = CreateDefaultBitmap();

        private ChessBoardHistory History { get; set; }

        private Image BoardImageWhite { get; set; }
        private Image BoardImageBlack { get; set; }
        private Image WhitePawn { get; set; }
        private Image WhiteKnight { get; set; }
        private Image WhiteBishop { get; set; }
        private Image WhiteRook { get; set; }
        private Image WhiteQueen { get; set; }
        private Image WhiteKing { get; set; }
        private Image BlackPawn { get; set; }
        private Image BlackKnight { get; set; }
        private Image BlackBishop { get; set; }
        private Image BlackRook { get; set; }
        private Image BlackQueen { get; set; }
        private Image BlackKing { get; set; }

        private Dictionary<Piece, Image> PieceImages { get; set; }
        private Point? MouseFrom { get; set; }
        private Point? MouseTo { get; set; }

        private bool IsBoardFlipped { get; set; }

        private MoveHistoryTable MoveHistory { get; set; }
        private int Plies { get; set; }
        private int FirstPly { get; set; }
        private string LastFen { get; set; }
        private int BaseMoveNumber;
        private bool IsSettingPosition { get; set; }

        private EventHandler onPositionChanged;
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

            PieceImages = new Dictionary<Piece, Image>();

            MouseFrom = null;
            MouseTo = null;

            MoveHistory = new MoveHistoryTable();

            moveHistoryGridView.DataSource = MoveHistory;
            moveHistoryGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;

            MakeDoubleBuffered(chessBoardPanel);

            LastFen = "";

            BaseMoveNumber = 1;
            MoveHistory.BaseMoveNumber = 1;

            IsBoardFlipped = false;
            IsSettingPosition = false;
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

        private void SetGame(ChessGame game)
        {
            SetPosition(FenProvider.StartPos);
            foreach (var move in game.Moves)
            {
                DoMove(move.SAN, true);
            }
        }

        private void SetPosition(string fen)
        {
            IsSettingPosition = true;

            History.SetInitialPosition(fen);
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

            if (int.TryParse(fen.Split(' ').Last(), out int n))
            {
                BaseMoveNumber = n;
                MoveHistory.BaseMoveNumber = n;
                foreach (MoveHistoryDataRow row in MoveHistory.Rows)
                {
                    row.UpdateBaseMoveNumber(n);
                }
            }

            UpdateFenTextBox(fen);

            IsSettingPosition = false;
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
            BoardImageWhite = Image.FromFile(path + "/board_w.png");
            BoardImageBlack = Image.FromFile(path + "/board_b.png");

            WhitePawn = Image.FromFile(path + "/white_pawn.png");
            WhiteKnight = Image.FromFile(path + "/white_knight.png");
            WhiteBishop = Image.FromFile(path + "/white_bishop.png");
            WhiteRook = Image.FromFile(path + "/white_rook.png");
            WhiteQueen = Image.FromFile(path + "/white_queen.png");
            WhiteKing = Image.FromFile(path + "/white_king.png");

            BlackPawn = Image.FromFile(path + "/black_pawn.png");
            BlackKnight = Image.FromFile(path + "/black_knight.png");
            BlackBishop = Image.FromFile(path + "/black_bishop.png");
            BlackRook = Image.FromFile(path + "/black_rook.png");
            BlackQueen = Image.FromFile(path + "/black_queen.png");
            BlackKing = Image.FromFile(path + "/black_king.png");

            UpdatePieceImagesDictionary();

            chessBoardPanel.Refresh();
        }

        private void UpdatePieceImagesDictionary()
        {
            PieceImages.Clear();

            PieceImages.Add(new Pawn(Player.White), WhitePawn);
            PieceImages.Add(new Knight(Player.White), WhiteKnight);
            PieceImages.Add(new Bishop(Player.White), WhiteBishop);
            PieceImages.Add(new Rook(Player.White), WhiteRook);
            PieceImages.Add(new Queen(Player.White), WhiteQueen);
            PieceImages.Add(new King(Player.White), WhiteKing);

            PieceImages.Add(new Pawn(Player.Black), BlackPawn);
            PieceImages.Add(new Knight(Player.Black), BlackKnight);
            PieceImages.Add(new Bishop(Player.Black), BlackBishop);
            PieceImages.Add(new Rook(Player.Black), BlackRook);
            PieceImages.Add(new Queen(Player.Black), BlackQueen);
            PieceImages.Add(new King(Player.Black), BlackKing);
        }

        private void DrawBoard(Graphics g)
        {
            var img = IsBoardFlipped ? BoardImageBlack : BoardImageWhite;
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
            Image img = PieceImages[piece];
            DrawOnSquare(g, img, file, rank);
        }

        private void DrawPieces(Graphics g)
        {
            var game = History.Current();
            var board = game.GetBoard();

            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
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
            WhitePawn = DefaultBitmap;
            WhiteKnight = DefaultBitmap;
            WhiteBishop = DefaultBitmap;
            WhiteRook = DefaultBitmap;
            WhiteQueen = DefaultBitmap;
            WhiteKing = DefaultBitmap;

            BlackPawn = DefaultBitmap;
            BlackKnight = DefaultBitmap;
            BlackBishop = DefaultBitmap;
            BlackRook = DefaultBitmap;
            BlackQueen = DefaultBitmap;
            BlackKing = DefaultBitmap;

            UpdatePieceImagesDictionary();

            SetPosition(FenProvider.StartPos);
        }

        public string NextMoveNumber()
        {
            int c = History.Plies;
            int move = c / 2 + BaseMoveNumber;
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

        public bool DoMove(string san, bool silent = false)
        {
            Move move = San.ParseSan(new ChessGame(History.Current().GCD), san);
            return DoMove(move, silent);
        }

        private bool DoMove(Move move, bool silent = false)
        {
            if (!History.IsMoveValid(move))
            {
                return false;
            }

            // We only synch when te move was valid
            if (!silent)
            {
                SynchronizeMoveListWithHistory();
            }

            History.DoMove(move);
            AddMoveToMoveHistory(History.Current().Move, silent);
            if (!silent)
            {
                moveHistoryGridView.Refresh();
                chessBoardPanel.Refresh();
            }

            return true;
        }

        private void RemoveLastMovesFromMoveHistory(int count)
        {
            for (int i = 0; i < count; ++i)
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
            if (from == null || to == null)
            {
                return false;
            }

            Position fromSquare = PointToSquare(from.Value);
            Position toSquare = PointToSquare(to.Value);

            Player player = History.Current().GCD.WhoseTurn;
            Move move = new Move(fromSquare, toSquare, player);
            return DoMove(move);
        }

        private void SetSelection(int beforePly)
        {
            if (beforePly < FirstPly || beforePly > Plies)
            {
                return;
            }

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

        private void AddMoveToMoveHistory(DetailedMove move, bool silent = false)
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

            if (!silent)
            {
                SetSelection(Plies);
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

        private void MoveHistoryGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (moveHistoryGridView.SelectedCells.Count == 0)
            {
                return;
            }

            var cell = moveHistoryGridView.SelectedCells[0];

            int row = cell.RowIndex;
            int col = cell.ColumnIndex;
            int ply = row * 2 + col;
            if (ply > Plies)
            {
                return;
            }

            History.SetCurrent(ply);
            string fen = History.Current().GetFen();
            if (!IsSettingPosition)
            {
                UpdateFenTextBox(fen);
            }

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
                fenTextBox.Text = fen;
                onPositionChanged?.Invoke(this, new EventArgs());
                LastFen = fen;
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

        private void CopyFenButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(fenTextBox.Text);
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            SetPosition(FenProvider.StartPos);
        }

        private void FlipBoardButton_Click(object sender, EventArgs e)
        {
            IsBoardFlipped = !IsBoardFlipped;
            Refresh();
        }

        private void TrySetFen(string fen)
        {
            var newFen = fen;
            if (LastFen != newFen)
            {
                try
                {
                    new ChessGame(newFen);
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid FEN.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SetPosition(newFen);
            }
        }

        private void SetFenButton_Click(object sender, EventArgs e)
        {
            using (var form = new FenInputForm())
            {
                form.ShowDialog();
                if (!form.WasCancelled)
                {
                    TrySetFen(form.Fen);
                }
            }
        }

        private void SetPgnButton_Click(object sender, EventArgs e)
        {
            using (var form = new PgnInputForm())
            {
                form.ShowDialog();
                if (!form.WasCancelled)
                {
                    var movetext = form.MoveText;
                    var pgnReader = new PgnReader<ChessGame>();
                    try
                    {
                        pgnReader.ReadPgnFromString(movetext);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid PGN.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    SetGame(pgnReader.Game);
                }
            }
        }

        public String GetNextMoveSan()
        {
            var e = History.Next();
            if (e == null)
            {
                return null;
            }

            return e.Move.SAN;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                TrySetFen(Clipboard.GetText());
            }
            else
            {
                MessageBox.Show("No text in clipboard.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    internal class MoveHistoryDataRow : DataRow
    {
        public int BaseMoveNumber = 1;
        private int _No;
        private DetailedMove _WhiteDetailedMove;
        private DetailedMove _BlackDetailedMove;
        public int No
        {
            get { return _No; }
            set { _No = value; base["No"] = (value + BaseMoveNumber - 1).ToString() + "."; }
        }
        public DetailedMove WhiteDetailedMove
        {
            get { return _WhiteDetailedMove; }
            set
            {
                _WhiteDetailedMove = value;
                if (value != null)
                {
                    base["WhiteMove"] = value.SAN;
                }
                else
                {
                    base["WhiteMove"] = "";
                }
            }
        }
        public DetailedMove BlackDetailedMove
        {
            get { return _BlackDetailedMove; }
            set
            {
                _BlackDetailedMove = value;
                if (value != null)
                {
                    base["BlackMove"] = value.SAN;
                }
                else
                {
                    base["BlackMove"] = "";
                }
            }
        }

        public MoveHistoryDataRow(DataRowBuilder builder, int n) :
            base(builder)
        {
            BaseMoveNumber = n;
        }

        public void UpdateBaseMoveNumber(int n)
        {
            BaseMoveNumber = n;
            No = _No;
        }
    }

    internal class MoveHistoryTable : DataTable
    {
        public int BaseMoveNumber = 1;

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
            return new MoveHistoryDataRow(builder, BaseMoveNumber);
        }
        public void Add(MoveHistoryDataRow row)
        {
            Rows.Add(row);
        }
    }
}
