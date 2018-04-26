﻿using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace EPI.C05_Arrays
{

    public static class Toy
    {
        public static void ZeroFill(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = 0;
            }
        }
    }
    public static class C05Q01
    {
        public static void Partition(int[] a, int i) // i is the index of the "pivot"
        {
            int pivot = a[i];
            int bottom = 0;
            int middle = 0;
            int top = a.Length - 1;

            while (bottom < top && middle < top)
            {
                if (a[bottom] < pivot)
                {
                    bottom++;
                    middle++;
                }
                else if (a[bottom] > pivot)
                {
                    a.Swap(bottom, top);
                    top--;
                }
                else
                {
                    middle++;
                    a.Swap(bottom, middle);
                }
            }
        }

        #region int[] Swap extension method
        private static void Swap(this int[] array, int a, int b)
        {
            if (a >= array.Length || b >= array.Length || a < 0 || b < 0 || a == b)
                return;
            int temp = array[a];
            array[a] = array[b];
            array[b] = temp;
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 1, 3, new int[] { 1, 4, 3, 2, 5 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 1, 0, new int[] { 2, 1, 3, 4, 5 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 0, 4, new int[] { 5, 2, 3, 4, 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 0, 5, new int[] { 1, 2, 3, 4, 5 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, -1, 4, new int[] { 1, 2, 3, 4, 5 })]
        public static void SwapTests(int[] a, int x, int y, int[] e)
        {
            a.Swap(x, y);
            Assert.Equal(a, e);
        }
        #endregion 
    }

    public class C05Q01_Tests
    {
        private readonly ITestOutputHelper output;

        public C05Q01_Tests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ArrayElementsModified()
        {
            int[] a = { 1, 2, 3 };
            Toy.ZeroFill(a);
            Assert.Equal(0, a[0]);
        }

        [Fact]
        public void Test01()
        {
            int[] a = { 99, 5, 4, 3, 2, 1, 7 };
            int p = 6;
            C05Q01.Partition(a, p);
            Assert.True(validatePartitionOrdering(a, a[p]));
            output.WriteLine(String.Join(" ", a));
        }

        [Fact]
        public void Test02()
        {
            int[] a = { 99, 3, 2, 7, 1, 4, 5, 7 };
            int p = 6;
            C05Q01.Partition(a, p);
            Assert.True(validatePartitionOrdering(a, a[p]));
            output.WriteLine(String.Join(" ", a));
        }

        [Theory]
        [InlineData(new int[] { 2, 1 }, 0)]
        [InlineData(new int[] { 2, 1 }, 1)]
        [InlineData(new int[] { 1, 3, 1 }, 1)]
        [InlineData(new int[] { 2, 2, 2, 2, 2, 1 }, 0)]
        [InlineData(new int[] { 2, 9, 2, 2, 2, 2, 1 }, 0)]
        [InlineData(new int[] { 2, 1, 1, 1, 1, 1, 9 }, 0)]
        public void Test03(int[] a, int p)
        {
            C05Q01.Partition(a, p);
            Assert.True(validatePartitionOrdering(a, a[p]));
            output.WriteLine(String.Join(" ", a));
        }

        private bool validatePartitionOrdering(int[] a, int pivot)
        {
            if (!a.Contains(pivot))
                return false;

            int x = 0;
            while (x < a.Length && a[x] != pivot)
            {
                if (a[x] > pivot)
                    return false;
                x++;
            }
            while (x < a.Length && a[x] == pivot)
            {
                x++;
            }
            while (x < a.Length)
            {
                if (a[x] < pivot)
                    return false;
                x++;
            }


            return true;
        }

        #region tests for validatePartitionOrdering method that drives the "real" Tests
        [Fact]
        public void OrderValidation_Right01()
        {
            int[] a = { 2, 1, 3, 4, 7, 5 };
            int p = 4;
            Assert.True(validatePartitionOrdering(a, p));
        }

        [Fact]
        public void OrderValidation_Right02()
        {
            int[] a = { 2, 1, 3, 4, 7, 5, 9, 9, 9, 99, 11 };
            int p = 9;
            Assert.True(validatePartitionOrdering(a, p));
        }

        [Theory]
        [InlineData(new int[] { 2, 3, 4, 7, 5, 9, 9, 9, 99, 11 }, 2)]
        [InlineData(new int[] { 9, 9, 9, 9, 2, 1, 3, 4, 7, 5, 11 }, 11)]
        public void OrderValiation_PivotAtEnd(int[] a, int p)
        {
            Assert.True(validatePartitionOrdering(a, p));
        }

        [Fact]
        public void OrderValidation_Wrong01()
        {
            int[] a = { 8, 1, 3, 4, 7, 5 };
            int p = 4;
            Assert.False(validatePartitionOrdering(a, p));
        }
        #endregion
    }
}
