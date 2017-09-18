using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CtciCsharp.Chapter01
{
    static class Q05
    {
        // ASS: just ignoring whitespace, assuming it won't be in input strings
        internal static bool IsOneAway(string a, string b)
        {
            int lengthDiff = Math.Abs(a.Length - b.Length);
            if (lengthDiff > 1)
                return false;

            int numDiffs = 0;
            int bigIndex = 0;
            int smallIndex = 0;
            string big = a;
            string small = b;

            if (a.Length < b.Length)
            {
                big = b;
                small = a;
            }

            while(smallIndex < small.Length)
            {
                if(small[smallIndex] != big[bigIndex])
                {
                    numDiffs++;

                    if (numDiffs > 1)
                        return false;
                    else if(lengthDiff > 0)
                    {
                        bigIndex++;
                        continue;
                    }
                }
                smallIndex++;
                bigIndex++;
            }

            return true;            
        }
    }

    public static class Q05_OneAway_Tests
    {
        [Theory]
        [InlineData("pale", "ple")]
        [InlineData("ple", "pale")]
        [InlineData("pales", "pale")]
        [InlineData("pale", "pales")]
        [InlineData("pale", "bale")]
        [InlineData("bale", "pale")]
        [InlineData("abc", "abc")]
        public static void SampleTrue(string a, string b)
        {
            Assert.True(Q05.IsOneAway(a, b));
        }

        [Theory]
        [InlineData("pale", "bake")]
        [InlineData("bake", "pale")]
        [InlineData("bakeXX", "pale")]
        public static void SampleFalse(string a, string b)
        {
            Assert.False(Q05.IsOneAway(a, b));
        }

    }
}
