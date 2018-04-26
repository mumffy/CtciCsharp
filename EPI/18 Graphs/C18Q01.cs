using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPI.C18_Graphs;

namespace EPI.C18_Graphs
{
    internal class Q01
    {

        public static PathNode[] FindPathToExit(bool?[,] maze, Node start, Node exit)
        {
            Queue<PathNode> queue = new Queue<PathNode>();
            HashSet<Node> visited = new HashSet<Node>();
            queue.Enqueue(new PathNode { Node = start });

            while (queue.Count > 0)
            {
                PathNode pathNode = queue.Dequeue();

                if (pathNode.Node.X == exit.X && pathNode.Node.Y == exit.Y)
                    return GetActualPath(pathNode);

                visited.Add(pathNode.Node);

                Node[] nextNodes = {
                    new Node(pathNode.Node.X, pathNode.Node.Y+1),
                    new Node(pathNode.Node.X+1, pathNode.Node.Y),
                    new Node(pathNode.Node.X, pathNode.Node.Y-1),
                    new Node(pathNode.Node.X-1, pathNode.Node.Y),
                };
                foreach (Node nextNode in nextNodes)
                {
                    if (nextNode.X >= 0 && nextNode.X < maze.GetLength(0) && nextNode.Y >= 0 && nextNode.Y < maze.GetLength(1) &&
                        maze[nextNode.X, nextNode.Y] == null && !visited.Contains(nextNode))
                    {
                        queue.Enqueue(new PathNode { Node = nextNode, Previous = pathNode });
                    }
                }
            }

            return null;
        }

        private static PathNode[] GetActualPath(PathNode exit)
        {
            return new PathNode[] { exit };
        }

        public class PathNode
        {
            public Node Node { get; set; }
            public PathNode Previous { get; set; }
        }

        public struct Node
        {
            public int X { get; }
            public int Y { get; }
            public Node(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }

    [TestClass]
    public class C18Q01_TestHelper
    {
        private static string[] encodedMaze;
        private static bool?[,] exampleMaze;

        public static string[] EncodedMaze => encodedMaze;
        public static bool?[,] ExampleMaze => exampleMaze;

        static C18Q01_TestHelper()
        {
            encodedMaze = new string[10]
            {
                "X.....XX..",
                "..X.......",
                "X.X..XX.XX",
                "...XXX..X.",
                ".XX.......",
                ".XX..X.XX.",
                "....X.....",
                "X.X.X.X...",
                "X.XX...XXX",
                ".......XX."
            };

            exampleMaze = GetGridFromEncoded(encodedMaze);
        }

        public static bool?[,] GetGridFromEncoded(string[] encoded)
        {
            bool?[,] grid = new bool?[encoded.Length, encoded[0].Length];
            for (int x = 0; x < encoded.Length; x++)
            {
                for (int y = 0; y < encoded[x].Length; y++)
                {
                    if (encoded[x][y] == '.')
                        grid[x, y] = null;
                    else
                        grid[x, y] = false;
                }
            }
            return grid;
        }

        public static string MazeToString(bool?[,] maze, bool withBorder = true)
        {
            StringBuilder sb = new StringBuilder();

            if (withBorder)
            {
                for (int i = 0; i < maze.GetLength(1) + 2; i++)
                    sb.Append('█');
                sb.AppendLine();
            }

            for (int x = 0; x < maze.GetLength(0); x++)
            {
                if (withBorder)
                    sb.Append('█');

                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    if (maze[x, y] == null)
                        sb.Append(' ');
                    else if (maze[x, y] == false)
                        sb.Append('█');
                    else
                        sb.Append('·');
                }

                if (withBorder)
                    sb.Append('█');

                sb.AppendLine();
            }

            if (withBorder)
            {
                for (int i = 0; i < maze.GetLength(1) + 2; i++)
                    sb.Append('█');
                sb.AppendLine();
            }

            return sb.ToString();
        }

        [TestMethod]
        public void MazeIs10by10()
        {
            foreach (string s in encodedMaze)
            {
                Assert.AreEqual(10, s.Length);
            }
        }

        [TestMethod]
        public void Print_Test()
        {
            System.Console.WriteLine(MazeToString(exampleMaze));
        }

    }

    [TestClass]
    public class C18Q01_Tests
    {
        [TestMethod]
        public void Example()
        {
            var reversePath = Q01.FindPathToExit(C18Q01_TestHelper.ExampleMaze, new Q01.Node(9, 0), new Q01.Node(0, 9));

            bool?[,] mazeCopy = new bool?[C18Q01_TestHelper.ExampleMaze.GetLength(0), C18Q01_TestHelper.ExampleMaze.GetLength(1)];
            Array.Copy(C18Q01_TestHelper.ExampleMaze, mazeCopy, C18Q01_TestHelper.ExampleMaze.Length);

            Q01.PathNode pathNode = reversePath[0];

            while (pathNode != null)
            {
                mazeCopy[pathNode.Node.X, pathNode.Node.Y] = true;
                pathNode = pathNode.Previous;
            }

            System.Console.WriteLine(C18Q01_TestHelper.MazeToString(mazeCopy));
        }
    }
}
