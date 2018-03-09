using EPI.DataStructures.LinkedList;
using System;
using System.Linq;
using Xunit;

namespace EPI.C07_LinkedLists
{
    public static class C07Q02
    {
        // head of a list is node #1, its next is node #2, and so on...
        public static void ReverseSublist(LinkedList<int> list, int start, int end)
        {
            if (start < 1 || start >= end)
                return;

            Node<int> current = list.Head;
            Node<int> left, right, startNode, endNode;
            right = endNode = current;
            left = new Node<int>();
            Node<int> behind = null, temp;

            int i = 1;
            while (current != null && i < start)
            {
                left = current;
                i++;
                current = current.Next;
            }

            if (current == null)
                return;
            startNode = current;

            while (current != null && i <= end)
            {
                endNode = current;
                temp = current.Next;
                current.Next = behind;
                behind = current;

                i++;
                current = temp;
                right = current;
            }
            if (i < end)
                throw new ArgumentException("{0} is not a valid end node number", nameof(end));

            left.Next = endNode;
            startNode.Next = right;
            if (start == 1)
            {
                list.Head = endNode;
            }
        }
    }


    public class C07Q02_Tests
    {
        [Fact]
        public void Example()
        {
            LinkedList<int> list = new LinkedList<int>(new int[] { 11, 3, 5, 7, 2 });
            LinkedList<int> expected = new LinkedList<int>(new int[] { 11, 7, 5, 3, 2 });

            C07Q02.ReverseSublist(list, 2, 4);
            Assert.True(LinkedList<int>.AreValuesEqual(expected, list));
        }

        [Theory]
        [InlineData(new int[] { 5, 6, 7, 8, 9 }, 1, 3,
                    new int[] { 7, 6, 5, 8, 9 })]
        [InlineData(new int[] { 5, 6, 7, 8, 9 }, 3, 5,
                    new int[] { 5, 6, 9, 8, 7 })]
        [InlineData(new int[] { 5, 6, 7, 8, 9 }, 1, 5,
                    new int[] { 9, 8, 7, 6, 5 })]
        [InlineData(new int[] { 5, 6, 7, 8, 9 }, 3, 3,
                    new int[] { 5, 6, 7, 8, 9 })]
        [InlineData(new int[] { 5, 6, 7, 8, 9 }, 1, 4,
                    new int[] { 8, 7, 6, 5, 9 })]
        public void Tests(int[] input, int startNode, int endNode, int[] expectedOutput)
        {
            LinkedList<int> list = new LinkedList<int>(input);
            LinkedList<int> expected = new LinkedList<int>(expectedOutput);

            C07Q02.ReverseSublist(list, startNode, endNode);
            Assert.True(LinkedList<int>.AreValuesEqual(expected, list));
        }
    }
}

