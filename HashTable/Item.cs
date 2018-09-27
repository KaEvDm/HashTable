using System;

namespace HashTable
{
    public class Item<TKey, TValue>
    {
        public TKey Key { get; }
        public TValue Value { get; }

        public Item(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
