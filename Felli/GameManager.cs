using System;
using System.Collections.Generic;
using System.Text;

namespace Felli
{
    public class GameManager
    {
        /// <summary>
        /// Bidimensional object array that holds the game objects 
        /// </summary>
        public Square[,] grid = new Square[5, 3];

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
            SetPlayers();
            GameLoop();
        }

        /// <summary>
        /// Spawn of the entities in the grid
        /// </summary>
        private void SpawnEntities()
        {
            //Spawn the black pieces
            grid[0, 0].Piece = new Piece(0, 0, 1, PieceColor.black);
            grid[0, 1].Piece = new Piece(0, 1, 2, PieceColor.black);
            grid[0, 2].Piece = new Piece(0, 2, 3, PieceColor.black);
            grid[1, 0].Piece = new Piece(1, 0, 4, PieceColor.black);
            grid[1, 1].Piece = new Piece(1, 1, 5, PieceColor.black);
            grid[1, 2].Piece = new Piece(1, 2, 6, PieceColor.black);

            //Spawn the white pieces
            grid[3, 0].Piece = new Piece(3, 0, 4, PieceColor.white);
            grid[3, 1].Piece = new Piece(3, 1, 5, PieceColor.white);
            grid[3, 2].Piece = new Piece(3, 2, 6, PieceColor.white);
            grid[4, 0].Piece = new Piece(4, 0, 1, PieceColor.white);
            grid[4, 1].Piece = new Piece(4, 1, 2, PieceColor.white);
            grid[4, 2].Piece = new Piece(4, 2, 3, PieceColor.white);

            //Identifying non-playable positions for rendering purposes
            grid[2, 0] = new Square(PlayableType.nonPlayable);
            grid[2, 2] = new Square(PlayableType.nonPlayable);

        }

        private void SetPlayers()
        {
            string input = null;

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
        private void GameLoop()
        {
            string input = null;
            currentPlayer = p1;

            //Infinite loop
            while (true)
            {
                //UpdateBlockedPieces();

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

                    playingPiece = SelectPlayingPiece(input);

                    if (playingPiece == null)
                    {
                        r.InvalidPieceText();
                        Console.ReadKey();
                    }
                }

                input = null;

                //A tuple with a boolean that decides if the piece moves and an integer with the quantity of steps it can move (row and col) and a bool for erase enemy.
                (bool, int, int, bool) canMove = (false, 0, 0, false);

                while (canMove.Item1 == false)
                {
                    while (input != "1" && input != "2" && input != "3" && input != "4" && input != "6" && input != "7" && input != "8" && input != "9")
                    {
                        Console.Clear();
                        r.Draw(grid);
                        r.ShowInputMovements();
                        r.ShowPossibleDirections(grid[playingPiece.Row, playingPiece.Column].PossibleMovements);
                        input = Console.ReadLine();

                        canMove = CheckMovement(input);

                        if (!canMove.Item1)
                        {
                            r.InvalidMovementText();
                            input = null;
                            Console.ReadKey();
                        }

                    }

                }

                if (canMove.Item4)
                {
                    playingPiece.Move((Direction)Convert.ToInt32(input));
                    grid[playingPiece.Row, playingPiece.Column].Piece = null;
                    playingPiece.ResetMovement();
                    UpdatePieces();
                }
                playingPiece.Row = canMove.Item2;
                playingPiece.Column = canMove.Item3;
                //If erasing enemy is set to true

                grid[playingPiece.Row, playingPiece.Column].Piece = playingPiece;
                grid[playingPiece.PreviousRow, playingPiece.PreviousColumn].Piece = null;



                //Fazer Check win

                ChangeTurn();
                input = null;
                playingPiece = null;

            }
        }

        private void CreatePlayers(string s)
        {
            switch (s)
            {
                case "W":
                    p1 = new Player(PieceColor.white, 1);
                    p2 = new Player(PieceColor.black, 2);
                    break;

                case "B":
                    p1 = new Player(PieceColor.black, 1);
                    p2 = new Player(PieceColor.white, 2);
                    break;
            }

        }

        private void ChangeTurn()
        {
            if (currentPlayer == p1)
            {
                currentPlayer = p2;
            }
            else
            {
                currentPlayer = p1;
            }
        }


        private void SelectPlayer(string s)
        {
            switch (s)
            {
                case "W":
                    p1 = new Player(PieceColor.white, 1);
                    p2 = new Player(PieceColor.black, 2);
                    break;
                case "B":
                    p1 = new Player(PieceColor.black, 1);
                    p2 = new Player(PieceColor.white, 2);
                    break;
            }
        }

        private Piece SelectPlayingPiece(string input)
        {
            Piece p = null;

            foreach (Square go in grid)
            {
                if (go.Piece != null)
                {
                    //Definir a peça 
                    if (go.Piece.Id == Convert.ToInt32(input) && go.Piece.Color == currentPlayer.Color && go.Piece.IsBlocked == false)
                    {
                        p = go.Piece;
                    }
                }
            }
            return p;
        }

