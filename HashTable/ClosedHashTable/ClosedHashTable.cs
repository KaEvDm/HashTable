using System;
using System.Collections.Generic;
using System.Linq;

namespace HashTable
{
    // Метод открытой адресации(закрытое хеширование)
    public sealed class ClosedHashTable<TKey, TValue> : HashTable<TKey, TValue>
    {
        private Dictionary<int, KeyValuePair<TKey, TValue>> cells;
        private readonly ProbingType probingType;
        private IHashProbing<TKey> hashProbing;

        public override int Count => cells.Count();

        public ClosedHashTable(ProbingType probingType, int k = 1)
        {
            this.probingType = probingType;

            switch (probingType)
            {
                case ProbingType.Linear:
                    {
                        Size = MathTools.GetCoPrime(k, 256, 1024);
                        if (Size == -1) throw new Exception("Невозможный размер таблицы");

                        hashProbing = new LinearProbing<TKey>(k);
                        break;
                    }
                case ProbingType.Quadratic:
                    {
                        Size = (int)Math.Pow(2, 8);
                        hashProbing = new QuadraticProbing<TKey>();
                        break;
                    }
                case ProbingType.DoubleHashing:
                    {
                        Size = MathTools.GetPrime(256, 1024);
                        if (Size == -1) throw new Exception("Невозможный размер таблицы");

                        hashProbing = new DoubleHashing<TKey>(Size);
                        break;
                    }
            }
            cells = new Dictionary<int, KeyValuePair<TKey, TValue>>(Size);
        }

        public override void Add(TKey key, TValue value)
        {
            CheckKey(key);
            var hash = GetHash(key);

            if (LoadFactor > 0.5)
            {
                IncreaseSize();
                Add(key, value);
            }
            else
            {
                for (int i = 0; i < Size; i++)
                {
                    var nextHash = (hash + hashProbing.Step(i, key)) % Size;

                    if (IsCellContainsKey(nextHash, key))
                    {
                        throw new ArgumentException($"Хеш-таблица уже содержит элемент с ключом {key}." +
                            " Ключ должен быть уникален.", nameof(key));
                    }
                    else
                    {
                        cells.Add(nextHash, new KeyValuePair<TKey, TValue>(key, value));
                        return;
                    }
                }
            }
        }

        public override bool Remove(TKey key)
        {
            CheckKey(key);
            var hash = GetHash(key);

            for (int i = 0; i < Size; i++)
            {
                var nextHash = (hash + hashProbing.Step(i, key)) % Size;

                if (IsCellContainsKey(nextHash, key))
                {
                    cells.Remove(nextHash);
                    return true;
                }
            }
            return false;
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            CheckKey(key);
            var hash = GetHash(key);

            for (int i = 0; i < Size; i++)
            {
                var nextHash = (hash + hashProbing.Step(i, key)) % Size;

                if (IsCellContainsKey(nextHash, key))
                {
                    value = cells[nextHash].Value;
                    return true;
                }
            }
            value = default(TValue);
            return false;
        }

        private void IncreaseSize()
        {
            GetNewSize();

            var newCells = new Dictionary<int, KeyValuePair<TKey, TValue>>(Size);

            foreach (var cell in cells)
            {
                var hash = cell.Key;
                var key = cell.Value.Key;

                var newHash = GetHash(key);

                if (hash != newHash)
                {
                    for (int i = 0; i < Size; i++)
                    {
                        var stepHash = (newHash + hashProbing.Step(i, key)) % Size;

                        if (newCells.ContainsKey(stepHash))
                        {
                            if (newCells[stepHash].Key.Equals(key))
                            {
                                throw new ArgumentException($"Хеш-таблица уже содержит элемент с ключом {key}." +
                                    " Ключ должен быть уникален.", nameof(key));
                            }
                        }
                        else
                        {
                            newCells.Add(stepHash, new KeyValuePair<TKey, TValue>(key, cell.Value.Value));
                            break;
                        }
                    }
                }
            }
            cells = newCells;
        }

        private void GetNewSize()
        {
            switch (probingType)
            {
                case ProbingType.Linear:
                    {
                        int k = (hashProbing as LinearProbing<TKey>).IntervalBetweenProbes;
                        Size = MathTools.GetCoPrime(k, Size * 2, Size * Size);
                        if (Size == -1) throw new Exception("Невозможный размер таблицы");
                        break;
                    }
                case ProbingType.Quadratic:
                    {
                        Size = (int)Math.Pow(2, Math.Log(Size, 2) + 2);
                        break;
                    }
                case ProbingType.DoubleHashing:
                    {
                        Size = MathTools.GetPrime(Size * 2, Size * Size);
                        if (Size == -1) throw new Exception("Невозможный размер таблицы");

                        hashProbing = new DoubleHashing<TKey>(Size);
                        break;
                    }
            }
        }

        private void CheckKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
        }

        private bool IsCellContainsKey(int hash, TKey key)
        {
            return cells.ContainsKey(hash) && cells[hash].Key.Equals(key);
        }
    }
}
