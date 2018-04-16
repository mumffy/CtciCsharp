using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace EPI.C15_Recursion
{
    public class Q01
    {
        public static void HanoiMove(Stack<int>[] state, int count, int src, int dst, int tmp, Action<Stack<int>[]> output)
        {
            if (count < 1)
                return;

            if (count == 1)
            {
                var item = state[src].Pop();
                state[dst].Push(item);
                output(HanoiStateCopy(state));
                return;
            }

            HanoiMove(state, count - 1, src, tmp, dst, output);
            HanoiMove(state, 1, src, dst, tmp, output);
            HanoiMove(state, count - 1, tmp, dst, src, output);
        }

        internal static Stack<int>[] HanoiStateCopy(Stack<int>[] s)
        {
            Stack<int>[] t = new Stack<int>[s.Length];
            for (int i = 0; i < s.Length; i++)
                t[i] = new Stack<int>(s[i].Reverse());
            return t;
        }

        [Fact]
        public void StateCopyTest()
        {
            Stack<int>[] s = new Stack<int>[2];
            s[0] = new Stack<int>();
            s[1] = new Stack<int>();
            s[0].Push(5);
            s[0].Push(4);
            s[0].Push(3);
            s[1].Push(9);
            s[1].Push(8);
            s[1].Push(7);
            s[1].Push(6);
            Stack<int>[] t = HanoiStateCopy(s);
            Assert.Equal(3, t[0].Count);
            Assert.Equal(3, t[0].Pop());
            Assert.Equal(4, t[0].Pop());
            Assert.Equal(5, t[0].Pop());
            Assert.Empty(t[0]);
            Assert.Equal(3, s[0].Count);
            Assert.Equal(3, s[0].Peek());

            Assert.Equal(4, t[1].Count);
            Assert.Equal(6, t[1].Pop());
            Assert.Equal(7, t[1].Pop());
            Assert.Equal(8, t[1].Pop());
            Assert.Equal(9, t[1].Pop());
            Assert.Empty(t[1]);
            Assert.Equal(4, s[1].Count);
            Assert.Equal(6, s[1].Peek());
        }
    }

    public class C15Q01_Tests
    {
        private readonly ITestOutputHelper output;

        public C15Q01_Tests(ITestOutputHelper output)
        {
            this.output = output;
        }

        private void HanoiStatusPrint(Stack<int>[] hanoiState)
        {
            int maxCount = -1;
            foreach (Stack<int> s in hanoiState)
            {
                if (s.Count > maxCount)
                    maxCount = s.Count;
            }
            while (maxCount > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hanoiState.Length; i++)
                {
                    if (hanoiState[i].Count == maxCount)
                        sb.AppendFormat("{0}.", hanoiState[i].Pop());
                    else
                        sb.Append("..");
                }
                output.WriteLine(sb.ToString());
                maxCount--;
            }
            output.WriteLine("######");
            output.WriteLine("");
        }

        [Fact]
        public void StatusPrintTest()
        {
            Stack<int>[] s = new Stack<int>[3];
            s[0] = new Stack<int>();
            s[1] = new Stack<int>();
            s[2] = new Stack<int>();
            s[0].Push(5);
            s[0].Push(4);
            s[0].Push(3);
            s[1].Push(9);
            s[1].Push(8);
            s[1].Push(7);
            s[1].Push(6);
            s[2].Push(999);
            HanoiStatusPrint(s);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void TowersOfHanoiTest(int height)
        {
            Stack<int>[] state = new Stack<int>[] {
                new Stack<int>(),
                new Stack<int>(),
                new Stack<int>()
            };
            for (int i = height; i > 0; i--)
            {
                state[0].Push(i);
            }
            HanoiStatusPrint(Q01.HanoiStateCopy(state));
            Q01.HanoiMove(state, height, 0, 1, 2, HanoiStatusPrint);
        }
    }
}
