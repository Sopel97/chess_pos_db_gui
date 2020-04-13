using System.Collections.Generic;

namespace chess_pos_db_gui
{
    public class LRUCache<K, V>
    {
        private readonly int capacity;
        private readonly Dictionary<K, LinkedListNode<LRUCacheItem<K, V>>> cacheMap = new Dictionary<K, LinkedListNode<LRUCacheItem<K, V>>>();
        private readonly LinkedList<LRUCacheItem<K, V>> lruList = new LinkedList<LRUCacheItem<K, V>>();

        public LRUCache(int capacity)
        {
            this.capacity = capacity;
        }

        public void Clear()
        {
            cacheMap.Clear();
            lruList.Clear();
        }

        public V Get(K key)
        {
            if (cacheMap.TryGetValue(key, out LinkedListNode<LRUCacheItem<K, V>> node))
            {
                V value = node.Value.value;
                lruList.Remove(node);
                lruList.AddLast(node);
                return value;
            }

            return default;
        }

        public void Add(K key, V val)
        {
            if (cacheMap.Count >= capacity)
            {
                RemoveFirst();
            }

            LRUCacheItem<K, V> cacheItem = new LRUCacheItem<K, V>(key, val);
            LinkedListNode<LRUCacheItem<K, V>> node = new LinkedListNode<LRUCacheItem<K, V>>(cacheItem);
            lruList.AddLast(node);
            cacheMap.Add(key, node);
        }

        private void RemoveFirst()
        {
            // Remove from LRUPriority
            LinkedListNode<LRUCacheItem<K, V>> node = lruList.First;
            lruList.RemoveFirst();

            // Remove from cache
            cacheMap.Remove(node.Value.key);
        }
    }

    class LRUCacheItem<K, V>
    {
        public LRUCacheItem(K k, V v)
        {
            key = k;
            value = v;
        }
        public K key;
        public V value;
    }
}
