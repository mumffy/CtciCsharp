using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EPI.C16_Dynamic_Programming
{
    class Q03
    {
        public static int CountDownRightPaths(int rows, int cols)
        {
            int[,] cache = new int[rows, cols];

            for (int i = 0; i < rows - 1; i++)
                cache[i, cols - 1] = 1;
            for (int i = 0; i < cols - 1; i++)
                cache[rows - 1, i] = 1;

            for (int y = cols - 2; y >= 0; y--)
                for (int x = rows - 2; x >= 0; x--)
                    cache[x, y] = cache[x + 1, y] + cache[x, y + 1];

            return cache[0, 0];
        }
    }

    public class C16Q03_Tests
    {
        [Fact]
        public void Example()
        {
            Assert.Equal(70, Q03.CountDownRightPaths(5, 5));
        }

        [Theory]
        [InlineData(3, 3, 6)]
        [InlineData(4, 3, 10)]
        [InlineData(3, 4, 10)]
        [InlineData(5, 4, 35)]
        [InlineData(4, 5, 35)]
        public void Tests(int rows, int cols, int numPaths)
        {
            Assert.Equal(numPaths, Q03.CountDownRightPaths(rows, cols));
        }
    }
}