using EPI.DataStructures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace EPI.C08_Stacks_and_Queues
{
    public static class Q07
    {
        public static string BfsPrint<T>(BinaryTree<T> tree)
        {
            Queue<Node<T>> queue = new Queue<Node<T>>();
            StringBuilder sb = new StringBuilder();

            queue.Enqueue(tree.Root);

            while (queue.Count > 0)
            {
                Node<T> node = queue.Dequeue();
                if (node != null)
                {
                    sb.AppendFormat("{0} ", node.Value);
                    queue.Enqueue(node.Left);
                    queue.Enqueue(node.Right);
                }
            }

            return sb.ToString();
        }

        public static List<List<T>> BfsTraverse<T>(BinaryTree<T> tree)
        {
            Queue<Data<T>> queue = new Queue<Data<T>>();
            List<List<T>> list = new List<List<T>>();

            queue.Enqueue(new Data<T>(tree.Root, 0));

            while (queue.Count > 0)
            {
                Data<T> data = queue.Dequeue();
                if (data.Node != null)
                {
                    Node<T> node = data.Node;
                    int depth = data.Depth;

                    while(list.Count <= depth)
                    {
                        list.Add(new List<T>());
                    }

                    list[depth].Add(node.Value);
                    queue.Enqueue(new Data<T>(node.Left, depth + 1));
                    queue.Enqueue(new Data<T>(node.Right, depth + 1));
                }
            }

            return list;
        }

        private class Data<T>
        {
            public Node<T> Node { get; set; }
            public int Depth { get; set; }

            public Data(Node<T> n, int depth)
            {
                Node = n;
                Depth = depth;
            }
        }
    }

    public class Tests
    {
        private readonly ITestOutputHelper output;

        public Tests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Sample()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Root = new Node<int>(314);
            tree.Root.Left = new Node<int>(6,
                left: new Node<int>(271,
                    left: new Node<int>(28),
                    right: new Node<int>(0)
                ),
                right: new Node<int>(561,
                    left: null,
                    right: new Node<int>(3,
                        left: new Node<int>(17)
                    )
                )
            );
            tree.Root.Right = new Node<int>(6,
                left: new Node<int>(2,
                    left: null,
                    right: new Node<int>(1,
                        left: new Node<int>(401,
                            left: null,
                            right: new Node<int>(641)
                        ),
                        right: new Node<int>(257)
                    )
                ),
                right: new Node<int>(271,
                    left: null,
                    right: new Node<int>(28)
                )
            );

            var l = Q07.BfsTraverse(tree);
            output.WriteLine(Q07.BfsPrint(tree));
        }
    }
}
