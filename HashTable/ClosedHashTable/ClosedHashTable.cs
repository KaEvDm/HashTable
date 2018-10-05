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
                        Size = (int)Math.Pow(2, 10);
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
                    var newHash = (hash + hashProbing.Step(i, key)) % Size;

                    if (cells.ContainsKey(newHash))
                    {
                        //на случай, если ключи равны
                        if (cells[newHash].Key.Equals(key))
                        {
                            throw new ArgumentException($"Хеш-таблица уже содержит элемент с ключом {key}." +
                                " Ключ должен быть уникален.", nameof(key));
                        }
                    }
                    else
                    {
                        //ячейка пуста, заполняем её
                        cells.Add(newHash, new KeyValuePair<TKey, TValue>(key, value));
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
                var newHash = (hash + hashProbing.Step(i, key)) % Size;

                if (cells.ContainsKey(newHash))
                {
                    if (cells[newHash].Key.Equals(key))
                    {
                        cells.Remove(newHash);
                        return true;
                    }
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
                var newHash = (hash + hashProbing.Step(i, key)) % Size;

                if (cells.ContainsKey(newHash))
                {
                    if (cells[newHash].Key.Equals(key))
                    {
                        value = cells[newHash].Value;
                        return true;
                    }
                }
            }

            value = default(TValue);
            return false;
        }

        public void PrintTable()
        {
            Console.WriteLine($"++++++++++++++++++++++{Size}++++++++++++++++");
            for (int i = 0; i < Size; i++)
            {   
                if (cells.ContainsKey(i))
                {
                    Console.WriteLine($"[{i}]({cells[i].Key})");
                }
                else
                {
                    Console.WriteLine($"[{i}]({00})");
                }

            }
            Console.WriteLine($"++++++++++++++++++++++++{Size}++++++++++++++++");
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
                        Size = (int)Math.Pow(2, Math.Log(Size, 2) * 2);
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

        private void IncreaseSize()
        {
            GetNewSize();

            var newCells = new Dictionary<int, KeyValuePair<TKey, TValue>>(Size);
            Console.WriteLine($"=================={Size}===================");

            foreach (var cell in cells)
            {
                var hash = cell.Key;
                var key = cell.Value.Key;
                Console.Write($"|[{hash}]({key})|");
                var newHash = GetHash(key);

                //если ячейка не на своём месте
                if (hash != newHash)
                {
                    //добавляем элемент со смещением
                    for (int i = 0; i < Size; i++)
                    {
                        var stepHash = (newHash + hashProbing.Step(i, key)) % Size;
                        Console.Write($" -> [{stepHash}]");
                        if (newCells.ContainsKey(stepHash))
                        {
                            Console.Write($"({newCells[stepHash].Key})");
                            //на случай, если ключи равны
                            if (newCells[stepHash].Key.Equals(key))
                            {
                                throw new ArgumentException($"Хеш-таблица уже содержит элемент с ключом {key}." +
                                    " Ключ должен быть уникален.", nameof(key));
                            }
                        }
                        else
                        {
                            //ячейка пуста, заполняем её
                            newCells.Add(stepHash, new KeyValuePair<TKey, TValue>(key, cell.Value.Value));
                            break;
                        }
                    }
                }
                Console.WriteLine();
            }
            cells = newCells;
            Console.WriteLine($"=================={Size}===================");
        }

        private void CheckKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
        }
    }
}
