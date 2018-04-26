using Microsoft.VisualStudio.TestTools.UnitTesting;
using EPI.C16_Dynamic_Programming;
using System;

namespace EPI.C16_Dynamic_Programming
{
    public class Q06
    {
        public static int FindMaxValueWithinConstraint(Item[] items, int limit)
        {
            int[,] cache = new int[items.Length, limit + 1];

            for (int i = 0; i < items.Length; i++)
            {
                for (int capacity = 0; capacity < limit + 1; capacity++)
                {
                    if (i == 0)
                    {
                        if (capacity < items[i].Weight)
                            cache[i, capacity] = 0;
                        else
                            cache[i, capacity] = items[i].Value;
                    }
                    else
                    {
                        if (capacity < items[i].Weight)
                            cache[i, capacity] = cache[i - 1, capacity];
                        else
                        {
                            int withThisItem = cache[i - 1, capacity - items[i].Weight] + items[i].Value;
                            int withoutThisItem = cache[i - 1, capacity];
                            cache[i, capacity] = Math.Max(withThisItem, withoutThisItem);
                        }
                    }
                }
            }
            return cache[items.Length - 1, limit];
        }
    }

    public class Item
    {
        public int Value { get; }
        public int Weight { get; }
        public Item(int value, int weight)
        {
            Value = value;
            Weight = weight;
        }
    }

    [TestClass]
    public class C16Q06_Tests
    {
        [TestMethod]
        public void Example02()
        {
            Item[] items = new Item[] {
                new Item(60, 5),
                new Item(50, 3),
                new Item(70, 4),
                new Item(30, 2),
            };
            Assert.AreEqual(80, Q06.FindMaxValueWithinConstraint(items, 5));
        }

        [TestMethod]
        public void Example01()
        {
            Item[] items = new Item[] {
                new Item(65, 20),
                new Item(35, 8),
                new Item(245, 60),
                new Item(195, 55),
                new Item(65, 40),
                new Item(150, 70),
                new Item(275, 85),
                new Item(155, 25), //08
                new Item(120, 30),
                new Item(320, 65),
                new Item(75, 75),
                new Item(40, 10),
                new Item(200, 95),
                new Item(100, 50),
                new Item(220, 40),
                new Item(99, 10), //16
            };
            Assert.AreEqual(695, Q06.FindMaxValueWithinConstraint(items, 130));
        }
    }
}
