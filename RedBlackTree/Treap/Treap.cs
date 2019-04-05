using Aaron.DataStructure.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.DataStructure.Treap
{
    public class Treap<TK,TP>:IHeap<TreapNode<TK,TP>>, IBinaryTree<TreapNode<TK,TP>, TK>, IPriorityQueue<TreapNode<TK,TP>>
        where TK:IComparable<TK>
        where TP:IComparable<TP>
    {
        private TreapNode<TK, TP> Root;

        public Treap()
        {
            Root = null;
        }

        private void LeftRotate(TreapNode<TK,TP> node)
        {
            var rightChild = node.RightChild;
            node.RightChild = rightChild.LeftChild;
            if (node.RightChild != null) node.RightChild.Parent = node;
            rightChild.Parent = node.Parent;
            rightChild.LeftChild = node;
            node.Parent = rightChild;
            if (rightChild.Parent != null)
            {
                if (rightChild.Parent.LeftChild == node)
                    rightChild.Parent.LeftChild = rightChild;
                else
                    rightChild.Parent.RightChild = rightChild;
            }
            else
                Root = rightChild;
        }
        private void RightRotate(TreapNode<TK, TP> node)
        {
            var leftChild = node.LeftChild;
            node.LeftChild = leftChild.RightChild;
            if (node.LeftChild != null) node.LeftChild.Parent = node;
            leftChild.Parent = node.Parent;
            leftChild.RightChild = node;
            node.Parent = leftChild;
            if (leftChild.Parent != null)
            {
                if (leftChild.Parent.LeftChild == node)
                    leftChild.Parent.LeftChild = leftChild;
                else
                    leftChild.Parent.RightChild = leftChild;
            }
            else
                Root = leftChild;
        }

        public TreapNode<TK,TP> Search(TK key)
        {
            TreapNode<TK, TP> ans = Root;
            while (ans != null)
            {
                var a = key.CompareTo(ans.Index);
                if (a == 0) return ans;
                else if (a < 0) ans = ans.LeftChild;
                else ans = ans.RightChild;
            }
            return null;
        }

        public void Insert(TreapNode<TK,TP> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (Root == null)
            {
                Root = node;
                return;
            }
            var r = Root;
            while (true)
            {
                if (node.CompareTo(r) < 0)
                {
                    if (r.LeftChild == null)
                    {
                        r.LeftChild = node;
                        node.Parent = r;
                        break;
                    }
                    else
                        r = r.LeftChild;
                }
                else
                {
                    if (r.RightChild == null)
                    {
                        r.RightChild = node;
                        node.Parent = r;
                        break;
                    }
                    else
                        r = r.RightChild;
                }
            }

            while (node.Parent != null && node.Priority.CompareTo(node.Parent.Priority) < 0)
            {
                if (node.Parent.LeftChild == node)
                {
                    RightRotate(node.Parent);
                }
                else
                {
                    LeftRotate(node.Parent);
                }
            }
        }

        public void Delete(TreapNode<TK,TP> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            while (true)
            {
                if (node.LeftChild != null && node.RightChild != null)
                {
                    if (node.LeftChild.Priority.CompareTo(node.RightChild.Priority) < 0)
                        RightRotate(node);
                    else
                        LeftRotate(node);
                }
                else if (node.LeftChild != null)
                    RightRotate(node);
                else if (node.RightChild != null)
                    LeftRotate(node);
                else break;
            }
            var parent = node.Parent;
            if (parent != null)
            {
                if (parent.LeftChild == node)
                    parent.LeftChild = null;
                else parent.RightChild = null;
            }
            else
            {
                if (Root != node) throw new Exception("Something is wrong.");
                else Root = null;
            }
        }

        public void Traverse()
        {
            if (Root != null) Traverse(Root);
        }
        public void Traverse(TreapNode<TK,TP> node)
        {
            if (node.LeftChild != null)
                Traverse(node.LeftChild);
            Console.WriteLine($"node {node.Index}: Priority={node.Priority}");
            if (node.RightChild != null)
                Traverse(node.RightChild);
        }

        public void Pop() => Delete(Root);
        public void Push(TreapNode<TK, TP> node) => Insert(node);
        public TreapNode<TK, TP> Top() => Root;

        public void Enqueue(TreapNode<TK, TP> node) => Insert(node);
        public void Dequeue() => Delete(Root);
        public TreapNode<TK, TP> Peek() => Root;
    }
}
