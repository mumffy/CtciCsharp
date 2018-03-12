using System;
using System.Collections.Generic;
using Xunit;

namespace EPI.C08_Stacks_and_Queues
{
    public static class C08Q02
    {
        public static int EvaluateRPN(string input)
        {
            Stack<RpnToken> stack = TokensToStack(input);
            int result = 0;

            while (stack.Count > 0)
            {
                int lhs = (stack.Pop() as Number).Value;
                if (stack.Count > 0)
                {
                    int rhs = (stack.Pop() as Number).Value;
                    OpType op = (stack.Pop() as Operator).Value;
                    switch (op)
                    {
                        case OpType.Add:
                            result = lhs + rhs;
                            break;
                        case OpType.Subtract:
                            result = lhs - rhs;
                            break;
                        case OpType.Multiply:
                            result = lhs * rhs;
                            break;
                        case OpType.Divide:
                            result = lhs / rhs;
                            break;
                    }
                    stack.Push(new Number(result));
                }
                else
                {
                    result = lhs;
                }
            }

            return result;
        }

        private static Stack<RpnToken> TokensToStack(string input)
        {
            Stack<RpnToken> stack = new Stack<RpnToken>();
            string[] tokens = input.Split(',');

            for (int i = tokens.Length - 1; i >= 0; i--)
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
                Value = i;
            }
            public int Value { get; }
        }

        public class Operator : RpnToken
        {
            public Operator(OpType o)
            {
                Value = o;
            }
            public OpType Value { get; }
        }

        public enum OpType { Add, Subtract, Multiply, Divide };

        [Fact]
        public static void TokensToStack_Test()
        {

            string input = "10,5,+";
            var stack = TokensToStack(input);

            var numItem = (Number)stack.Pop();
            Assert.Equal(10, numItem.Value);
            numItem = (Number)stack.Pop();
            Assert.Equal(5, numItem.Value);
            var opItem = (Operator)stack.Pop();
            Assert.Equal(OpType.Add, opItem.Value);

        }

    }

    public class C08Q02_Tests
    {
        [Theory]
        [InlineData("1729", 1729)]
        [InlineData("3,4,+,2,*,1,+", 15)]
        [InlineData("1,1,+,-2,*", -4)]
        [InlineData("-641,6,/,28,/", -3)]
        public void Examples(string input, int expectedOutput)
        {
            Assert.Equal(expectedOutput, C08Q02.EvaluateRPN(input));
        }
    }
}

