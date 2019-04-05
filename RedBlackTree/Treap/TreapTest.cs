using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.DataStructure.Treap
{
    public static class TreapTest
    {
        public static void TestValidation()
        {
            Treap<int, int> tree = new Treap<int, int>();
            Random r = new Random();
            for (int i = 0; i < 25; i++)
            {
                tree.Insert(new TreapNode<int, int>(i, r.Next()));
            }
            tree.Traverse();
            for (int i=10;i<15;i++)
            {
                var p = tree.Search(i);
                tree.Delete(p);
            }
            
            tree.Traverse();
        }
    }
}
