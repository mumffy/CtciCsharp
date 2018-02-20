using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CtciCsharp.Chapter01
{
    static class C01q06
    {
        /// <summary>
        /// ->  input contains only upper and lowercase chars
        ///  *  honor case-sensitivity
        ///  *  if "compressed" string length >= original length, return original string
        ///  ?  is there some easy way to tell if the compressed string won't be shorter?
        ///         no, we'd have to look at the entire input, anyway
        ///     
        /// BF  walk over entire string, for each char looked at, check if "next" chars are 
        ///     the same and count them, put this count into the "compressed" output => O(n)
        ///     
        /// BCR O(n) time because every char of the input (of length n chars) must be looked at
        /// 
        /// Elapsed Time: 35min
        /// </summary>
        public static string CompressString(string input)
        {
            StringBuilder result = new StringBuilder();
            char currentChar;
            int sameCharCount;

            int i = 0;
            while (i < input.Length)
            {
                currentChar = input[i];
                result.Append(currentChar);
                sameCharCount = 1;
                i++;

                while (i < input.Length && currentChar == input[i])
                {
                    sameCharCount++;
                    i++;
                }
                result.Append(sameCharCount);
            }

            if (input.Length <= result.Length)
            {
                return input;
            }
            return result.ToString();
        }
    }

    public class C01q06_StringCompression_Tests
    {
        [Fact]
        public void Sample()
        {
            string result = C01q06.CompressString("aabcccccaaa");
            Assert.Equal("a2b1c5a3", result);
        }

        [Fact]
        public void Empty()
        {
            string result = C01q06.CompressString("");
            Assert.Equal("", result);
        }

        [Fact]
        public void CompressedToSameLength()
        {
            string result = C01q06.CompressString("aabccc");
            Assert.Equal("aabccc", result);
        }

        [Fact]
        public void DifferentCasesInputShorter()
        {
            string result = C01q06.CompressString("aAbcCc");
            Assert.Equal("aAbcCc", result);
        }

        [Fact]
        public void DifferentCasesCompressionShorter()
        {
            string result = C01q06.CompressString("aaaaaaAAAAAbbbcccCCCCCCc");
            Assert.Equal("a6A5b3c3C6c1", result);
        }
    }
}
