using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            var testTable = new ClosedHashTable<int, string>(ProbingType.Linear, 2);


            testTable.Add(1, "1");
            testTable.Add(256, "256");
            testTable.Add(543, "543");
            testTable.Add(798, "798");
            testTable.Add(1029, "1029");

            testTable.Add(2, "2");
            testTable.Add(257, "257");
            testTable.Add(512, "512");
            testTable.Add(799, "799");
            testTable.Add(1030, "1030");

            testTable.Remove(798);

            for (int i = 3; i < 300; i++)
            {
                if(i!= 256 && i!= 257)
                testTable.Add(i, i.ToString());
            }

            testTable.PrintTable();

            for (int i = 3; i < 300; i++)
            {
                if (i != 256 && i != 257)
                    testTable.Remove(i);
            }

            testTable.PrintTable();

            Console.WriteLine(testTable[257]);
            Console.WriteLine(testTable[798]);

            Console.ReadKey();
        }
    }
}
