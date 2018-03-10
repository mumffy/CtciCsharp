using EPI.DataStructures.LinkedList;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EPI.C07_LinkedLists
{
    public static class C07Q03<T>
    {
        public static Node<T> FindCycleWithDict(EPI.DataStructures.LinkedList.LinkedList<T> list)
        {
            Node<T> current = list.Head;
            HashSet<Node<T>> visited = new HashSet<Node<T>>();

            while (current != null)
            {
                if (visited.Contains(current))
                    return current;

                visited.Add(current);
                current = current.Next;
            }

            return null;
        }

        public static Node<T> FindCycle(EPI.DataStructures.LinkedList.LinkedList<T> list)
        {
            if (list.Head == null || list.Head.Next == null)
                return null;

            Node<T> slow = list.Head;
            Node<T> fast = list.Head.Next;

            while (fast != null && fast != slow)
            {
                fast = fast.Next?.Next;
                slow = slow.Next;
            }
            if (fast == null)
                return null;

            // fast is currently in a cycle
            int cycleLength = 1;
            Node<T> seen = fast;
            while(seen != fast)
                cycleLength++;

            Node<T> cycleStart = list.Head;
            while (cycleStart != fast)
            {
                for (int i = 0; i < cycleLength; i++)
                {
                    fast = fast.Next;
                    if (fast == cycleStart)
                        return cycleStart;
                }
                cycleStart = cycleStart.Next;
            }
            return cycleStart;
        }
    }

    public class C07Q03_Tests
    {
        [Fact]
        public void HasCycle01()
        {
            EPI.DataStructures.LinkedList.LinkedList<Object> list = new EPI.DataStructures.LinkedList.LinkedList<Object>();
            Node<Object> a = new Node<object>();
            Node<Object> b = new Node<object>();
            Node<Object> c = new Node<object>();
            list.Head = a;
            a.Next = b;
            b.Next = c;
            c.Next = b;

            Assert.Equal(b, C07Q03<Object>.FindCycleWithDict(list));
            Assert.Equal(b, C07Q03<Object>.FindCycle(list));
        }

        [Fact]
        public void HasCycle02()
        {
            EPI.DataStructures.LinkedList.LinkedList<Object> list = new EPI.DataStructures.LinkedList.LinkedList<Object>();
            Node<Object> a = new Node<object>();
            Node<Object> b = new Node<object>();
            Node<Object> c = new Node<object>();
            Node<Object> d = new Node<object>();
            Node<Object> e = new Node<object>();
            Node<Object> f = new Node<object>();
            list.Head = a;
            a.Next = b;
            b.Next = c;
            c.Next = d;
            d.Next = e;
            e.Next = f;
            f.Next = c;

            Assert.Equal(c, C07Q03<Object>.FindCycleWithDict(list));
            Assert.Equal(c, C07Q03<Object>.FindCycle(list));
        }

        [Fact]
        public void CycleStartAtHead()
        {
            EPI.DataStructures.LinkedList.LinkedList<Object> list = new EPI.DataStructures.LinkedList.LinkedList<Object>();
            Node<Object> a = new Node<object>();
            Node<Object> b = new Node<object>();
            Node<Object> c = new Node<object>();
            Node<Object> d = new Node<object>();
            Node<Object> e = new Node<object>();
            Node<Object> f = new Node<object>();
            list.Head = a;
            a.Next = b;
            b.Next = c;
            c.Next = d;
            d.Next = e;
            e.Next = f;
            f.Next = a;

            Assert.Equal(a, C07Q03<Object>.FindCycleWithDict(list));
            Assert.Equal(a, C07Q03<Object>.FindCycle(list));
        }

        [Fact]
        public void CycleStartsAtHeadLengthOne()
        {
            EPI.DataStructures.LinkedList.LinkedList<Object> list = new EPI.DataStructures.LinkedList.LinkedList<Object>();
            Node<Object> a = new Node<object>();
            list.Head = a;
            a.Next = a;

            Assert.Equal(a, C07Q03<Object>.FindCycleWithDict(list));
            Assert.Equal(a, C07Q03<Object>.FindCycle(list));
        }

        [Fact]
        public void CycleAtTailLengthOne()
        {
            EPI.DataStructures.LinkedList.LinkedList<Object> list = new EPI.DataStructures.LinkedList.LinkedList<Object>();
            Node<Object> a = new Node<object>();
            Node<Object> b = new Node<object>();
            Node<Object> c = new Node<object>();
            Node<Object> d = new Node<object>();
            Node<Object> e = new Node<object>();
            Node<Object> f = new Node<object>();
            list.Head = a;
            a.Next = b;
            b.Next = c;
            c.Next = d;
            d.Next = e;
            e.Next = f;
            f.Next = f;

            Assert.Equal(f, C07Q03<Object>.FindCycleWithDict(list));
            Assert.Equal(f, C07Q03<Object>.FindCycle(list));
        }

        [Fact]
        public void NoCycle()
        {
            EPI.DataStructures.LinkedList.LinkedList<Object> list = new EPI.DataStructures.LinkedList.LinkedList<Object>();
            Node<Object> a = new Node<object>();
            Node<Object> b = new Node<object>();
            Node<Object> c = new Node<object>();
            list.Head = a;
            a.Next = b;
            b.Next = c;
            c.Next = null;

            Assert.Null(C07Q03<Object>.FindCycleWithDict(list));
            Assert.Null(C07Q03<Object>.FindCycle(list));
        }

        [Fact]
        public void NoCycleLengthOne()
        {
            EPI.DataStructures.LinkedList.LinkedList<Object> list = new EPI.DataStructures.LinkedList.LinkedList<Object>();
            Node<Object> a = new Node<object>();
            list.Head = a;
            a.Next = null;

            Assert.Null(C07Q03<Object>.FindCycleWithDict(list));
            Assert.Null(C07Q03<Object>.FindCycle(list));
        }
    }
}

