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

        public override void Add(Item<TKey, TValue> item)
        {
            throw new NotImplementedException();
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
