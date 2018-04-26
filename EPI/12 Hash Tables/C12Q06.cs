using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EPI.C12_HashTables
{
    static class Q06
    {
        // [A] case insensitive
        // [A] ignoring punctuation
        public static string FindNearestRepeats(out int minDistance, string[] input)
        {
            minDistance = int.MaxValue;
            string minDistWord = null;
            Dictionary<string, int> lastSeenIndex = new Dictionary<string, int>();
            string word;

            for (int i = 0; i < input.Length; i++)
            {
                word = input[i].ToLower();
                if (lastSeenIndex.ContainsKey(word) && i - lastSeenIndex[word] < minDistance)
                {
                    minDistance = i - lastSeenIndex[word];
                    minDistWord = word;
                }
                lastSeenIndex[word] = i;
            }
            if (minDistWord == null)
                throw new InvalidOperationException("Input did not contain repeated words.");

            return minDistWord;
        }
    }

    public class C12Q06_Tests
    {
        [Fact]
        public void Example()
        {
            string[] input = "All work and no play makes for no work no fun and no results".Split();
            int distance;
            Assert.Equal("no", Q06.FindNearestRepeats(out distance, input));
            Assert.Equal(2, distance);
        }

        [Theory]
        [InlineData("All work and no play makes for No work nO fun and no results", 2, "no")]
        [InlineData("All aLL and no play makes for No work nO fun and no results", 1, "all")]
        [InlineData("aaa bb ccc d bb ccc d", 3, "bb")]
        [InlineData("a b c d e f g a", 7, "a")]
        [InlineData("a b c a b c d e d", 2, "d")]
        public void Tests(string input, int minDistance, string minDistWord)
        {
            int distance;
            Assert.Equal(minDistWord, Q06.FindNearestRepeats(out distance, input.Split()));
            Assert.Equal(minDistance, distance);
        }

        [Fact]
        public void NoRepeatsThrowsException()
        {
            int distance;
            string[] input = "aaa bbb c d eee fff g hhh jj".Split();
            Assert.Throws<InvalidOperationException>(() => Q06.FindNearestRepeats(out distance, input));
        }
    }
}
