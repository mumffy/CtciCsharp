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
            //return FindFirstGreaterKey_RecursiveSearch(tree.Root, target)?.FirstGreaterKey;
            return EpiSolution(tree.Root, target, null);
        }


        private static int? EpiSolution(Node<int> node, int target, int? bestSoFar)
        {
            if (node == null)
                return bestSoFar;

            if (node.Value > target && (bestSoFar == null || node.Value < bestSoFar))
            {
                bestSoFar = node.Value;
                return EpiSolution(node.Left, target, bestSoFar);
            }
            else
            {
                return EpiSolution(node.Right, target, bestSoFar);
            }
        }

        /// <summary>
        /// Two (three) possibilities:
        /// a.  Target is *not* in the BST: traverse to the leaf where target *would* be inserted.
        ///     The solution is the nearest ancestor whose left subtree contains this leaf position.
        ///     Could even be the direct parent, if this "new" leaf is its left child.
        /// b.  Target *is* in the BST and is less than the greatest key.
        ///     Actually, same case as a., when you encounter the node(s) with equal values, traverse right.
        /// (c) Target *is* in the BST and *is* the greatest key.
        ///     No solution, since target already has the greatest key value.
        ///         
        /// This approach is better than the "full" in-order traversal done by FindFirstGreaterKey_Recursive(...)
        /// but it's not "fully" optimal because while it takes advantage of the fact that the solution will "seen"
        /// when backtracking the path to the leaf back up to the root, it will continue to make more comparisons than
        /// absolutely necessary.
        /// 
        /// A further optimization would be to "find" the nearest ancestor whose left subtree contained this new leaf,
        /// as described in the two (three) possibilities above.
        /// </summary>
        private static Solution FindFirstGreaterKey_RecursiveSearch(Node<int> node, int target)
        {
            if (node == null) // this is where the target would be inserted
                return new Solution();

            Solution s = null;
            if (node.Value <= target)
                s = FindFirstGreaterKey_RecursiveSearch(node.Right, target);

            if (node.Value > target)
                s = FindFirstGreaterKey_RecursiveSearch(node.Left, target);

            if (node.Value > target && (s.FirstGreaterKey == null || s.FirstGreaterKey > node.Value))
                s.FirstGreaterKey = node.Value;

            return s;
        }

        private class Solution
        {
            public int? FirstGreaterKey;
        }

        // this uses in-order traversal... should be able to further optimize by skipping the nodes we know will *not* be the answer...
        //    i.e. take advantage of the BST property and actually "search" for the first greater key...
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

