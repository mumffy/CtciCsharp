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
    class Q04<T> where T : IComparable
    {
        // Assume all keys are distinct
        /// <summary>
        /// Solution seems to have just two forms:
        /// a.  The two target nodes are in different subtrees.
        ///     Perform "simulataneous" search for both targets until you reach a node where the search would
        ///     need to descend both left and right - this node is the LCA.
        /// b.  One target node is a descendant of the other.
        ///     While performing the search in a., one of the target nodes will actually be found.
        ///     This found node is the LCA.
        /// </summary>
        public static Node<T> FindLowestCommonAncestor(BinarySearchTree<T> tree, Node<T> a, Node<T> b)
        {
            Node<T> smaller;
            Node<T> bigger;
            if (a.Value.CompareTo(b.Value) < 0)
            {
                smaller = a;
                bigger = b;
            }
            else
            {
                smaller = b;
                bigger = a;
            }
            return FindLowestCommonAncestor(tree.Root, smaller, bigger);
        }

        private static Node<T> FindLowestCommonAncestor(Node<T> node, Node<T> a, Node<T> b)
        {
            if (node == null)
                return null;

            if (node.Value.CompareTo(a.Value) == 0 || node.Value.CompareTo(b.Value) == 0 ||
               (node.Value.CompareTo(a.Value) > 0 && node.Value.CompareTo(b.Value) < 0))
                return node;

            if (node.Value.CompareTo(b.Value) > 0)
                return FindLowestCommonAncestor(node.Left, a, b);
            return FindLowestCommonAncestor(node.Right, a, b); // node.Value < a.Value
        }
    }

    public class C14Q04_Tests
    {
        BinarySearchTree<int> exampleTree;

        public C14Q04_Tests()
        {
            exampleTree = new C14Q02_Tests().ExampleTree;
        }

        [Fact]
        public void Example()
        {
            var nodeC = exampleTree.Find(3);
            var nodeG = exampleTree.Find(17);
            var lca = exampleTree.Find(7);
            Assert.Equal(3, nodeC.Value);
            Assert.Equal(17, nodeG.Value);
            Assert.Equal(7, lca.Value);

            Assert.Equal(lca, Q04<int>.FindLowestCommonAncestor(exampleTree, nodeC, nodeG));
        }

        [Fact]
        public void RootIsLca()
        {
            var x = exampleTree.Find(13);
            var y = exampleTree.Find(41);
            var lca = exampleTree.Find(19);
            Assert.Equal(lca, Q04<int>.FindLowestCommonAncestor(exampleTree, x, y));
        }

        [Fact]
        public void BiggerNodeIsLca()
        {
            var x = exampleTree.Find(31);
            var y = exampleTree.Find(43);
            var lca = exampleTree.Find(43);
            Assert.Equal(lca, Q04<int>.FindLowestCommonAncestor(exampleTree, x, y));
        }

        [Fact]
        public void SmallerNodeIsLca()
        {
            var x = exampleTree.Find(7);
            var y = exampleTree.Find(13);
            var lca = exampleTree.Find(7);
            Assert.Equal(lca, Q04<int>.FindLowestCommonAncestor(exampleTree, x, y));
        }

    }

}
