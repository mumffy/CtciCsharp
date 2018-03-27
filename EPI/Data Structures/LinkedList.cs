using Xunit;

namespace EPI.DataStructures.LinkedList
{
    public class LinkedList<T>
    {
        public Node<T> Head { get; set; }

        public LinkedList() : this(null)
        {
        }

        public LinkedList(Node<T> head)
        {
            Head = head;
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
            foreach(int i in expected)
            {
                Assert.Equal(i, n.Value);
                n = n.Next;
            }
            Assert.Null(n);
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
}
