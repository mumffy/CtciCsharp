using EPI.DataStructures.LinkedList;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EPI.C07_LinkedLists
{
    public static class C07Q04<T>
    {
        public static Node<T> FindOverlapWithHash(EPI.DataStructures.LinkedList.LinkedList<T> aList, EPI.DataStructures.LinkedList.LinkedList<T> bList)
        {
            HashSet<Node<T>> aNodes = new HashSet<Node<T>>();
            Node<T> a = aList.Head;
            Node<T> b = bList.Head;

            if (a == null || b == null)
                return null;

            while (a != null)
            {
                aNodes.Add(a);
                a = a.Next;
            }
            while (b != null)
            {
                if (aNodes.Contains(b))
                {
                    return b;
                }
                b = b.Next;
            }
            return null;
        }

        public static Node<T> FindOverlapBruteForce(EPI.DataStructures.LinkedList.LinkedList<T> aList, EPI.DataStructures.LinkedList.LinkedList<T> bList)
        {
            Node<T> a = aList.Head;
            Node<T> b;

            while (a != null)
            {
                b = bList.Head;
                while (b != null)
                {
                    if (a == b)
                        return b;
                    b = b.Next;
                }
                a = a.Next;
            }

            return null;
        }

        public static Node<T> FindOverlap(EPI.DataStructures.LinkedList.LinkedList<T> aList, EPI.DataStructures.LinkedList.LinkedList<T> bList)
        {
            Node<T> a = aList.Head;
            Node<T> b = bList.Head;
            Node<T> longer, shorter;
            int aLen = 0;
            int bLen = 0;

            while (a != null)
            {
                aLen++;
                a = a.Next;
            }
            while (b != null)
            {
                bLen++;
                b = b.Next;
            }

            if (aLen > bLen)
            {
                longer = aList.Head;
                shorter = bList.Head;
            }
            else
            {
                longer = bList.Head;
                shorter = aList.Head;
            }

            for (int i = 0; i < Math.Abs(aLen - bLen); i++)
                longer = longer.Next;

            while(longer != null && longer != shorter) {
                longer = longer.Next;
                shorter = shorter.Next;
            }
            return longer;
        }
    }


    public class C07Q04_Tests
    {
        [Fact]
        public void Example()
        {
            EPI.DataStructures.LinkedList.LinkedList<Object> listA = new EPI.DataStructures.LinkedList.LinkedList<Object>();
            EPI.DataStructures.LinkedList.LinkedList<Object> listB = new EPI.DataStructures.LinkedList.LinkedList<Object>();
            Node<Object> aa = new Node<object>();
            Node<Object> ab = new Node<object>();
            Node<Object> ba = new Node<object>();
            Node<Object> bb = new Node<object>();
            Node<Object> bc = new Node<object>();
            Node<Object> bd = new Node<object>();
            listA.Head = aa;
            aa.Next = ab;
            ab.Next = bb;
            listB.Head = ba;
            ba.Next = bb;
            bb.Next = bc;
            bc.Next = bd;
            bd.Next = null;

            Assert.Equal(bb, C07Q04<Object>.FindOverlapWithHash(listA, listB));
            Assert.Equal(bb, C07Q04<Object>.FindOverlapBruteForce(listA, listB));
            Assert.Equal(bb, C07Q04<Object>.FindOverlap(listA, listB));
        }

        [Fact]
        public void SameHead()
        {
            EPI.DataStructures.LinkedList.LinkedList<Object> listA = new EPI.DataStructures.LinkedList.LinkedList<Object>();
            EPI.DataStructures.LinkedList.LinkedList<Object> listB = new EPI.DataStructures.LinkedList.LinkedList<Object>();
            Node<Object> aa = new Node<object>();
            Node<Object> ab = new Node<object>();
            Node<Object> ac = new Node<object>();
            listA.Head = aa;
            listB.Head = aa;
            aa.Next = ab;
            ab.Next = ac;
            ac.Next = null;

            Assert.Equal(aa, C07Q04<Object>.FindOverlapWithHash(listA, listB));
            Assert.Equal(aa, C07Q04<Object>.FindOverlapBruteForce(listA, listB));
            Assert.Equal(aa, C07Q04<Object>.FindOverlap(listA, listB));
        }

        [Fact]
        public void OverlapAtHead()
        {
            EPI.DataStructures.LinkedList.LinkedList<Object> listA = new EPI.DataStructures.LinkedList.LinkedList<Object>();
            EPI.DataStructures.LinkedList.LinkedList<Object> listB = new EPI.DataStructures.LinkedList.LinkedList<Object>();
            Node<Object> aa = new Node<object>();
            Node<Object> ab = new Node<object>();
            Node<Object> ba = new Node<object>();
            Node<Object> bb = new Node<object>();
            Node<Object> bc = new Node<object>();
            Node<Object> bd = new Node<object>();
            listA.Head = aa;
            aa.Next = ab;
            ab.Next = ba;
            listB.Head = ba;
            ba.Next = bb;
            bb.Next = bc;
            bc.Next = bd;
            bd.Next = null;

            Assert.Equal(ba, C07Q04<Object>.FindOverlapWithHash(listA, listB));
            Assert.Equal(ba, C07Q04<Object>.FindOverlapBruteForce(listA, listB));
            Assert.Equal(ba, C07Q04<Object>.FindOverlap(listA, listB));
        }

        [Fact]
        public void OverlapAtTail()
        {
            EPI.DataStructures.LinkedList.LinkedList<Object> listA = new EPI.DataStructures.LinkedList.LinkedList<Object>();
            EPI.DataStructures.LinkedList.LinkedList<Object> listB = new EPI.DataStructures.LinkedList.LinkedList<Object>();
            Node<Object> aa = new Node<object>();
            Node<Object> ab = new Node<object>();
            Node<Object> ba = new Node<object>();
            Node<Object> bb = new Node<object>();
            Node<Object> bc = new Node<object>();
            Node<Object> bd = new Node<object>();
            listA.Head = aa;
            aa.Next = ab;
            ab.Next = bd;
            listB.Head = ba;
            ba.Next = bb;
            bb.Next = bc;
            bc.Next = bd;
            bd.Next = null;

            Assert.Equal(bd, C07Q04<Object>.FindOverlapWithHash(listA, listB));
            Assert.Equal(bd, C07Q04<Object>.FindOverlapBruteForce(listA, listB));
            Assert.Equal(bd, C07Q04<Object>.FindOverlap(listA, listB));
        }

        [Fact]
        public void NoOverlap()
        {
            EPI.DataStructures.LinkedList.LinkedList<Object> listA = new EPI.DataStructures.LinkedList.LinkedList<Object>();
            EPI.DataStructures.LinkedList.LinkedList<Object> listB = new EPI.DataStructures.LinkedList.LinkedList<Object>();
            Node<Object> aa = new Node<object>();
            Node<Object> ab = new Node<object>();
            Node<Object> ba = new Node<object>();
            Node<Object> bb = new Node<object>();
            Node<Object> bc = new Node<object>();
            Node<Object> bd = new Node<object>();
            listA.Head = aa;
            aa.Next = ab;
            ab.Next = null;
            listB.Head = ba;
            ba.Next = bb;
            bb.Next = bc;
            bc.Next = bd;
            bd.Next = null;

            Assert.Null(C07Q04<Object>.FindOverlapWithHash(listA, listB));
            Assert.Null(C07Q04<Object>.FindOverlapBruteForce(listA, listB));
            Assert.Null(C07Q04<Object>.FindOverlap(listA, listB));
        }
    }
}

