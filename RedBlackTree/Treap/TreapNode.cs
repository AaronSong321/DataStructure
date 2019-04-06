using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.DataStructure.Treap
{
    public class TreapNode<TK, TP>:IComparable<TreapNode<TK,TP>>
        where TK:IComparable<TK>
        where TP:IComparable<TP>
    {
        public TK Index { get; private set; }
        public TP Priority { get; private set; }
        public TreapNode<TK, TP> LeftChild { get; set; }
        public TreapNode<TK, TP> RightChild { get; set; }
        public TreapNode<TK, TP> Parent { get; set; }

        public TreapNode(TK index, TP priority)
        {
            Index = index;
            Priority = priority;
            LeftChild = null;
            RightChild = null;
            Parent = null;
        }

        public int CompareTo(TreapNode<TK,TP> other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return Index.CompareTo(other.Index);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is TreapNode<TK, TP> a)
                return CompareTo(a) == 0;
            else return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
