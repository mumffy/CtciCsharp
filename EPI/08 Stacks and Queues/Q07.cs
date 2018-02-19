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

                    while (list.Count <= depth)
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

        public static List<List<T>> BfsTraverseWithoutWrapper<T>(BinaryTree<T> tree)
        {
            Queue<Node<T>> currentDepth = new Queue<Node<T>>();
            Queue<Node<T>> nextDepth = new Queue<Node<T>>();
            List<T> currentDepthValues;
            List<List<T>> list = new List<List<T>>();
            Node<T> node;

            nextDepth.Enqueue(tree.Root);
            while (nextDepth.Count > 0)
            {
                currentDepthValues = new List<T>();
                currentDepth = nextDepth;
                nextDepth = new Queue<Node<T>>();

                while (currentDepth.Count > 0)
                {
                    node = currentDepth.Dequeue();
                    if (node != null)
                    {
                        currentDepthValues.Add(node.Value);
                        nextDepth.Enqueue(node.Left);
                        nextDepth.Enqueue(node.Right);
                    }
                }

                list.Add(currentDepthValues);
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
        public void Example()
        {
            BinaryTree<int> tree = GetExampleTree();
            var l = Q07.BfsTraverse(tree);
            AssertExampleListofList(l);
        }

        [Fact]
        public void TestWithoutAdditonalDataStructure()
        {
            var l = Q07.BfsTraverseWithoutWrapper(GetExampleTree());
            AssertExampleListofList(l);
        }

        private BinaryTree<int> GetExampleTree()
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
            return tree;
        }

        private void AssertExampleListofList(List<List<int>> l)
        {
            Assert.Equal(new List<int> { 314 }, l[0]);
            Assert.Equal(new List<int> { 6, 6 }, l[1]);
            Assert.Equal(new List<int> { 271, 561, 2, 271 }, l[2]);
            Assert.Equal(new List<int> { 28, 0, 3, 1, 28 }, l[3]);
            Assert.Equal(new List<int> { 17, 401, 257 }, l[4]);
            Assert.Equal(new List<int> { 641 }, l[5]);
        }
    }
}
