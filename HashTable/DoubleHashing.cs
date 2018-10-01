using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class DoubleHashing : HashProbing
    {
        private int SecretHashCode = "Chked".GetHashCode();
        public override int NextStep()
        {
            if (Iteration < SizeTable) 
            {
                var adress = (CurrentHash ^ SecretHashCode) % SizeTable;
                Iteration++;
                return adress;
            }
            else
            {
                // необходимо перестроить таблицу с новым размером.
                return 1;
            }
        }

        // Двойное хеширование: интервал между ячейками фиксирован, как при линейном пробировании,
        // но, в отличие от него, размер интервала вычисляется второй, вспомогательной хеш-функцией, 
        // а значит, может быть различным для разных ключей. Значения этой хеш-функции должны быть 
        // ненулевыми и взаимно-простыми с размером хеш-таблицы, что проще всего достичь, взяв простое 
        // число в качестве размера, и потребовав, чтобы вспомогательная хеш-функция принимала 
        // значения от 1 до N — 1
    }
}
