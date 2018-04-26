using System.Collections.Generic;
using Xunit;

namespace EPI.DataStructures.LinkedList
{
    public class LinkedList<T>
    {
        public Node<T> Head { get; set; }

        public LinkedList()
        {
            Head = null;
        }

        public LinkedList(Node<T> head)
        {
            Head = head;
        }

        public LinkedList(IEnumerable<T> e)
        {
            Node<T> tail = new Node<T>();
            Node<T> dummyHead = tail;
            Node<T> current;

            foreach (T i in e)
            {
                current = new Node<T>(i);
                tail.Next = current;
                tail = current;
            }
            Head = dummyHead.Next;
        }

        public static bool AreValuesEqual(LinkedList<T> listA, LinkedList<T> listB)
        {
            if (listA == null && listB != null || listA != null && listB == null)
                return false;
            if (listA == null && listB == null)
                return true;

            Node<T> a = listA.Head;
            Node<T> b = listB.Head;

            while (a != null && b != null)
            {
                if (!a.Value.Equals(b.Value))
                {
                    return false;
                }
                a = a.Next;
                b = b.Next;
            }
            if (a != null || b != null)
                return false;

            return true;
        }
    }

    public class LinkedListInteger : LinkedList<int>
    {
        public int ToInt()
        {
            int result = 0;
            Node<int> n = Head;

            while (n != null)
            {
                result *= 10;
                result += n.Value;
                n = n.Next;
            }
            return result;
        }
    }

    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }
        public Node()
        {

        }
        public Node(T value, Node<T> next = null)
        {
            Value = value;
            Next = next;
        }
    }

    public class LinkedList_Tests
    {
        [Fact]
        public void Basics()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.Head = new Node<int>(123);
            Node<int> n = list.Head;
            int[] a = new int[] { 555, -3, 99, 765 };
            foreach (int i in a)
            {
                n.Next = new Node<int>(i);
                n = n.Next;
            }

            int[] expected = new int[] { 123, 555, -3, 99, 765 };
            n = list.Head;
            foreach (int i in expected)
            {
                Assert.Equal(i, n.Value);
                n = n.Next;
            }
            Assert.Null(n);
        }

        [Fact]
        public void ctor_tests()
        {
            int[] a = new int[] { 3, 1, 6, 8, -99, 33 };
            LinkedList<int> list = new LinkedList<int>(a);
            Node<int> node = list.Head;
            for (int i = 0; i < a.Length; i++)
            {
                Assert.Equal(a[i], node.Value);
                node = node.Next;
            }
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 },
                    new int[] { 1, 2, 3 }, true)]
        [InlineData(new int[] { 1, 2, },
                    new int[] { 1, 2, 3 }, false)]
        [InlineData(new int[] { 1, 2, 3 },
                    new int[] { 1, 2 }, false)]
        [InlineData(new int[] { 1, 2, 3 },
                    new int[] { 1, 9, 3 }, false)]
        [InlineData(new int[] { 1, 9, 3 },
                    new int[] { 1, 2, 3 }, false)]
        [InlineData(new int[] { },
                    new int[] { 1, 2, 3 }, false)]
        public void ValueEqual_Tests(int[] a, int[] b, bool expected)
        {
            LinkedList<int> listA = new LinkedList<int>(a);
            LinkedList<int> listB = new LinkedList<int>(b);
            Assert.Equal(expected, LinkedList<int>.AreValuesEqual(listA, listB));
        }

        [Fact]
        public void ValueEqual_Null_Tests()
        {
            Assert.False(LinkedList<int>.AreValuesEqual(null, new LinkedList<int>(new int[] { 1, 2, 3 })));
            Assert.False(LinkedList<int>.AreValuesEqual(new LinkedList<int>(new int[] { 1, 2, 3 }), null));
        }
    }

    public class LinkedListInteger_Tests
    {
        [Fact]
        public void Basics01()
        {
            LinkedListInteger list = new LinkedListInteger();
            list.Head = new Node<int>(1);
            list.Head.Next = new Node<int>(2);
            list.Head.Next.Next = new Node<int>(3);
            Assert.Equal(123, list.ToInt());
        }
        [Fact]
        public void Basics02()
        {
            LinkedListInteger list = new LinkedListInteger();
            list.Head = new Node<int>(9);
            Assert.Equal(9, list.ToInt());
        }
    }

    public class DoublyLinkedList<T>
    {
        private int count;
        private DoublyLinkedListNode<T> head;
        private DoublyLinkedListNode<T> tail;

        public int Count => count;
        public DoublyLinkedListNode<T> Head => head;
        public DoublyLinkedListNode<T> Tail => tail;

        public DoublyLinkedListNode<T> AddToTail(T value)
        {
            var newNode = new DoublyLinkedListNode<T>(value);
            return AddToTail(newNode);
        }

        public DoublyLinkedListNode<T> AddToTail(DoublyLinkedListNode<T> newNode)
        {
            if (head == null)
            {
                head = newNode;
            }
            if (tail != null)
            {
                var oldTail = tail;
                newNode.Prev = oldTail;
                oldTail.Next = newNode;
            }
            tail = newNode;
            count++;
            return newNode;
        }

        public DoublyLinkedListNode<T> Remove(DoublyLinkedListNode<T> node)
        {
            if (count == 0)
                return null;

            if (node == head)
                return RemoveHead();

            if (node.Prev != null)
                node.Prev.Next = node.Next;

            if (node.Next != null)
                node.Next.Prev = node.Prev;

            count--;
            return node;
        }

        public DoublyLinkedListNode<T> RemoveHead()
        {
            if (count == 0)
                return null;

            var oldHead = head;
            if (head == tail)
            {
                head = null;
                tail = null;
            }
            else
            {
                head = head.Next;
                head.Prev = null;
            }
            count--;
            return oldHead;
        }

        public void MoveToTail(DoublyLinkedListNode<T> node)
        {
            if (node == tail || Count == 0)
                return;

            if (node == head)
                head = node.Next;

            if (node.Prev != null)
                node.Prev.Next = node.Next;

            if (node.Next != null)
                node.Next.Prev = node.Prev;

            tail.Next = node;
            tail.Next.Prev = tail;
            tail = node;
            tail.Next = null;
        }
    }

    public class DoublyLinkedListNode<T>
    {
        public DoublyLinkedListNode<T> Prev { get; set; }
        public DoublyLinkedListNode<T> Next { get; set; }
        public T Value { get; }
        public DoublyLinkedListNode(T value)
        {
            Value = value;
        }
    }

    public class DoublyLinkedListTests
    {
        DoublyLinkedList<string> dll;
        public DoublyLinkedListTests()
        {
            dll = new DoublyLinkedList<string>();
        }

        [Fact]
        public void Basics()
        {
            Assert.Equal(0, dll.Count);

            dll.AddToTail("apple");
            Assert.Equal(1, dll.Count);
            Assert.Null(dll.Head.Prev);
            Assert.Null(dll.Head.Next);

            dll.AddToTail("banana");
            var coconutNode = dll.AddToTail("coconut");
            dll.AddToTail("durian");
            dll.AddToTail("eggplant");

        }

        [Fact]
        public void MoveToTail_AlreadyTail()
        {
            Assert.Equal(0, dll.Count);

            var apple = dll.AddToTail("apple");
            var banana = dll.AddToTail("banana");
            var coconut = dll.AddToTail("coconut");

            dll.MoveToTail(coconut);

            Assert.Equal(3, dll.Count);
            Assert.Equal(apple, dll.Head);
            Assert.Equal(banana, dll.Head.Next);
            Assert.Equal(coconut, dll.Head.Next.Next);
            Assert.Equal(coconut, dll.Tail);
        }

        [Fact]
        public void MoveToTail_FromHead()
        {
            var apple = dll.AddToTail("apple");
            var banana = dll.AddToTail("banana");
            var coconut = dll.AddToTail("coconut");

            dll.MoveToTail(apple);

            Assert.Equal(3, dll.Count);
            Assert.Equal(banana, dll.Head);
            Assert.Equal(coconut, dll.Head.Next);
            Assert.Equal(apple, dll.Head.Next.Next);
            Assert.Equal(apple, dll.Tail);
            Assert.Equal(coconut, dll.Tail.Prev);
            Assert.Equal(banana, dll.Tail.Prev.Prev);
        }

        [Fact]
        public void MoveToTail_FromMiddle()
        {
            var apple = dll.AddToTail("apple");
            var banana = dll.AddToTail("banana");
            var coconut = dll.AddToTail("coconut");

            dll.MoveToTail(banana);

            Assert.Equal(3, dll.Count);
            Assert.Equal(apple, dll.Head);
            Assert.Equal(coconut, dll.Head.Next);
            Assert.Equal(banana, dll.Head.Next.Next);
            Assert.Equal(banana, dll.Tail);
            Assert.Equal(coconut, dll.Tail.Prev);
            Assert.Equal(apple, dll.Tail.Prev.Prev);
        }


        [Fact]
        public void MoveToTail_FromHeadThenFromMiddle()
        {
            var apple = dll.AddToTail("apple");
            var banana = dll.AddToTail("banana");
            var coconut = dll.AddToTail("coconut");

            dll.MoveToTail(apple);

            Assert.Equal(3, dll.Count);
            Assert.Equal(banana, dll.Head);
            Assert.Equal(coconut, dll.Head.Next);
            Assert.Equal(apple, dll.Head.Next.Next);
            Assert.Equal(apple, dll.Tail);
            Assert.Equal(coconut, dll.Tail.Prev);
            Assert.Equal(banana, dll.Tail.Prev.Prev);

            dll.MoveToTail(coconut);

            Assert.Equal(3, dll.Count);
            Assert.Equal(banana, dll.Head);
            Assert.Equal(apple, dll.Head.Next);
            Assert.Equal(coconut, dll.Head.Next.Next);
            Assert.Equal(coconut, dll.Tail);
            Assert.Equal(apple, dll.Tail.Prev);
            Assert.Equal(banana, dll.Tail.Prev.Prev);
        }

        [Fact]
        public void RemoveHead()
        {
            var apple = dll.AddToTail("apple");
            var banana = dll.AddToTail("banana");
            var coconut = dll.AddToTail("coconut");
            Assert.Equal(3, dll.Count);

            var removedApple = dll.RemoveHead();
            Assert.Equal(2, dll.Count);
            Assert.Equal(apple, removedApple);
            Assert.Equal("apple", removedApple.Value);
            Assert.Equal(banana, dll.Head);
            Assert.Null(dll.Head.Prev);

            var removedBanana = dll.RemoveHead();
            Assert.Equal(1, dll.Count);
            Assert.Equal(banana, removedBanana);
            Assert.Equal("banana", removedBanana.Value);
            Assert.Equal(coconut, dll.Head);
            Assert.Null(dll.Head.Prev);

            var removedCoconut = dll.RemoveHead();
            Assert.Equal(0, dll.Count);
            Assert.Equal(coconut, removedCoconut);
            Assert.Null(dll.Head);
            Assert.Null(dll.Tail);
        }

        [Fact]
        public void Remove()
        {
            var apple = dll.AddToTail("apple");
            var banana = dll.AddToTail("banana");
            var coconut = dll.AddToTail("coconut");
            Assert.Equal(3, dll.Count);

            var removedBanana = dll.Remove(banana);
            Assert.Equal(2, dll.Count);
            Assert.Equal(banana, removedBanana);
            Assert.Equal("banana", removedBanana.Value);
            Assert.Equal(apple, dll.Head);
            Assert.Null(dll.Head.Prev);

            var removedApple = dll.Remove(apple);
            Assert.Equal(1, dll.Count);
            Assert.Equal(apple, removedApple);
            Assert.Equal(coconut, dll.Head);
            Assert.Equal(coconut, dll.Tail);
            Assert.Null(dll.Head.Prev);

            var removedCoconut = dll.Remove(coconut);
            Assert.Equal(0, dll.Count);
            Assert.Equal(coconut, removedCoconut);
            Assert.Null(dll.Head);
            Assert.Null(dll.Tail);
        }
    }
}

