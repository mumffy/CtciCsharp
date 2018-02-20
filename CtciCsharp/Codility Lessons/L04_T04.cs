//using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility_L04_T04_MaxCounters
{
    class Solution
    {
        public int[] solution(int N, int[] A)
        {
            int[] result = new int[N];
            int item;
            int maxValue = 0;
            bool isMaxValueSet = false;

            for (int i = 0; i < A.Length; i++)
            {
                item = A[i] - 1;
                if (item < N)
                {
                    result[item]++;
                    isMaxValueSet = false;
                    if (result[item] > maxValue)
                    {
                        maxValue = result[item];
                    }
                }
                else if (item == N && !isMaxValueSet)
                {
                    isMaxValueSet = true;
                    for (int j = 0; j < result.Length; j++)
                    {
                        result[j] = maxValue;
                    }
                }
            }

            return result;
        }

        public int[] solutionX(int N, int[] A)
        {
            int[] result = new int[N];
            int item;
            int maxValue = 0;

            int lastMaxCounterIndex = Array.LastIndexOf(A, N + 1);

            if (lastMaxCounterIndex != -1)
            {
                for (int i = lastMaxCounterIndex; i >= 0; i--)
                {
                    item = A[i] - 1;
                    if (item < N)
                    {
                        result[item]++;
                        if (result[item] > maxValue)
                        {
                            maxValue = result[item];
                        }
                    }
                }

                for (int k = 0; k < result.Length; k++)
                {
                    result[k] = maxValue;
                }
            }
            else
            {
                lastMaxCounterIndex = 0;
            }

            for (int i = lastMaxCounterIndex; i < A.Length; i++)
            {
                item = A[i] - 1;
                if (item < N)
                {
                    result[item]++;
                }
            }

            return result;
        }

    }

    public class C_L04_T04_Tests
    {

        [Theory]
        [InlineData(5, new int[] { 3, 4, 4, 6, 1, 4, 4 }, new int[] { 3, 2, 2, 4, 2 })]
        public void Ex1(int N, int[] A, int[] expected)
        {
            Solution s = new Solution();
            int[] result = s.solution(N, A);
            Assert.Equal(expected, result);
        }

    }

}
