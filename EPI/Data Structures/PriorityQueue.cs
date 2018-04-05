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

    public class BinaryMaxHeap<T> : IPriorityQueue<T> where T : IComparable
    {
        private Node<T> root;
        private int count;
        internal Node<T> Root => root;
        public int Count => count;

        public BinaryMaxHeap()
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
                if (lhsMaintainsHeapProperty(traverse.Value, traverse.Parent.Value))
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
            if (oldParent.Parent != null)
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

        virtual protected bool lhsMaintainsHeapProperty(T lhs, T rhs)
        {
            if (lhs.CompareTo(rhs) > 0)
                return true;
            return false;
        }

        public T Pop()
        {
            T result = root.Value;
            count--;
            if (count == 0)
            {
                root = null;
                return result;
            }

            Node<T> finalLeaf = root;
            Stack<bool> pathToFinalLeaf = getPathToNewLeaf();
            bool finalLeafWasLeftChild = count % 2 == 1;
            while (pathToFinalLeaf.Count > 0)
            {
                if (pathToFinalLeaf.Pop())
                    finalLeaf = finalLeaf.Left;
                else
                    finalLeaf = finalLeaf.Right;
            }
            finalLeaf.Left = root.Left;
            finalLeaf.Right = root.Right;
            if (finalLeafWasLeftChild)
                finalLeaf.Parent.Left = null;
            else
                finalLeaf.Parent.Right = null;

            if (finalLeaf.Left == finalLeaf)
                finalLeaf.Left = null;
            else if (finalLeaf.Right == finalLeaf)
                finalLeaf.Right = null;

            finalLeaf.Parent = null;
            root = finalLeaf;
            if (root.Left != null)
                root.Left.Parent = root;
            if (root.Right != null)
                root.Right.Parent = root;

            Node<T> parent = root;
            Node<T> child = null;
            bool goLeft;
            while (parent != null)
            {
                if (parent.Left != null && parent.Right != null)
                {
                    if (lhsMaintainsHeapProperty(parent.Left.Value, parent.Right.Value))
                    {
                        child = parent.Left;
                        goLeft = true;
                    }
                    else
                    {
                        child = parent.Right;
                        goLeft = false;
                    }
                }
                else if (parent.Left != null)
                {
                    child = parent.Left;
                    goLeft = true;
                }
                else if (parent.Right != null)
                {
                    child = parent.Right;
                    goLeft = false;
                }
                else // no children
                    break;

                if (lhsMaintainsHeapProperty(child.Value, parent.Value))
                {
                    bool wasRoot = parent == root;
                    Swap(parent: ref parent, child: ref child);
                    if (wasRoot)
                        root = parent;
                    parent = child;
                    continue;
                }
                break;
            }

            return result;
        }
    }

    public class BinaryMinHeap<T> : BinaryMaxHeap<T> where T : IComparable
    {
        override protected bool lhsMaintainsHeapProperty(T lhs, T rhs)
        {
            if (lhs.CompareTo(rhs) < 0)
                return true;
            return false;
        }
    }

    public abstract class IntBinaryHeap : IPriorityQueue<int>
    {
        protected IPriorityQueue<int> pq;
        public int Count => pq.Count;
        public int Peek()
        {
            return pq.Peek();
        }
        public int Pop()
        {
            return pq.Pop();
        }
        public void Push(int number)
        {
            pq.Push(number, number);
        }
        public void Push(int priority, int number)
        {
            Push(number);
        }
    }

    public class IntBinaryMaxHeap : IntBinaryHeap
    {
        internal Node<int> Root => (pq as BinaryMaxHeap<int>).Root;
        public IntBinaryMaxHeap() : base()
        {
            pq = new BinaryMaxHeap<int>();
        }
    }

    public class IntBinaryMinHeap : IntBinaryHeap
    {
        public IntBinaryMinHeap() : base()
        {
            pq = new BinaryMinHeap<int>();
        }
    }

    public class BinaryHeap_Tests
    {
        //TODO use IoC container to use array-based PQ for tests

        [Fact]
        public void AddMaintainsHeapProperty()
        {
            IntBinaryMaxHeap heap = new IntBinaryMaxHeap();
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
            Assert.Null(heap.Root.Left.Left.Left);
            Assert.Null(heap.Root.Left.Left.Right);
            Assert.Null(heap.Root.Left.Right.Left);
            Assert.Null(heap.Root.Left.Right.Right);
            Assert.Null(heap.Root.Right.Left.Left);
            Assert.Null(heap.Root.Right.Left.Right);
            Assert.Null(heap.Root.Right.Right.Left);
            Assert.Null(heap.Root.Right.Right.Right);

        }

        [Fact]
        public void PopBasics()
        {
            IntBinaryMaxHeap heap = new IntBinaryMaxHeap();
            heap.Push(5);
            Assert.Equal(1, heap.Count);
            Assert.Null(heap.Root.Parent);
            Assert.Null(heap.Root.Left);
            Assert.Null(heap.Root.Right);

            Assert.Equal(5, heap.Pop());
            Assert.Equal(0, heap.Count);
            Assert.Null(heap.Root);
        }

        [Fact]
        public void PopMaintainsHeapProperty()
        {
            IntBinaryMaxHeap heap = new IntBinaryMaxHeap();
            heap.Push(5);
            heap.Push(10);
            heap.Push(20);
            Assert.Equal(20, heap.Pop());
            Assert.Equal(10, heap.Pop());
            Assert.Equal(5, heap.Pop());
            Assert.Equal(0, heap.Count);
        }

        [Fact]
        public void PopMaintainsHeapProperty02()
        {
            IntBinaryMaxHeap heap = new IntBinaryMaxHeap();
            heap.Push(5);
            heap.Push(10);
            heap.Push(20);
            heap.Push(99);
            heap.Push(35);
            heap.Push(777);
            heap.Push(1);
            Assert.Equal(777, heap.Pop());
            Assert.Equal(99, heap.Pop());
            Assert.Equal(35, heap.Pop());
            Assert.Equal(20, heap.Pop());
            Assert.Equal(10, heap.Pop());
            Assert.Equal(5, heap.Pop());
            Assert.Equal(1, heap.Pop());
            Assert.Equal(0, heap.Count);
        }

        [Fact]
        public void PopMaintainsMinHeapProperty()
        {
            IntBinaryMinHeap heap = new IntBinaryMinHeap();
            heap.Push(5);
            heap.Push(10);
            heap.Push(20);
            heap.Push(99);
            heap.Push(35);
            heap.Push(777);
            heap.Push(1);
            Assert.Equal(1, heap.Pop());
            Assert.Equal(5, heap.Pop());
            Assert.Equal(10, heap.Pop());
            Assert.Equal(20, heap.Pop());
            Assert.Equal(35, heap.Pop());
            Assert.Equal(99, heap.Pop());
            Assert.Equal(777, heap.Pop());
            Assert.Equal(0, heap.Count);
        }
    }

}
