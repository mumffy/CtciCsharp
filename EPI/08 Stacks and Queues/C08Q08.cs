using EPI.DataStructures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using EPI.C08_Stacks_and_Queues.Q08;

namespace EPI.C08_Stacks_and_Queues.Q08
{
    interface ICircularQueue<T>
    {
        void Enqueue(T value);
        T Dequeue();
        int Count { get; }
        int Capacity { get; }
    }

    public class CircularQueue<T> : ICircularQueue<T>
    {
        static readonly int CAPACITY = 100;
        private int capacity;
        private int count = 0;
        private T[] array;
        private int frontIndex = 0;
        private int backIndex = -1;

        public int Capacity => capacity;
        public int Count => count;

        public CircularQueue() : this(CAPACITY)
        {
        }
        public CircularQueue(int capacity)
        {
            this.capacity = capacity;
            array = new T[capacity];
        }


        public void Enqueue(T value)
        {
            if (count == capacity)
            {
                capacity *= 2;
                Array.Resize(ref array, capacity);
                
            }
            backIndex = (backIndex + 1) % capacity;
            array[backIndex] = value;
            count++;
        }

        public T Dequeue()
        {
            if(count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }
            T item = array[frontIndex];
            frontIndex = (frontIndex + 1) % capacity;
            count--;
            return item;
        }
    }

    public class C08Q08_Tests
    {

        [Fact]
        public void Resize_Test()
        {
            const int capacity = 5;
            ICircularQueue<string> queue = new CircularQueue<string>(capacity);
            Assert.Equal(capacity, queue.Capacity);

            queue.Enqueue("apple");
            queue.Enqueue("banana");
            queue.Enqueue("coconut");
            Assert.Equal(3, queue.Count);
            queue.Enqueue("durian");
            queue.Enqueue("eggplant");
            queue.Enqueue("fig");
            Assert.Equal(6, queue.Count);
            Assert.Equal(10, queue.Capacity);

            Assert.Equal("apple", queue.Dequeue());
            Assert.Equal(5, queue.Count);
            Assert.True(queue.Capacity > 5);
            Assert.Equal("banana", queue.Dequeue());
            Assert.Equal(4, queue.Count);
            Assert.Equal("coconut", queue.Dequeue());
            Assert.Equal("durian", queue.Dequeue());
            Assert.Equal("eggplant", queue.Dequeue());
            Assert.Equal(1, queue.Count);
            Assert.Equal("fig", queue.Dequeue());
            Assert.Equal(0, queue.Count);
        }

        [Fact]
        public void Circular_Test()
        {
            const int capacity = 3;
            ICircularQueue<string> queue = new CircularQueue<string>(capacity);
            Assert.Equal(capacity, queue.Capacity);

            queue.Enqueue("apple");
            queue.Enqueue("banana");
            queue.Enqueue("coconut");
            Assert.Equal("apple", queue.Dequeue());
            Assert.Equal(2, queue.Count);

            queue.Enqueue("durian");
            Assert.Equal(capacity, queue.Capacity);
            Assert.Equal(3, queue.Count);

            Assert.Equal("banana", queue.Dequeue());
            Assert.Equal(capacity, queue.Capacity);
            Assert.Equal(2, queue.Count);

            queue.Enqueue("eggplant");
            Assert.Equal(capacity, queue.Capacity);
            Assert.Equal(3, queue.Count);

            Assert.Equal("coconut", queue.Dequeue());
            Assert.Equal(capacity, queue.Capacity);
            Assert.Equal(2, queue.Count);

            queue.Enqueue("fig");
            Assert.Equal(capacity, queue.Capacity);
            Assert.Equal(3, queue.Count);

            Assert.Equal("durian", queue.Dequeue());
            Assert.Equal(2, queue.Count);
            Assert.Equal("eggplant", queue.Dequeue());
            Assert.Equal(1, queue.Count);
            Assert.Equal("fig", queue.Dequeue());
            Assert.Equal(0, queue.Count);
            Assert.Equal(capacity, queue.Capacity);
        }
    }
}
