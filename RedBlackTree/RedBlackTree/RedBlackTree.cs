using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.DataStructure.RBTree
{
    public class RedBlackTree<Key, Value>
           where Key:IComparable<Key>
    {
        private RedBlackNode<Key,Value> Root { get; set; }
        private RedBlackNode<Key, Value> Sentinel { get; set; }

        public uint BlackHeight
        {
            get
            {
                RedBlackNode<Key, Value> node = Root;
                uint ans = 0;
                if (Root == Sentinel) return ans;
                else ans++;
                while (node.LeftChild != Sentinel)
                {
                    node = node.LeftChild;
                    if (node.NodeColor == Color.Black) ++ans;
                }
                return ans;
            }
        }

        public RedBlackTree()
        {
            Sentinel = new RedBlackNode<Key, Value>();
            Root = Sentinel;
            Sentinel.NodeColor = Color.Black;
        }

        public RedBlackNode<Key,Value> Search(Key key)
        {
            var ans = Root;
            while (ans != Sentinel)
            {
                var b = key.CompareTo(ans.Index);

                if (b == 0)
                {
                    return ans;
                }
                else if (b < 0)
                {
                    ans = ans.LeftChild;
                }
                else
                {
                    ans = ans.RightChild;
                }
            }
            return null;
        }

        public RedBlackNode<Key,Value> Minimum(RedBlackNode<Key,Value> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (node == Sentinel) return Sentinel;
            while (node.LeftChild != Sentinel)
            {
                node = node.LeftChild;
            }
            return node;
        }

        private void LeftRotate(RedBlackNode<Key,Value> node)
        {
            RedBlackNode<Key, Value> ersatz = node.RightChild;
            node.RightChild = ersatz.LeftChild;
            if (ersatz.LeftChild != Sentinel)
            {
                ersatz.LeftChild.Parent = node;
            }
            ersatz.Parent = node.Parent;
            if (node.Parent == Sentinel)
            {
                Root = ersatz;
            }
            else if (node.Parent.LeftChild == node)
            {
                node.Parent.LeftChild = ersatz;
            }
            else
            {
                node.Parent.RightChild = ersatz;
            }
            ersatz.LeftChild = node;
            node.Parent = ersatz;
        }
        private void RightRotate(RedBlackNode<Key,Value> node)
        {
            RedBlackNode<Key, Value> bulge = node.LeftChild;
            node.LeftChild = bulge.RightChild;
            if (bulge.RightChild != Sentinel)
            {
                bulge.RightChild.Parent = node;
            }
            bulge.Parent = node.Parent;
            if (node.Parent == Sentinel)
            {
                Root = bulge;
            }
            else if (node.Parent.LeftChild == node)
            {
                node.Parent.LeftChild = bulge;
            }
            else
            {
                node.Parent.RightChild = bulge;
            }
            bulge.RightChild = node;
            node.Parent = bulge;
        }

        public void Insert(RedBlackNode<Key,Value> node)
        {
            RedBlackNode<Key, Value> dupe = Sentinel;
            var blather = Root;
            while (blather != Sentinel)
            {
                dupe = blather;
                if (node<blather)
                {
                    blather = blather.LeftChild;
                }
                else
                {
                    blather = blather.RightChild;
                }
            }
            node.Parent = dupe;
            if (dupe == Sentinel)
            {
                Root = node;
            }
            else if (node < dupe)
            {
                dupe.LeftChild = node;
            }
            else
            {
                dupe.RightChild = node;
            }
            node.LeftChild = Sentinel;
            node.RightChild = Sentinel;
            node.NodeColor = Color.Red;
            InsertFixup(node);
        }
        private void InsertFixup(RedBlackNode<Key,Value> node)
        {
            while (node.Parent.NodeColor == Color.Red)
            {
                if (node.Parent.Parent.LeftChild == node.Parent)
                {
                    var sequitur = node.Parent.Parent.RightChild;
                    if (sequitur.NodeColor == Color.Red)
                    {
                        sequitur.NodeColor = Color.Black;
                        node.Parent.NodeColor = Color.Black;
                        node.Parent.Parent.NodeColor = Color.Red;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node.Parent.RightChild == node)
                        {
                            node = node.Parent;
                            LeftRotate(node);
                        }
                        node.Parent.NodeColor = Color.Black;
                        node.Parent.Parent.NodeColor = Color.Red;
                        RightRotate(node.Parent.Parent);
                    }
                }
                else
                {
                    var sue = node.Parent.Parent.LeftChild;
                    if (sue.NodeColor == Color.Red)
                    {
                        sue.NodeColor = Color.Black;
                        node.Parent.NodeColor = Color.Black;
                        node.Parent.Parent.NodeColor = Color.Red;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node.Parent.LeftChild == node)
                        {
                            node = node.Parent;
                            RightRotate(node);
                        }
                        node.Parent.NodeColor = Color.Black;
                        node.Parent.Parent.NodeColor = Color.Red;
                        LeftRotate(node.Parent.Parent);
                    }
                }
            }
            Root.NodeColor = Color.Black;
        }

        private void Transplant(RedBlackNode<Key,Value> desolate, RedBlackNode<Key,Value> trans)
        {
            if (desolate.Parent == Sentinel)
            {
                Root = trans;
            }
            else if (desolate.Parent.LeftChild == desolate)
            {
                desolate.Parent.LeftChild = trans;
            }
            else
            {
                desolate.Parent.RightChild = trans;
            }
            trans.Parent = desolate.Parent;
        }
        public void Delete(RedBlackNode<Key,Value> z)
        {
            if (z == null) throw new ArgumentNullException(nameof(z));
            var y = z;
            var desiccate = y.NodeColor;
            RedBlackNode<Key, Value> x;
            if (z.LeftChild == Sentinel)
            {
                x = z.RightChild;
                Transplant(z, z.RightChild);
            }
            else if (z.RightChild == Sentinel)
            {
                x = z.LeftChild;
                Transplant(z, z.LeftChild);
            }
            else
            {
                y = Minimum(z.RightChild);
                desiccate = y.NodeColor;
                x = y.RightChild;
                if (y.Parent == z)
                {
                    x.Parent = y;
                }
                else
                {
                    Transplant(y, y.RightChild);
                    y.RightChild = z.RightChild;
                    y.RightChild.Parent = y;
                }
                Transplant(z, y);
                y.LeftChild = z.LeftChild;
                y.LeftChild.Parent = y;
                y.NodeColor = z.NodeColor;
            }
            if (desiccate == Color.Black)
            {
                DeleteFixup(x);
            }
        }
        private void DeleteFixup(RedBlackNode<Key,Value> node)
        {
            while (node != Root && node.NodeColor == Color.Black)
            {
                if (node == node.Parent.LeftChild)
                {
                    var sibling = node.Parent.RightChild;
                    if (sibling.NodeColor == Color.Red)
                    {
                        sibling.NodeColor = Color.Black;
                        node.Parent.NodeColor = Color.Red;
                        LeftRotate(node.Parent);
                        sibling = node.Parent.RightChild;
                    }
                    if (sibling.LeftChild.NodeColor == Color.Black &&
                        sibling.RightChild.NodeColor == Color.Black)
                    {
                        sibling.NodeColor = Color.Red;
                        node = node.Parent;
                    }
                    else
                    {
                        if (sibling.RightChild.NodeColor == Color.Black)
                        {
                            sibling.LeftChild.NodeColor = Color.Black;
                            sibling.NodeColor = Color.Red;
                            RightRotate(sibling);
                            sibling = node.Parent.RightChild;
                        }
                        sibling.NodeColor = node.Parent.NodeColor;
                        node.Parent.NodeColor = Color.Black;
                        sibling.RightChild.NodeColor = Color.Black;
                        LeftRotate(node.Parent);
                        node = Root;
                    }
                }
                else
                {
                    var sibling = node.Parent.LeftChild;
                    if (sibling.NodeColor == Color.Red)
                    {
                        sibling.NodeColor = Color.Black;
                        node.Parent.NodeColor = Color.Red;
                        RightRotate(node.Parent);
                        sibling = node.Parent.LeftChild;
                    }
                    if (sibling.LeftChild.NodeColor == Color.Black && 
                        sibling.RightChild.NodeColor == Color.Black)
                    {
                        sibling.NodeColor = Color.Red;
                        node = node.Parent;
                    }
                    else
                    {
                        if (sibling.LeftChild.NodeColor == Color.Black)
                        {
                            sibling.RightChild.NodeColor = Color.Black;
                            sibling.NodeColor = Color.Red;
                            LeftRotate(sibling);
                            sibling = node.Parent.LeftChild;
                        }
                        sibling.NodeColor = node.Parent.NodeColor;
                        node.Parent.NodeColor = Color.Black;
                        sibling.LeftChild.NodeColor = Color.Black;
                        RightRotate(node.Parent);
                        node = Root;
                    }
                }

                node.NodeColor = Color.Black;
            }
        }

        private uint traverseNumber;
        public void Traverse()
        {
            traverseNumber = 0;
            if (Root != Sentinel)
                Traverse(Root);
            Console.WriteLine($"Traverse {traverseNumber} nodes.");
        }
        private void Traverse(RedBlackNode<Key,Value> node)
        {
            if (node.LeftChild != Sentinel)
            {
                Traverse(node.LeftChild);
            }
            traverseNumber++;
            string b = $"Node {node.Index}, Parent";
            if (node.Parent == Sentinel) b += " Sentinel";
            else b += $" {node.Parent.Index}";
            if (node.LeftChild == Sentinel) b += ", Left Sentinel";
            else b += $", Left {node.LeftChild.Index}";
            if (node.RightChild == Sentinel) b += ", Right Sentinel";
            else b += $", Right {node.RightChild.Index}";
            Console.WriteLine(b);
            //node.Print();
            if (node.RightChild != Sentinel)
            {
                Traverse(node.RightChild);
            }
        }
    }
}
