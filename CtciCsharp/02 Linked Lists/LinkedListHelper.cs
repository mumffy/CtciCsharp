using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CtciCsharp.Chapter02
{
    public class LinkedListToy<T>
    {
        Node _head;
        Node _tail;

        public LinkedListToy()
        {
        }

        public Node Head
        {
            get
            {
                return _head;
            }
        }

        public void Append(T data)
        {
            Node newNode = new Node(data);
            if(_head == null)
            {
                _head = newNode;
                _tail = newNode;
            }
            else
            {
                _tail.Next = newNode;
            }
            
        }

        public class Node
        {
            T _data;
            Node _next;

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

            public Node Next
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
    }

    public class Q01H_LinkedListToy_Tests
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
    }
}
