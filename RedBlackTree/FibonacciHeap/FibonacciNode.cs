using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.DataStructure.FibonacciHeap
{
    public class FibonacciNode<TK> : IComparable<FibonacciNode<TK>>, IEnumerable<FibonacciNode<TK>>
        where TK : IComparable<TK>
    {
        public TK Key { get; internal set; }
        internal FibonacciNode<TK> Left { get; set; }
        internal FibonacciNode<TK> Right { get; set; }
        internal FibonacciNode<TK> Child { get; set; }
        internal FibonacciNode<TK> Parent { get; set; }
        internal int Degree { get; set; }
        internal bool Mark { get; set; }
        internal bool MinimumCausedByDeletion { get; set; }


        public FibonacciNode(TK key)
        {
            Key = key;
            Left = null;
            Right = null;
            Child = null;
            Parent = null;
            Degree = 0;
            Mark = false;
            MinimumCausedByDeletion = false;
        }

        public int CompareTo(FibonacciNode<TK> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (MinimumCausedByDeletion)
            {
                if (node.MinimumCausedByDeletion) throw new Exception($"Both of the operators are minimum. Somthing wrong in the code.");
                else return int.MinValue;
            }
            else if (node.MinimumCausedByDeletion) return int.MaxValue;
            return Key.CompareTo(node.Key);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is FibonacciNode<TK> node)
                return CompareTo(node) == 0;
            else return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator <(FibonacciNode<TK> left, FibonacciNode<TK> right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));
            return left.CompareTo(right) < 0;
        }
        public static bool operator >(FibonacciNode<TK> left, FibonacciNode<TK> right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));
            return left.CompareTo(right) > 0;
        }

        internal void AddChild(FibonacciNode<TK> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (node.Parent == this) throw new InvalidOperationException($"The node is already a child of this node.");
            if (Child == null)
            {
                Child = node;
                node.Left = node;
                node.Right = node;
            }
            else if (Child.Right == Child)
            {
                Child.Right = node;
                Child.Left = node;
                node.Right = Child;
                node.Left = Child;
            }
            else
            {
                Child.Right.Left = node;
                node.Right = Child.Right;
                Child.Right = node;
                node.Left = Child;
            }
            node.Parent = this;
            Degree++;
        }

        internal void DeleteChild(FibonacciNode<TK> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (node.Parent != this) throw new InvalidOperationException($"{nameof(node)} is not a child node");
            if (Child.Left == Child && Child == node)
            {
                Child = null;
            }
            else
            {
                node.Left.Right = node.Right;
                node.Right.Left = node.Left;
                if (Child == node)
                    Child = node.Right;
            }
            node.Parent = null;
            node.Left = null;
            node.Right = null;
            Degree--;
        }

        public IEnumerator<FibonacciNode<TK>> GetEnumerator()
        {
            if (Child != null)
            {
                int i = 0;
                var b = Child;
                while (i < Degree)
                {
                    yield return b;
                    b = b.Right;
                    i++;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
