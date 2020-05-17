using chess_pos_db_gui.src.util;
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
            public bool UseEval { get; set; }
            public bool UseCount { get; set; }

            public double EvalWeight { get; set; }
            public double GamesWeight { get; set; }
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
            EnumArray<GameLevel, AggregatedEntry> aggregatedEntries,
            ChessDBCNScore score,
            Options options
            )
        {
            const long maxAllowedEloDiff = 400;
            const double minPerf = 0.01;

            // if there's less than this amount of games then the goodness contribution will be penalized.
            ulong penaltyFromCountThreshold = 1;

            bool useAnyGames = aggregatedEntries.Any(e => e.Value.Count != 0);
            bool useEval = options.UseEval;
            bool useCount = options.UseCount;

            AggregatedEntry totalEntry = new AggregatedEntry();
            foreach(KeyValuePair<GameLevel, AggregatedEntry> e in aggregatedEntries)
            {
                totalEntry.Combine(e.Value);
            }

            if (useAnyGames && totalEntry.Count == 0)
            {
                return 0.0;
            }

            if (useEval && score == null && Math.Abs(totalEntry.TotalEloDiff / (long)totalEntry.Count) > maxAllowedEloDiff)
            {
                return 0.0;
            }

            double gamesWeight = useAnyGames ? options.GamesWeight : 0.0;
            double evalWeight = useEval ? options.EvalWeight : 0.0;

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

            double adjustedGamesPerf = calculateAdjustedPerf(totalEntry);

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
                adjustedGamesPerf = penalizePerf(adjustedGamesPerf, totalEntry.Count);
            }

            double gamesGoodness = Math.Pow(adjustedGamesPerf, gamesWeight);

            // If eval is not present then assume 0.5 but reduce it for moves with low game count.
            // The idea is that we don't want missing eval to penalize common moves.
            double evalGoodness =
                Math.Pow(
                    score != null
                    ? score.Perf
                    : EloCalculator.GetExpectedPerformance(
                        -EloCalculator.EloError99pct(
                            totalEntry.WinCount, 
                            totalEntry.DrawCount, 
                            totalEntry.LossCount
                            )
                        )
                    , evalWeight);

            double weightSum = gamesWeight + evalWeight;

            double goodness = Math.Pow(gamesGoodness * evalGoodness, 1.0 / weightSum);

            return goodness;
        }
    }
}
