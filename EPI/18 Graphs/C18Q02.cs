using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace EPI.C18_Graphs
{
    internal class Q02
    {
        public static bool?[,] DarkenEnclosedREegions(bool?[,] grid)
        {
            bool?[,] gridCopy = new bool?[grid.GetLength(0), grid.GetLength(1)];
            Array.Copy(grid, gridCopy, grid.Length);

            HashSet<Item> leaveWhite = new HashSet<Item>();
            Queue<Item> queue = new Queue<Item>();

            int rowMax = grid.GetLength(0) - 1;
            int colMax = grid.GetLength(1) - 1;

            for (int i = 0; i <= rowMax; i++)
            {
                if (grid[i, 0] == null)
                    queue.Enqueue(new Item(i, 0));
                if (grid[i, colMax] == null)
                    queue.Enqueue(new Item(i, colMax));
            }
            for (int j = 0; j <= colMax; j++)
            {
                if (grid[0, j] == null)
                    queue.Enqueue(new Item(0, j));
                if (grid[rowMax, j] == null)
                    queue.Enqueue(new Item(rowMax, j));
            }

            while (queue.Count > 0)
            {
                Item item = queue.Dequeue();
                leaveWhite.Add(item);

                Item[] neighbors = new Item[4] {
                    new Item(item.X,item.Y+1),
                    new Item(item.X+1,item.Y),
                    new Item(item.X,item.Y-1),
                    new Item(item.X-1,item.Y),
                };
                foreach (Item neighbor in neighbors)
                    if (neighbor.X >= 0 && neighbor.X <= rowMax && neighbor.Y >= 0 && neighbor.Y <= colMax &&
                        grid[neighbor.X, neighbor.Y] == null)
                    {
                        queue.Enqueue(neighbor);
                    }
            }

            for (int x = 0; x <= rowMax; x++)
                for (int y = 0; y <= colMax; y++)
                    if (gridCopy[x, y] == null && !leaveWhite.Contains(new Item(x, y)))
                        gridCopy[x, y] = false;
            return gridCopy;
        }

        private struct Item
        {
            public int X { get; }
            public int Y { get; }
            public Item(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }

    public class C18Q02_Tests
    {
        private readonly ITestOutputHelper output;
        private readonly string[] encodedGrid;
        private readonly bool?[,] exampleGrid;
        public C18Q02_Tests(ITestOutputHelper output)
        {
            this.output = output;

            encodedGrid = new String[4]
            {
                "XXXX",
                ".X.X",
                "X..X",
                "XXXX",
            };
            exampleGrid = C18Q01_TestHelper.GetGridFromEncoded(encodedGrid);
        }

        [Fact]
        public void Example()
        {
            output.WriteLine(C18Q01_TestHelper.MazeToString(exampleGrid, withBorder: false));
            var darkenedGrid = Q02.DarkenEnclosedREegions(exampleGrid);
            output.WriteLine("");
            output.WriteLine(C18Q01_TestHelper.MazeToString(darkenedGrid, withBorder: false));
        }
    }
}
