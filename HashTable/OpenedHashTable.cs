using System;
using System.Collections.Generic;
using System.Linq;

namespace HashTable
{
    // Метод цепочек(открытое хеширование)
    public class OpenedHashTable<TKey, TValue> : HashTable<TKey, TValue>
    {
        private Dictionary<int, Dictionary<TKey, TValue>> chains;
        public override int Count => chains.Values.Select(chain => chain.Count).Sum();

        private void CheckKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
        }
        
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="OpenedHashTable{TKey, TValue}"/>,
        /// который является пустым.
        /// </summary>
        public OpenedHashTable(int size = 256)
        {
            chains = new Dictionary<int, Dictionary<TKey, TValue>>();
            Size = size;
        }

        /// <summary>
        /// Добавляет указанные ключ и значение в хеш-таблицу <see cref="OpenedHashTable{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">Ключ добавляемого элемента.</param>
        /// <param name="value">
        /// Добавляемое значение элемента.
        /// Для ссылочных типов допускается значение null.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Элемент с таким ключом уже существует в <see cref="OpenedHashTable{TKey, TValue}"/>
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        ///  Свойство key имеет значение null.
        /// </exception>
        public override void Add(TKey key, TValue value)
        {
            CheckKey(key);

            var hash = GetHash(key);

            if (chains.ContainsKey(hash))
            {
                if (chains[hash].ContainsKey(key))
                {
                    throw new ArgumentException($"Хеш-таблица уже содержит элемент с ключом {key}." +
                        " Ключ должен быть уникален.", nameof(key));
                }
                else chains[hash].Add(key, value);
            }
            else chains.Add(hash, new Dictionary<TKey, TValue> { { key, value } });
        }

        /// <summary>
        /// Возвращает значение, связанное с заданным ключом.
        /// </summary>
        /// <param name="key">Ключ значения, которое необходимо получить.</param>
        /// <param name="value">
        /// Этот метод возвращает значение, связанное с указанным ключом, если он найден;
        /// в противном случае — значение по умолчанию для типа параметра value.
        /// </param>
        /// <returns>
        /// true, если <see cref="OpenedHashTable{TKey, TValue}"/> содержит элемент с указанным
        /// ключом, в противном случае — false.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///  Свойство key имеет значение null.
        /// </exception>
        public override bool TryGetValue(TKey key, out TValue value)
        {
            CheckKey(key);

            var hash = GetHash(key);

            if (chains.ContainsKey(hash) && chains[hash].ContainsKey(key))
            {
                value = chains[hash][key];
                return true;
            }
            else
            {
                value = default(TValue);
                return false;
            }
        }

        /// <summary>
        /// Удаляет элемент из хеш-таблицы <see cref="OpenedHashTable{TKey, TValue}"/> с указанным ключом.
        /// </summary>
        /// <param name="key">Ключ элемента, который требуется удалить.</param>
        /// <returns>
        /// Значение true, если элемент был найден и удален; в противном случае — значение false.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///  Свойство key имеет значение null.
        /// </exception>
        public override bool Remove(TKey key)
        {
            CheckKey(key);

            var hash = GetHash(key);

            if (!chains.ContainsKey(hash)) return false;

            return chains[hash].Remove(key);
        }
    }
}
