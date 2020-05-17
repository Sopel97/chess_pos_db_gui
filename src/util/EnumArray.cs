using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace chess_pos_db_gui.src.util
{
    // https://stackoverflow.com/questions/981776/using-an-enum-as-an-array-index-in-c-sharp
    public class EnumArray<TKey, T> : IEnumerable<KeyValuePair<TKey, T>> where TKey : struct
    {
        public EnumArray()
        {
            if (!typeof (TKey).IsEnum) throw new InvalidOperationException("Generic type argument is not an Enum");
            var size = Convert.ToInt32(Keys.Max()) + 1;
            Values = new T[size];
        }

        protected T[] Values;

        public static IEnumerable<TKey> Keys
        {
            get { return Enum.GetValues(typeof (TKey)).OfType<TKey>(); }
        }

        public T this[TKey index]
        {
            get { return Values[Convert.ToInt32(index)]; }
            set { Values[Convert.ToInt32(index)] = value; }
        }

        private IEnumerable<KeyValuePair<TKey, T>> CreateEnumerable()
        {
            return Keys.Select(key => new KeyValuePair<TKey, T>(key, Values[Convert.ToInt32(key)]));
        }

        public IEnumerator<KeyValuePair<TKey, T>> GetEnumerator()
        {
            return CreateEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class InitializedEnumArray<TKey, T> : EnumArray<TKey, T>
        where TKey : struct
        where T : new()
    {
        public InitializedEnumArray() :
            base()
        {
            for (int i = 0; i < Values.Length; ++i)
            {
                Values[i] = new T();
            }
        }
    }
}
