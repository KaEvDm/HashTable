using System;
using System.Collections.Generic;

namespace HashTable
{
    public abstract class HashTable<TKey, TValue>
    {
        //SecretHashCode определяется как "KaEvDm".GetHashCode();
        private const int SecretHashCode = -1474368688;

        public int Size { get; protected set; } = 256;
        public int LoadFactor { get => Count/Size; }
        public int Count { get; protected set; }

        public abstract void Add(KeyValuePair<TKey, TValue> item);
        public abstract TValue Search(TKey key);
        public abstract bool Delete(TKey key);

        protected int GetHash(TKey key) => (key.GetHashCode() ^ SecretHashCode) % Size;

        public TValue this[TKey key]
        {
            get => Search(key);
            set => Add(new KeyValuePair<TKey, TValue>(key, value));
        }
    }
}
