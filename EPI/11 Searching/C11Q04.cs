using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EPI.C11_Search
{
    static class Q04
    {
        public static int GetIntSqrt(int target)
        {
            if (target < 0)
                throw new InvalidOperationException();

            int closestSqrt = -1;
            int traverse = target / 2;
            int ceiling = target;
            int product;

            while (traverse < ceiling)
            {
                if (traverse == closestSqrt)
                    return closestSqrt;

                product = traverse * traverse;
                if (product == target)
                    return traverse;

                if (product > target)
                {
                    ceiling = traverse;
                    traverse /= 2;
                }
                else
                {
                    closestSqrt = traverse;
                    traverse = (traverse + ceiling) / 2;
                }
            }

            return closestSqrt;
        }

        public static int BookAnswer(int target)
        {
            if (target < 0)
                throw new InvalidOperationException();

            int low = 0;
            int high = target;
            int mid = (high + low) / 2;
            int squared;

            while (low <= high)
            {
                mid = (high + low) / 2;
                squared = mid * mid;

                if (squared == target)
                    return mid;

                if (squared > target)
                    high = mid - 1;
                else
                    low = mid + 1;
            }

            return mid - 1;
        }

        public static int SmartAssAns(int number)
        {
            return (int)Math.Floor(Math.Pow(number, 0.5)) ;
        }
    }

    public class C11Q04_Tests
    {
        [Fact]
        public void ExampleWithBookAnswer()
        {
            Assert.Equal(4, Q04.BookAnswer(16));
            Assert.Equal(17, Q04.BookAnswer(300));
        }

        [Fact]
        public void Example()
        {
            Assert.Equal(4, Q04.GetIntSqrt(16));
            Assert.Equal(17, Q04.GetIntSqrt(300));
            Assert.Equal(4, Q04.SmartAssAns(16));
            Assert.Equal(17, Q04.SmartAssAns(300));
        }

        [Fact]
        public void EdgeCases()
        {
            Assert.Equal(0, Q04.BookAnswer(0));
            Assert.Equal(1, Q04.BookAnswer(1));
            //Assert.Equal(0, Q04.GetIntSqrt(0));
            //Assert.Equal(1, Q04.GetIntSqrt(1));
            Assert.Equal(0, Q04.SmartAssAns(0));
            Assert.Equal(1, Q04.SmartAssAns(1));
        }
    }
}
