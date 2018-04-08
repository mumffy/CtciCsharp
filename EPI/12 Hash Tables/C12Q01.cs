using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EPI.C12_HashTables
{
    static class Q01
    {
        public static bool CanFormPalindrome(string s)
        {
            Dictionary<char, int> charCount = new Dictionary<char, int>();
            bool result = false;
            int charsWithOddCount = 0;

            foreach (char theChar in s)
            {
                char c = Char.ToLower(theChar);
                if (!charCount.ContainsKey(c))
                {
                    charCount[c] = 1;
                    charsWithOddCount++;
                }
                else
                {
                    charCount[c]++;
                    if (charCount[c] % 2 == 0)
                        charsWithOddCount--;
                }
            }
            return charsWithOddCount <= 1;
        }
    }

    public class C12Q01_Tests
    {
        [Theory]
        [InlineData("edified", true)]
        [InlineData("levle", true)]
        [InlineData("rotator", true)]
        [InlineData("rotaToR", true)]
        [InlineData("abc", false)]
        public void Example(string input, bool expected)
        {
            Assert.Equal(expected, Q01.CanFormPalindrome(input));
        }
    }
}
