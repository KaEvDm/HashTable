using System;

namespace HashTable
{
    public abstract class HashTable<TKey, TValue>
    {
        //SecretHashCode определяется как "KaEvDm".GetHashCode();
        private const int SecretHashCode = -1474368688;
        private const int Size = 256;

        public abstract void Add(Item<TKey, TValue> item);
        public abstract TValue Search(TKey key);
        public abstract void Delete(TKey key);

        protected int GetHash(TKey key) => (key.GetHashCode() ^ SecretHashCode) % Size;
    }
}
