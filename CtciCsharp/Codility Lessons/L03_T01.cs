using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Codility_L03_T01_PermMissingElem
{
    class Solution
    {
        //public int solution(int[] A)
        //{
        //    Array.Sort(A);
        //    for (int i = 0; i < A.Length; i++)
        //    {
        //        if (A[i] != i + 1)
        //        {
        //            return i + 1;
        //        }

        //    }
        //    return A.Length + 1;
        //}

        public int solution(int[] A)
        {
            for (int i = 0; i < A.Length; i++)
            {
                if(Math.Abs(A[i]) <= A.Length)
                {
                    A[Math.Abs(A[i]) - 1] *= -1;
                }
            }
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] > 0)
                {
                    return i + 1;
                }
            }
            return A.Length + 1;
        }

    }

    [TestClass]
    public class C_L03_T01_Tests
    {

        [TestMethod]
        public void Ex1()
        {
            Solution s = new Solution();
            int result = s.solution(new int[] { 2, 3, 1, 5 });
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void Ex2()
        {
            Solution s = new Solution();
            int result = s.solution(new int[] { 1 });
            Assert.AreEqual(2, result);
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
