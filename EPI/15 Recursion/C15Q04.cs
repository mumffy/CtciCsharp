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

        public static HashSet<HashSet<int>> FindPowerSet_Iterative(HashSet<int> input)
        {
            HashSet<HashSet<int>> result = new HashSet<HashSet<int>>();
            int[] array = new int[input.Count];

            int i = 0;
            foreach (int number in input)
                array[i++] = number;

            for (int x = 0; x < 1 << input.Count; x++)
            {
                int bitmap = x;
                HashSet<int> thisSet = new HashSet<int>();
                for (int y = 0; y <= input.Count; y++)
                {
                    if ((bitmap & 1) == 1)
                        thisSet.Add(array[y]);
                    bitmap >>= 1;
                }
                result.Add(thisSet);
            }
            return result;
        }

        public HashSet<HashSet<int>> FindPowerSet(HashSet<int> input)
        {
            //array = input.ToArray();
            //FindPowerSet(0, new HashSet<int> { });
            //return result;
            return FindPowerSet_Iterative(input);
        }

        public void FindPowerSet(int count, HashSet<int> output)
        {
            if (count == array.Length)
            {
                result.Add(new HashSet<int>(output));
                return;
            }

            //without array[count] in the answer being generated
            FindPowerSet(count + 1, output);

            //with array[count] in the answer
            output.Add(array[count]);
            FindPowerSet(count + 1, output);
            output.Remove(array[count]);
        }
    }

    public class C15Q04_Tests
    {
        [Fact]
        public void Example()
        {
            var result = new Q04().FindPowerSet(new HashSet<int> { 0, 1, 2 });
            Assert.Equal(8, result.Count);
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 0 })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 1 })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 2 })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 0, 1 })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 0, 2 })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 1, 2 })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 0, 1, 2 })).Any());
        }

        [Fact]
        public void InputIsEmptySet()
        {
            var result = new Q04().FindPowerSet(new HashSet<int> { });
            Assert.Equal(1, result.Count);
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { })).Any());
        }

        [Fact]
        public void InputOfSize1()
        {
            var result = new Q04().FindPowerSet(new HashSet<int> { 0 });
            Assert.Equal(2, result.Count);
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 0 })).Any());
        }

        [Fact]
        public void InputOfSize4()
        {
            var result = new Q04().FindPowerSet(new HashSet<int> { 0, 1, 2, 3 });
            Assert.Equal(16, result.Count);
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 0 })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 1 })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 2 })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 3 })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 0, 1 })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 0, 2 })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 0, 3 })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 1, 2 })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 1, 3 })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 2, 3 })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 0, 1, 2 })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 0, 1, 3 })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 0, 2, 3 })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 1, 2, 3 })).Any());
            Assert.True(result.Where(x => x.SetEquals(new HashSet<int> { 0, 1, 2, 3 })).Any());
        }
    }

}