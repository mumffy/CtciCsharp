using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPI.DataStructures.PriorityQueue;
using Xunit;

namespace EPI.C10_Heaps
{
    public static class Q01
    {
        public static List<int> SortedMerge(int[][] files)
        {
            BinaryMaxHeap<LineItem> heap = new BinaryMaxHeap<LineItem>();
            List<int> result = new List<int>();

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Length > 0)
                {
                    int value = files[i][0];
                    heap.Push(value, new LineItem(value: value, fileNum: i, nextIndex: 1));
                }
            }

            while (heap.Count > 0)
            {
                LineItem item = heap.Pop();
                result.Add(item.Value);

                int fileNum = item.FileNum;
                int fileIndex = item.NextIndex;
                if (fileIndex < files[fileNum].Length)
                {
                    int value = files[fileNum][fileIndex];
                    heap.Push(value, new LineItem(value, fileNum, fileIndex + 1));
                }
            }

            return result;
        }

        public struct LineItem : IComparable
        {
            public int Value { get; }
            public int FileNum { get; }
            public int NextIndex { get; }

            public LineItem(int value, int fileNum, int nextIndex)
            {
                Value = value;
                FileNum = fileNum;
                NextIndex = nextIndex;
            }

            public int CompareTo(object obj)
            {
                LineItem otherItem = (LineItem)obj;
                return Value.CompareTo(otherItem.Value);
            }
        }
    }
    public class C10Q01_Tests
    {
        [Fact]
        public void Example()
        {
            int[][] input = new int[][] {
                new int[] { 7, 5, 3 },
                new int[] { 6, 0 },
                new int[] { 28, 6, 0 }
            };
            var result = Q01.SortedMerge(input);
            Assert.Equal(new int[] { 28, 7, 6, 6, 5, 3, 0, 0 }, result);
        }
    }
}
