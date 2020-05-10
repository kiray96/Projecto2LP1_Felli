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
                Console.ForegroundColor = ConsoleColor.White;
                s = "■";
            }

            else if (obj is Black)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Black black = (Black)obj;
                switch (black.Id)
                {
                    case 1:
                        s = "1";
                        break;

                    case 2:
                        s = "2";
                        break;

                    case 3:
                        s = "3";
                        break;

                    case 4:
                        s = "4";
                        break;

                    case 5:
                        s = "5";
                        break;

                    case 6:
                        s = "6";
                        break;
                }
            }

            else if (obj is White)
            {
                Console.ForegroundColor = ConsoleColor.White;
                White white = (White)obj;
                switch (white.Id)
                {
                    case 1:
                        s = "1";
                        break;

                    case 2:
                        s = "2";
                        break;

                    case 3:
                        s = "3";
                        break;

                    case 4:
                        s = "4";
                        break;

                    case 5:
                        s = "5";
                        break;

                    case 6:
                        s = "6";
                        break;
                }
            }
            else if (obj is Blank)
            {
                s = "-";
            }

            return s;
        }
    }
}
