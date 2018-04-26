using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EPI.C11_Search
{
    public static class Q08
    {
        public static int FindKthLargest(int[] array, int k) // k is *not* zero-based, i.e. there is no "0st" largest element
        {
            if (k < 1 || k > array.Length)
                throw new InvalidOperationException();

            int pivot = -1;
            int targetIndex = array.Length - k;
            int low = 0;
            int high = array.Length - 1;
            Random random = new Random();

            while (pivot != targetIndex)
            {
                pivot = random.Next(low, high + 1); // "max value" arg is non-inclusive
                int pivotValue = array[pivot];
                int left = low;
                pivot = left;
                int right = high;

                while (left < right)
                {
                    if (array[left] < pivotValue)
                    {
                        left++;
                    }
                    else if (array[left] > pivotValue)
                    {
                        array.Swap(left, right);
                        right--;
                    }
                    else
                    {
                        array.Swap(left, left + 1);
                    }
                }

                if (left == targetIndex)
                    return array[left];
                else if (left < targetIndex)
                    low = left;
                else
                    high = left;
            }
            return pivot;
        }

        internal static void Swap(this int[] array, int indexA, int indexB)
        {
            int temp = array[indexA];
            array[indexA] = array[indexB];
            array[indexB] = temp;
        }

    }

    public class ArraySwap_Tests
    {
        [Fact]
        public static void ArraySwapTest()
        {
            int[] a = { 1, 2, 3, 4, 5 };
            a.Swap(0, 4);
            Assert.Equal(new int[] { 5, 2, 3, 4, 1 }, a);
        }
    }

    public class C11Q08_Tests
    {
        [Fact]
        public void Example()
        {
            int[] input = { 3, 2, 1, 5, 4 };
            Assert.Equal(5, Q08.FindKthLargest(new int[] { 3, 2, 1, 5, 4 }, 1));
            Assert.Equal(3, Q08.FindKthLargest(new int[] { 3, 2, 1, 5, 4 }, 3));
            Assert.Equal(1, Q08.FindKthLargest(new int[] { 3, 2, 1, 5, 4 }, 5));
        }
    }
}
