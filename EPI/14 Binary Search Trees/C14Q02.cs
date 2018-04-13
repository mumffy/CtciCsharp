using EPI.DataStructures;
using EPI.DataStructures.BinarySearchTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EPI.C14_Binary_Search_Trees
{
    class Q02
    {

        public static int? FindFirstGreaterKey(BinarySearchTree<int> tree, int target)
        {
            return FindFirstGreaterKey_Recursive(tree.Root, target);
        }

        private static int? FindFirstGreaterKey_Recursive(Node<int> node, int target)
        {
            if (node == null)
                return null;

            int? left = FindFirstGreaterKey_Recursive(node.Left, target);
            if (left != null)
                return left;

            if (node.Value > target)
                return node.Value;

            return FindFirstGreaterKey_Recursive(node.Right, target);
        }
    }

    public class C14Q02_Tests
    {
        BinarySearchTree<int> exampleTree;

        public C14Q02_Tests()
        {
            exampleTree = new BinarySearchTree<int>();

            exampleTree.Root = new Node<int>(19);
            exampleTree.Root.Left = new Node<int>(7);
            exampleTree.Root.Left.Left = new Node<int>(3);
            exampleTree.Root.Left.Left.Left = new Node<int>(2);
            exampleTree.Root.Left.Left.Right = new Node<int>(5);
            exampleTree.Root.Left.Right = new Node<int>(11);
            exampleTree.Root.Left.Right.Right = new Node<int>(17);
            exampleTree.Root.Left.Right.Right.Left = new Node<int>(13);
            exampleTree.Root.Right = new Node<int>(43);
            exampleTree.Root.Right.Left = new Node<int>(23);
            exampleTree.Root.Right.Left.Right = new Node<int>(37);
            exampleTree.Root.Right.Left.Right.Left = new Node<int>(29);
            exampleTree.Root.Right.Left.Right.Left.Right = new Node<int>(32);
            exampleTree.Root.Right.Left.Right.Right = new Node<int>(41);
            exampleTree.Root.Right.Right = new Node<int>(47);
            exampleTree.Root.Right.Right.Right = new Node<int>(53);
        }

        [Fact]
        public void VerifyBst()
        {
            Assert.True(Q01<int>.IsBst(exampleTree));
        }

        [Fact]
        public void Example()
        {
            Assert.Equal(29, Q02.FindFirstGreaterKey(exampleTree, 23));
        }
        [Fact]
        public void ShouldReturnSmallestNode()
        {
            Assert.Equal(2, Q02.FindFirstGreaterKey(exampleTree, 1));
        }
        [Fact]
        public void ShouldReturnGreatestNode()
        {
            Assert.Equal(53, Q02.FindFirstGreaterKey(exampleTree, 52));
        }
        [Fact]
        public void BstHasNoGreaterKey_ShouldReturnNull()
        {
            Assert.Null(Q02.FindFirstGreaterKey(exampleTree, 999));
        }

    }
}

