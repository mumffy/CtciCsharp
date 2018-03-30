using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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
        private Node<T> root;
        private int count;
        internal Node<T> Root => root;
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
                if (goLefts.Pop())
                    traverse = traverse.Left;
                else
                    traverse = traverse.Right;
            }
            if (goLefts.Pop())
                traverse.Left = newNode;
            else
                traverse.Right = newNode;
            newNode.Parent = traverse;
            count++;

            traverse = newNode;
            while (traverse.Parent != null)
            {
                if (traverse.Value.CompareTo(traverse.Parent.Value) > 0) // this is a max-heap
                {
                    Node<T> parent = traverse.Parent;
                    Swap(parent: ref parent, child: ref traverse);
                    traverse = parent;
                }
                else // heap property is maintained
                    return;
            }
        }

        private void Swap(ref Node<T> parent, ref Node<T> child)
        {
            bool wasLeftChild;
            if (parent.Left == child)
                wasLeftChild = true;
            else
                wasLeftChild = false;

            Node<T> oldParent = parent;
            Node<T> oldChildLeft = child.Left;
            Node<T> oldChildRight = child.Right;
            parent = child;
            parent.Parent = oldParent.Parent;
            if(oldParent.Parent != null)
            {
                if (oldParent.Parent.Left == oldParent)
                    oldParent.Parent.Left = parent;
                else
                    oldParent.Parent.Right = parent;
            }
                
            if (wasLeftChild)
            {
                parent.Left = oldParent;
                parent.Right = oldParent.Right;
            }
            else
            {
                parent.Right = oldParent;
                parent.Left = oldParent.Left;
            }
            if (parent.Left != null)
                parent.Left.Parent = parent;
            if (parent.Right != null)
                parent.Right.Parent = parent;
            oldParent.Left = oldChildLeft;
            oldParent.Right = oldChildRight;
            child = oldParent;

            if (root == oldParent)
                root = parent;
        }

        private Stack<bool> getPathToNewLeaf()
        {
            Stack<bool> goLefts = new Stack<bool>();
            int num = count;
            while (num > 0)
            {
                if (num % 2 == 0)
                    goLefts.Push(false);
                else
                    goLefts.Push(true);
                num = (num - 1) / 2;
            }
            return goLefts;
        }

        public T Pop()
        {
            throw new NotImplementedException();
        }
    }

    public class IntBinaryHeap : BinaryHeap<int>
    {
        public void Push(int value)
        {
            Push(value, value);
        }
    }

    public class BinaryHeap_Tests
    {
        //TODO use IoC container to use array-based PQ for tests

        [Fact]
        public void AddMaintainsHeapProperty()
        {
            IntBinaryHeap heap = new IntBinaryHeap();
            heap.Push(5);
            Assert.Equal(1, heap.Count);
            Assert.Null(heap.Root.Parent);
            Assert.Null(heap.Root.Left);
            Assert.Null(heap.Root.Right);

            heap.Push(10);
            Assert.Equal(2, heap.Count);
            Assert.Null(heap.Root.Parent);
            Assert.Null(heap.Root.Right);
            Assert.Equal(10, heap.Root.Value);
            Assert.Equal(5, heap.Root.Left.Value);
            Assert.Equal(heap.Root, heap.Root.Left.Parent);

            heap.Push(20);
            Assert.Equal(3, heap.Count);
            Assert.Null(heap.Root.Parent);
            Assert.Equal(20, heap.Root.Value);
            Assert.Equal(5, heap.Root.Left.Value);
            Assert.Equal(10, heap.Root.Right.Value);
            Assert.Equal(heap.Root, heap.Root.Left.Parent);
            Assert.Equal(heap.Root, heap.Root.Right.Parent);
            Assert.Null(heap.Root.Left.Left);
            Assert.Null(heap.Root.Left.Right);
            Assert.Null(heap.Root.Right.Left);
            Assert.Null(heap.Root.Right.Right);

            heap.Push(99);
            heap.Push(35);
            heap.Push(777);
            heap.Push(1);
            Assert.Equal(7, heap.Count);
            Assert.Null(heap.Root.Parent);
            Assert.Equal(777, heap.Root.Value);
            Assert.Equal(35, heap.Root.Left.Value);
            Assert.Equal(99, heap.Root.Right.Value);
            Assert.Equal(5, heap.Root.Left.Left.Value);
            Assert.Equal(20, heap.Root.Left.Right.Value);
            Assert.Equal(10, heap.Root.Right.Left.Value);
            Assert.Equal(1, heap.Root.Right.Right.Value);

        }
    }
}
