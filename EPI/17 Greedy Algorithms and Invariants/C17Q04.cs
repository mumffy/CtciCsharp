using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPI.C17_Greedy_Algorithms_and_Invariants
{
    class Q04
    {
        public static bool HasThreeSum(int[] array, int target)
        {
            return HasThreeSum_WithInvariant(array, target);
        }

        private static bool HasThreeSum_WithInvariant(int[] array, int target)
        {
            int remaining, low, high, sum;
            int[] a = new int[array.Length];
            Array.Copy(array, a, array.Length);
            Array.Sort(a);

            foreach (int x in a)
            {
                remaining = target - x;
                low = 0;
                high = a.Length - 1;
                while (low <= high)
                {
                    sum = x + a[low] + a[high];
                    if (sum == target)
                        return true;
                    else if (sum < target)
                        low++;
                    else // sum > target
                        high--;
                }
            }

            return false;
        }

        private static bool HasThreeSum_WithHashtable(int[] array, int target)
        {
            HashSet<int> numbers = new HashSet<int>(array);
            int remaining;

            foreach (int x in array)
            {
                remaining = target - x;
                foreach (int y in array)
                    if (numbers.Contains(remaining - y))
                        return true;
            }

            return false;
        }
    }

    [TestClass]
    public class C17Q04_Tests
    {
        int[] exampleArray;
        public C17Q04_Tests()
        {
            exampleArray = new int[] { 11, 2, 5, 7, 3 };
        }

        [TestMethod]
        public void ExampleHasSum()
        {
            Assert.IsTrue(Q04.HasThreeSum(exampleArray, 21));
        }

        [TestMethod]
        public void ExampleNoSum()
        {
            Assert.IsFalse(Q04.HasThreeSum(exampleArray, 22));
        }

        [TestMethod]
        public void TargetRequiresRepeats()
        {
            Assert.IsTrue(Q04.HasThreeSum(new int[] { 1, 7, 9 }, 21));
            Assert.IsTrue(Q04.HasThreeSum(new int[] { 1, 7, 9 }, 27));
            Assert.IsTrue(Q04.HasThreeSum(new int[] { 1, 7, 9 }, 3));
        }

        [TestMethod]
        public void TargetIsGreaterThanMaxOrLessThanMin()
        {
            Assert.IsFalse(Q04.HasThreeSum(new int[] { 1, 7, 9 }, 10));
            Assert.IsFalse(Q04.HasThreeSum(new int[] { 1, 7, 9 }, 0));
        }
    }
}
