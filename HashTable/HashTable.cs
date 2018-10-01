﻿using System;
using System.Collections.Generic;

namespace HashTable
{
    public abstract class HashTable<TKey, TValue>
    {
        private readonly int SecretHashCode = "KaEvDm".GetHashCode();

        public int Size { get; protected set; }
        public int LoadFactor { get => Count/Size; }
        public virtual int Count { get; protected set; }

        public abstract void Add(TKey key, TValue value);
        public abstract bool TryGetValue(TKey key, out TValue value);
        public abstract bool Remove(TKey key);

        protected int GetHash(TKey key) => (key.GetHashCode() ^ SecretHashCode) % Size;

        public TValue this[TKey key]
        {
            get
            {
                TryGetValue(key, out TValue value);
                return value;
            }
            set => Add(key, value);
        }
    }
}
