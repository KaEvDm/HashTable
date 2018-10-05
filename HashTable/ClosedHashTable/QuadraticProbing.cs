using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class QuadraticProbing<TKey> : IHashProbing<TKey>
    {
        public int Step(int iteration, TKey key)
        {
            return iteration * iteration;
        }

        // Квадратичное пробирование: интервал между ячейками с каждым шагом увеличивается на константу. 
        // Если размер хеш-таблицы равен степени двойки (N = 2p), то одним из примеров последовательности, 
        // при которой каждый элемент будет просмотрен по одному разу, является:
        // hash(x) mod N, (hash(x) + 1 * 1) mod N, (hash(x) + 2 * 2) mod N, (hash(x) + 3 * 3) mod N, …
    }
}
