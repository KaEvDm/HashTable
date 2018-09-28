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

            //Assert.AreEqual(testTable.Search(5), "five");
            //Assert.AreEqual(testTable.Search(300), "three hundred");
            //Assert.IsNull(testTable.Search(10));

            //Console.WriteLine(testTable.Search(300));
            //Console.WriteLine(testTable.Search(5));
            //Console.WriteLine(testTable.Search(10));

            //try { testTable.Add(new Item<int, string>(5, "five")); }
            //catch (ArgumentException e) { Console.WriteLine($"1 add 5: {e.Message}"); }

            //try { testTable.Delete(5); }
            //catch (ArgumentException e) { Console.WriteLine($"2 del 5: {e.Message}"); }

            //try { testTable.Delete(5); }
            //catch (ArgumentException e) { Console.WriteLine($"3 del 5: {e.Message}"); }

            //try { testTable.Delete(4); }
            //catch (ArgumentException e) { Console.WriteLine($"4 del 4: {e.Message}"); }

            //try { testTable.Delete(300); }
            //catch (ArgumentException e) { Console.WriteLine($"5 del 300: {e.Message}"); }

            //try { Assert.IsNull(testTable.Search(300)); }
            //catch (ArgumentException e) { Console.WriteLine($"6 srch 300: {e.Message}"); }

            //try { testTable.Add(new Item<int, string>(300, "three hundred")); }
            //catch (ArgumentException e) { Console.WriteLine($"7 add 300: {e.Message}"); }

            //try { Assert.AreEqual(testTable.Search(300), "three hundred"); }
            //catch (ArgumentException e) { Console.WriteLine($"8 srch 300: {e.Message}"); }

            Console.ReadKey();
        }
    }
}
