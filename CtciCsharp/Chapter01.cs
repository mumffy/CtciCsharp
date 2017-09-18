using System;
using System.Collections.Generic;
using Xunit;

namespace CtciCsharp
{
    public class Ch01
    {
        public bool Q01_IsUnique(string input)
        {
            List<char> seenChars = new List<char>();
            foreach (char c in input)
            {
                // TODO List.Contains vs Dict.Contains runtime?
                if (seenChars.Contains(c))
                {
                    return false;
                }
                seenChars.Add(c);
            }
            return true;
        }

        public bool Q01X_IsUnique_NoAddDSes(string input)
        {
            // BF: foreach char in string, walk rest of string to check for same char
            // better: do in-place sort on the string - can achieve O(nlogn) with Heapsort,
            //   then just walk through now-sorted string, and check adjacent chars for dupes
            throw new NotImplementedException();
        }

        public bool Q02_CheckPermutation(string x, string y)
        {
            // ? case sensitivity?

            // BF: could just walk through each string and count char occurrances for 2*O(n) + C
            // less space: sort each string in-place for 2*O(nlogn), then see if two strings are same

            if (x.Length != y.Length)
            {
                return false;
            }

            Dictionary<char, int> xCharCount = new Dictionary<char, int>();
            Dictionary<char, int> yCharCount = new Dictionary<char, int>();
            Q02_CountChars(x, xCharCount);
            Q02_CountChars(y, yCharCount);

            foreach (char key in xCharCount.Keys)
            {
                if (!yCharCount.ContainsKey(key))
                {
                    return false;
                }
                if (xCharCount[key] != yCharCount[key])
                {
                    return false;
                }
            }
            return true;
        }

        private void Q02_CountChars(string x, Dictionary<char, int> charCount)
        {
            foreach (char c in x)
            {
                if (!charCount.ContainsKey(c))
                {
                    charCount[c] = 1;
                }
                else
                {
                    charCount[c]++;
                }
            }
        }

        public char[] Q03_URLify(char[] s, int trueLength)
        {
            // ? what about other types of whitespace?
            //     too complicated for now...
            // ? what if it's all whitespace?
            //     just return as-is

            const string REPLACEMENT = "%20";
            const int REPLEN = 3;

            int i = trueLength - 1;

            int spaceCount = 0;
            for (int j = 0; j < trueLength; j++)
            {
                if (Char.IsWhiteSpace(s[j]))
                    spaceCount++;
            }
            if (spaceCount == 0)
                return s;

            // now i is index of first non-whitespace char
            while (i >= 0)
            {
                int delta = spaceCount * (REPLEN - 1);

                if (!Char.IsWhiteSpace(s[i]))
                {
                    s[i + delta] = s[i];
                }
                else
                {
                    for (int x = 0; x < REPLEN; x++)
                        s[i + delta - (REPLEN-1) + x] = REPLACEMENT[x];
                    spaceCount--;
                }
                i--;
            }

            return s;
        }

        public bool Q04_PalindromePermutation(string s)
        {
            // ASS: whitespace is just ignored
            // ASS: casing is just ignored
            // ?    what about emptystring or all whitespace? 
            //          treat as special case: return true
            // ?    what about null?  return false
            //
            // !    a palindrome must have pairs of chars, except the "middle" char
            // !    just count the chars, all counts must be even, only one may be odd

            throw new NotImplementedException();
        }
    }

    public static class Ch01Q04
    {
        public static bool Q04_PalindromePermutation(string input)
        {
            if (input == null)
                return false;
            if (String.IsNullOrWhiteSpace(input))
                return true;

            Dictionary<char, int> charCount = new Dictionary<char, int>();
            foreach(char c in input.ToLower())
            {
                if (Char.IsWhiteSpace(c))
                    continue;

                if (!charCount.ContainsKey(c))
                {
                    charCount[c] = 0;
                }
                charCount[c]++;
            }

            int oddCount = 0;
            foreach(char key in charCount.Keys)
            {
                if(charCount[key] %2 != 0)
                {
                    oddCount++;
                }
            }

            if (oddCount <= 1)
                return true;
            return false;
        }
    }

    public class Q04_PalindromePermutation_Tests
    {
        [Fact]
        public void Sample()
        {
            Assert.True(Ch01Q04.Q04_PalindromePermutation("Tact Coa"));
        }

        [Theory]
        [InlineData("car rac e")]
        [InlineData("stop pots")]
        [InlineData("   abcba   ")]
        [InlineData("   abccba   ")]
        [InlineData("a")]
        [InlineData("aa")]
        [InlineData("aaa")]
        [InlineData("aaaa")]
        [InlineData("")]
        [InlineData("   ")]
        public void YesItIs(string s)
        {
            Assert.True(Ch01Q04.Q04_PalindromePermutation(s));
        }


        [Theory]
        [InlineData("abc")]
        [InlineData("xy   z")]
        [InlineData("   abcjk   ")]
        [InlineData(null)]
        public void NotItIsnt(string s)
        {
            Assert.False(Ch01Q04.Q04_PalindromePermutation(s));
        }
    }
}
