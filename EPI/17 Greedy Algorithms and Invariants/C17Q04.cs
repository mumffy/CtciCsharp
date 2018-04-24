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
            HashSet<int> numbers = new HashSet<int>(array);
            int remaining;

            foreach (int x in array)
            {
                remaining = target - x;
                foreach(int y in array)
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
    }
}
