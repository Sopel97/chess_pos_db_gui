using chess_pos_db_gui.src.app.chessdbcn;
using ChessDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace chess_pos_db_gui.src.app
{
    public class QueryExecutor
    {
        private static readonly int queryCacheSize = 128;
        private DatabaseProxy Database { get; set; }
        private LRUCache<QueryQueueEntry, CacheEntry> QueryCache { get; set; }

        private QueryQueue QueryQueue { get; set; }

        private object CacheLock { get; set; }

        private Thread QueryThread { get; set; }
        private ConditionVariable AnyOutstandingQuery { get; set; }

        private volatile bool EndQueryThread;

        private Mutex QueueMutex { get; set; }

        private ChessDBCNScoreProvider ScoreProvider { get; set; }

        private EventHandler<KeyValuePair<QueryQueueEntry, CacheEntry>> onDataReceived;
        public event EventHandler<KeyValuePair<QueryQueueEntry, CacheEntry>> DataReceived
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
            QueueMutex = new Mutex();
            QueryQueue = new QueryQueue();
            CacheLock = new object();
            EndQueryThread = false;
            AnyOutstandingQuery = new ConditionVariable();
            QueryCache = new LRUCache<QueryQueueEntry, CacheEntry>(queryCacheSize);
            QueryThread = new Thread(new ThreadStart(RunQueryThread));
            QueryThread.Start();
        }

        public void ResetQueueAndCache()
        {
            QueueMutex.WaitOne();
            for (; ; )
            {
                if (QueryQueue.Pop() == null)
                {
                    break;
                }
            }

            QueueMutex.ReleaseMutex();
            lock (CacheLock)
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
                QueueMutex.WaitOne();
                while (QueryQueue.IsEmpty() && !EndQueryThread)
                {
                    AnyOutstandingQuery.Wait(QueueMutex);
                }

                if (EndQueryThread)
                {
                    QueueMutex.ReleaseMutex();
                    break;
                }

                var sig = QueryQueue.Pop();
                QueueMutex.ReleaseMutex();

                QueryAsyncToCacheAndUpdate(sig);
            }
        }

        public void ScheduleUpdateDataAsync(QueryQueueEntry e)
        {
            {
                var c = GetFromCache(e);
                if (c != null)
                {
                    onDataReceived?.Invoke(this, new KeyValuePair<QueryQueueEntry, CacheEntry>(e, c));
                    return;
                }
            }

            QueueMutex.WaitOne();
            QueryQueue.Enqueue(e);
            QueueMutex.ReleaseMutex();
            AnyOutstandingQuery.Signal();
        }

        private CacheEntry GetFromCache(QueryQueueEntry e)
        {
            lock (CacheLock)
            {
                return QueryCache.Get(e);
            }
        }

        private Dictionary<Move, ChessDBCNScore> GetChessdbcnScores(string fen)
        {
            return ScoreProvider.GetScores(fen);
        }

        private CacheEntry QueryAsyncToCache(QueryQueueEntry sig)
        {
            if (!Database.IsOpen)
            {
                return null;
            }

            try
            {
                var data = new CacheEntry(null, null);

                var scores = sig.QueryEval
                    ? Task.Run(() => GetChessdbcnScores(sig.CurrentFen))
                    : Task.FromResult(new Dictionary<Move, ChessDBCNScore>());

                if (sig.San == "--")
                {
                    data.Stats = Database.Query(sig.Fen);
                    data.Scores = scores.Result;
                    lock (CacheLock)
                    {
                        QueryCache.Add(sig, data);
                    }
                }
                else
                {
                    data.Stats = Database.Query(sig.Fen, sig.San);
                    data.Scores = scores.Result;
                    lock (CacheLock)
                    {
                        QueryCache.Add(sig, data);
                    }
                }

                return data;
            }
            catch
            {
                return null;
            }
        }

        private void QueryAsyncToCacheAndUpdate(QueryQueueEntry sig)
        {
            var e = QueryAsyncToCache(sig);
            onDataReceived?.Invoke(this, new KeyValuePair<QueryQueueEntry, CacheEntry>(sig, e));
        }

        public void Dispose()
        {
            EndQueryThread = true;
            AnyOutstandingQuery.Signal();
            QueryThread.Join();
        }
    }
}
