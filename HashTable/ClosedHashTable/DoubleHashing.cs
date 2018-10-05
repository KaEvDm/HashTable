using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class DoubleHashing<TKey> : IHashProbing<TKey>
    {
        private readonly int SecretHashCode = "Chked".GetHashCode();
        private int TableSize { get; set; }

        public DoubleHashing(int size)
        {
            TableSize = size;
        }

        public int Step(int iteration, TKey key)
        {
            return iteration * GetHash(key);
        }

        private int GetHash(TKey key) => Math.Abs(((key.GetHashCode() ^ SecretHashCode) % (TableSize - 1))) + 1;

        // Двойное хеширование: интервал между ячейками фиксирован, как при линейном пробировании,
        // но, в отличие от него, размер интервала вычисляется второй, вспомогательной хеш-функцией, 
        // а значит, может быть различным для разных ключей. Значения этой хеш-функции должны быть 
        // ненулевыми и взаимно-простыми с размером хеш-таблицы, что проще всего достичь, взяв простое 
        // число в качестве размера, и потребовав, чтобы вспомогательная хеш-функция принимала 
        // значения от 1 до N — 1
    }
}
