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

            public double EvalWeight { get; set; }
            public double GamesWeight { get; set; }
            public double DrawScore { get; internal set; }
        }

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
            ulong penaltyFromCountThreshold = 10;

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

            if (useEval && score == null && Math.Abs(totalEntry.TotalEloDiff / (long)totalEntry.Count) > maxAllowedEloDiff)
            {
                return 0.0;
            }

            double gamesWeight = options.UseGames ? options.GamesWeight : 0.0;
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
                    totalPerf = EloCalculator.GetExpectedPerformance(EloCalculator.GetEloFromPerformance(totalPerf) - totalEloError);
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

            adjustedGamesPerf = penalizePerf(adjustedGamesPerf, totalEntry.Count);

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
