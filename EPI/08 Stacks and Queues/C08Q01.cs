using System;
using System.Collections.Generic;
using Xunit;

namespace EPI.C08_Stacks_and_Queues
{
    public class StackWithMax<T> where T : IComparable
    {
        private Stack<Item<T>> stack = new Stack<Item<T>>();
        public T Max
        {
            get
            {
                return getMax();
            }
        }

        public void Push(T i)
        {
            T newMax = i;

            if (stack.Count > 0 && Max.CompareTo(i) > 0)
                newMax = Max;

            stack.Push(new Item<T> { value = i, maxSoFar = newMax });
        }

        public T Pop()
        {
            return stack.Pop().value;
        }

        private T getMax()
        {
            return stack.Peek().maxSoFar;
        }

        private class Item<T>
        {
            public T value { get; set; }
            public T maxSoFar { get; set; }
        }
    }

    public class C08Q01_Tests
    {
        [Fact]
        public void Test01()
        {
            StackWithMax<int> stack = new StackWithMax<int>();
            Assert.Throws<InvalidOperationException>(() => stack.Pop());
            Assert.Throws<InvalidOperationException>(() => stack.Max);

            stack.Push(10);
            Assert.Equal(10, stack.Max);

            stack.Push(5);
            Assert.Equal(10, stack.Max);

            stack.Push(99);
            Assert.Equal(99, stack.Max);

            Assert.Equal(99, stack.Pop());
            Assert.Equal(10, stack.Max);

            Assert.Equal(5, stack.Pop());
            Assert.Equal(10, stack.Max);

            Assert.Equal(10, stack.Pop());
            Assert.Throws<InvalidOperationException>(() => stack.Pop());
            Assert.Throws<InvalidOperationException>(() => stack.Max);
        }

    }
}
