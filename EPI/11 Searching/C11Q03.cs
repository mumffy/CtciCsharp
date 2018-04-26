using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EPI.C11_Search
{
    static class Q03
    {
        public static int FindSmallestInCyclicallySortedArray(int[] array)
        {
            int low = 0;
            int high = array.Length - 1;
            int mid, midPrev;

            while (low <= high)
            {
                mid = (low + high) / 2;
                midPrev = mid == 0 ? array.Length - 1 : mid - 1;
                if (array[midPrev] > array[mid])
                    return mid;

                if (array[mid] <= array[high])
                {
                    high = mid - 1;
                }
                else if (array[mid] > array[high])
                {
                    low = mid + 1;
                }
            }

            return -1;
        }
    }

    public class C11Q03_Tests
    {
        [Fact]
        public void Example()
        {
            int[] input = new int[] { 378, 478, 550, 631, 103, 203, 220, 234, 279, 369 };
            Assert.Equal(4, Q03.FindSmallestInCyclicallySortedArray(input));
        }

        [Theory]
        [InlineData(new int[] { 378, 478, 550, 631, 103, 203, 220, 234, 279, 369 }, 4)]
        [InlineData(new int[] { 103, 203, 220, 234, 279, 369, 378, 478, 550, 631 }, 0)]
        [InlineData(new int[] { 203, 220, 234, 279, 369, 378, 478, 550, 631, 103 }, 9)]
        public void Tests(int[] input, int expected)
        {
            Assert.Equal(expected, Q03.FindSmallestInCyclicallySortedArray(input));
        }
    }
}
