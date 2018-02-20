//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace CodilityRockstarA01
{
    class Solution
    {
        public string solution(string S)
        {
            StringBuilder result = new StringBuilder();
            int digitsInFinalBlock;
            int digitsLength;

            foreach (char c in S)
            {
                if (Char.IsDigit(c))
                {
                    result.Append(c);
                }
            }

            digitsLength = result.Length;
            digitsInFinalBlock = digitsLength % 3; // a single digit "in" the final block means that the *two* final blocks will have the form: DD-DD

            for (int i = digitsLength - digitsInFinalBlock - 3; i > 0; i -= 3)
            {
                result.Insert(i, '-');
            }

            if (digitsLength >= 4 && digitsInFinalBlock == 2 || digitsInFinalBlock == 1)
            {
                result.Insert(result.Length - 2, '-');
            }

            return result.ToString();
        }

    }

    public class R01_Tests
    {
        private readonly ITestOutputHelper output;

        public R01_Tests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData("00-44  48 5555 8361", "004-448-555-583-61")]
        [InlineData("0 - 22 1985--324", "022-198-53-24")]
        [InlineData("555372654", "555-372-654")]
        [InlineData("-55--------------", "55")]
        [InlineData("-----------123-", "123")]
        [InlineData("1234", "12-34")]
        [InlineData("12345", "123-45")]
        [InlineData("123456", "123-456")]
        [InlineData("1234567", "123-45-67")]
        [InlineData("00", "00")]
        [InlineData("000", "000")]
        [InlineData("0000", "00-00")]
        [InlineData("00000", "000-00")]
        [InlineData("000000", "000-000")]
        [InlineData("0000000", "000-00-00")]

        public void Examples(string S, string expected)
        {
            Solution s = new Solution();
            string result = s.solution(S);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void HundredDigits()
        {
            StringBuilder input = new StringBuilder();
            StringBuilder expected = new StringBuilder();

            for (int i = 0; i < 100; i++)
            {
                input.Append('0');
            }

            int digitsInBlock = 0;
            for (int i = 0; i < 96; i++)
            {
                expected.Append('0');
                digitsInBlock++;
                if(digitsInBlock == 3)
                {
                    expected.Append('-');
                    digitsInBlock = 0;
                }
            }
            expected.Append("00-00");

            Solution s = new Solution();
            string result = s.solution(input.ToString());
            Assert.Equal(expected.ToString(), result);
            
            output.WriteLine("In:  {0}", input.ToString());
            output.WriteLine("Out: {0}", expected.ToString());
        }

    }

}

