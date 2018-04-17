using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace EPI.C15_Recursion
{
    public class Q03
    {
        private int[] input;
        private List<int[]> results;

        public Q03()
        {
            results = new List<int[]>();
        }

        public List<int[]> EpiGeneratePermutations(int[] input)
        {
            this.input = (int[])input.Clone();
            EpiGeneratePermutations(0);
            return results;
        }

        public void EpiGeneratePermutations(int len)
        {
            if (len == input.Length)
                results.Add(input.Clone() as int[]);

            for (int i = len; i < input.Length; i++)
            {
                int temp = input[i];
                input[i] = input[len];
                input[len] = temp;
                EpiGeneratePermutations(len + 1);
                temp = input[i];
                input[i] = input[len];
                input[len] = temp;
            }
        }

        /// <summary>
        /// > The input array contains *distinct* integers 
        /// </summary>
        public List<int[]> GeneratePermutations(int[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {

            }
            return results;
        }


        public void GeneratePermutations(int[] input, int[] permutation)
        {
            if (input.Length == 0)
                results.Add(permutation);

            foreach (int i in input)
            {
                List<int> a = new List<int>();
            }
        }
    }

    public class C15Q03_Tests
    {
        private readonly ITestOutputHelper output;

        public C15Q03_Tests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Example()
        {
            var result = new Q03().EpiGeneratePermutations(new int[] { 2, 3, 5, 7 });
            Assert.Equal(24, result.Count);

            HashSet<int[]> h = new HashSet<int[]>();
            foreach (int[] a in result)
                h.Add(a);
            Assert.Equal(24, h.Count);
        }

        [Fact]
        public void Test()
        {
            var result = new Q03().EpiGeneratePermutations(new int[] { 1, 2, 3 });
            Assert.Equal(6, result.Count);
            Assert.True(result.ContainsEquivalent(new int[] { 1, 2, 3 }));
            Assert.True(result.ContainsEquivalent(new int[] { 1, 3, 2 }));
            Assert.True(result.ContainsEquivalent(new int[] { 2, 1, 3 }));
            Assert.True(result.ContainsEquivalent(new int[] { 2, 3, 1 }));
            Assert.True(result.ContainsEquivalent(new int[] { 3, 1, 2 }));
            Assert.True(result.ContainsEquivalent(new int[] { 3, 2, 1 }));
        }



    }

    public static class ListOfArrayExtensions
    {
        public static bool ContainsEquivalent(this List<int[]> listOfArrays, int[] array)
        {
            return listOfArrays.Where(x => x.SequenceEqual(array)).Any();
        }
    }
}