using System;
using EPI.DataStructures.LinkedList;
using Xunit;

namespace Online
{
    public static class OTS
    {
        public static LinkedListInteger LinkedListSum(LinkedListInteger a, LinkedListInteger b)
        {
            LinkedListInteger sum = new LinkedListInteger();

            return sum;
        }

        public static Node<int> ReverseList(Node<int> head)
        {
            Node<int> traversal = head;
            Node<int> behind = null;
            Node<int> current = null;

            while(traversal != null)
            {
                current = traversal;
                traversal = traversal.Next;
                current.Next = behind;
                behind = current;
            }
            return current;
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

        private LinkedListInteger GetLinkedListIntege(int i)
        {
            LinkedListInteger list = new LinkedListInteger();



            return list;
        }

    }
}
