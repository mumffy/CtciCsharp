using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPI.DataStructures.PriorityQueue;
using Xunit;

namespace EPI.C10_Heaps
{
    public static class Q04
    {
        internal static int[] KSmallestInts(int[] array, int k)
        {
            IntBinaryMaxHeap heap = new IntBinaryMaxHeap();
            int index = 0;
            List<int> result = new List<int>();

            while (index < array.Length && heap.Count < k)
            {
                heap.Push(array[index++]);
            }

            while (index < array.Length)
            {
                heap.Push(array[index++]);
                heap.Pop();
            }

            while (heap.Count > 0)
            {
                result.Add(heap.Pop());
            }

            result.Sort();
            return result.ToArray();
        }
    }

    public class C10Q04_Tests
    {
        [Fact]
        public void Basic()
        {
            int[] input = { 2, 99, 123, 7, 1000, -1, 6, 5, 8, 777, 4 };
            var result = Q04.KSmallestInts(input, 3);
            Assert.Equal(new int[] { -1, 2, 4 }, result);
        }
    }
}
