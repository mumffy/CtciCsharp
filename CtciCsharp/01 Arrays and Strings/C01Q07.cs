using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CtciCsharp._01_Arrays_and_Strings
{
    static class C01Q07
    {

        /// <summary>
        /// ->  input is NxN matrix
        /// ->  each pixel is 4bytes
        ///  ?  what's the significance of each pixel being 4bytes?
        ///      32bits, just represent with an Int32?
        ///
        /// BF  Walk over each element in matrix, and copy to new matrix, 
        ///     and return the new matrix => O(n), where n is number of elements in matrix
        /// 
        /// BCR O(n)?? each element has to be "touched"?
        /// 
        /// Elasped Time: 55min + 20min fixes -->scrapped
        ///               10min retry (not in place)
        /// </summary>
        public static int[,] RotateMatrix(int[,] matrix, int n)
        {
            int[,] result = new int[n, n]; 

            for (int row=0; row<n; row++)
            {
                for(int col=0; col<n; col++)
                {
                    result[col, n-1-row] = matrix[row, col];
                }
            }
            return result;
        }

        public static int[,] RotateMatrix_FirstTry(int[,] matrix, int n)
        {
            int[,] result = new int[n, n];

            int middle = n / 2;
            result[middle, middle] = matrix[middle, middle];

            for (int i=0; i<n/2; i++)
            {
                RotateSquare(matrix, result, i, n-i);
            }
            return result;
        }

        private static void RotateSquare(int[,] input, int[,] output, int START, int N)
        {
            if (N == 1)
            {
                output[START, START] = input[START, START];
                return;
            }

            int END = N - 1;
            for(int i=START; i<N; i++)
            {
                output[i, END] = input[START, i];
                output[END, END-i] = input[i, END];
                output[END-i, START] = input[END, END-i];
                output[START, i] = input[END-i, START];
            }
        }
 

    }

    public class C01Q07_RotateMatrix_Tests
    {
        [Fact]
        public void Test1x1()
        {
            int[,] input = new int[1, 1] { { 187 } };
            var result = C01Q07.RotateMatrix(input, 1);
            int[,] expected = new int[1, 1] { { 187 } };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test2x2()
        {
            int[,] input = new int[2, 2] { { 1, 2 }, 
                                           { 3, 4 } };
            var result = C01Q07.RotateMatrix(input, 2);
            int[,] expected = new int[2, 2] { { 3, 1 }, 
                                              { 4, 2 } };

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test3x3()
        {
            int[,] input = new int[3, 3] { { 1, 2, 3 },
                                           { 4, 5, 6 },
                                           { 7, 8, 9 } };
            var result = C01Q07.RotateMatrix(input, 3);
            int[,] expected = new int[3, 3] { { 7, 4, 1 },
                                              { 8, 5, 2 },
                                              { 9, 6, 3 } };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test4x4()
        {
            int[,] input = new int[4, 4] { { 01, 02, 03, 04 },
                                           { 05, 06, 07, 08 },
                                           { 09, 10, 11, 12 },
                                           { 13, 14, 15, 16 } };
            var result = C01Q07.RotateMatrix(input, 4);
            int[,] expected = new int[4, 4] { { 13, 09, 05, 01 },
                                              { 14, 10, 06, 02 },
                                              { 15, 11, 07, 03 },
                                              { 16, 12, 08, 04 } };
            Assert.Equal(expected, result);
        }
    }
}
