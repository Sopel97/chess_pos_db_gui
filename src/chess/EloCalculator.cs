﻿using System;

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

        public static double GetEloFromPerformance(double perf)
        {
            return -400.0 * Math.Log((1.0 - perf) / perf) / Math.Log(10.0);
        }

        public static double GetAdjustedPerformance(double actualPerf, double expectedPerf, double maxEloDiff = double.PositiveInfinity)
        {
            var actualElo = Clamp(GetEloFromPerformance(actualPerf), maxEloDiff);
            var expectedElo = Clamp(GetEloFromPerformance(expectedPerf), maxEloDiff);
            return GetExpectedPerformance(actualElo - expectedElo);
        }

        /*
         * elo error formula: http://talkchess.com/forum3/viewtopic.php?p=645304#p645304
         * 
         * s(p) = sqrt([p*(1 - p) - draw_ratio/4]/(N - 1))
         * z = 2,58 (for 99% confidence) (would be 2 for 95% confidence)
         *
         * elo_error = 1600 * z * s(H_Perf) / ln(10)
         */
        private static double ComputeS(double p, double drawRatio, double total)
        {
            double safePerf = 0.15;
            p = Math.Max(safePerf, Math.Min(1.0 - safePerf, p));
            var v = p * (1.0 - p);
            // return Math.Sqrt((v - drawRatio / 4.0) / (total - 1.0)); // returns 0 when drawRatio == 1... that's bad...
            return Math.Sqrt(v / (total - 1.0));
        }

        public static double EloError99pct(ulong engineWins, ulong engineDraws, ulong engineLosses, ulong? lowN = null)
        {
            double total = engineWins + engineDraws + engineLosses;

            if (total < 2)
            {
                return double.PositiveInfinity;
            }

            double drawRatio = engineDraws / total;

            double z = 2.58;
            double perf = (engineWins + engineDraws * 0.5) / total;

            double error = 1600.0 * z * ComputeS(perf, drawRatio, total) / Math.Log(10);

            if (lowN.HasValue)
            {
                double d = lowN.Value / 2.0;
                error /= 1.0 - Math.Exp(-total / d);
            }

            return error;
        }

        public static double Clamp(double elo, double max)
        {
            return Math.Min(Math.Max(elo, -max), max);
        }
    }
}
