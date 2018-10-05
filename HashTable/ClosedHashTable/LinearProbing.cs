using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class LinearProbing<TKey> : IHashProbing<TKey>
    {
        public readonly int IntervalBetweenProbes;

        public LinearProbing(int intervalBetweenProbes)
        {
            IntervalBetweenProbes = intervalBetweenProbes;
        }

        public int Step(int iteration, TKey key)
        {
            return IntervalBetweenProbes * iteration;
        }

        // Линейное пробирование: ячейки хеш-таблицы последовательно просматриваются с 
        // некоторым фиксированным интервалом k между ячейками (обычно k = 1), то есть i-й элемент
        // последовательности проб — это ячейка с номером (hash(x) + ik) mod N. Для того, чтобы все ячейки 
        // оказались просмотренными по одному разу, необходимо, чтобы k было взаимно-простым с размером хеш-таблицы.
    }
}
