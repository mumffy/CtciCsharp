using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Codility_Lesson01
{
    class Solution
    {
        public int solution(int N)
        {
            int potentialGap = 0;
            int longestGapLength = 0;

            string binary = Convert.ToString(N, 2);

            System.Console.WriteLine(binary);

            int i = 0;
            while(i < binary.Length)
            {
                if (binary[i] == '1')
                {
                    potentialGap = 0;
                    i++;
                    while (i < binary.Length && binary[i] == '0')
                    {
                        i++;
                        potentialGap += 1;
                    }
                    if (i < binary.Length && binary[i] == '1' && potentialGap > longestGapLength)
                    {
                        longestGapLength = potentialGap;
                    }

                }
                else
                {
                    i++;
                }
            }

            return longestGapLength;
        }

    }

    [TestClass]
    public class X_tests
    {

        [TestMethod]
        public void Ex1()
        {
            Solution s = new Solution();
            int result = s.solution(9);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Ex2()
        {
            Solution s = new Solution();
            int result = s.solution(529);
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void Ex3()
        {
            Solution s = new Solution();
            int result = s.solution(20);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Ex4()
        {
            Solution s = new Solution();
            int result = s.solution(15);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Ex5()
        {
            Solution s = new Solution();
            int result = s.solution(1041);
            Assert.AreEqual(5, result);
        }


        [TestMethod]
        public void Ex6()
        {
            Solution s = new Solution();
            int result = s.solution(2147483647);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void LongestGapAtEnd()
        {
            Solution s = new Solution();
            int result = s.solution(41);
            Assert.AreEqual(2, result);
        }

        

    }

}
