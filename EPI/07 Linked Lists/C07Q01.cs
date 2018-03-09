using EPI.DataStructures.LinkedList;
using System.Linq;
using Xunit;

namespace EPI.C07_LinkedLists
{
    public static class C07Q01
    {
        public static LinkedList<int> MergeSortedLists(LinkedList<int> listA, LinkedList<int> listB)
        {
            if (listA.Head == null)
                return listB;
            else if (listB.Head == null)
                return listA;

            Node<int> a = listA.Head;
            Node<int> b = listB.Head;
            Node<int> dummyHead = new Node<int>();
            Node<int> tail = dummyHead;

            while (a != null && b != null)
            {
                if (a.Value < b.Value)
                {
                    tail.Next = a;
                    tail = a;
                    a = a.Next;
                }
                else
                {
                    tail.Next = b;
                    tail = b;
                    b = b.Next;
                }
            }
            tail.Next = a ?? b;

            return new LinkedList<int>(dummyHead.Next);
        }
    }


    public class C07Q01_Tests
    {
        [Fact]
        public void Example()
        {
            LinkedList<int> a = new LinkedList<int>(new int[] { 2, 5, 7 });
            LinkedList<int> b = new LinkedList<int>(new int[] { 3, 11 });
            LinkedList<int> expected = new LinkedList<int>(new int[] { 2, 3, 5, 7, 11 });

            Node<int> mergedNode = C07Q01.MergeSortedLists(a, b).Head;
            Node<int> expectedNode = expected.Head;

            while (expectedNode != null)
            {
                Assert.Equal(expectedNode.Value, mergedNode.Value);
                expectedNode = expectedNode.Next;
                mergedNode = mergedNode.Next;
            }
            Assert.Null(mergedNode);
        }

        [Theory]
        [InlineData(
            new int[] { 1, 1, 1, 1, 1, 1, 1 },
            new int[] { 0 },
            new int[] { 0, 1, 1, 1, 1, 1, 1, 1 })]
        [InlineData(
            new int[] { 1, 1, 1, 1, 1, 1, 1 },
            new int[] { 99 },
            new int[] { 1, 1, 1, 1, 1, 1, 1, 99 })]
        [InlineData(
            new int[] { 1, 1, 1 },
            new int[] { 1, 1, 1 },
            new int[] { 1, 1, 1, 1, 1, 1 })]
        [InlineData(
            new int[] { 1, 1, 1 },
            new int[] { 1, 1, 1, 9 },
            new int[] { 1, 1, 1, 1, 1, 1, 9 })]
        public void Tests(int[] arrayA, int[] arrayB, int[] arrayExpected)
        {
            LinkedList<int> a = new LinkedList<int>(arrayA);
            LinkedList<int> b = new LinkedList<int>(arrayB);
            LinkedList<int> expected = new LinkedList<int>(arrayExpected);

            Node<int> mergedNode = C07Q01.MergeSortedLists(a, b).Head;
            Node<int> expectedNode = expected.Head;

            while (expectedNode != null)
            {
                Assert.Equal(expectedNode.Value, mergedNode.Value);
                expectedNode = expectedNode.Next;
                mergedNode = mergedNode.Next;
            }
            Assert.Null(mergedNode);
        }
    }
}

