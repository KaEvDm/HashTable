using System;
using System.Collections.Generic;
using System.Linq;

namespace HashTable
{
    //========================================================\\
    // !!                                                  !! \\
    // !!           Эксперементальная версия               !! \\
    // !!                                                  !! \\
    //========================================================\\

    // Метод открытой адресации(закрытое хеширование)
    public sealed class ClosedHashTable<TKey, TValue> : HashTable<TKey, TValue>
    {
        private Dictionary<int, KeyValuePair<TKey, TValue>> cells;
        private readonly Func<int, int, TKey, int> probing;

        public override int Count => cells.Count();

        public ClosedHashTable(ProbingType probingType, int k = 1)
        {
            switch (probingType)
            {
                case ProbingType.Linear:
                    {
                        var hashProbing = new HashProbing<TKey>(k);

                        Size = MathTools.GetCoPrime(k, 256, 1024);
                        if (Size == -1) throw new Exception("Я накосячил с простыми числами");

                        probing = hashProbing.LinearStep;
                        break;
                    }
                case ProbingType.Quadratic:
                    {
                        var hashProbing = new HashProbing<TKey>();
                        Size = (int)Math.Pow(2, 10);
                        probing = hashProbing.QuadraticStep;
                        break;
                    }
                case ProbingType.DoubleHashing:
                    {
                        var hashProbing = new HashProbing<TKey>();

                        Size = MathTools.GetPrime(256, 1024);
                        if (Size == -1) throw new Exception("Я накосячил с простыми числами");

                        probing = new HashProbing<TKey>().DoubleHashingStep;
                        break;
                    }
            }

            cells = new Dictionary<int, KeyValuePair<TKey, TValue>>(Size);
        }

        public override void Add(TKey key, TValue value)
        {
            CheckKey(key);

            var hash = GetHash(key);

            //Нам нужно пробежать всю таблицу
            //для разных последовательностей проб, это делается разным способом
            for (int i = 0; i < Size / 2; i++)
            {
                var adress = (hash + probing(i, Size, key)) % Size;

                //если в ячейке есть значение
                if (cells.ContainsKey(adress))
                {
                    //на случай, если ключи равны
                    if (cells[adress].Key.Equals(key))
                    {
                        throw new ArgumentException($"Хеш-таблица уже содержит элемент с ключом {key}." +
                            " Ключ должен быть уникален.", nameof(key));
                    }

                    //делаем следующую пробу (смотрим некст ячейку)
                    continue;
                }
                else
                {
                    //ячейка пуста, заполняем её
                    cells.Add(hash, new KeyValuePair<TKey, TValue>(key, value));
                    return;
                }
            }

            //если мы здесь, значит мы пробежали половину таблицы и не нашли пустых ячеик
            //значит нам нужно увеличить размер таблицы
            IncreaseSize();

            //В табилцу с измененным размером, нужно положить наше новое значение
            Add(key, value);
        }


        public override bool Remove(TKey key)
        {
            CheckKey(key);

            var hash = GetHash(key);

            if (cells.ContainsKey(hash))
            {
                if (cells[hash].Key.Equals(key))
                {
                    cells.Remove(hash);
                    // удаляем и смещаем все зависимые ячейки hash которых, 
                    // не равен хешу от ключа который в них лежит
                    //пока не дойдём до пустой
                    ShiftCells(hash);
                }
            }
            else return false;

            return true; // написано от балды чтобы компилятор не ругался на функцию
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            CheckKey(key);

            var hash = GetHash(key);
            value = default(TValue);
            var adress = 0;
            int i = 0;

            // Нам нужно пробежать всю таблицу определённым способом, 
            // пока мы не встетим пустую ячейку  или не вернёмся в начальное место

            while(adress != hash)
            {
                adress = (hash + probing(i, Size, key)) % Size;

                //если в ячейке есть значение
                if (cells.ContainsKey(adress))
                {
                    //если ключи равны, это именно та ячейка которую мы ищем
                    if (cells[adress].Key.Equals(key))
                    {
                        value = cells[adress].Value;
                        return true;
                    }
                    //если ячейка не пуста но содержит другой ключ, след итерация
                    continue;
                }
                else
                {
                    //ячейка пуста
                    return false;
                }
            }
            return false;
        }

        private bool ShiftCells(int hash)
        {
            // на вход поступает хеш пустой ячейки,
            // нужно проверить, не пытались ли в этот хэш положить другие значения
            // находя их по probing
            // и смещать одну за другой (возможно рекурсивно)
            // пока не встретим пустую ячейку

            return true;
        }

        private bool IncreaseSize()
        {
            // важно помнить, что при изменении размера таблицы,
            // необходимо поменять расположение всех её ячеек
            return true;
        }

        private void CheckKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
        }

        public enum ProbingType
        {
            Linear,
            Quadratic,
            DoubleHashing
        }
    }
}
