using ChessDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui.src.app
{
    public static class GoodnessCalculator
    {
        public class Options
        {
            public bool UseHumanGames { get; set; }
            public bool UseEngineGames { get; set; }
            public bool UseEval { get; set; }
            public bool UseCombinedGames { get; set; }
            public bool CombineGames { get; set; }
            public bool UseCount { get; set; }

            public double HumanWeight { get; set; }
            public double EngineWeight { get; set; }
            public double EvalWeight { get; set; }
            public double CombinedGamesWeight { get; set; }
        }

        /*
         * W - number of wins
         * D - number of draws
         * L - number of losses
         *
         * Ev_Perf - performance estimate based on eval (between 0 and 1)
         * H_Perf - human performance (between 0 and 1)
         * E_Perf - engine performance (between 0 and 1)
         * 
         * expected_perf - performance expected from elo diff
         *
         * H_weight - weight for human games
         * E_weight - weight for engine games
         * Eval_weight - weight for eval
         * Count_confidence - whether elo_error is to be used (boolean)
         *
         * elo error formula: http://talkchess.com/forum3/viewtopic.php?p=645304#p645304
         *
         * N = W+D+L
         * draw_ratio = D/N
         * s(p) = sqrt([p*(1 - p) - draw_ratio/4]/(N - 1))
         * z = 2,58 (for 99% confidence) (would be 2 for 95% confidence)
         *
         * H_elo_error = 1600 * z * s(H_Perf) / ln(10)
         * E_elo_error = 1600 * z * s(E_Perf) / ln(10)
         *
         * if eval not provided:
         *     Eval_weight = 0
         *
         * if Count_confidence:
         *     H_Perf = Perf(Elo(H_Perf) - H_elo_error)
         *     E_Perf = Perf(Elo(E_Perf) - E_elo_error)
         *     
         * H_APerf = AdjustPerf(H_Perf, expected_perf)
         * E_APerf = AdjustPerf(E_Perf, expected_perf)
         *
         * weight_sum = H_weight + E_weight + Eval_weight
         * a = pow(H_APerf, H_weight)
         * b = pow(E_APerf, A_weight)
         * c = pow(Ev_Perf, Eval_weight)
         * goodness = pow(a*b*c, 1.0/weight_sum)
         */
        public static double CalculateGoodness(
            Player sideToMove,
            AggregatedEntry entry, 
            AggregatedEntry nonEngineEntry, 
            ChessDBCNScore score,
            Options options
            )
        {
            long maxAllowedEloDiff = 400;

            // if there's less than this amount of games then the goodness contribution will be penalized.
            ulong penaltyFromCountThreshold = 1;

            bool useHumanWeight = options.UseHumanGames && !options.CombineGames;
            bool useEngineWeight = options.UseEngineGames && !options.CombineGames;
            bool useTotalWeight = options.UseCombinedGames && options.CombineGames;
            bool useAnyGames = useHumanWeight || useEngineWeight || useTotalWeight;
            bool useEvalWeight = options.UseEval;
            bool useCount = options.UseCount;

            // return 0 if no games being considered
            ulong usedGameCount = 0;
            ulong usedWins = 0;
            ulong usedDraws = 0;
            ulong usedLosses = 0;
            if (useTotalWeight)
            {
                usedGameCount += entry.Count;
                usedWins += entry.WinCount;
                usedDraws += entry.DrawCount;
                usedLosses += entry.LossCount;
            }
            if (useHumanWeight)
            {
                usedGameCount += nonEngineEntry.Count;
                usedWins += nonEngineEntry.WinCount;
                usedDraws += nonEngineEntry.DrawCount;
                usedLosses += nonEngineEntry.LossCount;
            }
            if (useEngineWeight)
            {
                usedGameCount += entry.Count - nonEngineEntry.Count;
                usedWins += entry.WinCount - nonEngineEntry.WinCount;
                usedDraws += entry.DrawCount - nonEngineEntry.DrawCount;
                usedLosses += entry.LossCount - nonEngineEntry.LossCount;
            }
            if (useAnyGames && usedGameCount == 0)
            {
                return 0.0;
            }

            if (useEvalWeight && score == null && Math.Abs(entry.EloDiff / (long)entry.Count) > maxAllowedEloDiff)
            {
                return 0.0;
            }

            double humanWeight = useHumanWeight ? (double)options.HumanWeight : 0.0;
            double engineWeight = useEngineWeight ? (double)options.EngineWeight : 0.0;
            double totalWeight = useTotalWeight ? (double)options.CombinedGamesWeight : 0.0;
            double evalWeight = useEvalWeight ? (double)options.EvalWeight : 0.0;

            ulong totalCount = entry.Count;
            double adjustedTotalPerf = 1.0f;
            if (totalCount > 0)
            {
                ulong totalWins = entry.WinCount;
                ulong totalDraws = entry.DrawCount;
                ulong totalLosses = totalCount - totalWins - totalDraws;
                double totalPerf = (totalWins + totalDraws * 0.5) / totalCount;
                double totalEloError = EloCalculator.EloError99pct(totalWins, totalDraws, totalLosses);
                double expectedTotalPerf = EloCalculator.GetExpectedPerformance((entry.EloDiff) / (double)totalCount);
                if (sideToMove == Player.Black)
                {
                    totalPerf = 1.0 - totalPerf;
                    expectedTotalPerf = 1.0 - expectedTotalPerf;
                }
                if (useCount)
                {
                    totalPerf = EloCalculator.GetExpectedPerformance(EloCalculator.GetEloFromPerformance(totalPerf) - totalEloError);
                }
                adjustedTotalPerf = EloCalculator.GetAdjustedPerformance(totalPerf, expectedTotalPerf);
            }

            double adjustedEnginePerf = 1.0f;
            ulong engineCount = entry.Count - nonEngineEntry.Count;
            if (engineCount > 0)
            {
                ulong engineWins = entry.WinCount - nonEngineEntry.WinCount;
                ulong engineDraws = entry.DrawCount - nonEngineEntry.DrawCount;
                ulong engineLosses = engineCount - engineWins - engineDraws;
                double enginePerf = (engineWins + engineDraws * 0.5) / engineCount;
                double engineEloError = EloCalculator.EloError99pct(engineWins, engineDraws, engineLosses);
                double expectedEnginePerf = EloCalculator.GetExpectedPerformance((entry.EloDiff - nonEngineEntry.EloDiff) / (double)engineCount);
                if (sideToMove == Player.Black)
                {
                    enginePerf = 1.0 - enginePerf;
                    expectedEnginePerf = 1.0 - expectedEnginePerf;
                }
                if (useCount)
                {
                    enginePerf = EloCalculator.GetExpectedPerformance(EloCalculator.GetEloFromPerformance(enginePerf) - engineEloError);
                }
                adjustedEnginePerf = EloCalculator.GetAdjustedPerformance(enginePerf, expectedEnginePerf);
            }

            double adjustedHumanPerf = 1.0f;
            ulong humanCount = nonEngineEntry.Count;
            if (humanCount > 0)
            {
                ulong humanWins = nonEngineEntry.WinCount;
                ulong humanDraws = nonEngineEntry.DrawCount;
                ulong humanLosses = humanCount - humanWins - humanDraws;
                double humanPerf = (humanWins + humanDraws * 0.5) / humanCount;
                double humanEloError = EloCalculator.EloError99pct(humanWins, humanDraws, humanLosses);
                double expectedHumanPerf = EloCalculator.GetExpectedPerformance(nonEngineEntry.EloDiff / (double)humanCount);
                if (sideToMove == Player.Black)
                {
                    humanPerf = 1.0 - humanPerf;
                    expectedHumanPerf = 1.0 - expectedHumanPerf;
                }
                if (useCount)
                {
                    humanPerf = EloCalculator.GetExpectedPerformance(EloCalculator.GetEloFromPerformance(humanPerf) - humanEloError);
                }
                adjustedHumanPerf = EloCalculator.GetAdjustedPerformance(humanPerf, expectedHumanPerf);
            }

            double minPerf = 0.01;

            double penalizePerf(double perf, double numGames)
            {
                if (numGames >= penaltyFromCountThreshold)
                {
                    return perf;
                }

                if (numGames == 0)
                {
                    return minPerf;
                }

                double r = ((numGames + 1.0) / (penaltyFromCountThreshold + 1));
                double penalty = 1 - Math.Log(r);
                return Math.Max(minPerf, perf / penalty);
            }

            if (useCount)
            {
                adjustedEnginePerf = penalizePerf(adjustedEnginePerf, engineCount);
                adjustedHumanPerf = penalizePerf(adjustedHumanPerf, humanCount);
                adjustedTotalPerf = penalizePerf(adjustedTotalPerf, totalCount);
            }

            double engineGoodness = Math.Pow(adjustedEnginePerf, engineWeight);
            double humanGoodness = Math.Pow(adjustedHumanPerf, humanWeight);
            double totalGoodness = Math.Pow(adjustedTotalPerf, totalWeight);
            // if eval is not present then assume 0.5 but penalize moves with low game count
            double evalGoodness =
                Math.Pow(
                    score != null
                    ? score.Perf
                    : EloCalculator.GetExpectedPerformance(-EloCalculator.EloError99pct(usedWins, usedDraws, usedLosses))
                    , evalWeight);

            double weightSum = engineWeight + humanWeight + totalWeight + evalWeight;

            double goodness = Math.Pow(engineGoodness * humanGoodness * totalGoodness * evalGoodness, 1.0 / weightSum);

            return goodness;
        }
    }
}
