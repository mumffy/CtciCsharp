using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtciCsharp._02_Linked_Lists
{
    public class C02Q02
    {
        public static void Main()
        {
            List<int> l = new List<int> { 1, 2, 3, 4 };
            l.Sort((x, y) =>
                {
                    if (x > y) return -1;
                    if (x < y) return 1;
                    return 0;
                });

            foreach (int i in l)
            {
                System.Console.WriteLine(i);
            }
        }
    }
}
