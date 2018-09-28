﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace HashTable
{
    public sealed class ClosedHashTable<TKey, TValue> : HashTable<TKey, TValue>
    {
        public override void Add(Item<TKey, TValue> item)
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
