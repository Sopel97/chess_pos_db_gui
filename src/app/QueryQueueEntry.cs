namespace chess_pos_db_gui
{
    public class QueryQueueEntry
    {
        public string Sig { get; private set; }
        public string Fen { get; private set; }
        public string San { get; private set; }
        public string CurrentFen { get; private set; }
        public bool QueryEval { get; private set; }

        public QueryQueueEntry(ChessBoard chessBoard, bool queryEval)
        {
            San = chessBoard.GetLastMoveSan();
            Fen = San == "--"
                ? chessBoard.GetFen()
                : chessBoard.GetPrevFen();

            CurrentFen = chessBoard.GetFen();

            Sig = Fen + San;

            QueryEval = queryEval;
        }

        public override int GetHashCode()
        {
            return Sig.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == this.GetType())
            {
                return Sig.Equals(((QueryQueueEntry)obj).Sig);
            }
            return false;
        }
    }

}
