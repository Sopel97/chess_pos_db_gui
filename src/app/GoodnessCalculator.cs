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
            public bool IncreaseErrorBarForLowN { get; set; }

            public double EvalWeight { get; set; }
            public double GamesWeight { get; set; }
            public double DrawScore { get; set; }

            public ulong LowN { get; set; }
        }

        // subtracts totalEloError from expectedElo
        public static double CalculateGoodness(
            Player sideToMove,
            EnumArray<GameLevel, AggregatedEntry> aggregatedEntries,
            ChessDBCNScore score,
            Options options
            )
        {
            const double maxAllowedPlayerEloDiff = 400;
            const double maxCalculatedEloDiff = 800;

            bool useEval = options.EvalWeight > 0.0;
            bool useGames = options.GamesWeight > 0.0;

            AggregatedEntry totalEntry = new AggregatedEntry();
            foreach(KeyValuePair<GameLevel, AggregatedEntry> e in aggregatedEntries)
            {
                totalEntry.Combine(e.Value);
            }

            if (useGames && totalEntry.Count == 0)
            {
                return 0.0;
            }

            if (useEval && score == null && Math.Abs(totalEntry.TotalEloDiff / (double)totalEntry.Count) > maxAllowedPlayerEloDiff)
            {
                return 0.0;
            }

            double gamesWeight = options.GamesWeight;
            double evalWeight = options.EvalWeight;

            double calculateAdjustedPerf(AggregatedEntry e)
            {
                if (e.Count > 0)
                {
                    ulong? lowNThreshold = options.IncreaseErrorBarForLowN ? (ulong?)options.LowN : null;
                    ulong totalWins = e.WinCount;
                    ulong totalDraws = e.DrawCount;
                    ulong totalLosses = e.Count - totalWins - totalDraws;
                    double totalEloError = EloCalculator.Clamp(
                        EloCalculator.EloError99pct(totalWins, totalDraws, totalLosses, lowNThreshold),
                        2 * maxCalculatedEloDiff
                        );
                    double totalPerf = (totalWins + totalDraws * options.DrawScore) / e.Count;
                    double expectedTotalPerf = EloCalculator.GetExpectedPerformance(e.TotalEloDiff / (double)e.Count);
                    if (sideToMove == Player.Black)
                    {
                        totalPerf = 1.0 - totalPerf;
                        expectedTotalPerf = 1.0 - expectedTotalPerf;
                    }
                    double adjustedPerf = EloCalculator.GetAdjustedPerformance(totalPerf, expectedTotalPerf, maxCalculatedEloDiff);
                    double expectedElo = 
                        EloCalculator.Clamp(
                            EloCalculator.Clamp(
                                EloCalculator.GetEloFromPerformance(adjustedPerf),
                                maxCalculatedEloDiff
                                ) - totalEloError, 
                            maxCalculatedEloDiff);
                    adjustedPerf = EloCalculator.GetExpectedPerformance(expectedElo);
                    return adjustedPerf;
                }
                else
                {
                    return 0.0;
                }
            }

            double adjustedGamesPerf = calculateAdjustedPerf(totalEntry);

            double gamesGoodness = EloCalculator.Clamp(
                EloCalculator.GetEloFromPerformance(adjustedGamesPerf),
                maxCalculatedEloDiff
                ) * gamesWeight;

            // If eval is not present then assume 0.5 but reduce it for moves with low game count.
            // The idea is that we don't want missing eval to penalize common moves.
            double evalGoodness =
                (
                    score != null
                    ? EloCalculator.GetEloFromPerformance(score.Perf)
                    : -EloCalculator.EloError99pct(
                            totalEntry.WinCount,
                            totalEntry.DrawCount,
                            totalEntry.LossCount
                        )
                ) * evalWeight;

            double weightSum = gamesWeight + evalWeight;

            double goodness = EloCalculator.GetExpectedPerformance((gamesGoodness + evalGoodness) / weightSum);

            return goodness;
        }
        public class PrioritizeEvalOptions
        {
            public double EvalWeight { get; set; }
            public double GamesWeight { get; set; }
            public double DrawScore { get; set; }

            public double EloErrorHalfWeight { get; set; }
        }

        // multiplies games weight by e^-(elo_error/EloErrorHalfWeight)
        public static double CalculateGoodnessPrioritizeEval(
            Player sideToMove,
            EnumArray<GameLevel, AggregatedEntry> aggregatedEntries,
            ChessDBCNScore score,
            PrioritizeEvalOptions options
            )
        {
            const double maxAllowedPlayerEloDiff = 400;
            const double maxCalculatedEloDiff = 800;

            AggregatedEntry totalEntry = new AggregatedEntry();
            foreach (KeyValuePair<GameLevel, AggregatedEntry> e in aggregatedEntries)
            {
                totalEntry.Combine(e.Value);
            }

            if (score == null && Math.Abs(totalEntry.TotalEloDiff / (double)totalEntry.Count) > maxAllowedPlayerEloDiff)
            {
                return 0.0;
            }

            double evalWeight = options.EvalWeight;

            Tuple<double, double> calculateAdjustedPerf(AggregatedEntry e)
            {
                ulong totalWins = e.WinCount;
                ulong totalDraws = e.DrawCount;
                ulong totalLosses = e.Count - totalWins - totalDraws;
                double totalEloError = EloCalculator.Clamp(
                    EloCalculator.EloError99pct(totalWins, totalDraws, totalLosses),
                    2 * maxCalculatedEloDiff
                    );
                double gw = options.GamesWeight * Math.Exp(-(totalEloError / options.EloErrorHalfWeight));

                if (e.Count > 0)
                {
                    double totalPerf = (totalWins + totalDraws * options.DrawScore) / e.Count;
                    double expectedTotalPerf = EloCalculator.GetExpectedPerformance(e.TotalEloDiff / (double)e.Count);
                    if (sideToMove == Player.Black)
                    {
                        totalPerf = 1.0 - totalPerf;
                        expectedTotalPerf = 1.0 - expectedTotalPerf;
                    }
                    double adjustedPerf = EloCalculator.GetAdjustedPerformance(totalPerf, expectedTotalPerf, maxCalculatedEloDiff);
                    return new Tuple<double, double>(gw, adjustedPerf);
                }
                else
                {
                    return new Tuple<double, double>(0, 0);
                }
            }

            (double gamesWeight, double adjustedGamesPerf) = calculateAdjustedPerf(totalEntry);

            double gamesGoodness = EloCalculator.Clamp(
                EloCalculator.GetEloFromPerformance(adjustedGamesPerf), 
                maxCalculatedEloDiff
                ) * gamesWeight;

            // If eval is not present then assume 0.5 but reduce it for moves with low game count.
            // The idea is that we don't want missing eval to penalize common moves.
            double evalGoodness =
                (
                    score != null
                    ? EloCalculator.GetEloFromPerformance(score.Perf)
                    : -EloCalculator.EloError99pct(
                            totalEntry.WinCount,
                            totalEntry.DrawCount,
                            totalEntry.LossCount
                        )
                ) * evalWeight;

            double weightSum = gamesWeight + evalWeight;

            double goodness = EloCalculator.GetExpectedPerformance((gamesGoodness + evalGoodness) / weightSum);

            return goodness;
        }
    }
}
