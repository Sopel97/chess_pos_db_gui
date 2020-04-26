using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui.src.chess
{
    public class ReverseMoveWithEran
    {
        public ReverseMove ReverseMove { get; set; }
        public string Eran { get; set; }

        public ReverseMoveWithEran(ReverseMove move, string eran)
        {
            ReverseMove = move;
            Eran = eran;
        }

        public override string ToString()
        {
            return Eran;
        }
    }
}
