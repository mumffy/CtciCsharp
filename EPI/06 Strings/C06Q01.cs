using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace EPI.C05_Arrays
{
    public static class C06Q01
    {
        public static int ToInt(this String s)
        {
            int result = 0;
            int multiplier = 1;
            int stopIndex = 0;

            if (s[0] == '-')
            {
                multiplier = -1;
                stopIndex = 1;
            }

            for (int i = s.Length - 1; i >= stopIndex; i--)
            {
                result += (s[i] - '0') * multiplier;
                multiplier *= 10;
            }

            return result;
        }

        public static string Stringify(this int i)
        {
            if (i == 0)
            {
                return "0";
            }

            StringBuilder result = new StringBuilder();
            bool isNegative = i < 0;
            int num = Math.Abs(i);

            while (num > 0)
            {
                result.Insert(0, (char)(num % 10 + '0'));
                num /= 10;
            }

            if (isNegative)
                result.Insert(0, '-');

            return result.ToString();
        }
    }


    public class C06Q01_Tests
    {
        [Fact]
        public void Example_StringToInt()
        {
            Assert.Equal(123, "123".ToInt());
        }

        [Fact]
        public void Example_InToString()
        {
            Assert.Equal("321", 321.Stringify());
        }

        [Theory]
        [InlineData("000", 0)]
        [InlineData("700", 700)]
        [InlineData("-98700", -98700)]
        public void StringToInt(string s, int i)
        {
            Assert.Equal(i, s.ToInt());
        }

        [Theory]
        [InlineData(-123, "-123")]
        [InlineData(0, "0")]
        public void IntToString(int i, string s)
        {
            Assert.Equal(s, i.Stringify());
        }
    }
}

