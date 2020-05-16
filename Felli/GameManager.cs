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
        /// Accepts user input and converts it into a game action
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
                    while (input != "1" && input != "2" && input != "3" && input != "4" && input != "5" && input != "6")
                    {
                        Console.Clear();
                        r.Draw(grid);
                        r.ShowSelectPieceText(currentPlayer.Color, currentPlayer.Id);
                        input = Console.ReadLine();
                    }

                    //To start with the chosen piece
                    playingPiece = SelectPlayingPiece(input);

                    //If the playing pice is not valid 
                    if (playingPiece == null)
                    {
                        r.InvalidPieceText();
                        Console.ReadKey();
                    }
                }

                input = null;

                //A tuple with a boolean that decides if the piece moves and an integer with the quantity of steps it can move (row and col) and a bool for erase enemy.
                (bool, int, int, bool) movement = (false, 0, 0, false);


                while (movement.Item1 == false)
                {
                    //While the input is different from the possible moves
                    while (input != "1" && input != "2" && input != "3" && input != "4" && input != "6" && input != "7" && input != "8" && input != "9")
                    {
                        Console.Clear();
                        r.Draw(grid);
                        r.ShowInputMovements();
                        r.ShowPossibleDirections(grid[playingPiece.Row, playingPiece.Column].PossibleMovements, playingPiece);
                        input = Console.ReadLine();

                        movement = CheckMovement(input);

                        //if the piece movement is set to false show the invalid movement text
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
                    //
                    playingPiece.Move((Direction)Convert.ToInt32(input));
                    grid[playingPiece.Row, playingPiece.Column].Piece = null;
                    playingPiece.ResetMovement();
                    UpdatePieces();
                }
                playingPiece.Row = movement.Item2;
                playingPiece.Column = movement.Item3;

                grid[playingPiece.Row, playingPiece.Column].Piece = playingPiece;
                grid[playingPiece.PreviousRow, playingPiece.PreviousColumn].Piece = null;

                UpdateBlockedPieces();

                if (CheckWin())
                {
                    Console.Clear();
                    r.Draw(grid);
                    r.PlayerWin(currentPlayer);
                    break;
                }

                ChangeTurn();
                input = null;
                playingPiece = null;

            }
        }

        /// <summary>
        /// To create a player to play
        /// </summary>
        /// <param name="s"></param>
        private void CreatePlayers(string s)
        {
            switch (s)
            {
                //if the first player choses the white pieces
                case "W":
                    p1 = new Player(PieceColor.white, 1);
                    p2 = new Player(PieceColor.black, 2);
                    break;

                //if the first player choses the black pieces
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
                //it's the second player's turn
                currentPlayer = p2;
            }
            else
            {
                //it's the first player's turn
                currentPlayer = p1;
            }
        }
       
        /// <summary>
        /// To select the piece to play
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private Piece SelectPlayingPiece(string input)
        {
            Piece p = null;

            foreach (Square go in grid)
            {
                if (go.Piece != null)
                {
                    //Define the piece 
                    if (go.Piece.Id == Convert.ToInt32(input) && go.Piece.Color == currentPlayer.Color && go.Piece.IsBlocked == false)
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
        /// Black and White pieces victory condition verification
        /// </summary>
        /// <returns></returns>
        private bool CheckWin()
        {            
            if (p1.Color == currentPlayer.Color)
            {
                //in case player two runs out of pieces or has them all blocked
                if (p2.PieceCount == 0 || HasAllPiecesBlocked(p2.Color))
                {
                    return true;
                }
            }            
            else
            {
                //in case player one runs out of pieces or has them all blocked
                if (p1.PieceCount == 0 || HasAllPiecesBlocked(p1.Color))
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Check if the player have all pieces blocked 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private bool HasAllPiecesBlocked(PieceColor color)
        {
            bool value = false;

            //for each position in the grid check if all the pieces are blocked
            foreach (Square sq in grid)
            {
                if (sq.HasPiece())
                {
                    if (sq.Piece.Color == color)
                    {
                        if (sq.Piece.IsBlocked) value = true;
                        else
                        {
                            value = false;
                            break;
                        }
                    }
                }
            }
            return value;
        }

        /// <summary>
        /// Movement Method
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
            if (input != "1" && input != "2" && input != "3" && input != "4" && input != "6" && input != "7" && input != "8" && input != "9") return (false, 0, 0, false);
            else
            {
                //Convert input to Int and pass it as a direction  
                Direction dir = (Direction)Convert.ToInt32(input);

                //if the position where the player wants to go is free
                if (grid[playingPiece.Row, playingPiece.Column].HasDirection(dir))
                {
                    playingPiece.Move(dir);
                    Square targetSq = grid[playingPiece.Row, playingPiece.Column];
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
                        if (playingPiece.Color != targetSq.Piece.Color)
                        {
                            if (targetSq.HasDirection(dir))
                            {
                                playingPiece.Move(dir);
                                newRow = playingPiece.Row;
                                newColumn = playingPiece.Column;
                                eraseEnemy = true;

                                if (grid[playingPiece.Row, playingPiece.Column].HasPiece()) value = false;
                                else value = true;
                            }
                        }
                    }

                    playingPiece.Row = previousRow;
                    playingPiece.Column = previousColumn;
                    playingPiece.PreviousRow = previousRow;
                    playingPiece.PreviousColumn = previousColumn;
                }
                return (value, newRow, newColumn, eraseEnemy);
            }
        }

        /// <summary>
        /// Update to ckeck if is any blocked piece
        /// </summary>
        private void UpdateBlockedPieces()
        {
            //Cycle through the board
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    //If a pice exists, check if it is blocked
                    if (grid[i, j].Piece != null)
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
            //for each possible movement check if the square is blocked
            foreach (Direction d in sq.PossibleMovements)
            {
                value = CheckBlockedPositions(p, d);
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
        /// Verification of blocked positions within the board
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

            p.Move(d);

            //if the position has a piece
            if (grid[p.Row, p.Column].HasPiece())
            {
                //
                if (grid[p.Row, p.Column].Piece.Color != p.Color)
                {
                    //if the direction is possible
                    if ((grid[p.Row, p.Column].HasDirection(d)))
                    {
                        p.Move(d);
                        if (grid[p.Row, p.Column].HasPiece()) value = true;
                    }
                }
                else
                {
                    value = true;
                }
            }
            p.Row = row;
            p.Column = column;
            p.PreviousRow = previousRow;
            p.PreviousColumn = previousColumn;
            return value;
        }
    }

}

