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

        /// <summary>
        /// Method that sets the symbol on each board position
        /// </summary>
        /// <param name="square"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Method that asks the player who plays first.
        /// </summary>
        public void ShowPlayerSelection()
        {
            Console.WriteLine("Who will play first?? W for white, B for black.");
        }

        /// <summary>
        /// Method that asks the player to select a piece to play
        /// </summary>
        public void ShowSelectPieceText()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Choose the piece you want to play from 1-6.");
            Console.WriteLine();
        }

        /// <summary>
        /// Method that informs the player their choice is invalid
        /// </summary>
        public void InvalidPieceText()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("This Choice is invalid. Press any key to continue...");
            Console.WriteLine();
        }

        /// <summary>
        /// Method that informs the players that Player 2 won 
        /// </summary>
        public void Player2Win()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Player 2 won.");
            Console.WriteLine();
        }

        /// <summary>
        /// Method that informs the players that Player 1 won 
        /// </summary>
        public void Player1Win()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Player 1 won.");
            Console.WriteLine();
        }

        /// <summary>
        /// Method that shows the player the possible movements
        /// </summary>
        /// <param name="possibleMoves"></param>
        public void ShowPossibleDirections(Direction[] possibleMoves)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Possible movements:");
            foreach (Direction d in possibleMoves)
            {
                Console.Write(d + ", ");
            }
            Console.WriteLine("\n________");
            Console.WriteLine();
            Console.Write("Input: ");
        }

        public void ShowInputMovements()
        {
            Console.WriteLine("");
            Console.WriteLine(@"  7(NW)   8(N)   9(NE)");
            Console.WriteLine(@"    \      |      /");
            Console.WriteLine(@"     \     |     /");
            Console.WriteLine(@"      \    |    /");
            Console.WriteLine(@"4(W)-------------- 6(E)");
            Console.WriteLine(@"      /    |    \");
            Console.WriteLine(@"     /     |     \");
            Console.WriteLine(@"    /      |      \");
            Console.WriteLine(@"  1(SW)   2(S)   3(SE)");
            Console.WriteLine();
            Console.WriteLine("Choose an available number to move in the respective direction!");
            Console.WriteLine();
            Console.WriteLine("________");
        }


    }
}
