using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.DataStructure.Basic
{
    public interface IBinaryTree<TNode, TK>
        where TK:IComparable<TK>
        where TNode:IComparable<TNode>
    {
        void Insert(TNode node);
        TNode Search(TK key);
        void Delete(TNode node);
    }
}
