using System;
using System.Collections.Generic;

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

            foreach(char key in xCharCount.Keys)
            {
                if (!yCharCount.ContainsKey(key))
                {
                    return false;
                }
                if(xCharCount[key] != yCharCount[key])
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

        public string Q03_URLify(char[] s, int length)
        {
            // what about trailing whitespace?
            //   since it

            throw new NotImplementedException();
        }
    }
}
