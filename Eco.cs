using System;
using System.Json;

namespace chess_pos_db_gui
{
    public struct Eco
    {
        public char Category { get; set; }
        public byte Index { get; set; }

        public static Eco FromJson(JsonValue json)
        {
            return Eco.FromString(json);
        }

        public static Eco FromString(string str)
        {
            if (str.Length != 3) throw new ArgumentException();

            if (str[0] < 'A' || str[0] > 'E') throw new ArgumentException();

            return new Eco
            {
                Category = str[0],
                Index = Byte.Parse(str.Substring(1, 2))
            };
        }

        public Eco(char category, byte index)
        {
            Category = category;
            Index = index;
        }

        public override string ToString()
        {
            return Category + Index.ToString("D2");
        }
    }
}