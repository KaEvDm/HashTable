using System;
using System.Collections.Generic;
using System.Linq;

namespace HashTable
{
    // Метод открытой адресации(закрытое хеширование)
    public sealed class ClosedHashTable<TKey, TValue> : HashTable<TKey, TValue>
    {
        public override void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public override TValue Search(TKey key)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(TKey key)
        {
            throw new NotImplementedException();
        }
    }
}
