using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Codility_Lesson02_OddOccurrencesInArray
{
    class Solution
    {
        public int solution(int[] A)
        {
            HashSet<int> s = new HashSet<int>();
            foreach (int i in A)
            {
                if (s.Contains(i))
                {
                    s.Remove(i);
                }
                else
                {
                    s.Add(i);
                }
            }
            return s.Take(1).First();
        }

    }

    [TestClass]
    public class C_L02_Tests
    {

        [TestMethod]
        public void Ex1()
        {
            Solution s = new Solution();
            int result = s.solution(new int[] { 9, 3, 9, 3, 9, 7, 9 });
            Assert.AreEqual(7, result);
        }

    }

}
