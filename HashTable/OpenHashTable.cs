using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    public class OpenHashTable<TKey, TValue> : HashTable<TKey, TValue>
    {
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
