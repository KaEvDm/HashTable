using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            var testTable = new OpenedHashTable<int, string>();

            testTable.Add(5, "five");
            testTable.Add(300, "three hundred");

            Assert.AreEqual(testTable[5], "five");
            Assert.AreEqual(testTable[300], "three hundred");
            Assert.IsNull(testTable[10]);

            Console.WriteLine(testTable[300]);
            Console.WriteLine(testTable[5]);
            Console.WriteLine(testTable[10]);

            try { testTable.Add(5, "five"); }
            catch (ArgumentException e) { Console.WriteLine($"testTable.Add(5, \"five\"): {e.Message}"); }

            testTable.Remove(300);
            Assert.IsNull(testTable[300]);

            testTable.Add(300, "three hundred");

            Assert.AreEqual(testTable[300], "three hundred");

            var classHashTable = new OpenedHashTable<String, String>();

            classHashTable.Add(default(string), "dsfsdfsfds");

            Console.ReadKey();
        }
    }
}
