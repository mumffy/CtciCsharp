using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPI.DataStructures.PriorityQueue
{
    public interface IPriorityQueue<T> where T : IComparable
    {
        int Count { get; }
        T Peek();
        void Push(int priority, T value);
        T Pop();
    }

    internal class Node<T>
    {
        public int Priority { get; }
        public T Value { get; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public Node<T> Parent { get; set; }

        public Node(int priority, T value)
        {
            Priority = priority;
            Value = value;
        }
    }

    public class BinaryHeap<T> : IPriorityQueue<T> where T : IComparable
    {
        Node<T> root;
        private int count;
        public int Count => count;

        public BinaryHeap()
        {
            count = 0;
        }

        public T Peek()
        {
            if (count > 0)
            {
                return root.Value;
            }
            throw new InvalidOperationException();
        }
        public void Push(int priority, T value)
        {
            Node<T> newNode = new Node<T>(priority, value);
            if (Count == 0)
            {
                root = newNode;
                count++;
                return;
            }

            Node<T> traverse = root;
            Stack<bool> goLefts = getPathToNewLeaf();
            while (goLefts.Count > 1)
            {
                //TODO
            }
            if (goLefts.Pop())
                traverse.Left = newNode;
            else
                traverse.Right = newNode;
            newNode.Parent = traverse;

            traverse = newNode;
            while (traverse.Parent != null)
            {
                if (traverse.Value.CompareTo(traverse.Parent.Value) > 0)
                    Swap(parent: traverse.Parent, child: traverse);
                else // heap property is maintained
                    return;
            }
        }

        private void Swap(Node<T> parent, Node<T> child)
        {
            bool wasLeftChild;
            if (parent.Left == child)
                wasLeftChild = true;
            else
                wasLeftChild = false;

            if (child.Parent == root)
            {
                Node<T> oldChildLeft = child.Left;
                Node<T> oldChildRight = child.Right;
                Node<T> oldRoot = root;
                root = child;
                root.Left = oldRoot.Left;
                root.Right = oldRoot.Right;
                root.Parent = null;
                if (wasLeftChild)
                    root.Left = oldRoot;
                else
                    root.Right = oldRoot;
                oldRoot.Left = oldChildLeft;
                oldRoot.Right = oldChildRight;
                oldRoot.Parent = root;
                return;
            }


        }

        private Stack<bool> getPathToNewLeaf()
        {
            Stack<bool> goLefts = new Stack<bool>();
            int num = count;
            while (num > 0)
            {
                if (num % 2 == 0)
                    goLefts.Push(true);
                else
                    goLefts.Push(false);
                num = (num - 1) / 2;
            }
            return goLefts;
        }

        public T Pop()
        {
            return default(T);
        }
    }

    class BinaryHead_Tests
    {

    }
}
