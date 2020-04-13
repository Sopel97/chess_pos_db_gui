using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace chess_pos_db_gui
{
    public class ConditionVariable
    {
        private readonly ConcurrentQueue<ManualResetEventSlim> _waitingThreads = new ConcurrentQueue<ManualResetEventSlim>();

        /// <summary>
        ///     Atomically unlocks and waits for a signal.
        ///     Then relocks the mutex before returning
        /// </summary>
        /// <param name="mutex"></param>
        public void Wait(Mutex mutex)
        {
            if (mutex == null)
            {
                throw new ArgumentNullException("mutex");
            }
            var waitHandle = new ManualResetEventSlim();
            try
            {
                _waitingThreads.Enqueue(waitHandle);
                mutex.ReleaseMutex();
                waitHandle.Wait();
            }
            finally
            {
                waitHandle.Dispose();
            }
            mutex.WaitOne();
        }

        public void WaitRead(ReaderWriterLockSlim readerWriterLock)
        {
            if (readerWriterLock == null)
            {
                throw new ArgumentNullException("readerWriterLock");
            }
            var waitHandle = new ManualResetEventSlim();
            try
            {
                _waitingThreads.Enqueue(waitHandle);
                readerWriterLock.ExitReadLock();
                waitHandle.Wait();
            }
            finally
            {
                waitHandle.Dispose();
            }
            readerWriterLock.EnterReadLock();
        }

        public void Signal()
        {
            if (_waitingThreads.TryDequeue(out ManualResetEventSlim waitHandle))
            {
                waitHandle.Set();
            }
        }

        public void Broadcast()
        {
            while (_waitingThreads.TryDequeue(out ManualResetEventSlim waitHandle))
            {
                waitHandle.Set();
            }
        }
    }
}
