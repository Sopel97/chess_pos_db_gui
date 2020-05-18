using chess_pos_db_gui.src.app.board;
using chess_pos_db_gui.src.app.board.forms;
using chess_pos_db_gui.src.chess;
using chess_pos_db_gui.src.util;
using ChessDotNet;
using ChessDotNet.Pieces;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace chess_pos_db_gui
{
    public partial class ChessBoard : UserControl
    {
        private struct DrawingSpaceUsage
        {
            public Rectangle SquaresSpace { get; set; }
            public int RimThickness { get; set; }
            public int OuterRimTransitionThickness { get; set; }
            public int InnerRimTransitionThickness { get; set; }
        }

        private ChessBoardHistory BoardHistory { get; set; }

        private MoveHistoryTable MoveHistory { get; set; }

        public BoardTheme BoardImages { get; set; }
        public PieceTheme PieceImages { get; set; }

        private Point? MouseFrom { get; set; }
        private Point? MouseTo { get; set; }

        private Point LastMousePosition { get; set; } = new Point(0, 0);

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

        public void SetGame(ChessGame game)
        {
            SetGame(FenProvider.StartPos, game);
        }

        public void SetGame(string startpos, ChessGame game)
        {
            SetPosition(startpos);
            foreach (var move in game.Moves)
            {
                DoMove(move.SAN, true);
            }
            SetSelection(FirstPly);
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

        public void SetThemes(BoardTheme board, PieceTheme piece)
        {
            BoardImages = board;
            PieceImages = piece;

            chessBoardPanel.Refresh();
        }

        private DrawingSpaceUsage GetDrawingSpaceUsage()
        {
            float rimThickness = 0.0f;
            float rimInnerTransitionThickness = 0.0f;
            float rimOuterTransitionThickness = 0.0f;
            float totalRimThickness = 0.0f;
            var rimConfig = BoardImages.Config.Rim;
            if (rimConfig != null)
            {
                totalRimThickness += rimThickness = rimConfig.Thickness;
                if (rimConfig.InnerTransition != null)
                {
                    totalRimThickness += rimInnerTransitionThickness = rimConfig.InnerTransition.Thickness;
                }
                if (rimConfig.OuterTransition != null)
                {
                    totalRimThickness += rimOuterTransitionThickness = rimConfig.OuterTransition.Thickness;
                }
            }

            // times 2 because on both sides, div by 8 because 8 squares.
            int squareSpaceWidth = (int)(chessBoardPanel.Width / (1.0f + totalRimThickness * 2.0f / 8.0f));
            int squareSpaceHeight = (int)(chessBoardPanel.Height / (1.0f + totalRimThickness * 2.0f / 8.0f));

            int squareW = squareSpaceWidth / 8;
            int squareH = squareSpaceHeight / 8;

            squareSpaceWidth = squareW * 8;
            squareSpaceHeight = squareH * 8;

            int actualRimThickness = (int)(squareW * rimThickness);
            int actualRimInnerTransitionThickness = (int)(squareW * rimInnerTransitionThickness);
            int actualRimOuterTransitionThickness = (int)(squareW * rimOuterTransitionThickness);
            int actualTotalRimThickness =
                actualRimThickness
                + actualRimInnerTransitionThickness
                + actualRimOuterTransitionThickness;

            int squaresSpaceX = actualTotalRimThickness;
            int squaresSpaceY = actualTotalRimThickness;

            return new DrawingSpaceUsage
            {
                SquaresSpace = new Rectangle(squaresSpaceX, squaresSpaceY, squareSpaceWidth, squareSpaceHeight),
                RimThickness = actualRimThickness,
                InnerRimTransitionThickness = actualRimInnerTransitionThickness,
                OuterRimTransitionThickness = actualRimOuterTransitionThickness
            };
        }

        private Rectangle GetChessBoardSquaresSpace()
        {
            return GetDrawingSpaceUsage().SquaresSpace;
        }

        private Rectangle GetSquareHitbox(int file, int rank, Rectangle squaresSpace)
        {
            if (IsBoardFlipped)
            {
                file = 7 - file;
                rank = 7 - rank;
            }

            var squaresRect = GetChessBoardSquaresSpace();

            int sw = squaresRect.Width / 8;
            int sh = squaresRect.Height / 8;

            int x = squaresRect.X + sw * file;
            int y = squaresRect.Y + sh * rank;

            return new Rectangle(x, y, sw, sh);
        }

        private Position ConvertPointToSquare(Point point)
        {
            var squaresRect = GetChessBoardSquaresSpace();

            if (!squaresRect.Contains(point))
            {
                return null;
            }

            int w = squaresRect.Width;
            int h = squaresRect.Height;

            int sw = w / 8;
            int sh = h / 8;

            int x = (point.X - squaresRect.X) / sw;
            int y = (point.Y - squaresRect.Y) / sh;

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

        private Rectangle EnlargeRect(Rectangle rect, int d)
        {
            return new Rectangle(rect.X - d, rect.Y - d, rect.Width + 2 * d, rect.Height + 2 * d);
        }

        private void DrawSquare(Graphics g, Piece piece, int file, int rank, Rectangle squaresSpace)
        {
            var squareRect = GetSquareHitbox(file, rank, squaresSpace);
            var pieceRect = GetSquareHitbox(file, rank, squaresSpace);

            if (MouseFrom.HasValue)
            {
                Position fromSquare = ConvertPointToSquare(MouseFrom.Value);
                if (fromSquare != null && (int)fromSquare.File == file && fromSquare.Rank == 8 - rank)
                {
                    int dx = LastMousePosition.X - MouseFrom.Value.X;
                    int dy = LastMousePosition.Y - MouseFrom.Value.Y;
                    pieceRect.X += dx;
                    pieceRect.Y += dy;
                }
            }

            var biggerRect = EnlargeRect(squareRect, 2);

            bool isLightSquare = (file + rank) % 2 == 0;

            var squareImg =
                isLightSquare
                ? BoardImages.LightSquare
                : BoardImages.DarkSquare;

            g.DrawImage(squareImg, squareRect);

            if (piece != null)
            {
                var pieceImg = PieceImages.GetImageForPiece(piece);
                g.DrawImage(pieceImg, pieceRect);
            }
        }

        private void DrawSquares(Graphics g, Rectangle squaresSpace)
        {
            var game = BoardHistory.Current();
            var board = game.GetBoard();

            // We draw the square from which a piece is dragged last
            // so that it's not occluded by others.

            Position fromSquare =
                MouseFrom.HasValue
                ? ConvertPointToSquare(MouseFrom.Value)
                : null;

            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    if (fromSquare != null && (int)fromSquare.File == x && fromSquare.Rank == 8 - y)
                    {
                        // this draw is deferred
                        continue;
                    }
                    
                    Piece piece = board[y][x];
                    DrawSquare(g, piece, x, y, squaresSpace);
                }
            }

            if (fromSquare != null)
            {
                int x = (int)fromSquare.File;
                int y = 8 - fromSquare.Rank;

                Piece piece = board[y][x];
                DrawSquare(g, piece, x, y, squaresSpace);
            }
        }

        private void ChessBoardPanel_Paint(object sender, PaintEventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            if (BoardImages == null || PieceImages == null)
            {
                return;
            }

            Graphics g = e.Graphics;

            var space = GetDrawingSpaceUsage();

            DrawSquares(g, space.SquaresSpace);
            DrawRim(g, space);

            DrawIndicators(g, space);
        }

        private void DrawIndicators(Graphics g, DrawingSpaceUsage space)
        {
            var rimConfig = BoardImages.Config.Rim;

            if (rimConfig == null)
            {
                DrawIndicatorsOnSquares(g, space);
            }
            else
            {
                DrawIndicatorsOnRim(g, space);
            }
        }

        private void DrawIndicatorsOnRim(Graphics g, DrawingSpaceUsage space)
        {
            var rimConfig = BoardImages.Config.Rim;
            if (rimConfig == null)
            {
                return;
            }

            var rankIndicatorSides = new List<BoardTheme.RimSide>();
            if (rimConfig.IndicatorsOnBothSides)
            {
                rankIndicatorSides.Add(BoardTheme.RimSide.Left);
                rankIndicatorSides.Add(BoardTheme.RimSide.Right);
            }
            else
            {
                rankIndicatorSides.Add(
                    BoardImages.Config.Indicators.RelativeFile == 0
                    ? BoardTheme.RimSide.Left
                    : BoardTheme.RimSide.Right
                    );
            }

            var fileIndicatorSides = new List<BoardTheme.RimSide>();
            if (rimConfig.IndicatorsOnBothSides)
            {
                fileIndicatorSides.Add(BoardTheme.RimSide.Bottom);
                fileIndicatorSides.Add(BoardTheme.RimSide.Top);
            }
            else
            {
                fileIndicatorSides.Add(
                    BoardImages.Config.Indicators.RelativeRank == 0
                    ? BoardTheme.RimSide.Top
                    : BoardTheme.RimSide.Bottom
                    );
            }

            var font = BoardImages.Config.Indicators.Font;

            for (int relativeFile = 0; relativeFile < 8; ++relativeFile)
            {
                foreach (var side in fileIndicatorSides)
                {
                    int relativeRank = side == BoardTheme.RimSide.Top ? 0 : 7;

                    int file =
                        IsBoardFlipped
                        ? 7 - relativeFile
                        : relativeFile;

                    int rank =
                        IsBoardFlipped
                        ? 7 - relativeRank
                        : relativeRank;

                    bool isLightSquare = (file + rank) % 2 == 1;

                    var rect = GetRimSideHitbox(file, rank, side, space);

                    var brush =
                        isLightSquare
                        ? BoardImages.Config.Indicators.LightSquareBrush
                        : BoardImages.Config.Indicators.DarkSquareBrush;

                    var text = string.Empty + BoardImages.Config.Indicators.FileIndicators[relativeFile];
                    g.DrawString(
                        text,
                        font,
                        brush,
                        rect,
                        BoardImages.Config.Indicators.FileIndicatorFormat
                        );
                }
            }

            for (int relativeRank = 0; relativeRank < 8; ++relativeRank)
            {
                foreach (var side in rankIndicatorSides)
                {
                    int relativeFile = side == BoardTheme.RimSide.Left ? 0 : 7;

                    int file =
                        IsBoardFlipped
                        ? 7 - relativeFile
                        : relativeFile;

                    int rank =
                        IsBoardFlipped
                        ? 7 - relativeRank
                        : relativeRank;

                    bool isLightSquare = (file + rank) % 2 == 1;

                    var rect = GetRimSideHitbox(file, rank, side, space);

                    var brush =
                        isLightSquare
                        ? BoardImages.Config.Indicators.LightSquareBrush
                        : BoardImages.Config.Indicators.DarkSquareBrush;

                    var text = string.Empty + BoardImages.Config.Indicators.RankIndicators[7 - relativeRank];
                    g.DrawString(
                        text,
                        font,
                        brush,
                        rect,
                        BoardImages.Config.Indicators.FileIndicatorFormat
                        );
                }
            }
        }

        private Rectangle GetRimSideHitbox(int file, int rank, BoardTheme.RimSide side, DrawingSpaceUsage space)
        {
            bool isHorizontal = side == BoardTheme.RimSide.Bottom || side == BoardTheme.RimSide.Top;

            int squareW = space.SquaresSpace.Width / 8;
            int squareH = space.SquaresSpace.Height / 8;
            int rimThickness = space.RimThickness;
            int innerRimTransitionThickness = space.InnerRimTransitionThickness;

            int x = 0;
            int y = 0;
            int w = 0;
            int h = 0;

            if (side == BoardTheme.RimSide.Left)
            {
                y = space.SquaresSpace.Y;
                x = space.SquaresSpace.X - rimThickness - space.InnerRimTransitionThickness;
                w = rimThickness;
                h = squareH;
            }
            else if (side == BoardTheme.RimSide.Right)
            {
                y = space.SquaresSpace.Y;
                x = space.SquaresSpace.X + space.SquaresSpace.Width + space.InnerRimTransitionThickness;
                w = rimThickness;
                h = squareH;
            }
            else if (side == BoardTheme.RimSide.Top)
            {
                y = space.SquaresSpace.Y - rimThickness - space.InnerRimTransitionThickness;
                x = space.SquaresSpace.X;
                w = squareW;
                h = rimThickness;
            }
            else if (side == BoardTheme.RimSide.Bottom)
            {
                y = space.SquaresSpace.Y + space.SquaresSpace.Height + space.InnerRimTransitionThickness;
                x = space.SquaresSpace.X;
                w = squareW;
                h = rimThickness;
            }

            int xIncr = isHorizontal ? squareW : 0;
            int yIncr = !isHorizontal ? squareH : 0;
            
            return new Rectangle(x + file * xIncr, y + rank * yIncr, w, h);
        }

        private void DrawIndicatorsOnSquares(Graphics g, DrawingSpaceUsage space)
        {
            var indicatorFile =
                IsBoardFlipped
                ? 7 - BoardImages.Config.Indicators.RelativeFile
                : BoardImages.Config.Indicators.RelativeFile;

            var indicatorRank =
                IsBoardFlipped
                ? 7 - BoardImages.Config.Indicators.RelativeRank
                : BoardImages.Config.Indicators.RelativeRank;

            var font = BoardImages.Config.Indicators.Font;

            for (int file = 0; file < 8; ++file)
            {
                var rank = indicatorRank;
                var squareRect = GetSquareHitbox(file, rank, space.SquaresSpace);
                var biggerRect = EnlargeRect(squareRect, 2);

                bool isLightSquare = (file + rank) % 2 == 0;

                var brush =
                    isLightSquare
                    ? BoardImages.Config.Indicators.LightSquareBrush
                    : BoardImages.Config.Indicators.DarkSquareBrush;

                var text = string.Empty + BoardImages.Config.Indicators.FileIndicators[file];
                g.DrawString(
                    text,
                    font,
                    brush,
                    biggerRect,
                    BoardImages.Config.Indicators.FileIndicatorFormat
                    );
            }

            for (int rank = 0; rank < 8; ++rank)
            {
                var file = indicatorFile;
                var squareRect = GetSquareHitbox(file, rank, space.SquaresSpace);
                var biggerRect = EnlargeRect(squareRect, 2);

                bool isLightSquare = (file + rank) % 2 == 0;

                var brush =
                    isLightSquare
                    ? BoardImages.Config.Indicators.LightSquareBrush
                    : BoardImages.Config.Indicators.DarkSquareBrush;

                if (file == indicatorFile)
                {
                    var text = string.Empty + BoardImages.Config.Indicators.RankIndicators[7 - rank];
                    g.DrawString(
                        text,
                        font,
                        brush,
                        biggerRect,
                        BoardImages.Config.Indicators.RankIndicatorFormat
                        );
                }
            }
        }

        private Rectangle[] GetRimTransitionRectangles(Rectangle squaresRectangle, int distance, int thickness)
        {
            Rectangle[] rects = new Rectangle[4];

            int offset = distance + thickness;
            int minX = squaresRectangle.X - offset;
            int minY = squaresRectangle.Y - offset;
            int maxX = squaresRectangle.X + squaresRectangle.Width + distance;
            int maxY = squaresRectangle.Y + squaresRectangle.Height + distance;
            int w = maxX - minX + thickness;
            int h = maxY - minY + thickness;

            rects[0] = new Rectangle(minX, minY, w, thickness);
            rects[1] = new Rectangle(minX, minY, thickness, h);

            rects[2] = new Rectangle(minX, maxY, w, thickness);
            rects[3] = new Rectangle(maxX, minY, thickness, h);

            return rects;
        }

        private Rectangle[] GetInnerRimTransitionRectangles(DrawingSpaceUsage space)
        {
            return GetRimTransitionRectangles(space.SquaresSpace, 0, space.InnerRimTransitionThickness);
        }

        private Rectangle[] GetOuterRimTransitionRectangles(DrawingSpaceUsage space)
        {
            return GetRimTransitionRectangles(
                space.SquaresSpace, 
                space.InnerRimTransitionThickness + space.RimThickness, 
                space.OuterRimTransitionThickness
                );
        }

        private void DrawRim(Graphics g, DrawingSpaceUsage space)
        {
            var rimConfig = BoardImages.Config.Rim;
            if (rimConfig == null)
            {
                return;
            }

            DrawRimSquares(g, space);

            var innerTransitionRects = GetInnerRimTransitionRectangles(space);
            g.FillRectangles(new SolidBrush(rimConfig.InnerTransition.Color), innerTransitionRects);

            var outerTransitionRects = GetOuterRimTransitionRectangles(space);
            g.FillRectangles(new SolidBrush(rimConfig.OuterTransition.Color), outerTransitionRects);
        }

        private void DrawRimSquares(Graphics g, DrawingSpaceUsage space)
        {
            DrawRimSquaresSide(g, space, BoardTheme.RimSide.Left);
            DrawRimSquaresSide(g, space, BoardTheme.RimSide.Right);
            DrawRimSquaresSide(g, space, BoardTheme.RimSide.Top);
            DrawRimSquaresSide(g, space, BoardTheme.RimSide.Bottom);

            DrawRimSquaresCorner(g, space, BoardTheme.RimCorner.TopLeft);
            DrawRimSquaresCorner(g, space, BoardTheme.RimCorner.TopRight);
            DrawRimSquaresCorner(g, space, BoardTheme.RimCorner.BottomLeft);
            DrawRimSquaresCorner(g, space, BoardTheme.RimCorner.BottomRight);
        }

        private void DrawRimSquaresCorner(Graphics g, DrawingSpaceUsage space, BoardTheme.RimCorner corner)
        {
            bool isSquareLight = corner == BoardTheme.RimCorner.BottomRight || corner == BoardTheme.RimCorner.TopLeft;

            int rimThickness = space.RimThickness + space.InnerRimTransitionThickness;

            int x = 0;
            int y = 0;

            if (corner == BoardTheme.RimCorner.TopLeft)
            {
                y = space.SquaresSpace.Y - rimThickness;
                x = space.SquaresSpace.X - rimThickness;
            }
            else if (corner == BoardTheme.RimCorner.TopRight)
            {
                y = space.SquaresSpace.Y - rimThickness;
                x = space.SquaresSpace.X + space.SquaresSpace.Width;
            }
            else if (corner == BoardTheme.RimCorner.BottomLeft)
            {
                y = space.SquaresSpace.Y + space.SquaresSpace.Height;
                x = space.SquaresSpace.X - rimThickness;
            }
            else if (corner == BoardTheme.RimCorner.BottomRight)
            {
                y = space.SquaresSpace.Y + space.SquaresSpace.Height;
                x = space.SquaresSpace.X + space.SquaresSpace.Width;
            }

            var image =
                isSquareLight
                ? BoardImages.LightRimCorner[corner]
                : BoardImages.DarkRimCorner[corner];

            var rect = new Rectangle(x, y, rimThickness, rimThickness);

            g.DrawImage(image, rect);
        }

        private void DrawRimSquaresSide(Graphics g, DrawingSpaceUsage space, BoardTheme.RimSide side)
        {
            bool isHorizontal = side == BoardTheme.RimSide.Bottom || side == BoardTheme.RimSide.Top;
            bool isSquareLight = side == BoardTheme.RimSide.Bottom || side == BoardTheme.RimSide.Right;

            int squareW = space.SquaresSpace.Width / 8;
            int squareH = space.SquaresSpace.Height / 8;
            int rimThickness = space.RimThickness + space.InnerRimTransitionThickness;

            int x = 0;
            int y = 0;
            int w = 0;
            int h = 0;

            if (side == BoardTheme.RimSide.Left)
            {
                y = space.SquaresSpace.Y;
                x = space.SquaresSpace.X - rimThickness;
                w = rimThickness;
                h = squareH;
            }
            else if (side == BoardTheme.RimSide.Right)
            {
                y = space.SquaresSpace.Y;
                x = space.SquaresSpace.X + space.SquaresSpace.Width;
                w = rimThickness;
                h = squareH;
            }
            else if (side == BoardTheme.RimSide.Top)
            {
                y = space.SquaresSpace.Y - rimThickness;
                x = space.SquaresSpace.X;
                w = squareW;
                h = rimThickness;
            }
            else if (side == BoardTheme.RimSide.Bottom)
            {
                y = space.SquaresSpace.Y + space.SquaresSpace.Height;
                x = space.SquaresSpace.X;
                w = squareW;
                h = rimThickness;
            }

            int xIncr = isHorizontal ? squareW : 0;
            int yIncr = !isHorizontal ? squareH : 0;
                
            for (int i = 0; i < 8; ++i)
            {
                var image =
                    isSquareLight
                    ? BoardImages.LightRimSide[side]
                    : BoardImages.DarkRimSide[side];

                var rect = new Rectangle(x, y, w, h);

                g.DrawImage(image, rect);

                isSquareLight = !isSquareLight;
                x += xIncr;
                y += yIncr;
            }
        }

        private void ChessBoard_Load(object sender, EventArgs e)
        {
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

        public bool DoMove(Move move, bool silent = false)
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
            if (fromSquare == null || toSquare == null)
            {
                return false;
            }

            Player player = BoardHistory.Current().GCD.WhoseTurn;
            Move move = new Move(fromSquare, toSquare, player);
            if (BoardHistory.NeedsToBePromotion(move))
            {
                using (var dialog = new PromotionSelectionForm(PieceImages, player))
                {
                    var result = dialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        move.Promotion = dialog.PromotedPieceType;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

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

            chessBoardPanel.Refresh();
        }

        private void ChessBoardPanel_MouseUp(object sender, MouseEventArgs e)
        {
            MouseTo = new Point(e.X, e.Y);
            if (MouseFrom == null || MouseTo == null)
            {
                return;
            }

            TryDragPiece(MouseFrom.Value, MouseTo.Value);

            MouseFrom = null;
            MouseTo = null;

            chessBoardPanel.Refresh();
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

        public string GetPrevMoveEran()
        {
            var e = BoardHistory.Prev();
            if (e == null)
            {
                return null;
            }

            return Eran.MakeFromBoardAndMove(
                new ChessGame(e.GCD),
                BoardHistory.Current().Move
                );
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

        private void chessBoardPanel_MouseMove(object sender, MouseEventArgs e)
        {
            LastMousePosition = new Point(e.Location.X, e.Location.Y);
            if (MouseFrom.HasValue)
            {
                chessBoardPanel.Refresh();
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
