using System.Collections.Generic;
using Xunit;

namespace CtciCsharp.Chapter02
{
    public class LinkedListToy<T>
    {
        Node<T> _head;
        Node<T> _tail;

        public LinkedListToy()
        {
        }

        public LinkedListToy(params T[] list) : this(new List<T>(list))
        {
        }

        public LinkedListToy(IEnumerable<T> list)
        {
            foreach (T item in list)
            {
                Append(item);
            }
        }

        public Node<T> Head
        {
            get
            {
                return _head;
            }
        }

        public void Append(T data)
        {
            Node<T> newNode = new Node<T>(data);
            if (_head == null)
            {
                _head = newNode;
            }
            else
            {
                _tail.Next = newNode;
            }
            _tail = newNode;

        }

    }
    public class Node<T>
    {
        T _data;
        Node<T> _next;

        public Node(T data)
        {
            Data = data;
        }

        public T Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        public Node<T> Next
        {
            get
            {
                return _next;
            }
            set
            {
                _next = value;
            }
        }
    }

    public class C02Q01H_LinkedListToy_Tests
    {
        [Fact]
        public void BasicLL()
        {
            LinkedListToy<string> L = new LinkedListToy<string>();
            Assert.Equal(null, L.Head);

            L.Append("apple");
            L.Append("banana");
            Assert.Equal("apple", L.Head.Data);
            Assert.Equal("banana", L.Head.Next.Data);
            Assert.Equal(null, L.Head.Next.Next);
        }

        [Fact]
        public void CtorParamList()
        {
            LinkedListToy<string> L = new LinkedListToy<string>(new List<string> { "apple", "banana" });
            Assert.Equal("apple", L.Head.Data);
            Assert.Equal("banana", L.Head.Next.Data);
            Assert.Equal(null, L.Head.Next.Next);
        }

        [Fact]
        public void CtorParams2()
        {
            LinkedListToy<int> list = new LinkedListToy<int>(7, 8, 9, 3);
            C02Q01.RemoveDupes(list);
            Assert.NotNull(list.Head);
            Assert.Equal(7, list.Head.Data);
            Assert.Equal(8, list.Head.Next.Data);
            Assert.Equal(9, list.Head.Next.Next.Data);
            Assert.Equal(3, list.Head.Next.Next.Next.Data);
            Assert.Null(list.Head.Next.Next.Next.Next);
        }

        [Fact]
        public void CtorParams()
        {
            LinkedListToy<string> L = new LinkedListToy<string>("apple", "banana");
            Assert.Equal("apple", L.Head.Data);
            Assert.Equal("banana", L.Head.Next.Data);
            Assert.Equal(null, L.Head.Next.Next);
        }
    }
}
