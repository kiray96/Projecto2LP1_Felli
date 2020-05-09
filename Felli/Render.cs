using System;
using System.Collections.Generic;
using System.Text;

namespace Felli
{
    class Render
    {
        /// <summary>
        /// Draw the grid on the console
        /// </summary>
        /// <param name="grid"></param>
        public void Draw(object[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); ++i)
            {
                //Iterate through the grid  
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    //SwitchTextColor();

                    Console.Write(SetSymbol(grid[i, j]) + " ");

                    //If we found the end of the row switch color 
                    if (j == grid.GetLength(1) - 1)
                    {
                        //SwitchTextColor();
                    }
                }

                Console.WriteLine();
            }
        }
        public string SetSymbol(object obj)
        {
            string s = null;

            if (obj is Square)
            {
                s = "■";
            }
            else if (obj is Black)
            {
                s = "B";
            }
            else if (obj is White)
            {
                s = "W";
            }

            return s;
        }
    }
}
