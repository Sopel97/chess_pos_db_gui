using chess_pos_db_gui.src.app.chessdbcn;
using ChessDotNet;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace chess_pos_db_gui.src.app
{
    public class QueryExecutor
    {
        private static readonly int queryCacheSize = 128;

        private DatabaseProxy Database { get; set; }

        private Thread QueryThread { get; set; }
        private volatile bool EndQueryThread;

        private QueryQueue QueryQueue { get; set; }
        private Mutex QueryQueueMutex { get; set; }

        private LRUCache<QueryQueueEntry, QueryCacheEntry> QueryCache { get; set; }
        private object QueryCacheLock { get; set; }

        private ConditionVariable AnyOutstandingQuery { get; set; }

        private ChessDBCNScoreProvider ScoreProvider { get; set; }

        private EventHandler<KeyValuePair<QueryQueueEntry, QueryCacheEntry>> onDataReceived;
        public event EventHandler<KeyValuePair<QueryQueueEntry, QueryCacheEntry>> DataReceived
        {
            add
            {
                onDataReceived += value;
            }

            remove
            {
                onDataReceived -= value;
            }
        }

        public QueryExecutor(DatabaseProxy db)
        {
            ScoreProvider = new ChessDBCNScoreProvider();
            Database = db;
            QueryQueueMutex = new Mutex();
            QueryQueue = new QueryQueue();
            QueryCacheLock = new object();
            EndQueryThread = false;
            AnyOutstandingQuery = new ConditionVariable();
            QueryCache = new LRUCache<QueryQueueEntry, QueryCacheEntry>(queryCacheSize);
            QueryThread = new Thread(new ThreadStart(RunQueryThread));
            QueryThread.Start();
        }

        public void ResetQueueAndCache()
        {
            QueryQueueMutex.WaitOne();
            for (; ; )
            {
                if (QueryQueue.Pop() == null)
                {
                    break;
                }
            }

            QueryQueueMutex.ReleaseMutex();
            lock (QueryCacheLock)
            {
                QueryCache.Clear();
            }
        }

        public void SetDatabase(DatabaseProxy db)
        {
            ResetQueueAndCache();

            Database = db;
        }

        private void RunQueryThread()
        {
            for (; ; )
            {
                QueryQueueMutex.WaitOne();
                while (QueryQueue.IsEmpty() && !EndQueryThread)
                {
                    AnyOutstandingQuery.Wait(QueryQueueMutex);
                }

                if (EndQueryThread)
                {
                    QueryQueueMutex.ReleaseMutex();
                    break;
                }

                var sig = QueryQueue.Pop();
                QueryQueueMutex.ReleaseMutex();

                QueryAsyncToCacheAndUpdate(sig);
            }
        }

        public void ScheduleUpdateDataAsync(QueryQueueEntry key)
        {
            {
                var entry = GetFromCache(key);
                if (entry != null)
                {
                    onDataReceived?.Invoke(this, new KeyValuePair<QueryQueueEntry, QueryCacheEntry>(key, entry));
                    return;
                }
            }

            QueryQueueMutex.WaitOne();
            QueryQueue.Enqueue(key);
            QueryQueueMutex.ReleaseMutex();
            AnyOutstandingQuery.Signal();
        }

        private QueryCacheEntry GetFromCache(QueryQueueEntry key)
        {
            lock (QueryCacheLock)
            {
                return QueryCache.Get(key);
            }
        }

        private Dictionary<Move, ChessDBCNScore> GetChessdbcnScores(string fen)
        {
            return ScoreProvider.GetScores(fen);
        }

        private QueryCacheEntry QueryAsyncToCache(QueryQueueEntry key)
        {
            if (!Database.IsOpen)
            {
                return null;
            }

            try
            {
                var data = new QueryCacheEntry(null, null);

                var scores = key.QueryEval
                    ? Task.Run(() => GetChessdbcnScores(key.CurrentFen))
                    : Task.FromResult(new Dictionary<Move, ChessDBCNScore>());

                if (key.San == San.NullMove)
                {
                    data.Stats = Database.Query(key.QueryFen);
                    data.Scores = scores.Result;
                    lock (QueryCacheLock)
                    {
                        QueryCache.Add(key, data);
                    }
                }
                else
                {
                    data.Stats = Database.Query(key.QueryFen, key.San);
                    data.Scores = scores.Result;
                    lock (QueryCacheLock)
                    {
                        QueryCache.Add(key, data);
                    }
                }

                return data;
            }
            catch
            {
                return null;
            }
        }

        private void QueryAsyncToCacheAndUpdate(QueryQueueEntry key)
        {
            var entry = QueryAsyncToCache(key);
            onDataReceived?.Invoke(this, new KeyValuePair<QueryQueueEntry, QueryCacheEntry>(key, entry));
        }

        public void Dispose()
        {
            EndQueryThread = true;
            AnyOutstandingQuery.Signal();
            QueryThread.Join();
        }
    }

    internal class QueryQueue
    {
        private QueryQueueEntry Current { get; set; }
        private QueryQueueEntry Next { get; set; }

        private readonly object Lock;

        public QueryQueue()
        {
            Current = null;
            Next = null;

            Lock = new object();
        }

        public void Enqueue(QueryQueueEntry key)
        {
            lock (Lock)
            {
                if (Current == null)
                {
                    Current = key;
                }
                else
                {
                    Next = key;
                }
            }
        }

        public bool IsEmpty()
        {
            return Current == null;
        }

        public QueryQueueEntry Pop()
        {
            lock (Lock)
            {
                if (Current != null)
                {
                    var ret = Current;
                    Current = Next;
                    Next = null;
                    return ret;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
