namespace chess_pos_db_gui
{
    public class QueryQueueEntry
    {
        public string QueryFen { get; private set; }
        public string CurrentFen { get; private set; }
        public string San { get; private set; }
        public bool QueryEval { get; private set; }

        public string Signature
        {
            get
            {
                return QueryFen + San;
            }
        }

        public QueryQueueEntry(ChessBoard chessBoard, bool queryEval)
        {
            QueryFen = 
                San == chess_pos_db_gui.San.NullMove
                ? chessBoard.GetFen()
                : chessBoard.GetPrevFen();

            CurrentFen = chessBoard.GetFen();

            San = chessBoard.GetLastMoveSan();

            QueryEval = queryEval;
        }

        public override int GetHashCode()
        {
            return Signature.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == GetType())
            {
                return Signature.Equals(((QueryQueueEntry)obj).Signature);
            }
            return false;
        }
    }

}
