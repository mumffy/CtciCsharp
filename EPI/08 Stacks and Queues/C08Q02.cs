using System;
using System.Collections.Generic;
using Xunit;

namespace EPI.C08_Stacks_and_Queues
{
    public static class C08Q02
    {
        public static int EvaluateRPN(string input)
        {

            return 0;
        }

        private static Stack<RpnToken> TokensToStack(string input)
        {
            Stack<RpnToken> stack = new Stack<RpnToken>();
            string[] tokens = input.Split();

            for (int i = tokens.Length - 1; i > 0; i--)
            {
                string token = tokens[i];
                int num;
                if (Int32.TryParse(token, out num))
                {
                    stack.Push(new Number(num));
                }
                else 
                {
                    OpType op;
                    switch (token)
                    {
                        case "+":
                            op = OpType.Add;
                            break;
                        case "-":
                            op = OpType.Subtract;
                            break;
                        case "*":
                            op = OpType.Multiply;
                            break;
                        case "/":
                            op = OpType.Divide;
                            break;
                        default:
                            throw new Exception($"{token} is not a known operation");
                    }
                    stack.Push(new Operator(op));
                } 
            }

            return stack;
        }

        public abstract class RpnToken
        {
        }

        private class Number : RpnToken
        {
            public Number(int i)
            {
                value = i;
            }
            public int value { get; set; }
        }

        public class Operator : RpnToken
        {
            public Operator(OpType o)
            {
                value = o;
            }
            public OpType value { get; set; }
        }

        public enum OpType { Add, Subtract, Multiply, Divide };

        [Fact]
        public static void TokensToStack_Test()
        {

        }

    }

    public class C08Q02_Tests
    {
        [Theory]
        [InlineData("1729", 1729)]
        [InlineData("3,4,+,2,*,1,+", 15)]
        [InlineData("1,1+,-2,*", -4)]
        [InlineData("-641,6,/,28,/", -3)]
        public void Examples(string input, int expectedOutput)
        {
            Assert.Equal(expectedOutput, C08Q02.EvaluateRPN(input));
        }
    }
}

