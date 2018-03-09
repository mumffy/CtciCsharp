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

            LinkedList<int> merged = new LinkedList<int>();
            Node<int> a = listA.Head;
            Node<int> b = listB.Head;
            Node<int> temp;

            merged.Head = a.Value < b.Value ? a : b;
            while (a != null && b != null)
            {
                if (a.Value < b.Value)
                {
                    temp = a.Next;
                    a.Next = b;
                    a = temp;
                }
                else
                {
                    temp = b.Next;
                    b.Next = a;
                    b = temp;
                }
            }

            return merged;
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

            while(expectedNode != null)
            {
                Assert.Equal(expectedNode.Value, mergedNode.Value);
                expectedNode = expectedNode.Next;
                mergedNode = mergedNode.Next;
            }
            Assert.Null(mergedNode);
            Assert.Equal(expected, C07Q01.MergeSortedLists(a, b));
        }
    }
}

