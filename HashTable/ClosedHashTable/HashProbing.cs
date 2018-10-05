﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    public interface IHashProbing<TKey>
    {
        int Step(int iteration, TKey key);
    }
}
