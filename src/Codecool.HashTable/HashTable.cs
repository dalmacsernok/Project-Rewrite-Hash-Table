using System;
using System.Collections.Generic;
using System.Text;

namespace Codecool.HashTable
{
    /// <summary>
    /// Collection for storing KeyValue pairs.
    /// </summary>
    /// <typeparam name="TKey">Type of key</typeparam>
    /// <typeparam name="TValue">Type of value</typeparam>
    public class HashTable<TKey, TValue>
    {
        /// <summary>
        /// Number of buckets - DO NOT MODIFY!
        /// </summary>
        private const int _bucketSize = 16;

        /// <summary>
        /// It is used to avoid NullReferenceException when comparing keys.
        /// </summary>
        private readonly EqualityComparer<TKey> _comparer;

        /// <summary>
        /// This holds all the data. Its a primitive array where every element is a <see cref="LinkedList{T}"/>.
        /// The list holds elements of type <see cref="Entry{TKey, TValue}"/>.
        /// </summary>
        private readonly LinkedList<Entry<TKey, TValue>>[] _buckets;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashTable{TKey, TValue}"/> class.
        /// </summary>
        public HashTable()
        {
            _comparer = EqualityComparer<TKey>.Default;
            _buckets = new LinkedList<Entry<TKey, TValue>>[_bucketSize];

            for (int i = 0; i < _bucketSize; i++)
            {
                _buckets[i] = new LinkedList<Entry<TKey, TValue>>();
            }
        }

        /// <summary>
        /// Gets or sets a new value for a given key in the hashtable.
        /// by allowing the client code to use [] notation.
        /// </summary>
        /// <param name="key">Index of new element</param>
        /// <returns>Value belongs to the entry with the given index.</returns>
        public TValue this[TKey key]
        {
            get
            {
                var entry = FindEntryInBucket(GetBucketAtIndex(GetBucketIndexForKey(key)), key);
                if (entry is not null)
                {
                    return entry.Value;
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
            set
            {
                int index = GetBucketIndexForKey(key);
                var list = GetBucketAtIndex(index);
                var entry = FindEntryInBucket(list, key);
                if (entry is not null)
                {
                    entry.Value = value;
                }
                else
                {
                    list.AddFirst(new LinkedListNode<Entry<TKey, TValue>>(new Entry<TKey, TValue>(key, value)));
                }
            }
        }

        /// <summary>
        /// Delete all entry from hash table.
        /// </summary>
        public void Clear()
        {
            foreach (var entry in _buckets)
            {
                entry.Clear();
            }
        }

        /// <summary>
        /// Remove the entry belongs to the given key.
        /// If key not found, throw an <see cref="KeyNotFoundException"/>.
        /// </summary>
        /// <param name="key">Key to be removed</param>
        /// <returns>Value of removed entry</returns>
        public TValue Remove(TKey key)
        {
            int index = GetBucketIndexForKey(key);
            var list = GetBucketAtIndex(index);
            var entry = FindEntryInBucket(list, key);
            if (entry is not null)
            {
                TValue result = entry.Value;
                list.Remove(entry);
                return result;
            }
            else
            {
                throw new KeyNotFoundException();
            }

        }

        /// <summary>
        /// This function converts somehow the key to an integer between 0 and bucketSize
        /// </summary>
        /// <param name="key">Key to find hashcode for.</param>
        /// <returns>Hashcode converted to int.</returns>
        private int GetBucketIndexForKey(TKey key)
        {
            if (key == null)
            {
                return 0;
            }
            else
            {
                return Math.Abs(key.GetHashCode() % _bucketSize);
            }
            
        }

        private LinkedList<Entry<TKey, TValue>> GetBucketAtIndex(int index)
        {
            return _buckets[index];
        }

        private Entry<TKey, TValue> FindEntryInBucket(LinkedList<Entry<TKey, TValue>> list, TKey key)
        {
            foreach (Entry<TKey, TValue> entry in list)
            {
                if (_comparer.Equals(entry.Key, key))
                {
                    return entry;
                }
                
            }

            return null;

        }
    }
}
