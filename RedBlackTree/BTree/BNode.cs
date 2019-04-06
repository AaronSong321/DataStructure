using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aaron.DataStructure.BTree
{
    public class BNode<TK>:IComparable<BNode<TK>>
        where TK:IComparable<TK>
    {
        public TK Index { get; }
        //public TD Data { get; }
        public uint Count { get; set; }
        public TK[] Indexes { get; }
        public bool Leaf { get; set; }
        public uint MinimumDegree { get; }
        public BNode<TK>[] Children { get; }

        /*
        internal async Task<BNode<TK,TD>> GetChildren(int index)
        {
            if (index >= N || index < 0)
                throw new IndexOutOfRangeException($"{nameof(index)}={index}, bound=0..{N - 1}");
            await Task.Run(() => Thread.Sleep(DiskReadTime));
            return children[index];
        }
        internal async Task SetChildren(int index, BNode<TK,TD> node)
        {
            if (index >= N || index < 0)
                throw new IndexOutOfRangeException($"{nameof(index)}={index}, bound=0..{N - 1}");
            await Task.Run(() => Thread.Sleep(DiskWriteTime));
            children[index] = node;
        }
        public BNode<TK, TD> GetChildren(int index)
        {
            if (index >= N || index < 0)
                throw new IndexOutOfRangeException($"{nameof(index)}={index}, bound=0..{N - 1}");
            Thread.Sleep(DiskReadTime);
            return children[index];
        }
        public void SetChildren(BNode<TK,TD> node, int index)
        {
            if (index >= N || index < 0)
                throw new IndexOutOfRangeException($"{nameof(index)}={index}, bound=0..{N - 1}");
            Thread.Sleep(DiskWriteTime);
            children[index] = node;
        }
        public BNode<TK,TD> this[int index]
        {
            get => GetChildren(index);
            set => SetChildren(value, index);
        }
        */

        public BNode(uint t)
        {
            Indexes = new TK[2 * t - 1];
            Children = new BNode<TK>[2 * t];
        }


        public int CompareTo(BNode<TK> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            return Index.CompareTo(node.Index);
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is BNode<TK> b)
                return CompareTo(b) == 0;
            else return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


    }
}
