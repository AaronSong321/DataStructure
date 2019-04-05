using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.DataStructure.VEBTree
{
    public static class VEBTest
    {
        public static void TestDouble()
        {
            for (uint i = 0; i < 10; i++)
            {
                double k = Math.Pow(2, i);
                Console.WriteLine(Convert.ToUInt32(k));
            }
        }

        public static void TestValidation()
        {
            VEBTree k = new VEBTree(5);
            for (uint i = 0; i < 0x20; i++)
            {
                Console.WriteLine($"Inserting {i}");
                k.Insert(i);
            }
            for (uint i = 0; i < 0x20; i++)
            {
                Console.WriteLine($"Node {i}: IsMember={k.IsMember(i)}, Predecessor={k.Predecessor(i)}, Successor={k.Successor(i)}.");
            }
            uint[] numberToDelete = { 1, 2, 4, 6, 11, 31 };
            foreach (var a in numberToDelete)
                k.Delete(a);
            for (uint i = 0; i < 0x20; i++)
                Console.WriteLine($"Node {i} IsMemeber={k.IsMember(i)}");
        }
    }
}
