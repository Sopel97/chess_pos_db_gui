using chess_pos_db_gui.src.app.board;
using chess_pos_db_gui.src.chess;
using chess_pos_db_gui.src.util;
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

        private ChessBoardHistory BoardHistory { get; set; }

        private MoveHistoryTable MoveHistory { get; set; }

        private BoardTheme BoardImages { get; set; }
        private PieceTheme PieceImages { get; set; }

        private Point? MouseFrom { get; set; }
        private Point? MouseTo { get; set; }

        private bool IsBoardFlipped { get; set; }
        private int Plies { get; set; }
        private int FirstPly { get; set; }
        private string LastFen { get; set; }
        private int BaseMoveNumber { get; set; }
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

            BoardHistory = new ChessBoardHistory();

            MouseFrom = null;
            MouseTo = null;

            MoveHistory = new MoveHistoryTable();

            moveHistoryGridView.DataSource = MoveHistory;
            moveHistoryGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;

            WinFormsControlUtil.MakeDoubleBuffered(chessBoardPanel);

            LastFen = "";

            BaseMoveNumber = 1;
            MoveHistory.BaseMoveNumber = 1;

            IsBoardFlipped = false;
            IsSettingPosition = false;
        }

        public string GetFen()
        {
            return BoardHistory.Current().GetFen();
        }

        public string GetPrevFen()
        {
            return BoardHistory.Prev().GetFen();
        }

        public string GetLastMoveSan()
        {
            return BoardHistory.Current().GetSan();
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

            BoardHistory.SetInitialPosition(fen);
            MoveHistory.Clear();
            Plies = 0;
            MoveHistory.Rows.Add();
            MoveHistory.Last().No = 1;
            if (BoardHistory.Current().GCD.WhoseTurn == Player.Black)
            {
                BoardHistory.DuplicateLast();
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

        public void LoadImages()
        {
            BoardImages = new BoardTheme("assets/graphics/board_sets/chess24");
            PieceImages = new PieceTheme("assets/graphics/piece_sets/chess24");

            chessBoardPanel.Refresh();
        }

        private Rectangle GetSquareHitbox(int file, int rank)
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

            return new Rectangle((int)x, (int)y, (int)sw + 1, (int)sh + 1);
        }

        private Position ConvertPointToSquare(Point point)
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

        private void DrawSquare(Graphics g, Piece piece, int file, int rank)
        {
            var rect = GetSquareHitbox(file, rank);
            var squareImg =
                (file + rank) % 2 == 0
                ? BoardImages.LightSquare
                : BoardImages.DarkSquare;

            g.DrawImage(squareImg, rect);

            if (piece != null)
            {
                var pieceImg = PieceImages.GetImageForPiece(piece);
                g.DrawImage(pieceImg, rect);
            }
        }

        private void DrawSquares(Graphics g)
        {
            var game = BoardHistory.Current();
            var board = game.GetBoard();

            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    Piece piece = board[y][x];
                    DrawSquare(g, piece, x, y);
                }
            }
        }

        private void ChessBoardPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            DrawSquares(g);
        }

        private static Bitmap CreateDefaultBitmap()
        {
            var bmp = new Bitmap(8, 8);
            Graphics.FromImage(bmp).Clear(Color.Red);
            return bmp;
        }

        private void ChessBoard_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                LoadImages();
            }

            SetPosition(FenProvider.StartPos);
        }

        public string NextMoveNumber()
        {
            int c = BoardHistory.Plies;
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
            if (BoardHistory.Plies < FirstPly)
            {
                SetSelection(FirstPly);
            }

            if (Plies > BoardHistory.Plies)
            {
                RemoveLastMovesFromMoveHistory(Plies - BoardHistory.Plies);
            }
        }

        public bool DoMove(string san, bool silent = false)
        {
            Move move = San.ParseSan(new ChessGame(BoardHistory.Current().GCD), san);
            return DoMove(move, silent);
        }

        private bool DoMove(Move move, bool silent = false)
        {
            if (!BoardHistory.IsMoveValid(move))
            {
                return false;
            }

            // We only synch when te move was valid
            if (!silent)
            {
                SynchronizeMoveListWithHistory();
            }

            BoardHistory.DoMove(move);
            AddMoveToMoveHistory(BoardHistory.Current().Move, silent);
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

        private bool TryDragPiece(Point from, Point to)
        {
            Position fromSquare = ConvertPointToSquare(from);
            Position toSquare = ConvertPointToSquare(to);

            Player player = BoardHistory.Current().GCD.WhoseTurn;
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
            if (MouseFrom == null || MouseTo == null)
            {
                return;
            }

            TryDragPiece(MouseFrom.Value, MouseTo.Value);
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

            BoardHistory.SetCurrentPly(ply);
            string fen = BoardHistory.Current().GetFen();
            if (!IsSettingPosition)
            {
                UpdateFenTextBox(fen);
            }

            chessBoardPanel.Refresh();
        }

        internal Player SideToMove()
        {
            return BoardHistory.Current().GCD.WhoseTurn;
        }

        private void UpdateFenTextBox(string fen)
        {
            if (LastFen != fen)
            {
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
            SetSelection(BoardHistory.Plies - 1);
        }

        private void GoToNextButton_Click(object sender, EventArgs e)
        {
            SetSelection(BoardHistory.Plies + 1);
        }

        private void GoToEndButton_Click(object sender, EventArgs e)
        {
            SetSelection(BoardHistory.Count - 1);
        }

        private void CopyFenButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(LastFen);
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

        public string GetNextMoveSan()
        {
            var e = BoardHistory.Next();
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

        private void MoveHistoryGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (!moveHistoryGridView.Focused)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Left:
                    SetSelection(BoardHistory.Plies - 1);
                    e.Handled = true;
                    break;

                case Keys.Right:
                    SetSelection(BoardHistory.Plies + 1);
                    e.Handled = true;
                    break;
            }
        }
    }
    internal class MoveHistoryDataRow : DataRow
    {
        public int BaseMoveNumber { get; set; } = 1;

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
        public int BaseMoveNumber { get; set; } = 1;

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
