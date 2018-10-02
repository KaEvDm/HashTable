using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    public class HashProbing<TKey>
    {
        private readonly int SecretHashCode = "Chked".GetHashCode();
        private readonly int IntervalBetweenProbes;

        public HashProbing() { }

        public HashProbing(int intervalBetweenProbes)
        {
            IntervalBetweenProbes = intervalBetweenProbes;
        }

        public int LinearStep(int iteration, int size, TKey key)
        {
            return iteration * IntervalBetweenProbes;
        }

        public int QuadraticStep(int iteration, int size, TKey key)
        {
            return iteration * iteration;
        }

        public int DoubleHashingStep(int iteration, int size, TKey key)
        {
            return iteration * GetHash(key, size);
        }

        private int GetHash(TKey key, int size) => ((key.GetHashCode() ^ SecretHashCode) % (size - 1)) + 1;
    }
}