        private void SetPossibleMovements()
        {
            grid[0, 0].PossibleMovements
                = new Direction[] { Direction.E, Direction.S };
            grid[0, 1].PossibleMovements
                = new Direction[] { Direction.S, Direction.E, Direction.W };
            grid[0, 2].PossibleMovements
                = new Direction[] { Direction.W, Direction.S };
            grid[1, 0].PossibleMovements
                = new Direction[] { Direction.N, Direction.E, };
            grid[1, 1].PossibleMovements
                = new Direction[] { Direction.N, Direction.S, Direction.E, Direction.W };
            grid[1, 2].PossibleMovements
                = new Direction[] { Direction.N, Direction.W, Direction.SW };
            grid[2, 1].PossibleMovements
                = new Direction[] { Direction.NE, Direction.N, Direction.NW,
                    Direction.SW, Direction.S, Direction.SE };
            grid[3, 0].PossibleMovements
                = new Direction[] { Direction.NE, Direction.E, Direction.S };
            grid[3, 1].PossibleMovements
                = new Direction[] { Direction.N, Direction.S, Direction.E, Direction.W };
            grid[3, 2].PossibleMovements
                = new Direction[] { Direction.NW, Direction.W, Direction.S };
            grid[4, 0].PossibleMovements
                = new Direction[] { Direction.N, Direction.E };
            grid[4, 1].PossibleMovements
                = new Direction[] { Direction.W, Direction.E, Direction.N };
            grid[4, 2].PossibleMovements
                = new Direction[] { Direction.W, Direction.N };
        }

        private bool CheckWin()
        {
            if (p1.Color == currentPlayer.Color)
            {
                if (p1.PieceCount == 0)
                {
                    r.Player2Win();
                    return true;
                }
            }
            else
            {
                if (p2.PieceCount == 0)
                {
                    r.Player1Win();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Movement Method
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private (bool, int, int, bool) CheckMovement(string input)
        {
            bool value = false;
            int row = 0;
            int column = 0;
            int newRow = 0;
            int newColumn = 0;
            bool eraseEnemy = false;

            if (input != "1" && input != "2" && input != "3" && input != "4" && input != "6" && input != "7" && input != "8" && input != "9") return (false, 0, 0, false);
            else
            {
                Direction dir = (Direction)Convert.ToInt32(input);

                if (grid[playingPiece.Row, playingPiece.Column].HasDirection(dir))
                {
                    playingPiece.Move(dir);
                    Square targetSq = grid[playingPiece.Row, playingPiece.Column];
                    row = playingPiece.PreviousRow;
                    column = playingPiece.PreviousColumn;
                    newRow = playingPiece.Row;
                    newColumn = playingPiece.Column;

                    // If the target square doesn't have a piece
                    if (!targetSq.HasPiece())
                    {
                        value = true;
                    }
                    else
                    {
                        if (playingPiece.Color == targetSq.Piece.Color)
                        {
                            value = false;
                        }
                        else
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
                            else
                            {
                                if (targetSq.HasSimilarDirection(dir))
                                {
                                    Direction d = targetSq.GetSimilarDirection(dir);
                                    playingPiece.Move(d);
                                    newRow = playingPiece.Row;
                                    newColumn = playingPiece.Column;
                                    eraseEnemy = true;

                                    if (grid[playingPiece.Row, playingPiece.Column].HasPiece()) value = false;
                                    else value = true;
                                }
                                else value = false;
                            }
                        }
                    }

                    playingPiece.Row = row;
                    playingPiece.Column = column;
                    playingPiece.PreviousRow = row;
                    playingPiece.PreviousColumn = column;
                }
                else value = false;
                return (value, newRow, newColumn, eraseEnemy);
            }
        }


        /*private bool CheckMovement()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j].Piece.Id == playingPiece.Id && grid[i, j].Piece.Color == playingPiece.Color)
                    {
                        while (true)
                        {
                            r.ShowPossibleDirections(grid[i, j].PossibleMovements);
                            string selectedMovement = Console.ReadLine();
                            foreach (Direction d in grid[i, j].PossibleMovements)
                            {
                                if (Direction.IsDefined(typeof(Direction), selectedMovement.ToUpper()))
                                {
                                    if (selectedMovement.ToUpper() == d.ToString())
                                    {
                                        playingPiece.Move(d);
                                        return true;
                                    }
                                }
                            }
                            r.InvalidPieceText();
                        }
                    }
                }
            }
            return false;
        }
        */

        private void UpdateBlockedPieces()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    //Se existe uma peça, verifica se está bloqueada
                    if (grid[i, j].Piece != null)
                    {
                        grid[i, j].Piece.IsBlocked = CheckPieceBlock(grid[i, j], grid[i, j].Piece);
                    }
                }
            }
        }

        private bool CheckPieceBlock(Square sq, Piece p)
        {
            bool value = false;

            foreach (Direction d in sq.PossibleMovements)
            {
                switch (d)
                {
                    case Direction.E:
                        value = (grid[p.Row, p.Column + 1].HasPiece());
                        if (value == false) return false;
                        break;
                    case Direction.N:
                        value = (grid[p.Row - 1, p.Column].HasPiece());
                        if (value == false) return false;
                        break;
                    case Direction.NE:
                        value = (grid[p.Row - 1, p.Column + 1].HasPiece());
                        if (value == false) return false;
                        break;
                    case Direction.NW:
                        value = (grid[p.Row - 1, p.Column - 1].HasPiece());
                        if (value == false) return false;
                        break;
                    case Direction.S:
                        value = (grid[p.Row + 1, p.Column].HasPiece());
                        if (value == false) return false;
                        break;
                    case Direction.SE:
                        value = (grid[p.Row + 1, p.Column + 1].HasPiece());
                        if (value == false) return false;
                        break;
                    case Direction.SW:
                        value = (grid[p.Row + 1, p.Column - 1].HasPiece());
                        if (value == false) return false;
                        break;
                    case Direction.W:
                        value = (grid[p.Row, p.Column - 1].HasPiece());
                        if (value == false) return false;
                        break;
                }
            }
            return value;
        }
        private void UpdatePieces()
        {
            if (currentPlayer == p1) p2.PieceCount--;
            else p1.PieceCount--;
        }
    }

}

