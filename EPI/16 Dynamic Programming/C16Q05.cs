
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace EPI.C16_Dynamic_Programming
{
    public class Q05
    {
        public int[,] Grid { get; }
        public int[] Pattern { get; }
        private HashSet<Object> cache;
        public Q05(int[,] grid, int[] pattern)
        {
            Grid = grid;
            Pattern = pattern;
            cache = new HashSet<object>();
        }

        public bool IsPatternInGrid()
        {
            for (int x = 0; x < Grid.GetLength(0); x++)
                for (int y = 0; y < Grid.GetLength(1); y++)
                    if (IsPatternInGrid(0, x, y))
                        return true;
            return false;
        }

        private bool IsPatternInGrid(int offset, int row, int col)
        {
            if (offset == Pattern.Length)
                return true;

            if (row < 0 || row >= Grid.GetLength(0) || col < 0 || col >= Grid.GetLength(1) || cache.Contains(new { offset = offset, row = row, col = col }))
                return false;

            if (Grid[row, col] == Pattern[offset])
            {
                for (int delta = -1; delta <= 1; delta += 2)
                {
                    if (IsPatternInGrid(offset + 1, row + delta, col))
                        return true;
                    if (IsPatternInGrid(offset + 1, row, col + delta))
                        return true;
                }
            }

            cache.Add(new { offset = offset, row = row, col = col });
            return false;
        }
    }

    [TestClass]
    public class C16Q05_Tests
    {
        [TestMethod]
        public void Example()
        {
            int[,] grid = new int[3, 3] {
                { 1, 2, 3 },
                { 3, 4, 5 },
                { 5, 6, 7 }
            };
            int[] pattern = { 1, 3, 4, 6 };
            Assert.IsTrue(new Q05(grid, pattern).IsPatternInGrid());
        }

        [TestMethod]
        public void Example_NotInGrid()
        {
            int[,] grid = new int[3, 3] {
                { 1, 2, 3 },
                { 3, 4, 5 },
                { 5, 6, 7 }
            };
            int[] pattern = { 1, 2, 3, 4 };
            Assert.IsFalse(new Q05(grid, pattern).IsPatternInGrid());
        }
    }
}
