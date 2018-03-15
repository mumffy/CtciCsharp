using System;
using System.Collections.Generic;
using EPI.DataStructures;
using Xunit;
using Xunit.Abstractions;


namespace EPI.C09_Binary_Trees
{
    public static class Q03<T>
    {
        public static Node<T> FindLCA(BinaryTree<T> tree, Node<T> a, Node<T> b)
        {
            //return FindLCAComplicated(tree, a, b);
            return FindLCASimpler(tree, a, b);
        }

        private static Node<T> FindLCASimpler(BinaryTree<T> tree, Node<T> a, Node<T> b)
        {
            if (tree.Root == a || tree.Root == b)
                return null;

            return FindLCA(tree.Root, a, b).lca;
        }

        private static Result FindLCA(Node<T> current, Node<T> a, Node<T> b)
        {
            Result result = new Result(0, null);
            if (current == null)
                return result;

            Result leftResult = FindLCA(current.Left, a, b);
            Result rightResult = FindLCA(current.Right, a, b);
            if (leftResult.lca != null)
                return leftResult;
            if (rightResult.lca != null)
                return rightResult;

            result.numFound = leftResult.numFound + rightResult.numFound;
            if (result.numFound == 2 && result.lca == null)
                result.lca = current;

            if (current == a || current == b)
                result.numFound++;

            return result;
        }

        private class Result
        {
            public Result(int numFound, Node<T> lca)
            {
                this.numFound = numFound;
                this.lca = lca;
            }
            public int numFound { get; set; }
            public Node<T> lca { get; set; }
        }

        private static Node<T> FindLCAComplicated(BinaryTree<T> tree, Node<T> a, Node<T> b)
        {
            if (tree.Root == a || tree.Root == b)
                return null;

            var aToRoot = BfsFind(tree, a);
            var bToRoot = BfsFind(tree, b);
            int aIndex = 0;
            int bIndex = 0;
            int lengthDiff = Math.Abs(aToRoot.Count - bToRoot.Count);

            if (aToRoot.Count > bToRoot.Count)
                aIndex += lengthDiff;
            else
                bIndex += lengthDiff;

            while (aIndex < aToRoot.Count)
            {
                if (aToRoot[aIndex] == bToRoot[bIndex])
                    return aToRoot[aIndex];
                aIndex++;
                bIndex++;
            }
            return null;
        }

        public static List<Node<T>> BfsFind(BinaryTree<T> tree, T target)
        {
            return Bfs(tree.Root, target);
        }

        public static List<Node<T>> BfsFind(BinaryTree<T> tree, Node<T> target)
        {
            return Bfs(tree.Root, target);
        }

        private static List<Node<T>> Bfs(Node<T> n, T target)
        {
            if (n == null)
                return null;

            List<Node<T>> pathToRoot;
            if (n.Value.Equals(target))
            {
                pathToRoot = new List<Node<T>>();
                pathToRoot.Add(n);
            }
            else
            {
                pathToRoot = Bfs(n.Left, target) ?? Bfs(n.Right, target) ?? null;
                if (pathToRoot != null)
                {
                    pathToRoot.Add(n);
                }
            }
            return pathToRoot;
        }

        private static List<Node<T>> Bfs(Node<T> n, Node<T> target)
        {
            if (n == null)
                return null;

            List<Node<T>> pathToRoot;
            if (n == target)
            {
                pathToRoot = new List<Node<T>>();
                pathToRoot.Add(n);
            }
            else
            {
                pathToRoot = Bfs(n.Left, target) ?? Bfs(n.Right, target) ?? null;
                if (pathToRoot != null)
                {
                    pathToRoot.Add(n);
                }
            }
            return pathToRoot;
        }
    }


    public class C09Q03_Tests
    {
        private BinaryTree<string> getExampleTree()
        {
            BinaryTree<string> tree = new BinaryTree<string>();
            tree.Root = new Node<string>("A",
                left: new Node<string>("B",
                    left: new Node<string>("D"),
                    right: new Node<string>("E")),
                right: new Node<string>("C",
                    left: new Node<string>("K",
                        left: new Node<string>("L",
                            right: new Node<string>("M")),
                        right: new Node<string>("N")),
                    right: new Node<string>("P"))
                );
            return tree;
        }

        [Fact]
        public void Example()
        {
            BinaryTree<string> tree = getExampleTree();
            Node<string> k = Q03<string>.BfsFind(tree, "K")?[0];
            Node<string> m = Q03<string>.BfsFind(tree, "M")?[0];
            Node<string> n = Q03<string>.BfsFind(tree, "N")?[0];
            Assert.NotNull(k);
            Assert.NotNull(m);
            Assert.NotNull(n);
            Assert.Equal(m, k.Left.Right);
            Assert.Equal(n, k.Right);

            Node<string> lcaNode = Q03<string>.FindLCA(tree, m, n);
            Assert.Equal(k, lcaNode);
            Assert.Equal("K", lcaNode.Value);
        }

        [Fact]
        public void NoLCA()
        {
            BinaryTree<string> tree = getExampleTree();
            Node<string> k = Q03<string>.BfsFind(tree, "K")?[0];
            Assert.NotNull(k);

            Node<string> lcaNode = Q03<string>.FindLCA(tree, k, tree.Root);
            Assert.Equal(null, lcaNode);
        }

        [Fact]
        public void Test01()
        {
            BinaryTree<string> tree = getExampleTree();
            Node<string> d = Q03<string>.BfsFind(tree, "D")?[0];
            Node<string> n = Q03<string>.BfsFind(tree, "N")?[0];
            Assert.NotNull(d);
            Assert.NotNull(n);

            Node<string> lcaNode = Q03<string>.FindLCA(tree, d, n);
            Assert.Equal(tree.Root, lcaNode);
        }

        [Fact]
        public void Test02()
        {
            BinaryTree<string> tree = getExampleTree();
            Node<string> c = Q03<string>.BfsFind(tree, "C")?[0];
            Node<string> l = Q03<string>.BfsFind(tree, "L")?[0];
            Node<string> p = Q03<string>.BfsFind(tree, "P")?[0];
            Assert.NotNull(c);
            Assert.NotNull(l);
            Assert.NotNull(p);
            Assert.Equal(l, c.Left.Left);
            Assert.Equal(p, c.Right);

            Node<string> lcaNode = Q03<string>.FindLCA(tree, l, p);
            Assert.Equal(c, lcaNode);
            Assert.Equal("C", lcaNode.Value);
        }


    }



}
