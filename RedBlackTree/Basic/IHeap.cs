using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.DataStructure.Basic
{
    public interface IHeap<TNode>
        where TNode:IComparable<TNode>
    {
        void Push(TNode node);
        TNode Top();
        void Pop();
    }
}
