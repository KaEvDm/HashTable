using System;
using System.Collections.Generic;

namespace HashTable
{
    public abstract class HashTable<TKey, TValue>
    {
        private readonly int SecretHashCode = "KaEvDm".GetHashCode();

        public int Size { get; protected set; }
        public double LoadFactor { get => Count / (double)Size; }
        public virtual int Count { get; protected set; }

        public abstract void Add(TKey key, TValue value);
        public abstract bool TryGetValue(TKey key, out TValue value);
        public abstract bool Remove(TKey key);

        protected int GetHash(TKey key) => Math.Abs((key.GetHashCode() ^ SecretHashCode) % Size);

        /// <summary>
        /// Возвращает или задает элемент по указанному индексу.
        /// </summary>
        /// <param name="key">
        /// Ключ элемента, который требуется возвратить или задать.
        /// </param>
        /// <returns>
        /// Значение, с указанным ключом. Если элемента нет в коллекции <see cref="default{TValue}"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///  Свойство key имеет значение null.
        /// </exception>
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
