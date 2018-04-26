using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPI.DataStructures.PriorityQueue;
using Xunit;

namespace EPI.C10_Heaps
{
    public static class Q03
    {
        internal static int[] SortKSortedArray(int[] array, int k)
        {
            int index = 0;
            IntBinaryMinHeap heap = new IntBinaryMinHeap();
            List<int> result = new List<int>();

            while (index < array.Length && heap.Count < k + 1)
            {
                heap.Push(array[index++]);
            }

            while (index < array.Length)
            {
                result.Add(heap.Pop());
                heap.Push(array[index++]);
            }

            while(heap.Count > 0)
            {
                result.Add(heap.Pop());
            }

            return result.ToArray();
        }
    }

    public class C10Q03_Tests
    {
        [Fact]
        public void Example()
        {
            int[] input = { 3, -1, 2, 6, 4, 5, 8 };
            var result = Q03.SortKSortedArray(input, 2);
            Assert.Equal(new int[] { -1, 2, 3, 4, 5, 6, 8 }, result);
        }
    }
}
