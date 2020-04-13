using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui.src.chess.engine.uci
{
    static class UciUtil
    {
        public static bool IsValidUciMove(string s)
        {
            string files = "abcdefgh";
            string ranks = "12345678";
            string promotions = "qrbk";

            if (s == null)
            {
                return false;
            }

            if (s.Length > 5)
            {
                return false;
            }

            if (s == Lan.NullMove)
            {
                return true;
            }

            if (files.IndexOf(s[0]) == -1)
            {
                return false;
            }

            if (ranks.IndexOf(s[1]) == -1)
            {
                return false;
            }

            if (files.IndexOf(s[2]) == -1)
            {
                return false;
            }

            if (ranks.IndexOf(s[3]) == -1)
            {
                return false;
            }

            if (s.Length == 5)
            {
                return s[3] == '8' && promotions.IndexOf(s[4]) != -1;
            }

            return true;
        }
    }
}
