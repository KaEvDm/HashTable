using System;
using System.Collections.Generic;
using System.Linq;

namespace HashTable
{
    public class OpenedHashTable<TKey, TValue> : HashTable<TKey, TValue>
    {
        private Dictionary<int, List<Item<TKey, TValue>>> items;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="OpenedHashTable{TKey, TValue}"/>,
        /// который является пустым.
        /// </summary>
        public OpenedHashTable()
        {
            items = new Dictionary<int, List<Item<TKey, TValue>>>();
        }

        /// <summary>
        /// Добавляет обьект в хэш-таблицу <see cref="OpenedHashTable{TKey, TValue}"/>.
        /// </summary>
        public override void Add(Item<TKey, TValue> item)
        {
            var hash = GetHash(item.Key);

            if (items.ContainsKey(hash))
            {
                if (items[hash].SingleOrDefault(i => i.Key.Equals(item.Key)) != null)
                {
                    throw new ArgumentException($"Хеш-таблица уже содержит элемент с ключом {item.Key}." +
                        " Ключ должен быть уникален.", nameof(item.Key));
                }
                else items[hash].Add(item);
            }
            else items.Add(hash, new List<Item<TKey, TValue>> { item });
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

            if (item != null) return item.Value;
            else return default(TValue);
        }

        /// <summary>
        /// Удаляет элемент из хэш-таблицы <see cref="OpenedHashTable{TKey, TValue}"/> с указанным ключом.
        /// </summary>
        public override bool Delete(TKey key)
        {
            var hash = GetHash(key);

            if (!items.ContainsKey(hash)) return false;

            var item = items[hash].SingleOrDefault(i => i.Key.Equals(key));

            if (item != null)
            {
                items[hash].Remove(item);
                return true;
            }
            else return false;
        }
    }
}
