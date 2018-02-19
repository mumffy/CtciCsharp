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
            return false;
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

    }

    
    
}
