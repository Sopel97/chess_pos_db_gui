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
            public bool UseGames { get; set; }
            public bool UseEval { get; set; }
            public bool IncreaseErrorBarForLowN { get; set; }

            public double EvalWeight { get; set; }
            public double GamesWeight { get; set; }
            public double DrawScore { get; set; }

            public ulong LowN { get; set; }
        }

        public static double CalculateGoodness(
            Player sideToMove,
            EnumArray<GameLevel, AggregatedEntry> aggregatedEntries,
            ChessDBCNScore score,
            Options options
            )
        {
            const double maxAllowedPlayerEloDiff = 400;
            const double maxCalculatedEloDiff = 800;

            bool useEval = options.UseEval;

            AggregatedEntry totalEntry = new AggregatedEntry();
            foreach(KeyValuePair<GameLevel, AggregatedEntry> e in aggregatedEntries)
            {
                totalEntry.Combine(e.Value);
            }

            if (options.UseGames && totalEntry.Count == 0)
            {
                return 0.0;
            }

            if (useEval && score == null && Math.Abs(totalEntry.TotalEloDiff / (double)totalEntry.Count) > maxAllowedPlayerEloDiff)
            {
                return 0.0;
            }

            double gamesWeight = options.UseGames ? options.GamesWeight : 0.0;
            double evalWeight = useEval ? options.EvalWeight : 0.0;

            double calculateAdjustedPerf(AggregatedEntry e)
            {
                if (e.Count > 0)
                {
                    ulong? lowNThreshold = options.IncreaseErrorBarForLowN ? (ulong?)options.LowN : null;
                    ulong totalWins = e.WinCount;
                    ulong totalDraws = e.DrawCount;
                    ulong totalLosses = e.Count - totalWins - totalDraws;
                    double totalPerf = (totalWins + totalDraws * options.DrawScore) / e.Count;
                    double totalEloError = EloCalculator.Clamp(
                        EloCalculator.EloError99pct(totalWins, totalDraws, totalLosses, lowNThreshold),
                        2 * maxCalculatedEloDiff
                        );
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

            double gamesGoodness = EloCalculator.GetEloFromPerformance(adjustedGamesPerf) * gamesWeight;

            // If eval is not present then assume 0.5 but reduce it for moves with low game count.
            // The idea is that we don't want missing eval to penalize common moves.
            double evalGoodness =
                EloCalculator.GetEloFromPerformance(
                    score != null
                    ? score.Perf
                    : EloCalculator.GetExpectedPerformance(
                        -EloCalculator.EloError99pct(
                            totalEntry.WinCount, 
                            totalEntry.DrawCount, 
                            totalEntry.LossCount
                            )
                        )
                    ) * evalWeight;

            double weightSum = gamesWeight + evalWeight;

            double goodness = EloCalculator.GetExpectedPerformance((gamesGoodness + evalGoodness) / weightSum);

            return goodness;
        }
    }
}
