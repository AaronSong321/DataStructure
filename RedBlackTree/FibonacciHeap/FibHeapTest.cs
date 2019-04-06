using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.DataStructure.FibonacciHeap
{
    public static class FibHeapTest
    {
        public static void TestValidation()
        {
            FibonacciHeap<int> a = new FibonacciHeap<int>();
            //var radonGen = new Random();
            for (int i = 0; i < 30; i++)
            {
                a.Insert(i);
            }
            /*
            int[] deleteList = new int[10] { 8, 20, 3, 5, 6, 7, 8, 13, 29, 15 };
            for (int i = 0; i < 3; i++)
            {
                var minNode = a.Minimum();
                Console.WriteLine($"Deleting Minimum from {minNode.Key} to {minNode.Key - deleteList[i]}");
                a.DecreaseKey(minNode, minNode.Key - deleteList[i]);
            }
            for (int i = 0; i < 5; i++) a.ExtractMin();
            */

            var b = new FibonacciHeap<int>();
            for (int i = 0; i < 10; i++)
            {
                var m = a.ExtractMin();
                Console.WriteLine($"Extract minimum node {m.Key}");
                b.Insert(m.Key + 3);
            }
            b.PrintRoot();

            var c = a + b;
            c.PrintRoot();
            Console.WriteLine("c has:");
            for (int i = 0; i < c.Count; i++)
            {
                Console.Write($" {c.ExtractMin().Key}");
            }
        }
    }
}
