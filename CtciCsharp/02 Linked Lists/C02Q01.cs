using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CtciCsharp.Chapter02
{
    public static class C02Q01
    {
        public static void RemoveDupes(LinkedListToy<int> list)
        {
            HashSet<int> seen = new HashSet<int>();

            Node<int> prev = null;
            Node<int> curr = list.Head;

            while (curr != null)
            {
                if (!seen.Contains(curr.Data))
                {
                    seen.Add(curr.Data);
                    prev = curr;
                }
                else
                {
                    // prev cannot be list.Head
                    prev.Next = curr.Next;
                }

                curr = curr.Next;
            }
        }
    }

    public class C02Q01_RemoveDups_Tests
    {
        [Fact]
        public void Empty()
        {
            LinkedListToy<int> list = new LinkedListToy<int>();
            C02Q01.RemoveDupes(list);
            Assert.Null(list.Head);
        }

        [Fact]
        public void SingleElement()
        {
            LinkedListToy<int> list = new LinkedListToy<int>(1);
            C02Q01.RemoveDupes(list);
            Assert.NotNull(list.Head);
            Assert.Equal(1, list.Head.Data);
            Assert.Null(list.Head.Next);
        }

        [Fact]
        public void TwoElements()
        {
            LinkedListToy<int> list = new LinkedListToy<int>(7, 7);
            C02Q01.RemoveDupes(list);
            Assert.NotNull(list.Head);
            Assert.Equal(7, list.Head.Data);
            Assert.Null(list.Head.Next);
        }

        [Fact]
        public void NoDupes()
        {
            LinkedListToy<int> list = new LinkedListToy<int>(7, 8, 9);
            C02Q01.RemoveDupes(list);
            Assert.NotNull(list.Head);
            Assert.Equal(7, list.Head.Data);
            Assert.Equal(8, list.Head.Next.Data);
            Assert.Equal(9, list.Head.Next.Next.Data);
            Assert.Null(list.Head.Next.Next.Next);
        }

        [Fact]
        public void ShortTest()
        {
            LinkedListToy<int> list = new LinkedListToy<int>(1, 2, 2, 3, 1);
            C02Q01.RemoveDupes(list);

            var node = list.Head;
            int[] expectedOutcome = { 1, 2, 3 };
            foreach (int i in expectedOutcome)
            {
                Assert.Equal(i, node.Data);
                node = node.Next;
            }
            Assert.Null(node);
        }

    }
}
