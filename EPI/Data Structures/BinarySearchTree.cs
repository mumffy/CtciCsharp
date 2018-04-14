using EPI.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPI.DataStructures.BinarySearchTree
{
    public class BinarySearchTree<T> : BinaryTree<T> where T : IComparable
    {
        public int Count { get; set; }

        public Node<T> Find(T value)
        {
            return Find(Root, value);
        }

        private Node<T> Find(Node<T> node, T value)
        {
            if (node == null)
                return null;

            if (node.Value.CompareTo(value) < 0)
                return Find(node.Right, value);
            if (node.Value.CompareTo(value) > 0)
                return Find(node.Left, value);
            return node;
        }
    }
}
