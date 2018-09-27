using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    public class ClosedHashTable<THash, TKey, TValue> : HashTable<THash, TKey, TValue>
    {
        private Dictionary<TKey, List<Item<TKey, TValue>>> keyValuePairs;

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
