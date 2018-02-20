using System;
using System.Collections.Generic;
using EPI.DataStructures;
using Xunit;
using Xunit.Abstractions;

namespace EPI.C09_Binary_Trees
{
    public static class Q01
    {

        public static bool IsHeightBalanced(BinaryTree<string> tree)
        {
            int balancedHeight = GetBalancedHeight(tree.Root, 0);

            if (balancedHeight == -1)
            {
                return false;
            }
            return true;
        }

        public static int GetBalancedHeight(Node<string> n, int height)
        {
            if (n.Left == null && n.Right == null)
            {
                return height;
            }

            //TODO [?] undefined when a node has only one child, i.e. height of the "non child" is undefined
            if (n.Left == null)
            {
                return GetBalancedHeight(n.Right, height + 1);
            }
            else if (n.Right == null)
            {
                return GetBalancedHeight(n.Left, height + 1);
            }

            int leftHeight = GetBalancedHeight(n.Left, height + 1);
            int rightHeight = GetBalancedHeight(n.Right, height + 1);

            if (leftHeight == -1 || rightHeight == -1)
            {
                return -1;
            }

            if (Math.Abs(leftHeight - rightHeight) > 1)
            {
                return -1;
            }

            return Math.Max(leftHeight, rightHeight);

        }

        public static int GetHeightRecursive(Node<string> n)
        {
            if (n.Left == null && n.Right == null)
            {
                return 0;
            }

            return 1 + Math.Max(GetHeightRecursive(n.Left), GetHeightRecursive(n.Right));
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
            BinaryTree<string> tree = GetSampleTree();
            output.WriteLine(tree.ToBfstrString());
            Assert.Equal(4, Q01.GetHeightRecursive(tree.Root));
            Assert.True(Q01.IsHeightBalanced(tree));
        }

        [Fact]
        public void Test_Unbalanced01()
        {
            var tree = GetUnbalancedTree01();
            Assert.False(Q01.IsHeightBalanced(tree));
        }

        private BinaryTree<string> GetSampleTree()
        {
            BinaryTree<string> tree = new BinaryTree<string>();
            tree.Root = new Node<string>("A",
                left: new Node<string>("B",
                    left: new Node<string>("C",
                        left: new Node<string>("D",
                            left: new Node<string>("E"),
                            right: new Node<string>("F")
                        ),
                        right: new Node<string>("G")
                    ),
                    right: new Node<string>("H",
                        left: new Node<string>("I"),
                        right: new Node<string>("J")
                    )
                ),
                right: new Node<string>("K",
                    left: new Node<string>("L",
                        left: new Node<string>("M"),
                        right: new Node<string>("N")
                    ),
                    right: new Node<string>("O")
                )
            );

            return tree;
        }

        private BinaryTree<string> GetUnbalancedTree01()
        {
            BinaryTree<string> tree = new BinaryTree<string>();
            tree.Root = new Node<string>("A",
                left: new Node<string>("B",
                    left: new Node<string>("C",
                        left: new Node<string>("D",
                            left: new Node<string>("E"),
                            right: new Node<string>("F")
                        ),
                        right: new Node<string>("G")
                    ),
                    right: new Node<string>("H",
                        left: new Node<string>("I"),
                        right: new Node<string>("J")
                    )
                ),
                right: new Node<string>("K",
                    left: new Node<string>("L"//,
                        //left: new Node<string>("M"),
                        //right: new Node<string>("N")
                    ),
                    right: new Node<string>("O")
                )
            );

            return tree;
        }
    }



}
