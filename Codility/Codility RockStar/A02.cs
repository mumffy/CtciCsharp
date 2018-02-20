//using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodilityRockstarA02
{
    class Solution
    {
        public int solution(int A, int B)
        {
            string a = A.ToString();
            string b = B.ToString();
            return b.IndexOf(a);
        }

    }

    public class R02_Tests
    {

        [Theory]
        [InlineData(53, 1953786, 2)]
        [InlineData(0, 0, 0)]
        [InlineData(0, 999999999, -1)]
        [InlineData(9, 999999999, 0)]
        [InlineData(1234, 191239999, -1)]
        [InlineData(999999999, 999999999, 0)]
        [InlineData(99999999, 199999999, 1)]
        public void Examples(int A, int B, int expected)
        {
            Solution s = new Solution();
            int result = s.solution(A, B);
            Assert.Equal(expected, result);
        }

    }

}
