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
            if (tree.Root == a || tree.Root == b)
                return null;

            var aToRoot = BfsFind(tree, a);
            var bToRoot = BfsFind(tree, b);
            List<Node<T>> longer;
            List<Node<T>> shorter;
            int aIndex = 0;
            int bIndex = 0;
            int lengthDiff = Math.Abs(aToRoot.Count - bToRoot.Count);

            if (aToRoot.Count > bToRoot.Count)
                aIndex += lengthDiff;
            else
                bIndex += lengthDiff;

            while(aIndex < aToRoot.Count)
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
}



}
