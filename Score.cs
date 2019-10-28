using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui
{
    class Score
    {
        public int Value { get; set; }
        public double WinPct { get; set; }


        public Score(int v)
        {
            Value = v;
            WinPct = 0;
        }

        public Score(string str)
        {
            Value = int.Parse(str.Split(' ')[0]);
            WinPct = 0;
        }
        public Score(int v, double pct)
        {
            Value = v;
            WinPct = pct;
        }

        public Score(string value, string winpct)
        {
            Value = int.Parse(value.Split(' ')[0]);
            WinPct = double.Parse(winpct, CultureInfo.InvariantCulture) / 100.0;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
