using EPI.DataStructures;
using EPI.DataStructures.BinarySearchTree;
using System;
using Xunit;

namespace EPI.C14_Binary_Search_Trees
{
    public static class Q01<T> where T : IComparable
    {
        public static bool IsBst(BinarySearchTree<T> tree)
        {
            if (tree.Root == null)
                return false;
            return IsBst(tree.Root);
        }

        private static bool IsBst(Node<T> node)
        {
            if (node == null)
                return true;

            if (node.Left != null && node.Value.CompareTo(node.Left.Value) < 0)
                return false;

            if (node.Right != null && node.Value.CompareTo(node.Right.Value) > 0)
                return false;

            return IsBst(node.Left) && IsBst(node.Right);
        }
    }

    public class C14Q01_Tests
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();

        [Fact]
        public void RootOnly()
        {
            tree.Root = new Node<int>(5);
            Assert.True(Q01<int>.IsBst(tree));
        }

        [Fact]
        public void NoRoot()
        {
            Assert.False(Q01<int>.IsBst(tree));
        }

        [Fact]
        public void SimpleBst()
        {
            tree.Root = new Node<int>(10);
            tree.Root.Left = new Node<int>(5);
            tree.Root.Right = new Node<int>(99);
            Assert.True(Q01<int>.IsBst(tree));
        }

        [Fact]
        public void UnbalancedBstLeftHeavy()
        {
            tree.Root = new Node<int>(10);
            tree.Root.Left = new Node<int>(5);
            tree.Root.Left.Left = new Node<int>(1);
            Assert.True(Q01<int>.IsBst(tree));
        }

        [Fact]
        public void UnbalancedBstRightHeavy()
        {
            tree.Root = new Node<int>(10);
            tree.Root.Right = new Node<int>(22);
            tree.Root.Right.Right = new Node<int>(33);
            Assert.True(Q01<int>.IsBst(tree));
        }

        [Fact]
        public void IsBst01()
        {
            tree.Root = new Node<int>(10);
            tree.Root.Left = new Node<int>(5);
            tree.Root.Left.Left = new Node<int>(1);
            tree.Root.Left.Right = new Node<int>(7);
            tree.Root.Right = new Node<int>(99);
            tree.Root.Right.Left = new Node<int>(55);
            tree.Root.Right.Right = new Node<int>(100);
            Assert.True(Q01<int>.IsBst(tree));
        }
        [Fact]
        public void NotBst01()
        {
            tree.Root = new Node<int>(10);
            tree.Root.Left = new Node<int>(5);
            tree.Root.Left.Left = new Node<int>(1);
            tree.Root.Left.Right = new Node<int>(4); // violates BST property
            tree.Root.Right = new Node<int>(99);
            tree.Root.Right.Left = new Node<int>(55);
            tree.Root.Right.Right = new Node<int>(100);
            Assert.False(Q01<int>.IsBst(tree));
        }

        [Fact]
        public void NotBst02()
        {
            tree.Root = new Node<int>(10);
            tree.Root.Left = new Node<int>(5);
            tree.Root.Left.Left = new Node<int>(1);
            tree.Root.Left.Right = new Node<int>(7);
            tree.Root.Right = new Node<int>(99);
            tree.Root.Right.Left = new Node<int>(150); //violates BST property
            tree.Root.Right.Right = new Node<int>(200);
            Assert.False(Q01<int>.IsBst(tree));
        }

        [Fact]
        public void IsBst02_EqualNodes()
        {
            tree.Root = new Node<int>(10);
            tree.Root.Left = new Node<int>(10);
            tree.Root.Right = new Node<int>(10);
            Assert.True(Q01<int>.IsBst(tree));
        }
        [Fact]
        public void IsBst03_MoreEqualNodes()
        {
            tree.Root = new Node<int>(10);
            tree.Root.Left = new Node<int>(5);
            tree.Root.Left.Left = new Node<int>(1);
            tree.Root.Left.Right = new Node<int>(10);
            tree.Root.Right = new Node<int>(99);
            tree.Root.Right.Left = new Node<int>(99);
            tree.Root.Right.Right = new Node<int>(200);
            tree.Root.Right.Right.Left = new Node<int>(155);
            tree.Root.Right.Right.Right = new Node<int>(200);
            Assert.True(Q01<int>.IsBst(tree));
        }
    }
}
