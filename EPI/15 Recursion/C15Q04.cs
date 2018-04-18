using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace EPI.C15_Recursion
{
    public class Q04
    {
        private HashSet<HashSet<int>> result;
        private int[] array;

        public Q04()
        {
            result = new HashSet<HashSet<int>>();
        }

        public HashSet<HashSet<int>> FindPowerSet(HashSet<int> input)
        {
            array = input.ToArray();
            FindPowerSet(0, new HashSet<int> { });
            return result;
        }

        public void FindPowerSet(int count, HashSet<int> output)
        {
            if (count == array.Length)
            {
                result.Add(output);
                return;
            }

            HashSet<int> h = new HashSet<int>(output);
            HashSet<int> j = new HashSet<int>(output);
            h.Add(array[count]);
            FindPowerSet(count + 1, h);
            FindPowerSet(count + 1, j);
        }
    }

    public class C15Q04_Tests
    {
        [Fact]
        public void Example()
        {
            var result = new Q04().FindPowerSet(new HashSet<int> { 0, 1, 2 });
            Assert.Equal(8, result.Count);
            Assert.True(result.Where(x => x.SequenceEqual(new HashSet<int> { })).Any());
            Assert.True(result.Where(x => x.SequenceEqual(new HashSet<int> { 0 })).Any());
            Assert.True(result.Where(x => x.SequenceEqual(new HashSet<int> { 1 })).Any());
            Assert.True(result.Where(x => x.SequenceEqual(new HashSet<int> { 2 })).Any());
            Assert.True(result.Where(x => x.SequenceEqual(new HashSet<int> { 0, 1 })).Any());
            Assert.True(result.Where(x => x.SequenceEqual(new HashSet<int> { 0, 2 })).Any());
            Assert.True(result.Where(x => x.SequenceEqual(new HashSet<int> { 1, 2 })).Any());
            Assert.True(result.Where(x => x.SequenceEqual(new HashSet<int> { 0, 1, 2 })).Any());
        }
    }

}