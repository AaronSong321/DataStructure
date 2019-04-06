using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aaron.DataStructure.BTree
{
    public class BTree<TK>
        where TK:IComparable<TK>
    {
        private const int DiskReadTime = 2;
        private const int DiskWriteTime = 100;
        private static void ReadDisk() => Thread.Sleep(DiskReadTime);
        private static void WriteDisk() => Thread.Sleep(DiskWriteTime);

        private BNode<TK> Root { get; set; }
        public uint MinimumDegree { get; }

        public BTree(uint t)
        {
            var ro = new BNode<TK>(t)
            {
                Leaf = true,
                Count = 0
            };
            WriteDisk();
            Root = ro;
            MinimumDegree = t;
        }



        public (BNode<TK>, int) Search(TK key) => Search(Root, key);
        private (BNode<TK>, int) Search(BNode<TK> node, TK key)
        {
            int i = 0;
            while (i <= node.Count && key.CompareTo(node.Children[i].Index) > 0) i++;
            if (i < node.Count && key.CompareTo(node.Children[i].Index) == 0)
                return (node, i);
            else if (node.Leaf)
                return (null, -1);
            else
            {
                ReadDisk();//Children[i]
                return Search(node.Children[i], key);
            }
        }

        private void SplitChild(BNode<TK> node, int i)
        {
            var y = node.Children[i];
            var z = new BNode<TK>(MinimumDegree)
            {
                Leaf = y.Leaf,
                Count = MinimumDegree - 1
            };
            for (int j=0; j<MinimumDegree-1; j++)
            {
                z.Indexes[j] = y.Indexes[j + (int)MinimumDegree];
            }
            if (!y.Leaf)
                for (int j = 0; j < MinimumDegree; j++)
                    z.Children[j] = y.Children[j + (int)MinimumDegree];
            y.Count = MinimumDegree - 1;
            for (int j = (int)node.Count; j >= i; j--)
                node.Children[j + 1] = node.Children[j];
            node.Children[i + 1] = z;
            for (int j = (int)node.Count - 1; j >= i - 1; j--)
                node.Indexes[j + 1] = node.Indexes[j];
            node.Indexes[i] = y.Indexes[(int)MinimumDegree];
            node.Count++;
            WriteDisk();//y
            WriteDisk();//z
            WriteDisk();//x
        }
        public void Insert(TK key)
        {
            var r = Root;
            if (r.Count == 2 * MinimumDegree - 1)
            {
                var s = new BNode<TK>(MinimumDegree)
                {
                    Leaf = false,
                    Count = 0,
                };
                s.Children[0] = r;
                Root = s;
                SplitChild(s, 0);
                InsertNonfull(s, key);
            }
            else InsertNonfull(r, key);
        }
        private void InsertNonfull(BNode<TK> node, TK key)
        {
            var i = (int)(node.Count - 1);
            if (node.Leaf)
            {
                while (i >= 0 && node.Index.CompareTo(node.Indexes[i]) < 0)
                {
                    node.Indexes[i + 1] = node.Indexes[i];
                    i--;
                }
                node.Indexes[i + 1] = key;
                node.Count++;
                WriteDisk();//node
            }
            else
            {
                while (i >= 0 && node.Index.CompareTo(node.Indexes[i]) < 0) i--;
                i++;
                ReadDisk();//node.Children[i]
                if (node.Children[i].Count == 2 * MinimumDegree - 1)
                {
                    SplitChild(node, i);
                    if (key.CompareTo(node.Children[i].Index) > 0) i++;
                }
                InsertNonfull(node.Children[i], key);
            }
        }
    }
}
