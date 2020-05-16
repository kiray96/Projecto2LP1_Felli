using System;

namespace Felli
{
    /// <summary>
    /// Game Manager class
    /// </summary>
    public class GameManager
    {
        /// <summary>
        /// Bidimensional object array that holds the game objects 
        /// </summary>
        public Square[,] grid = new Square[5, 5];
        private Render r;
        private Piece playingPiece;
        private Player p1, p2, currentPlayer;


        /// <summary>
        /// Game manager constructor & game startup
        /// </summary>
        /// <param name="r"></param>
        public GameManager(Render r)
        {
            this.r = r;

            //Cycle through the bidimensional array
            for (int i = 0; i < grid.GetLength(0); ++i)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    //Populate the grid with the square objects 
                    grid[i, j] = new Square(PlayableType.playable);
                }
            }
            SpawnEntities();
            SetPossibleMovements();
            
        }

        /// <summary>
        /// Spawn of the entities in the grid
        /// </summary>
        private void SpawnEntities()
        {
            //Spawn the black pieces
            grid[0, 0].Piece = new Piece(0, 0, 1, PieceColor.black);
            grid[0, 2].Piece = new Piece(0, 2, 2, PieceColor.black);
            grid[0, 4].Piece = new Piece(0, 4, 3, PieceColor.black);
            grid[1, 1].Piece = new Piece(1, 1, 4, PieceColor.black);
            grid[1, 2].Piece = new Piece(1, 2, 5, PieceColor.black);
            grid[1, 3].Piece = new Piece(1, 3, 6, PieceColor.black);

            //Spawn the white pieces
            grid[3, 1].Piece = new Piece(3, 1, 4, PieceColor.white);
            grid[3, 2].Piece = new Piece(3, 2, 5, PieceColor.white);
            grid[3, 3].Piece = new Piece(3, 3, 6, PieceColor.white);
            grid[4, 0].Piece = new Piece(4, 0, 1, PieceColor.white);
            grid[4, 2].Piece = new Piece(4, 2, 2, PieceColor.white);
            grid[4, 4].Piece = new Piece(4, 4, 3, PieceColor.white);

            //Identifying non-playable positions for rendering purposes
            grid[0, 1] = new Square(PlayableType.nonPlayable);
            grid[0, 3] = new Square(PlayableType.nonPlayable);
            grid[1, 0] = new Square(PlayableType.nonPlayable);
            grid[1, 4] = new Square(PlayableType.nonPlayable);
            grid[2, 0] = new Square(PlayableType.nonPlayable);
            grid[2, 1] = new Square(PlayableType.nonPlayable);
            grid[2, 3] = new Square(PlayableType.nonPlayable);
            grid[2, 4] = new Square(PlayableType.nonPlayable);
            grid[3, 0] = new Square(PlayableType.nonPlayable);
            grid[3, 4] = new Square(PlayableType.nonPlayable);
            grid[4, 1] = new Square(PlayableType.nonPlayable);
            grid[4, 3] = new Square(PlayableType.nonPlayable);

        }

        /// <summary>
        /// Set the first player acording to the players input
        /// </summary>
        public void SetPlayers()
        {
            string input = null;

            //Converts the player's correct input to uppercase
            while (input != "W" && input != "B")
            {
                Console.Clear();
                r.ShowPlayerSelection();
                input = Console.ReadLine().ToUpper();
            }

            CreatePlayers(input);
        }

        /// <summary>
        ///  Whole game loop, accepts user input, converts it into a game action 
        /// and also verifies winner 
        /// </summary>
        public void GameLoop()
        {
            string input = null;
            currentPlayer = p1;

            UpdateBlockedPieces();

            //Infinite loop
            while (true)
            {

                while (playingPiece == null)
                {
                    input = null;
                    //While the input is different from the possible moves
                    while (input != "1" && input != "2" && input != "3" 
                    && input != "4" && input != "5" && input != "6")
                    {
                        Console.Clear();
                        r.Draw(grid);
                        r.ShowSelectPieceText(currentPlayer.Color, currentPlayer.Id);
                        input = Console.ReadLine();
                    }

                    //Recieve piece depending on input
                    playingPiece = SelectPlayingPiece(input);

                    //If the playing pice is not valid 
                    if (playingPiece == null)
                    {
                        r.InvalidPieceText();
                        Console.ReadKey();
                    }
                }

                input = null;

                //A tuple with a boolean that decides if the piece moves and two 
                //integers with the quantity of steps it can move (row and col) 
                //and a bool for erasing enemy.
                (bool, int, int, bool) movement = (false, 0, 0, false);

                //While movement is invalid
                while (movement.Item1 == false)
                {
                    //While the input is different from the possible moves
                    while (input != "1" && input != "2" && input != "3" 
                    && input != "4" && input != "6" && input != "7" 
                    && input != "8" && input != "9")
                    {
                        Console.Clear();
                        r.Draw(grid);
                        r.ShowInputMovements();
                        r.ShowPossibleDirections(grid[playingPiece.Row, 
                        playingPiece.Column].PossibleMovements, playingPiece);
                        input = Console.ReadLine();

                        movement = CheckMovement(input);

                        //if the piece movement is set to false show the invalid 
                        //movement text
                        if (!movement.Item1)
                        {
                            r.InvalidMovementText();
                            input = null;
                            Console.ReadKey();
                        }

                    }

                }

                //If erasing enemy is set to true
                if (movement.Item4)
                {
                    //Fake movement to enemy position
                    playingPiece.Move((Direction)Convert.ToInt32(input));
                    //Erase enemy 
                    grid[playingPiece.Row, playingPiece.Column].Piece = null;
                    //Reset piece movement
                    playingPiece.ResetMovement();
                    UpdatePieces();
                }

                //Set piece new position depending on row column retrieved from 
                //the movement tuple 
                playingPiece.Row = movement.Item2;
                playingPiece.Column = movement.Item3;

                //Put piece on the grid and erase previous piece position
                grid[playingPiece.Row, playingPiece.Column].Piece = playingPiece;
                grid[playingPiece.PreviousRow, playingPiece.PreviousColumn].Piece = null;

                UpdateBlockedPieces();


                //If a winner is found, show winning message and break out of 
                //the game loop
                if (CheckWin())
                {
                    Console.Clear();
                    r.Draw(grid);
                    r.PlayerWin(currentPlayer);
                    break;
                }

                //Change turn and reset inputs 
                ChangeTurn();
                input = null;
                playingPiece = null;

            }
        }

        /// <summary>
        /// Create p1 and p2
        /// </summary>
        /// <param name="s"></param>
        private void CreatePlayers(string s)
        {
            switch (s)
            {
                //If the first player choses the white pieces
                case "W":
                    p1 = new Player(PieceColor.white, 1);
                    p2 = new Player(PieceColor.black, 2);
                    break;

                //If the first player choses the black pieces
                case "B":
                    p1 = new Player(PieceColor.black, 1);
                    p2 = new Player(PieceColor.white, 2);
                    break;
            }

        }

        /// <summary>
        /// Method to change the turn
        /// </summary>
        private void ChangeTurn()
        {
            if (currentPlayer == p1)
            {
                //It's the second player's turn
                currentPlayer = p2;
            }
            else
            {
                //It's the first player's turn
                currentPlayer = p1;
            }
        }
       
        /// <summary>
        /// Select the piece to play with depending on user input 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private Piece SelectPlayingPiece(string input)
        {
            Piece p = null;

            //Cycle through the grid
            foreach (Square go in grid)
            {
                //If the square has a piece 
                if (go.HasPiece())
                {
                    //Define the piece if its valid
                    if (go.Piece.Id == Convert.ToInt32(input) 
                    && go.Piece.Color == currentPlayer.Color 
                    && go.Piece.IsBlocked == false)
                    {
                        p = go.Piece;
                    }
                }
            }
            return p;
        }

        /// <summary>
        /// Set the possible movements in each position 
        /// </summary>
        private void SetPossibleMovements()
        {
            grid[0, 0].PossibleMovements
                = new Direction[] { Direction.E, Direction.SE };
            grid[0, 2].PossibleMovements
                = new Direction[] { Direction.S, Direction.E, Direction.W };
            grid[0, 4].PossibleMovements
                = new Direction[] { Direction.W, Direction.SW };
            grid[1, 1].PossibleMovements
                = new Direction[] { Direction.NW, Direction.E, Direction.SE };
            grid[1, 2].PossibleMovements
                = new Direction[] { Direction.N, Direction.S, Direction.E, Direction.W };
            grid[1, 3].PossibleMovements
                = new Direction[] { Direction.NE, Direction.W, Direction.SW };
            grid[2, 2].PossibleMovements
                = new Direction[] { Direction.NE, Direction.N, Direction.NW,
                    Direction.SW, Direction.S, Direction.SE };
            grid[3, 1].PossibleMovements
                = new Direction[] { Direction.NE, Direction.E, Direction.SW };
            grid[3, 2].PossibleMovements
                = new Direction[] { Direction.N, Direction.S, Direction.E, Direction.W };
            grid[3, 3].PossibleMovements
                = new Direction[] { Direction.NW, Direction.W, Direction.SE };
            grid[4, 0].PossibleMovements
                = new Direction[] { Direction.NE, Direction.E };
            grid[4, 2].PossibleMovements
                = new Direction[] { Direction.W, Direction.E, Direction.N };
            grid[4, 4].PossibleMovements
                = new Direction[] { Direction.W, Direction.NW };
        }

        /// <summary>
        /// Victory condition verification
        /// </summary>
        /// <returns></returns>
        private bool CheckWin()
        {            
            if (p1.Color == currentPlayer.Color)
            {
                //In case player two runs out of pieces or has them all blocked
                if (p2.PieceCount == 0 || HasAllPiecesBlocked(p2.Color))
                {
                    return true;
                }
            }            
            else
            {
                //In case player one runs out of pieces or has them all blocked
                if (p1.PieceCount == 0 || HasAllPiecesBlocked(p1.Color))
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Check if the player has all pieces blocked 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private bool HasAllPiecesBlocked(PieceColor color)
        {
            bool value = false;

            //cycle through the grid to find pieces 
            foreach (Square sq in grid)
            {
                if (sq.HasPiece())
                {
                    //if piece is of the same color we want to check 
                    if (sq.Piece.Color == color)
                    {
                        if (sq.Piece.IsBlocked) value = true;
                        else
                        {
                            //return false and break out of foreach since player 
                            //can still move 
                            value = false;
                            break;
                        }
                    }
                }
            }
            return value;
        }

        /// <summary>
        /// Return valid or invalid movement
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private (bool, int, int, bool) CheckMovement(string input)
        {
            bool value = false;
            int previousRow = 0;
            int previousColumn = 0;
            int newRow = 0;
            int newColumn = 0;
            bool eraseEnemy = false;

            ////While the input is different from the possible moves
            if (input != "1" && input != "2" && input != "3" && input != "4" 
            && input != "6" && input != "7" && input != "8" && input != "9") 
            return (false, 0, 0, false);
            else
            {
                //Convert input to Int and pass it as a direction  
                Direction dir = (Direction)Convert.ToInt32(input);

                //If the position where the player is has the chosen direction
                if (grid[playingPiece.Row, playingPiece.Column].HasDirection(dir))
                {
                    playingPiece.Move(dir);
                    Square targetSq = grid[playingPiece.Row, playingPiece.Column];
                    //Store previous position values 
                    previousRow = playingPiece.PreviousRow;
                    previousColumn = playingPiece.PreviousColumn;
                    newRow = playingPiece.Row;
                    newColumn = playingPiece.Column;

                    // If the target square doesn't have a piece
                    if (!targetSq.HasPiece())
                    {
                        value = true;
                    }                    
                    else
                    {
                        //In case target square piece color is different from the 
                        //one playing  
                        if (playingPiece.Color != targetSq.Piece.Color)
                        {
                            //if the target square has the same possible direction 
                            //chosen by the player
                            if (targetSq.HasDirection(dir))
                            {
                                //Fake movement in the specified direction
                                playingPiece.Move(dir);
                                newRow = playingPiece.Row;
                                newColumn = playingPiece.Column;
                                eraseEnemy = true;

                                //If the square after the target square has also 
                                //a piece, then movement is invalid  
                                if (grid[playingPiece.Row, playingPiece.Column].HasPiece()) value = false;
                                else value = true;
                            }
                        }
                    }

                    //Reset values since this is only for verifications 
                    playingPiece.Row = previousRow;
                    playingPiece.Column = previousColumn;
                    playingPiece.PreviousRow = previousRow;
                    playingPiece.PreviousColumn = previousColumn;
                }
                //Return movement validity, new positions to move and erasing 
                //enemy value
                return (value, newRow, newColumn, eraseEnemy);
            }
        }

        /// <summary>
        /// Update blocked state in all pieces
        /// </summary>
        private void UpdateBlockedPieces()
        {
            //Cycle through the board
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    //If a piece exists, check if it is blocked
                    if (grid[i, j].HasPiece())
                    {
                        grid[i, j].Piece.IsBlocked = CheckPieceBlock(grid[i, j], grid[i, j].Piece);
                    }
                }
            }
        }

        /// <summary>
        /// To check if the piece is blocked
        /// </summary>
        /// <param name="sq"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool CheckPieceBlock(Square sq, Piece p)
        {
            bool value = false;
            //for each possible movement check if the piece is blocked
            foreach (Direction d in sq.PossibleMovements)
            {
                value = CheckBlockedPositions(p, d);
                //If a direction is not blocked, there's no need to check the rest
                if (value == false) break;
            }
            return value;
        }

        /// <summary>
        /// To update the remain pieces
        /// </summary>
        private void UpdatePieces()
        {
            if (currentPlayer == p1) p2.PieceCount--;
            else p1.PieceCount--;
        }

        /// <summary>
        /// Verification of blocked directions within the board
        /// </summary>
        /// <param name="p"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        private bool CheckBlockedPositions(Piece p, Direction d)
        {
            int row = p.Row;
            int column = p.Column;
            int previousRow = p.PreviousRow;
            int previousColumn = p.PreviousColumn;
            bool value = false;

            //Fake move piece
            p.Move(d);

            //If the target position has a piece
            if (grid[p.Row, p.Column].HasPiece())
            {
                //If target position color is diferent from the piece being checked 
                if (grid[p.Row, p.Column].Piece.Color != p.Color)
                {
                    //If target position has the same possible direction  
                    if ((grid[p.Row, p.Column].HasDirection(d)))
                    {
                        //Fake move piece
                        p.Move(d);
                        //If position after target piece has also a piece  
                        if (grid[p.Row, p.Column].HasPiece()) value = true;
                    }
                }
                else
                {
                    value = true;
                }
            }
            //Reset values
            p.Row = row;
            p.Column = column;
            p.PreviousRow = previousRow;
            p.PreviousColumn = previousColumn;
            
            return value;
        }
    }

}

