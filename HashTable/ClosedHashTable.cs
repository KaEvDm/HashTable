using System;
using System.Collections.Generic;
using System.Linq;

namespace HashTable
{
    // Метод открытой адресации(закрытое хеширование)
    public sealed class ClosedHashTable<TKey, TValue> : HashTable<TKey, TValue>
    {
        private Dictionary<int, KeyValuePair<TKey, TValue>> cells;
        private readonly HashProbing HashProbing;

        private void CheckKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
        }

        public ClosedHashTable(HashProbing probing)
        {
            cells = new Dictionary<int, KeyValuePair<TKey, TValue>>();
            HashProbing = probing;
        }

        public override void Add(TKey key, TValue value)
        {
            CheckKey(key);

            var hash = GetHash(key);

            HashProbing.Initialize(hash);

            for (int i = 0; i < Size; i++)
            {
                var adress = HashProbing.NextStep();
                if (cells.ContainsKey(adress))
                {
                    if (cells[adress].Key.Equals(key))
                    {
                        throw new ArgumentException($"Хеш-таблица уже содержит элемент с ключом {key}." +
                            " Ключ должен быть уникален.", nameof(key));
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    cells.Add(hash, new KeyValuePair<TKey, TValue>(key, value));
                    return;
                }
            }
        }


        public override bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }
    }
}
