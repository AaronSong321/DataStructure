using Aaron.DataStructure.RBTree;
using Aaron.DataStructure.Treap;
using Aaron.DataStructure.VEBTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.DataStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            //RedBlackTreeTest.TestValidation();
            //RedBlackTreeTest.TestPerformance(0x8000, 100);
            //RedBlackTreeTest.TestPerformance(0x10000, 100);
            //RedBlackTreeTest.TestPerformance(0x100000, 100);

            //VEBTest.TestDouble();
            //VEBTest.TestValidation();

            TreapTest.TestValidation();

            Console.ReadLine();
        }
    }
}
