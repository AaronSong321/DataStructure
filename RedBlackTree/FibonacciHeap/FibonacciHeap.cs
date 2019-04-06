using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.DataStructure.FibonacciHeap
{
    public class FibonacciHeap<TK>
        where TK:IComparable<TK>
    {
        private FibonacciNode<TK> Min { get; set; }

        private void AddToRootList(FibonacciNode<TK> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (Min == null)
            {
                Min = node;
                node.Left = node.Right = node;
            }
            else
            {
                Min.Left.Right = node;
                node.Left = Min.Left;
                Min.Left = node;
                node.Right = Min;
            }
            node.Parent = null;
        }
        private void RemoveFromRootList(FibonacciNode<TK> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (Min.Left == Min)
            {
                if (node != Min) throw new Exception("The node you are removing from the root list is something wrong.");
                Min = null;
            }
            else
            {
                node.Left.Right = node.Right;
                node.Right.Left = node.Left;
            }
            node.Parent = null;
        }
        public int Count { get; private set; }

        

        public FibonacciHeap()
        {
            Min = null;
            Count = 0;
        }

        public void Insert(TK key)
        {
            Insert(new FibonacciNode<TK>(key));
        }
        public void Insert(FibonacciNode<TK> node)
        {
            if (Min == null)
            {
                AddToRootList(node);
                Min = node;
            }
            else
            {
                AddToRootList(node);
                if (node.CompareTo(Min) < 0)
                    Min = node;
            }
            Count++;
        }

        /*
         * I want to declare an IEnumerator<> method for the RootList, so I can write 
         * foreach (var root in RootList)
         * but it did not work
         * cause you had to implement the interface explicitly and write a method named GetEnumerator()
         * 
        private IEnumerator<FibonacciNode<TK>> RootList()
        {
            if (Min != null)
            {
                int a = 0;
                var b = Min;
                while (a < Count)
                {
                    yield return b;
                    b = b.Right;
                    a++;
                }
            }
        }
        */

        public static FibonacciHeap<TK> Union(FibonacciHeap<TK> a, FibonacciHeap<TK> b)
        {
            FibonacciHeap<TK> ans = new FibonacciHeap<TK>
            {
                Min = a.Min
            };
            if (b.Min != null)
            {
                var curIt = b.Min;
                var initRoot = b.Min;
                a.AddToRootList(curIt);
                curIt = curIt.Right;
                while (curIt != initRoot)
                {
                    a.AddToRootList(curIt);
                    curIt = curIt.Right;
                }
            }
            //which means:
            //foreach (var n in b.RootList())
            //    a.RootList.AddLast(n);
            if (a.Min == null || (b.Min != null && b.Min.CompareTo(a.Min) < 0))
                ans.Min = b.Min;
            ans.Count = a.Count + b.Count;
            //delete a;
            //delete b;
            //if it was in C++..., a and b should be disposed by now
            //nothing is gonna happen if you said "a=null; b=null;", cause they are references, not references to pointers
            return ans;
        }
        public static FibonacciHeap<TK> operator +(FibonacciHeap<TK> a, FibonacciHeap<TK> b)
            => Union(a, b);

        public FibonacciNode<TK> Minimum() => Min;

        public FibonacciNode<TK> ExtractMin()
        {
            var z = Min;
            if (z != null)
            {
                foreach (var child in z)
                {
                    AddToRootList(child);
                    child.Parent = null;
                }
                RemoveFromRootList(z);
                if (z == z.Right)
                    Min = null;
                else
                {
                    Console.WriteLine($"{z.Key} removed. Ready to Consolidate");
                    Min = z.Right;
                    Consolidate();
                }
                Count--;
            }
            return z;
        }
        private void Consolidate()
        {
            FibonacciNode<TK>[] a = new FibonacciNode<TK>[Count+1];
            for (int i = 0; i <= Count; i++)
                a[i] = null;
            //Assert Min!=null;
            var w = Min;
            var initRoot = Min;
            
            do
            {
                var x = w;
                w = w.Right;
                Console.WriteLine($"Consolidating root {w.Key}, Degree={w.Degree}");
                var d = x.Degree;
                while (a[d] != null)
                {
                    var y = a[d];
                    if (x.CompareTo(y) > 0)
                    {
                        var t = x; x = y; y = t;
                    }
                    Console.WriteLine($"Linking node {y.Key} to {x.Key}");
                    Link(y, x);
                    a[d] = null;
                    d++;
                }
                a[d] = x;

            } while (w != initRoot);


            Min = null;
            for (int i = 0; i <= Count; i++)
            {
                if (a[i] != null)
                {
                    if (Min == null)
                    {
                        AddToRootList(a[i]);
                        Min = a[i];
                    }
                    else
                    {
                        AddToRootList(a[i]);
                        if (a[i].CompareTo(Min) < 0)
                            Min = a[i];
                    }
                }
            }
        }
        private void Link(FibonacciNode<TK> y, FibonacciNode<TK> x)
        {
            RemoveFromRootList(y);
            x.AddChild(y);
            y.Mark = false;
        }

        public void DecreaseKey(FibonacciNode<TK> x, TK k)
        {
            if (k.CompareTo(x.Key) > 0)
                throw new InvalidOperationException($"new key {k} is greater than the current key {x.Key}");
            x.Key = k;
            var y = x.Parent;
            if (y != null && x.CompareTo(y) < 0)
            {
                Cut(x, y);
                CascadingCut(y);
            }
            if (x.CompareTo(Min) < 0)
                Min = x;
        }
        private void Cut(FibonacciNode<TK> x, FibonacciNode<TK> y)
        {
            y.DeleteChild(x);
            AddToRootList(x);
            x.Parent = null;
            x.Mark = false;
        }
        private void CascadingCut(FibonacciNode<TK> y)
        {
            var z = y.Parent;
            if (z != null)
            {
                if (!y.Mark)
                    y.Mark = true;
                else
                {
                    Cut(y, z);
                    CascadingCut(z);
                }
            }
        }

        public void Delete(FibonacciNode<TK> x)
        {
            x.MinimumCausedByDeletion = true;
            DecreaseKey(x, x.Key);
            x.MinimumCausedByDeletion = false;
            ExtractMin();
        }

        internal void PrintRoot()
        {
            var initRoot = Min;
            if (Min == null) return;
            var b = Min;
            int rootCount = 0;
            Console.Write("Root:");
            do
            {
                Console.Write($" {b.Key}");
                b = b.Right;
                rootCount++;
            } while (b != initRoot);
            Console.WriteLine($"\n{rootCount} in total.");
        }
    }
}
