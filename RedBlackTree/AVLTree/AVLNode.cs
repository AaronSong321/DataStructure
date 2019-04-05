using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.DataStructure.AVLTree
{
    public class AVLNode<K, V> : IComparable<AVLNode<K, V>>
        where K : IComparable<K>
    {
        public K Index { get; set; }
        public V Data { get; set; }
        public AVLNode<K, V> LeftChild { get; set; }
        public AVLNode<K, V> RightChild { get; set; }
        public uint Height { get; set; }

        public AVLNode()
        {
            LeftChild = null;
            RightChild = null;
            Height = 0;
        }
        public AVLNode(K k, V v)
        {
            Index = k;
            Data = v;
            LeftChild = null;
            RightChild = null;
            Height = 0;
        }

        public int CompareTo(AVLNode<K,V> other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return Index.CompareTo(other.Index);
        }

        public static bool operator <(AVLNode<K, V> left, AVLNode<K, V> right)
        {
            return left.CompareTo(right) < 0;
        }
        public static bool operator >(AVLNode<K, V> left, AVLNode<K, V> right)
        {
            return left.CompareTo(right) > 0;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (obj is AVLNode<K, V> avl)
                return CompareTo(avl) == 0;
            else return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return $"Index={Index.ToString()}, Data={Data.ToString()}";
        }
    }
}
