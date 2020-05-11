﻿using System;
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
        public void Draw(Square[,] grid)
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
        public string SetSymbol(Square square)
        {
            string s = null;

            if (square.Piece == null)
            {
                Console.ForegroundColor = ConsoleColor.White;
                s = (square.Type == PlayableType.playable) ? "." : " ";
            }
            else
            {
                Piece piece = square.Piece;
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
            Console.WriteLine("Who will play first?? W for white, B for black.");
        }

        public void ShowSelectPieceText()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Choose the piece you want to play from 1-6.");
            Console.WriteLine();
        }

        public void InvalidPieceText()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("This Choice is invalid.");
            Console.WriteLine();
        }

        public void Player2Win()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Player 2 won.");
            Console.WriteLine();
        }

        public void Player1Win()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Player 1 won.");
            Console.WriteLine();
        }

        public void PlayerMove(Direction[] possibleMoves)
        {
            Console.WriteLine("Possible movements: ");
            foreach (Direction d in possibleMoves)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine(d.ToString());
                Console.WriteLine();
            }

            
        }
    }
}
