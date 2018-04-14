using EPI.DataStructures.BinarySearchTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using EPI.DataStructures;

namespace EPI.C14_Binary_Search_Trees
{
    class Q03<T> where T : IComparable
    {
        public static IEnumerable<T> FindKLargest(BinarySearchTree<T> tree, int k)
        {
            // if k > tree.Count throw ...
            return FindKLargest(tree.Root, k, new List<T>());
        }

        private static IList<T> FindKLargest(Node<T> node, int k, IList<T> list)
        {
            if (node == null)
                return list;

            FindKLargest(node.Right, k, list);
            if (list.Count == k)
                return list;
            else
                list.Add(node.Value);

            return FindKLargest(node.Left, k, list);
        }
    }

    public class C14Q03_Tests
    {
        BinarySearchTree<int> exampleTree;

        public C14Q03_Tests()
        {
            exampleTree = new C14Q02_Tests().ExampleTree;
        }

        [Fact]
        public void Example()
        {
            Assert.Equal(new List<int> { 53, 47, 43 }, Q03<int>.FindKLargest(exampleTree, 3));
        }

        [Theory]
        [InlineData(4, new int[] { 53, 47, 43, 41 })]
        [InlineData(16, new int[] { 53, 47, 43, 41, 37, 31, 29, 23, 19, 17, 13, 11, 7, 5, 3, 2 })]
        public void Tests(int k, int[] expected)
        {
            Assert.Equal(expected, Q03<int>.FindKLargest(exampleTree, k));
        }
    }
}
