using System;
using Xunit;

namespace EPI
{
    public static class DNF
    {
        public static int[] Partition(int[] A, int i)
        {
            if(i >= A.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            int pivot = A[i];
            int processing = 0;
            int bottom;
            int middle;
            int top = A.Length;
            int item;


            while (processing < top)
            {
                item = A[processing];

                if(item < pivot)
                {
                    //bottom

                }
            }
            return A;
        }
    }

    public class DNF_Tests
    {
        [Theory]
        [InlineData(new int[] { 3, 2, 1 }, 1, new int[] { 1, 2, 3 })]
        public void TestMethod1(int[] A, int i, int[] expected)
        {
            Assert.Equal(expected, DNF.Partition(A, i));
        }
    }
}
