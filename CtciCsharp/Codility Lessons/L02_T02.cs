using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Codility_L02_T02_CyclicRotation
{
    class Solution
    {
        public int[] solution(int[] A, int K)
        {
            int[] result = new int[A.Length];
            if (A.Length == 0)
            {
                return result;
            }

            int length = A.Length;
            int offset = length - (K % length); // K could be greater than A.Length
            int charsLeft = length;

            int i = 0;
            while (charsLeft > 0)
            {
                result[i] = A[offset % length];
                offset++;
                i++;
                charsLeft--;
            }

            foreach (int k in result)
                System.Console.WriteLine(k);
            return result;
        }

    }

    [TestClass]
    public class C_L02_Tests
    {

        [TestMethod]
        public void Ex1()
        {
            Solution s = new Solution();
            int[] result = s.solution(new int[] { 3, 8, 9, 7, 6 }, 1);
            CollectionAssert.AreEqual(new int[] { 6, 3, 8, 9, 7 }, result);
        }

        [TestMethod]
        public void Ex2()
        {
            Solution s = new Solution();
            int[] result = s.solution(new int[] { 3, 8, 9, 7, 6 }, 3);
            CollectionAssert.AreEqual(new int[] { 9, 7, 6, 3, 8 }, result);
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
