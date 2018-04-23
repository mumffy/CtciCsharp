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
        public void Example()
        {
            Item[] items = new Item[] {
                new Item(60, 5),
                new Item(50, 3),
                new Item(70, 4),
                new Item(30, 2),
            };
            Assert.AreEqual(80, Q06.FindMaxValueWithinConstraint(items, 5));
        }
    }
}
