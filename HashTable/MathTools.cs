using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    // !!! No Tested !!!
    public static class MathTools
    {
        public static bool CoPrime(int a, int b)
        {
            for (int i = 2; i <= Math.Min(a, b); i++)
            {
                if (a % i == 0 && b % i == 0) return false;
            }
            return true;
        }

        public static bool IsPrime(int a)
        {
            for (int i = 2; i <= a / 2; i++)
            {
                if (a % i == 0) return false;
            }
            return true;
        }

        public static int GetPrime(int startValue, int endValue = int.MaxValue)
        {
            for (int i = startValue; i < endValue; i++)
            {
                if (IsPrime(i)) return i;
            }
            return -1;
        }

        public static int GetCoPrime(int a, int startValue, int endValue = int.MaxValue)
        {
            for (int i = startValue; i < endValue; i++)
            {
                if (CoPrime(a, i)) return i;
            }
            return -1;
        }
    }
}
