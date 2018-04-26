using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace EPI.C15_Recursion
{
    public class Q02NQueens
    {
        private List<int[]> solutions;
        private int n;
        private bool[][] free;
        private int[] instance;

        public Q02NQueens(int n)
        {
            this.n = n;
            solutions = new List<int[]>();
        }

        public List<int[]> QueenSolve()
        {
            free = MakeNewArrayOfArraysInitToTrue(n);
            instance = new int[n];

            for (int i = 0; i < n; i++)
            {
                instance[0] = i;

            }

            return solutions;
        }

        private bool[][] MakeNewArrayOfArraysInitToTrue(int count)
        {
            bool[][] aoa = new bool[count][];
            for (int i = 0; i < count; i++)
            {
                aoa[i] = new bool[count];
                for (int x = 0; x < count; x++)
                    aoa[i][x] = true;
            }
            return aoa;
        }
    }

    public class C15Q02_Tests
    {
        private readonly ITestOutputHelper output;

        public C15Q02_Tests(ITestOutputHelper output)
        {
            this.output = output;
        }

    }
}
