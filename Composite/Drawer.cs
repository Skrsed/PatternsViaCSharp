using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    class Drawer
    {
        public Drawer()
        {
            var test = new List<int[,]>();
            test.Add(new int[5, 5]{
                { 0,0,1,0,0},
                    {0,1,0,1,0},
                    { 1,0,0,0,1},
                    { 0,1,0,1,0},
                    { 1,1,1,1,1},
            });
            // Draw(test);
        }
        public void Draw(List<int[,]> matrices)
        {
            var screenString = "";
            var lengthX = matrices.Max(x => x.GetLength(0));
            var lengthY = matrices.Max(x => x.GetLength(1));
            char[,] resArray = new char[lengthX, lengthY];
            foreach (var matrix in matrices)
            {
                for (var i = 0; i < matrix.GetLength(0); i++)
                    for (var j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] == 0)
                            continue;
                        resArray[i, j] = Pens.Symbols[matrix[i, j]];
                    }
            }
            for (var i = 0; i < resArray.GetLength(0); i++)
            {
                for (var j = 0; j < resArray.GetLength(1); j++)
                {
                    screenString += resArray[i, j];
                }
                screenString += "\n";
            }
            Console.Write(screenString);
        }

    }
}
