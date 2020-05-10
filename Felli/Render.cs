using System;
using System.Collections.Generic;
using System.Text;

namespace Felli
{
    public class Render
    {

        /// <summary>
        /// Draw the grid on the console
        /// </summary>
        /// <param name="grid"></param>
        public void Draw(IGameObject[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); ++i)
            {
                if (i % 2 != 0) Console.Write("\t");

                //Iterate through the grid  
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    //SwitchTextColor();

                    if (i % 2 == 0)
                    {
                        Console.Write(SetSymbol(grid[i, j]) + "\t\t");
                    }
                    else
                    {
                        Console.Write(SetSymbol(grid[i, j]) + "\t");
                    }

                    //If we found the end of the row switch color 
                    if (j == grid.GetLength(1) - 1)
                    {
                        //SwitchTextColor();
                    }
                }

                Console.WriteLine("\n\n");
            }
        }
        public string SetSymbol(IGameObject obj)
        {
            string s = null;

            if (obj is Square)
            {
                Square square = obj as Square;

                Console.ForegroundColor = ConsoleColor.White;
                s = (square.Type == PlayableType.playable) ? "." : " ";
            }

            else if (obj is Piece)
            {
                Piece piece = obj as Piece;
                Console.ForegroundColor = (
                    piece.Color == PieceColor.black)
                    ? ConsoleColor.DarkGray : ConsoleColor.White;

                switch (piece.Id)
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

            return s;
        }

        public void ShowPlayerSelection()
        {
            Console.WriteLine("Quem joga primeiro? B para brancas, P para pretas.");
        }
    }
}
