using System;
using System.Collections.Generic;
using System.Linq;

namespace HashTable
{
    // Метод цепочек(открытое хеширование)
    public class OpenedHashTable<TKey, TValue> : HashTable<TKey, TValue>
    {
        private Dictionary<int, List<KeyValuePair<TKey, TValue>>> items;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="OpenedHashTable{TKey, TValue}"/>,
        /// который является пустым.
        /// </summary>
        public OpenedHashTable()
        {
            items = new Dictionary<int, List<KeyValuePair<TKey, TValue>>>();
        }

        /// <summary>
        /// Добавляет обьект в хеш-таблицу <see cref="OpenedHashTable{TKey, TValue}"/>.
        /// </summary>
        public override void Add(KeyValuePair<TKey, TValue> item)
        {
            var hash = GetHash(item.Key);

            if (items.ContainsKey(hash))
            {
                if (items[hash].SingleOrDefault(i => i.Key.Equals(item.Key)).Equals(default(KeyValuePair<TKey, TValue>)))
                {
                    throw new ArgumentException($"Хеш-таблица уже содержит элемент с ключом {item.Key}." +
                        " Ключ должен быть уникален.", nameof(item.Key));
                }
                else items[hash].Add(item);
            }
            else items.Add(hash, new List<KeyValuePair<TKey, TValue>> { item });

            Count++;
        }

        /// <summary>
        /// Выполняет поиск значения по ключу.
        /// Если таблица не содержит элемента с заданным ключом, метод возвращает null.
        /// </summary>
        public override TValue Search(TKey key)
        {
            var hash = GetHash(key);

            if (!items.ContainsKey(hash)) return default(TValue);

            var item = items[hash].SingleOrDefault(i => i.Key.Equals(key));

            if (!item.Equals(default(KeyValuePair<TKey, TValue>))) return item.Value;
            else return default(TValue);
        }

        /// <summary>
        /// Удаляет элемент из хеш-таблицы <see cref="OpenedHashTable{TKey, TValue}"/> с указанным ключом.
        /// </summary>
        public override bool Delete(TKey key)
        {
            var hash = GetHash(key);

            if (!items.ContainsKey(hash)) return false;

            var item = items[hash].SingleOrDefault(i => i.Key.Equals(key));

            if (!item.Equals(default(KeyValuePair<TKey, TValue>)))
            {
                items[hash].Remove(item);
                Count--;
                return true;
            }
            else return false;
        }
    }
}
