using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.DataStructure.AVLTree
{
    public class AVLTree<K,V> where K:IComparable<K>
    {
        private AVLNode<K, V> Root { get; set; }

        public uint Height
        {
            get => Root == null ? 0 : Root.Height;
        }

        public AVLTree()
        {
            Root = null;
            throw new NotImplementedException("class AVLTree<K,V> is under consturction. Do not use it until it is completely finished.");
        }

        public uint GetHeight(AVLNode<K,V> node)
        {
            if (node == null) return 0;
            else return node.Height;
        }

        private AVLNode<K,V> LeftLeftRotate(AVLNode<K,V> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            var left = node.LeftChild;
            node.LeftChild = left.RightChild;
            left.RightChild = node;
            node.Height = Math.Max(GetHeight(node.LeftChild), GetHeight(node.RightChild)) + 1;
            left.Height = Math.Max(node.Height, GetHeight(left.LeftChild)) + 1;
            if (node == Root) Root = left;
            return left;
        }
        private AVLNode<K,V> RightRightRotate(AVLNode<K,V> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            var right = node.RightChild;
            node.RightChild = right.LeftChild;
            right.LeftChild = node;
            node.Height = Math.Max(GetHeight(node.LeftChild), GetHeight(node.RightChild)) + 1;
            right.Height = Math.Max(node.Height, GetHeight(right.RightChild)) + 1;
            if (Root == node) Root = right;
            return right;
        }
        private AVLNode<K,V> LeftRightRotate(AVLNode<K,V> node)
        {
            node.LeftChild = RightRightRotate(node.LeftChild);
            return LeftLeftRotate(node);
        }
        private AVLNode<K,V> RightLeftRotate(AVLNode<K,V> node)
        {
            node.RightChild = LeftLeftRotate(node.RightChild);
            return RightRightRotate(node);
        }

        public void Insert(AVLNode<K,V> node)
        {
            if (Root == null)
            {
                Root = node;
                return;
            }
            var ni = node.Index;
            var k = Root;
            AVLNode<K, V> parent = null;
            while (k != null)
            {
                if (ni.CompareTo(k.Index) < 0)
                {
                    parent = k;
                    k = k.LeftChild;
                }
                else if (ni.CompareTo(k.Index) > 0)
                {
                    parent = k;
                    k = k.RightChild;
                }
                else
                    throw new InvalidOperationException($"node {node.Index} already exists.");
            }
            if (parent.Index.CompareTo(node.Index) < 0)
            {
                parent.RightChild = node;
            }
            else
            {
                parent.LeftChild = node;
            }

            //under construction
        }

    }
}
