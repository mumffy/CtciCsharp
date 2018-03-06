using System;
using EPI.DataStructures.LinkedList;
using Xunit;
using System.Collections.Generic;

namespace Online
{
    public static class OTS
    {
        public static Node<int> LinkedListSum(LinkedListInteger a, LinkedListInteger b)
        {
            LinkedListInteger sum = new LinkedListInteger();
            Node<int> aLsd = ReverseList(a.Head);
            Node<int> bLsd = ReverseList(b.Head);
            Node<int> sumLsdTail = new Node<int>(0);
            Node<int> sumLsd = sumLsdTail;

            int lsdSum;
            bool hasCarry = false;
            while (aLsd != null && bLsd != null)
            {
                lsdSum = aLsd.Value + bLsd.Value + Convert.ToInt32(hasCarry);
                sumLsd.Next = new Node<int>(lsdSum % 10);
                sumLsd = sumLsd.Next;
                hasCarry = lsdSum >= 10;
                aLsd = aLsd.Next;
                bLsd = bLsd.Next;
            }
            if (aLsd != null || bLsd != null)
            {
                Node<int> remaining = aLsd ?? bLsd;
                while (remaining != null)
                {
                    lsdSum = remaining.Value + Convert.ToInt32(hasCarry);
                    sumLsd.Next = new Node<int>(lsdSum % 10);
                    sumLsd = sumLsd.Next;
                    hasCarry = lsdSum >= 10;
                    remaining = remaining.Next;
                }
            }
            if (hasCarry)
            {
                sumLsd.Next = new Node<int>(1);
                sumLsd = sumLsd.Next;
            }
            sum.Head = ReverseList(sumLsdTail.Next); // sumLsdTail.Next to "skip" the dummy head from the initialization
            return sum.Head;
        }

        public static Node<int> ReverseList(Node<int> head)
        {
            Node<int> traversal = head;
            Node<int> behind = null;
            Node<int> current = null;

            while (traversal != null)
            {
                current = traversal;
                traversal = traversal.Next;
                current.Next = behind;
                behind = current;
            }
            return current;
        }

        public static Node<int> LinkedListSumUsingStacks(LinkedListInteger aLL, LinkedListInteger bLL)
        {
            Node<int> newHead, result = null;
            Node<int> a = aLL.Head, b = bLL.Head;
            Stack<int> aStack = new Stack<int>();
            Stack<int> bStack = new Stack<int>();
            Stack<int> leftovers = null;
            int sum, carry = 0;

            while (a != null)
            {
                aStack.Push(a.Value);
                a = a.Next;
            }
            while (b != null)
            {
                bStack.Push(b.Value);
                b = b.Next;
            }

            // LIFO will cause A and B's least significant digits to be pop out first
            while (aStack.Count > 0 && bStack.Count > 0)
            {
                sum = aStack.Pop() + bStack.Pop() + carry;
                newHead = new Node<int>(sum % 10, result);
                result = newHead;

                carry = sum / 10;
            }

            if (aStack.Count > 0)
                leftovers = aStack;
            else if (bStack.Count > 0)
                leftovers = bStack;
            while(leftovers != null && leftovers.Count > 0)
            {
                sum = leftovers.Pop() + carry;
                newHead = new Node<int>(sum % 10, result);
                result = newHead;

                carry = sum / 10;
            }
            if (carry > 0)
            {
                newHead = new Node<int>(1, result);
                result = newHead;
            }

            return result;
        }
    }

    public class Tests
    {
        [Fact]
        public void Reverse_Test01()
        {
            LinkedListInteger list = new LinkedListInteger();
            list.Head = new Node<int>(1);
            list.Head.Next = new Node<int>(2);
            list.Head.Next.Next = new Node<int>(3);
            Assert.Equal(123, list.ToInt());
            Assert.Equal(123, list.ToInt());

            list.Head = OTS.ReverseList(list.Head);
            Assert.Equal(321, list.ToInt());

        }

        [Fact]
        public void Basic_Sum01()
        {
            LinkedListInteger a = new LinkedListInteger();
            a.Head = new Node<int>(1);
            a.Head.Next = new Node<int>(2);
            LinkedListInteger b = new LinkedListInteger();
            b.Head = new Node<int>(3);
            b.Head.Next = new Node<int>(4);


            var sum = OTS.LinkedListSum(a, b);
            LinkedListInteger s = new LinkedListInteger { Head = sum };
            Assert.Equal(46, s.ToInt());
        }

        [Fact]
        public void Carry_Sum01()
        {
            LinkedListInteger a = new LinkedListInteger();
            a.Head = new Node<int>(1);
            a.Head.Next = new Node<int>(2);
            LinkedListInteger b = new LinkedListInteger();
            b.Head = new Node<int>(8);


            var sum = OTS.LinkedListSum(a, b);
            LinkedListInteger s = new LinkedListInteger { Head = sum };
            Assert.Equal(20, s.ToInt());
        }


        [Fact]
        public void Carry_Sum02()
        {
            LinkedListInteger a = new LinkedListInteger();
            a.Head = new Node<int>(9);
            a.Head.Next = new Node<int>(9);
            a.Head.Next.Next = new Node<int>(9);
            a.Head.Next.Next.Next = new Node<int>(9);
            LinkedListInteger b = new LinkedListInteger();
            b.Head = new Node<int>(6);


            var sum = OTS.LinkedListSum(a, b);
            LinkedListInteger s = new LinkedListInteger { Head = sum };
            Assert.Equal(10005, s.ToInt());
        }

        [Fact]
        public void Stack_Basic_Sum01()
        {
            LinkedListInteger a = new LinkedListInteger();
            a.Head = new Node<int>(1);
            a.Head.Next = new Node<int>(2);
            LinkedListInteger b = new LinkedListInteger();
            b.Head = new Node<int>(3);
            b.Head.Next = new Node<int>(4);


            var sum = OTS.LinkedListSumUsingStacks(a, b);
            LinkedListInteger s = new LinkedListInteger { Head = sum };
            Assert.Equal(46, s.ToInt());
        }

        [Fact]
        public void Stack_Carry_Sum01()
        {
            LinkedListInteger a = new LinkedListInteger();
            a.Head = new Node<int>(1);
            a.Head.Next = new Node<int>(2);
            LinkedListInteger b = new LinkedListInteger();
            b.Head = new Node<int>(8);


            var sum = OTS.LinkedListSumUsingStacks(a, b);
            LinkedListInteger s = new LinkedListInteger { Head = sum };
            Assert.Equal(20, s.ToInt());
        }


        [Fact]
        public void Stack_Carry_Sum02()
        {
            LinkedListInteger a = new LinkedListInteger();
            a.Head = new Node<int>(9);
            a.Head.Next = new Node<int>(9);
            a.Head.Next.Next = new Node<int>(9);
            a.Head.Next.Next.Next = new Node<int>(9);
            LinkedListInteger b = new LinkedListInteger();
            b.Head = new Node<int>(6);


            var sum = OTS.LinkedListSumUsingStacks(a, b);
            LinkedListInteger s = new LinkedListInteger { Head = sum };
            Assert.Equal(10005, s.ToInt());
        }


    }
}
