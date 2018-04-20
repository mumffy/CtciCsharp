using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EPI.C16_Dynamic_Programming
{
    class Q01
    {
        public static int FindScoreCombinations(int finalScore)
        {
            return FindScoreCombinations(finalScore, new HashSet<string>(), 0, 0, 0);
        }

        private static int FindScoreCombinations(int finalScore, HashSet<string> cache, int twos, int threes, int sevens)
        {
            string cacheKey = String.Format("{0}|{1}|{2}", twos, threes, sevens);
            if (cache.Contains(cacheKey))
                return 0;
            else
                cache.Add(cacheKey);

            int product = twos * 2 + threes * 3 + sevens * 7;
            if (product == finalScore)
                return 1;
            else if (product > finalScore)
                return 0;

            return FindScoreCombinations(finalScore, cache, twos + 1, threes, sevens) +
                   FindScoreCombinations(finalScore, cache, twos, threes + 1, sevens) +
                   FindScoreCombinations(finalScore, cache, twos, threes, sevens + 1);
        }

    }

    public class C16Q01_Tests
    {
        [Fact]
        public void Example()
        {
            Assert.Equal(4, Q01.FindScoreCombinations(12));
        }

        //public void FindTheActualScoreCombinations()
        //{ }
    }
}
