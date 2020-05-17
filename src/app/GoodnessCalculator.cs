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
            public double DrawScore { get; internal set; }
        }

        /*
         * This is the general idea, it is slightly modified at
         * corner cases and extended for combined games.
         * 
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
            const long maxAllowedEloDiff = 400;
            const double minPerf = 0.01;

            // if there's less than this amount of games then the goodness contribution will be penalized.
            ulong penaltyFromCountThreshold = 1;

            bool useHumanWeight = options.UseHumanGames && !options.CombineGames;
            bool useEngineWeight = options.UseEngineGames && !options.CombineGames;
            bool useTotalWeight = options.UseCombinedGames && options.CombineGames;
            bool useAnyGames = useHumanWeight || useEngineWeight || useTotalWeight;
            bool useEvalWeight = options.UseEval;
            bool useCount = options.UseCount;

            AggregatedEntry engineEntry = entry - nonEngineEntry;

            // return 0 if no games being considered
            ulong usedGameCount = 0;
            ulong usedWins = 0;
            ulong usedDraws = 0;
            ulong usedLosses = 0;
            void addEntry(AggregatedEntry e)
            {
                usedGameCount += e.Count;
                usedWins += e.WinCount;
                usedDraws += e.DrawCount;
                usedLosses += e.LossCount;
            }

            if (useTotalWeight)
            {
                addEntry(entry);
            }
            if (useHumanWeight)
            {
                addEntry(nonEngineEntry);
            }
            if (useEngineWeight)
            {
                addEntry(engineEntry);
            }

            if (useAnyGames && usedGameCount == 0)
            {
                return 0.0;
            }

            if (useEvalWeight && score == null && Math.Abs(entry.TotalEloDiff / (long)entry.Count) > maxAllowedEloDiff)
            {
                return 0.0;
            }

            double humanWeight = useHumanWeight ? (double)options.HumanWeight : 0.0;
            double engineWeight = useEngineWeight ? (double)options.EngineWeight : 0.0;
            double totalWeight = useTotalWeight ? (double)options.CombinedGamesWeight : 0.0;
            double evalWeight = useEvalWeight ? (double)options.EvalWeight : 0.0;

            double calculateAdjustedPerf(AggregatedEntry e)
            {
                if (e.Count > 0)
                {
                    ulong totalWins = e.WinCount;
                    ulong totalDraws = e.DrawCount;
                    ulong totalLosses = e.Count - totalWins - totalDraws;
                    double totalPerf = (totalWins + totalDraws * options.DrawScore) / e.Count;
                    double totalEloError = EloCalculator.EloError99pct(totalWins, totalDraws, totalLosses);
                    double expectedTotalPerf = EloCalculator.GetExpectedPerformance((e.TotalEloDiff) / (double)e.Count);
                    if (sideToMove == Player.Black)
                    {
                        totalPerf = 1.0 - totalPerf;
                        expectedTotalPerf = 1.0 - expectedTotalPerf;
                    }
                    if (useCount)
                    {
                        totalPerf = EloCalculator.GetExpectedPerformance(EloCalculator.GetEloFromPerformance(totalPerf) - totalEloError);
                    }
                    return EloCalculator.GetAdjustedPerformance(totalPerf, expectedTotalPerf);
                }
                else
                {
                    return 1.0;
                }
            }

            ulong totalCount = entry.Count;
            double adjustedTotalPerf = calculateAdjustedPerf(entry);

            ulong engineCount = engineEntry.Count;
            double adjustedEnginePerf = calculateAdjustedPerf(engineEntry);

            ulong humanCount = nonEngineEntry.Count;
            double adjustedHumanPerf = calculateAdjustedPerf(nonEngineEntry);

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

            // If eval is not present then assume 0.5 but reduce it for moves with low game count.
            // The idea is that we don't want missing eval to penalize common moves.
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
