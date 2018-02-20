using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility_L03_T02_TapeEquilibrium
{
    class Solution
    {
        public int solution(int[] A)
        {
            int totalSum = 0;
            int minimalDifference = 999998;

            for (int i = 0; i < A.Length; i++)
            {
                totalSum += A[i];
            }

            int tapeFrontSum = A[0];
            int tapeBackSum;
            int diff;
            for (int i = 1; i < A.Length; i++)
            {
                tapeBackSum = totalSum - tapeFrontSum;
                diff = Math.Abs(tapeFrontSum - tapeBackSum);
                if (diff < minimalDifference)
                {
                    minimalDifference = diff;
                }
                tapeFrontSum += A[i];
            }

            return minimalDifference;
        }

    }

    [TestClass]
    public class C_L03_T02_Tests
    {

        [TestMethod]
        public void Ex1()
        {
            Solution s = new Solution();
            int result = s.solution(new int[] { 3, 1, 2, 4, 3 });
            Assert.AreEqual(1, result);
        }

    }

    class CollectionAssertComperator : IComparer
    {
        public int Compare(object x, object y)
        {
            CollectionAssert.AreEqual((ICollection)x, (ICollection)y);
            return 0;
        }
    }
}
