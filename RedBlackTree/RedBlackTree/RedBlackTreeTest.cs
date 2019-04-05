using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.DataStructure.RBTree
{
    public static class RedBlackTreeTest
    {
        public static void TestValidation()
        {
            RedBlackTree<int, double> tree = new RedBlackTree<int, double>();
            var random = new Random();
            for (int i=0; i<100; i++)
            {
                tree.Insert(new RedBlackNode<int, double>(i, random.NextDouble()));
            }
            tree.Traverse();
        }

        public static void TestPerformance(int dataSize, int testSize)
        {
            int totalSize = dataSize + testSize;
            RedBlackTree<int, int> tree = new RedBlackTree<int, int>();
            var random = new Random();
            var list = new List<RedBlackNode<int, int>>();
            for (int i = 0; i < totalSize; i++)
            {
                list.Append(new RedBlackNode<int, int>(i, BitConverter.ToInt32(new Guid().ToByteArray(), 0)));
            }
            list.Sort((a, b) => a.Data - b.Data);

            for (int i = 0; i < dataSize; i++)
            {
                tree.Insert(new RedBlackNode<int, int>(i, random.Next()));
            }

            var curtime = DateTime.Now;
            for (int i = dataSize; i < totalSize; i++)
            {
                tree.Insert(new RedBlackNode<int, int>(i, random.Next()));
            }
            var timeElapsed = DateTime.Now - curtime;
            Console.WriteLine($"Data size = {dataSize}, test size = {testSize}, test method = {nameof(RedBlackTree<int, int>.Insert)}, time elapsed = {timeElapsed.Milliseconds}");

            var watch = new Stopwatch();
            watch.Start();
            for (int i = dataSize; i < dataSize + testSize; i++)
            {
                var no = tree.Search(i);
                tree.Delete(no);
            }
            watch.Stop();
            Console.WriteLine($"Data size = {dataSize}, test size = {testSize}, test method = {nameof(RedBlackTree<int, int>.Search)} + {nameof(RedBlackTree<int, int>.Delete)}, time elapsed = {watch.ElapsedMilliseconds}");
        }

        public static void TestPerformance()
        {
            TestPerformance(0x1000, 100);
            TestPerformance(0x2000, 100);
            TestPerformance(0x4000, 100);
            TestPerformance(0x8000, 100);
            TestPerformance(0x10000, 100);
        }
    }
}
