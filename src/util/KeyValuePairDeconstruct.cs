using System.Collections.Generic;

namespace chess_pos_db_gui.src.util
{
    public static class KeyValuePairDeconstruct
    {
        public static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> tuple, out T1 key, out T2 value)
        {
            key = tuple.Key;
            value = tuple.Value;
        }
    }
}
