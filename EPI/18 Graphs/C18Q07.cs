using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPI.C18_Graphs;
using Xunit;

namespace EPI.C18_Graphs
{
    internal class Q07
    {

        public static int FindProductionSequenceLengthShortest(HashSet<String> dictionary, String s, String t)
        {
            var sequence = FindProductionSequenceShortest(dictionary, s, t);
            if (sequence == null)
                return -1;
            return sequence.Length - 1;
        }

        public static String[] FindProductionSequenceShortest(HashSet<String> dictionary, String s, String t)
        {
            if (s == t || s.Length != t.Length)
                return null;

            Dictionary<String, List<String>> adjList = BuildGraphFromWordSet(dictionary);
            Queue<Item> queue = new Queue<Item>();
            HashSet<String> visited = new HashSet<string>();
            queue.Enqueue(new Item(s, 0, null));

            while (queue.Count > 0)
            {
                Item item = queue.Dequeue();
                String word = item.Word;
                if (word == t)
                {
                    String[] result = new String[item.Distance + 1];
                    int i = item.Distance;
                    while (item != null)
                    {
                        result[i] = item.Word;
                        i--;
                        item = item.Previous;
                    }
                    return result;
                }

                visited.Add(word);
                foreach (String neighborWord in adjList[word])
                {
                    if (!visited.Contains(neighborWord))
                        queue.Enqueue(new Item(neighborWord, item.Distance + 1, item));
                }
            }
            return null;
        }

        private class Item
        {
            public String Word { get; }
            public int Distance { get; }
            public Item Previous { get; }

            public Item(string word, int distance, Item previous = null)
            {
                Word = word;
                Distance = distance;
                Previous = previous;
            }
        }

        public static Dictionary<String, List<String>> BuildGraphFromWordSet(HashSet<string> set)
        {
            Dictionary<String, List<String>> adjDict = new Dictionary<string, List<string>>();
            string[] words = set.ToArray();

            foreach (string word in words)
                adjDict[word] = new List<string>();

            for (int x = 0; x < words.Length; x++)
            {
                for (int y = x + 1; y < words.Length; y++)
                {
                    if (DiffByOneChar(words[x], words[y]))
                    {
                        adjDict[words[x]].Add(words[y]);
                        adjDict[words[y]].Add(words[x]);
                    }
                }
            }
            return adjDict;
        }

        public static bool DiffByOneChar(string a, string b)
        {
            bool seenDiff = false;

            if (a.Length != b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == b[i])
                    continue;

                if (a[i] != b[i] && !seenDiff)
                    seenDiff = true;
                else
                    return false;
            }
            return true;
        }
    }

    public class C18Q07_Tests
    {
        private HashSet<String> exampleWords;

        public C18Q07_Tests()
        {
            exampleWords = new HashSet<string> { "bat", "cot", "dog", "dag", "dot", "cat" };
        }

        [Fact]
        public void Helper_DiffByOneChar()
        {
            Assert.True(Q07.DiffByOneChar("abc", "axc"));
            Assert.True(Q07.DiffByOneChar("abc", "xbc"));
            Assert.True(Q07.DiffByOneChar("abc", "abx"));
        }

        [Fact]
        public void Helper_DiffByOneChar_Fail()
        {
            Assert.False(Q07.DiffByOneChar("abc", "abcd"));
            Assert.False(Q07.DiffByOneChar("abc", "axx"));
            Assert.False(Q07.DiffByOneChar("abc", "xxc"));
        }
        [Fact]
        public void Helper_BuildGraph()
        {
            var adjList = Q07.BuildGraphFromWordSet(exampleWords);
        }

        [Fact]
        public void Example()
        {
            Assert.Equal(3, Q07.FindProductionSequenceLengthShortest(exampleWords, "cat", "dog"));
            Assert.Equal(new String[] { "cat", "cot", "dot", "dog" }, Q07.FindProductionSequenceShortest(exampleWords, "cat", "dog"));
        }
    }
}
