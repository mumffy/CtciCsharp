using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace EPI.C18_Graphs
{
    internal class Q02
    {
        public bool[,] DarkenEnclosedREegions(bool[,] grid)
        {


            return null;
        }
    }

    public class C18Q02_Tests
    {
        private readonly ITestOutputHelper output;
        private readonly string[] encodedGrid;
        private readonly bool?[,] exampleGrid;
        public C18Q02_Tests(ITestOutputHelper output)
        {
            this.output = output;

            encodedGrid = new String[4]
            {
                "XXXX",
                ".X.X",
                "X..X",
                "XXXX",
            };
            exampleGrid = C18Q01_TestHelper.GetGridFromEncoded(encodedGrid);
        }

        //exampleGrid = 

        [Fact]
        public void Example()
        {
            output.WriteLine(C18Q01_TestHelper.MazeToString(exampleGrid, withBorder:false));
        }
    }
}
