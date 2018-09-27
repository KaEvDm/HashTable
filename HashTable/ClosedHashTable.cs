using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    public sealed class ClosedHashTable<TKey, TValue> : HashTable<TKey, TValue>
    {
        private Dictionary<int, List<Item<TKey, TValue>>> items;

        public ClosedHashTable()
        {
            items = new Dictionary<int, List<Item<TKey, TValue>>>();
        }

        public override void Add(Item<TKey, TValue> item)
        {
            var hash = GetHash(item.Key);

            if (items.ContainsKey(hash))
            {
                if (items[hash].SingleOrDefault(i => i.Key.Equals(item.Key)) != null)
                {
                    throw new ArgumentException($"Хеш-таблица уже содержит элемент с ключом {item.Key}. " +
                        $"Ключ должен быть уникален.", nameof(item.Key));
                }
                else
                {
                    items[hash].Add(item);
                }
            }
            else
            {
                items.Add(hash, new List<Item<TKey, TValue>>{ item });
            }
        }

        public override TValue Search(TKey key)
        {
            throw new NotImplementedException();
        }

        public override void Delete(TKey key)
        {
            throw new NotImplementedException();
        }
    }
}
