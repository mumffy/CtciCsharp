using System;
using System.Collections.Generic;
using Xunit;

namespace EPI.C13_Sorting
{
    static class Q01
    {
        public static int[] IntersectSortedArrays(params int[][] inputs)
        {
            if (inputs.Length == 2)
                return InterserctTwoSortedArrays(inputs[0], inputs[1]);

            List<int> result = new List<int>();

            foreach (int[] array in inputs)
            {

            }

            return null;
        }

        private static int[] InterserctTwoSortedArrays(int[] a, int[] b)
        {
            List<int> results = new List<int>();
            int aIndex = 0;
            int bIndex = 0;
            int aVal, bVal;

            while (aIndex < a.Length && bIndex < b.Length)
            {
                aVal = a[aIndex];
                bVal = b[bIndex];
                if (aVal < bVal)
                {
                    aIndex++;
                }
                else if (aVal > bVal)
                {
                    bIndex++;
                }
                else
                {
                    results.AddIfNotPresent(aVal);
                    aIndex++;
                    bIndex++;
                }
            }

            return results.ToArray();
        }

        private static void AddIfNotPresent(this List<int> list, int number)
        {
            if (list.Count == 0 || list[list.Count - 1] < number)
                list.Add(number);
        }
    }

    public class C13Q01_Tests
    {
        [Theory]
        [InlineData(new int[] { 5, 6, 8 },
            new int[] { 2, 3, 3, 5, 5, 6, 7, 7, 8, 12 },
            new int[] { 5, 5, 6, 8, 8, 9, 10, 10 })]
        public void Example(int[] expected, params int[][] inputs)
        {
            Assert.Equal(expected, Q01.IntersectSortedArrays(inputs));
        }

    }
}
