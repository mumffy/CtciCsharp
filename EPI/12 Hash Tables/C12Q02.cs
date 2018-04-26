using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EPI.C12_HashTables
{
    static class Q02
    {
        // [A] whitespace can be ignored
        // [A] case insensitive
        // [A] punctuation must also be "clipped" from the magazine
        public static bool CanMakePsychoLetter(string letter, string magazine)
        {
            Dictionary<char, int> charCount = new Dictionary<char, int>();
            char c;

            foreach (char theChar in letter)
            {
                if (Char.IsWhiteSpace(theChar))
                    continue;

                c = Char.ToLower(theChar);
                if (!charCount.ContainsKey(c))
                    charCount[c] = 0;
                charCount[c]++;
            }

            foreach(char theChar in magazine)
            {
                c = char.ToLower(theChar);

                if (char.IsWhiteSpace(c) || !charCount.ContainsKey(c))
                    continue;

                charCount[c]--;
                if (charCount[c] == 0)
                    charCount.Remove(c);

                if (charCount.Count == 0)
                    return true;
            }

            return charCount.Count == 0;
        }
    }

    public class C12Q02_Tests
    {
        [Theory]
        [InlineData("abc",
                    "cacabc", true)]
        [InlineData("abc",
                    "cacabcd", true)]
        [InlineData("abc",
                    "caca", false)]
        [InlineData("abc",
                    "cacad", false)]
        public void Example(string letter, string magazine, bool expected)
        {
            Assert.Equal(expected, Q02.CanMakePsychoLetter(letter, magazine));
        }
    }
}
