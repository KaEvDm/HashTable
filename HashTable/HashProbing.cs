using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    public abstract class HashProbing
    {
        protected int SizeTable { get; set; }
        protected int CurrentHash { get; set; }
        protected int Iteration { get; set; }

        public void Initialize(int hash)
        {
            CurrentHash = hash;
            Iteration = 0;
        }

        public abstract int NextStep();
    }
}
