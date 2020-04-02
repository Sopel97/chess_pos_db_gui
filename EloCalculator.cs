using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui
{
    class EloCalculator
    {
        private static readonly double maxElo = 800.0;

        public static double GetExpectedPerformance(double eloDiff)
        {
            return 1.0 / (1.0 + Math.Pow(10.0, -eloDiff / 400.0));
        }

        public static double GetExpectedPerformance(double whiteElo, double blackElo)
        {
            return GetExpectedPerformance(whiteElo - blackElo);
        }

        public static double GetEloFromPerformance(double perf)
        {
            return Math.Max(-maxElo, Math.Min(maxElo, -400.0 * Math.Log((1.0 - perf) / perf) / Math.Log(10.0)));
        }

        /*
         * s(p) = sqrt([p*(1 - p) - draw_ratio/4]/(N - 1))
         * z = 2,58 (for 99% confidence) (would be 2 for 95% confidence)
         *
         * elo_error = 1600 * z * s(H_Perf) / ln(10)
         */
        private static double s(double p, double drawRatio, double total)
        {
            double safePerf = 0.15;
            p = Math.Max(safePerf, Math.Min(1.0 - safePerf, p));
            var v = p * (1.0 - p);
            return Math.Sqrt((v - drawRatio / 4.0) / (total - 1.0));

        }

        public static double EloError99pct(ulong engineWins, ulong engineDraws, ulong engineLosses)
        {
            double total = (double)(engineWins + engineDraws + engineLosses);

            if (total < 2) return maxElo;

            double drawRatio = (double)engineDraws / total;
           
            double z = 2.58;
            double perf = ((double)engineWins + (double)engineDraws * 0.5) / total;

            return Math.Min(1600.0 * z * s(perf, drawRatio, total) / Math.Log(10), maxElo);
        }
    }
}
