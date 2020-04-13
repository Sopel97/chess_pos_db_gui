
using System;

namespace chess_pos_db_gui
{
    public enum UciScoreType
    {
        Cp,
        Mate
    }

    public enum UciScoreBoundType
    {
        Exact,
        LowerBound,
        UpperBound
    }

    public class UciScore
    {
        public int Value { get; private set; }
        private UciScoreType Type { get; set; }
#pragma warning disable IDE0052 // Remove unread private members
        private UciScoreBoundType BoundType { get; set; }
#pragma warning restore IDE0052 // Remove unread private members

        public UciScore(int value, UciScoreType type, UciScoreBoundType boundType)
        {
            Value = value;
            Type = type;
            BoundType = boundType;
        }

        public override string ToString()
        {
            if (Type == UciScoreType.Cp)
            {
                return (Value / 100.0).ToString("0.00");
            }
            else
            {
                return string.Format("{0}M{1}", Value < 0 ? "-" : "", Math.Abs(Value));
            }
        }

        public int ToInteger()
        {
            int score = Value;
            if (Type == UciScoreType.Mate)
            {
                score += score > 0 ? 1000000000 : -1000000000;
            }
            return score;
        }
    }
}
