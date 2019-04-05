using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.DataStructure.RBTree
{
    public enum Color { Red, Black }

    public class RedBlackNode<Key, Value> : IComparable<RedBlackNode<Key, Value>>
        where Key : IComparable<Key>
    {

        public Key Index { get; set; }
        public Value Data { get; set; }
        //public uint BlackHeight { get; private set; }
        public RedBlackNode<Key, Value> LeftChild { get; internal set; }
        public RedBlackNode<Key, Value> RightChild { get; internal set; }
        public RedBlackNode<Key, Value> Parent { get; internal set; }
        public Color NodeColor { get; internal set; }

        public RedBlackNode()
        {
            LeftChild = null;
            RightChild = null;
            Parent = null;
            NodeColor = Color.Red;
        }
        public RedBlackNode(Key k, Value v)
        {
            Index = k;
            Data = v;
            LeftChild = null;
            RightChild = null;
            Parent = null;
            NodeColor = Color.Red;
        }

        public int CompareTo(RedBlackNode<Key, Value> other)
        {
            if (other == null) return 1;
            else return Index.CompareTo(other.Index);
        }
        public static bool operator <(RedBlackNode<Key, Value> left, RedBlackNode<Key, Value> right)
            => left.CompareTo(right) < 0;
        public static bool operator >(RedBlackNode<Key, Value> left, RedBlackNode<Key, Value> right)
            => left.CompareTo(right) > 0;

        public override bool Equals(object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (obj is RedBlackNode<Key, Value> a)
                return CompareTo(a) == 0;
            else return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /*
        public static bool operator==(RedBlackNode<Key,Value> left, RedBlackNode<Key,Value> right)
        {
            if (left == null && right == null) return true;
            else if (left == null && right != null) return false;
            else if (left != null && right == null) return false;
            else return left.CompareTo(right) == 0;
        }
        public static bool operator!=(RedBlackNode<Key, Value> left, RedBlackNode<Key, Value> right)
        {
            if (left == null && right == null) return false;
            else if (left == null && right != null) return true;
            else return left.CompareTo(right) != 0;
        }
        */

        public override string ToString()
        {
            //RedBlackNode <Key={nameof(Key)},Value={nameof(Value)}>
            return $"Index={Index.ToString()}, Data={Data.ToString()}";
        }
        public void Print()
        {
            Console.WriteLine(ToString());
        }
    }
}
