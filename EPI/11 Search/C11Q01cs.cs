using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EPI.C11_Search
{
    static class Q01
    {
        static public int IndexOfFirstOccurrence(int[] array, int target)
        {
            int low = 0;
            int high = array.Length - 1;
            int mid;
            int found = -1;

            while(low <= high)
            {
                mid = (low + high) / 2;

                if (array[mid] == target)
                    found = mid;

                if (array[mid] >= target)
                {
                    high = mid-1;
                }
                else
                {
                    low = mid+1;
                }
            }

            return found;
        }
    }

    public class C11Q01_Tests
    {
        [Fact]
        public void Example()
        {
            int[] input = { -14, -10, 2, 108, 108, 243, 285, 285, 285, 401 };
            Assert.Equal(0, Q01.IndexOfFirstOccurrence(input, -14));
            Assert.Equal(9, Q01.IndexOfFirstOccurrence(input, 401));
            Assert.Equal(3, Q01.IndexOfFirstOccurrence(input, 108));
            Assert.Equal(6, Q01.IndexOfFirstOccurrence(input, 285));
        }
    }
}
