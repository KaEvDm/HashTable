using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    public abstract class HashTable<TKey, TValue>
    {
        public abstract void Insert(TValue item);
        public abstract TValue Search(TKey key);
        public abstract void Delete(TKey key);
    }
}
