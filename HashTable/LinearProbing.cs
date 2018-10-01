using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class LinearProbing : HashProbing
    {
        public int IntervalBetweenProbes { get; private set; } = 1;

        public LinearProbing(int k, int size)
        {
            IntervalBetweenProbes = k;
            SizeTable = size;
        }

        public override int NextStep()
        {
            if(Iteration < SizeTable)
            {
                var adress = (CurrentHash + Iteration * IntervalBetweenProbes) % SizeTable;
                Iteration++;
                return adress;
            }
            else
            {
                // необходимо перестроить таблицу с новым размером.
                return 1;
            }
        }

        // Линейное пробирование: ячейки хеш-таблицы последовательно просматриваются с 
        // некоторым фиксированным интервалом k между ячейками (обычно k = 1), то есть i-й элемент
        // последовательности проб — это ячейка с номером (hash(x) + ik) mod N. Для того, чтобы все ячейки 
        // оказались просмотренными по одному разу, необходимо, чтобы k было взаимно-простым с размером хеш-таблицы.
    }
}
