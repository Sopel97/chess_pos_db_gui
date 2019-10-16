using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui
{
    class AggregatedEntry
    {
        public ulong Count { get; set; }
        public ulong WinCount { get; set; }
        public ulong DrawCount { get; set; }
        public ulong LossCount { get; set; }
        public Optional<GameHeader> FirstGame { get; set; }
        public float Perf { get { return (WinCount + DrawCount / 2.0f) / Count; } }
        public float DrawRate { get { return (float)DrawCount / Count; } }

        public AggregatedEntry()
        {
            Count = 0;
            WinCount = 0;
            DrawCount = 0;
            LossCount = 0;
            FirstGame = Optional<GameHeader>.CreateEmpty();
        }

        public AggregatedEntry(SegregatedEntries entries, List<GameLevel> levels) :
            this()
        {
            foreach(KeyValuePair<Origin, Entry> entry in entries)
            {
                if (levels.Contains(entry.Key.Level))
                {
                    Combine(entry.Value, entry.Key.Result);
                }
            }
        }

        public void Combine(Entry entry, GameResult result)
        {
            Count += entry.Count;
            switch (result)
            {
                case GameResult.WhiteWin:
                    WinCount += entry.Count;
                    break;
                case GameResult.Draw:
                    DrawCount += entry.Count;
                    break;
                case GameResult.BlackWin:
                    LossCount += entry.Count;
                    break;
            }

            if (FirstGame.Count() == 0)
            {
                FirstGame = entry.FirstGame;
            }
            else if (entry.FirstGame.Count() != 0 && entry.FirstGame.First().GameId < FirstGame.First().GameId)
            {
                FirstGame = entry.FirstGame;
            }
        }

        public void Combine(AggregatedEntry entry)
        {
            Count += entry.Count;
            WinCount += entry.WinCount;
            DrawCount += entry.DrawCount;
            LossCount += entry.LossCount;

            if (FirstGame.Count() == 0)
            {
                FirstGame = entry.FirstGame;
            }
            else if (entry.FirstGame.Count() != 0 && entry.FirstGame.First().GameId < FirstGame.First().GameId)
            {
                FirstGame = entry.FirstGame;
            }
        }
    }
}
