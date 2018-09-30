using System;
using System.Collections.Generic;
using System.Linq;

namespace HashTable
{
    // Метод открытой адресации(закрытое хеширование)
    public sealed class ClosedHashTable<TKey, TValue> : HashTable<TKey, TValue>
    {
        public ClosedHashTable()
        {

        }

        public override void Add(TKey key, TValue value)
        {
            throw new NotImplementedException();
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
