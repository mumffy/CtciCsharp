using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Xunit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility_L03_T03_FrogJmp
{
    class Solution
    {
        public int solution(int X, int Y, int D)
        {
            int diff = Y - X;
            int jumps = (diff / D) + (diff % D > 0 ? 1 : 0);
            return jumps;
        }

    }

    public class C_L03_T03_Tests
    {

        //[Theory]
        //[InlineData(10, 85, 30, 3)]
        //[DataRow(10, 85, 30, 3)]
        //[DataTestMethod]
        public void Ex1(int X, int Y, int D, int expected)
        {
            Solution s = new Solution();
            int result = s.solution(X, Y, D);
            Assert.AreEqual(expected, result);
        }

    }
    
}
