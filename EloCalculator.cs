using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui
{
    class EloCalculator
    {
        public static double GetExpectedPerformance(double eloDiff)
        {
            return 1.0 / (1.0 + Math.Pow(10.0, -eloDiff / 400.0));
        }

        public static double GetExpectedPerformance(double whiteElo, double blackElo)
        {
            return GetExpectedPerformance(whiteElo - blackElo);
        }
    }
}
