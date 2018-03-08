using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace EPI.C05_Arrays
{
    public static class C05Q06
    {
        public static int? FindMaxProfit(int[] prices)
        {
            if (prices.Length < 2)
                return null;

            int maxProfit = prices[1] - prices[0];
            int minPrice = prices[0] < prices[1] ? prices[0] : prices[1];

            for (int i = 2; i < prices.Length; i++)
            {
                int profit = prices[i] - minPrice;
                if (profit > maxProfit)
                {
                    maxProfit = profit;
                }

                if (prices[i] < minPrice)
                {
                    minPrice = prices[i];
                }
            }

            return maxProfit;
        }
    }

    public class C05Q06_Tests
    {
        [Theory]
        [InlineData(new int[] { 99 }, null)]
        [InlineData(new int[] { 10, 5 }, -5)]
        [InlineData(new int[] { 10, 100, 0 }, 90)]
        [InlineData(new int[] { 10, 100, 200, 1000 }, 990)]
        [InlineData(new int[] { 10, 1000, 0, 200, 999 }, 999)]
        [InlineData(new int[] { 310, 315, 275, 295, 260, 270, 290, 230, 255, 250 }, 30)]
        public void Example(int[] prices, int? expectedMaxProfit)
        {
            Assert.Equal(expectedMaxProfit, C05Q06.FindMaxProfit(prices));
        }
    }
}
