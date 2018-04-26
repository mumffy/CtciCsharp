using System;
using System.Collections.Generic;
using EPI.DataStructures;
using Xunit;
using Xunit.Abstractions;


namespace EPI.C09_Binary_Trees
{
    public class Node : Node<string>
    {
        public Node Parent { get; }
        public Node(string value, Node parent, Node<string> left = null, Node<string> right = null) : base(value, left, right)
        {
            this.Parent = parent;
        }
    }

    public static class Q04
    {
        public static Node FindLCA(BinaryTree<string> tree, Node a, Node b)
        {
            if (tree.Root == a || tree.Root == b)
                return null;

            int aDepth = -1;
            int bDepth = -1;
            Node myA = a;
            Node myB = b;
            Node lower, higher;
            int depthDiff;

            while(myA != null)
            {
                myA = myA.Parent;
                aDepth++;
            }
            while (myB != null)
            {
                myB = myB.Parent;
                bDepth++;
            }

            if (aDepth > bDepth)
            {
                lower = a;
                higher = b;
            }else
            {
                lower = b;
                higher = a;
            }
            depthDiff = Math.Abs(aDepth - bDepth);

            while(depthDiff > 0)
            {
                lower = lower.Parent;
                depthDiff--;
            }
            while(lower != higher)
            {
                lower = lower.Parent;
                higher = higher.Parent;
            }
            return lower;
        }

    }


    public class C09Q04_Tests
    {
        private BinaryTree<string> getExampleTree()
        {
            BinaryTree<string> tree = new BinaryTree<string>();
            Node a = new Node("A", parent: null);
            Node b = new Node("B", parent: a);
            Node c = new Node("C", parent: a);
            Node d = new Node("D", parent: b);
            Node e = new Node("E", parent: b);
            Node k = new Node("K", parent: c);
            Node p = new Node("P", parent: c);
            Node l = new Node("L", parent: k);
            Node n = new Node("N", parent: k);
            Node m = new Node("M", parent: l);
            a.Left = b;
            a.Right = c;
            b.Left = d;
            b.Right = e;
            c.Left = k;
            c.Right = p;
            k.Left = l;
            k.Right = n;
            l.Right = m;
            tree.Root = a;
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
            Assert.Equal(k, (m as Node).Parent.Parent);
            Assert.Equal(k, (n as Node).Parent);

            Node<string> lcaNode = Q04.FindLCA(tree, m as Node, n as Node);
            Assert.Equal(k, lcaNode);
            Assert.Equal("K", lcaNode.Value);
        }

        [Fact]
        public void NoLCA()
        {
            BinaryTree<string> tree = getExampleTree();
            Node<string> k = Q03<string>.BfsFind(tree, "K")?[0];
            Assert.NotNull(k);

            Node<string> lcaNode = Q04.FindLCA(tree, k as Node, tree.Root as Node);
            Assert.Null(lcaNode);
        }

        [Fact]
        public void Test01()
        {
            BinaryTree<string> tree = getExampleTree();
            Node<string> d = Q03<string>.BfsFind(tree, "D")?[0];
            Node<string> n = Q03<string>.BfsFind(tree, "N")?[0];
            Assert.NotNull(d);
            Assert.NotNull(n);

            Node<string> lcaNode = Q04.FindLCA(tree, d as Node, n as Node);
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

            Node<string> lcaNode = Q04.FindLCA(tree, l as Node, p as Node);
            Assert.Equal(c, lcaNode);
            Assert.Equal("C", lcaNode.Value);
        }


    }



}
