using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    public abstract class HashTable<THash, TKey, TValue>
    {
        public abstract void Add(Item<TKey, TValue> item);
        public abstract TValue Search(TKey key);
        public abstract void Delete(TKey key);
    }
}
