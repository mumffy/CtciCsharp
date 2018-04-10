using System;
using System.Collections.Generic;
using Xunit;

namespace EPI.C13_Sorting
{
    public class Q02
    {
        // [A] first param will always be the longer one, with "empty" spaces to hold the result of the merge
        public static int?[] MergeTwoSortedArrays(int?[] a, int?[] b)
        {
            int destIndex;
            int ai = 0;
            int bi = b.Length - 1; ;
            int nonEmptyInA = 0;

            while (ai < a.Length && a[ai] != null)
            {
                nonEmptyInA = ++ai;
            }
            ai = nonEmptyInA - 1;
            destIndex = nonEmptyInA + b.Length - 1;


            while (ai >= 0 && bi >= 0)
            {
                if (a[ai] >= b[bi])
                {
                    a[destIndex] = a[ai];
                    ai--;
                }
                else
                {
                    a[destIndex] = b[bi];
                    bi--;
                }
                destIndex--;
            }

            while (bi >= 0)
                a[destIndex--] = b[bi--];
            while (ai >= 0)
                a[destIndex--] = a[ai--];

            return a;
        }
    }

    public class C13Q02_Tests
    {
        [Fact]
        public void Example()
        {
            int?[] a = new int?[] { 5, 13, 17, null, null, null, null, null };
            int?[] b = new int?[] { 3, 7, 11, 19 };
            int?[] expected = new int?[] { 3, 5, 7, 11, 13, 17, 19, null };
            Assert.Equal(expected, Q02.MergeTwoSortedArrays(a, b));
        }

    }
}
