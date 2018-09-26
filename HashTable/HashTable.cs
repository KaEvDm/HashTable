using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    public abstract class HashTable
    {
        public abstract void Insert(Item item);
        public abstract Item Search(int key);
        public abstract void Delete(int key);
    }
}
