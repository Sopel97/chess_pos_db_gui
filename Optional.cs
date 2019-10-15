using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui
{
    public class Optional<T> : IEnumerable<T>
    {
        private readonly T[] data;

        private Optional(T[] data)
        {
            this.data = data;
        }

        public static Optional<T> Create(T value)
        {
            return new Optional<T>(new T[] { value });
        }

        public static Optional<T> CreateEmpty()
        {
            return new Optional<T>(new T[0]);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)this.data).GetEnumerator();
        }

        System.Collections.IEnumerator
            System.Collections.IEnumerable.GetEnumerator()
        {
            return this.data.GetEnumerator();
        }
    }
}
