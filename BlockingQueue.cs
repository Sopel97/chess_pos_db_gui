using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui
{
    class BlockingQueue<T>
    {
        readonly Queue<T> q = new Queue<T>();

        public void Enqueue(T item)
        {
            lock (q)
            {
                q.Enqueue(item);
                System.Threading.Monitor.Pulse(q);
            }
        }

        public T Dequeue()
        {
            lock (q)
            {
                for (; ; )
                {
                    if (q.Count > 0)
                    {
                        return q.Dequeue();
                    }
                    System.Threading.Monitor.Wait(q);
                }
            }
        }

        public T DequeueTimed(int timeout)
        {
            lock (q)
            {
                for (; ; )
                {
                    if (q.Count > 0)
                    {
                        return q.Dequeue();
                    }
                    if (!System.Threading.Monitor.Wait(q, timeout))
                    {
                        return default;
                    }
                }
            }
        }

        public void Clear()
        {
            lock (q)
            {
                q.Clear();
            }
        }
    }
}
