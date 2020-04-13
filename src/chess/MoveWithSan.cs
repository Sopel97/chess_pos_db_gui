
using ChessDotNet;

namespace chess_pos_db_gui
{
    public class MoveWithSan
    {
        public Move Move { get; set; }
        public string San { get; set; }

        public MoveWithSan(Move move, string san)
        {
            Move = move;
            San = san;
        }

        public override string ToString()
        {
            return San;
        }
    }

}
