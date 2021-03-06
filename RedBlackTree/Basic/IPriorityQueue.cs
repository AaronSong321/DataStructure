﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.DataStructure.Basic
{
    public interface IPriorityQueue<TNode>
        where TNode: IComparable<TNode>
    {
        void Enqueue(TNode node);
        void Dequeue();
        TNode Peek();
    }
}
